using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class shop_credit_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public shop_credit shop_credit_;
        public shop_credit shop_credit
        {
            get { return shop_credit_; }
            set { shop_credit_ = value; }
        }

        public shop_credit_crud(shop_credit shop_credit_)
        {
            this.shop_credit_ = shop_credit_;
        }

        public shop_credit find_shop_credit_By_ID(int id)
        {
            try
            {
                shop_credit = db.shop_credits.SingleOrDefault(a => a.id == id);
                return shop_credit;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_credit find_shop_credit_By_AppId(string Appid)
        {
            try
            {
                shop_credit = db.shop_credits.SingleOrDefault(a => a.app_id == Appid);
                return shop_credit;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
        public shop_credit insert_shop_credit()
        {
            try
            {
                db.shop_credits.InsertOnSubmit(shop_credit);
                db.SubmitChanges();
                return find_shop_credit_By_AppId(shop_credit.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_credit alter_shop_credit()
        {
            try
            {
                shop_credit prev_shop_credit = db.shop_credits.SingleOrDefault(a => a.id == shop_credit.id);

                prev_shop_credit.balance_after = shop_credit.balance_after;
                prev_shop_credit.balance_before = shop_credit.balance_before;
                prev_shop_credit.credit_type = shop_credit.credit_type;
                prev_shop_credit.shop = shop_credit.shop;
                prev_shop_credit.transaction = shop_credit.shop;
                

                db.SubmitChanges();
                return shop_credit;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_credit delete_shop_credit()
        {
            try
            {
                shop_credit prev_shop_credit = db.shop_credits.SingleOrDefault(a => a.id == shop_credit.id);
                db.shop_credits.DeleteOnSubmit(prev_shop_credit);
                return shop_credit;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}