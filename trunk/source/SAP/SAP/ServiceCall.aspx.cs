using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAP.WebServices;
using System.Collections;
using System.Net;

namespace SAP
{
    public partial class ServiceCall : System.Web.UI.Page
    {
        public static DataTable dtContents;
        public static DataTable dtHeader;
        private GeneralFunctions GF;
        private string DocType = "191";

       


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtHeader = new DataTable();
                dtHeader.Columns.Add("customer");
                dtHeader.Columns.Add("custmrName");
                dtHeader.Columns.Add("itemCode");
                dtHeader.Columns.Add("itemName");
                dtHeader.Columns.Add("subject");
                dtHeader.Columns.Add("descrption");
                dtHeader.Columns.Add("U_UserID");

                dtHeader.Rows.Add("", "", "", "", "","", User.Identity.Name);
            }
            
        }

        void LoadDefault()
        {
            txtBP.Text = "C00001";
            txtBPName.Text = "DNTN SXTM Trường Hải";
            txtItemNo.Text = "ABD-22002395";
            txtItemName.Text = "Acrobe 9 IE Win DVD Set";
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            
            if (this.Request["__EVENTARGUMENT"] != null && this.Request["__EVENTARGUMENT"].ToString() != "")
            {
                //Int32 itemNo = 0;
                if (GF == null) GF = new GeneralFunctions(User.Identity.Name);

                switch (this.Request["__EVENTARGUMENT"].ToString())
                {
                    case "EditItemCallBack":
                        ItemMaster chosenItem = Session["chosenItem"] as ItemMaster;
                       // itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                        if (chosenItem != null)
                        {   // update grid
                            txtItemNo.Text = chosenItem.ItemCode;
                            txtItemName.Text = chosenItem.ItemName;
                        }
                        break;
                   
                    case "EditCustomerCallBack":
                        BusinessPartner chosenPartner = Session["chosenPartner"] as BusinessPartner;
                        if (chosenPartner != null)
                        {
                            this.txtBPName.Text = chosenPartner.CardName;
                            this.txtBP.Text = chosenPartner.CardCode;

                            MasterData masterDataWS = new MasterData();
                            DataSet contactPersons = masterDataWS.GetContactPerson(chosenPartner.CardCode, User.Identity.Name);
                            ListItem item = new ListItem();
                            foreach (DataRow row in contactPersons.Tables[0].Rows)
                            {
                                String name = row[1].ToString() + " " + row[2].ToString();
                                item = new ListItem(name, row[1].ToString());
                                if ("Y".Equals(row[0].ToString()))
                                    item.Selected = true;
                                ddlContactPerson.Items.Add(item);
                            }
                        }
                        break;
                    
                    default:
                        break;
                }
            }

        }

        void ClearScreen()
        {
            LoadDefault();
            txtSubject.Text = "";
            txtRemark.Text = "";
        }
        public String _collectData()
        {
            try
            {
                if (GF == null) GF = new GeneralFunctions(User.Identity.Name);
                //Update table header
                DataRow dr = dtHeader.Rows[0];
                dr["customer"] = txtBP.Text;
                dr["custmrName"] = txtBPName.Text;
                dr["itemCode"] =  txtItemNo.Text;
                dr["itemName"] =  txtItemName.Text;
                dr["subject"] = txtSubject.Text;
                dr["descrption"] = txtRemark.Text;

                DocumentXML objInfo = new DocumentXML();
                String RemoveColumn = "No";
                return objInfo.ToXMLStringFromDS(DocType, dtHeader, dtContents, RemoveColumn);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void imgAdd_Click(object sender, ImageClickEventArgs e)
        {
            String simulate = System.Configuration.ConfigurationManager.AppSettings["Simulate"];
            String requestXML = _collectData();
            SAP.WebServices.Transaction ts = new WebServices.Transaction();
            DataSet ds = ts.CreateMarketingDocument(requestXML, User.Identity.Name, DocType,"",false);
            if ((int)ds.Tables[0].Rows[0]["ErrCode"] != 0)
            {
                Session["errorMessage"] = ds.Tables[0].Rows[0]["ErrMsg"];
                Session["requestXML"] = requestXML;
                String errorMessage = GeneralFunctions.UrlFullEncode(ds.Tables[0].Rows[0]["ErrMsg"].ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                    "Main.setMasterMessage('" + errorMessage + "','');", true);
            }
            else
            {
                Session["successMessage"] = "Operation complete sucessful!";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                   "Main.setMasterMessage('" + "Operation complete sucessful!" + "','');", true);

                ClearScreen();
            }
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                              "Dialog.hideLoader();", true);
        }
    }
}