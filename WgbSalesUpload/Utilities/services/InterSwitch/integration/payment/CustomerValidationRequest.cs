using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities.InterSwitch
{
    public class CustomerValidationRequest
    {
        private int merchantReference;
        private string serviceUsername, servicePassword, FtpUsername, FtpPassword, paymePaymentItemCode,custReference;
        //private 

        public CustomerValidationRequest()
        { 
        
        }

        public void setPaymentItemCode(string paymentCode)
        {
            this.paymePaymentItemCode = paymentCode;
        }
        public string getPaymentItemCode()
        {
            return paymePaymentItemCode;
        }

        public string getFtpPassword()
        {
            return FtpPassword;
        }

        public string getFtpUsername()
        {
            return FtpUsername;
        }

        public int getMerchantReference()
        {
            return merchantReference;
        }

        public string getCustReference()
        {
            return custReference;
        }

        public string getServicePassword()
        {
            return servicePassword;
        }

        public string getServiceUsername()
        {
            return serviceUsername;
        }

        public void setCustReference(string custReference)
        {
            this.custReference = custReference;
        }

        public void setFtpPassword(string FtpPassword)
        {
            this.FtpPassword = FtpPassword;
        }

        public void setFtpUsername(string FtpUsername)
        {
            this.FtpUsername = FtpUsername;
        }

        public void setMerchantRefrence(int merchantRefrence)
        {
            this.merchantReference = merchantRefrence;
        }

        public void setServicePassword(string servicePassword)
        {
            this.servicePassword = servicePassword;
        }

        public void setServiceUsername(string serviceUsername)
        {
            this.serviceUsername = serviceUsername;
        }


    }
    
}