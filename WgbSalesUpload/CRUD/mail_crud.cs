using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class mail_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public mail mail_;
        public mail mail
        {
            get { return mail_; }
            set { mail_ = value; }
        }

        public mail_crud(mail mail_)
        {
            this.mail_ = mail_;
        }

        public mail_crud()
        {
            // TODO: Complete member initialization
        }

        public mail find_mail_By_ID(int id)
        {
            try
            {
                mail = db.mails.SingleOrDefault(a => a.id == id);
                return mail;
            }
            catch (Exception)
            {
                return null;
            }
        }

       


        public mail insert_mail()
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
                   // mail.app_id = app.app_id;
                    db.mails.InsertOnSubmit(mail);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return mail;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public mail alter_mail()
        {
            try
            {
                mail prev_mail = db.mails.SingleOrDefault(a => a.id == mail.id);

                prev_mail.description = mail.description;
                prev_mail.name = mail.name;
                prev_mail.content = mail.content;
                
                
                

                db.SubmitChanges();
                return mail;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public mail delete_mail()
        {
            try
            {
                mail prev_mail = db.mails.SingleOrDefault(a => a.id == mail.id);
                db.mails.DeleteOnSubmit(prev_mail);
                return mail;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<WgbSalesUpload.mail> getAllmails()
        {
            return db.mails.ToList<mail>();
        }

        public WgbSalesUpload.mail findByCode(string code)
        {
            return db.mails.SingleOrDefault(a => a.code == code); 
        }
    }
}