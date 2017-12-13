using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class shop_transaction_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public shop_transaction shop_transaction_;
        public shop_transaction shop_transaction
        {
            get { return shop_transaction_; }
            set { shop_transaction_ = value; }
        }

        public shop_transaction_crud(shop_transaction shop_transaction_)
        {
            this.shop_transaction_ = shop_transaction_;
        }

        public shop_transaction_crud()
        {
            // TODO: Complete member initialization
        }

        public shop_transaction find_shop_transaction_By_ID(int id)
        {
            try
            {
                shop_transaction = db.shop_transactions.SingleOrDefault(a => a.id == id);
                return shop_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_transaction find_shop_transaction_By_AppId(string Appid)
        {
            try
            {
                shop_transaction = db.shop_transactions.SingleOrDefault(a => a.app_id == Appid);
                return shop_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
        public shop_transaction insert_shop_transaction()
        {
            try
            {
                db.shop_transactions.InsertOnSubmit(shop_transaction);
                db.SubmitChanges();
                return find_shop_transaction_By_AppId(shop_transaction.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_transaction alter_shop_transaction()
        {
            try
            {
                shop_transaction prev_shop_transaction = db.shop_transactions.SingleOrDefault(a => a.id == shop_transaction.id);

                prev_shop_transaction.amount = shop_transaction.amount;
                prev_shop_transaction.balance_before = shop_transaction.balance_before;
                prev_shop_transaction.balance_after = shop_transaction.balance_after;
                prev_shop_transaction.posted_by = shop_transaction.posted_by;
                prev_shop_transaction.shop = shop_transaction.shop;
                prev_shop_transaction.trans_category = shop_transaction.trans_category;
                prev_shop_transaction.trans_timestamp = shop_transaction.trans_timestamp;
                prev_shop_transaction.trans_type = shop_transaction.trans_type;
                
                

                db.SubmitChanges();
                return shop_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_transaction delete_shop_transaction()
        {
            try
            {
                shop_transaction prev_shop_transaction = db.shop_transactions.SingleOrDefault(a => a.id == shop_transaction.id);
                db.shop_transactions.DeleteOnSubmit(prev_shop_transaction);
                return shop_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}