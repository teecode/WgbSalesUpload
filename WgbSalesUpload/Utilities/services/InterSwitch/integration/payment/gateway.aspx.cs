using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using WgbSalesUpload.Utilities.InterSwitch;

namespace WgbSalesUpload.Utilities.services.InterSwitch.integration.payment
{
    public class gateway : IHttpHandler
    {

        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public void ProcessRequest(HttpContext context)
        {
            if (isIPValid(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]))
            {
                StreamReader stream = new StreamReader(context.Request.InputStream);
                string x = stream.ReadToEnd();
                try
                {
                    loadStream(x);
                }
                catch (Exception ex)
                {
                    generateError(x);
                }

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private bool isIPValid(string ip)
        {

            string ConfigurationKeyAllowedSingleIPs = ConfigurationManager.AppSettings["AllowedAdminSingleIPs"];
            // string ConfigurationKeyAllowedMaskedIPs = ConfigurationManager.AppSettings["AllowedAdminMaskedIPs"];
            //string ConfigurationKeyDeniedSingleIPs = ConfigurationManager.AppSettings["DeniedAdminSingleIPs"];
            // string ConfigurationKeyDeniedMaskedIPs = ConfigurationManager.AppSettings["DeniedAdminMaskedIPs"];

            return (ConfigurationKeyAllowedSingleIPs.Split(',').Contains(ip));

        }

        private void generateError(string x)
        {
            XDocument doc = null;
            XDocument xmlInput = XDocument.Parse(x);
            IEnumerable<XElement> requests = xmlInput.Elements();
            //initialize to objects and process then return request;
            string root = xmlInput.Root.Name.ToString();

            if (root.Contains("Customer"))
            {
                doc = new XDocument(

                     new XDeclaration("1.0", "utf-8", null),
                      new XElement("CustomerInformationResponse",
                        new XElement("MerchantReference", requests.ElementAt(0).Element("MerchantReference").Value),
                         new XElement("Customers",
                          from sc in requests
                          select new XElement("Customer",
                            new XElement("Status", 1),
                     new XElement("CustReference", sc.Element("CustReference").Value)
                    )
                        )));
                sendResponse(doc);
            }
            else if (root.Contains("PaymentNotificationRequest"))
            {
                var paymentrequests = xmlInput.Descendants("Payment").ToList();
                doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", null),
                          new XElement("PaymentNotificationResponse",
                          new XElement("Payments",
                           from payment in paymentrequests
                           select new XElement("Payment",
                               new XElement("PaymentLogId", payment.Element("PaymentLogId").Value),
                               new XElement("Status", 1)
                     ))));
                sendResponse(doc);
            }


        }

        public void loadStream(string x)
        {
            //if (Request.ContentType == "text/xml" || Request.ContentType == "text/plain" || Request.ContentType == "text/html"|| Request.ContentType == "application/xml")
            //{

            XDocument doc = null; ;
            XDocument xmlInput = XDocument.Parse(x);
            IEnumerable<XElement> requests = xmlInput.Elements();
            //initialize to objects and process then return request;
            string root = xmlInput.Root.Name.ToString();

            if (root.Contains("Customer"))
            {
                doc = generateCustomerValidationRequests(doc, requests);
            }
            else if (root.Contains("PaymentNotificationRequest"))
            {
                payment_request req = new payment_request { request = x, date = DateTime.Now };
                BettingSuiteDataContext db = new BettingSuiteDataContext();
                db.payment_requests.InsertOnSubmit(req);
                db.SubmitChanges();

                var paymentrequests = xmlInput.Descendants("Payment").ToList();
                doc = generatePaymentNotificationRequests(doc, paymentrequests);
            }

            sendResponse(doc);
            // }

            //}

        }



        public application Application()
        {
            application app = new application
            {
                app_id = Guid.NewGuid().ToString()
            };
            db.applications.InsertOnSubmit(app);
            return app;
        }



        private void SendSmsAndMail(shop shop, transaction_category tran, debt debt, debt_transaction dtran, debt_lodgement_reimbursment newmlr)
        {
            try
            {
                bool mailvalidity = new Utilities.Mail_Validity_Helper().getMailSendingStatus();
                SMS_Vendor_Validity_Helper valhelp = new SMS_Vendor_Validity_Helper();
                sms_vendor vendor = valhelp.getVendorsByName("50kobo");

                bool smsvalidity = (DateTime.Today <= vendor.expiry_date) ? true : false;

                if (shop.agent != null)
                {

                    sms message;
                    if (tran.code == "DL")
                    {
                        if (smsvalidity)
                        {
                            sm mm = new CRUD.sms_crud().findByCode("DL");
                            message = new sms();

                            if (message.initializeDebtParameters(shop, debt, dtran, newmlr, mm.subject.Trim() + "\r\n" + mm.content.Trim() + "\r\n" + mm.conclusion.Trim()))
                                message.CustomizeAndSendSMS();
                        }
                        if (mailvalidity)
                        {

                            mail mail = new CRUD.mail_crud().findByCode("DL");
                            MailClient client = new MailClient();
                            client.SendDebtLodgement(shop, mail, debt, dtran);
                        }
                    }

                }
            }
            catch (Exception)
            {

            }
        }

