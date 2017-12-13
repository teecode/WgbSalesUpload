using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.CRUD
{
    public class state_list_crud
    {
        BettingSuiteDataContext db = new BettingSuiteDataContext();
        public state_list state_list_;
        public state_list state_list
        {
            get { return state_list_; }
            set { state_list_ = value; }
        }

        public state_list_crud(state_list state_list_)
        {
            this.state_list_ = state_list_;
        }

        public state_list_crud()
        {
            // TODO: Complete member initialization
        }

        public state_list find_state_list_By_ID(int id)
        {
            try
            {
                state_list = db.state_lists.SingleOrDefault(a => a.id == id);
                return state_list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        

        
        public state_list insert_state_list()
        {
            try
            {
                db.state_lists.InsertOnSubmit(state_list);
                db.SubmitChanges();
                return state_list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public state_list alter_state_list()
        {
            try
            {
                state_list prev_state_list = db.state_lists.SingleOrDefault(a => a.id == state_list.id);

                prev_state_list.state = state_list.state;
                
                

                db.SubmitChanges();
                return state_list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public state_list delete_state_list()
        {
            try
            {
                state_list prev_state_list = db.state_lists.SingleOrDefault(a => a.id == state_list.id);
                db.state_lists.DeleteOnSubmit(prev_state_list);
                return state_list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        

        public List<WgbSalesUpload.state_list> getAllStates()
        {
            return db.state_lists.ToList<state_list>();
        }

        internal List<WgbSalesUpload.state_list> getAllStatesinList(string state)
        {
            string[] stateints = state.Split(';');
            int[] stint = new int[stateints.Length];
            int count = 0;
            foreach (string s in stateints)
            {
                stint[count] = int.Parse(stateints[count]);
                count++;
            }

            return db.state_lists.Where(a => stint.Contains(a.id)).ToList<state_list>();
        }
    }
}