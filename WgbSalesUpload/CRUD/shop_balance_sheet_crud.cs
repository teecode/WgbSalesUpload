using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class shop_balance_sheet_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public shop_balance_sheet shop_balance_sheet_;
        public shop_balance_sheet shop_balance_sheet
        {
            get { return shop_balance_sheet_; }
            set { shop_balance_sheet_ = value; }
        }

        public shop_balance_sheet_crud(shop_balance_sheet shop_balance_sheet_)
        {
            this.shop_balance_sheet_ = shop_balance_sheet_;
        }

        public shop_balance_sheet_crud()
        {
            // TODO: Complete member initialization
        }

        public shop_balance_sheet find_shop_balance_sheet_By_ID(int id)
        {
            try
            {
                shop_balance_sheet = db.shop_balance_sheets.SingleOrDefault(a => a.id == id);
                return shop_balance_sheet;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_balance_sheet find_shop_balance_sheet_By_AppId(string Appid)
        {
            try
            {
                shop_balance_sheet = db.shop_balance_sheets.SingleOrDefault(a => a.App_id == Appid);
                return shop_balance_sheet;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
        public shop_balance_sheet insert_shop_balance_sheet()
        {
            try
            {
                db.shop_balance_sheets.InsertOnSubmit(shop_balance_sheet);
                db.SubmitChanges();
                return find_shop_balance_sheet_By_AppId(shop_balance_sheet.App_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_balance_sheet alter_shop_balance_sheet()
        {
            try
            {
                shop_balance_sheet prev_shop_balance_sheet = db.shop_balance_sheets.SingleOrDefault(a => a.id == shop_balance_sheet.id);

                prev_shop_balance_sheet.credit_balance = shop_balance_sheet.credit_balance;
                prev_shop_balance_sheet.netbalance = shop_balance_sheet.netbalance;
                prev_shop_balance_sheet.online_credit = shop_balance_sheet.online_credit;
                prev_shop_balance_sheet.sales_balance = shop_balance_sheet.sales_balance;
                prev_shop_balance_sheet.online_credit = shop_balance_sheet.online_credit;
                prev_shop_balance_sheet.sales_balance = shop_balance_sheet.sales_balance;
                

                db.SubmitChanges();
                return shop_balance_sheet;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_balance_sheet delete_shop_balance_sheet()
        {
            try
            {
                shop_balance_sheet prev_shop_balance_sheet = db.shop_balance_sheets.SingleOrDefault(a => a.id == shop_balance_sheet.id);
                db.shop_balance_sheets.DeleteOnSubmit(prev_shop_balance_sheet);
                return shop_balance_sheet;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public WgbSalesUpload.shop_balance_sheet getBalanceForShop(int shopid)
        {
           return  db.shop_balance_sheets.SingleOrDefault(a => a.shop == shopid);
        }

        public WgbSalesUpload.shop_balance_sheet find_shop_balance_sheet_By_Shop(int shopId)
        {
            return db.shop_balance_sheets.SingleOrDefault(a => a.shop == shopId);
        }
    }

}