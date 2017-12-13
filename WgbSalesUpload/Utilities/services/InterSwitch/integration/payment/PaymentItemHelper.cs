using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities.services.InterSwitch.integration.payment
{
    public class PaymentItemHelper
    {

        private string ItemName, ItemCode, LeadBankCode, LeadBankName, LeadBankCbnCode;
        private Decimal ItemAmount;

        public PaymentItemHelper(string ItemName, string ItemCode, string LeadBankCode, string LeadBankName, string LeadBankCbnCode, Decimal ItemAmount) {
        this.ItemName = ItemName;
        this.ItemCode = ItemCode;
        this.LeadBankCode = LeadBankCode;
        this.LeadBankName = LeadBankName;
        this.LeadBankCbnCode = LeadBankCbnCode;
        this.ItemAmount = ItemAmount;
    }

    public PaymentItemHelper() {
    }

       
    public string getItemName() {
        return ItemName;
    }

    public void setItemName(string ItemName) {
        this.ItemName = ItemName;
    }

    public string getItemCode() {
        return ItemCode;
    }

    public void setItemCode(string ItemCode) {
        this.ItemCode = ItemCode;
    }

    public string getLeadBankCode() {
        return LeadBankCode;
    }

    public void setLeadBankCode(string LeadBankCode) {
        this.LeadBankCode = LeadBankCode;
    }

    public string getLeadBankName() {
        return LeadBankName;
    }

    public void setLeadBankName(string LeadBankName) {
        this.LeadBankName = LeadBankName;
    }

    public string getLeadBankCbnCode() {
        return LeadBankCbnCode;
    }

    public void setLeadBankCbnCode(string LeadBankCbnCode) {
        this.LeadBankCbnCode = LeadBankCbnCode;
    }

    public Decimal getItemAmount() {
        return ItemAmount;
    }

    public void setItemAmount(Decimal ItemAmount) {
        this.ItemAmount = ItemAmount;
    }
   

       
    }
}