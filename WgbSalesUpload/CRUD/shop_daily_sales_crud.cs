using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class shop_daily_sale_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public shop_daily_sale shop_daily_sale_;
        public shop_daily_sale shop_daily_sale
        {
            get { return shop_daily_sale_; }
            set { shop_daily_sale_ = value; }
        }

        public shop_daily_sale_crud(shop_daily_sale shop_daily_sale_)
        {
            this.shop_daily_sale_ = shop_daily_sale_;
        }

        public shop_daily_sale find_shop_daily_sale_By_ID(int id)
        {
            try
            {
                shop_daily_sale = db.shop_daily_sales.SingleOrDefault(a => a.id == id);
                return shop_daily_sale;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_daily_sale find_shop_daily_sale_By_AppId(string Appid)
        {
            try
            {
                shop_daily_sale = db.shop_daily_sales.SingleOrDefault(a => a.app_id == Appid);
                return shop_daily_sale;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public shop_daily_sale insert_shop_daily_sale()
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

                    shop_daily_sale.app_id = app.app_id;
                    db.shop_daily_sales.InsertOnSubmit(shop_daily_sale);

                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_shop_daily_sale_By_AppId(shop_daily_sale.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_daily_sale alter_shop_daily_sale()
        {
            try
            {
                shop_daily_sale prev_shop_daily_sale = db.shop_daily_sales.SingleOrDefault(a => a.id == shop_daily_sale.id);

                prev_shop_daily_sale.balance = shop_daily_sale.balance;
                prev_shop_daily_sale.cancelled = shop_daily_sale.cancelled;
                prev_shop_daily_sale.commision = shop_daily_sale.commision;
                prev_shop_daily_sale.date = shop_daily_sale.date;
                prev_shop_daily_sale.date_posted = shop_daily_sale.date_posted;
                prev_shop_daily_sale.islocked = shop_daily_sale.islocked;
                prev_shop_daily_sale.isverified = shop_daily_sale.isverified;
                prev_shop_daily_sale.net_balance = shop_daily_sale.net_balance;
                prev_shop_daily_sale.posted_by = shop_daily_sale.posted_by;
                prev_shop_daily_sale.sales = shop_daily_sale.sales;
                prev_shop_daily_sale.shop = shop_daily_sale.shop;
                prev_shop_daily_sale.stake = shop_daily_sale.stake;
                prev_shop_daily_sale.winnings = shop_daily_sale.winnings;
                

                db.SubmitChanges();
                return shop_daily_sale;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_daily_sale delete_shop_daily_sale()
        {
            try
            {
                shop_daily_sale prev_shop_daily_sale = db.shop_daily_sales.SingleOrDefault(a => a.id == shop_daily_sale.id);
                db.shop_daily_sales.DeleteOnSubmit(prev_shop_daily_sale);
                return shop_daily_sale;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}