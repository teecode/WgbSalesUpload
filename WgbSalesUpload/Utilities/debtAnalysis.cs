using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class debtAnalysis
    {
        private string Shop_Code_;
        private decimal Current_Bal_;
        private decimal One_Week_;
        private decimal Two_Week_;
        private decimal One_Month_;
        private decimal Two_Month_;
        private decimal Three_Month_;
        private decimal Six_Month_;
        private decimal Older_;

        public debtAnalysis(string Shop_Code_, decimal Current_Bal_, decimal One_Week_, decimal Two_Week_, decimal One_Month_, decimal Two_Month_, decimal Three_Month_, decimal Six_Month_, decimal Older_)
        { 
            this.Shop_Code_ = Shop_Code_;
            this.Current_Bal_ = Current_Bal_;
            this.One_Week_ = One_Week_;
            this.Two_Week_ = Two_Week_;
            this.One_Month_ = One_Month_;
            this.Two_Month_ = Two_Month_;
            this.Three_Month_ = Three_Month_;
            this.Six_Month_ = Six_Month_;
            this.Older_ = Older_;
        }

        public string Shop
        {
            get { return Shop_Code_; }
            set { Shop_Code_ = value; }
        }

        public decimal Current_Bal
        {
            get { return Current_Bal_; }
            set { Current_Bal_ = value; }
        }

        public decimal One_Week
        {
            get { return One_Week_; }
            set { One_Week_ = value; }
        }

        public decimal Two_Weeks
        {
            get { return Two_Week_; }
            set { Two_Week_ = value; }
        }

        public decimal One_Month
        {
            get { return One_Month_; }
            set { One_Month_ = value; }
        }

        public decimal Two_Months
        {
            get { return Two_Month_; }
            set { Two_Month_ = value; }
        }

        public decimal Three_Months
        {
            get { return Three_Month_; }
            set { Three_Month_ = value; }
        }

        public decimal Six_Months
        {
            get { return Six_Month_; }
            set { Six_Month_ = value; }
        }

        public decimal Older
        {
            get { return Older_; }
            set { Older_ = value; }
        }


        




    }
}