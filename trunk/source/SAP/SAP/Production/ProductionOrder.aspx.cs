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
    public partial class ProductionOrder : System.Web.UI.Page
    {
        public static DataTable dtItem;
        public static DataTable dtHeader;
        private string DocType = "22";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtItem = new DataTable();
                dtItem.Columns.Add("No");//need to remove
                dtItem.Columns.Add("ItemCode");
                dtItem.Columns.Add("Dscription");//need to remove
                dtItem.Columns.Add("PlannedQty");
                dtItem.Columns.Add("wareHouse");
                dtItem.Columns.Add("IssueType");

                dtHeader = new DataTable();
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
                dtHeader.Rows.Add("","","","","","20121001", "20121001", "From SAP WEB", "Transfer JE Remark", User.Identity.Name);

                this.lvStage.DataSource = dtItem;
                this.lvStage.DataBind();

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
                        case "EditCustomerCallBack":
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
                                DataRow dr = dtItem.Rows[itemNo - 1];
                                dr["No"] = itemNo;
                                dr["ItemCode"] = chosenItem.ItemCode;
                                dr["Dscription"] = chosenItem.ItemName;
                                dr["Quantity"] = 1;
                                this.lvStage.DataSource = dtItem;
                                this.lvStage.DataBind();
                            }
                            break;
                        case "EditBOMCallBack":
                            BOM chosenBOM = Session["chosenItem"] as BOM;
                            if (chosenBOM != null)
                            {
                                txtItemCode.Text = chosenBOM.ItemCode;
                                txtItemName.Text = chosenBOM.ItemName;

                                GetDefault gf = new GetDefault();
                                dtItem= gf.GetBOMChild(User.Identity.Name, txtItemCode.Text).Tables[0];
                                this.lvStage.DataSource = dtItem;
                                this.lvStage.DataBind();
                            }
                            break;
                        case "EditWareHouseCallBack":
                            WareHouse chosenWarehouse = Session["chosenWarehouse"] as WareHouse;
                        
                            if (chosenWarehouse != null)
                            {
                                if (txtWarehouse.Text == "")
                                {
                                    txtWarehouse.Text = chosenWarehouse.WhsCode;
                                }
                                else
                                {
                                    itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                                    DataRow dr = dtItem.Rows[itemNo-1];
                                    dr["wareHouse"] = chosenWarehouse.WhsCode;
                                    this.lvStage.DataSource = dtItem;
                                    this.lvStage.DataBind();
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
                if (dtItem.Rows.Count == 0)
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
        #region "List View Stage"
        
            protected void lvStage_ItemCommand(object sender, ListViewCommandEventArgs e)
            {
                try
                {
                    ListViewItem lvi = e.Item;
                    switch (e.CommandName)
                    {
                        case "Delete":
                            int i_idx = e.Item.DataItemIndex;
                            dtItem.Rows.RemoveAt(i_idx);// code for dummy
                            this._StageCancelAddNew();
                            break;
                        case "CancelUpdate":
                            Label lblCode = (Label)lvi.FindControl("lblItemCode");
                             if (lblCode == null || string.IsNullOrEmpty(lblCode.Text))
                             {
                                 Label lblNo1 = (Label)lvi.FindControl("lblNoEdit");
                                 if (string.IsNullOrEmpty(lblNo1.Text)) return;

                                 foreach (DataRow row in dtItem.Rows)
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
                            foreach (DataRow row in dtItem.Rows)
                            {
                                if (row["No"].ToString().Equals(lblNo.Text))
                                {
                                    row["ItemCode"] = ((Label)lvi.FindControl("lblItemCode")).Text;
                                    row["Dscription"] = ((Label)lvi.FindControl("lblItemName")).Text;
                                    row["Quantity"] = ((TextBox)lvi.FindControl("txtQuantityEdit")).Text;
                                    row["WhsCode"] = ((Label)lvi.FindControl("lblWarehouse")).Text;
                                    break;
                                }
                            }
                            this.lvStage.EditIndex = -1;
                            this.lvStage.DataSource = dtItem;
                            this.lvStage.DataBind();
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

            protected void lvStage_ItemEditing(object sender, ListViewEditEventArgs e)
            {
                //click edit link
                this.btnAddRecord.Enabled = false;
                this.lvStage.EditIndex = e.NewEditIndex;
                this.lvStage.DataSource = dtItem;
                this.lvStage.DataBind();
            }

            protected void lvStage_ItemInserted(object sender, ListViewInsertedEventArgs e)
            {
                this._StageCancelAddNew();
            }

            protected void lvStage_ItemInserting(object sender, ListViewInsertEventArgs e)
            {
                
            }

            protected void _btnAddRecord_Click(object sender, EventArgs e)
            {
                int No = GetNo();
                dtItem.Rows.Add(No, "", "", 1, "","M");
                this.lvStage.DataSource = dtItem;
                this.lvStage.EditIndex = No-1;// dtItem.Rows.Count - 1;
                this.lvStage.DataBind();
            }
                
            protected void lvStage_ItemUpdating(object sender, ListViewUpdateEventArgs e)
            {
                Label lblCode = (Label)lvStage.Items[e.ItemIndex].FindControl("lblItemCode");
                if (lblCode == null || string.IsNullOrEmpty(lblCode.Text))
                {
                    e.Cancel = true;
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors", "Main.setMasterMessage('Missing Item','');", true);
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading", "Dialog.hideLoader();", true);
                    return;
                }
                this.lvStage.EditIndex = -1;
                this.lvStage.DataSource = dtItem;
                this.lvStage.DataBind();
            }
            protected void lvStage_ItemCreated(object sender, ListViewItemEventArgs e)
            {

            }

            protected void lvStage_ItemDeleting(object sender, ListViewDeleteEventArgs e)
            {

            }
        # endregion
        #region "Functions"
            private void _StageCancelAddNew()
            {
                this.lvStage.InsertItemPosition = InsertItemPosition.None;
                this.btnAddRecord.Enabled = true;
                this.lvStage.EditIndex = -1;
                this.lvStage.DataSource = dtItem;
                this.lvStage.DataBind();
            }
            private int GetNo()
            {
                return dtItem.Rows.Count+1;
            }
            void ClearScreen()
            {
                txtItemCode.Text = "";
                txtItemName.Text = "";
                txtCardCode.Text = "";
                txtCostCenter.Text = "";
                txtWarehouse.Text = "";

                dtItem.Clear();

                lvStage.DataSource = dtItem;
                lvStage.DataBind();
            }
            public String _collectData()
            {
                try
                {
                    //Update table header
                    DataRow dr = dtHeader.Rows[0];
                    //dr["CardCode"] = txtBP.Text;
                    //dr["CardName"] = txtBPName.Text;
                    //dr["Address"] = txtAddress.Text;
                    //dr["Filler"] = txtFromWarehouse.Text;

                    //dr["DocDate"] = String.Format("{0:yyyyMMdd}", DateTime.Parse(txtPostingDate.Text));
                    //dr["DocDueDate"] = String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDocumentDate.Text)); 
                    //dr["Comments"] = txtRemarks.Text;
                    //dr["JrnlMemo"] = txtJournalRemark.Text;
                   
                    DocumentXML objInfo = new DocumentXML();
                    String RemoveColumn = "No";
                    return objInfo.ToXMLStringFromDS(DocType, dtHeader, dtItem, RemoveColumn);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        # endregion 

            protected void ProductListPagerCombo_PreRender(object sender, EventArgs e)
            {
                lvStage.DataSource = dtItem;
                lvStage.DataBind();
            }

            protected void lvStage_ItemCanceling(object sender, ListViewCancelEventArgs e)
            {
                 Label lblCode = (Label)lvStage.Items[e.ItemIndex].FindControl("lblItemCode");
                 if (lblCode == null || string.IsNullOrEmpty(lblCode.Text))
                 {
                     dtItem.Rows.RemoveAt(0);
                 }
                //int i_idx = e.Item.DataItemIndex;
                //dtItem.Rows.RemoveAt(i_idx);// code for dummy

                //this._StageCancelAddNew();
            }
    }
}