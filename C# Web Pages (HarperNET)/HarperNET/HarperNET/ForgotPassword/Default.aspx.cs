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
using System.Data.SqlClient;
using System.Text;
using HarperCRYPTO;

public partial class ForgotPassword_Default : System.Web.UI.Page
{
    // ***********************************************
    // META TAG - Values
    public const string META_DESC = "The ultimate guide to the world’s most exclusive luxury hotels  and exotic resorts. Andrew Harper has long been regarded as the ultimate authority on worldwide luxury travel.";
    public const string META_KEYWORDS = "Luxury Travel, Luxury Hotel, Luxury Resort, Luxury Vacation, Luxury Cruise, Hotel Review, Resort Review, Exotic Hideaways, Exotic Travel, Exotic Resort, Andrew Harper's Hideaway Report, Andrew Harper Travel";
    // ***********************************************

    HarperSecureService.SecureServices SecureClient = new HarperSecureService.SecureServices();

    string db_connection = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        // ***********************************************
        // META TAG - First Line in the Page_Load
        //((Main)Master).MetaDescription = META_DESC;
        //((Main)Master).MetaKeywords = META_KEYWORDS;
        // ***********************************************
        try
        {
            db_connection = ConfigurationManager.ConnectionStrings["AHT_MainConnectionString"].ConnectionString;
            if (!Page.IsPostBack)
            {
                ClearForm();
            }
        }
        catch (Exception ex)
        {
            SetError(ex);
        }
    }
    private void ClearForm()
    {
        pnlForm.Visible = true;
        email.Text = string.Empty;
        lblMessage.Text = string.Empty;
        lblMessage.Visible = false;
        lblConfirmation.Visible = false;
    }
    private void SetError(Exception ex)
    {
        pnlForm.Visible = false;
        StaticMethods.LogError("ForgotPassword/Default.aspx", ex.Message, ex.StackTrace);
        lblMessage.Text = string.Format("We encountered an error while trying to reset your password.<br /><br />{0}", new object[] { Resources.GlobalStrings.ContactUs });
        lblMessage.Visible = true;
    }
    protected void SubmitClicked(object sender, EventArgs e)
    { 
        if (string.IsNullOrEmpty(email.Text))
        {
            SetError(new Exception("Email address is required."));
            pnlForm.Visible = true;
        }
        else
        {
            try
            {
                HarperSecureService.UpdatePasswordResponse updateResponse = new HarperSecureService.UpdatePasswordResponse();
                HarperLINQ.tbl_Customer user = new HarperLINQ.tbl_Customer(email.Text, false);
                if (user == null)
                {
                    SetError(new Exception("Unable to find a member with this address."));
                    lblMessage.Text = "Unable to find a member with this email address.";
                    pnlForm.Visible = true;
                }
                else
                {
                    if (string.IsNullOrEmpty(user.cusPassword))
                    {
                        SetError(new Exception("Error retrieving password - no password or blank password on file."));
                        lblMessage.Text = string.Format("{0}</br>{1}</br>", new object[] { Resources.GlobalStrings.Apology, Resources.GlobalStrings.ContactUs });
                        pnlForm.Visible = true;
                    }
                    else
                    {
                        string pwd = Cryptography.Decrypt256FromHEX(user.cusPassword);
                        if (!string.IsNullOrEmpty(user.cusUserName)
                            && !string.IsNullOrEmpty(pwd)
                            && !string.IsNullOrEmpty(email.Text))
                        {
                            SupportClasses.Mailer Emailer = new SupportClasses.Mailer();
                            Emailer.SendEmail(ConfigurationManager.AppSettings["mailserviceuser"],
                                ConfigurationManager.AppSettings["mailservicepwd"],
                                "Andrew Harper Password Reminder",
                                ConfigurationManager.AppSettings["MembershipEmailFrom"].ToString(),
                                user.cusEmail,
                                string.Empty, string.Empty,
                                getEmailBody(user.cusUserName, pwd), true,
                                ConfigurationManager.AppSettings["EmailServer"]);

                            lblConfirmation.Visible = true;
                            pnlForm.Visible = false;
                            lblMessage.Visible = false;
                            string s = "<p>As requested, a reminder of your username and password has been sent to " + user.cusEmail + ".  Please retrieve your credentials, and use them to log in to our website <a href=\"http://www.andrewharper.com/login\" target=\"_top\">here</a>.</p><p>As a reminder, all username and passwords are case-sensitive.</p><p>If you do not receive the email shortly, please check your spam or junk email folder.  If you continue to experience difficulties, please contact Membership at membership@andrewharper.com or call (866) 831-4314 from the U.S or +1 (512) 904-7342 internationally.</p>";
                            lblConfirmation.Text = s;// string.Format("A reminder has been sent to {0}. If you do not receive the email within thirty minutes please check your spam or junk email folder.", email.Text);
                            lblConfirmation.Visible = true;

                            WriteLog(string.Format("Password for {0} was sent to {1}", new object[] { user.cusUserName, user.cusEmail }));
                        }
                        else
                        {
                            SetError(new Exception("Unable to send password reminder email."));
                            lblMessage.Text = "Unable to send password reminder email.";
                            pnlForm.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetError(new Exception("Unable to send password reminder email, email address may be duplicate."));
                lblMessage.Text = "Unable to send password reminder email, email address may be duplicate.";
                pnlForm.Visible = true;
                WriteLog(ex);
            }
        }
    }

    private void WriteLog(string message)
    {
        HarperACL.AppEventLog log = new HarperACL.AppEventLog();
        log.aelAppName = "ForgotPassword";
        log.aelDateCreated = DateTime.Now;
        log.aelMessage1 = string.Empty;
        log.aelMessage2 = string.Empty;
        log.aelSeverity = "INFO";
        using (HarperACL.ACLDataDataContext context = new HarperACL.ACLDataDataContext(db_connection))
        {
            context.AppEventLogs.InsertOnSubmit(log);
            context.SubmitChanges();
        }
    }
    private void WriteLog(Exception e)
    {
        HarperACL.AppEventLog log = new HarperACL.AppEventLog();
        log.aelAppName = "ForgotPassword";
        log.aelDateCreated = DateTime.Now;
        log.aelMessage1 = string.Empty;
        log.aelMessage2 = string.Empty;
        log.aelMessage3 = e.Message;
        log.aelSeverity = "EXCEPTION";
        using (HarperACL.ACLDataDataContext context = new HarperACL.ACLDataDataContext(db_connection))
        {
            context.AppEventLogs.InsertOnSubmit(log);
            context.SubmitChanges();
        }
    }
    private string getEmailBody(string user, string pwd)
    {
        StringBuilder email = new StringBuilder();

        #region Create email text and insert variables
        email.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
        email.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        email.Append("<head>");
        email.Append("    <base target=\"_blank\" />");
        email.Append("    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />");
        email.Append("    <title>Your Featured Travel Values | News From Andrew Harper</title>");
        email.Append("</head>");
        email.Append("<head>");
        email.Append("    <style type=\"text/css\">");
        email.Append("        body");
        email.Append("        {");
        email.Append("            font-family: Georgia, serif;");
        email.Append("            background-color: #eee;");
        email.Append("        }");
        email.Append("        abbr");
        email.Append("        {");
        email.Append("            font-variant: small-caps;");
        email.Append("        }");
        email.Append("        a:active, a:visited, a:link");
        email.Append("        {");
        email.Append("            color: #5c8727;");
        email.Append("            text-decoration: none;");
        email.Append("        }");
        email.Append("        a:hover");
        email.Append("        {");
        email.Append("            color: #5c8727;");
        email.Append("            text-decoration: underline;");
        email.Append("        }");
        email.Append("        .GrayHead");
        email.Append("        {");
        email.Append("            color: #808086;");
        email.Append("            letter-spacing: 1px;");
        email.Append("        }");
        email.Append("    </style>");
        email.Append("</head>");
        email.Append("<body>");
        email.Append("    <div align=\"center\" style=\"font-size: 8pt; line-height: 1.3; padding-bottom: 3px;\">");
        email.Append("        <font face=\"Verdana,sans-serif\" color=\"#000\"><strong>Andrew Harper Password Reminder</strong><br />");
        email.Append("            Follow us on <a href=\"http://www.facebook.com/pages/Andrew-Harper/70674486854?sid=7bb23ae8202668ee9c3ba769e85d4c4c&amp;ref=search\"");
        email.Append("                style=\"color: #5c8727; text-decoration: underline;\" target=\"_blank\">Facebook</a>");
        email.Append("            and <a href=\"http://twitter.com/HarperTravel\" style=\"color: #5c8727; text-decoration: underline;\"");
        email.Append("                target=\"_blank\">Twitter</a><br />");
        email.Append("            Please add <a href=\"mailto:ah.email@andrewharper.com\" style=\"color: #5c8727; text-decoration: underline;\">");
        email.Append("                ah.email@andrewharper.com</a> to your address book to ensure that our emails");
        email.Append("            reach your inbox. </font>");
        email.Append("    </div>");
        email.Append("    <table width=\"600\" align=\"center\" cellpadding=\"3\" cellspacing=\"0\">");
        email.Append("        <tr bgcolor=\"#ffffff\">");
        email.Append("            <td bgcolor=\"#ffffff\">");
        email.Append("                <table cellpadding=\"3\" cellspacing=\"0\" width=\"600\">");
        email.Append("                    <tr bgcolor=\"#FFFFFF\">");
        email.Append("                        <td>");
        email.Append("                            <table border=\"0\" align=\"center\" cellpadding=\"3\" cellspacing=\"8\">");
        email.Append("                                <tr valign=\"top\" align=\"left\">");
        email.Append("                                    <td width=\"600\" height=\"60\">");
        email.Append("                                        <div align=\"center\" style=\"border-bottom: 1px solid #CCCCCC;\">");
        email.Append("                                            <p>");
        email.Append("                                                <a href=\"https://www.andrewharper.com/user\">");
        email.Append("                                                    <img src=\"http://www.andrewharper.com/ImageStore/images/ForgotPassword/AH_Logo.jpg\" alt=\"Andrew Harper\"");
        email.Append("                                                        width=\"133\" height=\"110\" vspace=\"3\" border=\"0\" align=\"top\" /></p>");
        email.Append("                                        </div>");
        email.Append("                                    </td>");
        email.Append("                                </tr>");
        email.Append("                                <tr align=\"left\" valign=\"top\" bgcolor=\"#FFFFFF\">");
        email.Append("                                    <td align=\"center\">");
        email.Append("                                    </td>");
        email.Append("                                </tr>");
        email.Append("                                <tr>");
        email.Append("                                    <td align=\"center\">");
        email.Append("                                        <span style=\"font-size: 18px; font-family: Georgia, serif; color: #333333; line-height: 1.4em;\">");
        email.Append("                                            Andrew Harper Password Reminder </span>");
        email.Append("                                        <div style=\"font-size: 13px; font-family: Verdana, Geneva, sans-serif; color: #333333;");
        email.Append("                                            line-height: 1.4em; padding-top: 3px; border-bottom: 1px solid #CCCCCC;\">");
        email.Append("                                            <p>");
        email.Append("                                                Below is your login account information:<br />");
        email.Append("                                                <br />");
        email.Append("                                                <strong>User Name:</strong>");
        // ******************************************************************************************************************
        // ******************************************************************************************************************
        // ******************************************************************************************************************
        // ******************************************************************************************************************
        // ******************************************************************************************************************
        // ******************************************************************************************************************
        // ******************************************************************************************************************
        // ******************************************************************************************************************
        email.Append(user);
        email.Append("                                                <br />");
        email.Append("                                                <strong>Password:</strong>");
        // ******************************************************************************************************************
        // ******************************************************************************************************************
        // ******************************************************************************************************************
        // ******************************************************************************************************************
        // ******************************************************************************************************************
        // ******************************************************************************************************************
        // ******************************************************************************************************************
        // ******************************************************************************************************************
        email.Append(pwd);
        email.Append("                                                <br />");
        email.Append("                                                <br />");
        email.Append("                                                Please <a href=\"https://www.andrewharper.com/user\"><strong>CLICK HERE</strong></a>");
        email.Append("                                                to login.");
        email.Append("                                            </p>");
        email.Append("                                            <p>");
        email.Append("                                                If you need further assistance, please contact our Membership Office");
        email.Append("                                                        <br />");
        email.Append("                                                at <strong>(866) 831-4314 or (630) 734-4610</strong>.<br />");
        email.Append("                                                <br />");
        email.Append("                                            </p>");
        email.Append("                                        </div>");
        email.Append("                                    </td>");
        email.Append("                                </tr>");
        email.Append("                            </table>");
        email.Append("                        </td>");
        email.Append("                    </tr>");
        email.Append("                    <tr>");
        email.Append("                        <td width=\"75%\" valign=\"top\">");
        email.Append("                            <div align=\"center\">");
        email.Append("                                <div align=\"center\">");
        email.Append("                                    <a href=\"http://www.andrewharper.com\">");
        email.Append("                                        <img src=\"http://www.andrewharper.com/ImageStore/Images/icons/harper_icon.gif\" alt=\"Andrew Harper\"");
        email.Append("                                            width=\"35\" height=\"34\" border=\"0\" style=\"border: medium none\" /></a>&nbsp;");
        email.Append("                                    <a href=\"http://www.facebook.com/pages/Andrew-Harper/70674486854?sid=7bb23ae8202668ee9c3ba769e85d4c4c&amp;ref=search\"");
        email.Append("                                        target=\"_blank\">");
        email.Append("                                        <img src=\"http://www.andrewharper.com/ImageStore/Images/icons/icon_facebook.gif\"");
        email.Append("                                            alt=\"Andrew Harper Facebook\" width=\"35\" height=\"34\" border=\"0\" style=\"border: medium none\" />&nbsp;</a>");
        email.Append("                                    <a href=\"http://twitter.com/HarperTravel\" target=\"_blank\">");
        email.Append("                                        <img src=\"http://www.andrewharper.com/ImageStore/Images/icons/icon_twitter.gif\" alt=\"Andrew Harper Twitter\"");
        email.Append("                                            width=\"35\" height=\"34\" border=\"0\" style=\"border: medium none\" />&nbsp;</a>");
        email.Append("                                    <a href=\"http://www.thingsyoushouldknowblog.com/?feed=rss2\" target=\"_blank\">");
        email.Append("                                        <img src=\"http://www.andrewharper.com/ImageStore/Images/icons/icon_rss_blog.gif\"");
        email.Append("                                            alt=\"RSS feed blog\" width=\"35\" height=\"34\" border=\"0\" style=\"border: none\" />&nbsp;</a>");
        email.Append("                                </div>");
        email.Append("                            </div>");
        email.Append("                            <div align=\"center\" style=\"font-size: 8pt; line-height: 150%; margin-bottom: 0; padding: 5px8px 6px 8px;\">");
        email.Append("                                <font face=\"Georgia, serif\" color=\"#000000\">&copy; Andrew Harper LLC | (866) 831-4314");
        email.Append("                                    or (630) 734-4610 | 1601 Rio Grande, Suite 410, Austin, TX 78701");
        email.Append("                                    <br />");
        email.Append("                                    View our <a href=\"http://www.andrewharper.com/privacy-policy\"");
        email.Append("                                        style=\"color: #5c8727;\">Privacy Statement</a> |<a href=\"http://www.andrewharper.com/terms-conditions\"");
        email.Append("                                            style=\"color: #5c8727;\"> Contact Us</a> | <a href=\"http://www.andrewharper.com/contact-us\"");
        email.Append("                                                style=\"color: #5c8727;\">Terms &amp; Conditions</a> </font></font>");
        email.Append("                            </div>");
        email.Append("                        </td>");
        email.Append("                    </tr>");
        email.Append("                </table>");
        email.Append("            </td>");
        email.Append("        </tr>");
        email.Append("    </table>");
        email.Append("</body>");
        email.Append("</html>");
        #endregion

        return email.ToString();
    }
}
