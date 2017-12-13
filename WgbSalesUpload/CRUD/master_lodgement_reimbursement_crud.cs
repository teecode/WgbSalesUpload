using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class master_lodgement_reimbursment_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public master_lodgement_reimbursment master_lodgement_reimbursment_;
        public master_lodgement_reimbursment master_lodgement_reimbursment
        {
            get { return master_lodgement_reimbursment_; }
            set { master_lodgement_reimbursment_ = value; }
        }

        public master_lodgement_reimbursment_crud(master_lodgement_reimbursment master_lodgement_reimbursment_)
        {
            this.master_lodgement_reimbursment_ = master_lodgement_reimbursment_;
        }

        public master_lodgement_reimbursment_crud()
        {
            // TODO: Complete member initialization
        }

        public master_lodgement_reimbursment find_master_lodgement_reimbursment_By_ID(int id)
        {
            try
            {
                master_lodgement_reimbursment = db.master_lodgement_reimbursments.SingleOrDefault(a => a.id == id);
                return master_lodgement_reimbursment;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_lodgement_reimbursment find_master_lodgement_reimbursment_By_AppId(string Appid)
        {
            try
            {
                master_lodgement_reimbursment = db.master_lodgement_reimbursments.SingleOrDefault(a => a.app_id == Appid);
                return master_lodgement_reimbursment;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
        public master_lodgement_reimbursment insert_master_lodgement_reimbursment()
        {
            try
            {
                db.master_lodgement_reimbursments.InsertOnSubmit(master_lodgement_reimbursment);
                db.SubmitChanges();
                return find_master_lodgement_reimbursment_By_AppId(master_lodgement_reimbursment.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_lodgement_reimbursment alter_master_lodgement_reimbursment()
        {
            try
            {
                master_lodgement_reimbursment prev_master_lodgement_reimbursment = db.master_lodgement_reimbursments.SingleOrDefault(a => a.id == master_lodgement_reimbursment.id);

                prev_master_lodgement_reimbursment.amount = master_lodgement_reimbursment.amount;
                prev_master_lodgement_reimbursment.bank = master_lodgement_reimbursment.bank;
                prev_master_lodgement_reimbursment.date = master_lodgement_reimbursment.date;
                prev_master_lodgement_reimbursment.from_date = master_lodgement_reimbursment.from_date;
                prev_master_lodgement_reimbursment.islocked = master_lodgement_reimbursment.islocked;
                prev_master_lodgement_reimbursment.isverified = master_lodgement_reimbursment.isverified;
                prev_master_lodgement_reimbursment.shop = master_lodgement_reimbursment.shop;
                prev_master_lodgement_reimbursment.teller_number = master_lodgement_reimbursment.teller_number;
                prev_master_lodgement_reimbursment.to_date = master_lodgement_reimbursment.to_date;
                prev_master_lodgement_reimbursment.transaction = master_lodgement_reimbursment.transaction;
                prev_master_lodgement_reimbursment.transaction_category = master_lodgement_reimbursment.transaction_category;
                prev_master_lodgement_reimbursment.transaction_type = master_lodgement_reimbursment.transaction_type;
                prev_master_lodgement_reimbursment.weekno = master_lodgement_reimbursment.weekno;

                db.SubmitChanges();
                return master_lodgement_reimbursment;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public master_lodgement_reimbursment delete_master_lodgement_reimbursment()
        {
            try
            {
                master_lodgement_reimbursment prev_master_lodgement_reimbursment = db.master_lodgement_reimbursments.SingleOrDefault(a => a.id == master_lodgement_reimbursment.id);
                db.master_lodgement_reimbursments.DeleteOnSubmit(prev_master_lodgement_reimbursment);
                return master_lodgement_reimbursment;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<master_lodgement_reimbursment> getLodgementReimbursementOfShopForDate(DateTime date, shop shop)
        {
            return db.master_lodgement_reimbursments.Where(a => a.shop == shop.id && a.date == date).ToList<master_lodgement_reimbursment>();
        }

        public List<WgbSalesUpload.master_lodgement_reimbursment> getLodgementReimbursementWithinDate(DateTime dateTime1, DateTime dateTime2)
        {
            return db.master_lodgement_reimbursments.Where(a => a.date >= dateTime1 && a.date <=dateTime2).ToList<master_lodgement_reimbursment>();
        }
    }
}