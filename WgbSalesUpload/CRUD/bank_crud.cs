using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class bank_crud
    {
        public bank bank_;
        public bank bank
        {
            get { return bank_; }
            set { bank_ = value; }
        }

        public bank_crud(bank bank_)
        {
            this.bank_ = bank_;
        }

        public bank_crud()
        {
            // TODO: Complete member initialization
        }

        public bank find_bank_By_ID(int id)
        {
            try
            {
                bank = db.banks.SingleOrDefault(a => a.id == id);
                return bank;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bank find_bank_By_AppId(string Appid)
        {
            try
            {
                bank = db.banks.SingleOrDefault(a => a.app_id == Appid);
                return bank;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public bank insert_bank()
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
                    bank.app_id = app.app_id;
                    db.banks.InsertOnSubmit(bank);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_bank_By_AppId(bank.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bank alter_bank()
        {
            try
            {
                bank prev_bank = db.banks.SingleOrDefault(a => a.id == bank.id);

                prev_bank.account_no = bank.account_no;
                prev_bank.code = bank.code;
                prev_bank.description = bank.description;
                prev_bank.name = bank.name;
                

                db.SubmitChanges();
                return bank;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bank delete_bank()
        {
            try
            {
                bank prev_bank = db.banks.SingleOrDefault(a => a.id == bank.id);
                db.banks.DeleteOnSubmit(prev_bank);
                return bank;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<WgbSalesUpload.bank> getAllBanks()
        {
            return db.banks.ToList<bank>();
        }
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        
    }
}