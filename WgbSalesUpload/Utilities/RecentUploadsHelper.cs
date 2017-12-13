using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class RecentUploadsHelper
    {

        private string Date_;
        private string Status_;

        public RecentUploadsHelper(string date, string Status)
        {
            Date_ = date;
            Status_ = Status;
        }

        public string Date
        {
           get{return Date_;}
            set{Date_ = value;}
        }
        public string Status
        {
            get { return Status_; }
            set { Status_ = value; }
    
        }
    }
}