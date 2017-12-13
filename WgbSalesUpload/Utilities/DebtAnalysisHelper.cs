using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class DebtAnalysisHelper
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();

        List<master_transaction> transactions = new List<master_transaction>();
        List<debtAnalysis> debtAnalysis = new List<debtAnalysis>();
        List<master_balance_sheet> balances = new List<master_balance_sheet>();

        DateTime lastmindate;
        bool islastdate = false;


        public DebtAnalysisHelper()
        {
            transactions = db.master_transactions.ToList<master_transaction>();
            balances  = db.master_balance_sheets.ToList<master_balance_sheet>();

            List<shop> shops = db.shops.ToList<shop>();

            foreach (shop shop in shops)
            { 
                List<master_transaction> shoptran = transactions.Where(a=>a.shop == shop.id).ToList<master_transaction>();
                string ShopCode = shop.shop_code;
                decimal currentBal = getCurrentBalance(shop);
                decimal One_Week_BAl = getOneWeekBalance(shoptran, shop);
                One_Week_BAl = getvalue(One_Week_BAl, currentBal);
                decimal Two_Week_BAl = getTwoWeekBalance(shoptran, shop);
                Two_Week_BAl = getvalue(Two_Week_BAl, One_Week_BAl);
                decimal One_Month_BAl = getOneMonthBalance(shoptran, shop);
                One_Month_BAl = getvalue(One_Month_BAl, Two_Week_BAl);
                decimal Two_Month_BAl = getTwoMonthBalance(shoptran, shop);
                Two_Month_BAl = getvalue(Two_Month_BAl, One_Month_BAl);
                decimal Three_Month_BAl = getThreeMonthBalance(shoptran, shop);
                Three_Month_BAl = getvalue(Three_Month_BAl, Two_Month_BAl);
                decimal Six_Month_BAl = getSixMonthBalance(shoptran, shop);
                Six_Month_BAl = getvalue(Six_Month_BAl, Three_Month_BAl);
                decimal Older_BAl = getOtherBalance(shoptran, shop);
                Older_BAl = getvalue(Older_BAl, Six_Month_BAl);
                debtAnalysis debt = new debtAnalysis(ShopCode, currentBal, One_Week_BAl, Two_Week_BAl, One_Month_BAl, Two_Month_BAl, Three_Month_BAl, Six_Month_BAl, Older_BAl);
                debtAnalysis.Add(debt);
                islastdate = false;
                
            }
            

        }


        public decimal getvalue(decimal toget, decimal prevvalue)
        {
            if ( islastdate)
            {
                toget = 0;
            }
            else if (toget == 0)
            {
                toget = prevvalue;
            }
            return toget;
        }

        public List<debtAnalysis> getDebtAnalysis()
        {
            return debtAnalysis;
        }


        private decimal getOtherBalance(List<master_transaction> shoptran, shop shop)
        {
            try
            {
                DateTime date = DateTime.Today.AddMonths(-7);
                DateTime dateprev = DateTime.Today;
                List<master_transaction> mthtran = gettranwithindate(date, dateprev, shoptran);
                master_transaction maxtran = getMAxTransaction(mthtran, date);
                return (maxtran.balance_before.Value != 0) ? maxtran.balance_before.Value : maxtran.balance_after.Value;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        private decimal getSixMonthBalance(List<master_transaction> shoptran, shop shop)
        {
            try
            {
                DateTime date = DateTime.Today.AddMonths(-6);
                DateTime dateprev = DateTime.Today;
                List<master_transaction> mthtran = gettranwithindate(date,dateprev, shoptran);
                master_transaction maxtran = getMAxTransaction(mthtran, date);
                return (maxtran.balance_before.Value!=0)?maxtran.balance_before.Value : maxtran.balance_after.Value;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        private decimal getThreeMonthBalance(List<master_transaction> shoptran, shop shop)
        {
            try
            {
                DateTime date = DateTime.Today.AddMonths(-3);
                DateTime dateprev = DateTime.Today; 
                List<master_transaction> mthtran = gettranwithindate(date, dateprev, shoptran);
                master_transaction maxtran = getMAxTransaction(mthtran, date);
                return (maxtran.balance_before.Value != 0) ? maxtran.balance_before.Value : maxtran.balance_after.Value;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        private decimal getTwoMonthBalance(List<master_transaction> shoptran, shop shop)
        {
            try
            {
                DateTime date = DateTime.Today.AddMonths(-2);
                DateTime dateprev = DateTime.Today;
                List<master_transaction> mthtran = gettranwithindate(date, dateprev, shoptran);
                master_transaction maxtran = getMAxTransaction(mthtran, date);
                return (maxtran.balance_before.Value != 0) ? maxtran.balance_before.Value : maxtran.balance_after.Value;
            }
            catch (Exception)
            {
                
                return 0;
            }
        }

        private decimal getOneMonthBalance(List<master_transaction> shoptran, shop shop)
        {
            try
            {
                DateTime date = DateTime.Today.AddMonths(-1);
                DateTime dateprev = DateTime.Today;
                List<master_transaction> mthtran = gettranwithindate(date, dateprev, shoptran);
                master_transaction maxtran = getMAxTransaction(mthtran, date);
                return (maxtran.balance_before.Value != 0) ? maxtran.balance_before.Value : maxtran.balance_after.Value;
            }
            catch (Exception)
            {

                return 0;
            }
        }

        private decimal getTwoWeekBalance(List<master_transaction> shoptran, shop shop)
        {
            try
            {
                DateTime date = DateTime.Today.AddDays(-14);
                DateTime dateprev = DateTime.Today;
                List<master_transaction> weektran = gettranwithindate(date, dateprev, shoptran);
                master_transaction maxtran = getMAxTransaction(weektran, date);
                return (maxtran.balance_before.Value != 0) ? maxtran.balance_before.Value : maxtran.balance_after.Value;
            }
            catch (Exception)
            {
                
                return 0;
            }
        }

        private decimal getOneWeekBalance(List<master_transaction> shoptran, shop shop)
        {
            try
            {
                DateTime date = DateTime.Today.AddDays(-7);
                List<master_transaction> weektran = gettranwithindate(date, DateTime.Today, shoptran );
                master_transaction maxtran = getMAxTransaction(weektran, date);
                return (maxtran.balance_before.Value != 0) ? maxtran.balance_before.Value : maxtran.balance_after.Value;
            }
            catch (Exception)
            {
                
                return 0;
            }
        }

        private List<master_transaction> gettranwithindate(DateTime date, DateTime dateprev, List<master_transaction> tran)
        {
            try
            {
                return tran.Where(a => a.trans_timestamp >= date && a.trans_timestamp <=dateprev).ToList<master_transaction>();
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }

        private decimal getCurrentBalance(shop shop)
        {
            return balances.SingleOrDefault(a => a.shop == shop.id).sales_balance.Value;
        } 

        public master_transaction getMAxTransaction(List<master_transaction> transaction, DateTime date)
        {
            try
            {
                if (transaction != null && transaction.Count() > 0)
                {
                    //List<master_transaction> listdatetran = transaction.Where(a => a.trans_timestamp == date).ToList<master_transaction>();
                    //if (listdatetran != null && listdatetran.Count > 0)
                    //{
                        int id = transaction.Min(a => a.id);
                        master_transaction trans = transaction.SingleOrDefault(a => a.id == id);
                        if (trans.trans_timestamp == lastmindate)
                            islastdate = true;
                        else
                            islastdate = false;
                        lastmindate = trans.trans_timestamp;
                        return trans;
                    //}
                    //else
                    //    return null;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }


    }
}