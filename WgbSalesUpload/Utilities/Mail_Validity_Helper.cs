using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class Mail_Validity_Helper
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public mail_validity MailValidity()
        {
            return db.mail_validities.First();
        }

        public bool ReactivateMail(string validationcode, int days, int val)
        {
            mail_validity mailvalidity = db.mail_validities.First();
            if (mailvalidity.validatycode == validationcode && val == 1)
            {
                mailvalidity.expirydate = mailvalidity.expirydate.Value.AddDays(days);
                db.SubmitChanges();
                return true;
            }
            else if (mailvalidity.validatycode == validationcode && val == 2)
            {
                //sms_vendor smsv;
                db.sms_vendors.SingleOrDefault(a => a.vendor_name == "50kobo").expiry_date.AddDays(days);
                //smsv.expiry_date = smsv.expiry_date.AddDays(days);
                db.SubmitChanges();
                return true;
            }
            else
                return false;

        }

        public bool getMailSendingStatus()
        {
            mail_validity validity =MailValidity();
            if (validity.expirydate >= DateTime.Today)
            {
                return true;
            }
            else
            return false;
        }

        public sms_vendor SmsValidity()
        {
            return db.sms_vendors.First();
        }
    }
}