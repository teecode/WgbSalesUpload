using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class banner_crud
    {
        public banner banner_;
        public banner banner
        {
            get { return banner_; }
            set { banner_ = value; }
        }

        public banner_crud(banner banner_)
        {
            this.banner_ = banner_;
        }

        public banner_crud()
        {
            // TODO: Complete member initialization
        }

        public banner find_banner_By_ID(int id)
        {
            try
            {
                banner = db.banners.SingleOrDefault(a => a.id == id);
                return banner;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public banner find_banner_By_AppId(string Appid)
        {
            try
            {
                banner = db.banners.SingleOrDefault(a => a.app_id == Appid);
                return banner;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public banner insert_banner()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    application app = new application
                    {
                        app_id = Guid.NewGuid().ToString()
                    };
                    db.applications.InsertOnSubmit(app);
                    banner.app_id = app.app_id;
                    db.banners.InsertOnSubmit(banner);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_banner_By_AppId(banner.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public banner alter_banner()
        {
            try
            {
                banner prev_banner = db.banners.SingleOrDefault(a => a.id == banner.id);

                prev_banner.position = banner.position;
                prev_banner.url = banner.url;
                prev_banner.alt = banner.alt;
                prev_banner.source = banner.source;
                

                db.SubmitChanges();
                return banner;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public banner delete_banner()
        {
            try
            {
                banner prev_banner = db.banners.SingleOrDefault(a => a.id == banner.id);
                db.banners.DeleteOnSubmit(prev_banner);
                return banner;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<WgbSalesUpload.banner> getAllbanners()
        {
            return db.banners.ToList<banner>();
        }
        BettingSuiteDataContext db = new BettingSuiteDataContext();
    }
}