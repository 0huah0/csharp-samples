using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using HarperCRYPTO;

public partial class Default : System.Web.UI.Page
{
    HarperMembershipService.GetMemberResponse MemberResponse = null;
    HarperSecureService.CreateLoginResponse LoginResponse = null;
    HarperMembershipService.MembershipService MembershipClient = new HarperMembershipService.MembershipService();
    HarperSecureService.SecureServices SecureClient = new HarperSecureService.SecureServices();
    /* 
     * web.config allows public access to this directory via
     * 
    <location path="App/Public/">
        <system.web>
            <authorization>
                <allow users="*"/>
            </authorization>
        </system.web>
    </location>
     * 
     */

    protected void Page_Load(object sender, EventArgs e){}

    private void ClearForm()
    {
        txtMemberId.Text = string.Empty;
        txtZip.Text = string.Empty;

        lblMessage.Text = string.Empty;
        lblMessage.Visible = false;
    }
    private void SetError(Exception ex)
    {
        ClearForm();
        StaticMethods.LogError("CreateUsername/Default.aspx", ex.Message, ex.StackTrace);
        lblMessage.Text = string.Format("We encountered an error while trying to find your membership information.<br /><br />{0}", new object[] { Resources.GlobalStrings.ContactUs });
        lblMessage.Visible = true;
    }

    protected void btnLookUp_Click(object sender, EventArgs e)
    {
        try
        {
            HarperMembershipService.BaseResponse response = MembershipClient.GetMemberBySFGId(Cryptography.Hash(txtMemberId.Text.Trim().Replace("-", "").Replace(" ", ""),true));
            MemberResponse = response.TypedResponse as HarperMembershipService.GetMemberResponse;
            string sfg_zip = MemberResponse.MemberData.Address.PostalCode;
            if (sfg_zip.Contains("-"))
            {
                sfg_zip = sfg_zip.Split('-')[0];
            }
            string mbr_zip = txtZip.Text.Trim();
            if (mbr_zip.Contains("-"))
            {
                mbr_zip = mbr_zip.Split('-')[0];
            }
            if (sfg_zip == mbr_zip && MemberResponse.MemberData.MemberId.Length > 0)
            {
                Session["MemberId"] = Cryptography.Hash(MemberResponse.MemberData.MemberId, true);
                Session["Zip"] = Cryptography.Hash(txtZip.Text.Trim().Replace("-", "").Replace(" ", ""), true);
                Response.Redirect("UserConfirm.aspx", false);
            }
            else
            {
                SetError(new Exception("Zip code doesn't match or SFG id is empty"));
            }
        }
        catch (Exception ex)
        {
            SetError(ex);
        }
    }
}
