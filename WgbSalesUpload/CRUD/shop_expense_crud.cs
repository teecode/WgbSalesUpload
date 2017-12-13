using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class shop_expense_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public shop_expense shop_expense_;
        public shop_expense shop_expense
        {
            get { return shop_expense_; }
            set { shop_expense_ = value; }
        }

        public shop_expense_crud(shop_expense shop_expense_)
        {
            this.shop_expense_ = shop_expense_;
        }

        public shop_expense_crud()
        {
            // TODO: Complete member initialization
        }

        public List<shop_expense> getExpensesWithindateAndShop(DateTime dt1, DateTime dt2, int shopID)
        {
           return db.shop_expenses.Where(a => a.date.Value >= dt1 && a.date <= dt2 && a.shop == shopID).ToList<shop_expense>();
        }

        public shop_expense find_shop_expense_By_ID(int id)
        {
            try
            {
                shop_expense = db.shop_expenses.SingleOrDefault(a => a.id == id);
                return shop_expense;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_expense find_shop_expense_By_AppId(string Appid)
        {
            try
            {
                shop_expense = db.shop_expenses.SingleOrDefault(a => a.app_id == Appid);
                return shop_expense;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
        public shop_expense insert_shop_expense()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    application app = new application
                    {
                        app_id = Guid.NewGuid().ToString()
                    };
                    db.SubmitChanges();
                    shop_expense.app_id = app.app_id;
                    db.shop_expenses.InsertOnSubmit(shop_expense);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_shop_expense_By_AppId(shop_expense.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_expense alter_shop_expense()
        {
            try
            {
                shop_expense prev_shop_expense = db.shop_expenses.SingleOrDefault(a => a.id == shop_expense.id);

                prev_shop_expense.amount = shop_expense.amount;
                prev_shop_expense.comment = shop_expense.comment;
                prev_shop_expense.date = shop_expense.date;
                prev_shop_expense.expense_type = shop_expense.expense_type;
                prev_shop_expense.islocked = shop_expense.islocked;
                prev_shop_expense.isverified = shop_expense.isverified;
                prev_shop_expense.shop = shop_expense.shop;
                prev_shop_expense.expense_type = shop_expense.expense_type;
                

                db.SubmitChanges();
                return shop_expense;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_expense delete_shop_expense()
        {
            try
            {
                shop_expense prev_shop_expense = db.shop_expenses.SingleOrDefault(a => a.id == shop_expense.id);
                db.shop_expenses.DeleteOnSubmit(prev_shop_expense);
                return shop_expense;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}