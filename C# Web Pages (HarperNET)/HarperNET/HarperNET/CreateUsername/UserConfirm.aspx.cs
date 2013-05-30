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

public partial class UserConfirm : System.Web.UI.Page
{
    HarperMembershipService.GetMemberResponse MemberResponse = null;
    HarperSecureService.CreateLoginResponse LoginResponse = null;
    HarperMembershipService.MembershipService MembershipClient = new HarperMembershipService.MembershipService();
    HarperSecureService.SecureServices SecureClient = new HarperSecureService.SecureServices();

    protected string CustomerID
    {
        get
        {
            return Cryptography.DeHash(Session["MemberId"].ToString(), true);
        }
    }
    protected string Zip
    {
        get
        {
            return Cryptography.DeHash(Session["Zip"].ToString(), true);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                ClearForm();
                PopulateForm();
            }
        }
        catch (Exception ex)
        {
            SetError(ex);
        }
    }
    private void ClearForm()
    {
        lblPassport.Text = string.Empty;
        lblFirstName.Text = string.Empty;
        lblLastName.Text = string.Empty;
        lblAddress1.Text = string.Empty;
        lblAddress2.Text = string.Empty;
        lblCity.Text =string.Empty;
        lblZip.Text = string.Empty;

        lblMessage.Text = string.Empty;
        lblMessage.Visible = false;
        memberdata.Visible = true;
    }
    private void SetError(Exception ex)
    {
        ClearForm();
        StaticMethods.LogError("CreateUsername/UserConfirm.aspx", ex.Message, ex.StackTrace);
        memberdata.Visible = false;
        lblMessage.Text = string.Format("We encountered an error while trying to create your username.<br /><br />{0}", new object[] { Resources.GlobalStrings.ContactUs });
        lblMessage.Visible = true;
    }
    protected void PopulateForm()
    {
        try
        {
            HarperMembershipService.BaseResponse response = MembershipClient.GetMemberBySFGId(Cryptography.Hash(CustomerID, true));
            MemberResponse = response.TypedResponse as HarperMembershipService.GetMemberResponse;
            Session["MemberResponse"] = MemberResponse;
            if (!MemberResponse.WebAccountFound || CustomerID == "10001636220")//the check on customer id (mcoupland) was put in so Agatha could step through the process and style the pages
            {
                lblPassport.Text = MemberResponse.MemberData.MemberId;
                lblFirstName.Text = MemberResponse.MemberData.FirstName;
                lblLastName.Text = MemberResponse.MemberData.LastName;
                lblAddress1.Text = MemberResponse.MemberData.Address.Address1;
                lblAddress2.Text = MemberResponse.MemberData.Address.Address2;
                lblCity.Text = MemberResponse.MemberData.Address.City;
                lblZip.Text = MemberResponse.MemberData.Address.PostalCode;
            }
            else
            {
                SetError(new Exception(string.Format("Unable to create username. Member id {0} already has a username at SFG.", MemberResponse.MemberData.MemberId)));
                lblMessage.Text = string.Format("We encountered an error while trying to create your username.<br />{0}<br /><br />{1}", new object[] { "A user account has already been created for this member number.", Resources.GlobalStrings.ContactUs}); 
                lblMessage.Visible = true;
            }
        }
        catch (Exception e)
        {
            SetError(e);
        }
    }

    protected void CorrectInformation_Click(object sender, EventArgs e)
    {
        string cid = Cryptography.Hash(lblPassport.Text);
        Response.Redirect(string.Format("CreateUserName.aspx?cusID={0}&zip={1}", new object[] {cid, lblZip.Text }));
    }
}

