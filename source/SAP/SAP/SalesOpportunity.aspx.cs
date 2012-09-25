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
    public partial class SalesOpportunity : System.Web.UI.Page
    {
        public static DataTable dtStage;
        public static DataTable dtPartner;
        public static DataTable dtCompetitor;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtStage = new DataTable();
                dtStage.Columns.Add("No");
                dtStage.Columns.Add("StartDate");
                dtStage.Columns.Add("ClosingDate");
                dtStage.Columns.Add("SalesEmployee");
                dtStage.Columns.Add("Stage");
                dtStage.Columns.Add("Percent");
                dtStage.Columns.Add("PotentialAmt");
                dtStage.Columns.Add("WeightedAmt");
                dtStage.Columns.Add("DocType");
                dtStage.Columns.Add("ShowBP");
                dtStage.Columns.Add("DocNo");

                //for (int i = 0; i < 2; i++)
                //    dtStage.Rows.Add(i, "20120924", "20120924", "0", "1", "10", "1000000", "0", "-1", "N", "0");
                //this.lvStage.DataSourceID = "dtStage";
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
                DataSet ds = masterDataWS.GetSalesBuyerMasterData();

                ddlBuyer.DataSource = ds.Tables[0];
                ddlBuyer.DataTextField = "Name";
                ddlBuyer.DataValueField = "Code";
                ddlBuyer.DataBind();

                ds = masterDataWS.GetLevelOfInterest();
                ddlInterest.DataSource = ds.Tables[0];
                ddlInterest.DataTextField = "Descript";
                ddlInterest.DataValueField = "Num";
                ddlInterest.DataBind();

                ds = masterDataWS.GetIndustry();
                ddlIndustry.DataSource = ds.Tables[0];
                ddlIndustry.DataTextField = "IndName";
                ddlIndustry.DataValueField = "IndCode";
                ddlIndustry.DataBind();

                ds = masterDataWS.GetInformationSource();
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
                            DataSet contactPersons = masterDataWS.GetContactPerson(chosenPartner.CardCode);
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
        public String _collectData()
        {
            try
            {
                OpportunityXML objInfo = new OpportunityXML("97", this.txtStartDate.Text,txtPredDate.Text, this.txtCustomerCode.Text, User.Identity.Name
                    ,txtPotentialAmt.Text);

                for (int i = 0; i < dtStage.Rows.Count; i++)
                {
                    DataRow row = dtStage.Rows[i];
                    int stagecode = int.Parse(row["Stage"].ToString());
                    if (stagecode!=0)
                    {
                        String StartDate=row["StartDate"].ToString();
                        String ClosingDate=null;

                        if (!String.IsNullOrEmpty(row["ClosingDate"].ToString()))
                        {
                            ClosingDate = row["ClosingDate"].ToString();
                        }
                        int SalesEmployee=0;

                        if (!String.IsNullOrEmpty(row["SalesEmployee"].ToString()))
                        {
                            SalesEmployee = int.Parse(row["SalesEmployee"].ToString());
                        }
                        Double Percent = Double.Parse(row["Percent"].ToString()); 
                        Double PotentialAmt = Double.Parse(row["PotentialAmt"].ToString());
                        Double WeightedAmt = Double.Parse(row["WeightedAmt"].ToString()); 
                        String DocType = row["DocType"].ToString(); 
                        int DocNo = int.Parse(row["DocNo"].ToString());
                        String ShowBP=row["ShowBP"].ToString();

                        Opportunity_StageXML objOrder = new Opportunity_StageXML(StartDate,ClosingDate,stagecode,Percent,DocNo,ShowBP,DocType,PotentialAmt,SalesEmployee,WeightedAmt);
                        objInfo.AddStageLine(objOrder);
                    }
                }
                return objInfo.ToXMLString();
            }
            catch (Exception)
            {
                throw;
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
            DataSet ds = ts.CreateOpportunity(requestXML);
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
            }
            
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "CloseLoading",
                              "Dialog.hideLoader();", true);
        }
        #region "List View Stage"
        
        protected void lvStage_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
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
                    this.lvStage.EditIndex = -1;
                    this.lvStage.DataSource = dtStage;
                    this.lvStage.DataBind();
                    break;
                case "Delete":
                    dtStage.Rows.RemoveAt(0);// code for dummy
                    this.lvStage.EditIndex = -1;
                    this.lvStage.DataSource = dtStage;
                    this.lvStage.DataBind();
                    break;
                default:
                    break;
            }
        }

        private void _StageCancelAddNew()
        {
            this.lvStage.InsertItemPosition = InsertItemPosition.None;
            this.btnAddRecord.Enabled = true;
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
            DataRow newRow = dtStage.NewRow();
            dtStage.Rows.Add(newRow);
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
            //if (e.Item.ItemType == ListViewItemType.InsertItem)
            //{
            //    DropDownList ddl = (DropDownList)e.Item.FindControl("ddlSalesEmployee"); if (ddl != null)
            //    {
            //        MasterData masterDataWS = new MasterData();
            //        DataSet salesBuyers = masterDataWS.GetSalesBuyerMasterData();
            //        ddl.DataSource = salesBuyers.Tables[0];
            //        ddl.DataTextField = "Name";
            //        ddl.DataValueField = "Code";
            //        ddl.DataBind();
            //    }
            //    ddl = (DropDownList)e.Item.FindControl("ddlStage"); if (ddl != null)
            //    {
            //        MasterData masterDataWS = new MasterData();
            //        DataSet salesBuyers = masterDataWS.GetStage();
            //        ddl.DataSource = salesBuyers.Tables[0];
            //        ddl.DataTextField = "Descript";
            //        ddl.DataValueField = "Num";
            //        ddl.DataBind();
            //    }
            //}
        }
        

        protected void lvStage_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            
        }
        # endregion

        protected void lvStage_ItemUpdated(object sender, ListViewUpdatedEventArgs e)
        {
            if (lvStage.EditIndex>=0)
            {
                //TextBox txt = new TextBox();
                //txt = (TextBox)lvStage.Items[e.].FindControl("txtPercent");
                //Label lbl = new Label();
                //lbl = (Label)lvStage.Items[e.ItemIndex].FindControl("lblPercent");
                //lbl.Text = txt.Text;
            }
        }
    }
}