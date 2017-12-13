using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class SalesSummaryProcessing
    {

        List<SummarySheet> SummarySheet;
        transaction_category lodgement;
        transaction_category reimbursement;
        transaction_category lodgementReversal;
        transaction_category reimbursementReversal;

        BettingSuiteDataContext db = new BettingSuiteDataContext();

        public SalesSummaryProcessing(List<master_transaction> maintransactions, List<master_daily_sale> dialysales, List<master_lodgement_reimbursment> mlr)
        {
            this.lodgement = new CRUD.transaction_category_crud().find_transaction_category_By_code("L");
            this.lodgementReversal = new CRUD.transaction_category_crud().find_transaction_category_By_code("LR");
            this.reimbursement = new CRUD.transaction_category_crud().find_transaction_category_By_code("R");
            this.reimbursementReversal = new CRUD.transaction_category_crud().find_transaction_category_By_code("RR");

            List<shop> shops = getShopsIntransaction(maintransactions);
            SummarySheet = new List<SummarySheet>();
            foreach (shop shop in shops)
            {
                try
                {
                    List<master_transaction> transactions = maintransactions.Where(a => a.shop == shop.id).ToList<master_transaction>();
                    List<master_daily_sale> ssales = dialysales.Where(a => a.shop == shop.id).ToList<master_daily_sale>();
                    List<master_lodgement_reimbursment> smlr = mlr.Where(a => a.shop == shop.id).ToList<master_lodgement_reimbursment>();
                    string shopcode = shop.shop_code;
                    decimal balbf = getbalbf(transactions, ssales);
                    decimal sales = getSales(ssales);
                    decimal winnings = getWinnings(ssales, transactions);
                    decimal commission = getCommission(ssales);
                    decimal reimbursement = getReimbursement(smlr);
                    decimal expectedlodgent = sales - winnings - commission + reimbursement;
                    decimal actuallodgement = getLodgement(smlr, transactions);
                    decimal bal = (expectedlodgent - actuallodgement) + balbf;

                    SummarySheet sheet = new SummarySheet(shopcode, balbf, sales, winnings, commission, reimbursement, expectedlodgent, actuallodgement, bal);

                    SummarySheet.Add(sheet);
                }
                catch (Exception)
                {
                    
                }
            }


        
        }

        public List<SummarySheet> GetSummarySheet()
        {
            return SummarySheet;
        }

        private decimal getSales(List<master_daily_sale> dailysales)
        {
            decimal retvalue = (dailysales.Sum(a => a.sales).Value); 
            return retvalue;
        }

        private decimal getWinnings(List<master_daily_sale> dailysales, List<master_transaction> transactions)
        {
            decimal retvalue = (dailysales.Sum(a => a.winnings).Value) + (transactions.Where(a => a.transaction_category.code == "LP").Sum(a => a.amount).Value - transactions.Where(a => a.transaction_category.code == "LPR").Sum(a => a.amount).Value);
            return retvalue;
        }

        private decimal getCommission(List<master_daily_sale> dailysales)
        {
            decimal retvalue = (dailysales.Sum(a => a.commision).Value);
            return retvalue;
        }

        private decimal getReimbursement(List<master_lodgement_reimbursment> mlr)
        {
            try
            {
                decimal retvalue = (mlr.Where(a => a.transaction_category == reimbursement.id).Sum(a => a.amount) - mlr.Where(a => a.transaction_category == reimbursementReversal.id).Sum(a => a.amount));
                return retvalue;
            }
            catch (Exception)
            {
                
                return 0;
            }
        }

        private decimal getLodgement(List<master_lodgement_reimbursment> mlr, List<master_transaction> transactions)
        {
            try
            {
                decimal retvalue = (mlr.Where(a => a.transaction_category == lodgement.id).Sum(a => a.amount) - mlr.Where(a => a.transaction_category == lodgementReversal.id).Sum(a => a.amount)) + (transactions.Where(a => a.transaction_category.code == "DCO").Sum(a => a.amount).Value - transactions.Where(a => a.transaction_category.code == "DCOR").Sum(a => a.amount).Value);
                return retvalue;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        

        private decimal getbalbf(List<master_transaction> transactions, List<master_daily_sale> sales)
        {
            int minsales = sales.Min(a => a.id);
            DateTime mindate = sales.SingleOrDefault(a => a.id == minsales).date.Value;
            string date = mindate.ToShortDateString();
            transactions = transactions.Where(a => a.details.Contains(date)).ToList<master_transaction>();
            int mintransaction = transactions.Min(a => a.id);

            decimal retvalue = transactions.SingleOrDefault(a => a.id == mintransaction).balance_before.Value;

            //int minsales = sales.Min(a => a.id);
            //DateTime mindate = sales.SingleOrDefault(a => a.id == minsales).date.Value;

            //List<master_transaction>  seltransactions = transactions.Where(a => a.details.Contains(mindate.ToShortDateString())).ToList<master_transaction>();
            //int mintranid = seltransactions.Min(a => a.id);

            //decimal retvalue = (transactions.SingleOrDefault(a => a.id == mintranid).balance_before.Value) + (transactions.Where(a => a.transaction_category.code == "CBF").Sum(a => a.amount).Value - transactions.Where(a => a.transaction_category.code == "CBFR").Sum(a => a.amount).Value) + (transactions.Where(a => a.transaction_category.code == "DBF").Sum(a => a.amount).Value - transactions.Where(a => a.transaction_category.code == "DBFR").Sum(a => a.amount).Value);
            return retvalue;
        }

        private List<shop>  getShopsIntransaction(List<master_transaction> transactions)
        {
            List<int> shopid = transactions.Select(a => a.shop.Value).Distinct().ToList<int>();

            return db.shops.Where(a => shopid.Contains(a.id)).ToList<shop>();


        }

    }
}