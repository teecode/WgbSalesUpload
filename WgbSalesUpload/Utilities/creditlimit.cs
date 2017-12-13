using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class creditlimit
    {
        private string shop_;
        private string phoneNumber_;
        private decimal currentbalance_;
        private decimal creditlimit_;
        private decimal creditdeficit_;
        private string remark_;



        private BettingSuiteDataContext db = new BettingSuiteDataContext();
        public creditlimit(shop shop)
        {
            int agentId = shop.agent.Value;
            admin_user user = db.admin_users.SingleOrDefault(a => a.id == agentId);
            shop_ = shop.shop_code;
            currentbalance_ = shop.master_balance_sheets[0].sales_balance.Value;
            creditlimit_ = shop.credit_limit.Value;
            creditdeficit_ = currentbalance_ - creditlimit_;
            phoneNumber_ = ((user.phone != null) ? user.phone : "" )+( (user.mobile != null) ? "/" + user.mobile : "");
            if (creditdeficit_ > 10000)
                Remark = "BLOCK";
            else
                Remark = "CREDIT CONTROLLER ";

        }

        public creditlimit(shop shop, decimal currentbalanace)
        {
            int agentId = shop.agent.Value;
            admin_user user = db.admin_users.SingleOrDefault(a => a.id == agentId);
            shop_ = shop.shop_code;
            currentbalance_ = currentbalanace;
            creditlimit_ = shop.credit_limit.Value;
            creditdeficit_ = currentbalance_ - creditlimit_;
            phoneNumber_ = ((user.phone != null) ? user.phone : "") + ((user.mobile != null) ? "/" + user.mobile : "");
            if (creditdeficit_ > 10000)
                Remark = "BLOCK";
            else
                Remark = "CREDIT CONTROLLER ";

        }

        public string Shop
        {
            get { return shop_; }
            set { shop_ = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber_; }
            set { phoneNumber_ = value; }
        }

        public decimal CurrentBalance
        {
            get { return currentbalance_; }
            set { currentbalance_ = value; }
        }

        public decimal CreditLimit
        {
            get { return creditlimit_; }
            set { creditlimit_ = value; }
        }

        public decimal CreditDeficit
        {
            get { return creditdeficit_; }
            set { creditdeficit_ = value; }
        }

        public string Remark
        {
            get { return remark_; }
            set { remark_ = value; }
        }

    }
}