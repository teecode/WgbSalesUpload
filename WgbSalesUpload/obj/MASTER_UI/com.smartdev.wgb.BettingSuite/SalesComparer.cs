using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WgbSalesUpload.MASTER_UI.com.smartdev.wgb.BettingSuite;

namespace WgbSalesUpload.MASTER_UI
{
 public class CombinedSalesComparer
    {

     BettingSuiteDataContext db;

     List<SalesCombiner> sclist_ = new List<SalesCombiner>();
     List<shop_daily_sale> shopsalesList;
     public CombinedSalesComparer(DateTime startdate, DateTime enddate) : this(startdate, enddate, true)
     {
       
     }

        public CombinedSalesComparer(DateTime startdate, DateTime enddate, bool compare)
        {

         db  = new BettingSuiteDataContext();
            //get available sales for both shop and master in the date range
            List<master_daily_sale> mastersalesList = db.master_daily_sales.Where(a => a.date >= startdate && a.date <= enddate).ToList<master_daily_sale>();
            if (compare)
            {
               shopsalesList = db.shop_daily_sales.Where(a => a.date >= startdate && a.date <= enddate).ToList<shop_daily_sale>();
            }
                //get all the available shops
            List<shop> shops = db.shops.ToList<shop>();
            foreach (shop shop in shops)
            {
                  
                List<master_daily_sale> mlist = mastersalesList.Where(a => a.shop1 == shop).ToList<master_daily_sale>();
                if (compare)
                {
                    List<shop_daily_sale> slist = shopsalesList.Where(a => a.shop1 == shop).ToList<shop_daily_sale>();
                    SalesCombiner sc = new SalesCombiner(mlist, slist, shop);
                    sc.computeSales();
                    sclist_.Add(sc);
                }
                else
                {
                    SalesCombiner sc = new SalesCombiner(mlist, shop);
                    sc.computeSales();
                    sclist_.Add(sc);
                }
            
            }
        }

           public List<SalesCombiner> sclist
           {
              get{return sclist_;}
               set{sclist_ = value;}
           }
         
        }
            // for(var day = startdate; day.Date <= enddate; day = day.AddDays(1))
            //  {
            //      msale = mastersalesList.SingleOrDefault(a => a.date == day);
            //      ssale = shopsalesList.SingleOrDefault(a => a.date == day);

            //  }

      //  public bool isSame(master_daily_sale mastersale, shop_daily_sale shopsale)
      //  {
      //      return mastersale.date == shopsale.date && mastersale.shop1 == shopsale.shop1;
      //  }

       
    }

