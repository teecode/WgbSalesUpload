using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class loginroles_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public loginrole loginrole_;
        public loginrole loginrole
        {
            get { return loginrole_; }
            set { loginrole_ = value; }
        }

        public loginroles_crud(loginrole loginrole_)
        {
            this.loginrole_ = loginrole_;
        }

        public loginroles_crud()
        {
            // TODO: Complete member initialization
        }


        public List<loginrole> getallroles()
        {
            return db.loginroles.ToList<loginrole>();
        }


        public loginrole find_loginrole_By_ID(int id)
        {
            try
            {
                loginrole = db.loginroles.SingleOrDefault(a => a.id == id);
                return loginrole;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public loginrole find_loginrole_By_AppId(string Appid)
        {
            try
            {
                loginrole = db.loginroles.SingleOrDefault(a => a.App_id == Appid);
                return loginrole;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public loginrole insert_loginrole()
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
                    loginrole.App_id = app.app_id;
                    db.loginroles.InsertOnSubmit(loginrole);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_loginrole_By_AppId(loginrole.App_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public loginrole alter_loginrole()
        {
            try
            {
                loginrole prev_loginrole = db.loginroles.SingleOrDefault(a => a.id == loginrole.id);

                prev_loginrole.role = loginrole.role;
               
                db.SubmitChanges();
                return loginrole;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public loginrole delete_loginrole()
        {
            try
            {
                loginrole prev_loginrole = db.loginroles.SingleOrDefault(a => a.id == loginrole.id);
                db.loginroles.DeleteOnSubmit(prev_loginrole);
                return loginrole;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}