using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class SummarySheet
    {
        private string Shop_;
        private decimal BalanceBF_;
        private decimal Sales_;
        private decimal Winnings_;
        private decimal Commission_;
        private decimal Reimbursement_;
        private decimal ExpectedLodgement_;
        private decimal ActualLodgement_;
        private decimal Balance_;

        public SummarySheet(string Shop_, decimal BalanceBF_, decimal Sales_, decimal Winnings_, decimal Commission_, decimal Reimbursement_, decimal ExpectedLodgement_, decimal ActualLodgement_, decimal Balance_)
        {
            this.Shop_ = Shop_;
            this.Balance_BF = BalanceBF_;
            this.Sales_ = Sales_;
            this.Winnings_ = Winnings_;
            this.Commission_ = Commission_;
            this.Reimbursement_ = Reimbursement_;
            this.ExpectedLodgement_ = ExpectedLodgement_;
            this.ActualLodgement_ = ActualLodgement_;
            this.Balance_ = Balance_;
        }

        public string Shop
        {
            get { return Shop_; }
            set { Shop_ = value; }
        }

        public decimal Balance_BF 
        {
            get { return BalanceBF_; }
            set { BalanceBF_ = value; }
        }

        public decimal Sales
        {
            get { return Sales_; }
            set { Sales_ = value; }
        }

        public decimal Winnings
        {
            get { return Winnings_; }
            set { BalanceBF_ = value; }
        }

        public decimal Commission
        {
            get { return Commission_; }
            set { Commission_ = value; }
        }

        public decimal Reimbursement
        {
            get { return Reimbursement_; }
            set { Reimbursement_ = value; }
        }

        public decimal ExpectedLodgement
        {
            get { return ExpectedLodgement_; }
            set { ExpectedLodgement_ = value; }
        }

        public decimal ActualLodgement
        {
            get { return ActualLodgement_; }
            set { ActualLodgement_ = value; }
        }

        public decimal Balance
        {
            get { return Balance_; }
            set { Balance_ = value; }
        }
    }
}