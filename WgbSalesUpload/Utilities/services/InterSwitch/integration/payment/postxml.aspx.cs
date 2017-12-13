using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WgbSalesUpload.Utilities.services.InterSwitch.integration.payment
{
    public partial class postxml : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Post back to either sandbox or live
            string url = string.Format("{0}://{1}{2}?id={3}", Request.Url.Scheme, Request.Url.Authority, VirtualPathUtility.ToAbsolute("~/Utilities/services/InterSwitch/integration/payment/gateway.ashx"), 123);
            //string url = "http://www.wgbet.ng/Utilities/services/InterSwitch/integration/payment/gateway.ashx";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);


            //Set values for the request back
            req.Method = "POST";
            req.ContentType = "text/xml";
            byte[] param = Request.BinaryRead(HttpContext.Current.Request.ContentLength);
            string strRequest = Encoding.ASCII.GetString(param);
            strRequest+= setxml(0);
            req.ContentLength = strRequest.Length;
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            streamOut.Write(strRequest);
            streamOut.Close();
            
            
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
             streamIn.Close();
            
            

        }

        private string setxml(int operation)
        {
            string strRequest = "";
            if(operation == 1)
                strRequest += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                            + "<PaymentNotificationRequest>"
                            + "<Payments>"
                            + "<Payment>"
                            + "<ProductGroupCode>HTTPSERVICE</ProductGroupCode>"
                            + "<PaymentLogId>02</PaymentLogId>"
                            + "<CustReference>A6018</CustReference>"
                            + "<AlternateCustReference>5678</AlternateCustReference>"
                            + "<Amount>27150.00</Amount>"
                            + "<PaymentStatus>0</PaymentStatus>"
                            + "<PaymentMethod>Cash</PaymentMethod>"
                            + "<PaymentReference>FBP|BRH|ABC|28-02-2013|312247</PaymentReference>"
                            + "<TerminalId/>"
                            + "<ChannelName>Bank Branc</ChannelName>"
                            + "<Location> Nekede Branch</Location>"
                            + "<IsReversal>False</IsReversal>"
                            + "<PaymentDate>02/28/2013 12:03:01</PaymentDate>"
                            + "<SettlementDate>03/01/2013 00:00:01</SettlementDate>"
                            + "<InstitutionId>FDNKD</InstitutionId>"
                            + "<InstitutionName>Federal Poly Nekede</InstitutionName>"
                            + "<BranchName> Nekede Branch</BranchName>"
                            + "<BankName>Fidelity Bank Plc</BankName>"
                            + "<FeeName/>"
                            + "<CustomerName>OCHULOR STANLEY</CustomerName>"
                            + "<OtherCustomerInfo>|07031967265</OtherCustomerInfo>"
                            + "<ReceiptNo>1305905886</ReceiptNo>"
                            + "<CollectionsAccount>NGN09023104005</CollectionsAccount>"
                            + "<ThirdPartyCode>ETECH03</ThirdPartyCode>"
                            + "<PaymentItems>"
                            + "<PaymentItem>"
                            + "<ItemName>OND Even.2nd Year(2012/2013) (Sch of Engineering)</ItemName>"
                            + "<ItemCode>LDC</ItemCode>"
                            + "<ItemAmount>27150.00</ItemAmount>"
                            + "<LeadBankCode>FBP</LeadBankCode>"
                            + "<LeadBankCbnCode>070</LeadBankCbnCode>"
                            + "<LeadBankName>Fidelity Bank Plc</LeadBankName>"
                            + "</PaymentItem>"
                            + "</PaymentItems>"
                            + "<BankCode>FBP</BankCode>"
                            + "<CustomerAddress/>"
                            + "<CustomerPhoneNumber>07031967265</CustomerPhoneNumber>"
                            + "<DepositorName>07031967265</DepositorName>"
                            + "<DepositSlipNumber>0625046</DepositSlipNumber>"
                            + "<PaymentCurrency>566</PaymentCurrency>"
                            + "</Payment>"
                            + "</Payments>"
                            + "</PaymentNotificationRequest>";
            else
                strRequest += "<?xml version=\"1.0\" encoding=\"utf-8\" ?>"
                                + "<CustomerInformationRequest>"
                                + "<MerchantReference>126</MerchantReference>"
                                + "<CustReference>A10363737</CustReference >"
                                +"<PaymentItemCode>SC</PaymentItemCode>"
                                + "<ServiceUsername></ServiceUsername>"
                                + "<ServicePassword></ServicePassword>"
                                + "<FtpUsername></FtpUsername>"
                                + "<FtpPassword></FtpPassword>"
                                + "</CustomerInformationRequest>";
            return strRequest;
        }
    }
}