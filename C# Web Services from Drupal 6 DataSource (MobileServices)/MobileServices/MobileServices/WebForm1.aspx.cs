using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;

namespace MobileServices
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            mobileservices.MobileServices client = new mobileservices.MobileServices();
            output.Text = client.Url;
            mobileservices.SearchResponse res = client.SearchHotels("chicago", "", "", "");
            output.Text += "<br />Hotel Count" + res.HotelIds.Count().ToString();
            mobileservices.Summary s = client.GetSummary("9611");
            output.Text += "<br />Hotel Count" + s.HotelName;
            mobileservices.Detail d = client.GetDetail("9611");
            output.Text += "<br />Hotel Count" + d.FromAndrewHarper;
        }
    }
}
