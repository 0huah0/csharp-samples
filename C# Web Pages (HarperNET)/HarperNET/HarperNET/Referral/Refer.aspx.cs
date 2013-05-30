using System;
using System.Collections;
using System.Collections.Generic;
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
using HarperLINQ;

public partial class Referral_Create : System.Web.UI.Page
{
    //reference to HarperWebServices.MembershipService
    //signature of the web service you will need to call
    //  BaseResponse CreateReferral(int donorid, string keycode, string pubcode, string emailaddress)
    HarperMembershipService.MembershipService webService = new HarperMembershipService.MembershipService();
    HarperLoggingService.LoggingService logService = new HarperLoggingService.LoggingService();
    ArrayList emailsToRefer = new ArrayList();
    ArrayList friendsToRefer = new ArrayList();
    tbl_Customer memberObject = new tbl_Customer();
    string memberId = string.Empty;
    string keyCode = string.Empty;
    string pubCode = string.Empty;
    string memberName = string.Empty;
    string memberEmail = string.Empty;

    //test: http://localhost:54094/HarperNET/Referral/Refer.aspx?MemberId=69835e4e4826be87769ab83335a6e5fa-7d4c1d49d123e8e13bc94d5e2561a170-MTM5OTMw&KeyCode=POTRIAL&PubCode=PO
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            btn_create_submit.Enabled = true;
            lblErrorMessage.Controls.Clear();
            memberId = Cryptography.DeHash(Request.Params["MemberId"],true);
            memberObject = new tbl_Customer(long.Parse(memberId), false);
            if (string.IsNullOrEmpty(memberObject.cusEmail)) { throw new Exception(string.Format("Unable to load Refer a friend refer.aspx, Member id {0}is invalid (no email).", memberId)); }
            memberName = string.Format("{0} {1}", new object[] { memberObject.cusFirstName, memberObject.cusLastName });
            if (!Page.IsPostBack)
            {
                donor_name.Text = memberName;
            }
            memberEmail = memberObject.cusEmail;
            try
            {
                keyCode = Request.Params["KeyCode"];
                pubCode = Request.Params["PubCode"];
                if (keyCode == null) { throw new Exception("Unable to load Refer a friend refer.aspx, Key code is null."); }
                if (pubCode == null) { throw new Exception("Unable to load Refer a friend refer.aspx, Pub code is null."); }
                if (keyCode != "POTRIAL") { throw new Exception("Unable to load Refer a friend refer.aspx, Key code is not POTRIAL."); }
                if (pubCode != "PO") { throw new Exception("Unable to load Refer a friend refer.aspx, Pub code is not PO."); }
            }
            catch
            {
                LiteralControl err = new LiteralControl();
                err.Text = "<p class=\"error-message\">An error has occurred.  Please contact the membership department at <a href=\"mailto:membership@andrewharper.com\">membership@andrewharper.com</a></p>";
                lblErrorMessage.Controls.Add(err);
                lblErrorMessage.Visible = true;
                btn_create_submit.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            LiteralControl err = new LiteralControl();
            err.Text = "<p class=\"error-message\">We are unable to locate your record. Please contact the membership department at <a href=\"mailto:membership@andrewharper.com\">membership@andrewharper.com</a></p>";            
            lblErrorMessage.Controls.Add(err);
            lblErrorMessage.Visible = true;
            btn_create_submit.Enabled = false;
        }        
    }

    protected void btnSubmit_click(object sender, EventArgs e)
    {
        try
        {
            buildEmailList();
            List<string> errorMessages = new List<string>();//The error messages to log to the error db
            List<string> failedReferrals = new List<string>();//The error messages to display on the confirmation page if some friends were added and some failed
            List<string> goodReferrals = new List<string>();//Referral Ids of the successful referrals
            System.Text.RegularExpressions.Regex emlregex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            foreach (string[] email in emailsToRefer)
            {
                if (!emlregex.IsMatch(email[0]))
                {
                    LiteralControl err = new LiteralControl();
                    err.Text = string.Format("<p class=\"error-message\">{0} is not a valid email address.</p>", email[0]);
                    lblErrorMessage.Controls.Add(err);
                    errorMessages.Add(err.Text);
                    failedReferrals.Add(string.Format("{0} is not a valid email address.", email[0]));
                }
                else
                {
                    string s1 = Cryptography.EncryptData(memberId);
                    HarperMembershipService.BaseResponse response = webService.CreateReferral(s1, donor_name.Text, memberEmail, keyCode, pubCode, email[1], email[0], chk_cc_donor.Checked);
                    HarperMembershipService.ReferralResponse typedresponse = (HarperMembershipService.ReferralResponse)response.TypedResponse;
                    if (typedresponse == null || !typedresponse.Success)
                    {
                        string replacementMessage = string.Empty;
                        string failedReferral = string.Format("We were unable to refer: {0}", email[1]);
                        foreach (HarperMembershipService.Message msg in response.Messages)
                        {
                            LiteralControl err = new LiteralControl();
                            err.Text = string.Format("<p class=\"error-message\">{0}</p>", msg.AhMessage);
                            lblErrorMessage.Controls.Add(err);
                            errorMessages.Add(err.Text);
                            //replace standard message with text appropriate for display when some failed and some did not
                            switch (msg.AhMessage)
                            {
                                case "The person you referred is already a current Andrew Harper member.  Please enter information for others you would like to refer for a complimentary membership.":
                                case "This referral offer is available only to new members.":
                                    replacementMessage = string.Format("{0} is already a current Andrew Harper member.", email[1]);
                                    break;
                                case "Another member has already referred your friend to Andrew Harper.  Please enter information for others you would like to refer for a complimentary membership.":
                                    replacementMessage = string.Format("Another member has already referred {0} to Andrew Harper.", email[1]);
                                    break;
                                default:
                                    replacementMessage = "An error has occurred.";
                                    break;
                            }
                        }
                        failedReferrals.Add(replacementMessage);
                    }
                    else
                    {
                        goodReferrals.Add(typedresponse.referralid.ToString());
                    }
                }
            }

            #region create encrypted query string for good referral ids
            string referralids = string.Empty;
            foreach (string goodReferral in goodReferrals)
            {
                referralids += string.Format("{0}|", HarperCRYPTO.Cryptography.EncryptData(goodReferral));
            }
            if (referralids.LastIndexOf("|") >= 0)
            {
                referralids = Server.UrlEncode(referralids.Substring(0, referralids.LastIndexOf("|")));
            }
            #endregion

            #region no errors - redirect
            if (failedReferrals.Count() == 0)
            {
                Response.Redirect(string.Format("~/Referral/confirmation.aspx?Referrals={0}", referralids), false);
            }
            #endregion

            #region no successful referrals
            else if (failedReferrals.Count() == emailsToRefer.Count)
            {
                lblErrorMessage.Visible = true;
            }
            #endregion

            #region some successful referrals, some errors - redirect
            else
            {
                string err = string.Empty;
                foreach (string failedReferral in failedReferrals)
                {
                    err += string.Format("{0}|", failedReferral);
                }
                err = Server.UrlEncode(err.Substring(0, err.LastIndexOf("|")));
                string url = string.Format("~/Referral/confirmation.aspx?Referrals={0}&Errors={1}", new object[] { referralids, err });
                Response.Redirect(url, false);
            }
            #endregion

        }
        catch (Exception ex)
        {
            try
            {
                logService.LogAppEvent("", @"HarperNET", "Referral", "Error in btnSubmit_click", ex.Message, ex.StackTrace, "", "btnSubmit_click");
            }
            catch { }
            LiteralControl err = new LiteralControl();
            err.Text = "<p class=\"error-message\">An error has occurred.  Please contact the membership department at <a href=\"membership@andrewharper.com\">membership@andrewharper.com</a></p>";
            lblErrorMessage.Controls.Add(err);
            lblErrorMessage.Visible = true;
        }
    }
    protected void buildEmailList()
    {
        try
        {
            string currentFriendEmail = Request.Form["email_1"];
            string currentFriendName = Request.Form["referral_name_1"];
            if (currentFriendEmail != null && currentFriendName != null)
            {
                emailsToRefer.Add(new string[] { currentFriendEmail, currentFriendName });
            }
            if (!string.IsNullOrEmpty(Request.Form["email_2"]) && !string.IsNullOrEmpty(Request.Form["referral_name_2"]))
            {
                string[] additionalFriendNames = Request.Form["email_2"].Split(',');  //check for null
                string[] additionalFriendEmails = Request.Form["referral_name_2"].Split(',');
                for (int i = 0; i < additionalFriendNames.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(additionalFriendEmails[i])
                        && !string.IsNullOrEmpty(additionalFriendNames[i]))
                    {
                        emailsToRefer.Add(new string[] { additionalFriendNames[i], additionalFriendEmails[i] });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            logService.LogAppEvent("", @"HarperNET", "Referral", "Error in buildEmailList", ex.Message, ex.StackTrace, "", "buildEmailList");
            LiteralControl err = new LiteralControl();
            err.Text = "<p class=\"error-message\">An error has occurred.  Please contact the membership department at <a href=\"membership@andrewharper.com\">membership@andrewharper.com</a></p>";
            lblErrorMessage.Controls.Add(err); 
            lblErrorMessage.Visible = true;
        }
	}
}
