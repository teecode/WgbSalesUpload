using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using WgbSalesUpload.Utilities.services.InterSwitch.integration.payment;

namespace WgbSalesUpload.Utilities.InterSwitch
{
    public class PaymentNotificationRequestHelper
    {
        private string CustReference, PaymentMethod, PaymentReference, OriginalPaymentReference,OriginalPaymentName, ChannelName, Location, InstitutionID
                       , InstitutionName, BankName, CustomerName, OtherCustomerInfo, ReceiptNo,AlternateCustReference;
        private Decimal Amount, ItemAmount;

        private int PaymentLogId, OriginalPaymentLogId, PaymentCurrency,paymentStatus;

        private DateTime PaymentDate;

        private string BranchName, CollectionsAccount, BankCode, DepositorName, CustomerPhoneNumber, CustomerAddress, CategoryCode
                       , CategoyName, LeadBankCode, LeadBankName, LeadBankCbnCode, ThirdPartyCode, ServiceUsername, FeeName, ServicePassword,
                        FTPUsername, FTPPassword;
        
        private bool reversal;
        private string TerminalId;
        private DateTime SettlementDate;
        private List<PaymentItemHelper> PaymentItems;


        public PaymentNotificationRequestHelper()
        { 
        
        }



        public int getPaymentStatus()
        {
            return paymentStatus;
        }

        public void setPaymentStatus(int status)
        {
            this.paymentStatus = status;
        }

        public string getAlternateCustReference()
        {
            return AlternateCustReference;
        }

        public void setAlternateCustReference(string AltCustReference)
        {
            this.AlternateCustReference = AltCustReference;
        }
        public string getCustReference()
        {
            return CustReference;
        }

        public void setCustReference(string CustReference)
        {
            this.CustReference = CustReference;
        }

        public string getPaymentMethod()
        {
            return PaymentMethod;
        }

        public void setPaymentMethod(string PaymentMethod)
        {
            this.PaymentMethod = PaymentMethod;
        }

        public string getPaymentReference()
        {
            return PaymentReference;
        }

        public void setPaymentReference(string PaymentReference)
        {
            this.PaymentReference = PaymentReference;
        }

        public string getOriginalPaymentReference()
        {
            return OriginalPaymentReference;
        }

        public void setOriginalPaymentReference(string OriginalPaymentReference)
        {
            this.OriginalPaymentReference = OriginalPaymentReference;
        }

        public string getChannelName()
        {
            return ChannelName;
        }

        public void setChannelName(string ChannelName)
        {
            this.ChannelName = ChannelName;
        }

        public string getLocation()
        {
            return Location;
        }

        public void setLocation(string Location)
        {
            this.Location = Location;
        }

        public string getInstitutionID()
        {
            return InstitutionID;
        }

        public void setInstitutionID(string InstitutionID)
        {
            this.InstitutionID = InstitutionID;
        }

        public string getInstitutionName()
        {
            return InstitutionName;
        }

        public void setInstitutionName(string InstitutionName)
        {
            this.InstitutionName = InstitutionName;
        }

        public string getBankName()
        {
            return BankName;
        }

        public void setBankName(string BankName)
        {
            this.BankName = BankName;
        }

        public string getCustomerName()
        {
            return CustomerName;
        }

        public void setCustomerName(string CustomerName)
        {
            this.CustomerName = CustomerName;
        }

        public string getOtherCustomerInfo()
        {
            return OtherCustomerInfo;
        }

        public void setOtherCustomerInfo(string OtherCustomerInfo)
        {
            this.OtherCustomerInfo = OtherCustomerInfo;
        }


        
        public string getReceiptNo()
        {
            return ReceiptNo;
        }

        public void setReceiptNo(string ReceiptNo)
        {
            this.ReceiptNo = ReceiptNo;
        }

        public Decimal getAmount()
        {
            return Amount;
        }

        public void setAmount(Decimal Amount)
        {
            this.Amount = Amount;
        }

        public Decimal getItemAmount()
        {
            return ItemAmount;
        }

        public void setItemAmount(Decimal ItemAmount)
        {
            this.ItemAmount = ItemAmount;
        }

        public int getPaymentLogId()
        {
            return PaymentLogId;
        }

        public void setPaymentLogId(int PaymentLogId)
        {
            this.PaymentLogId = PaymentLogId;
        }

        public int getOriginalPaymentLogId()
        {
            return OriginalPaymentLogId;
        }

        public void setOriginalPaymentLogId(int OriginalPaymentLogId)
        {
            this.OriginalPaymentLogId = OriginalPaymentLogId;
        }

        public int getPaymentCurrency()
        {
            return PaymentCurrency;
        }

        public void setPaymentCurrency(int PaymentCurrency)
        {
            this.PaymentCurrency = PaymentCurrency;
        }

        public DateTime getPaymentDate()
        {
            return PaymentDate;
        }

