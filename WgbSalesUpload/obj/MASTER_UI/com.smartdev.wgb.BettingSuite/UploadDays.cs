using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.MASTER_UI.com.smartdev.wgb.BettingSuite
{
    public class UploadDays
    {
        private DateTime date_;
        private bool Balance_;

        public UploadDays(DateTime date, bool Balance)
        {
            date_ = date;
            Balance_ = Balance;
        }

        public DateTime date
        {
            get { return date_; }
            set { date_ = value; }
        }

        public bool Balance
        {
            get { return Balance_; }
            set { Balance_ = value; }
        }
    }
}