using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WgbSalesUpload.Utilities.services.InterSwitch.integration.payment
{
    public class ItemHelper
    {
        string productName, productCode;
        int quantity;
        decimal price,subtotal,tax,total;
      
        

        public ItemHelper (string productName, string productCode, int quantity, Decimal price, Decimal subtotal, Decimal tax, Decimal total) {
        this.productName = productName;
        this.productCode = productCode;
        this.quantity = quantity;
        this.price = price;
        this.subtotal = subtotal;
        this.tax = tax;
        this.total = total;
    }

    public Decimal getTotal() {
        return total;
    }

    public void setTotal(Decimal total) {
        this.total = total;
    }

    public string getProductName() {
        return productName;
    }

    public void setProductName(string productName) {
        this.productName = productName;
    }

    public string getProductCode() {
        return productCode;
    }

    public void setProductCode(string productCode) {
        this.productCode = productCode;
    }

    public int getQuantity() {
        return quantity;
    }

    public void setQuantity(int quantity) {
        this.quantity = quantity;
    }

    public Decimal getPrice() {
        return price;
    }

    public void setPrice(Decimal price) {
        this.price = price;
    }

    public Decimal getSubtotal() {
        return subtotal;
    }

    public void setSubtotal(Decimal subtotal) {
        this.subtotal = subtotal;
    }

    public Decimal getTax() {
        return tax;
    }

    public void setTax(Decimal tax) {
        this.tax = tax;
    }

    }
}
