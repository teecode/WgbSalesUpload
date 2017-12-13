using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class credit_type_type_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public credit_type credit_type_;
        public credit_type credit_type
        {
            get { return credit_type_; }
            set { credit_type_ = value; }
        }

        public credit_type_type_crud(credit_type credit_type_)
        {
            this.credit_type_ = credit_type_;
        }

        public credit_type find_credit_type_By_ID(int id)
        {
            try
            {
                credit_type = db.credit_types.SingleOrDefault(a => a.id == id);
                return credit_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public credit_type find_credit_type_By_AppId(string Appid)
        {
            try
            {
                credit_type = db.credit_types.SingleOrDefault(a => a.App_id == Appid);
                return credit_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public credit_type insert_credit_type()
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
                    credit_type.App_id = app.app_id;
                    db.credit_types.InsertOnSubmit(credit_type);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_credit_type_By_AppId(credit_type.App_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public credit_type alter_credit_type()
        {
            try
            {
                credit_type prev_credit_type = db.credit_types.SingleOrDefault(a => a.id == credit_type.id);

                prev_credit_type.shop_credits = credit_type.shop_credits;
                prev_credit_type.description = credit_type.description;
                prev_credit_type.name = credit_type.name;
                

                db.SubmitChanges();
                return credit_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public credit_type delete_credit_type()
        {
            try
            {
                credit_type prev_credit_type = db.credit_types.SingleOrDefault(a => a.id == credit_type.id);
                db.credit_types.DeleteOnSubmit(prev_credit_type);
                return credit_type;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}