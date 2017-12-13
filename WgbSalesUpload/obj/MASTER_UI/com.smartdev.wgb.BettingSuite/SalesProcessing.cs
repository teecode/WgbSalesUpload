using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WgbSalesUpload.CRUD;

namespace WgbSalesUpload.MASTER_UI.com.smartdev.wgb.BettingSuite
{

    public class SalesProcessing
    {
        string connectionString = "";
        HttpPostedFile myFile;
        DataTable dtExcelRecords = null;
        public SalesProcessing(HttpPostedFile myFile)
        {
            this.myFile = myFile;
        }

        public bool validateUploadData(HttpServerUtility Server)
        {




            string fileName = Path.GetFileName(myFile.FileName);
            string fileExtension = Path.GetExtension(myFile.FileName);
            string fileLocation = Server.MapPath("~/App_Data/" + fileName);
            myFile.SaveAs(fileLocation);

            //Check whether file extension is xls or xslx

            if (fileExtension == ".xls")
            {
                //connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;\"";
            }
            else if (fileExtension == ".xlsx")
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                 //connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + @";Extended Properties=" + Convert.ToChar(34).ToString() + @"Excel 8.0;Imex=1;HDR=Yes;" + Convert.ToChar(34).ToString();
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

        public DataTable displayUploadedSBData(HttpServerUtility Server)
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

        public DataTable displayUploadedLuckyBallsData(HttpServerUtility Server)
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

        //Adjust accordingly to Lucky Balls Excel format
        public List<master_luckyballs_sale> fillLuckyBallsDataToList(DateTime date)

        {
            List<shop> shops = new CRUD.shop_crud().getallShop().ToList<shop>();
            if (dtExcelRecords == null)
            {
                return null;
            }
            else
            {
                List<master_luckyballs_sale> sales = new List<master_luckyballs_sale>();

                shop_crud shophelper = new shop_crud();
                //shop shoped;
                foreach (DataRow row in dtExcelRecords.Rows)
                {
                    int rowindex = dtExcelRecords.Rows.IndexOf(row);
                    //if(dtExcelRecords.Rows.IndexOf(row) == 0)
                    //    continue;
                    if (dtExcelRecords.Rows.IndexOf(row) == dtExcelRecords.Rows.Count - 1)
                        break;
                    // 
                    var values = row.ItemArray;
                    master_luckyballs_sale sale;
                    string excelshopcode = (string)values[0];
                    string excelshopcodeINT = excelshopcode.Substring(0, excelshopcode.IndexOf('-')).ToString().Trim();
                    string excelshopcodeString = excelshopcode.Substring(excelshopcode.IndexOf('-')+1).ToString().Trim();
                    int errorcount = -1;
                    try
                    {
                        int excelshopid = int.Parse(excelshopcodeINT.ToString());
                        shop shoped = shops.SingleOrDefault(a => a.id == excelshopid && a.shop_code == excelshopcodeString);// new shop_crud().find_shop_By_Code((string)values[1]) != null ? shophelper.find_shop_By_Code((string)values[1]) : new shop {id=-1, shop_code=(string) values[1]};
                        

                        sale = new master_luckyballs_sale
                        {
                            // shop1 = (shop_ != null) ? shop_ : null,
                            //((shophelper.find_shop_By_Code((string)values[1])) != null) ? shophelper.find_shop_By_Code((string)values[1]) : null,
                            //(string)values[1]) != null) ? shophelper.find_shop_By_Code((string)values[1]) : null,
                            shop1 = (shoped != null) ? shoped : new shop { shop_code = values[1].ToString(), app_id = values[1].ToString() },
                            // shop = shoped.id,//shoped.id,
                            //app_id = values[1].ToString(),//temp string used to display shop in list view
                            date = date,
                            stake = Decimal.Parse(values[3].ToString()),
                            winnings = Decimal.Parse(values[5].ToString()),
                            cancelled = Decimal.Zero,
                            single_stake_sales = Decimal.Zero,
                            isverified = false,
                            islocked = false,
                            date_posted = DateTime.Today,

                        };

                        // sale.shop1 = ((shophelper.find_shop_By_Code((string)values[1])) != null) ? shophelper.find_shop_By_Code((string)values[1]) : null;

                        sale.sales = Decimal.Subtract((Decimal)sale.stake, (Decimal)sale.cancelled);
                        sale.balance = Decimal.Subtract((Decimal)sale.sales, (Decimal)sale.winnings);
                        sale.other_stake_commision = 0;//Decimal.Multiply((Decimal)(sale.sales - sale.single_stake_sales), (shoped.commission != null) ? shoped.commission.Value / 100 : Utilities.COMMISSION.COMMISSION_CONSTANT / 100);
                        sale.single_stake_commision = 0;//Decimal.Multiply((Decimal)sale.single_stake_sales, Utilities.COMMISSION.SINGLE_COMMISION_CONSTANT / 100);
                        sale.commision = sale.single_stake_commision + sale.other_stake_commision;
                        //sale.commision = Decimal.Multiply((Decimal)sale.sales, (shoped.commission != null) ? shoped.commission.Value / 100 : Utilities.COMMISSION.COMMISSION_CONSTANT / 100);
                        sale.net_balance = Decimal.Subtract((Decimal)sale.balance, (Decimal)sale.commision);
                        sale.single_stake_percent = sale.sales != 0 ? (sale.single_stake_sales / sale.sales) * 100 : 0;
                        //   sale = new master_luckyballs_sales_crud(sale).insert_master_luckyballs_sale();
                        if (sale == null) sale = new master_luckyballs_sale();
                        sales.Add(sale);
                        //   sale = new master_luckyballs_sales_crud(sale).insert_master_luckyballs_sale();
                    }


                    catch (Exception)
                    {

                        sale = new master_luckyballs_sale();
                       // sale.shop1 = new shop {id = errorcount, shop_code = excelshopcode, app_id = excelshopcode};
                      //  errorcount--;
                        sale.app_id = excelshopcode;
                        sales.Add(sale);
                    }

                }

                return sales;
            }

        } 

        public List<master_sb_sale> fillSBDataToList(DateTime date)
        {
            List<shop> shops = new CRUD.shop_crud().getallShop().ToList<shop>();
            if (dtExcelRecords == null)
            {
                return null;
            }
            else
            {
                List<master_sb_sale> sales = new List<master_sb_sale>();

               shop_crud shophelper = new shop_crud();
                //shop shoped;
                foreach (DataRow row in dtExcelRecords.Rows)
                {
                    int rowindex = dtExcelRecords.Rows.IndexOf(row);
                    //if(dtExcelRecords.Rows.IndexOf(row) == 0)
                    //    continue;
                    if (dtExcelRecords.Rows.IndexOf(row) == dtExcelRecords.Rows.Count - 1 )
                        break;
                  // 
                    var values = row.ItemArray;
                     master_sb_sale sale;
                     string excelshopcode = (string)values[1];
                     shop shoped = shops.SingleOrDefault(a => a.shop_code == excelshopcode);// new shop_crud().find_shop_By_Code((string)values[1]) != null ? shophelper.find_shop_By_Code((string)values[1]) : new shop {id=-1, shop_code=(string) values[1]};
                   try
                    {
                        sale = new master_sb_sale
                      {
                          // shop1 = (shop_ != null) ? shop_ : null,
                          //((shophelper.find_shop_By_Code((string)values[1])) != null) ? shophelper.find_shop_By_Code((string)values[1]) : null,
                          //(string)values[1]) != null) ? shophelper.find_shop_By_Code((string)values[1]) : null,
                          shop1 = (shoped != null) ? shoped : new shop { shop_code = values[1].ToString(), app_id = values[1].ToString() },
                          // shop = shoped.id,//shoped.id,
                          //app_id = values[1].ToString(),//temp string used to display shop in list view
                          date = date,
                          stake = Decimal.Parse(values[5].ToString()),
                          winnings = Decimal.Parse(values[7].ToString()),
                          cancelled = Decimal.Parse(values[10].ToString()),
                          single_stake_sales = Decimal.Parse(values[16].ToString()),
                          isverified = false,
                          islocked = false,
                          date_posted = DateTime.Today,

                      };
                       
                         // sale.shop1 = ((shophelper.find_shop_By_Code((string)values[1])) != null) ? shophelper.find_shop_By_Code((string)values[1]) : null;
                    
                       sale.sales = Decimal.Subtract((Decimal)sale.stake,(Decimal)sale.cancelled);
                      sale.balance =  Decimal.Subtract((Decimal)sale.sales,(Decimal)sale.winnings);
                      sale.other_stake_commision = Decimal.Multiply((Decimal)(sale.sales - sale.single_stake_sales), (shoped.commission != null) ? shoped.commission.Value /100 : Utilities.COMMISSION.COMMISSION_CONSTANT/100);
                      sale.single_stake_commision = Decimal.Multiply((Decimal)sale.single_stake_sales, Utilities.COMMISSION.SINGLE_COMMISION_CONSTANT / 100); 
                      sale.commision = sale.single_stake_commision + sale.other_stake_commision;
                      //sale.commision = Decimal.Multiply((Decimal)sale.sales, (shoped.commission != null) ? shoped.commission.Value / 100 : Utilities.COMMISSION.COMMISSION_CONSTANT / 100);
                       sale.net_balance = Decimal.Subtract((Decimal)sale.balance, (Decimal)sale.commision);
                       sale.single_stake_percent = sale.sales!= 0 ? (sale.single_stake_sales / sale.sales) * 100 : 0;
                        //   sale = new master_sb_sales_crud(sale).insert_master_sb_sale();
                         if (sale == null) sale = new master_sb_sale();
                       sales.Add(sale);
                     //   sale = new master_sb_sales_crud(sale).insert_master_sb_sale();
                } 
                    
                   
                    catch (Exception )
                    {

                        sale = new master_sb_sale();
                      //  sale.shop1 = new shop { shop_code = values[1].ToString(), app_id = values[1].ToString() };
                        sale.app_id = values[1].ToString();
                         sales.Add(sale);
                    }
                   
                }
                
                return sales;
            }

        }
    

           
       /** public System.Data.DataTable xlsInsert(string pth)
        {
            string strcon = string.Empty;
            if (Path.GetExtension(pth).ToLower().Equals(".xls") || Path.GetExtension(pth).ToLower().Equals(".xlsx"))
            {
                strcon = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                                + pth +
                                ";Extended Properties=\"Excel 8.0;HDR=YES;\"";
            }
            else
            {
                strcon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                              + pth +
                              ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            }
            string strselect = "Select * from [Sheet1$]";
            DataTable exDT = new DataTable();
            using (OleDbConnection excelCon = new OleDbConnection(strcon))
            {
                try
                {
                    excelCon.Open();
                    using (OleDbDataAdapter exDA =
            new OleDbDataAdapter(strselect, excelCon))
                    {
                        exDA.Fill(exDT);
                    }
                }
                catch (OleDbException oledb)
                {
                    throw new Exception(oledb.Message.ToString());
                }
                finally
                {
                    excelCon.Close();
                }
                for (int i = 0; i  exDT.Rows.Count; i++)
                {
                    // Check if first column is empty
                    // If empty then delete such record
                    if (exDT.Rows[i]["CardNo"].ToString() == string.Empty)
                    {
                        exDT.Rows[i].Delete();
                    }
                }
                exDT.AcceptChanges();  // refresh rows changes
                if (exDT.Rows.Count == 0)
                {
                    throw new Exception("File uploaded has no record found.");
                }
                return exDT;
            }
        }

        /**  protected void btnUpload_Click(object sender, EventArgs e)
          {
            
                  string fleUpload = Path.GetExtension(myFile.FileName.ToString());
                  if (fleUpload.Trim().ToLower() == ".xls" |
              fleUpload.Trim().ToLower() == ".xlsx")
                  {
                      // Save excel file into Server sub dir
                      // to catch excel file downloading permission
                      myFile.SaveAs(Server.MapPath("~/myFileFile/" +
                          myFile.FileName.ToString()));
                      string uploadedFile = (Server.MapPath("~/myFileFile/" +
                          myFile.FileName.ToString()));
                      try
                      {
                          dt = xlsInsert(uploadedFile);
                         // GridView1.DataSource = dt;
                         // GridView1.DataBind();
                      }
                      catch (Exception)
                      {
                          uplod = false;
                          this.lblMessage.Text = "System uploading Error";
                      }
                      File.Delete(uploadedFile); // Delete upload Excel
                      //file in sub dir 'lsUploadFile' no need to keep...
                  }
                  if (uplod)
                  {
                      string mess1 = "File has successfully uploaded";
                      this.lblMessage.Text = mess1;
                  }
              }
             **/
        
    } 

    }
   

    
