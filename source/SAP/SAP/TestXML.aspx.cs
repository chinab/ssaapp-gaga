using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAP
{
    public partial class TestXML : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGenXML_Click(object sender, EventArgs e)
        {
            PurchaseInfo info = new PurchaseInfo("adm01", "12122012", "12122012", "12122012", "v0001", "sumpro","");
            OrderItem item1 = new OrderItem("i001", "IPhone 5", 5, 100, "apple01", "va1", 15000);
            OrderItem item2 = new OrderItem("i002", "IPad 4", 5, 30, "apple01", "va1", 5000);
            info.AddOrderItem(item1);
            info.AddOrderItem(item2);
            this.TextBox1.Text = info.ToXMLString();
        }
    }
}