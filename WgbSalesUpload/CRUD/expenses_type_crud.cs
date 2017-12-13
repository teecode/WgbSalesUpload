using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class expenses_type_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public expenses_type expenses_type_;
        public expenses_type expenses_type
        {
            get { return expenses_type_; }
            set { expenses_type_ = value; }
        }

        public expenses_type_crud(expenses_type expenses_type_)
        {
            this.expenses_type_ = expenses_type_;
        }

        public expenses_type_crud()
        {
            // TODO: Complete member initialization
        }


        public List<expenses_type> getAllExpenseType()
        {
            return db.expenses_types.ToList<expenses_type>();
        }

        public expenses_type find_expenses_type_By_ID(int id)
        {
            try
            {
                expenses_type = db.expenses_types.SingleOrDefault(a => a.id == id);
                return expenses_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public expenses_type find_expenses_type_By_AppId(string Appid)
        {
            try
            {
                expenses_type = db.expenses_types.SingleOrDefault(a => a.app_id == Appid);
                return expenses_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public expenses_type insert_expenses_type()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    application app = new application
                    {
                        app_id = Guid.NewGuid().ToString()
                    };
                    db.applications.InsertOnSubmit(app);
                    expenses_type.app_id = app.app_id;
                    db.expenses_types.InsertOnSubmit(expenses_type);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_expenses_type_By_AppId(expenses_type.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public expenses_type alter_expenses_type()
        {
            try
            {
                expenses_type prev_expenses_type = db.expenses_types.SingleOrDefault(a => a.id == expenses_type.id);
                prev_expenses_type.description = expenses_type.description;
                prev_expenses_type.name = expenses_type.name;
                db.SubmitChanges();
                return expenses_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public expenses_type delete_expenses_type()
        {
            try
            {
                expenses_type prev_expenses_type = db.expenses_types.SingleOrDefault(a => a.id == expenses_type.id);
                db.expenses_types.DeleteOnSubmit(prev_expenses_type);
                return expenses_type;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}