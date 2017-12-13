using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class master_credit_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public master_credit master_credit_;
        public master_credit master_credit
        {
            get { return master_credit_; }
            set { master_credit_ = value; }
        }

        public master_credit_crud(master_credit master_credit_)
        {
            this.master_credit_ = master_credit_;
        }

        public master_credit find_master_credit_By_ID(int id)
        {
            try
            {
                master_credit = db.master_credits.SingleOrDefault(a => a.id == id);
                return master_credit;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_credit find_master_credit_By_AppId(string Appid)
        {
            try
            {
                master_credit = db.master_credits.SingleOrDefault(a => a.app_id == Appid);
                return master_credit;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public master_credit insert_master_credit()
        {
            try
            {
                db.master_credits.InsertOnSubmit(master_credit);
                db.SubmitChanges();
                return find_master_credit_By_AppId(master_credit.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_credit alter_master_credit()
        {
            try
            {
                master_credit prev_master_credit = db.master_credits.SingleOrDefault(a => a.id == master_credit.id);

                prev_master_credit.balance_after = master_credit_.balance_after;
                prev_master_credit.balance_before = master_credit.balance_before;
                prev_master_credit.credit_type = master_credit.credit_type;
                prev_master_credit.shop = master_credit.shop;
                prev_master_credit.transaction = master_credit.transaction;
                

                db.SubmitChanges();
                return master_credit;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_credit delete_master_credit()
        {
            try
            {
                master_credit prev_master_credit = db.master_credits.SingleOrDefault(a => a.id == master_credit.id);
                db.master_credits.DeleteOnSubmit(prev_master_credit);
                return master_credit;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}