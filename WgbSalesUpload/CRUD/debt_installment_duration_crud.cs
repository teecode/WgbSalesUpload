using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class debt_installment_duration_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public debt_installment_duration debt_installment_duration_;
        public debt_installment_duration debt_installment_duration
        {
            get { return debt_installment_duration_; }
            set { debt_installment_duration_ = value; }
        }

        public debt_installment_duration_crud(debt_installment_duration debt_installment_duration_)
        {
            this.debt_installment_duration_ = debt_installment_duration_;
        }

        public debt_installment_duration_crud()
        {
            // TODO: Complete member initialization
        }

        public debt_installment_duration find_debt_installment_duration_By_ID(int id)
        {
            try
            {
                debt_installment_duration = db.debt_installment_durations.SingleOrDefault(a => a.id == id);
                return debt_installment_duration;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt_installment_duration find_debt_installment_duration_By_AppId(string Appid)
        {
            try
            {
                debt_installment_duration = db.debt_installment_durations.SingleOrDefault(a => a.app_id == Appid);
                return debt_installment_duration;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt_installment_duration find_debt_installment_duration_By_code(string code)
        {
            try
            {
                debt_installment_duration tran = db.debt_installment_durations.SingleOrDefault(a => a.code == code);
                return tran;
            }
            catch (Exception)
            {
                return null;
            }
        }

        

        
        public debt_installment_duration insert_debt_installment_duration()
        {
            try
            {
                db.debt_installment_durations.InsertOnSubmit(debt_installment_duration);
                db.SubmitChanges();
                return find_debt_installment_duration_By_AppId(debt_installment_duration.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt_installment_duration alter_debt_installment_duration()
        {
            try
            {
                debt_installment_duration prev_debt_installment_duration = db.debt_installment_durations.SingleOrDefault(a => a.id == debt_installment_duration.id);
                prev_debt_installment_duration.name = debt_installment_duration.name;
                prev_debt_installment_duration.code = debt_installment_duration.code;
                
                db.SubmitChanges();
                return debt_installment_duration;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt_installment_duration delete_debt_installment_duration()
        {
            try
            {
                debt_installment_duration prev_debt_installment_duration = db.debt_installment_durations.SingleOrDefault(a => a.id == debt_installment_duration.id);
                db.debt_installment_durations.DeleteOnSubmit(prev_debt_installment_duration);
                return debt_installment_duration;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<WgbSalesUpload.debt_installment_duration> getAllDebtType()
        {
            return db.debt_installment_durations.ToList<debt_installment_duration>();
        }

        public List<WgbSalesUpload.debt_installment_duration> getAllDebtDuration()
        {
            return db.debt_installment_durations.ToList<debt_installment_duration>();
        }
    }
}