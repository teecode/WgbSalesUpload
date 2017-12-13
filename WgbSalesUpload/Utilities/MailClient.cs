using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

using System.Net;
using System.Net.Mime;
using System.Threading;

namespace WgbSalesUpload.Utilities
{
    public class MailClient
    {
        public string errmsg = "";
        MailMessage mail;

        public bool SendPersonalMail(string toadd, string subject, string message )
        {
            //MailMessage msg = new MailMessage();
            //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            //try
            //{
            //    msg.Subject = subject;
            //    msg.To.Add(toadd);
            //    msg.Body = message;
            //    client.Send(msg);
            //    return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}


            MailMessage msg = new MailMessage();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            try
            {

                msg.Subject = subject;
                msg.To.Add(toadd);
                // msg.Body = message;
                //MailMessage mailMessage = new MailMessage();
                msg.IsBodyHtml = true;
                client.EnableSsl = true;
                Thread workerSms;

                string html = message;
                //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
                mail = msg;
                ThreadStart threadStart = delegate() { SendCreatedMail(msg, client); };
                workerSms = new Thread(threadStart);
                workerSms.Start();
                // client.Send(msg);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SendLodgementReimbursementMail(shop shop, mail mail, master_transaction tran)
        {
            try
            {
                admin_user user = new CRUD.admin_users_crud().find_admin_user_By_ID(shop.agent.Value);
                string body = mail.content;
                if (user != null && user.email != "" && user.email != string.Empty && user.email != null)
                {
                    string accountName = ((user.firstname != null) ? user.firstname.ToUpperInvariant() : "") + " " + ((user.middlename != null) ? user.middlename.ToUpperInvariant() : "") + " " + ((user.lastname != null) ? user.lastname.ToUpperInvariant() : "");
                    string balance = "";
                    if (shop.master_balance_sheets != null && shop.master_balance_sheets.Count > 0)
                        balance = tran.amount.ToString();

                    body = body.Replace("{shop_code}", shop.shop_code);
                    body = body.Replace("{account_name}", (accountName != null) ? accountName : "UNAVAILABLE");
                    body = body.Replace("{amount}", balance);
                    body = body.Replace("{curr_debt_bal}", tran.balance_after.ToString());
                    body = body.Replace("{bank_acct}", getbankaccounts());
                    
                    SendBulkMail(user.email, mail.name, body);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;

            }
        }

        private string getbankaccounts()
        {
            List<bank> banks = new CRUD.bank_crud().getAllBanks();
            banks.Remove(banks.SingleOrDefault(a => a.code == "CASH"));
            string ret = "<table style=\"border-radius:3px; border: 2px\" ><tr><td>BANK</td> <td>ACCOUNT NAME</td><td> ACCOUNT NUMBER </td></tr>";
            foreach (bank bank in banks)
            {
                ret += "<tr> <td>" + bank.name + "</td><td> Winners Golden Bet </td><td>" + bank.account_no + "</td></tr>";
            }
            ret += "</Table>";
            return ret;
        }

        public bool SendBulkMail(string toadds, string subject, string message)
        {
            
            MailMessage msg = new MailMessage();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            try
            {
                
                msg.Subject = subject;
                msg.To.Add(toadds);
               // msg.Body = message;
                //MailMessage mailMessage = new MailMessage();
                msg.IsBodyHtml = true;
                client.EnableSsl = true;
                Thread workerSms;
                
                string html = message;
                //msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
                mail = msg;
                ThreadStart threadStart = delegate() { SendCreatedMail(msg, client); };
                workerSms = new Thread(threadStart);
                workerSms.Start();
               // client.Send(msg);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SendCreatedMail(MailMessage msg, System.Net.Mail.SmtpClient client)
        {
            try
            {
                
                client.Send(msg);
            }
            catch (Exception ex)
            {
                errmsg = ex.Message;

            }
        }


        public void SendMail(string to, string subject , string body)
        {

            String APIKey = "e648d341830a463ed54ae39331d39da2";
            String SecretKey = "619f1bee9f58a23485714b1966fd7116";
            String From = "balance@winnersgoldenbet.ng";
            String To = to;

            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(From);

            msg.To.Add(new MailAddress(To));

            msg.Subject = subject;
            msg.Body = body;

            SmtpClient client = new SmtpClient("in.mailjet.com", 465);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(APIKey, SecretKey);

            client.Send(msg);
        }

        public void sendma(string to, string subject , string body)
        { 
            try{
                     MailMessage mailMsg = new MailMessage();

                // To
                mailMsg.To.Add(new MailAddress(to));

                // From
                mailMsg.From = new MailAddress("balance@winnersgoldenbet.ng");

                // Subject and multipart/alternative Body
                mailMsg.Subject = subject;
                string text = subject;
                string html = subject;
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                // Init SmtpClient and send
                SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("balance@winnersgoldenbet.ng", "wgbbalance2014");
                smtpClient.Credentials = credentials;

                smtpClient.Send(mailMsg);
      }
        catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
        }



        public bool SendCommissionLodgementReimbursementMail(shop shop, WgbSalesUpload.mail mail, commission_transaction newmastertransaction)
        {
            try
            {
                admin_user user = new CRUD.admin_users_crud().find_admin_user_By_ID(shop.agent.Value);
                string body = mail.content;
                if (user != null && user.email != "" && user.email != string.Empty && user.email != null)
                {
                    string accountName = ((user.firstname != null) ? user.firstname.ToUpperInvariant() : "") + " " + ((user.middlename != null) ? user.middlename.ToUpperInvariant() : "") + " " + ((user.lastname != null) ? user.lastname.ToUpperInvariant() : "");
                    string balance = "";
                    if (newmastertransaction != null && shop.master_balance_sheets.Count > 0)
                        balance = newmastertransaction.amount.ToString();

                    body = body.Replace("{shop_code}", shop.shop_code);
                    body = body.Replace("{account_name}", (accountName != null) ? accountName : "UNAVAILABLE");
                    body = body.Replace("{amount}", balance);
                    body = body.Replace("{curr_debt_bal}", newmastertransaction.balance_after.ToString());
                    body = body.Replace("{bank_acct}", getbankaccounts());

                    SendBulkMail(user.email, mail.name, body);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;

            }
        }
    }
}