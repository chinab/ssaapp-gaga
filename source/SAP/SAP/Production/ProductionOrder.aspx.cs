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
    public partial class ProductionOrder : System.Web.UI.Page
    {
        public static DataTable dtContents;
        public static DataTable dtHeader;
        private string DocType = "22";
        private GeneralFunctions GF = new GeneralFunctions(HttpContext.Current.User.Identity.Name);
        private string TblHeaderName = "OWOR";
        private string TblLineName = "WOR1";
        private string CurrentPageUrl = "/Production/ProductionOrder.aspx";
        private string HeaderKeyName = "DocEntry";
        public static string KeepColumsContent = "";
        #region "Event"
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    ClearScreen();
                    String orderId = Request.QueryString["order_id"];
                    if (!String.IsNullOrEmpty(orderId))
                    {
                        LoadData(orderId, DocType);
                        //if (txtStatus.Text == "C")
                        //    SetScreenStatus("Close");
                        //else if (txtStatus.Text == "O")
                        //    SetScreenStatus("Update");
                    }
                    else
                    {
                       // LoadDefault();
                        SetScreenStatus("New");
                    }

                    this.txtDueDate.Text = DateTime.Now.ToShortDateString();
                    this.txtPostingDate.Text = DateTime.Now.ToShortDateString();
                    txtQuantity.Text = "1";
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
                                //this.txtBPName.Text = chosenPartner.CardName;
                                this.txtCardCode.Text = chosenPartner.CardCode;
                            }
                            break;
                        case "EditItemCallBack":
                            ItemMaster chosenItem = Session["chosenItem"] as ItemMaster;
                            if (chosenItem != null)
                            {
                                itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                                if (itemNo == -1)
                                {
                                    txtItemCode.Text = chosenItem.ItemCode;
                                    txtItemName.Text = chosenItem.ItemName;
                                }
                                else
                                {
                                    itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                                    DataRow dr = dtContents.Rows[itemNo - 1];
                                    dr["No"] = itemNo;
                                    dr["ItemCode"] = chosenItem.ItemCode;
                                    dr["Dscription"] = chosenItem.ItemName;
                                    dr["Quantity"] = 1;
                                    this.lvContents.DataSource = dtContents;
                                    this.lvContents.DataBind();
                                }
                            }
                            break;
                        case "EditBOMCallBack":
                            BOM chosenBOM = Session["chosenItem"] as BOM;
                            if (chosenBOM != null)
                            {
                                txtItemCode.Text = chosenBOM.ItemCode;
                                txtItemName.Text = chosenBOM.ItemName;

                                GetDefault gf = new GetDefault();
                                dtContents= gf.GetBOMChild(User.Identity.Name, txtItemCode.Text).Tables[0];
                                this.lvContents.DataSource = dtContents;
                                this.lvContents.DataBind();
                                updateTableTotalPrice();
                            }
                            break;
                        case "EditWareHouseCallBack":
                            WareHouse chosenWarehouse = Session["chosenWarehouse"] as WareHouse;
                        
                            if (chosenWarehouse != null)
                            {
                                itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                                if (itemNo == -1)
                                {
                                    txtWarehouse.Text = chosenWarehouse.WhsCode;
                                }
                                else
                                {
                                    DataRow dr = dtContents.Rows[itemNo-1];
                                    dr["wareHouse"] = chosenWarehouse.WhsCode;
                                    this.lvContents.DataSource = dtContents;
                                    this.lvContents.DataBind();
                                }
                            }
                            break;
                        case "EditProjectCallBack":
                            Project chosenProject = Session["chosenProject"] as Project;
                            if (chosenProject != null)
                            {
                                txtProject.Text = chosenProject.PrjCode;
                            }
                            break;
                        case "EditCostCenterCallBack":
                            CostCenter chosenCostCenter = Session["chosenCostCenter"] as CostCenter;
                            if (chosenCostCenter != null)
                            {
                                txtCostCenter.Text = chosenCostCenter.PrcCode;
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
            protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (txtWarehouse.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors", "Main.setMasterMessage('Missing From Warehouse','');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading","Dialog.hideLoader();", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                                      "Main.setMasterMessage('" + ex.ToString() + "','');", true);

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                     "Dialog.hideLoader();", true);
            }

        }
            protected void lvContents_ItemCommand(object sender, ListViewCommandEventArgs e)
            {
                try
                {
                    ListViewItem lvi = e.Item;
                    switch (e.CommandName)
                    {
                        case "Delete":
                            int i_idx = e.Item.DataItemIndex;
                            dtContents.Rows.RemoveAt(i_idx);// code for dummy
                            this._StageCancelAddNew();
                            break;
                        case "CancelUpdate":
                            Label lblCode = (Label)lvi.FindControl("lblItemCode");
                             if (lblCode == null || string.IsNullOrEmpty(lblCode.Text))
                             {
                                 Label lblNo1 = (Label)lvi.FindControl("lblNoEdit");
                                 if (string.IsNullOrEmpty(lblNo1.Text)) return;

                                 foreach (DataRow row in dtContents.Rows)
                                 {
                                     if (row["No"].ToString().Equals(lblNo1.Text))
                                     {
                                         row.Delete();
                                         break;
                                     }
                                 }
                             }
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
                                    row["PlannedQty"] = ((TextBox)lvi.FindControl("txtQuantityEdit")).Text;
                                    row["WhsCode"] = ((Label)lvi.FindControl("lblWarehouse")).Text;
                                    row["IssueType"] = ((DropDownList)lvi.FindControl("ddlIssueType")).SelectedValue.ToString();
                                    break;
                                }
                            }
                            this.lvContents.EditIndex = -1;
                            this.lvContents.DataSource = dtContents;
                            this.lvContents.DataBind();
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
            protected void lvContents_ItemEditing(object sender, ListViewEditEventArgs e)
            {
                //click edit link
                this.btnAddRecord.Enabled = false;
                this.lvContents.EditIndex = e.NewEditIndex;
                this.lvContents.DataSource = dtContents;
                this.lvContents.DataBind();
            }
            protected void lvContents_ItemInserted(object sender, ListViewInsertedEventArgs e)
            {
                this._StageCancelAddNew();
            }
            protected void _btnAddRecord_Click(object sender, EventArgs e)
            {
                if (txtItemCode.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors", "Main.setMasterMessage('Missing BOM Item','');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading", "Dialog.hideLoader();", true);
                    return;
                }
                int iNo = dtContents.Rows.Count + 1;
                dtContents.Rows.Add();
                dtContents.Rows[iNo - 1]["No"] = iNo;

                this.lvContents.DataSource = dtContents;

                int lastpage = this.ProductListPagerCombo.TotalRowCount / this.ProductListPagerCombo.PageSize;
                this.ProductListPagerCombo.SetPageProperties(lastpage * this.ProductListPagerCombo.PageSize, this.ProductListPagerCombo.MaximumRows, false);
                this.lvContents.EditIndex = iNo - 1;
                this.lvContents.DataBind();
                SetControlsStatus("Add");
            }
            protected void lvContents_ItemUpdating(object sender, ListViewUpdateEventArgs e)
            {
                Label lblCode = (Label)lvContents.Items[e.ItemIndex].FindControl("lblItemCode");
                if (lblCode == null || string.IsNullOrEmpty(lblCode.Text))
                {
                    e.Cancel = true;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors", "Main.setMasterMessage('Missing Item','');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading", "Dialog.hideLoader();", true);
                    return;
                }
                this.lvContents.EditIndex = -1;
                this.lvContents.DataSource = dtContents;
                this.lvContents.DataBind();
            }
            protected void ProductListPagerCombo_PreRender(object sender, EventArgs e)
            {
                lvContents.DataSource = dtContents;
                lvContents.DataBind();
            }
            protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (ddlType.SelectedItem.Value.ToString() == "P")
                {
                    linkItems.NavigateUrl = "javascript:Main.openDialog('../Popup_EditItem.aspx?id=-1','');";
                }
                else
                {
                    linkItems.NavigateUrl = "javascript:Main.openDialog('../Popup_EditBOM.aspx','');";
                }
            }
        #endregion
            #region "Functions"
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
               // dtContents = GF.ConvertDataTable_RemoveCols(returnDoc.Tables[1], KeepColumsContent);
                DataRow dr = dtHeader.Rows[0];
                SetNavigatorURL(dr["DocEntry"].ToString());
                //hlJE.NavigateUrl = "../Financials/JournalEntry.aspx?order_id=" + dr["TransID"].ToString();
                //this.txtName.Text = dr["CardName"].ToString();
                //this.txtVendor.Text = dr["CardCode"].ToString();
                //lBP.NavigateUrl = "../BusinessPartner/BusinessPartnerMaster.aspx?cardcode=" + txtVendor.Text;

                //this.txtNo.Text = dr["DocEntry"].ToString();
                //this.txtStatus.Text = dr["DocStatus"].ToString();
                CultureInfo ivC = new System.Globalization.CultureInfo("es-US");

                DateTime d = Convert.ToDateTime(dr["DocDate"], ivC);
                txtPostingDate.Text = String.Format("{0:MM/dd/yyyy}", d);

                d = Convert.ToDateTime(dtHeader.Rows[0]["DocDueDate"], ivC);
                txtDueDate.Text = String.Format("{0:MM/dd/yyyy}", d);

                //txtJournalRemark.Text = dr["JrnlMemo"].ToString();
                //txtRemarks.Text = dr["Comments"].ToString();

                updateTableTotalPrice();
                this.lvContents.DataSource = dtContents;
                this.lvContents.DataBind();
                updateTableTotalPrice();

            }       
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
            private void _StageCancelAddNew()
            {
                this.lvContents.InsertItemPosition = InsertItemPosition.None;
                this.btnAddRecord.Enabled = true;
                this.lvContents.EditIndex = -1;
                this.lvContents.DataSource = dtContents;
                this.lvContents.DataBind();
            }         
            void ClearScreen()
            {
                dtContents = new DataTable("WOR1");
                dtContents.Columns.Add("No");//need to remove
                dtContents.Columns.Add("ItemCode");
                dtContents.Columns.Add("Dscription");//need to remove
                dtContents.Columns.Add("PlannedQty");
                dtContents.Columns.Add("wareHouse");
                dtContents.Columns.Add("IssueType");

                dtHeader = new DataTable("OWOR");
                dtHeader.Columns.Add("Type");
                dtHeader.Columns.Add("Status");
                dtHeader.Columns.Add("PlannedQty");
                dtHeader.Columns.Add("CardCode");
                dtHeader.Columns.Add("warehouse");
                dtHeader.Columns.Add("PostDate");
                dtHeader.Columns.Add("DueDate");
                dtHeader.Columns.Add("Comments");
                dtHeader.Columns.Add("JrnlMemo");
                dtHeader.Columns.Add("U_UserID");
                dtHeader.Rows.Add();

                this.lvContents.DataSource = dtContents;
                this.lvContents.DataBind();

                txtItemCode.Text = "";
                txtItemName.Text = "";
                txtCardCode.Text = "";
                txtCostCenter.Text = "";
                txtWarehouse.Text = "";

                SetNavigatorURL("0");
                SetScreenStatus("New");

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
            public String _collectData()
            {
                try
                {
                    //Update table header
                    DataRow dr = dtHeader.Rows[0];
                    dr["Type"] = ddlType.SelectedItem.Value;
                    dr["Status"] = ddlStatus.SelectedItem.Value;
                    dr["PlannedQty"] = txtQuantity.Text;

                    dr["CardCode"] = txtCardCode.Text;
                    dr["warehouse"] = txtWarehouse.Text;
                    dr["PostDate"] = String.Format("{0:yyyyMMdd}", DateTime.Parse(txtPostingDate.Text));
                    dr["DueDate"] = String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDueDate.Text)); 
                    dr["Comments"] = "";
                    dr["JrnlMemo"] = "";
                    dr["U_UserID"] = User.Identity.Name;
                    Array arrContentsCols = new string[] { "Quantity" }; // Columns need to reset format numeric
                    DocumentXML objInfo = new DocumentXML();
                    DataSet ds = new DataSet("DS");
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
            protected void updateTableTotalPrice()
            {
                foreach (DataRow row in dtContents.Rows)
                {
                    row["PlannedQty"] = GF.FormatNumeric(row["PlannedQty"].ToString(), "QtyDec");
                }
                dtHeader.Rows[0]["PlannedQty"] = GF.FormatNumeric(dtHeader.Rows[0]["PlannedQty"].ToString(), "QtyDec");
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
            private void LoadDefault()
            { }
        # endregion 

            

   

            

            
    }
}