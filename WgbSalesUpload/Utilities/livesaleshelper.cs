using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class livesaleshelper
     {
        private master_daily_sale _master_daily_sales;
        private decimal sales_, winnings_, commision_,balance_, netbalance_, currentbalance_;
        private string shop_;
        private shop shop1_;
       
         public livesaleshelper()
         {
         }

        public livesaleshelper(master_daily_sale master_daily_sales)
        {
            _master_daily_sales = master_daily_sales;
            sales_ = master_daily_sales.sales.Value;
            winnings_ = master_daily_sales.winnings.Value;
            commision_ = master_daily_sales.commision.Value;
            balance_ = master_daily_sales.net_balance.Value;
            netbalance_ = master_daily_sales.shop1.master_balance_sheets[0].sales_balance.Value;
            currentbalance_ = balance_ + netbalance_;
            shop_ = master_daily_sales.shop1.shop_code;
            shop1_ = master_daily_sales.shop1;
        }

         public string shop
        {
            get { return shop_; }
            set { shop_ = value; }
        }
        public decimal sales
        {
            get { return sales_; }
            set { sales_ = value; }
        }

        public decimal winnings
        {
            get { return  winnings_; }
            set { winnings_ = value; }
        }

        public decimal commision
        {
            get { return commision_; }
            set { commision_ = value; }
        }

        public decimal balance
        {
            get { return balance_; }
            set { balance_ = value; }
        }

        public decimal netbalance
        {
            get { return netbalance_; }
            set { netbalance_ = value; }
        }


        public decimal currentbalance
        {
            get { return currentbalance_; }
            set { currentbalance_ = value; }
        }

        public shop shop1
        {
            get { return shop1_; }
            set { shop1_ = value; }
        }
    }
}