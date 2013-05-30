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
using System.Collections.Generic;
using HarperLINQ;
using HarperCRYPTO;

public partial class Referral_Default : System.Web.UI.Page
{    
    HarperMembershipService.MembershipService webService = new HarperMembershipService.MembershipService();
    HarperLoggingService.LoggingService logService = new HarperLoggingService.LoggingService();
    string country = string.Empty;
    string region = string.Empty;
    int referralid = -1;
    bool badReferralId = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            loadCountries();
            setStartingCountry();
        }
        finally
        {
            try
            {
                btn_redeem_submit.Enabled = true;
                lblErrorMessage.Controls.Clear();
                try
                {
                    referralid = int.Parse(Server.UrlDecode(Cryptography.DecryptData(Request["ReferralId"])));    
                    badReferralId = false;                
                }
                catch (Exception ex)
                {
                    logService.LogAppEvent("", @"HarperNET", "Referral", "Unable to decrypt referral id, using default referrer instead. (referralid in url): " + Request["ReferralId"], ex.Message, ex.StackTrace, "", "Page_Load");
                    badReferralId = true;                
                }
                if (!badReferralId)
                {
                    Referral referral = new Referral(referralid);
                    if (referral.id <= 0)
                    {
                        throw new Exception(string.Format("Cannot redeem referral - referral id {0} does not exist", referralid));
                    }
                    if (referral.dateredeemed != null)
                    {
                        throw new Exception(string.Format("Referral id {0} has already been redeemed", referralid));
                    }
                    if (!Page.IsPostBack)
                    {
                        email_address.Text = referral.friendemail;
                    }
                }
            }
            catch (Exception ex)
            {
                logService.LogAppEvent("", @"HarperNET", "Referral", "Error in Page_Load Referralid in url): " + Request["ReferralId"], ex.Message, ex.StackTrace, "", "Page_Load");
                LiteralControl err = new LiteralControl();
                err.Text = "<p class=\"error-message\">An error has occurred.  Please contact the membership department at <a href=\"mailto:membership@andrewharper.com\">membership@andrewharper.com</a></p>";
                lblErrorMessage.Controls.Add(err);
                lblErrorMessage.Visible = true;
                btn_redeem_submit.Enabled = false;
            }
        }
    }
    
    protected void btnSumbit_click(object sender, EventArgs e)
    {
        try
        {
            #region Unable to decode referral id, try finding original referral based on email address
            if (badReferralId)
            {
                try
                {
                    Referral badRef = new Referral(email_address.Text);
                    if (badRef.id > 0)
                    {
                        referralid = badRef.id;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    logService.LogAppEvent("", @"HarperNET", "Referral", "Unable to link email to referral id (id could not be decoded and email not on file). Using default referrer. Referral id in url: " + Request["ReferralId"] + ", email address entered: " + email_address.Text, ex.Message, ex.StackTrace, "", "Page_Load");
                    
                    Referral defaultReferral = new Referral();
                    defaultReferral.keycode = "POTRIAL";
                    defaultReferral.pubcode = "PO";
                    ReferralOffer offer = new ReferralOffer(defaultReferral.keycode, defaultReferral.pubcode);
                    
                    defaultReferral.ccmember = false;
                    defaultReferral.datecreated = DateTime.Now;
                    defaultReferral.dateexpires = defaultReferral.datecreated.AddMonths(offer.offerexpiresmonths);
                    defaultReferral.friendemail = email_address.Text;
                    defaultReferral.friendname = first_name.Text + " " + last_name.Text;

                    HarperLINQ.tbl_Customer defaultReferrer = new tbl_Customer(ConfigurationManager.AppSettings["default_referrer_username"], true);
                    defaultReferral.memberid = defaultReferrer.cusID;
                    defaultReferral.subscriptionlength = offer.triallengthinmonths;

                    defaultReferral.Save();
                    referralid = defaultReferral.id;
                }
            }
            #endregion

            HarperMembershipService.BaseResponse response = new HarperMembershipService.BaseResponse();
            HarperMembershipService.MembershipService webservice = new HarperMembershipService.MembershipService();

            #region Get selected region
            country = ddlCountries.SelectedValue;
            ISO3166 iso = new ISO3166(country, IdentifierType.Country_Code_Alpha2);
            string sfgcountrycode = iso.SFGCode;

            if (txtRegion.Text != "" && txtRegion.Text != null)
            {
                region = txtRegion.Text;
            }
            else if (txtRegionNotListed.Text != "" && txtRegionNotListed.Text != null)
            {
                region = txtRegionNotListed.Text;
            }
            else
            {
                region = ddlRegion.SelectedValue;
            }
            #endregion

            string erefid = Cryptography.EncryptData(referralid.ToString());
            string epwd = Cryptography.EncryptData(txtPassword.Text);

            #region Redeem the referral
            response = webservice.RedeemReferral(erefid, first_name.Text, last_name.Text, email_address.Text,
                sfgcountrycode, address_line_1.Text, address_line_2.Text, city.Text, region, postal.Text, true, 
                txtUserName.Text, epwd);
            #endregion

            #region Check for errors
            if (response == null)
            {
                throw new Exception(string.Format("Error redeeming referral id {0}, response from SFG was null.", referralid));
            }
            if (response.Messages != null && response.Messages.Count() > 0)
            {
                throw new Exception(response.Messages[0].AhMessage);
            }
            #endregion

            Response.Redirect("~/Referral/RedemptionConfirmation.aspx", false);
        }
        catch (Exception ex)
        {
            logService.LogAppEvent("", @"HarperNET", "Referral", "Error in btnSumbit_click", ex.Message, ex.StackTrace, "", "btnSubmit_click");
            LiteralControl err = new LiteralControl();
            err.Text = "<p class=\"error-message\">An error has occurred.  Please contact the membership department at <a href=\"mailto:membership@andrewharper.com\">membership@andrewharper.com</a></p>";
            lblErrorMessage.Controls.Add(err);
            lblErrorMessage.Visible = true;
        }
    }
    private void loadCountries()
    {
        if (!IsPostBack)
        {
            List<ISO3166> countries = HarperLINQ.ISO3166.GetSFGSafeCountries();
            ddlCountries.DataSource = countries;
            ddlCountries.DataTextField = "SafeDisplayName";
            ddlCountries.DataValueField = "Alpha2";
            ddlCountries.DataBind();
        }
    }
    private void setStartingCountry()
    {
        if (!IsPostBack)
        {
            string location = System.Globalization.RegionInfo.CurrentRegion.TwoLetterISORegionName.ToUpper();
            if (ddlCountries.Items.FindByValue(location) != null)
            {
                ddlCountries.SelectedIndex = ddlCountries.Items.IndexOf(ddlCountries.Items.FindByValue(location));
                loadRegions();
            }
        }

    }
    protected void loadRegions(object sender, EventArgs e)
    {
        loadRegions();
    }
    protected void loadRegions()
    {
        ListItem blank = new ListItem("", "-1");
        string countryName = ddlCountries.SelectedItem.Text;
        List<ISO3166> regions = HarperLINQ.ISO3166.GetRegions(ddlCountries.SelectedItem.Value);
        ddlRegion.Items.Clear();
        ddlRegion.Items.Add(blank);
        ddlRegion.DataSource = regions;
        ddlRegion.DataTextField = "SafeDisplayName";
        ddlRegion.DataValueField = "SafeDisplayName";
        ddlRegion.DataBind();

        if (countryName == "U.S.A.")
        {
            lblState.Visible = true;
            lblRegion.Visible = false;
            ddlRegion.Visible = true;
            txtRegion.Visible = false;
            lblRegionNotListed.Visible = false;
            txtRegionNotListed.Visible = false;
            req_txtRegion.Enabled = false;
            req_ddlRegion.Enabled = false;
            req_ddlState.Enabled = true;
        }
        else if (regions.Count == 0)
        {
            lblState.Visible = false;
            lblRegion.Visible = true;
            ddlRegion.Visible = false;
            txtRegion.Visible = true;
            lblRegionNotListed.Visible = false;
            txtRegionNotListed.Visible = false;
            req_txtRegion.Enabled = true;
            req_ddlRegion.Enabled = false;
            req_ddlState.Enabled = false;
        }
        else
        {
            lblState.Visible = false;
            lblRegion.Visible = true;
            ddlRegion.Visible = true;
            txtRegion.Visible = false;
            lblRegionNotListed.Visible = true;
            txtRegionNotListed.Visible = true;
            req_txtRegion.Enabled = false;
            req_ddlRegion.Enabled = true;
            req_ddlState.Enabled = false;
        }
        //if the region list is empty, show a text box, otherwise show a drop down with the region values
        //country not listed textbox
    }
}
