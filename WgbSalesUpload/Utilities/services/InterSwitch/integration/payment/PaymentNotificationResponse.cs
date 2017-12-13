using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities.InterSwitch
{
    public class PaymentNotificationResponse
    {
        private PaymentNotificationRequestHelper request;
        private int status;
        private string StatusMessage,PaymentLogId;
        private customer customer;
   

        public PaymentNotificationResponse(customer customer, PaymentNotificationRequestHelper request)
        {
             this.customer = customer;
             this.request = request;
             this.PaymentLogId = request.getPaymentLogId().ToString(); ;
        }

        public PaymentNotificationRequestHelper getRequest()
        {
            return request;
        }

        public void setRequest(PaymentNotificationRequestHelper request)
        {
            this.request = request;
        }

        public int getStatus()
        {
            return status;
        }

        public void setStatus(int status)
        {
            this.status = status;
        }

        public string getStatusMessage()
        {
            return StatusMessage;
        }

        public void setStatusMessage(string StatusMessage)
        {
            this.StatusMessage = StatusMessage;
        }

        public string getPaymentLogId()
        {
            return PaymentLogId;
        }

        public void setPaymentLogId(string PaymentLogId)
        {
            this.PaymentLogId = PaymentLogId;
        }

        public customer getCustomer()
        {
            return customer;
        }

        public void setCustomer(customer customer)
        {
            this.customer = customer;
        }

       
    }
}