using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WgbSalesUpload.Utilities.services.InterSwitch.integration.payment;

namespace WgbSalesUpload.Utilities.InterSwitch
{
    public class customer
    {
        private string firstname, lastname, middlename, email, phone, statusMessage, customerAletrnateReference, customerReferenceDescription;
        private int status, reference;
        private List<ItemHelper> PaymentItems;
        private shop shop;
        private admin_user user;
        private string prefix;

        public customer(string firstname, string lastname, string middlename, string email, string phone, string statusMessage, int status)
        {
            this.firstname = firstname;
            this.lastname = lastname;
            this.middlename = middlename;
            this.email = email;
            this.phone = phone;
            this.statusMessage = statusMessage;
            this.status = status;

        }

        public customer()
        {

        }


        public customer(shop shop)
        {
            admin_user user = new admin_user();
            try
            {

                if (shop.agent != null)
                {
                    //agent
                    user = new CRUD.admin_users_crud().find_admin_user_By_ID(shop.agent.Value);
                }
                else if (shop.shopmanager_id != null)
                {
                    user = shop.login.admin_users[0];

                }
                else status = 1;
                if (user != null)
                {
                    this.user = user;
                    this.shop = shop;
                    firstname = user.firstname;
                    middlename = user.lastname;
                    lastname = user.middlename;
                    email = user.email;
                    phone = user.phone;
                    statusMessage = "Customer is Valid";
                    status = 0;
                    reference = shop.id;
                    customerAletrnateReference = shop.shop_code;
                    customerReferenceDescription = user.account_name;

                }
                else
                status = 1;
            }
            catch (Exception ex)
            {
                //user = null;
                status = 1;
            }
        }



        public string getEmail()
        {
            return email;
        }

         public string getPrefix()
        {
            return prefix;
         }
         public void setPrefix(string prefix)
         {
            this.prefix = prefix;
         }
        public int getReference()
        {
            return reference;
        }
        public void setReference(int refr)
        {
            reference = refr;
        }

        public string getReferenceDescription()
        {
            return customerReferenceDescription;
        }
        public void setReferenceDescription(string description)
        {
            customerReferenceDescription = description;
        }

        public string getCustomerReferenceAlternate()
        {
            return customerAletrnateReference;
        }
        public void setCustomerReferenceAlternate(string alternate)
        {
            customerAletrnateReference = alternate;
        }

        public string getFirstname()
        {
            return firstname;
        }

        public string getLastname()
        {
            return lastname;
        }

        public string getMiddlename()
        {
            return middlename;
        }

        public string getPhone()
        {
            return phone;
        }

        public int getStatus()
        {
            return status;
        }

        public string getStatusMessage()
        {
            return statusMessage;
        }

        public List<ItemHelper> getPaymentItems()
        {
            return PaymentItems;
        }

        public void setPaymentItem(string paymentCode)
        {
            ItemHelper ItemHelper = null;
            string salescode = ConfigurationManager.AppSettings["SalesCode"];
            string OperationalDebtCode = ConfigurationManager.AppSettings["OperationalDebtCode"];
            string LeaseDebtCode = ConfigurationManager.AppSettings["LeaseDebtCode"];
            string ThermalPrinter = ConfigurationManager.AppSettings["Thermal Printer"];
            string VFLVDR = ConfigurationManager.AppSettings["VFL+VDR"];
            string VDR = ConfigurationManager.AppSettings["VDR"];
            string LUCKYSIX = ConfigurationManager.AppSettings["LUCKY SIX"];
            string NABGAMES = ConfigurationManager.AppSettings["NAB GAMES"];
            string OnlineAgency = ConfigurationManager.AppSettings["Online Agency"];
            //string OnlineUser = ConfigurationManager.AppSettings["Online User"];

            if (paymentCode.Equals(salescode))
                ItemHelper = new ItemHelper(shop.shop_code, "SALES", 1, decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"));
            else if (paymentCode.Equals(OperationalDebtCode))
                ItemHelper = new ItemHelper(shop.shop_code + " Operational Debt", "DEBT", 1, decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"));
            else if (paymentCode.Equals(LeaseDebtCode))
                ItemHelper = new ItemHelper(shop.shop_code + " Lease Debt", "LEASE", 1, decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"));
            else if (paymentCode.Equals(ThermalPrinter))
                ItemHelper = new ItemHelper(shop.shop_code + " Thermal Printer", "THERMAL", 1, decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"));
            else if (paymentCode.Equals(VFLVDR))
                ItemHelper = new ItemHelper(shop.shop_code + " VFL+VDR", "VIRTUALS", 1, decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"));
            else if (paymentCode.Equals(VDR))
                ItemHelper = new ItemHelper(shop.shop_code + " VDR", "VIRTUALS", 1, decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"));
            else if (paymentCode.Equals(LUCKYSIX))
                ItemHelper = new ItemHelper(shop.shop_code + " LUCKY SIX", "VIRTUALS", 1, decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"));
            else if (paymentCode.Equals(NABGAMES))
                ItemHelper = new ItemHelper(shop.shop_code + " NAB GAMES", "VIRTUALS", 1, decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"));
            else if (paymentCode.Equals(OnlineAgency))
                ItemHelper = new ItemHelper(user.login1.username + " ONLINE", "ONLINE", 1, decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"));
            // else if(paymentCode.Equals(OnlineUser))
            //ItemHelper = new ItemHelper("Operational Debt", user., 1, decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"), decimal.Parse("0.00"));
            if (ItemHelper != null)
            {
                if (PaymentItems == null)
                  PaymentItems = new List<ItemHelper>(1);
                  PaymentItems.Add(ItemHelper);
            }
        }



    }
}
