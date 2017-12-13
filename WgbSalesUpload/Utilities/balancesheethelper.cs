using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;


namespace WgbSalesUpload.Utilities
{
    public class balancesheethelper
    {
       // private BettingSuiteDataContext dbmc;
        private master_daily_sale master_daily_sale;
        master_balance_sheet sheet;
        List<transaction_category> trancategory;

        public balancesheethelper(master_balance_sheet existsheet,master_daily_sale sale, List<transaction_category> trancategory)
        {
            //dbmc = dbmc_;
            master_daily_sale = sale;
           this.trancategory = trancategory;
           sheet = existsheet;
           if (sheet == null)
           {

               application app = new application
               {
                   app_id = Guid.NewGuid().ToString()
               };

               sheet = new master_balance_sheet
               {
                   App_id = app.app_id,
                   shop = master_daily_sale.shop,
                   netbalance = Decimal.Zero,
                   sales_balance = Decimal.Zero,
                   credit_balance = Decimal.Zero,
                   online_credit = Decimal.Zero
               };
           }
        }
       
        private void calulateBlanaceSheetAfterDailySalesAlter (Decimal amount)
        {
           // master_balance_sheet sheet = dbmc.master_balance_sheets.SingleOrDefault(a => a.shop == master_daily_sale.shop);
            

            Decimal sbal = sheet.sales_balance.Value;
            Decimal nsbal = Decimal.Add(sbal,amount);
            sheet.sales_balance = nsbal;
            sheet.netbalance = Decimal.Add(nsbal, (Decimal.Add((decimal)sheet.online_credit, (decimal)sheet.credit_balance)));
           
        }

        public List<master_transaction> getDailySalestransactions()
        {

            List<master_transaction> transactions = new List<master_transaction>(3);
            transactions.Add(initializeDailySalesTransaction( "S", (decimal)master_daily_sale.sales));
            transactions.Add(initializeDailySalesTransaction( "W", (decimal)master_daily_sale.winnings));
            transactions.Add(initializeDailySalesTransaction( "C", (decimal)master_daily_sale.commision));
           // calulateBlanaceSheetAfterDailySalesAlter();
            return transactions;
        }

        public List<master_transaction> getDailySalestransactionsReversal()
        {
            
            List<master_transaction> transactions = new List<master_transaction>(3);
            transactions.Add(initializeDailySalesTransaction("SR", (decimal)master_daily_sale.sales));
            transactions.Add(initializeDailySalesTransaction("WR", (decimal)master_daily_sale.winnings));
            transactions.Add(initializeDailySalesTransaction("CR", (decimal)master_daily_sale.commision));
            return transactions;
        }

        private master_transaction initializeDailySalesTransaction(string transcode, decimal amount)
        {
            transaction_category tranct = trancategory.SingleOrDefault(a => a.code == transcode);
            master_transaction trans = new master_transaction();
            trans.amount = master_daily_sale.sales;
            trans.shop = master_daily_sale.shop;
            trans.trans_timestamp = DateTime.Now;
            trans.details = tranct.name + " For " + ((DateTime)master_daily_sale.date).ToShortDateString();
            trans.description = tranct.name + " For " + ((DateTime)master_daily_sale.date).ToShortDateString();
            application app = new application
            {
                app_id = Guid.NewGuid().ToString()
            };
            trans.app_id = app.app_id;
           // master_balance_sheet sheet = dbmc.master_balance_sheets.SingleOrDefault(a => a.shop == master_daily_sale.shop);
             trans.balance_before =  sheet!= null ? sheet.sales_balance : Decimal.Zero;
            trans.posted_by = master_daily_sale.posted_by;
          // transaction_category tc = dbmc.transaction_categories.SingleOrDefault(a => a.code == transcode);
            trans.trans_category = tranct.id;
            trans.trans_type = (tranct.type == true )? 1 : 0;
           trans.amount =amount;
           calulateBlanaceSheetAfterDailySalesAlter(tranct.type == false ? Decimal.Negate((decimal)trans.amount): (decimal)trans.amount); 
           //trans.balance_after = Decimal.Add((decimal)trans.balance_before, (tc.type == false ? Decimal.Negate((decimal)trans.amount): (decimal)trans.amount));
           trans.balance_after = sheet.sales_balance;
            return trans;


        }


        public master_balance_sheet balancesheet 
        
        { 
            get 
        {
            
            return sheet;
        } 
            
            set{ sheet = value;} }
    }
}