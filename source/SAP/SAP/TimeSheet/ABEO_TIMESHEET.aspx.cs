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
    public partial class ABEO_TIMESHEET : System.Web.UI.Page
    {
        public static DataTable dtContents;
        public static DataTable dtHeader;
        private GeneralFunctions GF;
        private string DocType = "33";
        private String clgCode="";
        void LoadData(int clgcode)
        {
            txtNo.Text = clgcode.ToString();
           ddlActivity.SelectedValue= dtHeader.Rows[0]["Action"].ToString();
           ddlType.SelectedValue= dtHeader.Rows[0]["CntctType"].ToString();
           txtSubject.Text= dtHeader.Rows[0]["CntctSbjct"].ToString();
           txtBP.Text= dtHeader.Rows[0]["CardCode"].ToString();
           txtBPName.Text = "";
           txtRemark.Text = dtHeader.Rows[0]["Notes"].ToString();
           txtSubject.Text = dtHeader.Rows[0]["Details"].ToString(); 
           txtDate.Text= dtHeader.Rows[0]["Recontact"].ToString();
           txtFromTime.Text= dtHeader.Rows[0]["BeginTime"].ToString();
           txtToTime.Text= dtHeader.Rows[0]["ENDTime"].ToString();
           if (dtHeader.Rows[0]["Closed"].ToString() == "Y")
           {
               cbClosed.Checked = true;
               imgAdd.Visible = false;
           }
           else
               cbClosed.Checked = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //-------------create table------------------
                dtHeader = new DataTable();
                dtHeader.Columns.Add("ClgCode");
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
                dtHeader.Columns.Add("Closed");
                dtHeader.Columns.Add("U_UserID");

                
                //------------------load activity--------------------
                MasterData masterDataWS = new MasterData();
                DataSet dsMaster = masterDataWS.GetActivityType(User.Identity.Name);

                ddlType.DataSource = dsMaster.Tables[0];
                ddlType.DataTextField = "name";
                ddlType.DataValueField = "code";
                ddlType.DataBind();


                clgCode = Request.QueryString["clgCode"];
                if (!String.IsNullOrEmpty(clgCode))
                {
                    Transaction trx = new Transaction();
                    dtHeader=trx.GetMarketingDocument_ReturnDS("33",Int32.Parse(clgCode),User.Identity.Name).Tables[0];
                    LoadData(Int32.Parse(clgCode));
                }
                else
                {
                    LoadDefault();
                    dtHeader.Rows.Add("","", "", "", "", "", "", "20121022", "830", "20121022", "930", "N", User.Identity.Name);
                }

                dsMaster = masterDataWS.GetActivitySubject(User.Identity.Name, Int32.Parse(ddlType.SelectedValue.ToString()));
                ddlSubject.DataSource = dsMaster.Tables[0];
                ddlSubject.DataTextField = "name";
                ddlSubject.DataValueField = "code";
                ddlSubject.DataBind();
                
            }
            
        }

        void LoadDefault()
        {
            txtBP.Text = "ABEO";
            txtBPName.Text = "ABEO";
            ddlActivity.SelectedValue = "T";
            ddlType.Items.FindByText("TIMESHEET").Selected = true;
            txtDate.Text= DateTime.Now.ToShortDateString();
            txtFromTime.Text = "830";
            txtToTime.Text = "1730";
            txtSubject.Text = "TIMESHEET | " + User.Identity.Name + " | " + String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDate.Text)) + " | " + txtBP.Text  ;
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
            txtSubject.Text = "";
            txtRemark.Text = "";
            txtNo.Text = "";
            clgCode = "";
            dtHeader.Clear();
            imgAdd.Visible = true;
            LoadDefault();
        }
        public String _collectData()
        {
            try
            {
                if (GF == null) GF = new GeneralFunctions(User.Identity.Name);
                //Update table header
                txtSubject.Text = "TIMESHEET | " + User.Identity.Name + " | " + String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDate.Text)) + " | " + txtBP.Text;

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

                if (cbClosed.Checked == true)
                { dr["Closed"] = "Y"; }
                DocumentXML objInfo = new DocumentXML();
                String RemoveColumn = "";

                if (!String.IsNullOrEmpty(clgCode))
                {
                    RemoveColumn = "";
                    dr["ClgCode"] = clgCode;
                }
                else
                    RemoveColumn = "ClgCode";

                return objInfo.ToXMLStringFromDS(DocType, dtHeader, dtContents, RemoveColumn);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void imgAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (txtRemark.Text == "") 
            {
                Session["errorMessage"] = "Detail information can't be blank";
                Session["requestXML"] = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                    "Main.setMasterMessage('Detail information can not be blank','');", true);
            }
            String requestXML = _collectData();
            SAP.WebServices.Transaction ts = new WebServices.Transaction();
            DataSet ds = ts.CreateMarketingDocument(requestXML, User.Identity.Name, DocType);
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
            DataSet dsMaster  = masterDataWS.GetActivitySubject(User.Identity.Name,Int32.Parse(ddlType.SelectedValue.ToString()));
            foreach (DataRow row in dsMaster.Tables[0].Rows)
            {
                item = new ListItem(row[1].ToString(), row[0].ToString());
                ddlSubject.Items.Add(item);
            }
        }

        protected void btnAddNew_Click(object sender, ImageClickEventArgs e)
        {
            ClearScreen();
        }

    }
}