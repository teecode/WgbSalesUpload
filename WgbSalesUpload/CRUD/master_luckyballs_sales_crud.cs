using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using WgbSalesUpload.Utilities;

namespace WgbSalesUpload.CRUD
{
    public class master_luckyballs_sales_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
         public master_luckyballs_sale master_luckyballs_sale_;
       
        public master_luckyballs_sale master_luckyballs_sale
        {
            get { return master_luckyballs_sale_; }
            set { master_luckyballs_sale_ = value; }
        }

        //public master_luckyballs_sales_crud(master_luckyballs_sale master_luckyballs_sale_, List<transaction_category> trancategory,List<master_balance_sheet> sheets)
        //{
        //    this.trancategory = trancategory;
        //    this.master_luckyballs_sale_ = master_luckyballs_sale_;
        //    this.sheets = sheets;
        //}

        public master_luckyballs_sales_crud(master_luckyballs_sale master_luckyballs_sale)
        {
            this.master_luckyballs_sale_ = master_luckyballs_sale;
        }

        public master_luckyballs_sale find_master_luckyballs_sale_By_ID(int id)
        {
            try
            {
                master_luckyballs_sale = db.master_luckyballs_sales.SingleOrDefault(a => a.id == id);
                return master_luckyballs_sale;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_luckyballs_sale find_master_luckyballs_sale_By_AppId(string Appid)
        {
            try
            {
                master_luckyballs_sale = db.master_luckyballs_sales.SingleOrDefault(a => a.app_id == Appid);
                return master_luckyballs_sale;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public shop find_shop_By_Code(string shopcode)
        {
            try
            {
                shop shop = db.shops.SingleOrDefault(a => a.shop_code == shopcode);

                return shop;
            }
            catch (Exception)
            {
                return null;
            }
        }

       public master_luckyballs_sale alter_master_luckyballs_sale()
        {
            try
            {
                master_luckyballs_sale prev_master_luckyballs_sale = db.master_luckyballs_sales.SingleOrDefault(a => a.id == master_luckyballs_sale.id);

                prev_master_luckyballs_sale.balance = master_luckyballs_sale.balance;
                prev_master_luckyballs_sale.cancelled = master_luckyballs_sale.cancelled;
                prev_master_luckyballs_sale.commision = master_luckyballs_sale.commision;
                prev_master_luckyballs_sale.date = master_luckyballs_sale.date;
                prev_master_luckyballs_sale.date_posted = master_luckyballs_sale.date_posted;
                prev_master_luckyballs_sale.islocked = master_luckyballs_sale.islocked;
                prev_master_luckyballs_sale.isverified = master_luckyballs_sale.isverified;
                prev_master_luckyballs_sale.net_balance = master_luckyballs_sale.net_balance;
                prev_master_luckyballs_sale.posted_by = master_luckyballs_sale.posted_by;
                prev_master_luckyballs_sale.sales = master_luckyballs_sale.sales;
                prev_master_luckyballs_sale.shop = master_luckyballs_sale.shop;
                prev_master_luckyballs_sale.stake= master_luckyballs_sale.stake;
                prev_master_luckyballs_sale.winnings = master_luckyballs_sale.winnings;
                db.SubmitChanges();
                return master_luckyballs_sale;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_luckyballs_sale delete_master_luckyballs_sale()
        {
            try
            {
                master_luckyballs_sale prev_master_luckyballs_sale = db.master_luckyballs_sales.SingleOrDefault(a => a.id == master_luckyballs_sale.id);
                db.master_luckyballs_sales.DeleteOnSubmit(prev_master_luckyballs_sale);
                return master_luckyballs_sale;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_luckyballs_sale insert_master_luckyballs_sale()
        {
            
            bool isupdate = false;
            shop shop = find_shop_By_Code(master_luckyballs_sale.shop1.shop_code);
            if(shop!=null)
            {
                try
                {
                WgbSalesUpload.master_luckyballs_sale prev_master_luckyballs_sale = db.master_luckyballs_sales.SingleOrDefault(u => u.shop1.id == shop.id && u.date == master_luckyballs_sale.date);
               using (TransactionScope scope = new TransactionScope())
                { 
                       if (prev_master_luckyballs_sale != null)
                    {
                        isupdate = true;
                        prev_master_luckyballs_sale.balance = master_luckyballs_sale.balance;
                        prev_master_luckyballs_sale.cancelled = master_luckyballs_sale.cancelled;
                        prev_master_luckyballs_sale.commision = master_luckyballs_sale.commision;
                        prev_master_luckyballs_sale.date = master_luckyballs_sale.date;
                        prev_master_luckyballs_sale.date_posted = master_luckyballs_sale.date_posted;
                        prev_master_luckyballs_sale.islocked = master_luckyballs_sale.islocked;
                        prev_master_luckyballs_sale.isverified = master_luckyballs_sale.isverified;
                        prev_master_luckyballs_sale.net_balance = master_luckyballs_sale.net_balance;
                        prev_master_luckyballs_sale.posted_by = master_luckyballs_sale.posted_by;
                        prev_master_luckyballs_sale.sales = master_luckyballs_sale.sales;
                       // prev_master_luckyballs_sale.shop = master_luckyballs_sale.shop;
                        prev_master_luckyballs_sale.stake = master_luckyballs_sale.stake;
                        prev_master_luckyballs_sale.winnings = master_luckyballs_sale.winnings;
                        prev_master_luckyballs_sale.single_stake_sales = master_luckyballs_sale.single_stake_sales;
                        prev_master_luckyballs_sale.single_stake_commision = master_luckyballs_sale.single_stake_commision;
                        prev_master_luckyballs_sale.single_stake_percent = master_luckyballs_sale.single_stake_percent;
                        prev_master_luckyballs_sale.other_stake_commision = master_luckyballs_sale.other_stake_commision;
                       // master_balance_sheet sheet = bsh.balancesheet;
                       // master_balance_sheet existsheet = db.master_balance_sheets.SingleOrDefault(a => a.shop == master_luckyballs_sale.shop);

                        //bsh = new balancesheethelper(existsheet, prev_master_luckyballs_sale, trancategory);
                        //List<master_transaction> translist2 = bsh.getDailySalestransactions();
                        ////lodge commission for aunty bola
                        //if (shop.shop_ownership == "WGB")
                        //{
                        //    transaction_category trancatcomm = trancategory.SingleOrDefault(a => a.code == "C");
                        //    LodgeCommission(translist2.SingleOrDefault(a => a.trans_category == trancatcomm.id), trancatcomm);
                        //}
                        //db.master_transactions.InsertAllOnSubmit(translist2);
                        //master_balance_sheet sheet2 = bsh.balancesheet;
                        //   if (existsheet == null)
                        //{
                        //    db.master_balance_sheets.InsertOnSubmit(sheet2);
                        //}
                        //else
                        //{
                        //    existsheet.netbalance = sheet.netbalance;
                        //    existsheet.sales_balance = sheet.sales_balance;
                        //    existsheet.online_credit = sheet.online_credit;
                        //    existsheet.credit_balance = sheet.credit_balance;
                        //    new CRUD.master_balance_sheet_crud(existsheet).alter_master_balance_sheet();

                        //}
                            db.SubmitChanges();
                       }
                       
                    if(!isupdate)
                    {
                        application app = new application
                         {
                             app_id = Guid.NewGuid().ToString()
                         };
                         app =  new application_crud(app).insert_application();
                        db.SubmitChanges();
                        master_luckyballs_sale.app_id = app.app_id;
                        master_luckyballs_sale.shop1 = shop;
                           master_luckyballs_sale.date_posted = DateTime.Today;


                           if (master_luckyballs_sale.shop1 != null)
                           {
                               db.master_luckyballs_sales.InsertOnSubmit(master_luckyballs_sale);
                               //    bsh = new balancesheethelper(existsheet, master_luckyballs_sale, trancategory);
                               //    List<master_transaction> translist = bsh.getDailySalestransactions();
                               //    //lodge commission for aunty bola
                               //    if (shop.shop_ownership == "WGB")
                               //    {
                               //        transaction_category trancatcomm = trancategory.SingleOrDefault(a => a.code == "C");
                               //        LodgeCommission(translist.SingleOrDefault(a => a.trans_category == trancatcomm.id),  trancatcomm);
                               //    }
                               //    db.master_transactions.InsertAllOnSubmit(translist);
                               //    master_balance_sheet sheet = bsh.balancesheet;
                               //    //master_balance_sheet existsheet = db.master_balance_sheets.SingleOrDefault(a => a.shop == master_luckyballs_sale.shop);
                               //    if (existsheet == null)
                               //    {
                               //        db.master_balance_sheets.InsertOnSubmit(sheet);
                               //    }
                               //    else
                               //    {
                               //        existsheet.netbalance = sheet.netbalance;
                               //        existsheet.sales_balance = sheet.sales_balance;
                               //        existsheet.online_credit = sheet.online_credit;
                               //        existsheet.credit_balance = sheet.credit_balance;
                               //        new CRUD.master_balance_sheet_crud(existsheet).alter_master_balance_sheet();

                               //    }
                               //}
                               // db.master_balance_sheets.InsertOnSubmit(sheet);
                               db.SubmitChanges();
                           }
                            }
                        
                          scope.Complete();   
                     }  
                 
                return  master_luckyballs_sale;
            
               }

               catch (Exception)
            {
                return null;
            }
        }
            else
            {
              return null;
            }
        }

        public master_luckyballs_sale geterrorsale()
        {
            return master_luckyballs_sale;
        }

        //private void LodgeCommission(master_transaction master_transaction, transaction_category tran)
        //{
        //    commission_balance balance = db.commission_balances.SingleOrDefault(a => a.shop == master_transaction.shop);

        //    if (balance == null)
        //    {
        //        balance = new commission_balance
        //        {
        //            shop = master_transaction.shop.Value,
        //            balance = 0
                    
        //        };
        //    }

        //    transaction_category commtran = null;
        //    if (tran.code == "CR")
        //    {
        //        commtran = db.transaction_categories.SingleOrDefault(a => a.code == "CTR");
        //    }
        //    else if (tran.code == "C")
        //    {
        //        commtran = db.transaction_categories.SingleOrDefault(a => a.code == "CT");
        //    }
        //    commission_transaction ctran = new commission_transaction
        //    {
        //        amount = master_transaction.amount,
        //        balance_after = getbal(balance.balance, master_transaction.amount, commtran),
        //        balance_before = balance.balance,
        //        description = master_transaction.details,
        //        details = master_transaction.details,
        //        posted_by = master_transaction.posted_by,
        //        shop = master_transaction.shop,
        //        trans_category = commtran.id,
        //        trans_timestamp = master_transaction.trans_timestamp,
        //        trans_type = commtran.type == true ? 1 : 0
        //    };

        //    balance.balance = getbal(balance.balance, master_transaction.amount, commtran).Value;


        //    using (TransactionScope scope = new TransactionScope())
        //    {
        //        if (balance.app_id == null)
        //        {
        //            balance.app_id = getAppID().app_id;
        //            db.commission_balances.InsertOnSubmit(balance);
        //        }
        //        ctran.app_id = getAppID().app_id;
        //        db.commission_transactions.InsertOnSubmit(ctran);
        //        db.SubmitChanges();
        //        scope.Complete();

        //    }




        //}

        public WgbSalesUpload.application getAppID()
        { 
            application app = new application
                         {
                             app_id = Guid.NewGuid().ToString()
                         };
            db.applications.InsertOnSubmit(app);
            return app;
        }

        
        public WgbSalesUpload.master_luckyballs_sale getsalesOfShopForDate(DateTime date, shop shop)
        {
            return db.master_luckyballs_sales.SingleOrDefault(a => a.shop1 == shop && a.date == date);
        }

        private decimal? getbal(decimal? bal1, decimal? bal2, transaction_category tran)
        {
            if (tran.type == false)
            {
                return bal1 - bal2;
            }
            else
                return bal1 + bal2;
        }

        public List<string> getSuspectedNonPayOutShops()
        {
            try
            {
                List<shop> shops = new CRUD.shop_crud().getallShop().ToList<shop>();
                List<string> shopstodisplay = new List<string>();
                var badshops =  db.master_luckyballs_sales.Where(a => a.date >= DateTime.Today.AddDays(-5) && a.date <= DateTime.Today).GroupBy(b => b.shop).Select(s => new { shop = s.Key, sum = s.Sum(y => y.winnings), sale = s.Sum(y => y.winnings) }).Where(e => e.sale > 10000);
                foreach(var t in badshops)
                {
                    shopstodisplay.Add(shops.SingleOrDefault(a => a.id == t.shop.Value).shop_code);
                }
                return shopstodisplay;
            }
            catch (Exception)
            {
                return new List<string>();
            }

        }

        public Boolean issalesAvailableForDate(DateTime date)
        {
            try
            {
                var ret = db.master_luckyballs_sales.Where(a => a.date == date);
                if (ret.Count() > 0)
                    return true;
                return false;
            }
            catch (Exception)
            {
                
                return false;
            }
              
        }

        public List<WgbSalesUpload.master_luckyballs_sale> getSalesWithinDAte(DateTime dateTime1, DateTime dateTime2)
        {
            return db.master_luckyballs_sales.Where(a => a.date >= dateTime1 && a.date <= dateTime2).ToList<master_luckyballs_sale>();
        }
    }



 }