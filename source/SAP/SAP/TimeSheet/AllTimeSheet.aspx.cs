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
    public partial class AllTimeSheet : System.Web.UI.Page
    {
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtFromDate.Text = DateTime.Now.ToShortDateString();
                this.txtToDate.Text = DateTime.Now.ToShortDateString();
                Binding();
            }
        }

        void Binding()
        {
            Reports rpt = new Reports();
            CultureInfo ivC = new System.Globalization.CultureInfo("es-US");
            ds = rpt.TimeSheet_All(User.Identity.Name, Convert.ToDateTime(txtFromDate.Text, ivC), Convert.ToDateTime(txtToDate.Text, ivC));

            DataView dv = new DataView(ds.Tables[0]);
            DataView dv1 = new DataView(ds.Tables[0]);

            dv.RowFilter="Closed='N'";
            this.lvStage.DataSource = dv;
            this.lvStage.DataBind();

            this.lvAll.DataSource =dv1;
            this.lvAll.DataBind();
        }
        protected void ProductListPagerCombo_PreRender(object sender, EventArgs e)
        {
            Binding();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            Binding();
        }

        protected void lvStage_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                LinkButton lb = (LinkButton)e.CommandSource;
                Response.Redirect("/TimeSheet/ABEO_TIMESHEET.aspx?clgCode=" + lb.CommandArgument);
            }
        }

        protected void lvAll_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                LinkButton lb = (LinkButton)e.CommandSource;
                Response.Redirect("/TimeSheet/ABEO_TIMESHEET.aspx?clgCode=" + lb.CommandArgument);
            }
        }
    }
}