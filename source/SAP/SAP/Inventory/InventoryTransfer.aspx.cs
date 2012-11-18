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
    public partial class InventoryTransfer : System.Web.UI.Page
    {
        public static DataTable dtContents;
        public static DataTable dtHeader;
        private GeneralFunctions GF = new GeneralFunctions(HttpContext.Current.User.Identity.Name);
        private string DocType = "67";
        private string TblHeaderName = "OWTR";
        private string TblLineName = "WTR1";
        private string CurrentPageUrl = "/Inventory/InventoryTransfer.aspx";
        private string HeaderKeyName = "DocEntry";
        public static string KeepColumsContent = "";
        
        
        
        #region "Events"
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    MasterData md = new MasterData();
                    ddlPriceList.DataSource = md.GetPriceList(User.Identity.Name).Tables[0];
                    ddlPriceList.DataValueField = "ListNum";
                    ddlPriceList.DataTextField = "ListName";
                    ddlPriceList.DataBind();

                    ClearScreen();
                    String orderId = Request.QueryString["order_id"];
                    if (!String.IsNullOrEmpty(orderId))
                    {
                        LoadData(orderId, DocType);
                        SetScreenStatus("Close");
                    }
                    else
                    {
                        //LoadDefault();
                        SetScreenStatus("New");
                    }

                    String BaseEntry = Request.QueryString["base_entry"];
                    if (!String.IsNullOrEmpty(BaseEntry))
                    {
                        String BaseType = Request.QueryString["base_type"];
                        LoadData(BaseEntry, BaseType);
                    }
                }
            }
            protected override void OnLoadComplete(EventArgs e)
            {
                try
                {
                    base.OnLoadComplete(e);
                    // get callback from popup
                    if (this.Request["__EVENTARGUMENT"] != null && this.Request["__EVENTARGUMENT"].ToString() != "")
                    {
                        Int32 itemNo = 0;
                        switch (this.Request["__EVENTARGUMENT"].ToString())
                        {
                            case "EditBusinessPartnerCallBack":
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

                            case "EditItemCallBack":
                                ItemMaster chosenItem = Session["chosenItem"] as ItemMaster;

                                itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                                if (chosenItem != null)
                                {
                                    DataRow dr = dtContents.Rows[itemNo - 1];
                                    dr["No"] = itemNo;
                                    dr["ItemCode"] = chosenItem.ItemCode;
                                    dr["Dscription"] = chosenItem.ItemName;
                                    dr["Quantity"] = 1;
                                    this.lvContent.DataSource = dtContents;
                                    this.lvContent.DataBind();
                                }
                                break;
                            case "EditWareHouseCallBack":
                                WareHouse chosenWarehouse = Session["chosenWarehouse"] as WareHouse;

                                if (chosenWarehouse != null)
                                {
                                    itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                                    if (itemNo == -1)
                                    {
                                        txtFromWarehouse.Text = chosenWarehouse.WhsCode;
                                    }
                                    else
                                    {
                                        
                                        DataRow dr = dtContents.Rows[itemNo - 1];
                                        dr["WhsCode"] = chosenWarehouse.WhsCode;
                                        dtContents.Rows[itemNo - 1]["Quantity"] = ((TextBox)lvContent.Items[itemNo - 1].FindControl("txtQuantityEdit")).Text;
                                

                                        this.lvContent.DataSource = dtContents;
                                        this.lvContent.DataBind();
                                    }
                                }
                                break;
                       
                            default:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                          "Main.setMasterMessage('" + ex.ToString() + "','');", true);

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                         "Dialog.hideLoader();", true);
                }

            }
            protected void lvContent_ItemCommand(object sender, ListViewCommandEventArgs e)
            {
                try
                {
                    ListViewItem lvi = e.Item;
                    switch (e.CommandName)
                    {
                        case "Delete":
                            // delete data and update dt
                            int i_idx = e.Item.DataItemIndex;
                            dtContents.Rows.RemoveAt(i_idx);// code for dummy
                        
                            this._StageCancelAddNew();
                            break;
                        case "CancelUpdate":
                            this._StageCancelAddNew();
                            break;
                        case "Update":

                            if (string.IsNullOrEmpty(((Label)lvi.FindControl("lblItemCode")).Text)) return;

                            Label lblNo = (Label)lvi.FindControl("lblNoEdit");
                            foreach (DataRow row in dtContents.Rows)
                            {
                                if (row["No"].ToString().Equals(lblNo.Text))
                                {
                                    row["ItemCode"] = ((Label)lvi.FindControl("lblItemCode")).Text;
                                    row["Dscription"] = ((Label)lvi.FindControl("lblItemName")).Text;
                                    row["Quantity"] = ((TextBox)lvi.FindControl("txtQuantityEdit")).Text;
                                    row["WhsCode"] = ((Label)lvi.FindControl("lblWarehouse")).Text;
                                    updateRowTotalPrice(row);
                                    break;
                                }
                            }
                            this.lvContent.EditIndex = -1;
                            this.lvContent.DataSource = dtContents;
                            this.lvContent.DataBind();
                            SetControlsStatus("Update");
                            break;
                   
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                          "Main.setMasterMessage('" + ex.ToString() + "','');", true);

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                         "Dialog.hideLoader();", true);
                }
            }
            protected void lvContent_ItemEditing(object sender, ListViewEditEventArgs e)
            {

                this.lvContent.EditIndex = e.NewEditIndex;

                // Reset FormatNumeric 
                string lsQty = GF.ResetFormatNumeric(dtContents.Rows[e.NewEditIndex]["Quantity"].ToString());       // Quantity

                dtContents.Rows[e.NewEditIndex]["Quantity"] = lsQty;

                this.lvContent.DataSource = dtContents;
                this.lvContent.DataBind();
                SetControlsStatus("Edit");
            }
            protected void lvContent_ItemInserted(object sender, ListViewInsertedEventArgs e)
            {
                this._StageCancelAddNew();
            }
            protected void _btnAddRecord_Click(object sender, EventArgs e)
            {
                int iNo = dtContents.Rows.Count + 1;
                dtContents.Rows.Add();
                dtContents.Rows[iNo - 1]["No"] = iNo;

                this.lvContent.DataSource = dtContents;
                this.lvContent.EditIndex = iNo - 1;// dtContents.Rows.Count - 1;
                this.lvContent.DataBind();
                //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                //            "javascript:Main.openDialog(\"../Popup_EditItem.aspx\",id=\""+iNo.ToString()+"\")", true);

            }
            protected void lvContent_ItemUpdating(object sender, ListViewUpdateEventArgs e)
            {
                Label lblCode = (Label)lvContent.Items[e.ItemIndex].FindControl("lblItemCode");
                if (lblCode == null || string.IsNullOrEmpty(lblCode.Text))
                {
                    e.Cancel = true;
                    return;
                }
                this.lvContent.EditIndex = -1;
                this.lvContent.DataSource = dtContents;
                this.lvContent.DataBind();
            }
            protected void ProductListPagerCombo_PreRender(object sender, EventArgs e)
            {
                lvContent.DataSource = dtContents;
                lvContent.DataBind();
            }
            protected void btnAdd_Click(object sender, ImageClickEventArgs e)
            {
                try
                {
                    if (txtFromWarehouse.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors", "Main.setMasterMessage('Missing From Warehouse','');", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading", "Dialog.hideLoader();", true);
                        return;
                    }
                    if (dtContents.Rows.Count == 0)
                    {

                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors", "Main.setMasterMessage('Missing Item','');", true);
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading", "Dialog.hideLoader();", true);
                        return;
                    }
                    String requestXML = _collectData();
                    SAP.WebServices.Transaction ts = new WebServices.Transaction();
                    DataSet ds = ts.CreateMarketingDocument(requestXML, User.Identity.Name, DocType, "", false);
                    if ((int)ds.Tables[0].Rows[0]["ErrCode"] != 0)
                    {
                        Session["errorMessage"] = ds.Tables[0].Rows[0]["ErrMsg"];
                        Session["requestXML"] = requestXML;
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                            "Main.setMasterMessage('" + WebUtility.HtmlEncode(ds.Tables[0].Rows[0]["ErrMsg"].ToString()) + "','');", true);
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
                catch (Exception ex)
                {
                    Session["errorMessage"] = ex.ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                          "Main.setMasterMessage('" + ex.ToString() + "','');", true);

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                         "Dialog.hideLoader();", true);
                }

            }
        # endregion
        #region "Functions"
            protected void SetNavigatorURL(string CurrentKey)
            {
                GetDefault df = new GetDefault();
                DataSet nav = df.GetNextPreviousID(DocType, User.Identity.Name, TblHeaderName, HeaderKeyName, CurrentKey);
                if (nav != null)
                {
                    this.linkFirst.NavigateUrl = CurrentPageUrl + "?order_id=" + nav.Tables[0].Rows[0]["Fir"].ToString(); ;
                    this.linkNext.NavigateUrl = CurrentPageUrl + "?order_id=" + nav.Tables[0].Rows[0]["Nex"].ToString(); ;
                    this.linkPrevious.NavigateUrl = CurrentPageUrl + "?order_id=" + nav.Tables[0].Rows[0]["Pre"].ToString(); ;
                    this.linkLast.NavigateUrl = CurrentPageUrl + "?order_id=" + nav.Tables[0].Rows[0]["Las"].ToString(); ;
                }
                this.linkNew.NavigateUrl = CurrentPageUrl + "?order_id=";
            }
            protected void LoadData(String orderId, string NewDocType)
            {
                Transaction ts = new Transaction();
                DataSet returnDoc = ts.GetMarketingDocument_ReturnDS(NewDocType, orderId, User.Identity.Name);
                DataTable dtHeader;

                if (returnDoc == null)
                    orderId = "1";
                else
                    dtHeader = returnDoc.Tables[0];

                returnDoc = ts.GetMarketingDocument_ReturnDS(NewDocType, orderId, User.Identity.Name);

                if (returnDoc == null || returnDoc.Tables.Count < 2)
                {

                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                                        "Main.setMasterMessage('No record found!','');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                                                        "Dialog.hideLoader();", true);
                    return;
                }

                dtHeader = returnDoc.Tables[0];
                dtContents = GF.ConvertDataTable_RemoveCols(returnDoc.Tables[1], KeepColumsContent);
                DataRow dr = dtHeader.Rows[0];
                SetNavigatorURL(dr["DocEntry"].ToString());
                hlJE.NavigateUrl = "../Financials/JournalEntry.aspx?order_id=" + dr["TransID"].ToString();
                CultureInfo ivC = new System.Globalization.CultureInfo("es-US");
                this.txtNo.Text = dr["DocEntry"].ToString();
                this.txtStatus.Text = dr["DocStatus"].ToString();
                this.txtBPName.Text = dr["CardName"].ToString();
                this.txtBP.Text = dr["CardCode"].ToString();
                //lBP.NavigateUrl = "../BusinessPartner/BusinessPartnerMaster.aspx?cardcode=" + txtVendor.Text;
                txtAddress.Text = dr["Address"].ToString();
                txtFromWarehouse.Text=dr["Filler"].ToString();
                MasterData masterDataWS = new MasterData();
                ddlContactPerson.DataSource = masterDataWS.GetContactPerson(txtBP.Text, User.Identity.Name).Tables[0];
                ddlContactPerson.DataValueField = "Code";
                ddlContactPerson.DataTextField = "FirstName";
                ddlContactPerson.DataBind();

                DateTime d = Convert.ToDateTime(dr["DocDate"], ivC);
                txtPostingDate.Text = String.Format("{0:MM/dd/yyyy}", d);

                d = Convert.ToDateTime(dr["TaxDate"], ivC);
                txtDocumentDate.Text = String.Format("{0:MM/dd/yyyy}", d);

                txtJournalRemark.Text = dr["JrnlMemo"].ToString();
                txtRemarks.Text = dr["Comments"].ToString();

                foreach (DataRow row in dtContents.Rows)
                {
                    updateRowTotalPrice(row);
                    if (DocType != NewDocType) //truong hop copy to
                    {
                        row["BaseEntry"] = orderId;
                        row["BaseType"] = NewDocType;
                        row["BaseLine"] = row["LineNum"];
                    }
                }
                this.lvContent.DataSource = dtContents;
                this.lvContent.DataBind();

            }       
            protected void SetScreenStatus(string Status)
            {
                switch (Status)
                {
                    case "New":
                        btnAdd.Visible = true;
                        btnAddRecord.Visible = true;
                        break;
                    case "Update":
                        btnAdd.Visible = false;
                        btnAddRecord.Visible = false;
                        break;
                    case "Close":
                        btnAdd.Visible = false;
                        btnAddRecord.Visible = false;
                        break;
                }
            }
            private void _StageCancelAddNew()
            {
                this.lvContent.InsertItemPosition = InsertItemPosition.None;
                this.btnAddRecord.Enabled = true;
                this.lvContent.EditIndex = -1;
                this.lvContent.DataSource = dtContents;
                this.lvContent.DataBind();
            }
            void ClearScreen()
            {
                dtContents = new DataTable("WTR1");
                dtContents.Columns.Add("No");
                dtContents.Columns.Add("ItemCode");
                dtContents.Columns.Add("Dscription");
                dtContents.Columns.Add("Quantity");
                dtContents.Columns.Add("WhsCode");
                dtContents.Columns.Add("BaseEntry");
                dtContents.Columns.Add("BaseLine");
                dtContents.Columns.Add("BaseType");
                dtContents.Columns.Add("LineNum");
                KeepColumsContent = GF.BuildKeepColumnStr(dtContents);

                dtHeader = new DataTable("OWTR");
                dtHeader.Columns.Add("CardCode");
                dtHeader.Columns.Add("CardName");
                dtHeader.Columns.Add("Address");
                dtHeader.Columns.Add("Filler");
                dtHeader.Columns.Add("DocDate");
                dtHeader.Columns.Add("DocDueDate");
                dtHeader.Columns.Add("Comments");
                dtHeader.Columns.Add("JrnlMemo");
                dtHeader.Columns.Add("U_UserID");
                dtHeader.Rows.Add();

                this.lvContent.DataSource = dtContents;
                this.lvContent.DataBind();

                this.txtDocumentDate.Text = DateTime.Now.ToShortDateString();
                this.txtPostingDate.Text = DateTime.Now.ToShortDateString();

                txtRemarks.Text = "";
                txtJournalRemark.Text = "";
                txtBP.Text = "";
                txtBPName.Text = "";
                txtFromWarehouse.Text = "";

                lvContent.DataSource = dtContents;
                lvContent.DataBind();

                SetNavigatorURL("0");
                SetScreenStatus("New");
            }
            public String _collectData()
            {
                try
                {
                    //Update table header
                    DataRow dr = dtHeader.Rows[0];
                    dr["CardCode"] = txtBP.Text;
                    dr["CardName"] = txtBPName.Text;
                    dr["Address"] = txtAddress.Text;
                    dr["Filler"] = txtFromWarehouse.Text;
                    CultureInfo ivC = new System.Globalization.CultureInfo("es-US");

                    dr["DocDate"] = Convert.ToDateTime(txtPostingDate.Text, ivC).ToString("yyyyMMdd");
                    dr["DocDueDate"] = Convert.ToDateTime(txtDocumentDate.Text, ivC).ToString("yyyyMMdd");

                    dr["Comments"] = txtRemarks.Text;
                    dr["JrnlMemo"] = txtJournalRemark.Text;
                    dr["U_UserID"] = User.Identity.Name;
                    DocumentXML objInfo = new DocumentXML();
                    DataSet ds = new DataSet("DS");
                    Array arrContentsCols = new string[] {"Quantity" };
                    dtHeader.TableName = TblHeaderName;
                    dtContents.TableName = TblLineName;
                    ds.Tables.Add(dtHeader.Copy());
                    ds.Tables.Add(GF.ResetFormatNumeric(dtContents, arrContentsCols).Copy());

                    return objInfo.ToXMLStringFromDS(DocType, ds);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            public void updateRowTotalPrice(DataRow row)
            {
                double quantity = 0;
                quantity = GF.Object2Double(row["Quantity"], "QtyDec");
                row["Quantity"] = GF.FormatNumeric(quantity.ToString(), "QtyDec");
            }
            private void SetControlsStatus(string asStatus)
            {
                switch (asStatus)
                {
                    case "Add":
                        btnAdd.Enabled = btnAddRecord.Enabled = false;
                        break;
                    case "Edit":
                        btnAdd.Enabled = btnAddRecord.Enabled = false;
                        break;
                    case "Update":
                        btnAdd.Enabled = btnAddRecord.Enabled = true;
                        break;
                    case "Save":
                        btnAdd.Enabled = btnAddRecord.Enabled = true;
                        break;
                }
            }
        # endregion 
            
    }
}