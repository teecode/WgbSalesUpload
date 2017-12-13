using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WgbSalesUpload.CRUD;

namespace WgbSalesUpload.MASTER_UI.com.smartdev.wgb.BettingSuite
{
    public class BalanceProcessing
    {
        string connectionString = "";
        HttpPostedFile myFile;
        DataTable dtExcelRecords = null;
        transaction_category creditbalance;
        transaction_category debitbalance;
        BettingSuiteDataContext db = new BettingSuiteDataContext();



        public BalanceProcessing(HttpPostedFile myFile,HttpServerUtility Server)
        {
            this.myFile = myFile;
            if(validateUploadData(Server))
                dtExcelRecords = displayUploadedData(Server);
        }

        public BalanceProcessing()
        {
            creditbalance = db.transaction_categories.SingleOrDefault(a => a.code == "CBF");
            debitbalance = db.transaction_categories.SingleOrDefault(a => a.code == "DBF");
        }

        public bool validateUploadData(HttpServerUtility Server)
        {

            string fileName = Path.GetFileName(myFile.FileName);
            string fileExtension = Path.GetExtension(myFile.FileName);
            string fileLocation = Server.MapPath("~/App_Data/" + fileName);

            if (File.Exists(fileExtension))
                File.Delete(fileLocation);

            myFile.SaveAs(fileLocation);

           

            //Check whether file extension is xls or xslx

            if (fileExtension == ".xls")
            {
                // connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;\"";
            }
            else if (fileExtension == ".xlsx")
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                // connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + @";Extended Properties=" + Convert.ToChar(34).ToString() + @"Excel 8.0;Imex=1;HDR=Yes;" + Convert.ToChar(34).ToString();
            }

            int nFileLen = myFile.ContentLength;
            if (nFileLen == 0)
            {
                throw new ArgumentNullException("No file was uploaded");

            }


            // Check file extension (must be JPG)
            if (!(System.IO.Path.GetExtension(myFile.FileName).ToLower() == ".xls" || System.IO.Path.GetExtension(myFile.FileName).ToLower() == ".xlsx"))
            {
                throw new ArgumentNullException("Not a Valid Excel File");
            }


            return true;

        }

