using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace WgbSalesUpload.CRUD
{
    public class commission_balance_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public commission_balance commission_balance_;
        public commission_balance commission_balance
        {
            get { return commission_balance_; }
            set { commission_balance_ = value; }
        }



        public commission_balance_crud(commission_balance commission_balance_)
        {
            this.commission_balance_ = commission_balance_;
        }

        public commission_balance_crud()
        {
            // TODO: Complete member initialization
        }

        public commission_balance find_commission_balance_By_ID(int id)
        {
            try
            {
                commission_balance = db.commission_balances.SingleOrDefault(a => a.id == id);
                return commission_balance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public commission_balance find_commission_balance_By_AppId(string Appid)
        {
            try
            {
                commission_balance = db.commission_balances.SingleOrDefault(a => a.app_id == Appid);
                return commission_balance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public commission_balance find_commission_balance_By_ShopId(int ShopId)
        {
            try
            {
                commission_balance = db.commission_balances.SingleOrDefault(a => a.shop == ShopId);
                return commission_balance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public commission_balance insert_commission_balance()
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
                    commission_balance.app_id = app.app_id;
                    db.commission_balances.InsertOnSubmit(commission_balance);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_commission_balance_By_AppId(commission_balance.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public commission_balance alter_commission_balance()
        {
            try
            {
                commission_balance prev_commission_balance_sheet = db.commission_balances.SingleOrDefault(a => a.id == commission_balance.id);

                prev_commission_balance_sheet.balance = commission_balance.balance;
                
                
                

                db.SubmitChanges();
                return commission_balance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public commission_balance delete_commission_balance()
        {
            try
            {
                commission_balance prev_commission_balance = db.commission_balances.SingleOrDefault(a => a.id == commission_balance.id);
                db.commission_balances.DeleteOnSubmit(prev_commission_balance);
                return commission_balance;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public WgbSalesUpload.commission_balance getCommissionBalanceByShopId(int shopId)
        {
            return db.commission_balances.SingleOrDefault(a => a.shop == shopId);
        }

        public List<commission_balance> getBalanceByDebt(bool p)
        {
            if(p)//creditor
                return db.commission_balances.Where(a => a.balance < 0).OrderBy(a=>a.balance).ToList<commission_balance>();
            else //debtors
                return db.commission_balances.Where(a => a.balance >= 0).OrderByDescending(a=>a.balance).ToList<commission_balance>();
        }

        

        public List<WgbSalesUpload.commission_balance> getAllCommissionBalances()
        {
            return db.commission_balances.ToList<commission_balance>();
        }


    }
}
