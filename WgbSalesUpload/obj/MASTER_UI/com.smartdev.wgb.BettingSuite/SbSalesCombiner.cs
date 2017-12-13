using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.obj.MASTER_UI.com.smartdev.wgb.BettingSuite
{
    public class SbSalesCombiner
    {
        private List<master_sb_sale> _mastersales;
        private List<shop_daily_sale> _shopsales;
        private master_sb_sale _combinedmastersales;
        private shop_daily_sale _combinedshopsales;
        private master_sb_sale _combinedsale;       
        private shop _shop;
        private DateTime _date;
        private decimal msales,ssales,csales;
        private decimal mwinnings,swinnings,cwinnings;
        private decimal mcancelled,scancelled,ccancelled;
        private decimal mbalance,sbalance,cbalance;
        private decimal mcommision,scommision,ccommision;
        private decimal mnetbalance,snetbalance,cnetbalance;
        private decimal mstake, sstake, cstake;
        private decimal msingle_sales, msingle_comm, mmulti_sales, mmulti_comm, msingle_percent;
 
          public SbSalesCombiner( List<master_sb_sale> mastersales ,List<shop_daily_sale> shopsales, shop shop)
         {

             _mastersales = mastersales;
             _shopsales = shopsales;
             _shop = shop;
             
         }

          public SbSalesCombiner(List<master_sb_sale> mastersales, shop shop) : this(mastersales,null,shop)
          {

          
          }
         
        public master_sb_sale combinedsale
          {
              get { return _combinedsale; }
              set { _combinedsale = value; }
          }


          public master_sb_sale combinedmastersales
          {
              get { return _combinedmastersales; }
              set { _combinedmastersales = value; }
          }


          public shop_daily_sale combinedshopsales
          {
              get { return _combinedshopsales; }
              set { _combinedshopsales = value; }
          }


          public shop shop
          {
              get { return _shop; }
              set { _shop = value; }
          }


          public DateTime date
          {
              get { return _date; }
              set { _date = value; }
          }


          public List<master_sb_sale> mastersales
          {
              get { return _mastersales; }
              set { _mastersales = value; }
          }

          public List<shop_daily_sale> shopsales
          {
              get { return _shopsales; }
              set { _shopsales = value; }
          }


          public bool hasMasterSales
          {
              get { return _mastersales != null && _mastersales.Count > 0; }
              //set { (_mastersales != null && _mastersales.Count > 0) = value; }
          }


          public bool hasShopSales
          {
              get { return _shopsales != null && _shopsales.Count > 0; }
              //set { (_mastersales != null && _mastersales.Count > 0) = value; }
          }


     

        public bool hasMasterSale()//, shop_daily_sale shopsale)
        {
            return _mastersales!= null && _mastersales.Count > 0 ;
        }

        public bool hasShopSale()//, shop_daily_sale shopsale)
        {
            return  _shopsales!= null &&_shopsales.Count>0;

        }

        public void computeSales()
        {
            if (hasMasterSale())
            {
                      mstake = (decimal)mastersales.Sum(a => a.stake);
                      msales = (decimal)mastersales.Sum(a => a.sales);
                      mwinnings = (decimal)mastersales.Sum(a => a.winnings);
                      mcancelled = (decimal)mastersales.Sum(a => a.cancelled);
                       mbalance = (decimal)mastersales.Sum(a => a.balance);
                       mcommision = (decimal)mastersales.Sum(a => a.commision);
                       mnetbalance = (decimal)mastersales.Sum(a => a.net_balance);
                       msingle_sales = (decimal)mastersales.Sum(a => a.single_stake_sales);
                       msingle_comm = (decimal)mastersales.Sum(a => a.single_stake_commision);
                       mmulti_sales = msales - msingle_sales;
                       mmulti_comm = mcommision - msingle_comm;
                       msingle_percent = (decimal)mastersales.Average(a => a.single_stake_percent);
                      
                     
            }
            else
            {
                msales = mwinnings = mcancelled = mbalance = mcommision = mnetbalance = mstake = Decimal.Zero;
            }
            if (hasShopSale())
            {
                sstake = (decimal)shopsales.Sum(a => a.stake);
                ssales = (decimal)shopsales.Sum(a => a.sales);
                swinnings = (decimal)shopsales.Sum(a => a.winnings);
                scancelled = (decimal)shopsales.Sum(a => a.cancelled);
                sbalance = (decimal)shopsales.Sum(a => a.balance);
                scommision = (decimal)shopsales.Sum(a => a.commision);
                snetbalance = (decimal)shopsales.Sum(a => a.net_balance);
               
            }
            else {
                ssales = swinnings = scancelled = sbalance = scommision = snetbalance = sstake = Decimal.Zero;
            }

            _combinedshopsales = new shop_daily_sale
            {
                stake = sstake,
                sales = ssales,
                winnings = swinnings,
                cancelled = scancelled,
                balance = sbalance,
                commision = scommision,
                net_balance = snetbalance,
                shop1 = shop

            };

            _combinedmastersales = new master_sb_sale
            {
                stake = mstake,
                sales = msales,
                winnings = mwinnings,
                cancelled = mcancelled,
                balance = mbalance,
                commision = mcommision,
                net_balance = mnetbalance,
                shop1 = shop,
                single_stake_commision =msingle_comm,
                single_stake_percent = msingle_percent,
                single_stake_sales = msingle_sales

            };

            cstake = Decimal.Subtract(mstake, sstake);
            csales = Decimal.Subtract(msales,ssales);
            cwinnings = Decimal.Subtract(mwinnings, swinnings);
            ccancelled = Decimal.Subtract(mcancelled,scancelled);
            cbalance = Decimal.Subtract(mbalance,sbalance);
            ccommision = Decimal.Subtract(mcommision,scommision);
            cnetbalance = Decimal.Subtract(mnetbalance,snetbalance);


            _combinedsale = new master_sb_sale
            {
                stake = cstake,
                sales = csales,
                winnings = cwinnings,
                cancelled = ccancelled,
                balance = cbalance,
                commision = ccommision,
                net_balance = cnetbalance,
                shop1 = _shop,
                single_stake_commision = msingle_comm,
                single_stake_percent = msingle_percent,
                single_stake_sales = msingle_sales
            };
           //_combinedmastersales.shop1.shop_code
        }
    }
}