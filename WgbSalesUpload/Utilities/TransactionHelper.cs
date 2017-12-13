using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace WgbSalesUpload.Utilities
{
    public class TransactionHelper
    {
        
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        

        public bool SaveMasterLodgementTransaction(master_lodgement_reimbursment newmlr, master_transaction newmastertransaction, master_balance_sheet masterbalance)
        {
             try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    

                    newmastertransaction.app_id = Application().app_id;
                    newmlr.app_id = Application().app_id;
                    
                    db.master_transactions.InsertOnSubmit(newmastertransaction);
                    db.SubmitChanges();

                    newmlr.transaction = new CRUD.master_transaction_crud().find_master_transaction_By_AppId(newmastertransaction.app_id).id;
                    newmlr.transaction_type = (newmastertransaction.transaction_category.type == true) ? 1 : 0;
                    newmlr.transaction_category = newmastertransaction.trans_category;
                    db.master_lodgement_reimbursments.InsertOnSubmit(newmlr);

                    master_balance_sheet prev_master_balance_sheet = db.master_balance_sheets.SingleOrDefault(a => a.id == masterbalance.id);

                    prev_master_balance_sheet.credit_balance = masterbalance.credit_balance;
                    prev_master_balance_sheet.netbalance = masterbalance.netbalance;
                    prev_master_balance_sheet.online_credit = masterbalance.online_credit;
                    prev_master_balance_sheet.sales_balance = masterbalance.sales_balance;


                    db.SubmitChanges();

                    scope.Complete();




                }
                return true;
            }
             catch (Exception)
             {
                 return false;
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

        public bool SaveLapsePaymentTransaction(master_transaction newmastertransaction, master_balance_sheet masterbalance)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    newmastertransaction.app_id = Application().app_id;
                    
                    db.master_transactions.InsertOnSubmit(newmastertransaction);

                    master_balance_sheet prev_master_balance_sheet = db.master_balance_sheets.SingleOrDefault(a => a.id == masterbalance.id);

                    prev_master_balance_sheet.credit_balance = masterbalance.credit_balance;
                    prev_master_balance_sheet.netbalance = masterbalance.netbalance;
                    prev_master_balance_sheet.online_credit = masterbalance.online_credit;
                    prev_master_balance_sheet.sales_balance = masterbalance.sales_balance;


                    db.SubmitChanges();

                    scope.Complete();


                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SaveCutOffTransaction(debt_transaction newdebttransaction, debt debt, master_transaction newmastertransaction, master_balance_sheet masterbalance)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    newmastertransaction.app_id = Application().app_id;
                    debt.app_id = Application().app_id;
                    newdebttransaction.app_id = Application().app_id;
                    

                    db.master_transactions.InsertOnSubmit(newmastertransaction);

                    master_balance_sheet prev_master_balance_sheet = db.master_balance_sheets.SingleOrDefault(a => a.id == masterbalance.id);

                    prev_master_balance_sheet.credit_balance = masterbalance.credit_balance;
                    prev_master_balance_sheet.netbalance = masterbalance.netbalance;
                    prev_master_balance_sheet.online_credit = masterbalance.online_credit;
                    prev_master_balance_sheet.sales_balance = masterbalance.sales_balance;

                    db.debts.InsertOnSubmit(debt);


                    db.SubmitChanges();

                    newdebttransaction.debt = new CRUD.debt_crud().find_debt_By_AppId(debt.app_id).id;

                    db.debt_transactions.InsertOnSubmit(newdebttransaction);

                    db.SubmitChanges();

                    scope.Complete();


                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        public bool SaveMasterCommissionLodgementTransaction(commission_lodgement_reimbursment newmlr, commission_transaction newmastertransaction, commission_balance commissionbalance)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {


                    newmastertransaction.app_id = Application().app_id;
                    newmlr.app_id = Application().app_id;

                    db.commission_transactions.InsertOnSubmit(newmastertransaction);
                    db.SubmitChanges();

                    newmlr.transaction = new CRUD.commission_transaction_crud().find_commission_transaction_By_AppId(newmastertransaction.app_id).id;
                    newmlr.transaction_type = (newmastertransaction.transaction_category.type == true) ? 1 : 0;
                    newmlr.transaction_category = newmastertransaction.trans_category;
                    db.commission_lodgement_reimbursments.InsertOnSubmit(newmlr);

                    commission_balance prev_master_balance_sheet = db.commission_balances.SingleOrDefault(a => a.id == commissionbalance.id);

                    prev_master_balance_sheet.balance = commissionbalance.balance;
                    


                    db.SubmitChanges();

                    scope.Complete();




                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
