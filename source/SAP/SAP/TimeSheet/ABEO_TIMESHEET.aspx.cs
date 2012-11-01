using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SAP.WebServices;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Net;

namespace SAP
{
    public partial class ABEO_TIMESHEET : System.Web.UI.Page
    {
        public static DataTable dtContents;
        public static DataTable dtHeader;
        public static DataTable dtAttachment;

        private GeneralFunctions GF = new GeneralFunctions();
        public static string DocType = "33";
        public static string KeepColums = "";
        public static string AttachmentPath = "";

        #region "Fuctions"

        private void LoadData(int ClgCode)
        {
            Transaction trx = new Transaction();
            dtHeader = trx.GetMarketingDocument_ReturnDS("33", ClgCode, User.Identity.Name).Tables[0];

            if (dtHeader.Rows.Count == 0)
                ClgCode = 1;
            dtHeader = trx.GetMarketingDocument_ReturnDS("33", ClgCode, User.Identity.Name).Tables[0];
            dtHeader = GF.ConvertDate_RemoveCols(dtHeader, KeepColums);

            txtNo.Text = dtHeader.Rows[0]["ClgCode"].ToString();
            SetNavigatorURL(Int32.Parse(txtNo.Text));

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
            txtBPName.Text = "";
            txtRemark.Text = HttpUtility.HtmlDecode(dtHeader.Rows[0]["Notes"].ToString());
            txtSubject.Text = dtHeader.Rows[0]["Details"].ToString();
            DateTime d = DateTime.ParseExact(dtHeader.Rows[0]["Recontact"].ToString(), "yyyyMMdd",
                                             CultureInfo.InvariantCulture, DateTimeStyles.None);
            txtDate.Text = String.Format("{0:MM/dd/yyyy}", d);

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

        private void LoadDefault()
        {
            txtBP.Text = "ABEO";
            txtBPName.Text = "ABEO";
            ddlActivity.SelectedValue = "T";
            ddlType.Items.FindByText("TIMESHEET").Selected = true;

            MasterData masterDataWS = new MasterData();
            ddlSubject.DataSource =
                masterDataWS.GetActivitySubject(User.Identity.Name, Int32.Parse(ddlType.SelectedValue.ToString())).
                    Tables[0];
            ddlSubject.DataTextField = "name";
            ddlSubject.DataValueField = "code";
            ddlSubject.DataBind();

            txtDate.Text = DateTime.Now.ToShortDateString();
            txtFromTime.Text = "830";
            txtToTime.Text = "1730";
            txtSubject.Text = "TIMESHEET | " + User.Identity.Name + " | " +
                              String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDate.Text)) + " | " + txtBP.Text;
        }

        private void ClearScreen()
        {
            //-------------create table------------------
            imgAdd.Visible = true;
            imgUpdate.Visible = false;
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
            dtHeader.Columns.Add("AtcEntry");
            dtHeader.Columns.Add("U_UserID");

            KeepColums = GF.BuildKeepColumnStr(dtHeader);

            txtSubject.Text = "";
            txtRemark.Text = "";
            txtNo.Text = "";
            imgAdd.Visible = true;

            dtHeader.Rows.Add("", "", "", "", "", "", "", "20121022", "830", "20121022", "930", "N", "",
                              User.Identity.Name);

            dtAttachment = new DataTable();
            dtAttachment.Columns.Add("No");
            dtAttachment.Columns.Add("srcPath");
            dtAttachment.Columns.Add("trgtPath");
            dtAttachment.Columns.Add("FileName");
            dtAttachment.Columns.Add("FileExt");
            dtAttachment.Columns.Add("Date");
            SetNavigatorURL(0);
        }

