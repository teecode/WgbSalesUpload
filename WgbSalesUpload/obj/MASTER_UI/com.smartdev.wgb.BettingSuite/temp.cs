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

    public class temp
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        string connectionString = "";
        HttpPostedFile myFile;
        DataTable dtExcelRecords = null;
        public temp(HttpPostedFile myFile)
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
            else if (fileExtension == ".xlsx" || fileExtension == ".xls") 
            {
                 connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                //  connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + @";Extended Properties=" + Convert.ToChar(34).ToString() + @"Excel 8.0;Imex=1;HDR=Yes;" + Convert.ToChar(34).ToString();
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

        public string fillDataToList(RadGrid r1, RadGrid r2, RadGrid r3)
        {
            if (dtExcelRecords == null)
            {
                return "Error : No Data to Process";
            }
            else
            {
                List<login> names = db.logins.ToList<login>();
                List<login> errornames = new List<login>();
                List<shop> shops = db.shops.ToList<shop>(); ;
                List<shop> errorshops = new List<shop>(); ;
                List<admin_user> users = db.admin_users.ToList<admin_user>();
                List<admin_user> errorusers = new List<admin_user>();
                admin_user user = null; shop shop = null;
                login login = null;
                shop_crud shophelper = new shop_crud();
                //shop shoped;
                foreach (DataRow row in dtExcelRecords.Rows)
                {

                    int rowindex = dtExcelRecords.Rows.IndexOf(row);
                    //  if (dtExcelRecords.Rows.IndexOf(row) == 0)
                    //     continue;
                    if (dtExcelRecords.Rows.IndexOf(row) == dtExcelRecords.Rows.Count - 1)
                        break;
                    var values = row.ItemArray;

                    string[] data = ((string)values[1]).Split('/');
                    if (data.Length > 2)
                    {
                        string state_string = data[0];
                        string shop_string = data[1];
                        string shop_type_string = data[2];
                        string shop_code = ((string)values[1]);
                        if (state_string != null && shop_string != null && shop_type_string != null && shops.SingleOrDefault(a => a.shop_code == shop_code ) == null)
                        {


                            if (shop_type_string.Contains("AG"))
                            {
                                try
                                {
                                    login = names.SingleOrDefault(b => b.username == shop_string);
                                    if (login != null)
                                    {
                                        user = login.admin_users[0];
                                    }
                                    else
                                    {
                                        login = new login();
                                        login.role = 3;
                                        login.username = shop_string;
                                        login.password = Utilities.MD5Encript.MD5Hash("changeme");
                                        login.App_id = Application(db).app_id;
                                        user = new admin_user { firstname = shop_string, middlename = "AGENCY", login1 = login };
                                        user.app_id = Application(db).app_id;
                                        names.Add(login);
                                        db.logins.InsertOnSubmit(login);
                                        user.login = login.id;
                                        db.admin_users.InsertOnSubmit(user);
                                        db.SubmitChanges();
                                        users.Add(user);
                                    }
                                    if (shops.SingleOrDefault(a => a.shop_code == ((string)values[1])) == null)
                                    {
                                        shop = new shop
                                        {
                                            shop_code = (string)values[1],
                                            isactive = true,
                                            is_bettingshop = true,
                                            state = db.state_lists.SingleOrDefault(a => a.code == state_string).id,
                                            shop_ownership = "AGENT",
                                            is_onlinefunding = false,
                                            credit_limit = 30000,
                                            shop_name = shop_string + " SHOP " + shop_type_string.Substring(shop_type_string.Length - 1, 1),
                                            commission = Utilities.COMMISSION.COMMISSION_CONSTANT,

                                        };
                                        shop.app_id = Application(db).app_id;
                                        shop.agent = user.id;
                                        db.shops.InsertOnSubmit(shop);
                                        db.SubmitChanges();
                                        shops.Add(shop);
                                    }


                                }

                                   catch (Exception)
                                {
                                     errorshops.Add(shop);
                                    errorusers.Add(user);
                                    errornames.Add(login);
                                }
                            }

                            else
                            {//insert hubs
                                try
                                {

                                    login = names.SingleOrDefault(b => b.username == shop_string);
                                    if (login != null)
                                    {
                                        user = login.admin_users[0];
                                    }
                                    else
                                    {
                                        login = new login();
                                        login.role = 2;
                                        login.username = shop_string;
                                        login.password = Utilities.MD5Encript.MD5Hash("changeme");
                                        login.App_id = Application(db).app_id;
                                        names.Add(login);
                                        user = new admin_user { firstname = shop_string, middlename = "MANAGER", login1 = login, num_of_shops = 1 };
                                        user.app_id = Application(db).app_id;
                                        db.logins.InsertOnSubmit(login);
                                        user.login = login.id;
                                        db.admin_users.InsertOnSubmit(user);
                                        db.SubmitChanges();
                                        users.Add(user);
                                    
                                    }
                                    if (shops.SingleOrDefault(a => a.shop_code == ((string)values[1])) == null)
                                    {
                                        shop = new shop
                                        {
                                            shop_code = (string)values[1],
                                            isactive = true,
                                            is_bettingshop = true,
                                            state = db.state_lists.SingleOrDefault(a => a.code == state_string).id,
                                            shop_ownership = "WGB",
                                            is_onlinefunding = false,
                                            commission = Utilities.COMMISSION.COMMISSION_CONSTANT,
                                        };
                                        if (shop_type_string.Contains("H"))
                                        {
                                            shop.shop_type = 0
                                                ;
                                            shop.shop_name = shop_string + " HUB";
                                        }
                                        else if (shop_type_string.Contains("BS"))
                                        {
                                            shop.shop_type = 1;
                                            shop.shop_name = shop_string + " BIG SHOP";
                                        }
                                        else
                                        {
                                            shop.shop_type = 2;
                                            shop.shop_name = shop_string + " REGULAR SHOP";
                                        }
                                        shop.app_id = Application(db).app_id;
                                        shop.shopmanager_id = login.id;
                                        db.shops.InsertOnSubmit(shop);
                                        db.SubmitChanges();
                                        shops.Add(shop);
                                    }


                                }
                                catch (Exception)
                                {
                                    errorshops.Add(shop);
                                    errorusers.Add(user);
                                    errornames.Add(login);

                                }
                            }


                            //remain shopmanager id


                        }





                    }
                    r1.DataSource = errornames;
                    r2.DataSource = errorshops;
                    r3.DataSource = errorusers;
                    r1.DataBind(); r2.DataBind(); r3.DataBind();
                }
                return "File processed Sucessfully";
            }
        }


        public application Application(BettingSuiteDataContext db)
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



