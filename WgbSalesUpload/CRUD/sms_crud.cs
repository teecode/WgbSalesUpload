using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class sms_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public sm sms_;
        public sm sms
        {
            get { return sms_; }
            set { sms_ = value; }
        }

        public sms_crud(sm sms_)
        {
            this.sms_ = sms_;
        }

        public sms_crud()
        {
            // TODO: Complete member initialization
        }

        public sm find_sms_By_ID(int id)
        {
            try
            {
                sms= db.sms.SingleOrDefault(a => a.id == id);
                return sms;
            }
            catch (Exception)
            {
                return null;
            }
        }




        public sm insert_sm()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //application app = new application
                    //{
                    //    app_id = Guid.NewGuid().ToString()
                    //};
                    //db.applications.InsertOnSubmit(app);
                    // sms.app_id = app.app_id;
                    db.sms.InsertOnSubmit(sms);
                    db.SubmitChanges();

                    scope.Complete();


                }
                 return sms;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public sm alter_sm()
        {
            try
            {
                sm prev_sms = db.sms.SingleOrDefault(a => a.id == sms.id);

                prev_sms.description = sms.description;
                prev_sms.category = sms.category;
                prev_sms.content = sms.content;
                prev_sms.conclusion = sms.conclusion;
                prev_sms.subject = sms.subject;
                   db.SubmitChanges();
                return sms;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public sm delete_sm()
        {
            try
            {
                sm prev_sms = db.sms.SingleOrDefault(a => a.id == sms.id);
                db.sms.DeleteOnSubmit(prev_sms);
                return sms;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<WgbSalesUpload.sm> getAllsms()
        {
            return db.sms.ToList<sm>();
        }

        public sm findByCode(string code)
        {
            try
            {
                sms = db.sms.SingleOrDefault(a => a.code == code);
                return sms;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}