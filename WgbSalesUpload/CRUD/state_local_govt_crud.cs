using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class state_local_govt_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public state_local_govt state_local_govt_;
        public state_local_govt state_local_govt
        {
            get { return state_local_govt_; }
            set { state_local_govt_ = value; }
        }

        public state_local_govt_crud(state_local_govt state_local_govt_)
        {
            this.state_local_govt_ = state_local_govt_;
        }

        public state_local_govt find_state_local_govt_By_ID(int id)
        {
            try
            {
                state_local_govt = db.state_local_govts.SingleOrDefault(a => a.id == id);
                return state_local_govt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        

        
        public state_local_govt insert_state_local_govt()
        {
            try
            {
                db.state_local_govts.InsertOnSubmit(state_local_govt);
                db.SubmitChanges();
                return state_local_govt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public state_local_govt alter_state_local_govt()
        {
            try
            {
                state_local_govt prev_state_local_govt = db.state_local_govts.SingleOrDefault(a => a.id == state_local_govt.id);

                prev_state_local_govt.state = state_local_govt.state;
                
                

                db.SubmitChanges();
                return state_local_govt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public state_local_govt delete_state_local_govt()
        {
            try
            {
                state_local_govt prev_state_local_govt = db.state_local_govts.SingleOrDefault(a => a.id == state_local_govt.id);
                db.state_local_govts.DeleteOnSubmit(prev_state_local_govt);
                return state_local_govt;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}