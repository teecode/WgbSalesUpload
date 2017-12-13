using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class winningsHelper
    {
        public master_balance_sheet master_balance_sheet;
        public string account_name_;
        public string account_number;
        public string bank_;
        public string shop_code;
        public decimal sales_balance_;

        public winningsHelper(master_balance_sheet master_balance_sheet_)
        {

            try
            {
                this.master_balance_sheet = master_balance_sheet_;
                shop_code = master_balance_sheet.shop1.shop_code;
                admin_user user = new admin_user();
                if (master_balance_sheet.shop1.agent != null)
                {
                    user = new CRUD.admin_users_crud().find_admin_user_By_ID(master_balance_sheet.shop1.agent.Value);
                }
                else if (master_balance_sheet.shop1.shopmanager_id != null)
                {
                    int shopmanager =  master_balance_sheet.shop1.shopmanager_id.Value;
                    login login = new CRUD.logins_crud().find_login_By_ID(shopmanager);
                    user = new CRUD.admin_users_crud().find_admin_user_By_ID(login.admin_users[0].id);
                }
                AccountNumber = (user.accountNo!=null)? user.accountNo: "";

                AccountName = (user.account_name != null) ? user.account_name : "";
                Bank = (user.bank_list != null) ? user.bank_list.name : "";
                Balance = master_balance_sheet.sales_balance.Value;
            }
            catch (Exception)
            {
                this.master_balance_sheet = master_balance_sheet_;
                shop_code = master_balance_sheet.shop1.shop_code;
                AccountNumber = "";
                AccountName = "";
                Bank = "";
                Balance = master_balance_sheet.sales_balance.Value;
            }


        }



        public string Shop
        {
            get { return shop_code; }
            set { shop_code = value; }
        }

        public decimal Balance
        {
            get { return sales_balance_; }
            set { sales_balance_ = value; }
        }

        public string AccountName
        {
            get { return account_name_; }
            set { account_name_ = value; }
        }

        public string AccountNumber
        {
            get { return account_number; }
            set { account_number = value; }
        }

        public string Bank
        {
            get { return bank_; }
            set { bank_ = value; }
        }
    }
}