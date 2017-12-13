using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class logins_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public login login_;
        public login login
        {
            get { return login_; }
            set { login_ = value; }
        }

        public logins_crud(login login_)
        {
            this.login_ = login_;
        }

        public logins_crud()
        {
            // TODO: Complete member initialization
        }

        public login find_login_By_ID(int id)
        {
            try
            {
                login = db.logins.SingleOrDefault(a => a.id == id);
                return login;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public login find_login_By_AppId(string Appid)
        {
            try
            {
                login = db.logins.SingleOrDefault(a => a.App_id == Appid);
                return login;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public login insert_login()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    application app = new application
                    {
                        app_id = Guid.NewGuid().ToString()
                    };
                    login.password = Utilities.MD5Encript.MD5Hash(login.password);
                    db.applications.InsertOnSubmit(app);
                    login.App_id = app.app_id;
                    db.logins.InsertOnSubmit(login);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_login_By_AppId(login.App_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        

        public login alter_login()
        {
            try
            {
                login prev_login = db.logins.SingleOrDefault(a => a.id == login.id);

                prev_login.password = Utilities.MD5Encript.MD5Hash(login.password);
                prev_login.role = login.role;
                prev_login.username = login.username;
                

                db.SubmitChanges();
                return login;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public login delete_login()
        {
            try
            {
                login prev_login = db.logins.SingleOrDefault(a => a.id == login.id);
                db.logins.DeleteOnSubmit(prev_login);
                return login;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<login> getAllLogins()
        {
            return db.logins.ToList<login>();
        }
    }
}