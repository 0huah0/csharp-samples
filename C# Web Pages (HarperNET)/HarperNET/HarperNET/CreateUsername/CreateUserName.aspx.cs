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
using HarperLINQ;
using System.Linq;

public partial class CreateUserName : System.Web.UI.Page
{
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
    protected HarperMembershipService.GetMemberResponse MemberResponseObject
    {
        get
        {
            return (HarperMembershipService.GetMemberResponse)Session["MemberResponse"];
        }
    }
    private void ClearForm()
    {
        txtUserName.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtConfirmEmail.Text = string.Empty;
        txtConfirmPassword.Text = string.Empty;

        lblMessage.Text = string.Empty;
        lblMessage.Visible = false;
        memberdata.Visible = true;
    }
    private void SetError(Exception ex)
    {
        ClearForm();
        StaticMethods.LogError("CreateUsername/CreateUserName.aspx", ex.Message, ex.StackTrace);
        if (ex.Message.Contains("Username in use"))
        {
            lblMessage.Text = "We're sorry, the username you selected is in use.  Please select a different username.";
        }
        else
        {
            lblMessage.Text = string.Format("We encountered an error while trying to create your username.<br /><br />{0}", new object[] { Resources.GlobalStrings.ContactUs });
        }
        lblMessage.Visible = true;
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (CustomerID == "10001636220")//the check on customer id (mcoupland) was put in so Agatha could step through the process and style the pages
            {
                Success();
            }
            else
            {
                tbl_Customer existing = new tbl_Customer(txtUserName.Text, true);
                if (existing.cusID > 0)
                {
                    throw new Exception("Username in use");
                }
                existing = new tbl_Customer(txtEmail.Text, false);
                if (existing.cusID > 0)
                {
                    throw new Exception("Email in use");
                }
                long memberid = 0;
                if (long.TryParse(CustomerID, out memberid))
                {
                    existing = new tbl_Customer(memberid, true);
                    if (existing.cusID > 0)
                    {
                        throw new Exception("Member Number in use");
                    }
                }
                else
                {
                    throw new Exception("Invalid Member Number");
                }

                if (CreateNewUser())
                {
                    Success();
                }
                else
                {
                    throw new Exception("Error saving user data");
                }
            }
        }
        catch(Exception ex)
        {
            SetError(ex);
        }
    }
    private void Success()
    {
        lblMessage.Text = "Your account has been successfully created!";
        LiteralControl lc = new LiteralControl("<br />To get the most of your membership, please select &quot;Login&quot; from the menu above to take full advantage of the features of our new website!");
        phSuccess.Controls.Add(lc);
        lblMessage.Visible = true;
        memberdata.Visible = false;
    }
    protected void PopulateForm()
    {
        try
        {
            txtEmail.Text = MemberResponseObject.MemberData.Email;
            txtConfirmEmail.Text = MemberResponseObject.MemberData.Email;
        }
        catch
        {
            lblMessage.Text = string.Format("{0}</br>{1}</br>", new object[] { Resources.GlobalStrings.Apology, Resources.GlobalStrings.ContactUs });
            lblMessage.Visible = true;
            memberdata.Visible = false;
        }
    }

    private bool CreateNewUser()
    {
        tbl_Customer Customer = new tbl_Customer();
        try
        {
            HarperMembershipService.GetMemberResponse user = (HarperMembershipService.GetMemberResponse)Session["MemberResponse"];
            object[] response = tbl_Customer.CreateCustomer(user.MemberData.Address.Address1,
                    user.MemberData.Address.Address2,
                    user.MemberData.Address.Address3,
                    user.MemberData.Address.City,
                    user.MemberData.Address.State,
                    user.MemberData.Address.Country,
                    user.MemberData.Address.PostalCode,
                    null,  
                    txtPassword.Text.Trim(), "UNKNOWN",
                    user.MemberData.Salutation,
                    user.MemberData.FirstName,
                    user.MemberData.MiddleInitial,
                    user.MemberData.LastName,
                    user.MemberData.Suffix,
                    user.MemberData.Email,
                    txtUserName.Text.Trim(),
                    CustomerID,
                    user.MemberData.Subscriptions[0].PublicationCode,
                    user.MemberData.Subscriptions[0].ExpireDate,
                    user.MemberData.Subscriptions[0].DateEntered,
                    txtUserName.Text.Trim(),
                    string.Empty);
            if (response != null && response[0] != null && ((int)response[0]) == 0 && response[1] != null)
            {
                Customer = (tbl_Customer)response[1];
            }
        }
        catch 
        {
            return false; 
        }
        return true;
    }
}
