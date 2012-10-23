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
    public partial class MyOpenTimeSheet : System.Web.UI.Page
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
            ds = rpt.TimeSheet_OpenList(User.Identity.Name, DateTime.Parse(txtFromDate.Text), DateTime.Parse(txtToDate.Text));

            this.lvStage.DataSource = ds.Tables[0];
            this.lvStage.DataBind();
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
                Response.Redirect("/TimeSheet/ABEO_TIMESHEET.aspx?clgCode=1");//+ ((Label)lvStage.FindControl("ClgCode")).Text);
            }
        }
    }
}