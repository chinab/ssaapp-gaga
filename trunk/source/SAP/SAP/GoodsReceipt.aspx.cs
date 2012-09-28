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
    public partial class GoodsReceipt : System.Web.UI.Page
    {
        public static DataTable dtItem;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtItem = new DataTable();
                dtItem.Columns.Add("No");
                dtItem.Columns.Add("ItemCode");
                dtItem.Columns.Add("ItemName");
                dtItem.Columns.Add("Quantity");
                dtItem.Columns.Add("Price");
                dtItem.Columns.Add("Total");
                dtItem.Columns.Add("Warehouse");
                dtItem.Columns.Add("Account");

                this.lvStage.DataSource = dtItem;
                this.lvStage.DataBind();

                this.txtDocumentDate.Text = DateTime.Now.ToShortDateString();
                this.txtPostingDate.Text = DateTime.Now.ToShortDateString();
            }
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            // get callback from popup
            if (this.Request["__EVENTARGUMENT"] != null && this.Request["__EVENTARGUMENT"].ToString() != "")
            {
                Int32 itemNo = 0;
                switch (this.Request["__EVENTARGUMENT"].ToString())
                {
                    case "EditItemCallBack":
                        ItemMaster chosenItem = Session["chosenItem"] as ItemMaster;
                       
                        itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                        if (chosenItem != null)
                        {
                            //dtItem.Rows.Add(GetNo(), "", "", 1, "0", "0", "", "");
                            // update grid
                            DataRow dr = dtItem.Rows[itemNo-1];
                            dr["No"] = itemNo;
                            dr["ItemCode"] = chosenItem.ItemCode;
                            dr["ItemName"] = chosenItem.ItemName;
                            dr["Quantity"] = 1;
                            dr["Price"] = "0";
                            dr["Total"] = "0";
                            this.lvStage.DataSource = dtItem;
                            this.lvStage.DataBind();
                            //this._StageCancelAddNew();
                        }
                        break;
                    case "EditWareHouseCallBack":
                        WareHouse chosenWarehouse = Session["chosenWarehouse"] as WareHouse;
                        itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                        if (chosenWarehouse != null)
                        {
                            DataRow dr = dtItem.Rows[itemNo-1];
                            dr["Warehouse"] = chosenWarehouse.WhsCode;
                            this.lvStage.DataSource = dtItem;
                            this.lvStage.DataBind();
                        }
                        break;
                    case "EditAccountCallBack":
                        AccountMasterData Account = Session["chosenAccount"] as AccountMasterData;
                        itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                        if (Account != null)
                        {
                            DataRow dr = dtItem.Rows[itemNo-1];
                            dr["Account"] = Account.AcctCode;
                            this.lvStage.DataSource = dtItem;
                            this.lvStage.DataBind();
                        }
                        break;
                    default:
                        break;
                }
            }

        }
        
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
                       
            String requestXML = _collectData();
            SAP.WebServices.Transaction ts = new WebServices.Transaction();
            DataSet ds = ts.CreateMarketingDocument(requestXML);
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
                
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                  "Dialog.hideLoader();", true);
            }
            

        }
        #region "List View Stage"
        
            protected void lvStage_ItemCommand(object sender, ListViewCommandEventArgs e)
            {
                ListViewItem lvi = e.Item;
                switch (e.CommandName)
                {
                    case "Delete":
                        // delete data and update dt
                        int i_idx = e.Item.DataItemIndex;
                        dtItem.Rows.RemoveAt(i_idx);// code for dummy
                        
                        this._StageCancelAddNew();
                        break;
                    case "CancelUpdate":
                        this._StageCancelAddNew();
                        break;
                    case "Update":
                        Label lblNo = (Label)lvi.FindControl("lblNoEdit");
                        foreach (DataRow row in dtItem.Rows)
                        {
                            if (row["No"].ToString().Equals(lblNo.Text))
                            {
                                row["ItemCode"] = ((Label)lvi.FindControl("lblItemCode")).Text;
                                row["ItemName"] = ((Label)lvi.FindControl("lblItemName")).Text;
                                row["Quantity"] = ((TextBox)lvi.FindControl("txtQuantityEdit")).Text;
                                row["Price"] = ((TextBox)lvi.FindControl("txtPriceEdit")).Text;
                                row["Total"] = ((TextBox)lvi.FindControl("txtTotalEdit")).Text;
                                row["Warehouse"] = ((Label)lvi.FindControl("lblWarehouse")).Text;
                                row["Account"] = ((Label)lvi.FindControl("lblAccount")).Text;
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
                dtItem.Rows.Add(No, "", "", 1, "0", "0", "", "");
                this.lvStage.DataSource = dtItem;
                this.lvStage.EditIndex = No-1;// dtItem.Rows.Count - 1;
                this.lvStage.DataBind();
            }
                
            protected void lvStage_ItemUpdating(object sender, ListViewUpdateEventArgs e)
            {
                //Click update link
                this._StageCancelAddNew();
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
                txtRemarks.Text = "";
                txtJournalRemark.Text = "";

                dtItem.Clear();

                lvStage.DataSource = dtItem;
                lvStage.DataBind();
            }
            public String _collectData()
            {
                try
                {
                    DocumentXML objInfo = new DocumentXML("59", this.txtPostingDate.Text, "", this.txtDocumentDate.Text,"", "", User.Identity.Name);

                    for (int i = 0; i < dtItem.Rows.Count; i++)
                    {
                        DataRow row = dtItem.Rows[i];
                        String itemcode = row["ItemCode"].ToString();

                        if (!String.IsNullOrEmpty(itemcode))
                        {
                            String des = row["ItemName"].ToString();
                            String quan = row["Quantity"].ToString();
                            String whscode = row["Warehouse"].ToString();
                            String UnitPrice = row["Price"].ToString();
                            String AccountCode= row["Account"].ToString();
                            Document_LineXML objOrder = new Document_LineXML(itemcode, des, int.Parse(quan.ToString()), 0, whscode, "", double.Parse(UnitPrice.ToString()),AccountCode);
                            objInfo.AddOrderItem(objOrder);
                        }
                    }
                    return objInfo.ToXMLString();
                }
                catch (Exception)
                {
                    throw;
                }

            }
        # endregion 
    }
}