        public void setPaymentDate(DateTime PaymentDate)
        {
            this.PaymentDate = PaymentDate;
        }

        public string getBranchName()
        {
            return BranchName;
        }

        public void setBranchName(string BranchName)
        {
            this.BranchName = BranchName;
        }

        public string getCollectionsAccount()
        {
            return CollectionsAccount;
        }

        public void setCollectionsAccount(string CollectionsAccount)
        {
            this.CollectionsAccount = CollectionsAccount;
        }

        public string getBankCode()
        {
            return BankCode;
        }

        public void setBankCode(string BankCode)
        {
            this.BankCode = BankCode;
        }

        public string getDepositorName()
        {
            return DepositorName;
        }

        public void setDepositorName(string DepositorName)
        {
            this.DepositorName = DepositorName;
        }

        public string getCustomerPhoneNumber()
        {
            return CustomerPhoneNumber;
        }

        public void setCustomerPhoneNumber(string CustomerPhoneNumber)
        {
            this.CustomerPhoneNumber = CustomerPhoneNumber;
        }

        public string getCustomerAddress()
        {
            return CustomerAddress;
        }

        public void setCustomerAddress(string CustomerAddress)
        {
            this.CustomerAddress = CustomerAddress;
        }

        public string getCategoryCode()
        {
            return CategoryCode;
        }

        public void setCategoryCode(string CategoryCode)
        {
            this.CategoryCode = CategoryCode;
        }

        public string getCategoyName()
        {
            return CategoyName;
        }

        public void setCategoyName(string CategoyName)
        {
            this.CategoyName = CategoyName;
        }

        public string getLeadBankCode()
        {
            return LeadBankCode;
        }

        public void setLeadBankCode(string LeadBankCode)
        {
            this.LeadBankCode = LeadBankCode;
        }

        public string getLeadBankName()
        {
            return LeadBankName;
        }

        public void setLeadBankName(string LeadBankName)
        {
            this.LeadBankName = LeadBankName;
        }

        public string getLeadBankCbnCode()
        {
            return LeadBankCbnCode;
        }

        public void setLeadBankCbnCode(string LeadBankCbnCode)
        {
            this.LeadBankCbnCode = LeadBankCbnCode;
        }

        public string getThirdPartyCode()
        {
            return ThirdPartyCode;
        }

        public void setThirdPartyCode(string ThirdPartyCode)
        {
            this.ThirdPartyCode = ThirdPartyCode;
        }

        public string getServiceUsername()
        {
            return ServiceUsername;
        }

        public void setServiceUsername(string ServiceUsername)
        {
            this.ServiceUsername = ServiceUsername;
        }

        public string getFeeName()
        {
            return FeeName;
        }

        public void setFeeName(string FeeName)
        {
            this.FeeName = FeeName;
        }

        public string getServicePassword()
        {
            return ServicePassword;
        }

        public void setServicePassword(string ServicePassword)
        {
            this.ServicePassword = ServicePassword;
        }

        public string getFTPUsername()
        {
            return FTPUsername;
        }

        public void setFTPUsername(string FTPUsername)
        {
            this.FTPUsername = FTPUsername;
        }

        public string getFTPPassword()
        {
            return FTPPassword;
        }

        public void setFTPPassword(string FTPPassword)
        {
            this.FTPPassword = FTPPassword;
        }

        public void setReversal(bool Reversal)
        {
            this.reversal = Reversal;
        }

        public bool isReversal()
        {
            return reversal;
        }

     

        public void setTerminalId(string tid)
        {
            this.TerminalId = tid;
        }

        private string getTerminalId()
        {
            return TerminalId;
        }
         
        public DateTime getSettlementDate()
        {
            return SettlementDate;
        }

        public void setPaymentItems(IEnumerable<XElement> enumerable)
        {
            if(enumerable.Count() > 0)
            {
            PaymentItems = new List<PaymentItemHelper>(enumerable.Count());
            
           foreach (var  request in enumerable)
           {
               PaymentItemHelper p = new PaymentItemHelper();
               p.setItemAmount(Decimal.Parse(request.Element("ItemAmount").Value));
               p.setItemCode(request.Element("ItemCode").Value);
               p.setItemName(request.Element("ItemName").Value);
              // p.setLeadBankCode((request.Element("LeadBankCode").Value));
              // p.setLeadBankCbnCode((request.Element("LeadBankCbnCode").Value));
              // p.setLeadBankName((request.Element("LeadBankName").Value));
               PaymentItems.Add(p);
           }
            }
        }

        public List<PaymentItemHelper> getPaymentItems()
        {
            return PaymentItems;
        }

        public void setSettlementDate(DateTime dateTime)
        {
            this.SettlementDate = dateTime;
        }

        public string getOriginalPaymentName()
        {
            return OriginalPaymentName;
        }

        public void setOriginalPaymentName(string name)
        {
            OriginalPaymentName = name;
        }
    }
}