using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class loginroleaccess_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public loginroleaccess loginroleaccess_;
        public loginroleaccess loginroleaccess
        {
            get { return loginroleaccess_; }
            set { loginroleaccess_ = value; }
        }

        public loginroleaccess_crud(loginroleaccess loginroleaccess_)
        {
            this.loginroleaccess_ = loginroleaccess_;
        }

        public loginroleaccess find_loginroleaccess_By_ID(int id)
        {
            try
            {
                loginroleaccess = db.loginroleaccesses.SingleOrDefault(a => a.id == id);
                return loginroleaccess;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public loginroleaccess find_loginroleaccess_By_AppId(string Appid)
        {
            try
            {
                loginroleaccess = db.loginroleaccesses.SingleOrDefault(a => a.App_id == Appid);
                return loginroleaccess;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public loginroleaccess insert_loginroleaccess()
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
                    loginroleaccess.App_id = app.app_id;
                    db.loginroleaccesses.InsertOnSubmit(loginroleaccess);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_loginroleaccess_By_AppId(loginroleaccess.App_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public loginroleaccess alter_loginroleaccess()
        {
            try
            {
                loginroleaccess prev_loginroleaccess = db.loginroleaccesses.SingleOrDefault(a => a.id == loginroleaccess.id);

                prev_loginroleaccess.hasaccess = loginroleaccess.hasaccess;
                prev_loginroleaccess.loginrole = loginroleaccess.loginrole;
                
                

                db.SubmitChanges();
                return loginroleaccess;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public loginroleaccess delete_loginroleaccess()
        {
            try
            {
                loginroleaccess prev_loginroleaccess = db.loginroleaccesses.SingleOrDefault(a => a.id == loginroleaccess.id);
                db.loginroleaccesses.DeleteOnSubmit(prev_loginroleaccess);
                return loginroleaccess;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}