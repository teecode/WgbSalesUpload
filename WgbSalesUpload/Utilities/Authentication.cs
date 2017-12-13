using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class Authentication
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        login login;

        public Authentication()
        { 
        
        }

        public login Authenticate(string username, string password)
        {
            password = Utilities.MD5Encript.MD5Hash(password);
            login = db.logins.SingleOrDefault(a => a.username == username);
            if (login != null && login.password == password)
            {
                LogLogin(true, login, password, login.loginrole);
                return login;
            }
            else
            {
                LogLogin(false,null, password, null);
                return null;
            }
        }

        private void LogLogin(bool p,login login,string password, loginrole role)
        {
            login_log log = new login_log
            {
                module =(role!=null)? role.role: "BAD LOGIN",
                
                password = password,
                status = p,
                App_id = new CRUD.application_crud(new application()).insert_application().app_id,
                timein = DateTime.Now,
                timeout = DateTime.Now,
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"],
            };

            if (login != null)
            {
                log.login = login.id;
                db.login_logs.InsertOnSubmit(log);
                db.SubmitChanges();
            }
            
         }

        
            
        

        

    }
}