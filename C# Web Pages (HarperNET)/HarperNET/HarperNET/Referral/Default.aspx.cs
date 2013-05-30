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
using HarperCRYPTO;
public partial class Referral_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(Request["MemberId"]))
        {
            string mbr = Cryptography.EncryptData(Request["MemberId"]);
            string key = "POTRIAL";
            string pub = "PO";
            Response.Redirect(string.Format("Refer.aspx?MemberId={0}&KeyCode={1}&PubCode={2}", new object[] { mbr, key, pub }));
        }
    }
}
