using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

    /// <summary>
    /// Summary description for XMLCoverter
    /// </summary>
    public class XMLCoverter
    {
        public const string SECTION_HTML = "Section_html.ascx";

        public delegate void AuctionTypeReachedHandler(
            PlaceHolder plaPlaceHolder);

        public event AuctionTypeReachedHandler AuctionTypeReached;

        public XMLCoverter()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected void OnAuctionTypeReached(
            PlaceHolder plaPlaceHolder)
        {
            if (AuctionTypeReached != null)
            {
                AuctionTypeReached(plaPlaceHolder);
            }
        }

        public void AddControls(
            string strXML,
            PlaceHolder plaPlaceHolder,
            Page pagMain)
        {
            AddControls(strXML, plaPlaceHolder, pagMain, true);
        }

        public void AddControls(
            string strXML,
            PlaceHolder plaPlaceHolder,
            Page pagMain,
            bool bolShowNonFixed)
        {
            System.Xml.XmlDocument docXML;
            System.Xml.XmlNodeList lstSections;
            int intCounter;
            string strSectionType;
            string strIsFixed;
            bool bolIsFixed;
            ISectionControl secSection;
            Control ctrTemp;

            docXML = new System.Xml.XmlDocument();

            docXML.LoadXml(strXML);

            lstSections = docXML.DocumentElement.SelectNodes("/package/section");

            //System.Diagnostics.Debug.WriteLine("lstSections.Count: " + lstSections.Count.ToString());

            for (intCounter = 0;
                intCounter < lstSections.Count;
                intCounter++)
            {
                //System.Diagnostics.Debug.WriteLine("Section Type: " + lstSections[intCounter].Attributes["type"]);

                if (lstSections[intCounter].Attributes["type"] != null)
                {
                    strSectionType = lstSections[intCounter].Attributes["type"].Value;

                    bolIsFixed = true;

                    if (lstSections[intCounter].Attributes["isfixed"] != null)
                    {
                        strIsFixed = lstSections[intCounter].Attributes["isfixed"].Value;

                        if (strIsFixed.ToLower() == "false")
                            bolIsFixed = false;
                    }

                    //System.Diagnostics.Debug.WriteLine(strSectionType);

                    switch (strSectionType)
                    {
                        case "html":
                            if ((!bolShowNonFixed) && (!bolIsFixed))
                            {
                                continue;
                            }

                            //System.Diagnostics.Trace.WriteLine(lstSections[intCounter].ToString());

                            ctrTemp = pagMain.LoadControl(SECTION_HTML);

                            secSection = (ISectionControl)ctrTemp;

                            secSection.SetSection(lstSections[intCounter]);

                            plaPlaceHolder.Controls.Add(ctrTemp);

                            break;
                        case "auction":

                            OnAuctionTypeReached(plaPlaceHolder);

                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }