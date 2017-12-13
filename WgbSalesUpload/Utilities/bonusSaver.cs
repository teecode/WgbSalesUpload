using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class bonusSaver
    {
        List<master_balance_sheet> ShopBalancesToUpdate;
        List<master_transaction> transactionsToSave;
        List<debt> shopDebtsToUpdate;
        List<debt_transaction> debtTransactionToSave;



        List<bonushelper> bonuses;
        transaction_category bonustransaction; 
        DateTime fromdate; 
        DateTime todate;
        admin_user user;

        BettingSuiteDataContext db = new BettingSuiteDataContext();

        bool transtate = false;

        public bonusSaver(List<bonushelper> bonuses, transaction_category bonustransaction, DateTime fromdate, DateTime todate, admin_user user)
        {
            this.bonuses = bonuses;
            this.bonustransaction = bonustransaction;
            this.fromdate = fromdate;
            this.todate = todate;
            this.user = user;

            List<int> shopsIds = bonuses.Select(a => a.shop.id).ToList <int>();
            this.shopDebtsToUpdate = db.debts.Where(a => shopsIds.Contains(a.shop)).ToList<debt>();
            this.ShopBalancesToUpdate = db.master_balance_sheets.Where(a => shopsIds.Contains(a.shop.Value)).ToList<master_balance_sheet>();
            this.transactionsToSave = new List<master_transaction>();
            this.debtTransactionToSave = new List<debt_transaction>();


            ComputeBonuses();
            SaveTransactions();
            SendSms();
            transtate = true;
        }

        public bool Transtate()
        {
            return transtate;
        }

        private void SaveTransactions()
        {
           
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    db.master_transactions.InsertAllOnSubmit(transactionsToSave);
                    db.debt_transactions.InsertAllOnSubmit(debtTransactionToSave);
                    foreach (master_balance_sheet bal in ShopBalancesToUpdate)
                    {
                        master_balance_sheet prevbal = db.master_balance_sheets.SingleOrDefault(a => a.id == bal.id);
                        prevbal.credit_balance = bal.credit_balance;
                        prevbal.sales_balance = bal.sales_balance;
                        prevbal.netbalance = bal.netbalance;
                    }

                    foreach (debt debt in shopDebtsToUpdate)
                    {
                        debt prevdebt = db.debts.SingleOrDefault(a => a.id == debt.id);
                        prevdebt.current_amount = debt.current_amount;
                        prevdebt.status = debt.status;
                    }

                    db.SubmitChanges();
                    scope.Complete();

                }

                


            }
            catch (Exception)
            {

            }
        }

        private void ComputeBonuses()
        {
            
           
            //AutoSendSMS("234" + user.phone.Substring(1, user.phone.Length-1));




            foreach (bonushelper bhelp in bonuses)
            {
                if (bhelp.Bonus > 0)
                {
                    try
                    {
                        //workerSms = new Thread(() => SaveTransaction(bhelp, bonustransaction, user));
                        //workerSms.Start();
                        //completed++;


                        shop shop = bhelp.shop;

                        List<debt> shopdebt = shopDebtsToUpdate.Where(a => a.shop == shop.id).ToList<debt>();
                        

                        if(shopdebt.Count >0)
                        {
                            master_balance_sheet shopbal = ShopBalancesToUpdate.SingleOrDefault(a => a.shop == shop.id);
                            bhelp.Bonus = lodgeDebt(shopdebt, bhelp.Bonus, shopbal);
                        }
                        if (bhelp.Bonus > 0)
                        {
                            master_balance_sheet shopbal = ShopBalancesToUpdate.SingleOrDefault(a => a.shop == shop.id);
                            bhelp.Bonus = LodgeSales(shop, bhelp.Bonus, shopbal); 
                        }

                            

                        

                    }
                    catch (Exception)
                    {

                        
                    }
                }
            }
            
        }

        private decimal LodgeSales(shop shop, decimal bonus, master_balance_sheet shopbal)
        {
            
                    master_transaction tran = new master_transaction()
                    {
                        amount = bonus,
                        app_id = Application().app_id,
                        balance_after = getbal(shopbal.sales_balance, bonus, bonustransaction),
                        balance_before = shopbal.sales_balance,
                        description = bonustransaction.description + " " + "for " + fromdate.ToShortDateString() + " - " + todate.ToShortDateString(),
                        details = bonustransaction.description + " " + "for " + fromdate.ToShortDateString() + " - " + todate.ToShortDateString(),
                        posted_by = user.id,
                        shop = shop.id,
                        trans_category = bonustransaction.id,
                        trans_timestamp = DateTime.Now,
                        trans_type = (bonustransaction.type == true) ? 0 : 1,

                    };


                    shopbal.sales_balance = getbal(shopbal.sales_balance, bonus, bonustransaction);
                    shopbal.netbalance = getbal(shopbal.netbalance, bonus, bonustransaction);
                    transactionsToSave.Add(tran);
                    return bonus;
        }

        private decimal lodgeDebt(List<debt> shopdebt, decimal bonus, master_balance_sheet shopbal)
        {
           
            foreach (debt debt in shopdebt)
            {


                if (bonus > 0)
                {
                    //debt less than or equal to bonus
                    if (debt.current_amount <= bonus && debt.status == true && debt.current_amount > 0)
                    {
                        decimal temp = debt.current_amount.Value;

                        debt.current_amount = 0;

                        debt.status = false;

                        debt_transaction dtran = new debt_transaction
                        {
                            amount = temp,
                            app_id = Application().app_id,
                            balance_after = getbal(temp, temp, bonustransaction),
                            balance_before = temp,
                            description = bonustransaction.description + " " + "for " + fromdate.ToShortDateString() + " - " + todate.ToShortDateString(),
                            details = bonustransaction.description + " " + "for " + fromdate.ToShortDateString() + " - " + todate.ToShortDateString(),
                            posted_by = user.id,
                            shop = debt.shop,
                            trans_category = bonustransaction.id,
                            trans_timestamp = DateTime.Now,
                            trans_type = (bonustransaction.type == true) ? 0 : 1,
                            debt = debt.id,

                        };
                        bonus -= temp;
                        debtTransactionToSave.Add(dtran);

                        shopbal.credit_balance = getbal(shopbal.credit_balance, temp, bonustransaction);
                        shopbal.netbalance = getbal(shopbal.netbalance, temp, bonustransaction);

                    }
                    else if (debt.current_amount > bonus && debt.status == true && debt.current_amount > 0)
                    {

                        decimal temp = debt.current_amount.Value;
                        debt.current_amount -= bonus;



                        debt_transaction dtran = new debt_transaction
                        {
                            amount = bonus,
                            app_id = Application().app_id,
                            balance_after = getbal(temp, bonus, bonustransaction),
                            balance_before = temp,
                            description = bonustransaction.description + " " + "for " + fromdate.ToShortDateString() + " - " + todate.ToShortDateString(),
                            details = bonustransaction.description + " " + "for " + fromdate.ToShortDateString() + " - " + todate.ToShortDateString(),
                            posted_by = user.id,
                            shop = debt.shop,
                            trans_category = bonustransaction.id,
                            trans_timestamp = DateTime.Now,
                            trans_type = (bonustransaction.type == true) ? 0 : 1,
                            debt = debt.id,

                        };

                       
                        debtTransactionToSave.Add(dtran);

                        shopbal.credit_balance = getbal(shopbal.credit_balance, bonus, bonustransaction);
                        shopbal.netbalance = getbal(shopbal.netbalance, bonus, bonustransaction);
                        bonus -= bonus;

                    }
                }
            
            }

            return bonus;
        }

        private void SendSms()
        {
            try
            {
                bool mailvalidity = new Utilities.Mail_Validity_Helper().getMailSendingStatus();
                SMS_Vendor_Validity_Helper valhelp = new SMS_Vendor_Validity_Helper();
                sms_vendor vendor = valhelp.getVendorsByName("50kobo");

                bool smsvalidity = (DateTime.Today <= vendor.expiry_date) ? true : false;

                foreach(bonushelper bonus in bonuses)
                if (bonus.shop.agent != null && bonus.Bonus>0)
                {

                    sms message = new sms();
                    if (bonustransaction.code == "B")
                    {
                        if (smsvalidity)
                        {
                            sm mm = new CRUD.sms_crud().findByCode("BA");
                            message = new sms();

                            try
                            {
                                if (message.initialisebonusparams(bonus.shop, mm.subject + "\r\n" + mm.content + "\r\n" + mm.conclusion, bonus.Bonus, bonus.shop.master_balance_sheets[0].netbalance.Value))
                                    message.CustomizeAndSendSMS();
                            }
                            catch (Exception)
                            {
                                
                            }
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