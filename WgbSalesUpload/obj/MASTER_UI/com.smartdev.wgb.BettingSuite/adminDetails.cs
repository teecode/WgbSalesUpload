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

    public class adminDetails
    {
        List<admin_user> errorlist_ = new List<admin_user>();
        List<admin_user> list_ = new List<admin_user>();
        string connectionString = "";
        HttpPostedFile myFile;
        DataTable dtExcelRecords = null;
                BettingSuiteDataContext db = new BettingSuiteDataContext();
        string[] bankcode = {"ACCESS","CITY","DIAMOND","ECO","ENTERPRISE","FIDELITY","FIRST",
                             "FCMB","GTB","HERITAGE","KEYSTONE","MAINSTREET","SKYE","STANBIC","STANDARD",
                            "STERLING","UNION","UBA","UNITY","WEMA","ZENITH"};


        public adminDetails(HttpPostedFile myFile, HttpServerUtility Server)
        {
            this.myFile = myFile;
            if (validateUploadData(Server))
                dtExcelRecords = displayUploadedData(Server);
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
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                // connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            }
            else
                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    //connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=1\"";
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + @";Extended Properties=" + Convert.ToChar(34).ToString() + @"Excel 8.0;Imex=1;HDR=Yes;" + Convert.ToChar(34).ToString();
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
           dtExcelRecords = dtExcelRecords.Rows.Cast<DataRow>().Where(row => !row.ItemArray.All(field => field is System.DBNull || field.ToString().Trim().Equals(string.Empty))).CopyToDataTable();
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

        public List<admin_user> fillDataToList()
        {
            if (dtExcelRecords == null)
            {
                return null;
            }
            else
            {
                List<admin_user> userList = new List<admin_user>();

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
                    shop shop = db.shops.SingleOrDefault(a => a.shop_code == values[4].ToString());
                    if(shop!= null && userList.Where(a => a.id == shop.agent).Count()< 1 )//&& values[5].ToString().Length>9)
                    {
                    admin_user user = db.admin_users.SingleOrDefault(a => a.id == shop.agent);
                        user.address = values[2]!= null ? values[2].ToString() : "";
                    try
                    {
                        string[] data = ((string)values[1]).Replace('.',' ').Split(' ');
                           string fname = null,lname = null,mname = null;// = data[0]; 

                           if (values[5] != null && values[5].ToString().Count() > 0)
                    {
                          user.accountNo =   String.Format("{0:0000000000}",values[5]);
                          if (values[6] != null && values[6].ToString().Count() > 0)
                          user.Bank = db.bank_lists.SingleOrDefault(a => a.code == values[6].ToString().Trim()).id;
                    }
                           
                       
                         mname = data[0];
                         fname = data[1];
                        if(data.Length>2)
                            lname = data[2];
                   
                          if(values[7]!= null && values[7].ToString().Count() >  0)
                           user.account_name = values[7].ToString();
                           else 
                             user.account_name = (string)values[1].ToString();
                              
                        user.firstname = fname;
                        user.middlename = mname;
                        user.lastname = (lname != null ? lname : "AGENCY");
                        if (values[8] != null && values[8].ToString().Count() > 0)
                        {
                        string[] phn = ((string)values[8]).Split('/');
                        if(phn.Length > 0)
                            user.phone = phn[0];
                        if(phn.Length > 1)
                            user.mobile = phn[1];
                       }
                      
                         userList.Add(user);
                         
                    }
                    catch (Exception ex)
                    {
                        errorlist_.Add(user);
                    }
                    }
                }
                list_ = userList;
                return userList;
            }

        }

        public List<admin_user >errorList
        {
            get { return errorlist_; }
            set { errorlist_ = value; }
        }

        public List<admin_user> successList
        {
            get { return list_; }
            set { list_ = value; }
        }

        public void saveList()
        {

            db.SubmitChanges();
            //foreach (admin_user user in list_)
            //{
            //    new CRUD.admin_users_crud(user).alter_admin_user();
            //}
        }
        
        
    }
}