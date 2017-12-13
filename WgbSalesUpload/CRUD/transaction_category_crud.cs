using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class transaction_category_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public transaction_category transaction_category_;
        public transaction_category transaction_category
        {
            get { return transaction_category_; }
            set { transaction_category_ = value; }
        }

        public transaction_category_crud(transaction_category transaction_category_)
        {
            this.transaction_category_ = transaction_category_;
        }

        public transaction_category_crud()
        {
            // TODO: Complete member initialization
        }

        public transaction_category find_transaction_category_By_ID(int id)
        {
            try
            {
                transaction_category = db.transaction_categories.SingleOrDefault(a => a.id == id);
                return transaction_category;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public transaction_category find_transaction_category_By_AppId(string Appid)
        {
            try
            {
                transaction_category = db.transaction_categories.SingleOrDefault(a => a.app_id == Appid);
                return transaction_category;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public transaction_category find_transaction_category_By_code(string code)
        {
            try
            {
                transaction_category tran = db.transaction_categories.SingleOrDefault(a => a.code == code);
                return tran;
            }
            catch (Exception)
            {
                return null;
            }
        }

        

        
        public transaction_category insert_transaction_category()
        {
            try
            {
                db.transaction_categories.InsertOnSubmit(transaction_category);
                db.SubmitChanges();
                return find_transaction_category_By_AppId(transaction_category.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public transaction_category alter_transaction_category()
        {
            try
            {
                transaction_category prev_transaction_category = db.transaction_categories.SingleOrDefault(a => a.id == transaction_category.id);

                prev_transaction_category.type = transaction_category.type;
                prev_transaction_category.code = transaction_category.code;
                prev_transaction_category.description = transaction_category.description;
                prev_transaction_category.name = transaction_category.name;
                

                db.SubmitChanges();
                return transaction_category;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public transaction_category delete_transaction_category()
        {
            try
            {
                transaction_category prev_transaction_category = db.transaction_categories.SingleOrDefault(a => a.id == transaction_category.id);
                db.transaction_categories.DeleteOnSubmit(prev_transaction_category);
                return transaction_category;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<WgbSalesUpload.transaction_category> getAllTransactions()
        {
            return db.transaction_categories.ToList<transaction_category>();
        }
    }
}