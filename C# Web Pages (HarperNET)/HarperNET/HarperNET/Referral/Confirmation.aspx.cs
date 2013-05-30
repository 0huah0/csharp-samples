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

public partial class Referral_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        phContent.Controls.Clear();
        LiteralControl ctl = new LiteralControl();
        List<string> referralIds = new List<string>();
        List<HarperLINQ.Referral> referralObjects = new List<HarperLINQ.Referral>();
        if (string.IsNullOrEmpty(Request["Referrals"]))
        {
            ctl.Text = "<p class=\"error-message\">An error has occurred.  Please contact the membership department at <a href=\"mailto:membership@andrewharper.com\">membership@andrewharper.com</a></p>";            
        }
        else
        {
            #region get referral objects
            try
            {
                referralIds = Request["Referrals"].Split('|').ToList();
                foreach (string goodReferral in referralIds)
                {
                    int referralid = int.Parse(HarperCRYPTO.Cryptography.DecryptData(goodReferral));
                    HarperLINQ.Referral referral = new HarperLINQ.Referral(referralid);
                    if (referral.id > 0
                        && !string.IsNullOrEmpty(referral.friendemail)
                        && referral.dateexpires >= DateTime.Now
                        && referral.dateredeemed == null)
                    {
                        referralObjects.Add(referral);
                    }
                }
            }
            catch { }
            #endregion

            #region no valid referrals(all are invalid, redeemed, or expired) - show error
            if(referralObjects.Count() <= 0)
            {
                ctl.Text = "<p class=\"error-message\">An error has occurred.  Please contact the membership department at <a href=\"mailto:membership@andrewharper.com\">membership@andrewharper.com</a></p>";
            }
            #endregion

            #region no errors - show pdf download
            else if (string.IsNullOrEmpty(Request["Errors"]))
            {
                ctl.Text = "<a href=\"http://www.andrewharper.com\"><img alt=\"header image\" src=\"images/Referral_ThankYouButton.jpg\" border=\"0\" /></a>";
                //ctl.Text = "<a href=\"http://viewer.zmags.com/publication/c0a78eee#/c0a78eee/1\" target=\"_blank\"><img alt=\"header image\" src=\"images/Referral_ThankYouButton.jpg\" border=\"0\" /></a>";
                if (referralObjects.Count() > 1)
                {
                    ctl.Text += "<h1>Your invitations have been sent.</h1>";
                }
                else
                {
                    ctl.Text += "<h1>Your invitation has been sent.</h1>";
                }
            }
            #endregion

            #region at least one good referral and some errors show pdf download and any errors
            else
            {
                #region show errors
                string content = "<a href=\"http://www.andrewharper.com\"><img alt=\"header image\" src=\"images/Referral_ThankYouButton.jpg\" border=\"0\" /></a><p class=\"error-message\">We were unable to refer all of your friends. Please review the following errors.</p><p>";
                //string content = "<a href=\"http://viewer.zmags.com/publication/c0a78eee#/c0a78eee/1\" target=\"_blank\"><img alt=\"header image\" src=\"images/Referral_ThankYouButton.jpg\" border=\"0\" /></a><p class=\"error-message\">We were unable to refer all of your friends. Please review the following errors.</p><p>";
                foreach (string err in Request["Errors"].Split('|'))
                {
                    content += string.Format("{0}<br />", Server.UrlDecode(err));
                }
                content = content.Substring(0, content.LastIndexOf("<br />"));
                content += "</p>";
                #endregion

                #region show successful referrals
                string friendList = string.Empty;
                int i = 0;
                foreach (HarperLINQ.Referral referral in referralObjects)
                {
                    i++;                    
                    if (i == referralObjects.Count())
                    {
                        friendList += referral.friendname;
                    }
                    else if (i == referralObjects.Count()-1)
                    {
                        friendList += string.Format("{0} and ", referral.friendname);
                    }
                    else
                    {
                        friendList += string.Format("{0}, ", referral.friendname);
                    }
                }
                if (referralObjects.Count > 1)
                {
                    content += string.Format("<h1>Your invitations to {0} have been sent.</h1>", friendList);
                }
                else
                {
                    content += string.Format("<h1>Your invitation to {0} has been sent.</h1>", friendList);
                }
                #endregion

                ctl.Text = content;
            }
            #endregion
        }
        phContent.Controls.Add(ctl);
    }
}
