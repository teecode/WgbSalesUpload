using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.SessionState;

namespace WgbSalesUpload.CRUD
{
    public class login_log_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public login_log log;
        public login_log login_log
        {
            get { return log; }
            set { log = value; }
        }

        public login_log find_login_log_By_ID(int id)
        {
            try
            {
                login_log = db.login_logs.SingleOrDefault(a => a.id == id);
                return login_log;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public login_log find_login_log_By_AppId(string Appid)
        {
            try
            {
                login_log = db.login_logs.SingleOrDefault(a => a.App_id== Appid);
                return login_log;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public login_log_crud(login_log log)
        {
            this.log = log;
        }

        public login_log_crud()
        {
            // TODO: Complete member initialization
        }

        public login_log insert_login_log()
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
                    login_log.App_id = app.app_id;
                    db.login_logs.InsertOnSubmit(login_log);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_login_log_By_AppId(login_log.App_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public login_log alter_login_log()
        {
            try
            {
                login_log prev_login_log = db.login_logs.SingleOrDefault(a => a.id == login_log.id);
                
                prev_login_log.ip = login_log.ip;
                prev_login_log.module = login_log.module;
                prev_login_log.password = login_log.password;
                prev_login_log.status = login_log.status;
                prev_login_log.timein = login_log.timein;
                prev_login_log.timeout = login_log.timeout;
                prev_login_log.login = login_log.login;

                db.SubmitChanges();
                return login_log;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public login_log delete_login_log()
        {
            try
            {
                login_log prev_login_log = db.login_logs.SingleOrDefault(a => a.id == login_log.id);
                db.login_logs.DeleteOnSubmit(prev_login_log);
                return login_log;
            }
            catch (Exception)
            {
                return null;
            }
        }



        public List<login_log> getRecentLogin()
        {
            
            return db.login_logs.OrderByDescending(a => a.id).Take(10).ToList<login_log>();
        }

        public List<login_log> getRecentLogin(int loginId)
        {

            return db.login_logs.Where(b=>b.login == loginId).OrderByDescending(a => a.id).Take(10).ToList<login_log>();
        }
    }
}