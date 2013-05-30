using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App_MarketPlace_AuctionDetail : Page
{
    // ***********************************************
    // META TAG - Values
    public const string META_DESC = "The ultimate guide to the world’s most exclusive luxury hotels  and exotic resorts. Andrew Harper has long been regarded as the ultimate authority on worldwide luxury travel.";
    public const string META_KEYWORDS = "Luxury Travel, Luxury Hotel, Luxury Resort, Luxury Vacation, Luxury Cruise, Hotel Review, Resort Review, Exotic Hideaways, Exotic Travel, Exotic Resort, Andrew Harper's Hideaway Report, Andrew Harper Travel";
    // ***********************************************

    DataSet m_dasAuction;

    protected int AuctionID
    {
        get
        {
            return Convert.ToInt32(Request.QueryString["aucID"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // ***********************************************
        // META TAG - First Line in the Page_Load
        //((Main)Master).MetaDescription = META_DESC;
        //((Main)Master).MetaKeywords = META_KEYWORDS;
        // ***********************************************

        PopulateForm();
    }

    protected void PopulateForm()
    {
        try
        {
            MarketPlaceDataLayer mdlDataLayer = DataLayerFactory.GetMarketPlace();
            XMLCoverter conConverter = new XMLCoverter();

            m_dasAuction = mdlDataLayer.AucGetAuctionDetailForAuctionID(AuctionID, true);

            lblTitle.Text = m_dasAuction.Tables[0].Rows[0]["adeName"].ToString();
            lblSubTitle.Text = m_dasAuction.Tables[0].Rows[0]["adeSubName"].ToString();

            btnPlaceBid1.Visible = Session["MemberId"]!=null && Session["MemberId"].ToString() != string.Empty;
            if (m_dasAuction.Tables[0].Rows[0]["StatusCode"].ToString() != "OPEN")
            {
                btnPlaceBid1.Visible = false;
            }

            conConverter.AuctionTypeReached += conConverter_AuctionTypeReached;
            conConverter.AddControls(m_dasAuction.Tables[0].Rows[0]["adeXML"].ToString(), plaDetails, this.Page);
        }
        catch (Exception ex)
        {
            Response.Redirect("Default.aspx",false);
        }
    }

    void conConverter_AuctionTypeReached(PlaceHolder plaPlaceHolder)
    {
        Controls_AuctionBidSummary absSummary = 
			  (Controls_AuctionBidSummary)LoadControl("AuctionBidSummary.ascx");

		  absSummary.PlaceBidClick += absSummary_PlaceBidClick;
		  plaPlaceHolder.Controls.Add(absSummary);
        Controls_AuctionBidSummary.PopulateBidSummary(absSummary, m_dasAuction);
    }

    void absSummary_PlaceBidClick(object sender, EventArgs e)
    {
        RedirectToBidPage();
    }

    protected void btnPlaceBid1_Click(object sender, EventArgs e)
    {
        RedirectToBidPage();
    }

    protected void RedirectToBidPage()
    {
        Response.Redirect("BidPage.aspx?aucID=" + this.AuctionID.ToString());
    }

    protected void btnReturn1_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewAll.aspx");
    }
    protected void btnReturn2_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewAll.aspx");
    }

}
