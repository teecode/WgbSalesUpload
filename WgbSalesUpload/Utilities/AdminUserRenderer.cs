using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class AdminUserRenderer
    {
        public admin_user user_;
        public string name_;
        public int id_;


        public string name
        { 
            get{return name_;}
            set { name_ = value; }
        }

        public admin_user user
        {
            get { return user_; }
            set { user_ = value; }
        }

        public int id
        {
            get { return id_; }
            set { id_ = value; }
        }
        public AdminUserRenderer(admin_user user) 
        {
            this.id = user.id;
            this.name = ((user.firstname != null)?user.firstname.ToUpperInvariant():"") + " " + ((user.middlename != null) ? user.middlename.ToUpperInvariant() : "") + " " + ((user.lastname!=null)?user.lastname.ToUpperInvariant():"");
            this.user_ = user;

        }

        public void AdminUserLoginRenderer()
        {
            this.id = user.login1.id;
            this.name = ((user.firstname != null) ? user.firstname.ToUpperInvariant() : "") + " " + ((user.middlename != null) ? user.middlename.ToUpperInvariant() : "") + " " + ((user.lastname != null) ? user.lastname.ToUpperInvariant() : "");
        }
    }
}