using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;

namespace WgbSalesUpload.CRUD
{
    public class admin_users_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public admin_user admin_user_;
        public admin_user admin_user
        {
            get { return admin_user_; }
            set { admin_user_ = value; }
        }

        public admin_users_crud()
        { 
        
        }


        public List<admin_user> getUSerWithRole(int role)
        {
            try
            {
                return db.admin_users.Where(a => a.login1.role == role).OrderBy(a=>a.id).ToList<admin_user>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<admin_user> getUSerWithRole(string rolecode)
        {
            try
            {
                return db.admin_users.Where(a => a.login1.loginrole.code == rolecode).OrderBy(a => a.id).ToList<admin_user>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        

        public admin_users_crud(admin_user admin_user_)
        {
            this.admin_user_ = admin_user_;
        }

        public admin_user find_admin_user_By_ID(int id)
        {
            try
            {
                admin_user = db.admin_users.SingleOrDefault(a => a.id == id);
                return admin_user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public admin_user find_admin_user_By_AppId(string Appid)
        {
            try
            {
                admin_user = db.admin_users.SingleOrDefault(a => a.app_id == Appid);
                return admin_user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public admin_user insert_admin_user()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    application app = new application
                    {
                        app_id =  Guid.NewGuid().ToString()
                    };
                    db.applications.InsertOnSubmit(app);
                    
                    admin_user.app_id = app.app_id;
                    
                    if(admin_user.login1!=null)
                    {
                        admin_user.login1.password = Utilities.MD5Encript.MD5Hash(admin_user.login1.password);
                    }
                    db.admin_users.InsertOnSubmit(admin_user);
                    db.SubmitChanges();

                    scope.Complete();
                    

                }

                
                return find_admin_user_By_AppId(admin_user.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public admin_user alter_admin_user()
        {
            try
            {
                admin_user prev_admin_user = db.admin_users.SingleOrDefault(a => a.id == admin_user.id);

                prev_admin_user.accountNo = admin_user.accountNo;
                prev_admin_user.address = admin_user.address;
                prev_admin_user.Bank = admin_user.Bank;
                prev_admin_user.bbm_pin = admin_user.bbm_pin;
                prev_admin_user.default_credit_limit = admin_user.default_credit_limit;
                prev_admin_user.email = admin_user.email;
                prev_admin_user.firstname = admin_user.firstname;
                prev_admin_user.isactive = admin_user.isactive;
                prev_admin_user.join_date = admin_user.join_date;
                prev_admin_user.lastname = admin_user.lastname;
                prev_admin_user.middlename = admin_user.middlename;
                prev_admin_user.mobile = admin_user.mobile;
                prev_admin_user.name_title = admin_user.name_title;
                prev_admin_user.num_of_shops = admin_user.num_of_shops;
                prev_admin_user.phone = admin_user.phone;
                prev_admin_user.photopath = admin_user.photopath;
                prev_admin_user.sex = admin_user.sex;
                prev_admin_user.skype = admin_user.skype;
                prev_admin_user.account_name = admin_user.account_name;

                if (admin_user.login1 != null)
                {
                    if(admin_user.login1.password!=null)
                        prev_admin_user.login1.password = Utilities.MD5Encript.MD5Hash(admin_user.login1.password);
                    prev_admin_user.login1.role = admin_user.login1.role;
                }
                
                db.SubmitChanges();
                return admin_user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public admin_user delete_admin_user()
        {
            try
            {
                admin_user prev_admin_user = db.admin_users.SingleOrDefault(a => a.id == admin_user.id);
                db.admin_users.DeleteOnSubmit(prev_admin_user);
                return admin_user;
            }
            catch (Exception)
            {
                return null;
            }
        }



        public List<admin_user> getAllUsers()
        {
            return db.admin_users.ToList<admin_user>();
        }

        public List<admin_user> getAllOwingAgents()
        { 
            List<admin_user> retvalue = new List<admin_user>();
            //List<int> AllAgents =   getUSerWithRole("AG").Select(a=>a.id).ToList<int>();
            List<master_balance_sheet> creditbalances =  db.master_balance_sheets.Where(a=>a.credit_balance > 0).ToList<master_balance_sheet>();
            creditbalances.RemoveAll(a => a.shop1.agent == null);

            List<int> agents = creditbalances.Select(a => a.shop1.agent.Value).ToList<int>();
            return db.admin_users.Where(a => agents.Contains(a.id)).ToList<admin_user>();

            //foreach(master_balance_sheet bal in creditbalances)
            //{
                

            //    try 
            //    {	        
            //        retvalue.Add(bal.shop1.login.admin_users[0]);
            //    }
            //    catch (Exception)
            //    {
		            
            //    }
            //}

            //return retvalue;
        }

            //public List<admin_user>  getUSerWithRoleinState(int role, List<state_list> statecordstates, List<shop> shops)
            //{
            //    try
            //    {
            //        List<int> agentint = shops.wher
            //       // List<admin_user>  agents =  db.admin_users.Where(a => a.login1.role == role && ).OrderBy(a => a.id).ToList<admin_user>();
                
            //    }
            //    catch (Exception)
            //    {
            //        return null;
            //    }
            //}
    }
}