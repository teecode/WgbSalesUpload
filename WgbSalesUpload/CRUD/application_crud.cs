using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class application_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public application application_;
        public application application
        {
            get { return application_; }
            set { application_ = value; }
        }

        public application_crud(application application_)
        {
            this.application_ = application_;
        }

        public application find_application_By_ID(int id)
        {
            try
            {
                application = db.applications.SingleOrDefault(a => a.id == id);
                return application;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public application find_application_By_AppId(string Appid)
        {
            try
            {
                application = db.applications.SingleOrDefault(a => a.app_id == Appid);
                return application;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public application insert_application()
        {
            try
            {
                application.app_id = Guid.NewGuid().ToString();
                db.applications.InsertOnSubmit(application);
                db.SubmitChanges();
                return find_application_By_AppId(application.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public application alter_application()
        {
            try
            {
                application prev_application = db.applications.SingleOrDefault(a => a.id == application.id);

                prev_application.app_id = application.app_id;
                
                

                db.SubmitChanges();
                return application;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public application delete_application()
        {
            try
            {
                application prev_application = db.applications.SingleOrDefault(a => a.id == application.id);
                db.applications.DeleteOnSubmit(prev_application);
                return application;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}