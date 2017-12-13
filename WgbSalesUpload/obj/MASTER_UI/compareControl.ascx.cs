using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WgbSalesUpload.MASTER_UI
{
    public partial class compareControl : System.Web.UI.UserControl
    {
      
        private Color _color = Color.SkyBlue;
        private Button _toggleButton;

        public Button toggleButton1
        {
            get { return _toggleButton; }
            set { _toggleButton = value; }
        }
        public Color MainpanelColor
        {
            get {return  _color;}
            set { _color = value; }
        }

        public string shopcode
        {
            get { return ShopCodeLabel.Text; }
            set { ShopCodeLabel.Text = value;}
        }

        public string masterbalance
        {
            get { return MasterBalanceLabel.Text; }
            set { MasterBalanceLabel.Text = value; }
        }

        public string shopbalance
        {
            get { return ShopBalanceLabel.Text; }
            set { ShopBalanceLabel.Text = value; }
        }

        public string  deficitbalance
        {
            get { return DeficitLabel.Text; }
            set { DeficitLabel.Text = value; }
        }

        public string summarizedmastersales
        {
            get {return summarymastersales.Text;}
            set {summarymastersales.Text = value; }
        }

        public string summarizedmasterwinnings
        {
            get { return summarymasterwinnings.Text; }
            set { summarymasterwinnings.Text = value; }
        }

        public string summarizedmastercancelled
        {
            get { return summarymastercancelled.Text; }
            set { summarymastercancelled.Text = value; }
        }

        public string summarizedmastercommision
        {
            get { return summarymastercommision.Text; }
            set { summarymastercommision.Text = value; }
        }
        
        public string summarizedmasternetbal
        {
            get { return summarymasternetbal.Text; }
            set { summarymasternetbal.Text = value; }
        }

        public string summarizedmasterbalance
        {
            get { return summarymasterBalance.Text; }
            set { summarymasterBalance.Text = value; }
        }

        public string summarizedshopsales
        {
            get { return summaryshopsales.Text; }
            set { summaryshopsales.Text = value; }
        }

        public string summarizedshopwinnings
        {
            get { return summaryshopwinnings.Text; }
            set { summaryshopwinnings.Text = value; }
        }

        public string summarizedshopcancelled
        {
            get { return summaryshopcancelled.Text; }
            set { summaryshopcancelled.Text = value; }
        }

        public string summarizedshopcommision
        {
            get { return summaryshopcommision.Text; }
            set { summaryshopcommision.Text = value; }
        }

        public string summarizedshopnetbal
        {
            get { return summaryshopnetbal.Text; }
            set { summaryshopnetbal.Text = value; }
        }

        public string summarizedshopbalance
        {
            get { return summaryshopBalance.Text; }
            set { summaryshopBalance.Text = value; }
        }

        public string summarizeddeficitsales
        {
            get { return summarydeficitsales.Text; }
            set { summarydeficitsales.Text = value; }
        }

        public string summarizeddeficitwinnings
        {
            get { return summarydeficitwinnings.Text; }
            set { summarydeficitwinnings.Text = value; }
        }

        public string summarizeddeficitcancelled
        {
            get { return summarydeficitcancelled.Text; }
            set { summarydeficitcancelled.Text = value; }
        }

        public string summarizeddeficitcommision
        {
            get { return summarydeficitcommision.Text; }
            set { summarydeficitcommision.Text = value; }
        }

        public string summarizeddeficitnetbal
        {
            get { return summarydeficitnetbal.Text; }
            set { summarydeficitnetbal.Text = value; }
        }

        public string summarizeddeficitbalance
        {
            get { return summarydeficitBalance.Text; }
            set { summarydeficitBalance.Text = value; }
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
              mainpanel.BackColor = _color;
                summarydeficitcancelled.Text = summarizeddeficitcancelled;
                summarydeficitwinnings.Text = summarizeddeficitwinnings;
                summarydeficitsales.Text = summarizeddeficitsales;
                summarydeficitBalance.Text = summarizeddeficitbalance;
                summarydeficitcommision.Text = summarizeddeficitcommision;
                summarydeficitnetbal.Text = summarizeddeficitnetbal;

                summaryshopcancelled.Text = summarizedshopcancelled;
                summaryshopwinnings.Text = summarizedshopwinnings;
                summaryshopsales.Text = summarizedshopsales;
                summaryshopBalance.Text = summarizedshopbalance;
                summaryshopcommision.Text = summarizedshopcommision;
                summaryshopnetbal.Text = summarizedshopnetbal;

                summarymastercancelled.Text = summarizedmastercancelled;
                summarymasterwinnings.Text = summarizedmasterwinnings;
                summarymastersales.Text = summarizedmastersales;
                summarymasterBalance.Text = summarizedmasterbalance;
                summarymastercommision.Text = summarizedmastercommision;
                summarymasternetbal.Text = summarizedmasternetbal;
            }
        

     
    }
}