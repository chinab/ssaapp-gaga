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
using System.Globalization;

namespace SAP
{
    public partial class Activity : System.Web.UI.Page
    {
        public static DataTable dtHeader;
        private GeneralFunctions GF = new GeneralFunctions(HttpContext.Current.User.Identity.Name);
        private string DocType = "33";
        private string TblHeaderName = "OCLG";
        private string TblLineName = "RPC1";
        public static string KeepColums = "";

        #region "Functions"
        private void LoadData(string ClgCode)
        {
            Transaction trx = new Transaction();
            dtHeader = trx.GetMarketingDocument_ReturnDS(DocType, ClgCode, User.Identity.Name).Tables[0];

            if (dtHeader.Rows.Count == 0)
                ClgCode = "1";
            dtHeader = trx.GetMarketingDocument_ReturnDS(DocType, ClgCode, User.Identity.Name).Tables[0];
            dtHeader = GF.ConvertDataTable_RemoveCols(dtHeader, KeepColums);

            txtNo.Text = dtHeader.Rows[0]["ClgCode"].ToString();
            SetNavigatorURL(txtNo.Text);

            ddlActivity.SelectedValue = dtHeader.Rows[0]["Action"].ToString();
            ddlType.SelectedValue = dtHeader.Rows[0]["CntctType"].ToString();

            ddlSubject.DataSource = null;
            MasterData masterDataWS = new MasterData();
            ddlSubject.DataSource =
                masterDataWS.GetActivitySubject(User.Identity.Name, Int32.Parse(ddlType.SelectedValue.ToString())).
                    Tables[0];
            ddlSubject.DataTextField = "name";
            ddlSubject.DataValueField = "code";
            ddlSubject.DataBind();

            ddlSubject.Text = dtHeader.Rows[0]["CntctSbjct"].ToString();
            txtBP.Text = dtHeader.Rows[0]["CardCode"].ToString();
                
            txtBPName.Text = trx.GetMarketingDocument_ReturnDS("2",txtBP.Text,User.Identity.Name).Tables[0].Rows[0]["CardName"].ToString();;
            lBP.NavigateUrl = "../BusinessPartner/BusinessPartnerMaster.aspx?cardcode=" + txtBP.Text;

            ddlContactPerson.DataSource = masterDataWS.GetContactPerson(txtBP.Text, User.Identity.Name).Tables[0];
            ddlContactPerson.DataValueField = "Code";
            ddlContactPerson.DataTextField = "FirstName";
            ddlContactPerson.DataBind();

            ddlContactPerson.SelectedValue = dtHeader.Rows[0]["CntctCode"].ToString();

            txtRemark.Text = HttpUtility.HtmlDecode(dtHeader.Rows[0]["Notes"].ToString());
            txtSubject.Text = dtHeader.Rows[0]["Details"].ToString();
            DateTime d = DateTime.ParseExact(dtHeader.Rows[0]["Recontact"].ToString(), "yyyyMMdd",
                                             CultureInfo.InvariantCulture, DateTimeStyles.None);
            txtFromDate.Text = String.Format("{0:MM/dd/yyyy}", d);

            txtFromTime.Text = dtHeader.Rows[0]["BeginTime"].ToString();
            txtToTime.Text = dtHeader.Rows[0]["ENDTime"].ToString();

            imgAdd.Visible = false;

            if (dtHeader.Rows[0]["Closed"].ToString() == "Y")
            {
                cbClosed.Checked = true;
                imgUpdate.Visible = false;
            }
            else
            {
                cbClosed.Checked = false;
                imgUpdate.Visible = true;
            }
        }
        void LoadDefault()
        {
            txtFromDate.Text = DateTime.Now.ToShortDateString();
            txtToDate.Text = DateTime.Now.ToShortDateString();
            txtFromTime.Text = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString();
            if (DateTime.Now.Minute + 15>60)
                txtToTime.Text = (DateTime.Now.Hour+1).ToString() + (60-DateTime.Now.Minute - 15).ToString();
            else
                txtToTime.Text = DateTime.Now.Hour.ToString() + (DateTime.Now.Minute+15).ToString();
            
        }
        void ClearScreen()
        {
            //-------------create table------------------
            imgAdd.Visible = true;
            imgUpdate.Visible = false;
            dtHeader = new DataTable("OCLG");
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
            dtHeader.Columns.Add("AtcEntry");
            dtHeader.Columns.Add("CntctCode");
            dtHeader.Columns.Add("AttendUser");
            dtHeader.Columns.Add("U_UserID");
            KeepColums = GF.BuildKeepColumnStr(dtHeader);

            txtSubject.Text = "";
            txtRemark.Text = "";
            txtNo.Text = "";
            imgAdd.Visible = true;

            dtHeader.Rows.Add();

            
            SetNavigatorURL("");
        }
        public String _collectData()
        {
            try
            {
                CultureInfo ivC = new System.Globalization.CultureInfo("es-US");

                DataRow dr = dtHeader.Rows[0];
                dr["Action"] = ddlActivity.SelectedValue.ToString();
                dr["CntctType"] = ddlType.SelectedValue.ToString();
                dr["CntctSbjct"] = ddlSubject.SelectedValue.ToString();
                dr["CardCode"] = txtBP.Text;
                dr["Notes"] = HttpUtility.HtmlAttributeEncode(txtRemark.Text);
                dr["Details"] = txtSubject.Text;

                dr["Recontact"] = Convert.ToDateTime(txtFromDate.Text, ivC).ToString("yyyyMMdd");
                dr["BeginTime"] = txtFromTime.Text;
                dr["endDate"] = Convert.ToDateTime(txtFromDate.Text, ivC).ToString("yyyyMMdd");
                dr["ENDTime"] = txtToTime.Text;
                if (ddlContactPerson.SelectedItem!=null)
                    dr["cntctcode"] = ddlContactPerson.SelectedItem.Value.ToString();
                dr["AttendUser"] = ddlUser.SelectedItem.Value.ToString();
                dr["U_UserID"] = User.Identity.Name;
                if (cbClosed.Checked == true)
                {
                    dr["Closed"] = "Y";
                }
                DocumentXML objInfo = new DocumentXML();
                DataSet ds = new DataSet("DS");
                dtHeader.TableName = TblHeaderName;
                ds.Tables.Add(dtHeader.Copy());

                return objInfo.ToXMLStringFromDS(DocType, ds);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                    "Main.setMasterMessage('" + GeneralFunctions.UrlFullEncode(ex.ToString()) + "','');", true);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                    "Dialog.hideLoader();", true);
                return "";
            }
        }
        protected void SetNavigatorURL(string CurrentKey)
        {
            GetDefault df = new GetDefault();
            DataSet nav = df.GetNextPreviousID(DocType, User.Identity.Name, "OCLG", "clgcode", CurrentKey);
            if (nav != null)
            {
                this.linkFirst.NavigateUrl = "/BusinessPartner/Activity.aspx?clgCode=" + nav.Tables[0].Rows[0]["Fir"].ToString();
                this.linkNext.NavigateUrl = "/BusinessPartner/Activity.aspx?clgCode=" + nav.Tables[0].Rows[0]["Nex"].ToString();
                this.linkPrevious.NavigateUrl = "/BusinessPartner/Activity.aspx?clgCode=" + nav.Tables[0].Rows[0]["Pre"].ToString();
                this.linkLast.NavigateUrl = "/BusinessPartner/Activity.aspx?clgCode=" + nav.Tables[0].Rows[0]["Las"].ToString();
            }
            this.linkNew.NavigateUrl = "/BusinessPartner/Activity.aspx?clgCode=";
        }
        #endregion

        #region "Event"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearScreen();
                try
                {
                    //------------------load activity--------------------
                    MasterData masterDataWS = new MasterData();
                    DataSet dsMaster = masterDataWS.GetActivityType(User.Identity.Name);

                    ddlType.DataSource = dsMaster.Tables[0];
                    ddlType.DataTextField = "name";
                    ddlType.DataValueField = "code";
                    ddlType.DataBind();

                    dsMaster = masterDataWS.GetSAPUser(User.Identity.Name);
                    ddlUser.DataSource = dsMaster.Tables[0];
                    ddlUser.DataTextField = "User_Code";
                    ddlUser.DataValueField = "UserID";
                    ddlUser.DataBind();

                    dsMaster = masterDataWS.GetActivitySubject(User.Identity.Name, Int32.Parse(ddlType.SelectedValue.ToString()));
                    ddlSubject.DataSource = dsMaster.Tables[0];
                    ddlSubject.DataTextField = "name";
                    ddlSubject.DataValueField = "code";
                    ddlSubject.DataBind();

                    string clgCode = Request.QueryString["clgCode"];
                    if (!String.IsNullOrEmpty(clgCode))
                    {
                        LoadData(clgCode);
                    }
                    else
                    {
                        LoadDefault();
                        string cardcode = Request.QueryString["CardCode"];

                        if (!String.IsNullOrEmpty(cardcode))
                        {
                            string cardname = Request.QueryString["CardName"];
                            txtBP.Text = cardcode;
                            txtBPName.Text = cardname;

                            ddlContactPerson.DataSource = masterDataWS.GetContactPerson(txtBP.Text, User.Identity.Name).Tables[0];
                            ddlContactPerson.DataValueField = "Code";
                            ddlContactPerson.DataTextField = "FirstName";
                            ddlContactPerson.DataBind();
                        }
                        //dtHeader.Rows.Add("","", "", "", "", "", "", "20121022", "830", "20121022", "930", "N", User.Identity.Name);
                    }
                }
                catch (Exception ex)
                {
                    Session["errorMessage"] = ex.ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('" + GeneralFunctions.UrlFullEncode(ex.ToString()) + "','');", true);

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                        "Dialog.hideLoader();", true);
                }
            }

        }
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);

            if (this.Request["__EVENTARGUMENT"] != null && this.Request["__EVENTARGUMENT"].ToString() != "")
            {
                //Int32 itemNo = 0;

                switch (this.Request["__EVENTARGUMENT"].ToString())
                {
                    case "EditBusinessPartnerCallBack":
                        BusinessPartner chosenPartner = Session["chosenPartner"] as BusinessPartner;
                        if (chosenPartner != null)
                        {
                            this.txtBPName.Text = chosenPartner.CardName;
                            this.txtBP.Text = chosenPartner.CardCode;
                            lBP.NavigateUrl = "../BusinessPartner/BusinessPartnerMaster.aspx?cardcode=" + txtBP.Text;
                            MasterData masterDataWS = new MasterData();
                            ddlContactPerson.DataSource = masterDataWS.GetContactPerson(txtBP.Text, User.Identity.Name).Tables[0];
                            ddlContactPerson.DataValueField = "Code";
                            ddlContactPerson.DataTextField = "FirstName";
                            ddlContactPerson.DataBind();
                        }
                        else
                        {
                            lBP.NavigateUrl = "";
                        }
                        break;

                    default:
                        break;
                }
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
                    "Main.setMasterMessage('" + GeneralFunctions.UrlFullEncode(ds.Tables[0].Rows[0]["ErrMsg"].ToString()) + "','');", true);

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
        protected void ddlAssignTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterData masterDataWS = new MasterData();
            DataSet dsMaster;
            if (ddlAssignTo.SelectedItem.Value == "C")
            {
                dsMaster = masterDataWS.GetSAPUser(User.Identity.Name);
                ddlUser.DataSource = dsMaster.Tables[0];
                ddlUser.DataTextField = "User_Code";
                ddlUser.DataValueField = "UserID";
                ddlUser.DataBind();
            }
            else
            {
                dsMaster = masterDataWS.GetEmployeeMasterData(User.Identity.Name);
                ddlUser.DataSource = dsMaster.Tables[0];
                ddlUser.DataTextField = "LastName";
                ddlUser.DataValueField = "Code";
                ddlUser.DataBind();
            }
        }
        #endregion
    }
}