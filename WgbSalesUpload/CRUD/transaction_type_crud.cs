using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WgbSalesUpload;

namespace WgbSalesUpload.CRUD
{
    public class transaction_type_crud
    {
       BettingSuiteDataContext db = new BettingSuiteDataContext();
        public transaction_type transaction_type_;
        public transaction_type transaction_type
        {
            get { return transaction_type_; }
            set { transaction_type_ = value; }
        }

        public transaction_type_crud(transaction_type transaction_type_)
        {
            this.transaction_type_ = transaction_type_;
        }

        public transaction_type_crud()
        {
            // TODO: Complete member initialization
        }

        public transaction_type find_transaction_type_By_ID(int id)
        {
            try
            {
                transaction_type = db.transaction_types.SingleOrDefault(a => a.id == id);
                return transaction_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public transaction_type find_transaction_type_By_AppId(string Appid)
        {
            try
            {
                transaction_type = db.transaction_types.SingleOrDefault(a => a.App_id == Appid);
                return transaction_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
        public transaction_type insert_transaction_type()
        {
            try
            {
                db.transaction_types.InsertOnSubmit(transaction_type);
                db.SubmitChanges();
                return find_transaction_type_By_AppId(transaction_type.App_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public transaction_type alter_transaction_type()
        {
            try
            {
                transaction_type prev_transaction_type = db.transaction_types.SingleOrDefault(a => a.id == transaction_type.id);

                
                prev_transaction_type.name = transaction_type.name;
                

                db.SubmitChanges();
                return transaction_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public transaction_type delete_transaction_type()
        {
            try
            {
                transaction_type prev_transaction_type = db.transaction_types.SingleOrDefault(a => a.id == transaction_type.id);
                db.transaction_types.DeleteOnSubmit(prev_transaction_type);
                return transaction_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<WgbSalesUpload.transaction_type> getAllTransactions()
        {
            return db.transaction_types.ToList<transaction_type>();
        }
    }
}