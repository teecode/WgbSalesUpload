using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class addressHelper
    {

        public string Shop_Code_;
        public string Address_;
        public string Phone_Number_;
        public string Mobile_;

        BettingSuiteDataContext db = new BettingSuiteDataContext();

        public addressHelper(shop shop)
        {
            Shop_Code = shop.shop_code  ;
            try
            {
                if (shop.agent != null)
                {
                    admin_user user = db.admin_users.SingleOrDefault(a => a.id == shop.agent);
                    Address = (user != null && user.address != null) ? user.address : "";
                    Phone_Number = (user != null && user.phone != null) ? user.phone : "";
                    Mobile = (user != null && user.mobile != null) ? user.mobile : "";
                }

                else if (shop.shopmanager_id != null)
                {
                    login login = db.logins.SingleOrDefault(a => a.id == shop.shopmanager_id);
                    admin_user user = login.admin_users[0];
                    Address = (user != null && user.address != null) ? user.address : "";
                    Phone_Number = (user != null && user.phone != null) ? user.phone : "";
                    Mobile = (user != null && user.mobile != null) ? user.mobile : "";
                }
            }
            catch (Exception)
            {
                Address ="";
                Phone_Number = "";
                Mobile = "";
                
            }
        }

        public string Shop_Code
        {
            get { return this.Shop_Code_; }
            set { this.Shop_Code_ = value; }
        }

        public string Address
        {
            get { return this.Address_; }
            set { this.Address_ = value; }
        }

        public string Phone_Number
        {
            get { return this.Phone_Number_; }
            set { this.Phone_Number_ = value; }
        }

        public string Mobile
        {
            get { return this.Mobile_; }
            set { this.Mobile_ = value; }
        }

    }
}