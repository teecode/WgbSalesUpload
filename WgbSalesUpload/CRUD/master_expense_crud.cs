using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class master_expense_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public master_expense master_expense_;
        public master_expense master_expense
        {
            get { return master_expense_; }
            set { master_expense_ = value; }
        }

        public master_expense_crud(master_expense master_expense_)
        {
            this.master_expense_ = master_expense_;
        }

        public master_expense find_master_expense_By_ID(int id)
        {
            try
            {
                master_expense = db.master_expenses.SingleOrDefault(a => a.id == id);
                return master_expense;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_expense find_master_expense_By_AppId(string Appid)
        {
            try
            {
                master_expense = db.master_expenses.SingleOrDefault(a => a.app_id == Appid);
                return master_expense;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
        public master_expense insert_master_expense()
        {
            try
            {
                db.master_expenses.InsertOnSubmit(master_expense);
                db.SubmitChanges();
                return find_master_expense_By_AppId(master_expense.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_expense alter_master_expense()
        {
            try
            {
                master_expense prev_master_expense = db.master_expenses.SingleOrDefault(a => a.id == master_expense.id);

                prev_master_expense.amount = master_expense.amount;
                prev_master_expense.comment = master_expense.comment;
                prev_master_expense.date = master_expense.date;
                prev_master_expense.expense_type = master_expense.expense_type;
                prev_master_expense.islocked = master_expense.islocked;
                prev_master_expense.isverified = master_expense.isverified;
                prev_master_expense.shop = master_expense.shop;
                prev_master_expense.expense_type = master_expense.expense_type;
                

                db.SubmitChanges();
                return master_expense;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_expense delete_master_expense()
        {
            try
            {
                master_expense prev_master_expense = db.master_expenses.SingleOrDefault(a => a.id == master_expense.id);
                db.master_expenses.DeleteOnSubmit(prev_master_expense);
                return master_expense;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}