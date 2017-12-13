using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class bonuscompute
    {
        DateTime fromdate;
        DateTime todate;
        string ownership;
        decimal bonuspercentage;
        int bonustype; //netprofit = 1  sales =2 
        int state;
        transaction_category tran = new transaction_category();
        List<bonushelper> bonushelper = new List<bonushelper>();

        BettingSuiteDataContext db = new BettingSuiteDataContext();


        public bonuscompute(DateTime fromdate, DateTime todate, string ownership, decimal bonuspercentage, int bonustype, int state)
        {
            this.fromdate = fromdate;
            this.todate = todate;
            this.ownership = (ownership != "All") ? ownership : null;
            this.bonuspercentage = bonuspercentage;
            this.state = state;
            this.bonustype = bonustype;
            computeSales();
        }

        public bonuscompute()
        { }


        private void computeSales()
        {
            List<shop> shops = new List<shop>();
            if (ownership != null)
            {
                shops = db.shops.Where(a => a.shop_ownership == ownership).ToList<shop>();
            }
            if (state != -1)
            {
                shops = db.shops.Where(a => a.state == state).ToList<shop>();
            }

            List<int> shopint = shops.Select(a => a.id).ToList<int>();
            List<master_daily_sale> sales = db.master_daily_sales.Where(a => shopint.Contains(a.shop.Value) && a.date >= fromdate && a.date <= todate).ToList<master_daily_sale>();

            foreach (shop shop in shops)
            {
                decimal winnings = sales.Where(a => a.shop1 == shop).Sum(a => a.winnings).Value;
                decimal shopsales = sales.Where(a => a.shop1 == shop).Sum(a => a.sales).Value;
                decimal shopcomm = sales.Where(a => a.shop1 == shop).Sum(a => a.commision).Value;
                decimal netprofit = 0;
                decimal bonus = 0;
                if (bonustype == 1)
                {
                    netprofit = shopsales - winnings-shopcomm;
                    bonus = (netprofit > 0) ? netprofit * (bonuspercentage / 100) : 0;
                }
                else if (bonustype == 2)
                {
                    netprofit = shopsales - shopcomm;
                    bonus = (netprofit > 0) ? netprofit * (bonuspercentage / 100) : 0;
                }
                bonus = Math.Round(bonus, 2);
                bonushelper bhelper = new bonushelper(shop, shop.shop_code, shopsales, winnings, shopcomm, netprofit, bonus);

                bonushelper.Add(bhelper);
            }

        }

        public List<bonushelper> getBonus()
        {
            return bonushelper;
        }



        public string SaveBonusTransaction(List<bonushelper> bonus, transaction_category bonustransaction, DateTime fromdate, DateTime todate, admin_user user)
        {
            this.fromdate = fromdate;
            this.todate = todate;
            int completed = 0;
            int failed = 0;
           // Thread workerSms;

            //AutoSendSMS("234" + user.phone.Substring(1, user.phone.Length-1));




            foreach (bonushelper bhelp in bonus)
            {
                if (bhelp.Bonus > 0)
                {
                    try
                    {
                        //workerSms = new Thread(() => SaveTransaction(bhelp, bonustransaction, user));
                        //workerSms.Start();
                        //completed++;

                        SaveTransaction(bhelp, bonustransaction, user);
                        completed++;

                    }
                    catch (Exception)
                    {

                        failed++;
                    }
                }
            }
            return "Completed "+ completed +"   Failed " + failed;
        }




        public void SaveTransaction(bonushelper bhelp, transaction_category bonustransaction, admin_user user)
        {
            master_balance_sheet sendbal;
            transaction_category sendtran;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    master_balance_sheet bal = db.master_balance_sheets.SingleOrDefault(a => a.shop == bhelp.shop.id);
                    master_transaction transaction = new master_transaction
                    {
                        amount = bhelp.Bonus,
                        app_id = Application().app_id,
                        balance_after = getbal(bal.sales_balance, bhelp.Bonus, bonustransaction),
                        balance_before = bal.sales_balance,
                        description = bonustransaction.description + " " + "for " + fromdate.ToShortDateString() + " - " + todate.ToShortDateString(),
                        details = bonustransaction.description + " " + "for " + fromdate.ToShortDateString() + " - " + todate.ToShortDateString(),
                        posted_by = user.id,
                        shop = bhelp.shop.id,
                        trans_category = bonustransaction.id,
                        trans_timestamp = DateTime.Now,
                        trans_type = (bonustransaction.type == true) ? 0 : 1,

                    };

                    db.master_transactions.InsertOnSubmit(transaction);

                    master_balance_sheet prevbal = db.master_balance_sheets.SingleOrDefault(a => a.shop == bhelp.shop.id);
                    prevbal.sales_balance = getbal(bal.sales_balance, bhelp.Bonus, bonustransaction);
                    prevbal.netbalance = getbal(bal.netbalance, bhelp.Bonus, bonustransaction);

                    sendbal = prevbal;
                    sendtran = bonustransaction;
                    db.SubmitChanges();

                    scope.Complete();


                   

                }

                SendSms(bhelp, sendbal, sendtran);
                

            }
            catch (Exception)
            {

            }

        }

        private void SendSms(bonushelper bhelp, master_balance_sheet balancesheet, transaction_category btran)
        {
            try
            {
                bool mailvalidity = new Utilities.Mail_Validity_Helper().getMailSendingStatus();
                SMS_Vendor_Validity_Helper valhelp = new SMS_Vendor_Validity_Helper();
                sms_vendor vendor = valhelp.getVendorsByName("50kobo");

                bool smsvalidity = (DateTime.Today <= vendor.expiry_date) ? true : false;

                if (bhelp.shop.agent != null)
                {

                    sms message = new sms();
                    if (btran.code == "B")
                    {
                        if (smsvalidity)
                        {
                            sm mm = new CRUD.sms_crud().findByCode("BA");
                            message = new sms();

                            if (message.initialisebonusparams(bhelp.shop, mm.subject + "\r\n" + mm.content + "\r\n" + mm.conclusion, bhelp.Bonus, balancesheet.sales_balance.Value)) ;
                            message.CustomizeAndSendSMS();
                        }

                    }

                }
            }
            catch (Exception)
            { 
            
            }
        }


        public application Application()
        {
            application app = new application
            {
                app_id = Guid.NewGuid().ToString()
            };
            db.applications.InsertOnSubmit(app);
            return app;
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
    }
}