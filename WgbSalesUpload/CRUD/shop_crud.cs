using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using WgbSalesUpload.Utilities;

namespace WgbSalesUpload.CRUD
{
    public class shop_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public shop shop_;
        public shop shop
        {
            get { return shop_; }
            set { shop_ = value; }
        }

        public shop_crud(shop shop_)
        {
            this.shop_ = shop_;
        }

        public shop_crud()
        {
            // TODO: Complete member initialization
        }


        public List<shop> find_shop_by_Manager(int managerId)
        {
            return db.shops.Where(a => a.shopmanager_id == managerId).ToList<shop>();
        }

        public shop find_shop_By_ID(int id)
        {
            try
            {
                shop = db.shops.SingleOrDefault(a => a.id == id);
                return shop;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop find_shop_By_AppId(string Appid)
        {
            try
            {
                shop = db.shops.SingleOrDefault(a => a.app_id == Appid);
                return shop;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
        public shop insert_shop()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    application app = new application
                    {
                        app_id = Guid.NewGuid().ToString()
                    };
                    db.SubmitChanges();
                    shop.app_id = app.app_id;
                    shop.master_balance_sheets.Add(new master_balance_sheet{App_id = Guid.NewGuid().ToString(), credit_balance =0, netbalance=0, online_credit=0, sales_balance=0});
                    db.shops.InsertOnSubmit(shop);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_shop_By_AppId(shop.app_id);
            }
             catch (Exception)
            {
                return null;
            }
        }

        public shop alter_shop()
        {
            try
            {
                shop prev_shop = db.shops.SingleOrDefault(a => a.id == shop.id);

                
                prev_shop.credit_limit = shop.credit_limit;
                prev_shop.is_bettingshop = shop.is_bettingshop;
                prev_shop.is_onlinefunding = shop.is_onlinefunding;
                prev_shop.isactive = shop.isactive;
                prev_shop.join_date = shop.join_date;
                prev_shop.local_govt = shop.local_govt;
                prev_shop.shop_code = shop.shop_code;
                prev_shop.shop_name = shop.shop_name;
                prev_shop.shop_ownership = shop.shop_ownership;
                prev_shop.shop_type = shop.shop_type;
                prev_shop.commission = shop.commission;
                
                prev_shop.state = shop.state;
                prev_shop.state_local_govt = shop.state_local_govt;
                
                if(shop.shop_ownership == "WGB")
                    prev_shop.shopmanager_id = shop.shopmanager_id;
                else
                    prev_shop.agent = shop.agent;

                db.SubmitChanges();
                return shop;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop delete_shop()
        {
            try
            {
                shop prev_shop = db.shops.SingleOrDefault(a => a.id == shop.id);
                db.shops.DeleteOnSubmit(prev_shop);
                return shop;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<WgbSalesUpload.shop> getallShop()
        {
            return db.shops;
        }

        public IEnumerable<WgbSalesUpload.shop> getallShopWithOwner(string p)
        {
            return db.shops.Where(a => a.shop_ownership == p);
        }

        public WgbSalesUpload.shop find_shop_By_Code(string code)
        {
           return db.shops.SingleOrDefault(a => a.shop_code.ToUpper() == code.ToUpper());
        }

        public List<shop> getallagentShop(int agent)
        {
            return db.shops.Where(a => a.agent.Value == agent).ToList<shop>();
        }

        public List<WgbSalesUpload.shop> getAllDebtorsShop()
        {
            List<shop> shops = new List<WgbSalesUpload.shop>();
            List<master_balance_sheet> debtbal = db.master_balance_sheets.Where(a=>a.credit_balance > 0).ToList<master_balance_sheet>();
            foreach (master_balance_sheet bal in debtbal)
            {
                shops.Add(bal.shop1);
            }
            return shops;

        }

        public List<WgbSalesUpload.shop> getAllDebtorsShopBeyondCreditLimit()
        {
            List<shop> retshops = new List<shop>();
            List<shop> shops = new List<shop>();
                
            shops =db.shops.Where(a => a.agent != null).ToList<shop>();

            foreach (shop shop in shops)
            {
                try
                {
                    if (shop.master_balance_sheets != null && shop.master_balance_sheets[0].sales_balance > shop.credit_limit)
                    {
                        retshops.Add(shop);
                    }
                }
                catch (Exception)
                {
                    
                    
                }
            }

            return retshops;
           // shops = db.shops.Where(a => a.master_balance_sheets!= null && a.master_balance_sheets[0].sales_balance > a.credit_limit && a.agent.Value != null).ToList<shop>();
            
            return shops;

        }

        //public List<WgbSalesUpload.shop> getAllDebtorsShopBeyondCreditLimit(List<livesaleshelper> sales)
        //{
        //    List<shop> retshops = new List<shop>();
        //    List<shop> shops = new List<shop>();

        //   // shops = db.shops.Where(a => a.agent != null).ToList<shop>();
        //    sales = sales.Where(a => a.shop1.agent != null).ToList<livesaleshelper>();
        //    foreach (shop shop in shops)
        //    {
        //        if (shop.master_balance_sheets != null && shop.master_balance_sheets[0].sales_balance > shop.credit_limit)
        //        {
        //            retshops.Add(shop);
        //        }
        //    }

        //    return retshops;
        //    // shops = db.shops.Where(a => a.master_balance_sheets!= null && a.master_balance_sheets[0].sales_balance > a.credit_limit && a.agent.Value != null).ToList<shop>();

        //    return shops;

        //}

        public List<WgbSalesUpload.shop> getAllOwingsShop()
        {
            List<shop> shops = new List<WgbSalesUpload.shop>();
            List<master_balance_sheet> debtbal = db.master_balance_sheets.Where(a => a.sales_balance > 0).ToList<master_balance_sheet>();
            foreach (master_balance_sheet bal in debtbal)
            {
                shops.Add(bal.shop1);
            }
            return shops;
        }

        public List<WgbSalesUpload.shop> getAllOwingsAgentShop()
        {
            List<shop> shops = new List<WgbSalesUpload.shop>();
            List<master_balance_sheet> debtbal = db.master_balance_sheets.Where(a => a.sales_balance > 0).ToList<master_balance_sheet>();
            foreach (master_balance_sheet bal in debtbal)
            {
                if (bal.shop1.agent != null)
                {
                    shops.Add(bal.shop1);
                }
            }
            return shops;
        }

        public List<WgbSalesUpload.shop> getAgentShops()
        {
            return db.shops.Where(a => a.agent != null).ToList<shop>();
        }

        public List<WgbSalesUpload.shop> getallshopsinstate(List<state_list> statecordstates)
        {
            List<int> statelnt = statecordstates.Select(a => a.id).ToList<int>();
            return db.shops.Where(a => statelnt.Contains(a.state.Value)).ToList<shop>();
        }
    }
}