        private decimal lodgeDebt(List<debt> shopdebt, decimal amount, master_balance_sheet shopbal)
        {

            foreach (debt debt in shopdebt)
            {
                transaction_category debttran = db.transaction_categories.SingleOrDefault(a => a.code == "DL");
                bank bank = new CRUD.bank_crud().find_bank_By_Code("INT");
                payment_type ptype = new CRUD.payment_type_crud().find_payment_type_By_ID(1);

                login lodin = db.logins.Single(a => a.username == "InterSwitch");
                if (amount > 0)
                {
                    //debt less than or equal to bonus
                    if (debt.current_amount <= amount && debt.status == true && debt.current_amount > 0)
                    {
                        decimal temp = debt.current_amount.Value;

                        debt.current_amount = 0;

                        debt.status = false;
                        debt.installment_count += 1;





                        debt_transaction dtran = new debt_transaction
                        {
                            amount = temp,
                            app_id = Application().app_id,
                            balance_after = getbal(temp, temp, debttran),
                            balance_before = temp,
                            description = debttran.description + " " + "for " + DateTime.Today,
                            details = debttran.description + " " + "for " + DateTime.Today,
                            posted_by = lodin.id,
                            shop = debt.shop,
                            trans_category = debttran.id,
                            trans_timestamp = DateTime.Now,
                            trans_type = (debttran.type == true) ? 0 : 1,
                            debt = debt.id,

                        };
                        db.debt_transactions.InsertOnSubmit(dtran);
                        db.SubmitChanges();

                        debt_transaction debtIdtran = new CRUD.debt_transaction_crud().find_debt_transaction_By_AppId(dtran.app_id);

                        debt_lodgement_reimbursment newmlr = new debt_lodgement_reimbursment
                        {
                            app_id = Application().app_id,
                            amount = temp,
                            bank = bank.id,
                            date = DateTime.Today,
                            shop = debt.shop,
                            teller_number = "",
                            islocked = true,
                            isverified = true,
                            payment_type = ptype.id,
                            transaction = (debtIdtran != null) ? debtIdtran.id : 142,
                            transaction_category = debttran.id,
                        };

                        db.debt_lodgement_reimbursments.InsertOnSubmit(newmlr);
                        amount -= temp;


                        //debtTransactionToSave.Add(dtran);

                        shopbal.credit_balance = getbal(shopbal.credit_balance, temp, debttran);

                        shopbal.netbalance = getbal(shopbal.netbalance, temp, debttran);

                        //SAVE DEBT .... BALANCES

                        debt DebtToUpdate = db.debts.SingleOrDefault(a => a.id == debt.id);
                        DebtToUpdate.current_amount = debt.current_amount;
                        DebtToUpdate.status = debt.status;
                        DebtToUpdate.installment_count = debt.installment_count;

                        master_balance_sheet salesbalancesToUpdate = db.master_balance_sheets.SingleOrDefault(a => a.shop == shopbal.shop);
                        salesbalancesToUpdate.credit_balance = shopbal.credit_balance;
                        salesbalancesToUpdate.netbalance = shopbal.netbalance;
                        db.SubmitChanges();

                        shop shop = new CRUD.shop_crud().find_shop_By_ID(debt.shop);
                        SendSmsAndMail(shop, debttran, debt, dtran, newmlr);


                    }
                    else if (debt.current_amount > amount && debt.status == true && debt.current_amount > 0)
                    {

                        decimal temp = debt.current_amount.Value;
                        debt.current_amount = getbal(debt.current_amount, amount, debttran);
                        debt.installment_count += 1;



                        debt_transaction dtran = new debt_transaction
                        {
                            amount = amount,
                            app_id = Application().app_id,
                            balance_after = getbal(temp, amount, debttran),
                            balance_before = temp,
                            description = debttran.description + " " + "for " + DateTime.Today,
                            details = debttran.description + " " + "for " + DateTime.Today,
                            posted_by = lodin.id,
                            shop = debt.shop,
                            trans_category = debttran.id,
                            trans_timestamp = DateTime.Now,
                            trans_type = (debttran.type == true) ? 0 : 1,
                            debt = debt.id,

                        };


                        db.debt_transactions.InsertOnSubmit(dtran);
                        db.SubmitChanges();
                        //debtTransactionToSave.Add(dtran);

                        debt_transaction debtIdtran = new CRUD.debt_transaction_crud().find_debt_transaction_By_AppId(dtran.app_id);

                        debt_lodgement_reimbursment newmlr = new debt_lodgement_reimbursment
                        {
                            app_id = Application().app_id,
                            amount = temp,
                            bank = bank.id,
                            date = DateTime.Today,
                            shop = debt.shop,
                            teller_number = "",
                            islocked = true,
                            isverified = true,
                            payment_type = ptype.id,
                            transaction = (debtIdtran != null) ? debtIdtran.id : 142,
                            transaction_category = debttran.id,


                        };
                        db.debt_lodgement_reimbursments.InsertOnSubmit(newmlr);


                        shopbal.credit_balance = getbal(shopbal.credit_balance, amount, debttran);

                        shopbal.netbalance = getbal(shopbal.netbalance, amount, debttran);

                        //SAVE DEBT .... BALANCES

                        debt DebtToUpdate = db.debts.SingleOrDefault(a => a.id == debt.id);
                        DebtToUpdate.current_amount = debt.current_amount;
                        DebtToUpdate.status = debt.status;
                        DebtToUpdate.installment_count = debt.installment_count;

                        master_balance_sheet salesbalancesToUpdate = db.master_balance_sheets.SingleOrDefault(a => a.shop == shopbal.shop);
                        salesbalancesToUpdate.credit_balance = shopbal.credit_balance;
                        salesbalancesToUpdate.netbalance = shopbal.netbalance;
                        amount -= amount;
                        db.SubmitChanges();
                        shop shop = new CRUD.shop_crud().find_shop_By_ID(debt.shop);
                        SendSmsAndMail(shop, debttran, debt, dtran, newmlr);
                        break;

                    }
                }

            }

            return amount;
        }

