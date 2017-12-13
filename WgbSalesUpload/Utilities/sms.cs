using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;

namespace WgbSalesUpload.Utilities
{

    public class sms
    {

        admin_user user;
        string messages;
        private BettingSuiteDataContext db = new BettingSuiteDataContext();
        private bool sendToAllNumbers;

        public sms()
        {
        }

        public void CustomizeAndSendSMS()
        {
            //try
            //{
                if (user.phone.Trim() == "" && user.mobile.Trim() == "")
                    return;

                if (user.phone.Trim() != "")
                {
                    Thread workerSms;
                    workerSms = new Thread(() => AutoSendSMS("234" + user.phone.Substring(1, user.phone.Length - 1)));
                    workerSms.Start();
                    //AutoSendSMS("234" + user.phone.Substring(1, user.phone.Length-1));
                }
                if (sendToAllNumbers)
                {
                    if (user.mobile.Trim() != "")
                    {
                        Thread workerSms;
                        workerSms = new Thread(() => AutoSendSMS("234" + user.phone.Substring(1, user.phone.Length)));
                        workerSms.Start();
                       // AutoSendSMS("234" + user.phone.Substring(1, user.phone.Length-1));
                    }
                }
        
              
            //catch (Exception ex)
            //{

            }
        

        public void AutoSendSMS(string SingleNumber)
        {


            try
            {

                //  string baseurl = "http://api.clickatell.com/http/sendmsg?api_id=3486030&user=smartdev&password=SmartDevelopers@1&to=" + SingleNumber + "&text=" + FormatSMS(message);
                //   string baseurl = "http://login.betasms.com/customer/api/?usernme=balance@winnersgoldenbet.ng&password=wgbbalance2014&message=" + FormatSMS(message) +"&sender=WGBET&mobiles=" + SingleNumber;
                // string baseurl = "http://bulksmsinnigeria247.com/components/com_smsreseller/smsapi.php?username=wgbet&password=wgbbalance2014&sender=WGBET&recipient="+SingleNumber+"&message="+FormatSMS(message)+"&";     
                string baseurl = "http://www.50kobo.com/tools/geturl/Sms.php?username=balance@winnersgoldenbet.ng&password=wgbbalance2014&sender=WGBET&message=" + System.Web.HttpUtility.UrlPathEncode(FormatSMS(messages)) + "&recipients=" + SingleNumber;
                //string ValwitUrl = SendUrl.Replace("@@phoneNumber", SingleNumber).Replace("@@Message", message);
                WebRequest request = WebRequest.Create(baseurl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string result = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex)
            {

            }


        }


        public bool initializeParameters(shop shop, string message, master_lodgement_reimbursment trans)
              
        {
                    if (message.Contains("{lodgement_analysis}"))
                    {
                        string lodgementstring = "";
                        lodgementstring = "NGN" +trans.amount.ToString("#,0.00") +" to our "+ trans.bank1.code + " A/C";
                        message = message.Replace("{lodgement_analysis}", lodgementstring);
                    }

                    if (message.Contains("{reimbursment_analysis}"))
                    {
                        string reimbursmentstring = "";
                        reimbursmentstring = "NGN" + trans.amount.ToString("#,0.00");
                        message = message.Replace("{reimbursment_analysis}", reimbursmentstring);
                     }
                    messages = message;
                    return initializeParameters(shop, message, new object());

                       
                      
                  
                }

        public bool initializeParameters(shop shop, string message, commission_lodgement_reimbursment trans)
        {
            if (message.Contains("{lodgement_analysis}"))
            {
                string lodgementstring = "";
                lodgementstring = "NGN" + trans.amount.ToString("#,0.00") + " to our " + trans.bank1.code + " A/C";
                message = message.Replace("{lodgement_analysis}", lodgementstring);
            }

            if (message.Contains("{reimbursment_analysis}"))
            {
                string reimbursmentstring = "";
                reimbursmentstring = "NGN" + trans.amount.ToString("#,0.00");
                message = message.Replace("{reimbursment_analysis}", reimbursmentstring);
            }
            if (message.Contains("{sales_bal}"))
            {
                string salesbalstring = "";
                salesbalstring = "NGN" + new CRUD.commission_balance_crud().find_commission_balance_By_ShopId(shop.id).balance.Value.ToString("#,0.00");
                message = message.Replace("{sales_bal}", salesbalstring);
            }
            messages = message;
            return initializeParameters(shop, message, new object());




        }

        public bool initialisebonusparams(shop shop, string message, decimal bonus, decimal newbalance)
        {
            
            try
            {
                if (shop.agent != null)
                {
                    user = new CRUD.admin_users_crud().find_admin_user_By_ID(shop.agent.Value);
                }
                else if (shop.shopmanager_id != null)
                {
                    int shopmanager = shop.shopmanager_id.Value;
                    login login = new CRUD.logins_crud().find_login_By_ID(shopmanager);
                    user = new CRUD.admin_users_crud().find_admin_user_By_ID(login.admin_users[0].id);
                }
                else
                    user = new admin_user();

                if (user.phone != null && user.phone != "" && user.phone.Count() > 0)
                {
                 
                if (message.Contains("{shop_code}"))
                {
                    string shopstring = message;
                    shopstring =  shop.shop_code + " ";
                    message = message.Replace("{shop_code}", shopstring);
                }

                if (message.Contains("{bonus}"))
                {
                   
                        message = message.Replace("{bonus}", bonus.ToString());
                    
                }

                if (message.Contains("{prevbal}"))
                {

                    message = message.Replace("{prevbal}", (newbalance-bonus).ToString());

                }
                if (message.Contains("{new_balance}"))
                {

                    message = message.Replace("{new_balance}", newbalance.ToString());
                    
                } 
                    messages = message;
                }
                
            }
            catch(Exception)
            {
            
            }
            return true;

        }

        public bool initializeParameters(shop shop, string message, object date)
        {

            try 
	    {
        if (shop.agent != null)
        {
            user = new CRUD.admin_users_crud().find_admin_user_By_ID(shop.agent.Value);
        }
        else if (shop.shopmanager_id != null)
        {
            int shopmanager = shop.shopmanager_id.Value;
            login login = new CRUD.logins_crud().find_login_By_ID(shopmanager);
            user = new CRUD.admin_users_crud().find_admin_user_By_ID(login.admin_users[0].id);
        }
        else
            user = new admin_user();

            if (user.phone!=null && user.phone!= "" && user.phone.Count() > 0)
            {

                if (message.Contains("{shop_code}"))
                {
                    string shopstring = message;
                    shopstring = " " + shop.shop_code + " ";
                    message = message.Replace("{shop_code}", shopstring);
                }
                if (message.Contains("{shop_id}"))
                {
                    string shopstring = message;
                    shopstring = shop.id + " ";
                    message = message.Replace("{shop_id}", shopstring);
                }

                if (message.Contains("{debt_analysis}"))
                {
                    List<debt> debts = db.debts.Where(a => a.shop == shop.id && a.status == true).ToList<debt>();
                    if (debts.Count > 0)
                    {
                        string debtstring = "";
                        foreach (debt debt in debts)
                        {
                            debtstring += debt.debt_type1.name + " DEBT : " + "NGN" + debt.current_amount.Value.ToString("#,0.00") + " \r\n ";
                        }
                        message = message.Replace("{debt_analysis}", debtstring);
                    }
                }

                if (message.Contains("{date}"))
                {
                    string datestring = "";
                    if (date == null)
                        datestring = DateTime.Now.ToShortDateString();
                    else
                        datestring = ((DateTime)date).ToShortDateString();
                    message = message.Replace("{date}", datestring);
                }

                if (message.Contains("{curr_date}"))
                {
                    string datestring = "";
                    datestring = DateTime.Now.ToShortDateString();
                    message = message.Replace("{curr_date}", datestring +" ");
                }
                    if (message.Contains("{sales_analysis}"))
                    {
                        string salesanalysisstring = "";
                        salesanalysisstring = shop.shop_code + " NGN" + shop.master_balance_sheets[0].sales_balance.Value.ToString("#,0.00");
                        message = message.Replace("{sales_analysis}", salesanalysisstring);
                    }

                    if (message.Contains("{sales_bal}"))
                    {
                        string salesbalstring = "";
                        salesbalstring = "NGN"+shop.master_balance_sheets[0].sales_balance.Value.ToString("#,0.00");
                        message = message.Replace("{sales_bal}", salesbalstring);
                    }

                    if (message.Contains("{message}"))
                    {
                        string messagestring = message;
                        initializeParameters(shop, messagestring, date);
                        message = message.Replace("{message}", messagestring);
                    }

                    if (message.Contains("{bank_details}"))
                    {
                        string bankdetstring = message;
                        if (user.bank_list != null)
                            bankdetstring = user.bank_list.code + "(" + user.accountNo + ")";
                        else
                            bankdetstring = "";
                        message = message.Replace("{bank_details}", bankdetstring);
                    }
                    

                
                messages = message;
                return true;
	            }
            return false;
            }
	            catch (Exception)
	            {
                    return false;
	            }
            }

        

        private string FormatSMS(string message)
        {
            string newmessage = message;
           // newmessage = message.Replace("/r/n", "\n");
           newmessage = message.Replace("\r\n", "\n").Replace('\r', '\n');
           newmessage = message.Replace("\\r\\n", "\n").Replace('\r', '\n');
           return newmessage;
        }

    }    
}
