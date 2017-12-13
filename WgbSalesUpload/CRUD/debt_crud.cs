using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class debt_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public debt debt_;
        public debt debt
        {
            get { return debt_; }
            set { debt_ = value; }
        }

        public debt_crud(debt debt_)
        {
            this.debt_ = debt_;
        }

        public debt_crud()
        {
            // TODO: Complete member initialization
        }

        public debt find_debt_By_ID(int id)
        {
            try
            {
                debt = db.debts.SingleOrDefault(a => a.id == id);
                return debt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt find_debt_By_AppId(string Appid)
        {
            try
            {
                debt = db.debts.SingleOrDefault(a => a.app_id == Appid);
                return debt;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public debt insert_debt()
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
                    debt.app_id = app.app_id;
                    db.debts.InsertOnSubmit(debt);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_debt_By_AppId(debt.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt alter_debt()
        {
            try
            {
                debt prev_debt = db.debts.SingleOrDefault(a => a.id == debt.id);

                prev_debt.debt_type = debt.debt_type;
                prev_debt.amount = debt.amount;
                prev_debt.current_amount = debt.current_amount;
                prev_debt.startdate = debt.startdate;
                prev_debt.expirydate = debt.expirydate;
                prev_debt.proposed_installment = debt.proposed_installment;
                prev_debt.installment_count = debt.installment_count;
                prev_debt.proposed_installment_amount = debt.proposed_installment_amount;
                prev_debt.installment_duration = debt.installment_duration;
                prev_debt.status = debt.status;
                prev_debt.other_information = debt.other_information;
                
                

                db.SubmitChanges();
                return debt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt delete_debt()
        {
            try
            {
                debt prev_debt = db.debts.SingleOrDefault(a => a.id == debt.id);
                db.debts.DeleteOnSubmit(prev_debt);
                return debt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<WgbSalesUpload.debt> getAlldebts()
        {
            return db.debts.ToList<debt>();
        }

        public List<WgbSalesUpload.debt> getAllShopdebts(shop shop, List<debt> debts)
        {
            try
            {
                if (debts == null)
                    debts = db.debts.ToList<debt>();

                return debts.Where(a => a.shop == shop.id).ToList<debt>();
            }
            catch (Exception)
            {

                return debts;
            }
        }

        public List<WgbSalesUpload.debt> getAllAgentdebts(admin_user user, List<debt> debts)
        {
            try
            {
                if (debts == null)
                    debts = db.debts.ToList<debt>();
                List<debt> agentdebts = new List<debt>();
                List<shop> shops = db.shops.Where(a => a.agent == user.id).ToList<shop>();
                foreach (shop shop in shops)
                    agentdebts.AddRange(getAllShopdebts(shop, debts));

                return agentdebts;
            }
            catch (Exception)
            {

                return debts;
            }

        }

        public List<WgbSalesUpload.debt> getAlldebtsByInstallmentPeriod(debt_installment_duration duration, List<debt> debts)
        {
            if (debts == null)
                debts = db.debts.ToList<debt>();
            return debts.Where(a =>a.installment_duration == duration.id).ToList<debt>();
            
        }

        public List<WgbSalesUpload.debt> getAlldebtsByAmount(decimal amount, List<debt> debts)
        {
            if (debts == null)
                debts = db.debts.ToList<debt>();
            return debts.Where(a => a.current_amount >= amount).ToList<debt>();
        }





        public List<WgbSalesUpload.debt> getAllActiveDebt()
        {
            return db.debts.Where(a => a.status == true).ToList<debt>();
        }



        public List<WgbSalesUpload.debt> getAllAgentdebtsByState(List<int> state, List<debt> debts)
        {
            if (debts == null)
                debts = db.debts.ToList<debt>();

            List<debt> statedebts = new List<debt>();
            List<shop> shops = db.shops.Where(a => state.Contains(a.state.Value)).ToList<shop>();
            foreach (shop shop in shops)
                statedebts.AddRange(getAllShopdebts(shop, debts));

            return statedebts;
        }
    }
}