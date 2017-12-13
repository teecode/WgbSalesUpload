using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class saleshelper
    {
        private string shopcode_;
        private string single_stake_perc;
        private decimal stake, cancelled, single_sales, single_comm, multi_sales, multi_comm, total_sales, total_comm, total_winnings, NetBalance_;

        public saleshelper(master_daily_sale sales)
        {
            this.shopcode_ = (sales.shop1!=null)?sales.shop1.shop_code : "ERROR SHOP";
            this.stake = sales.stake.Value;
            this.cancelled = sales.cancelled.Value;
            this.single_sales = sales.single_stake_sales != null ? sales.single_stake_sales.Value : Decimal.Zero;
            this.single_comm = sales.single_stake_commision != null ? sales.single_stake_commision.Value : Decimal.Zero; ;
            this.multi_sales = sales.sales.Value - this.single_sales;
            this.multi_comm = sales.commision.Value - this.single_comm;
            this.single_stake_perc = sales.single_stake_percent.ToString() + "%";
            this.total_sales = sales.sales.Value;
            this.total_comm = sales.commision.Value;
            this.total_winnings = sales.winnings.Value;
            this.NetBalance_ = sales.sales.Value - sales.winnings.Value - sales.commision.Value;
        }
        public saleshelper(master_sb_sale sales)
        {
            this.shopcode_ = (sales.shop1 != null) ? sales.shop1.shop_code : "ERROR SHOP";
            this.stake = sales.stake.Value;
            this.cancelled = sales.cancelled.Value;
            this.single_sales = sales.single_stake_sales != null ? sales.single_stake_sales.Value : Decimal.Zero;
            this.single_comm = sales.single_stake_commision != null ? sales.single_stake_commision.Value : Decimal.Zero; ;
            this.multi_sales = sales.sales.Value - this.single_sales;
            this.multi_comm = sales.commision.Value - this.single_comm;
            this.single_stake_perc = sales.single_stake_percent.ToString() + "%";
            this.total_sales = sales.sales.Value;
            this.total_comm = sales.commision.Value;
            this.total_winnings = sales.winnings.Value;
            this.NetBalance_ = sales.sales.Value - sales.winnings.Value - sales.commision.Value;
        }
        public saleshelper(master_luckyballs_sale sales)
        {
            this.shopcode_ = (sales.shop1 != null) ? sales.shop1.shop_code : "ERROR SHOP";
            this.stake = sales.stake.Value;
            this.cancelled = sales.cancelled.Value;
            this.single_sales = sales.single_stake_sales != null ? sales.single_stake_sales.Value : Decimal.Zero;
            this.single_comm = sales.single_stake_commision != null ? sales.single_stake_commision.Value : Decimal.Zero; ;
            this.multi_sales = sales.sales.Value - this.single_sales;
            this.multi_comm = sales.commision.Value - this.single_comm;
            this.single_stake_perc = sales.single_stake_percent.ToString() + "%";
            this.total_sales = sales.sales.Value;
            this.total_comm = sales.commision.Value;
            this.total_winnings = sales.winnings.Value;
            this.NetBalance_ = sales.sales.Value - sales.winnings.Value - sales.commision.Value;
        }

        public string ShopCode
        {
            get { return this.shopcode_; }
            set { this.shopcode_ = value; }
        }

        public decimal Stake
        {
            get { return this.stake; }
            set { this.stake = value; }
        }

        public decimal Cancelled
        {
            get { return this.cancelled; }
            set { this.cancelled = value; }
        }

        public decimal Single_Sales
        {
            get { return this.single_sales; }
            set { this.single_sales = value; }
        }

        public decimal Single_Comm
        {
            get { return this.single_comm; }
            set { this.single_comm = value; }
        }

        public decimal Multi_Sales
        {
            get { return this.multi_sales; }
            set { this.multi_sales = value; }
        }

        public decimal Multi_Comm
        {
            get { return this.multi_comm; }
            set { this.multi_comm = value; }
        }

        public string Single_Stake_Perc
        {
            get { return this.single_stake_perc; }
            set { this.single_stake_perc = value; }
        }

        public decimal Total_Sales
        {
            get { return this.total_sales; }
            set { this.total_sales = value; }
        }

        public decimal Total_Comm
        {
            get { return this.total_comm; }
            set { this.total_comm = value; }
        }

        public decimal Total_Winnings
        {
            get { return this.total_winnings; }
            set { this.total_winnings = value; }
        }

        public decimal NetBalance
        {
            get { return this.NetBalance_; }
            set { this.NetBalance = value; }
        }
    }
}