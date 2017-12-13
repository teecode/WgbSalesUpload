using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WgbSalesUpload.CRUD
{
    class commission_transaction_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public commission_transaction commission_transaction_;
        public commission_transaction commission_transaction
        {
            get { return commission_transaction_; }
            set { commission_transaction_ = value; }
        }

        public commission_transaction_crud(commission_transaction commission_transaction_)
        {
            this.commission_transaction_ = commission_transaction_;
        }

        public commission_transaction_crud()
        {
            // TODO: Complete member initialization
        }

        public commission_transaction find_commission_transaction_By_ID(int id)
        {
            try
            {
                commission_transaction = db.commission_transactions.SingleOrDefault(a => a.id == id);
                return commission_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public commission_transaction find_commission_transaction_By_AppId(string Appid)
        {
            try
            {
                commission_transaction = db.commission_transactions.SingleOrDefault(a => a.app_id == Appid);
                return commission_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
        public commission_transaction insert_commission_transaction()
        {
            try
            {
                db.commission_transactions.InsertOnSubmit(commission_transaction);
                db.SubmitChanges();
                return find_commission_transaction_By_AppId(commission_transaction.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public commission_transaction alter_commission_transaction()
        {
            try
            {
                commission_transaction prev_commission_transaction = db.commission_transactions.SingleOrDefault(a => a.id == commission_transaction.id);

                prev_commission_transaction.amount = commission_transaction.amount;
                prev_commission_transaction.balance_before = commission_transaction.balance_before;
                prev_commission_transaction.balance_after = commission_transaction.balance_after;
                prev_commission_transaction.posted_by = commission_transaction.posted_by;
                prev_commission_transaction.shop = commission_transaction.shop;
                prev_commission_transaction.trans_category = commission_transaction.trans_category;
                prev_commission_transaction.trans_timestamp = commission_transaction.trans_timestamp;
                prev_commission_transaction.trans_type = commission_transaction.trans_type;
                
                

                db.SubmitChanges();
                return commission_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public commission_transaction delete_commission_transaction()
        {
            try
            {
                commission_transaction prev_commission_transaction = db.commission_transactions.SingleOrDefault(a => a.id == commission_transaction.id);
                db.commission_transactions.DeleteOnSubmit(prev_commission_transaction);
                return commission_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<commission_transaction> GetShopTransactionWithinDate(shop shop, DateTime from, DateTime to)
        { 
            return db.commission_transactions.Where(a=>a.shop == shop.id && a.trans_timestamp>=from && a.trans_timestamp<=to).ToList<commission_transaction>();
        }



        public List<WgbSalesUpload.commission_transaction> getTransactionWithinDate(DateTime startdate, DateTime endDate)
        {
            return db.commission_transactions.Where(a => a.trans_timestamp >= startdate && a.trans_timestamp <= endDate).ToList<commission_transaction>();            
        }
    }
}
