using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAP.WebServices;
using System.Collections;

namespace SAP
{
    public partial class Activity : System.Web.UI.Page
    {
        public static DataTable dtContents;
        public static DataTable dtHeader;
        private GeneralFunctions GF;
        private string DocType = "33";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtHeader = new DataTable();
                dtHeader.Columns.Add("Action");
                dtHeader.Columns.Add("CntctType");
                dtHeader.Columns.Add("CntctSbjct");
                dtHeader.Columns.Add("CardCode");
                dtHeader.Columns.Add("Notes");
                dtHeader.Columns.Add("Details");

                dtHeader.Columns.Add("Recontact");
                dtHeader.Columns.Add("BeginTime");
                dtHeader.Columns.Add("endDate");
                dtHeader.Columns.Add("ENDTime");

                dtHeader.Columns.Add("U_UserID");
                dtHeader.Rows.Add("", "", "", "", "","","20121022","830","20121022","930", User.Identity.Name);

                MasterData masterDataWS = new MasterData();
                DataSet dsMaster = masterDataWS.GetActivityType(User.Identity.Name);
                ListItem item = new ListItem();

                ddlType.DataSource = dsMaster.Tables[0];
                ddlType.DataTextField = "name";
                ddlType.DataValueField = "code";
                ddlType.DataBind();

                dsMaster = masterDataWS.GetActivitySubject(User.Identity.Name, Int32.Parse(ddlType.SelectedValue.ToString()));
                ddlSubject.DataSource = dsMaster.Tables[0];
                ddlSubject.DataTextField = "name";
                ddlSubject.DataValueField = "code";
                ddlSubject.DataBind();

                LoadDefault();

            }
            
        }

        void LoadDefault()
        {
            //txtBP.Text = "ABEO";
            //txtBPName.Text = "ABEO";
            //ddlActivity.SelectedValue = "T";
           // ddlType.Items.FindByText("TIMESHEET").Selected = true;
            txtDate.Text= DateTime.Now.ToShortDateString();
            txtFromTime.Text = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
            txtToTime.Text = txtFromTime.Text;
           // txtSubject.Text = "TIMESHEET | " + User.Identity.Name + " | " + String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDate.Text)) + " | " + txtBP.Text  ;
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
                    case "EditVendorCallBack":
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
               // txtSubject.Text = "TIMESHEET | " + User.Identity.Name + " | " + String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDate.Text)) + " | " + txtBP.Text;
                txtToTime.Text = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
                DataRow dr = dtHeader.Rows[0];
                dr["Action"] = ddlActivity.SelectedValue.ToString();
                dr["CntctType"] = ddlType.SelectedValue.ToString();
                dr["CntctSbjct"] = ddlSubject.SelectedValue.ToString();
                dr["CardCode"] = txtBP.Text;
                dr["Notes"] = txtRemark.Text;
                dr["Details"] = txtSubject.Text;

                dr["Recontact"] = String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDate.Text));
                dr["BeginTime"] = txtFromTime.Text;
                dr["endDate"] = String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDate.Text));
                dr["ENDTime"] = txtToTime.Text;

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
            if (txtBP.Text == "")
            {
                Session["errorMessage"] = "Missing Business Partner";
                Session["requestXML"] = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                    "Main.setMasterMessage('Missing Business Partner','');", true);
                //return;
            }

            String requestXML = _collectData();
            SAP.WebServices.Transaction ts = new WebServices.Transaction();
            DataSet ds = ts.CreateMarketingDocument(requestXML, User.Identity.Name, DocType, "", false);
            if ((int)ds.Tables[0].Rows[0]["ErrCode"] != 0)
            {
                Session["errorMessage"] = ds.Tables[0].Rows[0]["ErrMsg"];
                Session["requestXML"] = requestXML;
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                    "Main.setMasterMessage('" + ds.Tables[0].Rows[0]["ErrMsg"] + "','');", true);
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

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterData masterDataWS = new MasterData();
            ListItem item = new ListItem();
            DataSet dsMaster = masterDataWS.GetActivitySubject(User.Identity.Name, Int32.Parse(ddlType.SelectedValue.ToString()));
            ddlSubject.DataSource = dsMaster.Tables[0];
            ddlSubject.DataTextField = "name";
            ddlSubject.DataValueField = "code";
            ddlSubject.DataBind();
        }

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSubject.Text = ddlSubject.SelectedItem.Text;
        }

    }
}