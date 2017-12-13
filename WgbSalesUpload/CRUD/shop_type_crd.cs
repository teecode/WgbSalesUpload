using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class shop_type_crd
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public shop_type shop_type_;
        public shop_type shop_type
        {
            get { return shop_type_; }
            set { shop_type_ = value; }
        }

        public shop_type_crd(shop_type shop_type_)
        {
            this.shop_type_ = shop_type_;
        }

        public shop_type find_shop_type_By_ID(int id)
        {
            try
            {
                shop_type = db.shop_types.SingleOrDefault(a => a.id == id);
                return shop_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_type find_shop_type_By_AppId(string Appid)
        {
            try
            {
                shop_type = db.shop_types.SingleOrDefault(a => a.app_id == Appid);
                return shop_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
        public shop_type insert_shop_type()
        {
            try
            {
                db.shop_types.InsertOnSubmit(shop_type);
                db.SubmitChanges();
                return find_shop_type_By_AppId(shop_type.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_type alter_shop_type()
        {
            try
            {
                shop_type prev_shop_type = db.shop_types.SingleOrDefault(a => a.id == shop_type.id);

                prev_shop_type.code = shop_type.code;
                prev_shop_type.name = shop_type.name;
                
                

                db.SubmitChanges();
                return shop_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_type delete_shop_type()
        {
            try
            {
                shop_type prev_shop_type = db.shop_types.SingleOrDefault(a => a.id == shop_type.id);
                db.shop_types.DeleteOnSubmit(prev_shop_type);
                return shop_type;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}