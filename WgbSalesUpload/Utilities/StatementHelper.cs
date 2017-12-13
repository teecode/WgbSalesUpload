using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class StatementHelper
    {
        String Credit_ = "", Debit_ = "", Date_ = "", Balance_ = "", Details_;
        master_transaction trans_;
        commission_transaction ctrans_;
        debt_transaction debt_trans_;

        public StatementHelper(master_transaction trans)
        {
            Date_ = trans.trans_timestamp.ToShortDateString();
            Debit_ = (trans.trans_type == 1) ? trans.amount.ToString() : string.Empty;
            Credit_ = (trans.trans_type == 0) ? trans.amount.ToString() : string.Empty;
            Balance_ = trans.balance_after.ToString();
            Details_ = (trans.details != string.Empty || trans.details != null) ? trans.details : trans.transaction_category.description;
            trans_ = trans;

        }

        public StatementHelper(debt_transaction trans)
        {
            Date_ = trans.trans_timestamp.ToShortDateString();
            Debit_ = (trans.trans_type == 1) ? trans.amount.ToString() : string.Empty;
            Credit_ = (trans.trans_type == 0) ? trans.amount.ToString() : string.Empty;
            Balance_ = trans.balance_after.ToString();
            Details_ = (trans.details != string.Empty || trans.details != null) ? trans.details : trans.transaction_category.description;
            debt_trans_ = trans;

        }

        public StatementHelper(commission_transaction trans)
        {
            Date_ = trans.trans_timestamp.ToShortDateString();
            Debit_ = (trans.trans_type == 1) ? trans.amount.ToString() : string.Empty;
            Credit_ = (trans.trans_type == 0) ? trans.amount.ToString() : string.Empty;
            Balance_ = trans.balance_after.ToString();
            Details_ = (trans.details != string.Empty || trans.details != null) ? trans.details : trans.transaction_category.description;
            ctrans_ = trans;

        }

        public string Date
        {

            set { Date_ = value; }
            get { return Date_; }
        }

        public string Details
        {

            set { Details_ = value; }
            get { return Details_; }
        }


        public string Debit
        {

            set { Debit_ = value; }
            get { return Debit_; }
        }

        public string Credit
        {

            set { Credit_ = value; }
            get { return Credit_; }
        }

        public string Balance
        {

            set { Balance_ = value; }
            get { return Balance_; }
        }

        public master_transaction trans
        {

            set { trans_ = value; }
            get { return trans_; }
        }

        public debt_transaction debt_trans
        {

            set { debt_trans_ = value; }
            get { return debt_trans_; }
        }

        public commission_transaction ctrans
        {

            set { ctrans_ = value; }
            get { return ctrans_; }
        }
    }
}