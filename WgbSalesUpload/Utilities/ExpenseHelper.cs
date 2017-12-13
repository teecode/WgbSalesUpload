using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace WgbSalesUpload.Utilities
{
    public class ExpenseHelper
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public int VerifyExPenses(List<shop_expense> ShopExpenses, bool verifymode)
        {
            int returnValue = 0;
            try
            {
                
                using (TransactionScope scope = new TransactionScope())
                    {
                    
                    transaction_category tran = new CRUD.transaction_category_crud().find_transaction_category_By_code((verifymode)?"E":"ER");

                        foreach (shop_expense shopexp in ShopExpenses)
                        {
                            
                            shop_expense expenses = db.shop_expenses.SingleOrDefault(a => a.id == shopexp.id);
                            if (expenses.isverified != verifymode)
                            {
                                expenses.islocked = verifymode;
                                expenses.isverified = verifymode;
                                shop_balance_sheet mastsheet;
                                shop_balance_sheet sheet = (new CRUD.shop_balance_sheet_crud().getBalanceForShop(shopexp.shop.Value));

                                if (sheet == null)
                                {
                                    mastsheet = new shop_balance_sheet
                                    {
                                        App_id = Application().app_id,
                                        credit_balance = 0,
                                        netbalance = 0,
                                        online_credit = 0,
                                        sales_balance = 0,
                                        shop = shopexp.shop.Value
                                    };
                                }
                                else
                                    mastsheet = sheet;

                                shop_transaction transaction = new shop_transaction
                                {
                                    app_id = Application().app_id,
                                    amount = shopexp.amount.Value,
                                    balance_before = mastsheet.netbalance,
                                    balance_after = getbal(mastsheet.netbalance, shopexp.amount, tran),
                                    posted_by = shopexp.shop1.login.id,
                                    shop = shopexp.shop,
                                    trans_category = tran.id,
                                    trans_timestamp = DateTime.Now,
                                    trans_type = (tran.type == true) ? 1 : 0,
                                    details = "Exp For - "+shopexp.comment,
                                };

                                mastsheet.sales_balance = getbal(mastsheet.sales_balance, transaction.amount, tran);
                                mastsheet.netbalance = getbal(mastsheet.netbalance, transaction.amount, tran);

                                
                                db.shop_transactions.InsertOnSubmit(transaction);
                                
                                if (sheet == null)
                                    db.shop_balance_sheets.InsertOnSubmit(mastsheet);
                                else
                                {
                                    shop_balance_sheet prevbalsheet = db.shop_balance_sheets.SingleOrDefault(a => a.shop == shopexp.shop);
                                    prevbalsheet.netbalance = mastsheet.netbalance;
                                    prevbalsheet.online_credit = mastsheet.online_credit;
                                    prevbalsheet.credit_balance = mastsheet.credit_balance;
                                    prevbalsheet.sales_balance = mastsheet.sales_balance;
                                }

                                    db.SubmitChanges();
                                    scope.Complete();
                                    returnValue++;
                            }
                        }

                    }
                    return returnValue;
                }
                catch (Exception)
                {
                    return 0;
                }


            
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

        public application Application()
        {
            application app = new application
            {
                app_id = Guid.NewGuid().ToString()
            };
            db.applications.InsertOnSubmit(app);
            return app;
        }


        public bool SaveMasterLodgementTransaction(shop_lodgement_reimbursment newmlr, shop_transaction newmastertransaction, shop_balance_sheet masterbalance)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    newmastertransaction.app_id = Application().app_id;

                    db.shop_transactions.InsertOnSubmit(newmastertransaction);
                    db.SubmitChanges();

                    newmlr.transaction = new CRUD.shop_transaction_crud().find_shop_transaction_By_AppId(newmastertransaction.app_id).id;

                    db.shop_lodgement_reimbursments.InsertOnSubmit(newmlr);

                    shop_balance_sheet prev_master_balance_sheet = db.shop_balance_sheets.SingleOrDefault(a => a.id == masterbalance.id);

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
    }
}
