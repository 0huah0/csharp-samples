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

public partial class _Default : System.Web.UI.Page
{
    // ***********************************************
    // META TAG - Values
    public const string META_DESC = "The ultimate guide to the world’s most exclusive luxury hotels  and exotic resorts. Andrew Harper has long been regarded as the ultimate authority on worldwide luxury travel.";
    public const string META_KEYWORDS = "Luxury Travel, Luxury Hotel, Luxury Resort, Luxury Vacation, Luxury Cruise, Hotel Review, Resort Review, Exotic Hideaways, Exotic Travel, Exotic Resort, Andrew Harper's Hideaway Report, Andrew Harper Travel";
    // ***********************************************
    public string terms_url = ConfigurationManager.AppSettings["terms_url"];
    protected DataSet m_dasThumbs;

    protected void Page_Load(object sender, EventArgs e)
    {
        //TODO: add meta data 
        // ***********************************************
        // META TAG - First Line in the Page_Load
        //((Main)Master).MetaDescription = META_DESC;
        //((Main)Master).MetaKeywords = META_KEYWORDS;
        // ***********************************************

        /**
         * 1/25/2012 - MPC: 
         * MemberId must now be encrypted using Cryptography.Encrypt256() and decrypted using Cryptography.Decrypt256FromHEX()
         * These methods are much more secure
         */
        if (!string.IsNullOrEmpty(Request["MemberId"]))
        {
            Session["MemberId"] = Request["MemberId"];
        }
        else
        {
            Session["MemberId"] = string.Empty; //"32A43324FF651C3AE4A2CCD0B723B26A" 142325 cms_mcoupland // 
        }
        if (!this.IsPostBack)
        {
            if (Request.QueryString["VW"] != null)
            {
                StaticMethods.SetDropDownValue(ddlView, Request.QueryString["VW"].ToString());
            }
        }

        PopulateGrid();
    }

    protected void ddlView_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Response.Redirect(
        //    DotNetNuke.Common.Globals.NavigateURL(
        //    this.TabId) + "?VW=" + ddlView.SelectedValue);

        Response.Redirect("ViewAll.aspx?VW=" + ddlView.SelectedValue);
    }

    protected void PopulateGrid()
    {
        MarketPlaceDataLayer mdlDataLayer;
        DataSet dasThumbs;

        mdlDataLayer = DataLayerFactory.GetMarketPlace();

        dasThumbs = mdlDataLayer.AucGetAuctionThumbs(
            ddlView.SelectedValue.ToString());

        m_dasThumbs = dasThumbs;

        lstThumbs.DataSource = dasThumbs.Tables[0];
        lstThumbs.DataBind();
    }

    protected void lstThumbs_ItemCreated(object sender, DataListItemEventArgs e)
    {
        DataRow[] arrThumb;
        AuctionThumb thmThumb;
        Control ctrControl;
        double dblDisplayDollarValue;
        DateTime datOpenDate;
        DateTime datCloseDate;

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            arrThumb = m_dasThumbs.Tables[0].Select("aucID = " + lstThumbs.DataKeys[e.Item.ItemIndex].ToString());

            if (arrThumb.Length <= 0)
                return;

            ctrControl = e.Item.FindControl("thmThumb");

            if (ctrControl == null)
                return;

            thmThumb = (AuctionThumb)ctrControl;

            thmThumb.AucID = Convert.ToInt32(arrThumb[0]["aucID"]);
            thmThumb.AucName = arrThumb[0]["adeName"].ToString();
            thmThumb.AucSubName = arrThumb[0]["adeSubName"].ToString();
            thmThumb.ImagePath = Resources.AuctionResources.AuctionImageLocation + arrThumb[0]["adeThumbImage"].ToString(); //System.Configuration.ConfigurationManager.AppSettings["MarketPlaceImagePrefix"] + arrThumb[0]["adeThumbImage"].ToString();

            thmThumb.ThumbClick += new AuctionThumb.ThumbClickEventHandler(thmThumb_ThumbClick);

            dblDisplayDollarValue = Convert.ToDouble(arrThumb[0]["DisplayDollarValue"]);

            if (arrThumb[0]["StatusCode"].ToString() == "OPEN")
            {
                datOpenDate = Convert.ToDateTime(arrThumb[0]["aucOpenDate"]);
                datCloseDate = Convert.ToDateTime(arrThumb[0]["aucCloseDate"]);

                thmThumb.SetAsOpen(
                    dblDisplayDollarValue, DateTime.Now, datCloseDate);
            }
            else if (arrThumb[0]["StatusCode"].ToString() == "UPCOMING")
            {
                datOpenDate = Convert.ToDateTime(arrThumb[0]["aucOpenDate"]);

                thmThumb.SetToBeOpened(
                    dblDisplayDollarValue,
                    datOpenDate);
            }
            else if ((arrThumb[0]["StatusCode"].ToString() == "WON") || (arrThumb[0]["StatusCode"].ToString() == "WON-NOWIN"))
            {
                thmThumb.SetAsClosed(
                    dblDisplayDollarValue);
            }
        }
    }

    void thmThumb_ThumbClick(object objSender, ThumbEventArgs teaArgs)
    {
        //???? this stopped working ?????
        Response.Redirect("AuctionDetail.aspx?aucID=" + teaArgs.AucID.ToString());
    }

}
