using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class paymentcodehelper
    {
        private string Shop_Code_;
        private string Payment_Code_;

        public paymentcodehelper(shop shop)
        {
            if (shop != null)
            {
                Payment_Code = "A" + shop.id;
                Shop_Code_ = shop.shop_code;
            }
            
        }

        public String ShopCode
        {
            get { return Shop_Code_; }
            set { Shop_Code_ = value; }
        }

        public String Payment_Code
        {
            get { return Payment_Code_; }
            set { Payment_Code_ = value; }
        }
    }
}