using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class payment_type_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public payment_type payment_type_;
        public payment_type payment_type
        {
            get { return payment_type_; }
            set { payment_type_ = value; }
        }

        public payment_type_crud()
        {
        }


        public payment_type_crud(payment_type payment_type_)
        {
            this.payment_type_ = payment_type_;
        }

        public payment_type find_payment_type_By_ID(int id)
        {
            try
            {
                payment_type = db.payment_types.SingleOrDefault(a => a.id == id);
                return payment_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<payment_type> getallpayment_types()
        {
            return db.payment_types.OrderBy(a => a.name).ToList<payment_type>();
        }

        public payment_type find_payment_type_By_AppId(string Appid)
        {
            try
            {
                payment_type = db.payment_types.SingleOrDefault(a => a.app_id == Appid);
                return payment_type;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public payment_type insert_payment_type()
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
                    payment_type.app_id = app.app_id;
                    db.payment_types.InsertOnSubmit(payment_type);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_payment_type_By_AppId(payment_type.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public payment_type alter_payment_type()
        {
            try
            {
                payment_type prev_payment_type = db.payment_types.SingleOrDefault(a => a.id == payment_type.id);

                prev_payment_type.code = payment_type.code;
                prev_payment_type.name = payment_type.name;
               
                

                db.SubmitChanges();
                return payment_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public payment_type delete_payment_type()
        {
            try
            {
                payment_type prev_payment_type = db.payment_types.SingleOrDefault(a => a.id == payment_type.id);
                db.payment_types.DeleteOnSubmit(prev_payment_type);
                return payment_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<payment_type> getAllPaymentMode()
        {
            return db.payment_types.ToList<payment_type>();
        }
    }
}