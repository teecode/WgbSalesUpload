using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class master_balance_sheet_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public master_balance_sheet master_balance_sheet_;
        public master_balance_sheet master_balance_sheet
        {
            get { return master_balance_sheet_; }
            set { master_balance_sheet_ = value; }
        }



        public master_balance_sheet_crud(master_balance_sheet master_balance_sheet_)
        {
            this.master_balance_sheet_ = master_balance_sheet_;
        }

        public master_balance_sheet_crud()
        {
            // TODO: Complete member initialization
        }

        public master_balance_sheet find_master_balance_sheet_By_ID(int id)
        {
            try
            {
                master_balance_sheet = db.master_balance_sheets.SingleOrDefault(a => a.id == id);
                return master_balance_sheet;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_balance_sheet find_master_balance_sheet_By_AppId(string Appid)
        {
            try
            {
                master_balance_sheet = db.master_balance_sheets.SingleOrDefault(a => a.App_id == Appid);
                return master_balance_sheet;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_balance_sheet find_master_balance_sheet_By_ShopId(int ShopId)
        {
            try
            {
                master_balance_sheet = db.master_balance_sheets.SingleOrDefault(a => a.shop == ShopId);
                return master_balance_sheet;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_balance_sheet insert_master_balance_sheet()
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
                    master_balance_sheet.App_id = app.app_id;
                    db.master_balance_sheets.InsertOnSubmit(master_balance_sheet);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_master_balance_sheet_By_AppId(master_balance_sheet.App_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_balance_sheet alter_master_balance_sheet()
        {
            try
            {
                master_balance_sheet prev_master_balance_sheet = db.master_balance_sheets.SingleOrDefault(a => a.id == master_balance_sheet.id);

                prev_master_balance_sheet.credit_balance = master_balance_sheet.credit_balance;
                prev_master_balance_sheet.netbalance = master_balance_sheet.netbalance;
                prev_master_balance_sheet.online_credit = master_balance_sheet.online_credit;
                prev_master_balance_sheet.sales_balance = master_balance_sheet.sales_balance;
                
                

                db.SubmitChanges();
                return master_balance_sheet;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_balance_sheet delete_master_balance_sheet()
        {
            try
            {
                master_balance_sheet prev_master_balance_sheet = db.master_balance_sheets.SingleOrDefault(a => a.id == master_balance_sheet.id);
                db.master_balance_sheets.DeleteOnSubmit(prev_master_balance_sheet);
                return master_balance_sheet;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public WgbSalesUpload.master_balance_sheet getMasterBalanceByShopId(int shopId)
        {
            return db.master_balance_sheets.SingleOrDefault(a => a.shop == shopId);
        }

        public List<master_balance_sheet> getBalanceByDebt(bool p)
        {
            if(p)//creditor
                return db.master_balance_sheets.Where(a => a.sales_balance < 0).OrderBy(a=>a.sales_balance).ToList<master_balance_sheet>();
            else //debtors
                return db.master_balance_sheets.Where(a => a.sales_balance >= 0).OrderByDescending(a=>a.sales_balance).ToList<master_balance_sheet>();
        }

        public List<master_balance_sheet> getBalanceByDebt(bool p, string ownership)
        {
            if (p)//creditor
                return db.master_balance_sheets.Where(a => a.sales_balance < 0 && a.shop1.shop_ownership == ownership).OrderBy(a => a.sales_balance).ToList<master_balance_sheet>();
            else //debtors
                return db.master_balance_sheets.Where(a => a.sales_balance >= 0 && a.shop1.shop_ownership == ownership).OrderByDescending(a => a.sales_balance).ToList<master_balance_sheet>();
        }

        public List<WgbSalesUpload.master_balance_sheet> getAllMasterBalances()
        {
            return db.master_balance_sheets.ToList<master_balance_sheet>();
        }



        public List<WgbSalesUpload.master_balance_sheet> getBalanceByDebtByState(bool p, List<state_list> statelist)
        {
            List<int> statelnt = statelist.Select(a => a.id).ToList<int>();
            
            if (p)//creditor
                return db.master_balance_sheets.Where(a => a.sales_balance < 0 && statelnt.Contains(a.shop1.state.Value)).OrderBy(a => a.sales_balance).ToList<master_balance_sheet>();
            else //debtors
                return db.master_balance_sheets.Where(a => a.sales_balance > 0 && statelnt.Contains(a.shop1.state.Value)).OrderByDescending(a => a.sales_balance).ToList<master_balance_sheet>();
        }
    }
}