        public DataTable displayUploadedData(HttpServerUtility Server)
        {

            //Create OleDB Connection and OleDb Command

            OleDbConnection con = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = con;
            OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
            dtExcelRecords = new DataTable();
            con.Open();
            DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
            cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
            dAdapter.SelectCommand = cmd;
            dAdapter.Fill(dtExcelRecords);
            dtExcelRecords = dtExcelRecords.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull || string.Compare((field as string).Trim(), string.Empty) == 0)).CopyToDataTable();
            con.Close();
            try
            {
                File.Delete(Server.MapPath("~/App_Data/" + myFile.FileName));
            }
            catch
            {

            }



            con.Close();
            return dtExcelRecords;
        }

        public List<master_balance_sheet> fillDataToList()
        {
            if (dtExcelRecords == null)
            {
                return null;
            }
            else
            {
                List<master_balance_sheet> balance = new List<master_balance_sheet>();

                //sho shophelper = new shop_crud();
                //shop shoped;
                foreach (DataRow row in dtExcelRecords.Rows)
                {
                    int rowindex = dtExcelRecords.Rows.IndexOf(row);
                    //if (dtExcelRecords.Rows.IndexOf(row) == 0)
                    //    continue;
                    //if (dtExcelRecords.Rows.IndexOf(row) == dtExcelRecords.Rows.Count - 1)
                    //    break;
                    // 
                    var values = row.ItemArray;
                    master_balance_sheet bal;
                    
                    // shop = new shop { id = -1, shop_code = (string)values[1] };// new shop_crud().find_shop_By_Code((string)values[1]) != null ? shophelper.find_shop_By_Code((string)values[1]) : new shop {id=-1, shop_code=(string) values[1]};
                    try
                    {
                        shop shop = new CRUD.shop_crud().find_shop_By_Code(values[1].ToString());
                        bal = new master_balance_sheet
                      {
                          App_id = (shop!=null)?shop.shop_code :"NO RECORD",
                          sales_balance = (values[2]!= null)?decimal.Parse(values[2].ToString()):0,
                          credit_balance =0,
                          netbalance = (values[2] != null) ? decimal.Parse(values[2].ToString()) : 0,
                          online_credit =0,
                          shop = (shop!=null)?shop.id: -1
                      };
                        balance.Add(bal);
                        
                    }


                    catch (Exception )
                    {

                    }

                }

                return balance;
            }

        }

        public int SaveBalanceList(List<master_balance_sheet> balance, int login,DateTime date)
        {
            int ret = 0;
            try
            {

                foreach (master_balance_sheet bals in balance)
                {
                    master_balance_sheet bal = bals;
                    if (bal.shop != -1)
                    {
                        master_balance_sheet existingbal = new CRUD.master_balance_sheet_crud().find_master_balance_sheet_By_ShopId(bal.shop.Value);

                        if (existingbal == null)
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                master_transaction transaction = new master_transaction
                                {
                                    app_id = Application().app_id,
                                    amount = bal.netbalance,
                                    balance_before = existingbal.netbalance,
                                    balance_after = Decimal.Add(bal.netbalance.Value, existingbal.netbalance.Value),
                                    description = "Balance B/D AS AT" + date.ToShortDateString(),
                                    details = "Balance B/D AS AT " + date.ToShortDateString(),
                                    posted_by = login,
                                    shop = bal.shop,
                                    trans_category = (bal.netbalance >= 0) ? creditbalance.id : debitbalance.id,
                                    trans_timestamp = DateTime.Now,
                                    trans_type = (bal.netbalance >= 0) ? 0 : 1
                                };
                                db.master_transactions.InsertOnSubmit(transaction);
                                db.master_balance_sheets.InsertOnSubmit(bal);

                                scope.Complete();
                            }
                        }
                        
                        else // (existingbal.netbalance != 0 || existingbal.sales_balance != 0)
                        {
                            using (TransactionScope scope = new TransactionScope())
                            {
                                master_transaction transaction = new master_transaction
                                {
                                    app_id = Application().app_id,
                                    amount = bal.netbalance,
                                    balance_before = existingbal.netbalance,
                                    balance_after = Decimal.Add(bal.netbalance.Value, existingbal.netbalance.Value),
                                    description = "Balance B/D AS AT" + date.ToShortDateString(),
                                    details = "Balance B/D AS AT " + date.ToShortDateString(),
                                    posted_by = login,
                                    shop = bal.shop,
                                    trans_category = (bal.netbalance >= 0) ? creditbalance.id : debitbalance.id,
                                    trans_timestamp = DateTime.Now,
                                    trans_type = (bal.netbalance >= 0) ? 0 : 1
                                };
                                db.master_transactions.InsertOnSubmit(transaction);
                                

                            bal.id = existingbal.id;
                            bal.sales_balance = Decimal.Add( bal.sales_balance.Value, existingbal.sales_balance.Value);
                            bal.netbalance = Decimal.Add(bal.netbalance.Value, existingbal.netbalance.Value);

                            master_balance_sheet prev_master_balance_sheet = db.master_balance_sheets.SingleOrDefault(a => a.id == bal.id);
                            prev_master_balance_sheet.netbalance = bal.netbalance;
                            prev_master_balance_sheet.sales_balance = bal.sales_balance;
                            db.SubmitChanges();

                            

                            scope.Complete();
                            }
                        }//else if (existingbal.netbalance == 0 && existingbal.sales_balance == 0)
                        //{
                        //    bal = new CRUD.master_balance_sheet_crud(bal).alter_master_balance_sheet();
                        //}

                    }
                    else
                        ret ++;
                }
            }
            catch (Exception)
            {
                ret++;
            }
            return ret;
        }

        public application Application()
        {
            application app = new application
            {
                app_id = Guid.NewGuid().ToString()
            };
            db.applications.InsertOnSubmit(app);
            return app;
        }
    }
}