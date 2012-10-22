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

namespace SAP
{
    public partial class JournalEntry : System.Web.UI.Page
    {
        public static DataTable dtContents;
        public static DataTable dtHeader;
        private GeneralFunctions GF;
        private string DocType = "30";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                this.txtDueDate.Text = DateTime.Now.ToShortDateString();
                this.txtPostingDate.Text = DateTime.Now.ToShortDateString();
                this.txtDocDate.Text = DateTime.Now.ToShortDateString();

                dtHeader = new DataTable();
                dtHeader.Columns.Add("RefDate");
                dtHeader.Columns.Add("DueDate");
                dtHeader.Columns.Add("TaxDate");
                dtHeader.Columns.Add("Memo");
                dtHeader.Columns.Add("Ref1");
                dtHeader.Columns.Add("Ref2");
                dtHeader.Columns.Add("Ref3");
                dtHeader.Rows.Add("", "", "", "From SBO WEB", "", "", "");

                dtContents = new DataTable();
                dtContents.Columns.Add("No");
                dtContents.Columns.Add("Account");
                dtContents.Columns.Add("Dscription");
                dtContents.Columns.Add("Debit");
                dtContents.Columns.Add("Credit");
               
                this.lvContents.DataSource = dtContents;
                this.lvContents.DataBind();

                MasterData masterDataWS = new MasterData();
                GF = new GeneralFunctions(User.Identity.Name);

