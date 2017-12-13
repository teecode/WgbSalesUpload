using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class shop_lodgement_reimbursment_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public shop_lodgement_reimbursment shop_lodgement_reimbursment_;
        public shop_lodgement_reimbursment shop_lodgement_reimbursment
        {
            get { return shop_lodgement_reimbursment_; }
            set { shop_lodgement_reimbursment_ = value; }
        }

        public shop_lodgement_reimbursment_crud(shop_lodgement_reimbursment shop_lodgement_reimbursment_)
        {
            this.shop_lodgement_reimbursment_ = shop_lodgement_reimbursment_;
        }

        public shop_lodgement_reimbursment find_shop_lodgement_reimbursment_By_ID(int id)
        {
            try
            {
                shop_lodgement_reimbursment = db.shop_lodgement_reimbursments.SingleOrDefault(a => a.id == id);
                return shop_lodgement_reimbursment;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_lodgement_reimbursment find_shop_lodgement_reimbursment_By_AppId(string Appid)
        {
            try
            {
                shop_lodgement_reimbursment = db.shop_lodgement_reimbursments.SingleOrDefault(a => a.app_id == Appid);
                return shop_lodgement_reimbursment;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
        public shop_lodgement_reimbursment insert_shop_lodgement_reimbursment()
        {
            try
            {
                db.shop_lodgement_reimbursments.InsertOnSubmit(shop_lodgement_reimbursment);
                db.SubmitChanges();
                return find_shop_lodgement_reimbursment_By_AppId(shop_lodgement_reimbursment.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_lodgement_reimbursment alter_shop_lodgement_reimbursment()
        {
            try
            {
                shop_lodgement_reimbursment prev_shop_lodgement_reimbursment = db.shop_lodgement_reimbursments.SingleOrDefault(a => a.id == shop_lodgement_reimbursment.id);

                prev_shop_lodgement_reimbursment.amount = shop_lodgement_reimbursment.amount;
                prev_shop_lodgement_reimbursment.bank = shop_lodgement_reimbursment.bank;
                prev_shop_lodgement_reimbursment.date = shop_lodgement_reimbursment.date;
                prev_shop_lodgement_reimbursment.from_date = shop_lodgement_reimbursment.from_date;
                prev_shop_lodgement_reimbursment.islocked = shop_lodgement_reimbursment.islocked;
                prev_shop_lodgement_reimbursment.isverified = shop_lodgement_reimbursment.isverified;
                prev_shop_lodgement_reimbursment.shop = shop_lodgement_reimbursment.shop;
                prev_shop_lodgement_reimbursment.teller_number = shop_lodgement_reimbursment.teller_number;
                prev_shop_lodgement_reimbursment.to_date = shop_lodgement_reimbursment.to_date;
                prev_shop_lodgement_reimbursment.transaction = shop_lodgement_reimbursment.transaction;
                prev_shop_lodgement_reimbursment.weekno = shop_lodgement_reimbursment.weekno;

                db.SubmitChanges();
                return shop_lodgement_reimbursment;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public shop_lodgement_reimbursment delete_shop_lodgement_reimbursment()
        {
            try
            {
                shop_lodgement_reimbursment prev_shop_lodgement_reimbursment = db.shop_lodgement_reimbursments.SingleOrDefault(a => a.id == shop_lodgement_reimbursment.id);
                db.shop_lodgement_reimbursments.DeleteOnSubmit(prev_shop_lodgement_reimbursment);
                return shop_lodgement_reimbursment;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}