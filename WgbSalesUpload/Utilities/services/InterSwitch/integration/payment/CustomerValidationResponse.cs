using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities.InterSwitch
{
    public class CustomerValidationResponse
    {
        private customer _customer; 
        private int _merchantRefernce;
        private decimal _amount;
       


        public CustomerValidationResponse(customer customer, int merchantRefernce)
        { 
           this._customer = customer;
            this._merchantRefernce = merchantRefernce;
             _amount = Decimal.Parse("0.00");


        }

       public customer customer
        {
            get { return this._customer; }
            set { this._customer = value; }
        }

       public int merchantReference
       {
           get { return this._merchantRefernce; }
           set { this._merchantRefernce = value; }
       }

       public decimal amount
       {
           get { return this._amount; }
           set { this._amount = value; }
       }

      
       
    }

    }
