using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class debt_lodgement_reimbursement_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public debt_lodgement_reimbursment debt_lodgement_reimbursement_;
        public debt_lodgement_reimbursment debt_lodgement_reimbursement
        {
            get { return debt_lodgement_reimbursement_; }
            set { debt_lodgement_reimbursement_ = value; }
        }

        public debt_lodgement_reimbursement_crud(debt_lodgement_reimbursment debt_lodgement_reimbursement_)
        {
            this.debt_lodgement_reimbursement_ = debt_lodgement_reimbursement_;
        }

        public debt_lodgement_reimbursement_crud()
        {
            // TODO: Complete member initialization
        }

        public debt_lodgement_reimbursment find_debt_lodgement_reimbursement_By_ID(int id)
        {
            try
            {
                debt_lodgement_reimbursement = db.debt_lodgement_reimbursments.SingleOrDefault(a => a.id == id);
                return debt_lodgement_reimbursement;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt_lodgement_reimbursment find_debt_lodgement_reimbursement_By_AppId(string Appid)
        {
            try
            {
                debt_lodgement_reimbursement = db.debt_lodgement_reimbursments.SingleOrDefault(a => a.app_id == Appid);
                return debt_lodgement_reimbursement;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public debt_lodgement_reimbursment insert_debt_lodgement_reimbursement()
        {
            try
            {
                db.debt_lodgement_reimbursments.InsertOnSubmit(debt_lodgement_reimbursement);
                db.SubmitChanges();
                return find_debt_lodgement_reimbursement_By_AppId(debt_lodgement_reimbursement.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt_lodgement_reimbursment alter_debt_lodgement_reimbursement()
        {
            try
            {
                debt_lodgement_reimbursment prev_debt_lodgement_reimbursement = db.debt_lodgement_reimbursments.SingleOrDefault(a => a.id == debt_lodgement_reimbursement.id);

                prev_debt_lodgement_reimbursement.amount = debt_lodgement_reimbursement.amount;
                prev_debt_lodgement_reimbursement.bank = debt_lodgement_reimbursement.bank;
                prev_debt_lodgement_reimbursement.date = debt_lodgement_reimbursement.date;
                prev_debt_lodgement_reimbursement.from_date = debt_lodgement_reimbursement.from_date;
                prev_debt_lodgement_reimbursement.islocked = debt_lodgement_reimbursement.islocked;
                prev_debt_lodgement_reimbursement.isverified = debt_lodgement_reimbursement.isverified;
                prev_debt_lodgement_reimbursement.shop = debt_lodgement_reimbursement.shop;
                prev_debt_lodgement_reimbursement.teller_number = debt_lodgement_reimbursement.teller_number;
                prev_debt_lodgement_reimbursement.to_date = debt_lodgement_reimbursement.to_date;
                prev_debt_lodgement_reimbursement.transaction = debt_lodgement_reimbursement.transaction;
                prev_debt_lodgement_reimbursement.transaction_category = debt_lodgement_reimbursement.transaction_category;
                prev_debt_lodgement_reimbursement.transaction_type = debt_lodgement_reimbursement.transaction_type;
                prev_debt_lodgement_reimbursement.weekno = debt_lodgement_reimbursement.weekno;

                db.SubmitChanges();
                return debt_lodgement_reimbursement;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt_lodgement_reimbursment delete_debt_lodgement_reimbursement()
        {
            try
            {
                debt_lodgement_reimbursment prev_debt_lodgement_reimbursement = db.debt_lodgement_reimbursments.SingleOrDefault(a => a.id == debt_lodgement_reimbursement.id);
                db.debt_lodgement_reimbursments.DeleteOnSubmit(prev_debt_lodgement_reimbursement);
                return debt_lodgement_reimbursement;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<debt_lodgement_reimbursment> getLodgementReimbursementOfShopForDate(DateTime date, shop shop)
        {
            return db.debt_lodgement_reimbursments.Where(a => a.shop == shop.id && a.date == date).ToList<debt_lodgement_reimbursment>();
        }
    }
}