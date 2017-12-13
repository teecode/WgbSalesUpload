using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class bank_list_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public bank_list bank_list_;
        public bank_list bank_list
        {
            get { return bank_list_; }
            set { bank_list_ = value; }
        }

        public bank_list_crud()
        {
        }


        public bank_list_crud(bank_list bank_list_)
        {
            this.bank_list_ = bank_list_;
        }

        public bank_list find_bank_list_By_ID(int id)
        {
            try
            {
                bank_list = db.bank_lists.SingleOrDefault(a => a.id == id);
                return bank_list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<bank_list> getallbanks()
        {
            return db.bank_lists.OrderBy(a => a.name).ToList<bank_list>();
        }

        public bank_list find_bank_list_By_AppId(string Appid)
        {
            try
            {
                bank_list = db.bank_lists.SingleOrDefault(a => a.app_id == Appid);
                return bank_list;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public bank_list insert_bank_list()
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
                    bank_list.app_id = app.app_id;
                    db.bank_lists.InsertOnSubmit(bank_list);
                    db.SubmitChanges();

                    scope.Complete();


                }


                return find_bank_list_By_AppId(bank_list.app_id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bank_list alter_bank_list()
        {
            try
            {
                bank_list prev_bank_list = db.bank_lists.SingleOrDefault(a => a.id == bank_list.id);

                prev_bank_list.code = bank_list.code;
                prev_bank_list.name = bank_list.name;
               
                

                db.SubmitChanges();
                return bank_list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bank_list delete_bank_list()
        {
            try
            {
                bank_list prev_bank_list = db.bank_lists.SingleOrDefault(a => a.id == bank_list.id);
                db.bank_lists.DeleteOnSubmit(prev_bank_list);
                return bank_list;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}