        public String _collectData()
        {
            try
            {
                CultureInfo ivC = new System.Globalization.CultureInfo("es-US");
                if (GF == null) GF = new GeneralFunctions(User.Identity.Name);
                //Update table header
                txtSubject.Text = "TIMESHEET | " + User.Identity.Name + " | " +
                                  Convert.ToDateTime(txtDate.Text, ivC).ToString("yyyyMMdd") + " | " + txtBP.Text;

                DataRow dr = dtHeader.Rows[0];
                dr["Action"] = ddlActivity.SelectedValue.ToString();
                dr["CntctType"] = ddlType.SelectedValue.ToString();
                dr["CntctSbjct"] = ddlSubject.SelectedValue.ToString();
                dr["CardCode"] = txtBP.Text;
                dr["Notes"] = HttpUtility.HtmlAttributeEncode(txtRemark.Text);
                dr["Details"] = txtSubject.Text;

                dr["Recontact"] = Convert.ToDateTime(txtDate.Text, ivC).ToString("yyyyMMdd");
                dr["BeginTime"] = txtFromTime.Text;
                dr["endDate"] = Convert.ToDateTime(txtDate.Text, ivC).ToString("yyyyMMdd");
                dr["ENDTime"] = txtToTime.Text;
                dr["AtcEntry"] = 2; // AttachmentString();
                if (cbClosed.Checked == true)
                {
                    dr["Closed"] = "Y";
                }
                DocumentXML objInfo = new DocumentXML();
                String RemoveColumn = "";

                RemoveColumn = "ClgCode";

                return objInfo.ToXMLStringFromDS(DocType, dtHeader, dtContents, RemoveColumn);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ResetLineNo()
        {
            int i = 0;
            foreach (DataRow row in dtAttachment.Rows)
                row["No"] = ++i;
        }

        private string AttachmentString()
        {
            string str = "";
            foreach (DataRow row in dtAttachment.Rows)
                str = str + row["trgtPath"].ToString() + row["FileName"].ToString() + ";";
            return str;
        }

        private String BuildStrXMLAttachment()
        {
            DataTable dtHeader;
            dtHeader = new DataTable();
            dtHeader.Columns.Add("AbsEntry");
            dtHeader.Rows.Add("");

            DataTable dtLines = dtAttachment;
            GeneralFunctions GF = new GeneralFunctions();
            dtLines = GF.ConvertDate_RemoveCols(dtLines, "");
            DocumentXML objInfo = new DocumentXML();
            return objInfo.ToXMLStringFromDS("221", dtHeader, dtAttachment, "No");
        }

        protected void SetNavigatorURL(int currentOrder)
        {
            int next = 0;
            int previous = 0;

            next = currentOrder + 1;

            if (currentOrder > 0)
                previous = currentOrder - 1;
            else
                previous = 1;

            this.linkFirst.NavigateUrl = "/TIMESHEET/ABEO_TIMESHEET.aspx?clgCode=1";
            this.linkNext.NavigateUrl = "/TIMESHEET/ABEO_TIMESHEET.aspx?clgCode=" + next;
            this.linkPrevious.NavigateUrl = "/TIMESHEET/ABEO_TIMESHEET.aspx?clgCode=" + previous;
            this.linkLast.NavigateUrl = "/TIMESHEET/ABEO_TIMESHEET.aspx?clgCode=-1";

            this.linkNew.NavigateUrl = "/TIMESHEET/ABEO_TIMESHEET.aspx?clgCode=";
        }

        #endregion

        #region "Events"

        protected void Page_Load(object sender, EventArgs e)
        {
            purchaseOrderUpdatePanel.Attributes["style"] = "display: '';";
            if (!IsPostBack)
            {
                ClearScreen();
                try
                {
                    //------------------load activity--------------------
                    MasterData masterDataWS = new MasterData();
                    DataSet dsMaster = masterDataWS.GetActivityType(User.Identity.Name);
                    AttachmentPath =
                        masterDataWS.GetCompanySetting(User.Identity.Name).Tables[0].Rows[0]["AttachPath"].ToString();

                    ddlType.DataSource = dsMaster.Tables[0];
                    ddlType.DataTextField = "name";
                    ddlType.DataValueField = "code";
                    ddlType.DataBind();


                    string clgCode = Request.QueryString["clgCode"];
                    if (!String.IsNullOrEmpty(clgCode))
                    {
                        LoadData(int.Parse(clgCode));
                    }
                    else
                    {
                        LoadDefault();
                        //dtHeader.Rows.Add("","", "", "", "", "", "", "20121022", "830", "20121022", "930", "N", User.Identity.Name);
                    }
                }
                catch (Exception ex)
                {
                    Session["errorMessage"] = ex.ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('" + ex.ToString() + "','');", true);

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
                if (GF == null) GF = new GeneralFunctions(User.Identity.Name);

                switch (this.Request["__EVENTARGUMENT"].ToString())
                {
                    case "EditCustomerCallBack":
                        BusinessPartner chosenPartner = Session["chosenPartner"] as BusinessPartner;
                        if (chosenPartner != null)
                        {
                            this.txtBPName.Text = chosenPartner.CardName;
                            this.txtBP.Text = chosenPartner.CardCode;

                            //MasterData masterDataWS = new MasterData();
                            //DataSet contactPersons = masterDataWS.GetContactPerson(chosenPartner.CardCode, User.Identity.Name);
                            //ListItem item = new ListItem();
                            //foreach (DataRow row in contactPersons.Tables[0].Rows)
                            //{
                            //    String name = row[1].ToString() + " " + row[2].ToString();
                            //    item = new ListItem(name, row[1].ToString());
                            //    if ("Y".Equals(row[0].ToString()))
                            //        item.Selected = true;
                            //    ddlContactPerson.Items.Add(item);
                            //}
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        protected void imgAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (HttpUtility.HtmlAttributeEncode(txtRemark.Text) == "")
                {
                    Session["errorMessage"] = "Detail information can't be blank";
                    Session["requestXML"] = "";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('Detail information can not be blank','');",
                                                        true);

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                        "Dialog.hideLoader();", true);
                    return;
                }
                DataSet ds = new DataSet();
                String requestXML = "";
                SAP.WebServices.Transaction ts = new WebServices.Transaction();

                //if (dtAttachment.Rows.Count > 0)
                //{
                //    //------------------------------add attachment---------------------------------
                //    requestXML = BuildStrXMLAttachment();
                //    ds = ts.CreateMarketingDocument(requestXML, User.Identity.Name, DocType, txtNo.Text, false);
                //    if ((int)ds.Tables[0].Rows[0]["ErrCode"] != 0)
                //    {
                //        Session["errorMessage"] = ds.Tables[0].Rows[0]["ErrMsg"];
                //        Session["requestXML"] = requestXML;
                //        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                //            "Main.setMasterMessage('" + ds.Tables[0].Rows[0]["ErrMsg"] + "','');", true);
                //    }
                //    else
                //    {
                //        string str = "";
                //        str = "a";
                //    }
                //}

                //---------------------------------------add activity------------------------------
                requestXML = _collectData();

                //GF.RunQuery("insert into TransLog(XMLString,DocType,U_UserID) values('" + requestXML + "','" + DocType + "','" + User.Identity.Name + "')");
                if (!String.IsNullOrEmpty(txtNo.Text))
                    ds = ts.CreateMarketingDocument(requestXML, User.Identity.Name, DocType, txtNo.Text, true);
                else
                    ds = ts.CreateMarketingDocument(requestXML, User.Identity.Name, DocType, "", false);

                if ((int) ds.Tables[0].Rows[0]["ErrCode"] != 0)
                {
                    //string errmss = ds.Tables[0].Rows[0]["ErrMsg"].ToString();
                    //if (errmss.Length > 10) errmss = errmss.Substring(0, 10);
                    //Session["errorMessage"] = errmss;
                    //Session["requestXML"] = requestXML;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('" + ds.Tables[0].Rows[0]["ErrCode"] +
                                                        "','');", true);
                }
                else
                {
                    Session["successMessage"] = "Operation complete sucessful!";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('" + "Operation complete sucessful!" +
                                                        "','');", true);
                    ClearScreen();

                    //Emailling em = new Emailling();
                    //string str = em.SendMail("Truong Thai Thuy", "thuytruong@electra-ai.com", "ABC", "ABC");
                    //if (str != "")
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                    //             "Main.setMasterMessage('" + str + "','');", true);
                    //}
                }
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                    "Dialog.hideLoader();", true);
            }
            catch (Exception ex)
            {
                Session["errorMessage"] = ex.ToString();
                Session["requestXML"] = "";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                    "Main.setMasterMessage('" + ex.ToString() + "','');", true);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                    "Dialog.hideLoader();", true);
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterData masterDataWS = new MasterData();
            ListItem item = new ListItem();
            DataSet dsMaster = masterDataWS.GetActivitySubject(User.Identity.Name,
                                                               Int32.Parse(ddlType.SelectedValue.ToString()));
            foreach (DataRow row in dsMaster.Tables[0].Rows)
            {
                item = new ListItem(row[1].ToString(), row[0].ToString());
                ddlSubject.Items.Add(item);
            }
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile != null)
            {
                string filename = FileUpload1.FileName;
                dtAttachment.Rows.Add(dtAttachment.Rows.Count, Path.GetFullPath(FileUpload1.FileName), AttachmentPath,
                                      FileUpload1.FileName, Path.GetExtension(FileUpload1.FileName),
                                      DateTime.Now.ToString("MM/dd/yyyy"));
                lvAttachments.DataSource = dtAttachment;
                lvAttachments.DataBind();
            }
        }

        protected void lvAttachments_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            ListViewItem lvi = e.Item;
            switch (e.CommandName)
            {
                case "DeleteItem":
                    // delete data and update dt
                    int i_idx = e.Item.DataItemIndex;
                    dtAttachment.Rows.RemoveAt(i_idx);
                    this.lvAttachments.EditIndex = -1;
                    ResetLineNo();
                    this.lvAttachments.DataSource = dtAttachment;
                    this.lvAttachments.DataBind();
                    break;
            }
        }

        #endregion
    }
}