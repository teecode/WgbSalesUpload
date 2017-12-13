using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class bonushelper
    {
        private shop shop_;
        private string shop_name_;
        private decimal sales_;
        private decimal winnings_;
        private decimal commission_;
        private decimal netprofit_;
        private decimal bonus_;

        public bonushelper(shop shop, string shop_name, decimal sales, decimal winnings, decimal commission, decimal netprofit, decimal bonus)
        {
            this.shop_ = shop;
            this.shop_name_ = shop_name;
            this.sales_ = sales;
            this.winnings_ = winnings;
            this.commission_ = commission;
            this.netprofit_ = netprofit;
            this.bonus_ = bonus;

        }

        internal shop shop
        {
            get { return this.shop_; }
            set { this.shop_ = value; }
        }

        public string Shop_Name
        {
            get { return this.shop_name_; }
            set { this.shop_name_ = value; }
        }

        public decimal Sales
        {
            get { return this.sales_; }
            set { this.sales_ = value; }
        }

        public decimal Winnings
        {
            get { return this.winnings_; }
            set { this.winnings_ = value; }
        }

        public decimal Commission
        {
            get { return this.commission_; }
            set { this.commission_ = value; }
        }

        public decimal NetProfit
        {
            get { return this.netprofit_; }
            set { this.netprofit_ = value; }
        }

        public decimal Bonus
        {
            get { return this.bonus_; }
            set { this.bonus_ = value; }
        }


    }
}