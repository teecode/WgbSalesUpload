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

    public class transdetails
    {
        List<master_transaction> errorlist_ = new List<master_transaction>();
        List<master_transaction> list_ = new List<master_transaction>();
        string connectionString = "";
        HttpPostedFile myFile;
        DataTable dtExcelRecords = null;
                BettingSuiteDataContext db = new BettingSuiteDataContext();
      


        public transdetails(HttpPostedFile myFile, HttpServerUtility Server)
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

        public List<master_transaction> fillDataToList()
        {
            List<master_transaction> transList = new List<master_transaction>();
            if (dtExcelRecords == null)
            {
                return null;
            }
            else
            {
               

                foreach (DataRow row in dtExcelRecords.Rows)
                {
                    int rowindex = dtExcelRecords.Rows.IndexOf(row);
                    //if (dtExcelRecords.Rows.IndexOf(row) == 0)
                    //    continue;
                    //if (dtExcelRecords.Rows.IndexOf(row) == dtExcelRecords.Rows.Count - 1)
                    //    break;
                    // 
                    var values = row.ItemArray;
                    master_transaction trans = null;
                    try
                    {
                    trans = db.master_transactions.SingleOrDefault(a => a.id == int.Parse(values[0].ToString()));
                    if(trans!= null)//&& values[5].ToString().Length>9)
                    {
                     trans.details = values[1].ToString();
                     transList.Add(trans);
                    }
                     }
                    catch (Exception ex)
                    {
                        errorlist_.Add(trans);
                    }
                   }
                }
                list_ = transList;
                return transList;
            }

        

        public List<master_transaction >errorList
        {
            get { return errorlist_; }
            set { errorlist_ = value; }
        }

        public List<master_transaction> successList
        {
            get { return list_; }
            set { list_ = value; }
        }

        public void saveList()
        {

            db.SubmitChanges();
            //foreach (master_transaction user in list_)
            //{
            //    new CRUD.master_transactions_crud(user).alter_master_transaction();
            //}
        }
        
        
    }
}