        private XDocument generateCustomerValidationRequests(XDocument doc, IEnumerable<XElement> requests)
        {

            List<CustomerValidationRequest> cvrequests = new List<CustomerValidationRequest>(requests.Count());

            foreach (var request in requests)
            {

                CustomerValidationRequest cvr = new CustomerValidationRequest();
                cvr.setMerchantRefrence(int.Parse(request.Element("MerchantReference").Value));
                cvr.setCustReference(request.Element("CustReference").Value);
                cvr.setFtpUsername(request.Element("FtpUsername").Value);
                cvr.setFtpPassword(request.Element("FtpPassword").Value);
                cvr.setServiceUsername(request.Element("ServiceUsername").Value);
                cvr.setServicePassword(request.Element("ServicePassword").Value);
                //cvr.setPaymentItemCode(request.Element("PaymentItemCode").Value);
                cvrequests.Add(cvr);
            }
            doc = ProcessCustomerValidationRequestsAndGetResponse(cvrequests);
            return doc;
        }

        private XDocument generatePaymentNotificationRequests(XDocument doc, IEnumerable<XElement> requests)
        {
            List<PaymentNotificationRequestHelper> pnrequests = new List<PaymentNotificationRequestHelper>(requests.Count());


            foreach (var request in requests)
            {
                string a = request.Name.ToString();
                PaymentNotificationRequestHelper pnr = new PaymentNotificationRequestHelper();
                string b = request.Element("PaymentLogId").ToString();
                pnr.setPaymentLogId(int.Parse(request.Element("PaymentLogId").Value));
                pnr.setCustReference(request.Element("CustReference").Value);
                pnr.setAlternateCustReference(request.Element("AlternateCustReference").Value);
                pnr.setAmount(decimal.Parse(request.Element("Amount").Value));
                pnr.setPaymentStatus(int.Parse(request.Element("PaymentStatus").Value));
                pnr.setPaymentMethod(request.Element("PaymentMethod").Value);
                pnr.setPaymentReference(request.Element("PaymentReference").Value);
                pnr.setTerminalId((request.Element("TerminalId").Value));
                pnr.setChannelName((request.Element("ChannelName").Value));
                pnr.setLocation((request.Element("Location").Value));
                pnr.setReversal(Boolean.Parse(request.Element("IsReversal").Value));
                pnr.setPaymentDate(DateTime.ParseExact(request.Element("PaymentDate").Value, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture));
                pnr.setSettlementDate(DateTime.ParseExact(request.Element("SettlementDate").Value, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture));
                pnr.setInstitutionID((request.Element("InstitutionId").Value));
                pnr.setInstitutionName((request.Element("InstitutionName").Value));
                pnr.setBranchName((request.Element("BranchName").Value));
                pnr.setBankName((request.Element("BankName").Value));
                pnr.setFeeName((request.Element("FeeName").Value));
                pnr.setCustomerName((request.Element("CustomerName").Value));
                pnr.setOtherCustomerInfo((request.Element("OtherCustomerInfo").Value));
                pnr.setReceiptNo((request.Element("ReceiptNo").Value));
                pnr.setCollectionsAccount((request.Element("CollectionsAccount").Value));
                pnr.setThirdPartyCode((request.Element("ThirdPartyCode").Value));
                pnr.setPaymentItems(request.Element("PaymentItems").Elements());
                pnr.setBankCode((request.Element("BankCode").Value));
                pnr.setCustomerAddress((request.Element("CustomerAddress").Value));
                pnrequests.Add(pnr);

            }
            doc = ProcessNotificationRequestsAndGetResponse(pnrequests);
            return doc;
        }

