using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarperLINQ;
using System.Configuration;
using System.Diagnostics;

namespace CreateReferralReminders
{
    public class Reminder
    {
        public void Run()
        {
            try
            {
                List<HarperLINQ.Referral> referrals = Referral.GetNeedsReminderList();
                foreach (Referral referral in referrals)
                {
                    tbl_Customer member = new tbl_Customer(referral.memberid, false);
                    string cc = null;
                    if (referral.ccmember)
                    {
                        cc = member.cusEmail;
                    }

                    ReferralOffer offer = new ReferralOffer(referral.keycode, referral.pubcode);
                    string membername = string.Format("{0} {1}", member.cusFirstName, member.cusLastName);
                    string link = string.Format("{0}/Referral/Redeem.aspx?ReferralId={1}", ConfigurationManager.AppSettings["server"], System.Web.HttpUtility.UrlEncode(HarperCRYPTO.Cryptography.EncryptData(referral.id.ToString())));
                    string emailbody = offer.reminderemailcopy.Replace("[membername]", membername).Replace("[friendname]", referral.friendname).Replace("[link]", link);

                    SimpleMail reminder = new SimpleMail(offer.reminderemailsubject,
                                                        offer.reminderemailfromaddress,
                                                        referral.friendemail,
                        //"mcoupland@andrewharper.com",
                                                        string.Empty, //do not cc the reminders?
                                                        offer.reminderemailbcc,
                                                        emailbody,
                                                        offer.reminderemailishtml.HasValue ? offer.reminderemailishtml.Value : true,
                                                        offer.reminderemailsmtp);
                    reminder.Save();
                    referral.reminderemailid = reminder.id;
                    referral.Save();
                }
            }
            catch (Exception ex)
            {
                string SourceName = "ReferralReminder";
                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, "Application");
                }

                EventLog eventLog = new EventLog();
                eventLog.Source = SourceName;
                string message = string.Format("Exception: {0} \n\nStack: {1}", ex.Message, ex.StackTrace);
                eventLog.WriteEntry(message, EventLogEntryType.Error);
            }
        }
    }
}
