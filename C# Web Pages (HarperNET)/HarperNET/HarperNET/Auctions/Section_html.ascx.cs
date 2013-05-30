using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

    public partial class Controls_Section_html : System.Web.UI.UserControl, ISectionControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void SetSection(System.Xml.XmlNode nodSection)
        {
            System.Xml.XmlNodeList lstHTML;
            System.Xml.XmlNodeList lstTitle;
            bool bolShowTitle;

            if (nodSection.Attributes["hastitle"] == null)
            {
                bolShowTitle = true;

            }
            else
            {
                bolShowTitle = Convert.ToBoolean(nodSection.Attributes["hastitle"].Value);
            }

            if (bolShowTitle)
            {
                lstTitle = nodSection.SelectNodes("title");

                lblTitle.Text = lstTitle[0].InnerText;
            }
            else
            {
                trTitleRow.Visible = false;
                trSep.Visible = false;
            }

            lstHTML = nodSection.SelectNodes("html");

            spnContent.InnerHtml = lstHTML[0].InnerText.Replace("src=\"", "src=\"" + System.Configuration.ConfigurationManager.AppSettings["MarketPlaceImagePrefix"]);
        }
    }
