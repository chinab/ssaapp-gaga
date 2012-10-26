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
    public partial class SalesOpportunity : System.Web.UI.Page
    {
        public static DataTable dtHeader;
        public static DataTable dtStage;
        public static DataTable dtPartner;
        public static DataTable dtCompetitor;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtHeader = new DataTable();
                dtHeader.Columns.Add("CardCode");
                dtHeader.Columns.Add("SlpCode");
                dtHeader.Columns.Add("OpenDate");
                dtHeader.Columns.Add("Source");
                dtHeader.Columns.Add("Territory");
                dtHeader.Columns.Add("CardName");
                dtHeader.Columns.Add("CprCode");
                dtHeader.Columns.Add("MaxSumLoc");
                dtHeader.Columns.Add("Name");
                dtHeader.Rows.Add("", "", "", "","","", "", "", "");


                dtStage = new DataTable();
                dtStage.Columns.Add("No");
                dtStage.Columns.Add("OpenDate");
                dtStage.Columns.Add("CloseDate");
                dtStage.Columns.Add("ClosePrcnt");
                dtStage.Columns.Add("MaxSumLoc");
                dtStage.Columns.Add("WtSumLoc");
                dtStage.Columns.Add("DocChkbox");
                dtStage.Columns.Add("SlpCode");
                dtStage.Columns.Add("Step_Id");
                dtStage.Columns.Add("DocNumber");
                dtStage.Columns.Add("ObjType");

                dtStage.Columns.Add("DocType");//need to remove
                dtStage.Columns.Add("Stage");//need to remove
                dtStage.Columns.Add("SalesEmployee");//need to remove
                
                this.lvStage.DataSource = dtStage;
                this.lvStage.DataBind();

                dtPartner = new DataTable();
                dtPartner.Columns.Add("No");
                dtPartner.Columns.Add("StartDate");
                dtPartner.Columns.Add("ClosingDate");
                dtPartner.Columns.Add("SalesEmployee");
                dtPartner.Columns.Add("Stage");
                dtPartner.Columns.Add("Percent");
                dtPartner.Columns.Add("PotentialAmt");
                dtPartner.Columns.Add("WeightedAmt");
                dtPartner.Columns.Add("DocType");
                dtPartner.Columns.Add("ShowBP");
                dtPartner.Columns.Add("DocNo");

                this.lvPartner.DataSource = dtPartner;
                this.lvPartner.DataBind();

                dtCompetitor = new DataTable();
                dtCompetitor.Columns.Add("No");
                dtCompetitor.Columns.Add("StartDate");
                dtCompetitor.Columns.Add("ClosingDate");
                dtCompetitor.Columns.Add("SalesEmployee");
                dtCompetitor.Columns.Add("Stage");
                dtCompetitor.Columns.Add("Percent");
                dtCompetitor.Columns.Add("PotentialAmt");
                dtCompetitor.Columns.Add("WeightedAmt");
                dtCompetitor.Columns.Add("DocType");
                dtCompetitor.Columns.Add("ShowBP");
                dtCompetitor.Columns.Add("DocNo");

                this.lvCompetitor.DataSource = dtCompetitor;
                this.lvCompetitor.DataBind();

                MasterData masterDataWS = new MasterData();
                DataSet ds = masterDataWS.GetSalesBuyerMasterData(User.Identity.Name);

                ddlBuyer.DataSource = ds.Tables[0];
                ddlBuyer.DataTextField = "Name";
                ddlBuyer.DataValueField = "Code";
                ddlBuyer.DataBind();

                ds = masterDataWS.GetLevelOfInterest(User.Identity.Name);
                ddlInterest.DataSource = ds.Tables[0];
                ddlInterest.DataTextField = "Descript";
                ddlInterest.DataValueField = "Num";
                ddlInterest.DataBind();

                ds = masterDataWS.GetIndustry(User.Identity.Name);
                ddlIndustry.DataSource = ds.Tables[0];
                ddlIndustry.DataTextField = "IndName";
                ddlIndustry.DataValueField = "IndCode";
                ddlIndustry.DataBind();

                ds = masterDataWS.GetInformationSource(User.Identity.Name);
                ddlSource.DataSource = ds.Tables[0];
                ddlSource.DataTextField = "Descript";
                ddlSource.DataValueField = "Num";
                ddlSource.DataBind();
            }
        }
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            if (!IsPostBack)
            {
                //this._ddlCurency_SelectedIndexChanged(null, null);
            }
            // get callback from popup
            if (this.Request["__EVENTARGUMENT"] != null && this.Request["__EVENTARGUMENT"].ToString() != "")
            {
                switch (this.Request["__EVENTARGUMENT"].ToString())
                {
                    case "EditVendorCallBack":
                        BusinessPartner chosenPartner = Session["chosenPartner"] as BusinessPartner;
                        if (chosenPartner != null)
                        {
                            this.txtCustomerName.Text = chosenPartner.CardName;
                            this.txtCustomerCode.Text = chosenPartner.CardCode;
                            this.txtStatus.Text = "Open";
                            this.txtStatus.Enabled = false;


                            this.txtStartDate.Text = DateTime.Now.ToShortDateString();
                            this.txtPredDate.Text = DateTime.Now.ToShortDateString();
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
                    case "EditEmployeeCallBack":
                        EmployeeMasterData employee = Session["chosenEmployee"] as EmployeeMasterData;
                        if (employee != null)
                        {
                            this.txtOwner.Text = employee.FirstName + " " + employee.MidName + " " + employee.LastName;
                            this.txtOwnerCode.Text = employee.Code;
                        }
                        break;
                    case "EditTerritoryCallBack":
                        Territory Terr = Session["chosenTerritory"] as Territory;
                        if (Terr != null)
                        {
                            this.txtTerritory.Text = Terr.descript.ToString();
                            this.txtterritryID.Text = Terr.territryID.ToString();
                        }
                        break;
                    case "EditProjectCallBack":
                        Project Proj = Session["chosenProject"] as Project;
                        if (Proj != null)
                        {
                            this.txtBPProjectCode.Text = Proj.PrjCode.ToString();
                            this.txtBPProjectName.Text = Proj.PrjName.ToString();
                        }
                        break;
                    default:
                        break;
                }
            }

        }
      
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (txtCustomerCode.Text=="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "OKErrors",
                    "Main.setMasterMessage('" + "Missing Business Partner!" + "','');", true);
                return;
            }
            
            String requestXML = _collectData();
            SAP.WebServices.Transaction ts = new WebServices.Transaction();
            DataSet ds = ts.CreateMarketingDocument(requestXML, User.Identity.Name, "97", "", false);
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
        #region "List View Stage"
        
        protected void lvStage_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            ListViewItem lvi = e.Item;
            switch (e.CommandName)
            {
                case "CancelAddNew":
                    this._StageCancelAddNew();
                    this.lvStage.DataBind();
                    break;
                case "Cancel":
                    this.lvStage.EditIndex = -1;
                    this.lvStage.DataSource = dtStage;
                    this.lvStage.DataBind();
                    break;
                case "Update":
                    Label lblNo = (Label)lvi.FindControl("lblNoEdit");
                    foreach (DataRow row in dtStage.Rows)
                    {
                        if (row["No"].ToString().Equals(lblNo.Text))
                        {
                            row["OpenDate"] = ((TextBox)lvi.FindControl("txtStartDateEdit")).Text;
                            row["CloseDate"] = ((TextBox)lvi.FindControl("txtClosingDateEdit")).Text;
                            row["SalesEmployee"] = ((DropDownList)lvi.FindControl("ddlSalesEmployeeEdit")).SelectedItem.Text;
                            row["Stage"] = ((DropDownList)lvi.FindControl("ddlStageEdit")).SelectedItem.Text;
                            row["ClosePrcnt"] = ((TextBox)lvi.FindControl("txtPercentEdit")).Text;
                            row["MaxSumLoc"] = ((TextBox)lvi.FindControl("txtPotentialAmtEdit")).Text;
                            row["WtSumLoc"] = ((TextBox)lvi.FindControl("txtWeightedAmtEdit")).Text;
                            row["DocType"] = ((DropDownList)lvi.FindControl("ddlDocTypeEdit")).SelectedItem.Text;

                            row["SlpCode"] = ((DropDownList)lvi.FindControl("ddlSalesEmployeeEdit")).SelectedValue;
                            row["Step_Id"] = ((DropDownList)lvi.FindControl("ddlStageEdit")).SelectedValue;
                            row["ObjType"] = ((DropDownList)lvi.FindControl("ddlDocTypeEdit")).SelectedValue;
                            break;
                        }
                    }
                    this.lvStage.EditIndex = -1;
                    this.lvStage.DataSource = dtStage;
                    this.lvStage.DataBind();
                    break;
                case "Delete":
                    // delete data and update dt
                    int i_idx = e.Item.DataItemIndex;
                    dtStage.Rows.RemoveAt(i_idx);// code for dummy

                    this._StageCancelAddNew();
                    break;
                default:
                    break;
            }
        }

        private void _StageCancelAddNew()
        {
            this.lvStage.InsertItemPosition = InsertItemPosition.None;
            this.btnAddRecord.Enabled = true;
            this.lvStage.EditIndex = -1;
            this.lvStage.DataSource = dtStage;
            this.lvStage.DataBind();
        }

        protected void lvStage_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            
            this.lvStage.EditIndex = e.NewEditIndex;
            this.lvStage.DataSource = dtStage;
            this.lvStage.DataBind();
        }

        protected void lvStage_ItemInserted(object sender, ListViewInsertedEventArgs e)
        {
            this._StageCancelAddNew();
        }

        protected void lvStage_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            string StartDate = "", ClosingDate = "", SalesEmployee = "", Stage = "", Percent="", PotentialAmt = "", 
                WeightedAmt = "", DocType = "", ShowBP = "", DocNo="",SalesEmployeeCode="",StageCode="",DocTypeCode="";

            TextBox txt = (e.Item.FindControl("txtStartDateInsert")) as TextBox;
            if (txt != null)
                StartDate = txt.Text;
            
            txt = (e.Item.FindControl("txtClosingDateInsert")) as TextBox;
            if (txt != null)
                ClosingDate = txt.Text;

            DropDownList ddl = (e.Item.FindControl("ddlSalesEmployeeInsert")) as DropDownList;
            if (ddl != null)
            {
                SalesEmployeeCode = ddl.SelectedValue;
                SalesEmployee = ddl.SelectedItem.Text;
            }
            ddl = (e.Item.FindControl("ddlStageInsert")) as DropDownList;
            if (ddl != null)
            {
                StageCode = ddl.SelectedValue;
                Stage = ddl.SelectedItem.Text;
            }
            txt = (e.Item.FindControl("txtPercentInsert")) as TextBox;
            if (txt != null)
                Percent = txt.Text;

            txt = (e.Item.FindControl("txtPotentialAmtInsert")) as TextBox;
            if (txt != null)
                PotentialAmt = txt.Text;
            
            txt = (e.Item.FindControl("txtWeightedAmtInsert")) as TextBox;
            if (txt != null)
                WeightedAmt = txt.Text;

            ddl = (e.Item.FindControl("ddlDocTypeInsert")) as DropDownList;
            if (ddl != null)
            {
                DocTypeCode = ddl.SelectedValue;
                DocType = ddl.SelectedItem.Text;
            }
            CheckBox ck = (e.Item.FindControl("ckShowBPInsert")) as CheckBox;
            if ((ddl != null))
                ShowBP = "Y";

            txt = (e.Item.FindControl("txtDocNoInsert")) as TextBox;
            if (txt != null)
                DocNo = txt.Text;

            dtStage.Rows.Add(GetNo(), StartDate, ClosingDate, SalesEmployee, Stage, Percent, PotentialAmt, WeightedAmt, 
                        DocType, ShowBP, DocNo, SalesEmployeeCode,StageCode,DocTypeCode);
            this.lvStage.DataSource = dtStage;
            this.lvStage.DataBind();
            this._StageCancelAddNew();
        }

        protected void _btnAddRecord_Click(object sender, EventArgs e)
        {
            this.lvStage.InsertItemPosition = InsertItemPosition.FirstItem;
            this.btnAddRecord.Enabled = false;
            this.lvStage.EditIndex = -1;
            this.lvStage.DataSource = dtStage;
            this.lvStage.DataBind();
        }
      
        protected void lvStage_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.InsertItem)
            {
                DropDownList ddl = (DropDownList)e.Item.FindControl("ddlSalesEmployeeInsert"); if (ddl != null)
                {
                    MasterData masterDataWS = new MasterData();
                    DataSet salesBuyers = masterDataWS.GetSalesBuyerMasterData(User.Identity.Name);
                    ddl.DataSource = salesBuyers.Tables[0];
                    ddl.DataTextField = "Name";
                    ddl.DataValueField = "Code";
                    ddl.DataBind();
                }
                ddl = (DropDownList)e.Item.FindControl("ddlStageInsert"); if (ddl != null)
                {
                    MasterData masterDataWS = new MasterData();
                    DataSet salesBuyers = masterDataWS.GetStage(User.Identity.Name);
                    ddl.DataSource = salesBuyers.Tables[0];
                    ddl.DataTextField = "Descript";
                    ddl.DataValueField = "Num";
                    ddl.DataBind();
                }
            }
        }
        
        protected void lvStage_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
           

            //dtStage.Rows.Add(1, StartDate, ClosingDate, SalesEmployee, Stage, Percent, PotentialAmt, WeightedAmt, DocType, ShowBP, DocNo);
            this.lvStage.EditIndex = -1;
            this.lvStage.DataSource = dtStage;
            this.lvStage.DataBind();
        }
       
        protected void lvStage_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (lvStage.EditIndex >= 0)
            {
                ListViewDataItem dataItem = (ListViewDataItem)e.Item;
                if (dataItem.DisplayIndex == lvStage.EditIndex)
                {
                    DropDownList list = (DropDownList)dataItem.FindControl("lstEditCountry");

                    DropDownList ddl = (DropDownList)e.Item.FindControl("ddlSalesEmployeeEdit"); if (ddl != null)
                    {
                        MasterData masterDataWS = new MasterData();
                        DataSet salesBuyers = masterDataWS.GetSalesBuyerMasterData(User.Identity.Name);
                        ddl.DataSource = salesBuyers.Tables[0];
                        ddl.DataTextField = "Name";
                        ddl.DataValueField = "Code";
                        ddl.DataBind();
                    }
                    ddl = (DropDownList)e.Item.FindControl("ddlStageEdit"); if (ddl != null)
                    {
                        MasterData masterDataWS = new MasterData();
                        DataSet salesBuyers = masterDataWS.GetStage(User.Identity.Name);
                        ddl.DataSource = salesBuyers.Tables[0];
                        ddl.DataTextField = "Descript";
                        ddl.DataValueField = "Num";
                        ddl.DataBind();
                    }

                }
            }
        }
        # endregion
        #region "Functions"
        void ClearScreen()
        {
            txtCustomerCode.Text = "";
            txtCustomerName.Text = "";
            ddlContactPerson.DataSource = null;
            txtOpportunityName.Text = "";

            dtStage.Clear();
            dtCompetitor.Clear();
            dtPartner.Clear();

            lvStage.DataSource = dtStage;
            lvStage.DataBind();
        }
        public String _collectData()
        {
            try
            {
                //Update table header
                DataRow dr = dtHeader.Rows[0];
                dr["CardCode"] = txtCustomerCode.Text;
                dr["SlpCode"] = ddlBuyer.Text;
                dr["OpenDate"] = txtStartDate.Text;
                dr["Source"] = "";
                dr["Territory"] = "";
                dr["CardName"] = "";
                dr["CprCode"] = "";
                dr["OpenDate"] = "";
                dr["MaxSumLoc"] = txtPotentialAmt.Text;
                dr["Name"] = "";

                //Remove row with empty itemcode
                foreach (DataRow row in dtStage.Rows)
                {
                    if (row["ItemCode"].ToString() == "")
                    {
                        row.Delete();
                    }
                }
                DocumentXML objInfo = new DocumentXML();
                String RemoveColumn = "No;DocType;Stage;SalesEmployee";
                return objInfo.ToXMLStringFromDS("97", dtHeader, dtStage, RemoveColumn);

            }
            catch (Exception)
            {
                throw;
            }

        }
        private int GetNo()
        {
            return dtStage.Rows.Count + 1;
        }
        # endregion 
    }
}