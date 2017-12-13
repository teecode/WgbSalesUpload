using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class master_transaction_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public master_transaction master_transaction_;
        public master_transaction master_transaction
        {
            get { return master_transaction_; }
            set { master_transaction_ = value; }
        }

        public master_transaction_crud(master_transaction master_transaction_)
        {
            this.master_transaction_ = master_transaction_;
        }

        public master_transaction_crud()
        {
            // TODO: Complete member initialization
        }

        public master_transaction find_master_transaction_By_ID(int id)
        {
            try
            {
                master_transaction = db.master_transactions.SingleOrDefault(a => a.id == id);
                return master_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_transaction find_master_transaction_By_AppId(string Appid)
        {
            try
            {
                master_transaction = db.master_transactions.SingleOrDefault(a => a.app_id == Appid);
                return master_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
        public master_transaction insert_master_transaction()
        {
            try
            {
                db.master_transactions.InsertOnSubmit(master_transaction);
                db.SubmitChanges();
                return find_master_transaction_By_AppId(master_transaction.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_transaction alter_master_transaction()
        {
            try
            {
                master_transaction prev_master_transaction = db.master_transactions.SingleOrDefault(a => a.id == master_transaction.id);

                prev_master_transaction.amount = master_transaction.amount;
                prev_master_transaction.balance_before = master_transaction.balance_before;
                prev_master_transaction.balance_after = master_transaction.balance_after;
                prev_master_transaction.posted_by = master_transaction.posted_by;
                prev_master_transaction.shop = master_transaction.shop;
                prev_master_transaction.trans_category = master_transaction.trans_category;
                prev_master_transaction.trans_timestamp = master_transaction.trans_timestamp;
                prev_master_transaction.trans_type = master_transaction.trans_type;
                
                

                db.SubmitChanges();
                return master_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_transaction delete_master_transaction()
        {
            try
            {
                master_transaction prev_master_transaction = db.master_transactions.SingleOrDefault(a => a.id == master_transaction.id);
                db.master_transactions.DeleteOnSubmit(prev_master_transaction);
                return master_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<master_transaction> GetShopTransactionWithinDate(shop shop, DateTime from, DateTime to)
        { 
            return db.master_transactions.Where(a=>a.shop == shop.id && a.trans_timestamp>=from && a.trans_timestamp<=to).ToList<master_transaction>();
        }



        public List<WgbSalesUpload.master_transaction> getTransactionWithinDate(DateTime startdate, DateTime endDate)
        {
            return db.master_transactions.Where(a => a.trans_timestamp >= startdate && a.trans_timestamp <= endDate).ToList<master_transaction>();            
        }
    }
}