                ClearScreen();
            }
        }

        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            
            // get callback from popup
            if (this.Request["__EVENTARGUMENT"] != null && this.Request["__EVENTARGUMENT"].ToString() != "")
            {
                Int32 itemNo = 0;
                if (GF == null) GF = new GeneralFunctions(User.Identity.Name);

                switch (this.Request["__EVENTARGUMENT"].ToString())
                {
                    case "EditAccountCallBack":
                        AccountMasterData chosenAcctCode = Session["chosenAccount"] as AccountMasterData;
                        itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                        if (chosenAcctCode != null)
                        {
                            // update grid
                            dtContents.Rows[itemNo - 1]["Account"] = chosenAcctCode.AcctCode;
                            dtContents.Rows[itemNo - 1]["Dscription"] = chosenAcctCode.AcctName;

                            this.lvContents.DataSource = dtContents;
                            this.lvContents.DataBind();
                        }
                        break;
                    case "EditTaxCodeCallBack":
                        TaxGroup chosenTaxCode = Session["chosenTaxCode"] as TaxGroup;
                        itemNo = Int32.Parse(Session["chosenItemNo"] as String);
                        if (chosenTaxCode != null)
                        {
                            // update grid
                            dtContents.Rows[itemNo - 1]["TaxCode"] = chosenTaxCode.Code;
                            dtContents.Rows[itemNo - 1]["VatPrcnt"] = chosenTaxCode.Rate;

                            this.lvContents.DataSource = dtContents;
                            this.lvContents.DataBind();
                        }
                        break;
                    case "EditVendorCallBack":
                        BusinessPartner chosenPartner = Session["chosenPartner"] as BusinessPartner;
                        if (chosenPartner != null)
                        {
                            
                        }
                        break;
                    
                    default:
                        break;
                }
            }

        }

        #region Functions
        private int GetNo()
        {
            return dtContents.Rows.Count + 1;
        }

        protected void ClearScreen()
        {
            dtContents.Clear();
        }
        private void ResetLineNo()
        {
            int i = 0;
            foreach (DataRow row in dtContents.Rows)
                row["No"] = ++i;
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

        public String _collectData()
        {
            try
            {
                if (GF == null) GF = new GeneralFunctions(User.Identity.Name);
                //Update table header

                DataRow dr = dtHeader.Rows[0];
                dr["RefDate"] = String.Format("{0:yyyyMMdd}", DateTime.Parse(txtPostingDate.Text));
                dr["DueDate"] = String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDueDate.Text));
                dr["TaxDate"] = String.Format("{0:yyyyMMdd}", DateTime.Parse(txtDocDate.Text));
                dr["Memo"] = txtRemarks.Text;
                dr["Ref1"] = txtRef1.Text;
                dr["Ref2"] = txtRef2.Text;
                dr["Ref3"] = txtRef3.Text;

                DocumentXML objInfo = new DocumentXML();
                String RemoveColumn = "No;Dscription";
                Array arrContentsCols = new string[] { "Debit", "Credit" }; // Columns need to reset format numeric
                return objInfo.ToXMLStringFromDS(DocType, dtHeader, GF.ResetFormatNumeric(dtContents, arrContentsCols), RemoveColumn);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Event
        
        protected void _btnAddRecord_Click(object sender, EventArgs e)
        {
            int iNo = GetNo();
            dtContents.Rows.Add(iNo, "", "", "0", "0");
            this.lvContents.DataSource = dtContents;

            int lastpage = this.ProductListPagerCombo.TotalRowCount / this.ProductListPagerCombo.PageSize;
            this.ProductListPagerCombo.SetPageProperties(lastpage * this.ProductListPagerCombo.PageSize, this.ProductListPagerCombo.MaximumRows, false);
            this.lvContents.EditIndex = iNo - 1;
            this.lvContents.DataBind();
            SetControlsStatus("Add");
        }

        #region item command
        protected void lvContents_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            ListViewItem lvi = e.Item;
            switch (e.CommandName)
            {
                case "CancelAddNew":
                    this._cancelAddNew();
                    this.lvContents.DataBind();
                    break;
                case "CancelUpdate":
                    this.lvContents.EditIndex = -1;
                    this.lvContents.DataSource = dtContents;
                    this.lvContents.DataBind();
                    break;
                case "Update":
                    // update new data to dt
                    if (string.IsNullOrEmpty(((Label)lvi.FindControl("lblAcctCode")).Text)) return;
                   
                    Label lblNo = (Label)lvi.FindControl("lblNo");
                    foreach(DataRow row in dtContents.Rows)
                    {
                        if (row["No"].ToString().Equals(lblNo.Text)) // update currentrow into data table
                        {
                            row["Account"] = ((Label)lvi.FindControl("lblAcctCode")).Text;
                            row["Dscription"] = ((Label)lvi.FindControl("lblDescription")).Text;
                            row["Debit"] = ((TextBox)lvi.FindControl("txtDebit")).Text;
                            row["Credit"] = ((TextBox)lvi.FindControl("txtCredit")).Text;
                            break;
                        }
                    }
                    this.lvContents.EditIndex = -1;
                    this.lvContents.DataSource = dtContents;
                    this.lvContents.DataBind();
                    SetControlsStatus("Update");
                    break;

                case "DeleteItem":
                    // delete data and update dt
                    int i_idx = e.Item.DataItemIndex;
                    dtContents.Rows.RemoveAt(i_idx);
                    this.lvContents.EditIndex = -1;
                    ResetLineNo();
                    this.lvContents.DataSource = dtContents;
                    this.lvContents.DataBind();
                    break;
                    
                default: break;
            }
        }
        #endregion

        #region edit item
        protected void lvContents_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            this.lvContents.EditIndex = e.NewEditIndex;

            if (GF == null) GF = new GeneralFunctions(User.Identity.Name);
            // Reset FormatNumeric 
            string Debit = GF.ResetFormatNumeric(dtContents.Rows[e.NewEditIndex]["Debit"].ToString());        
            string Credit = GF.ResetFormatNumeric(dtContents.Rows[e.NewEditIndex]["Credit"].ToString());    

            dtContents.Rows[e.NewEditIndex]["Debit"] = Debit;
            dtContents.Rows[e.NewEditIndex]["Credit"] = Credit;

            this.lvContents.DataSource = dtContents;
            this.lvContents.DataBind();
            SetControlsStatus("Edit");
        }
        #endregion

        #region insert item
        protected void lvContents_ItemInserted(object sender, ListViewInsertedEventArgs e)
        {
            this._cancelAddNew();
        }
        #endregion

        protected void imgbCancel_CancelAddNew(object sender, EventArgs e)
        {
            this.lvContents.DataSource = dtContents;
            this._cancelAddNew();
        }

        private void _cancelAddNew()
        {
            this.lvContents.InsertItemPosition = InsertItemPosition.None;
            this.btnAddRecord.Enabled = true;
            this.lvContents.DataBind();
        }
        protected void lvContents_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            Label lblCode = (Label)lvContents.Items[e.ItemIndex].FindControl("lblAcctCode");
            if (lblCode == null || string.IsNullOrEmpty(lblCode.Text))
            {
                e.Cancel = true;
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

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            String simulate = System.Configuration.ConfigurationManager.AppSettings["Simulate"];
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
            SetControlsStatus("Save");
        }
        #endregion

        
    }
}