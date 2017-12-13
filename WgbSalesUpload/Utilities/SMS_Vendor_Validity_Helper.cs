using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class SMS_Vendor_Validity_Helper
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();

        public List<sms_vendor> getAllVendors()
        {
            return db.sms_vendors.ToList<sms_vendor>();
        }

        public sms_vendor getVendorsByName(string name)
        {
            return db.sms_vendors.SingleOrDefault(a => a.vendor_name == name);
        }

        public bool rechargeVendor(string name, int newcredits)
        {
            try{
              sms_vendor vendor =  getVendorsByName(name);
              vendor.remaining_credits+= newcredits;
              db.SubmitChanges();
             return true;
            }
            catch (Exception ex)
            {
              return false;
            }
        }

        public bool getSMSSendingStatus(string name)
        {
            sms_vendor vendor = getVendorsByName(name);
            if (vendor.expiry_date >= DateTime.Today)
            {
                return true;
            }
            else
                return false;
        }

    }
}