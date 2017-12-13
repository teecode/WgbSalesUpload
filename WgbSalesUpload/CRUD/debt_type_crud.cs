using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class debt_type_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public debt_type debt_type_;
        public debt_type debt_type
        {
            get { return debt_type_; }
            set { debt_type_ = value; }
        }

        public debt_type_crud(debt_type debt_type_)
        {
            this.debt_type_ = debt_type_;
        }

        public debt_type_crud()
        {
            // TODO: Complete member initialization
        }

        public debt_type find_debt_type_By_ID(int id)
        {
            try
            {
                debt_type = db.debt_types.SingleOrDefault(a => a.id == id);
                return debt_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt_type find_debt_type_By_AppId(string Appid)
        {
            try
            {
                debt_type = db.debt_types.SingleOrDefault(a => a.app_id == Appid);
                return debt_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt_type find_debt_type_By_code(string code)
        {
            try
            {
                debt_type tran = db.debt_types.SingleOrDefault(a => a.code == code);
                return tran;
            }
            catch (Exception)
            {
                return null;
            }
        }

        

        
        public debt_type insert_debt_type()
        {
            try
            {
                db.debt_types.InsertOnSubmit(debt_type);
                db.SubmitChanges();
                return find_debt_type_By_AppId(debt_type.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt_type alter_debt_type()
        {
            try
            {
                debt_type prev_debt_type = db.debt_types.SingleOrDefault(a => a.id == debt_type.id);
                prev_debt_type.name = debt_type.name;
                prev_debt_type.code = debt_type.code;
                prev_debt_type.amount = debt_type.amount;
                prev_debt_type.installment = debt_type.installment;
                prev_debt_type.installment_amount = debt_type.installment_amount;
                prev_debt_type.intallment_duration = debt_type.intallment_duration;
                db.SubmitChanges();
                return debt_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public debt_type delete_debt_type()
        {
            try
            {
                debt_type prev_debt_type = db.debt_types.SingleOrDefault(a => a.id == debt_type.id);
                db.debt_types.DeleteOnSubmit(prev_debt_type);
                return debt_type;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<WgbSalesUpload.debt_type> getAllDebtType()
        {
            return db.debt_types.ToList<debt_type>();
        }
    }
}