        private XDocument ProcessNotificationRequestsAndGetResponse(List<PaymentNotificationRequestHelper> pnrequests)
        {
            BettingSuiteDataContext db = new BettingSuiteDataContext();
            List<PaymentNotificationResponse> pnresponses = new List<PaymentNotificationResponse>(pnrequests.Count());
            // List<PaymentNotificationResponse> invalidpnresponses = new List<PaymentNotificationResponse>(pnrequests.Count());
            foreach (PaymentNotificationRequestHelper pnr in pnrequests)
            {
                int refr = int.Parse(pnr.getCustReference().Substring(1));
                customer customer = null;
                //check Type of customer 
                if (pnr.getCustReference().StartsWith(ConfigurationManager.AppSettings["AGENT PREFIX"]))
                {

                    shop shop = db.shops.SingleOrDefault(a => a.id == refr);
                    if (shop != null)
                    {
                        customer = new customer(shop);
                        if (customer.getReference() != refr)
                            customer = null;
                    }
                    else customer = null;


                    if (customer != null)
                    {
                        // customer.setPaymentItem(cvr.getPaymentItemCode());
                        PaymentNotificationResponse pr = new PaymentNotificationResponse(customer, pnr);
                        pr.setStatus(0);
                        savePaymentDetails(pnr);
                        pr.setPaymentLogId(pnr.getPaymentLogId().ToString());
                        pnresponses.Add(pr);
                    }
                    else
                    {

                        customer = new customer(null, null, null, null, null, null, 1);
                        customer.setReference(refr);
                        PaymentNotificationResponse ipr = new PaymentNotificationResponse(customer, pnr);
                        ipr.setStatus(1);
                        pnresponses.Add(ipr);
                    }
                }
                else if (pnr.getCustReference().StartsWith(ConfigurationManager.AppSettings["ONLINE USER PREFIX"]))
                {
                    // SORT OUT ONLINE LATER


                }

                else
                {
                    customer = new customer(null, null, null, null, null, null, 1);
                    customer.setReference(refr);
                    PaymentNotificationResponse ipr = new PaymentNotificationResponse(customer, pnr);
                    ipr.setStatus(1);
                    pnresponses.Add(ipr);
                }

            }
            return generatePaymentNotificationResponse(pnresponses);
        }

