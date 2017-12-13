using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WgbSalesUpload.Utilities
{
    public class DebtRenderer
    {
        private debt debt_;
        private string name_;
        private int id_;


        public string name
        {
            get { return name_; }
            set { name_ = value; }
        }

        public debt debt
        {
            get { return debt_; }
            set { debt_ = value; }
        }

        public int id
        {
            get { return id_; }
            set { id_ = value; }
        }
        public DebtRenderer(debt debt)
        {
            this.id = debt.id;
            this.name = (debt.debt_type1 != null) ? debt.debt_type1.name : "UNKNOWN";
            this.debt_ = debt;

        }
    }
}