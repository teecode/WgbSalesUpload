using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class debt_transaction_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public debt_transaction debt_transaction_;
        public debt_transaction debt_transaction
        {
            get { return debt_transaction_; }
            set { debt_transaction_ = value; }
        }

        public debt_transaction_crud(debt_transaction debt_transaction_)
        {
            this.debt_transaction_ = debt_transaction_;
        }

        public debt_transaction_crud()
        {
            // TODO: Complete member initialization
        }

        public debt_transaction find_debt_transaction_By_ID(int id)
        {
            try
            {
                debt_transaction = db.debt_transactions.SingleOrDefault(a => a.id == id);
                return debt_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt_transaction find_debt_transaction_By_AppId(string Appid)
        {
            try
            {
                debt_transaction = db.debt_transactions.SingleOrDefault(a => a.app_id == Appid);
                return debt_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
        public debt_transaction insert_debt_transaction()
        {
            try
            {
                db.debt_transactions.InsertOnSubmit(debt_transaction);
                db.SubmitChanges();
                return find_debt_transaction_By_AppId(debt_transaction.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt_transaction alter_debt_transaction()
        {
            try
            {
                debt_transaction prev_debt_transaction = db.debt_transactions.SingleOrDefault(a => a.id == debt_transaction.id);

                prev_debt_transaction.amount = debt_transaction.amount;
                prev_debt_transaction.balance_before = debt_transaction.balance_before;
                prev_debt_transaction.balance_after = debt_transaction.balance_after;
                prev_debt_transaction.posted_by = debt_transaction.posted_by;
                prev_debt_transaction.shop = debt_transaction.shop;
                prev_debt_transaction.trans_category = debt_transaction.trans_category;
                prev_debt_transaction.trans_timestamp = debt_transaction.trans_timestamp;
                prev_debt_transaction.trans_type = debt_transaction.trans_type;
                
                

                db.SubmitChanges();
                return debt_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt_transaction delete_debt_transaction()
        {
            try
            {
                debt_transaction prev_debt_transaction = db.debt_transactions.SingleOrDefault(a => a.id == debt_transaction.id);
                db.debt_transactions.DeleteOnSubmit(prev_debt_transaction);
                return debt_transaction;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<debt_transaction> GetShopDebtTransactionWithinDate(shop shop, DateTime from, DateTime to)
        { 
            return db.debt_transactions.Where(a=>a.shop == shop.id && a.trans_timestamp>=from && a.trans_timestamp<=to).ToList<debt_transaction>();
        }



        public List<debt_transaction> GetShopDebtTransactionWithinDate(shop shop, debt debt)
        {
            return db.debt_transactions.Where(a => a.shop == shop.id && a.debt == debt.id).ToList<debt_transaction>();
        }
    }
}