        private bool savePaymentDetails(PaymentNotificationRequestHelper pnr)
        {
            BettingSuiteDataContext db = new BettingSuiteDataContext();
            if (iSNotDuplicate(pnr.getPaymentLogId(), db))
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        PaymentNotificationRequest pr = setPaymentRequestObjAndSave(pnr, db);
                        shop shop = new CRUD.shop_crud().find_shop_By_ID(pr.CustReference);
                        foreach (PaymentItemHelper it in pnr.getPaymentItems())
                        {
                            if (it.getItemCode().Equals(ConfigurationManager.AppSettings["SalesCode"].ToString()))
                            {
                                AddSalesTransaction(shop, pr, db);
                            }
                            else if (it.getItemCode().Equals(ConfigurationManager.AppSettings["LeaseDebtCode"].ToString()))
                            {
                                AddDebtTransaction(shop, pr, db);
                            }
                            else if (it.getItemCode().Equals(ConfigurationManager.AppSettings["OperationalDebtCode"].ToString()))
                            {
                                AddDebtTransaction(shop, pr, db);
                            }
                            else if (it.getItemCode().Equals(ConfigurationManager.AppSettings["Online Agency"].ToString()))
                            {
                                //sendmail
                                MailClient mc = new MailClient();
                                mail mail = new CRUD.mail_crud().findByCode("OL");
                                string body = mail.content;
                                body = body.Replace("{shop_code}", shop.shop_code);
                                body = body.Replace("{account_name}", pnr.getCustomerName());
                                body = body.Replace("{amount}", "₦" + pr.Amount.ToString());
                                body = body.Replace("{amount_to_lodge}", "₦" + (decimal.Multiply(pr.Amount, decimal.Parse("1.10"))).ToString());
                                body = body.Replace("{payment_method}", "InterSwitch");

                                mc.SendBulkMail(ConfigurationManager.AppSettings["ONLINE SENDING MAIL"].ToString(), shop.shop_code + " (₦" + pr.Amount.ToString() + ") ", body);
                                //send sms to agent
                                SMS_Vendor_Validity_Helper valhelp = new SMS_Vendor_Validity_Helper();
                                sms_vendor vendor = valhelp.getVendorsByName("50kobo");

                                bool smsvalidity = (DateTime.Today <= vendor.expiry_date) ? true : false;

                                if (smsvalidity)
                                {

                                    sms message = new sms();
                                    if (message.initializeParameters(shop, "Online Lodgement Confirmation" + "\r\n" + "Your lodgement of " + pr.Amount.ToString() + " for online agency has being confirmed. You would be credited with " + (decimal.Multiply(pr.Amount, decimal.Parse("1.10"))).ToString() + "\r\n" + "", pr.PaymentDate))
                                        message.CustomizeAndSendSMS();
                                }
                                //send online dept updates on online user

                            }
                            else if (it.getItemCode().Equals(ConfigurationManager.AppSettings["Online User"].ToString()))
                            {


                            }
                            else if (it.getItemCode().Equals(ConfigurationManager.AppSettings["Payment Commision"].ToString()))
                            {
                                //futute implementation
                            }
                            else
                            {
                                SMS_Vendor_Validity_Helper valhelp = new SMS_Vendor_Validity_Helper();
                                sms_vendor vendor = valhelp.getVendorsByName("50kobo");

                                bool smsvalidity = (DateTime.Today <= vendor.expiry_date) ? true : false;

                                if (smsvalidity)
                                {
                                    sms message = new sms();
                                    if (message.initializeParameters(shop, "Payment Confirmation" + "\r\n" + "Your lodgement of " + pr.Amount.ToString() + " for " + it.getItemName() + " has being confirmed and would be processed shortly" + "\r\n" + "call 014609630 for info/complaints", pr.PaymentDate))
                                        message.CustomizeAndSendSMS();
                                }
                            }

                        }

                        db.SubmitChanges();
                        scope.Complete();

                        return true;
                    }

                }
                catch (Exception)
                {
                    return false;
                }

            }
            else
            {

            }
            return false;

        }

        private void AddDebtTransaction(shop shop, PaymentNotificationRequest pr, BettingSuiteDataContext db)
        {
            try
            {


                List<debt> debts = new CRUD.debt_crud().getShopdebts(shop, null);

                if (debts != null && debts.Count > 0)
                {


                    decimal amount = lodgeDebt(debts, pr.Amount, shop.master_balance_sheets[0]);
                    if (amount > 0)
                    {
                        transaction_category saleslodge = db.transaction_categories.SingleOrDefault(a => a.code == "L");
                        //amount = LodgeSales(shop, amount, shop.master_balance_sheets[0]);
                        AddnewLodgementReimbursementTransaction(shop, saleslodge, pr, db, amount);
                    }


                }
            }
            catch (Exception)
            {

            }



        }



        private PaymentNotificationRequest setPaymentRequestObjAndSave(PaymentNotificationRequestHelper pnr, BettingSuiteDataContext db)
        {
            try
            {
                PaymentNotificationRequest newpr = new PaymentNotificationRequest();
                newpr.Amount = Math.Abs(pnr.getAmount());
                newpr.BankCode = pnr.getBankCode();
                newpr.BankName = pnr.getBankName();
                newpr.BranchName = pnr.getBranchName();
                newpr.CategoryCode = pnr.getCategoryCode();
                newpr.CategoryName = pnr.getCategoyName();
                newpr.ChannelName = pnr.getChannelName();
                newpr.CollectionsAccount = pnr.getCollectionsAccount();
                newpr.CustomerAddress = pnr.getCustomerAddress();
                newpr.CustomerName = pnr.getCustomerName();
                newpr.CustomerPhoneNumber = pnr.getCustomerPhoneNumber();
                newpr.CustReference = int.Parse(pnr.getCustReference().Substring(1));
                newpr.DepositorName = pnr.getDepositorName();
                newpr.FeeName = pnr.getFeeName();
                newpr.FTPPassword = pnr.getFTPPassword();
                newpr.FTPUsername = pnr.getFTPUsername();
                newpr.InstitutionId = pnr.getInstitutionID();
                newpr.InstitutionName = pnr.getInstitutionName();
                newpr.isReversal = pnr.isReversal();
                newpr.Location = pnr.getLocation();
                newpr.OriginalPaymentLogid = pnr.getOriginalPaymentLogId();
                newpr.OriginalPaymentName = pnr.getOriginalPaymentName();
                newpr.OriginalPaymentReference = pnr.getOriginalPaymentReference();
                newpr.OtherCustomerInfo = pnr.getOtherCustomerInfo();
                newpr.PaymentCurrency = pnr.getPaymentCurrency();
                newpr.PaymentDate = pnr.getPaymentDate();
                newpr.PaymentLogId = pnr.getPaymentLogId();
                newpr.PaymentMethod = pnr.getPaymentMethod();
                newpr.PaymentReference = pnr.getPaymentReference();
                newpr.PaymentStatus = pnr.getPaymentStatus();
                newpr.ServicePassword = pnr.getServicePassword();
                newpr.ServiceUsername = pnr.getServiceUsername();
                newpr.SettlementDate = pnr.getSettlementDate();
                List<PaymentItem> pItems = null;
                List<PaymentItemHelper> phs = pnr.getPaymentItems();
                // List<Item> t = db.Items.ToList<Item>();
                if (phs.Count > 0)
                {
                    pItems = new List<PaymentItem>();
                    foreach (PaymentItemHelper pay in phs)
                    {
                        PaymentItem p = new PaymentItem();
                        p.ItemAmount = pay.getItemAmount();
                        p.LeadBankCbnCode = pay.getLeadBankCbnCode();
                        p.LeadBankCode = pay.getLeadBankCode();
                        p.LeadBankName = pay.getLeadBankName();
                        p.Item = db.Items.SingleOrDefault(a => a.code.Equals(pay.getItemCode())).id;
                        pItems.Add(p);
                        // setItemCodeLater 
                    }
                    db.PaymentItems.InsertAllOnSubmit(pItems);
                    newpr.PaymentItem1 = pItems[0];
                    db.PaymentNotificationRequests.InsertOnSubmit(newpr);

                }
                return newpr;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private bool iSNotDuplicate(int paymentID, BettingSuiteDataContext db)
        {
            PaymentNotificationRequest pr = db.PaymentNotificationRequests.SingleOrDefault(a => a.PaymentLogId == paymentID);
            if (pr != null)
                return false;
            return true;
        }

        private XDocument generatePaymentNotificationResponse(List<PaymentNotificationResponse> pnresponses)
        {
            try
            {

                XDocument document1 = null;
                if (pnresponses.Count() > 0)
                {
                    document1 = new XDocument(
                           from payment in pnresponses
                           select new XElement("Payment",
                               new XElement("PaymentLogId", payment.getPaymentLogId()),
                               new XElement("Status", payment.getStatus())
                               // new XElement("StatusMessage", payments.customer.getStatusMessage())
                               // new XElement("Amount", Customer.amount.ToString())
                               // new XElement("PaymentItems",
                               //from Citem in Customer.customer.getPaymentItems().Where(a => a != null )
                               // select new XElement("Item",
                               //        new XElement("ProductName",Citem.getProductName()),
                               //        new XElement("ProductCode",Citem.getProductCode()),
                               //        new XElement("Quantity",Citem.getQuantity()),
                               //        new XElement("Price",Citem.getPrice()),
                               //        new XElement("Subtotal",Citem.getSubtotal()),
                               //        new XElement("Tax",Citem.getTax()),
                               //        new XElement("Total",Citem.getTotal())
                               //     ))
                                 ));
                }
                else document1 = new XDocument();


                XDocument document = new XDocument(

                    new XDeclaration("1.0", "utf-8", null),

                         new XElement("PaymentNotificationResponse",
                              new XElement("Payments",
                              from Customer in document1.Elements()
                              select Customer

                              )));
                return document;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void sendResponse(XDocument doc)
        {
            try
            {
                HttpContext.Current.Response.Clear();
                // HttpContext.Current.Response.Write(doc.w
                HttpContext.Current.Response.ContentType = "text/xml";
                doc.Save(HttpContext.Current.Response.Output);
                //HttpContext.Current.Response.Write(doc);
                //HttpContext.Current.Response.End();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                throw new HttpException(404, "Page not Found");
            }
        }

        private XDocument ProcessCustomerValidationRequestsAndGetResponse(List<CustomerValidationRequest> cvrequests)
        {
            BettingSuiteDataContext db = new BettingSuiteDataContext();
            List<CustomerValidationResponse> cvresponses = new List<CustomerValidationResponse>(cvrequests.Count());
            List<CustomerValidationResponse> invalidcvresponses = new List<CustomerValidationResponse>(cvrequests.Count());
            foreach (CustomerValidationRequest cvr in cvrequests)
            {

                int refr = int.Parse(cvr.getCustReference().Substring(1));
                customer customer = null;

                //check Type of customer 
                if (cvr.getCustReference().StartsWith(ConfigurationManager.AppSettings["AGENT PREFIX"]))
                {

                    shop shop = db.shops.SingleOrDefault(a => a.id == refr);
                    if (shop != null)
                    {
                        customer = new customer(shop);
                        customer.setPrefix(cvr.getCustReference().ElementAt(0).ToString());
                        if (customer.getReference() != refr)
                            customer = null;
                    }
                    else customer = null;


                    if (customer != null)
                    {
                        // customer.setPaymentItem(cvr.getPaymentItemCode());
                        CustomerValidationResponse cr = new CustomerValidationResponse(customer, cvr.getMerchantReference());
                        cvresponses.Add(cr);
                    }
                    else
                    {

                        customer = new customer(null, null, null, null, null, null, 1);
                        customer.setPrefix(cvr.getCustReference().ElementAt(0).ToString());
                        customer.setReference(refr);
                        CustomerValidationResponse icr = new CustomerValidationResponse(customer, cvr.getMerchantReference());
                        invalidcvresponses.Add(icr);
                    }
                }
                else
                {
                    // Implement Online Users Later
                    customer = new customer(null, null, null, null, null, null, 1);
                    // customer.setPrefix(cvr.getCustReference().ElementAt(0).ToString());
                    customer.setReference(refr);
                    CustomerValidationResponse icr = new CustomerValidationResponse(customer, cvr.getMerchantReference());
                    invalidcvresponses.Add(icr);
                }

            }
            return generateCustomerValidationResponse(cvresponses, invalidcvresponses);
        }

        private XDocument generateCustomerValidationResponse(List<CustomerValidationResponse> cvresponses, List<CustomerValidationResponse> invcvresponses)
        {
            try
            {
                string merchantreference = "";
                XDocument document1 = null, document2 = null;
                if (cvresponses.Count() > 0)
                {
                    merchantreference = cvresponses[0].merchantReference.ToString();
                    document1 = new XDocument(
                    from Customer in cvresponses
                    select new XElement("Customer",
                       new XElement("Status", Customer.customer.getStatus()),
                        new XElement("CustReference", Customer.customer.getPrefix() + Customer.customer.getReference()),
                        new XElement("FirstName", Customer.customer.getFirstname()),
                        new XElement("LastName", Customer.customer.getLastname()),
                        new XElement("OtherName", Customer.customer.getMiddlename()),
                        new XElement("Email", Customer.customer.getEmail()),
                        new XElement("ThirdPartyCode", ""),
                        new XElement("Amount", "0.00"),
                        new XElement("StatusMessage", Customer.customer.getStatusMessage())
                        // new XElement("Amount", Customer.amount.ToString())
                        // new XElement("PaymentItems",
                        //from Citem in Customer.customer.getPaymentItems().Where(a => a != null )
                        // select new XElement("Item",
                        //        new XElement("ProductName",Citem.getProductName()),
                        //        new XElement("ProductCode",Citem.getProductCode()),
                        //        new XElement("Quantity",Citem.getQuantity()),
                        //        new XElement("Price",Citem.getPrice()),
                        //        new XElement("Subtotal",Citem.getSubtotal()),
                        //        new XElement("Tax",Citem.getTax()),
                        //        new XElement("Total",Citem.getTotal())
                        //     ))
                          ));
                }
                else document1 = new XDocument();

                if (invcvresponses.Count() > 0)
                {
                    merchantreference = invcvresponses[0].merchantReference.ToString();
                    document2 = new XDocument(
                    from Customer2 in invcvresponses
                    select new XElement("Customer",
                        new XElement("Status", Customer2.customer.getStatus()),
                         new XElement("CustReference", Customer2.customer.getPrefix() + Customer2.customer.getReference())
                        )
                       );
                }
                else
                    document2 = new XDocument();

                XDocument document = new XDocument(

                    new XDeclaration("1.0", "utf-8", null),

                         new XElement("CustomerInformationResponse",
                              new XElement("MerchantReference", merchantreference),
                               new XElement("Customers",
                              from Customer in document1.Elements()
                              select Customer,
                              from el2 in document2.Elements()
                              select el2
                              )));
                return document;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private void AddSalesTransaction(shop shop, PaymentNotificationRequest pnr, BettingSuiteDataContext db)
        {

            int tt = db.transaction_categories.Single(a => a.code.Equals("L")).id;
            if (pnr.isReversal)
            {
                tt = db.transaction_categories.Single(a => a.code.Equals("LR")).id;

            }
            transaction_category selectedtran = db.transaction_categories.SingleOrDefault(a => a.id == tt);
            //string[] MDS = {"S", "W", "C", "SR", "WR","CR"};

            string[] MLR = { "L", "R", "LR", "RR" };
            string[] LPT = { "LP", "LPR" };

            if (MLR.Contains(selectedtran.code))
            {
                decimal amount = new decimal(null);
                AddnewLodgementReimbursementTransaction(shop, selectedtran, pnr, db, amount);
            }

            else
            {

            }
        }

        private decimal? getbal(decimal? bal1, decimal? bal2, transaction_category tran)
        {
            if (tran.type == false)
            {
                return bal1 - bal2;
            }
            else
                return bal1 + bal2;
        }

        private void AddnewLodgementReimbursementTransaction(shop shop, transaction_category tran, PaymentNotificationRequest pnr, BettingSuiteDataContext db, decimal amount)
        {

            amount = (amount == null) ? pnr.Amount : amount;

            DateTime date = DateTime.Today;
            master_balance_sheet masterbalance = new CRUD.master_balance_sheet_crud().getMasterBalanceByShopId(shop.id);

            if (masterbalance == null)
            {
                masterbalance = new master_balance_sheet
                {
                    shop = shop.id,
                    credit_balance = 0,
                    online_credit = 0,
                    sales_balance = 0,
                    netbalance = 0,

                };
                masterbalance = new CRUD.master_balance_sheet_crud(masterbalance).insert_master_balance_sheet();
            }

            master_transaction reversalmastertransaction = new master_transaction();


            master_lodgement_reimbursment newmlr = new master_lodgement_reimbursment
            {
                amount = amount,
                bank = 0,//interswitch id,
                date = date,
                shop = shop.id,
                teller_number = pnr.PaymentLogId.ToString(),
                islocked = true,
                isverified = true,
                payment_type = 5,
                transaction_category = tran.id,
            };

            login lodin = db.logins.Single(a => a.username == "InterSwitch");
            master_transaction newmastertransaction = new master_transaction
            {
                amount = Math.Abs(amount),
                balance_before = (masterbalance != null) ? masterbalance.sales_balance : 0,
                balance_after = (masterbalance != null) ? getbal(masterbalance.sales_balance, amount, tran) : amount,
                shop = shop.id,
                trans_category = tran.id,
                trans_timestamp = DateTime.Now,
                trans_type = (tran.type == true) ? 1 : 0,
                posted_by = lodin.id,// interswitch id,
                description = pnr.PaymentReference + " Posted For " + date.ToShortDateString(),
                details = (tran.code == "L") ? "Lodgement For " + date.ToShortDateString() : "Lodgement Reversal For " + date.ToShortDateString()
            };

            masterbalance.sales_balance = (masterbalance != null) ? getbal(masterbalance.sales_balance, newmastertransaction.amount, tran) : newmastertransaction.amount;
            masterbalance.netbalance = (masterbalance != null) ? getbal(masterbalance.netbalance, newmastertransaction.amount, tran) : newmastertransaction.amount;

            bool loadtran = new Utilities.TransactionHelper().SaveMasterLodgementTransaction(newmlr, newmastertransaction, masterbalance);

            if (loadtran)
            {

                try
                {
                    bool mailvalidity = new Utilities.Mail_Validity_Helper().getMailSendingStatus();
                    SMS_Vendor_Validity_Helper valhelp = new SMS_Vendor_Validity_Helper();
                    sms_vendor vendor = valhelp.getVendorsByName("50kobo");

                    bool smsvalidity = (DateTime.Today <= vendor.expiry_date) ? true : false;

                    if (newmlr.bank1.code != "CASH")
                    {

                        sms message;
                        if (tran.code == "L")
                        {
                            if (smsvalidity)
                            {
                                sm mm = new CRUD.sms_crud().findByCode("CA");
                                message = new sms();
                                shop = new CRUD.shop_crud().find_shop_By_ID(shop.id);
                                if (message.initializeParameters(shop, mm.subject.Trim() + "\r\n" + mm.content.Trim() + "\r\n" + mm.conclusion.Trim(), newmlr))
                                    message.CustomizeAndSendSMS();
                            }
                            if (mailvalidity)
                            {

                                mail mail = new CRUD.mail_crud().findByCode("LA");
                                MailClient client = new MailClient();
                                client.SendLodgementReimbursementMail(shop, mail, newmastertransaction);
                            }
                        }
                        else if (tran.code == "R")
                        {
                            if (smsvalidity)
                            {
                                sm mm = new CRUD.sms_crud().findByCode("DA");
                                message = new sms();
                                if (message.initializeParameters(shop, mm.subject + "\r\n" + mm.content + "\r\n" + mm.conclusion, newmlr))
                                    message.CustomizeAndSendSMS();
                            }
                            if (mailvalidity)
                            {

                                mail mail = new CRUD.mail_crud().findByCode("RA");
                                MailClient client = new MailClient();
                                client.SendLodgementReimbursementMail(shop, mail, newmastertransaction);
                            }
                        }
                    }
                }
                catch (Exception)
                {

                }

            }
            else
            {


            }


        }

    }
}
