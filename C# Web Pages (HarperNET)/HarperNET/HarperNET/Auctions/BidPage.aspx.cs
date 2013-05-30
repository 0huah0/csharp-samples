
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using HarperCRYPTO;
using HarperLINQ;

public partial class App_MarketPlace_BidPage : System.Web.UI.Page
{
	// ***********************************************
	// META TAG - Values
	public const string META_DESC = "The ultimate guide to the world’s most exclusive luxury hotels  and exotic resorts. Andrew Harper has long been regarded as the ultimate authority on worldwide luxury travel.";
	public const string META_KEYWORDS = "Luxury Travel, Luxury Hotel, Luxury Resort, Luxury Vacation, Luxury Cruise, Hotel Review, Resort Review, Exotic Hideaways, Exotic Travel, Exotic Resort, Andrew Harper's Hideaway Report, Andrew Harper Travel";
	// ***********************************************

	DataSet m_dasAuction;
	DataSet m_dasDropDowns;
	DataSet m_dasBillingInfo;
	bool m_bolHasBidForm = false;
    bool m_bolHasCustomerRecord = false;
    private string Username
    {
        get { return ViewState["Username"].ToString(); }
        set { ViewState["Username"] = value; }
    }
    
        /**
         * 1/25/2012 - MPC:
         * MemberId must now be encrypted using Cryptography.Encrypt256() and decrypted using Cryptography.Decrypt256FromHEX()
         * These methods are much more secure
         */
    public string MemberId
    {
        get { return Cryptography.Decrypt256FromHEX(Session["MemberId"].ToString()); }
    }
	protected int AuctionID
	{
		get
		{
			return Convert.ToInt32(Request.QueryString["aucID"]);
		}
	}

	protected void Page_Init(object sender, EventArgs e)
	{
        SetCustomerFlags();
        SetDropDownTable();
        PopulateForm();
	}
	protected void SetDropDownTable()
	{
		MarketPlaceDataLayer mdlDataLayer = DataLayerFactory.GetMarketPlace();
		m_dasDropDowns = mdlDataLayer.AucGetBidPageDropDowns();
	}
	protected void SetCustomerFlags()
	{
        try
        {
            tbl_Customer Member = new tbl_Customer(long.Parse(MemberId),false);
		    m_bolHasCustomerRecord = true;
            Username = Member.cusUserName;
            m_dasBillingInfo = DataLayerFactory.GetMarketPlace().AucGetBillingInfo(Username);
        }
        catch (Exception ex)
        {
		    m_bolHasCustomerRecord = false;
            return;
        }
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		// ***********************************************
		// META TAG - First Line in the Page_Load
		//((Main)Master).MetaDescription = META_DESC;
		//((Main)Master).MetaKeywords = META_KEYWORDS;
		// ***********************************************
	}
	protected void PopulateForm()
	{
		MarketPlaceDataLayer mdlDataLayer = DataLayerFactory.GetMarketPlace();
		XMLCoverter conConverter = new XMLCoverter();

		m_dasAuction = mdlDataLayer.AucGetAuctionDetailForAuctionID(AuctionID, false);
		lblTitle.Text = m_dasAuction.Tables[0].Rows[0]["adeName"].ToString();
		lblSubTitle.Text = m_dasAuction.Tables[0].Rows[0]["adeSubName"].ToString();
		conConverter.AuctionTypeReached += conConverter_AuctionTypeReached;
		m_bolHasBidForm = false;
		conConverter.AddControls(m_dasAuction.Tables[0].Rows[0]["adeXML"].ToString(), plaDetails, this.Page, false);
	}
	protected void conConverter_AuctionTypeReached(PlaceHolder plaPlaceHolder)
	{
		if (m_bolHasBidForm)
		{
			return;
		}

		Controls_AuctionBidSummary absSummary = (Controls_AuctionBidSummary)LoadControl("AuctionBidSummary.ascx");
		absSummary.EnableViewState = false;
		plaPlaceHolder.Controls.Add(absSummary);
		Controls_AuctionBidSummary.PopulateBidSummary(absSummary, m_dasAuction);

        if (!m_bolHasCustomerRecord)
        {
            //AddNoCustomerRecordControl();
            return;
        }
		absSummary.ShowBidButton = false;

		Controls_AuctionPlaceBid apbPlaceBid = (Controls_AuctionPlaceBid)LoadControl("AuctionPlaceBid.ascx");
		#region set up apbPlaceBid
		apbPlaceBid.PopulateYearDropDown();
        SetBillingInformation(apbPlaceBid);
        apbPlaceBid.NextAutoBid = Convert.ToDouble(m_dasAuction.Tables[0].Rows[0]["DisplayDollar"]) + 10.0;
		apbPlaceBid.BidNextClick += apbPlaceBid_BidNextClick;
		apbPlaceBid.CancelClick += apbPlaceBid_CancelClick;
		apbPlaceBid.SaveClick += apbPlaceBid_SaveClick;
		apbPlaceBid.FinishClick += apbPlaceBid_FinishClick;
		apbPlaceBid.ReturnClick += apbPlaceBid_ReturnClick;
		apbPlaceBid.HighBidRequest += apbPlaceBid_HighBidRequest;
		apbPlaceBid.AutoBidValidRequest += apbPlaceBid_AutoBidValidRequest;
		apbPlaceBid.OneTimeBidValidRequest += apbPlaceBid_OneTimeBidValidRequest;
		#endregion
		plaPlaceHolder.Controls.Add(apbPlaceBid);
		m_bolHasBidForm = true;
	}

    void SetBillingInformation(Controls_AuctionPlaceBid apbPlaceBid)
    {
        apbPlaceBid.SetCountryTable(m_dasDropDowns.Tables[0], "cntCode", "cntName");
        apbPlaceBid.SetUSStateTable(m_dasDropDowns.Tables[1], "staCode", "staName");
        apbPlaceBid.CCName = m_dasBillingInfo.Tables[1].Rows[0]["CCName"].ToString();
        apbPlaceBid.Email = m_dasBillingInfo.Tables[1].Rows[0]["Email"].ToString();
        apbPlaceBid.PhoneNumber = m_dasBillingInfo.Tables[1].Rows[0]["Phone1"].ToString();
        apbPlaceBid.CCType = m_dasBillingInfo.Tables[1].Rows[0]["CardType"].ToString();
        apbPlaceBid.CCNum = m_dasBillingInfo.Tables[1].Rows[0]["CardNum"].ToString();
        apbPlaceBid.CCMonth = Convert.ToInt32(m_dasBillingInfo.Tables[1].Rows[0]["ExpMo"]);
        apbPlaceBid.CCYear = Convert.ToInt32(m_dasBillingInfo.Tables[1].Rows[0]["ExpYr"]);
        apbPlaceBid.CCSecNum = m_dasBillingInfo.Tables[1].Rows[0]["SecNum"].ToString();
        apbPlaceBid.BillingAddress1 = m_dasBillingInfo.Tables[1].Rows[0]["Address1"].ToString();
        apbPlaceBid.BillingAddress2 = m_dasBillingInfo.Tables[1].Rows[0]["Address2"].ToString();
        apbPlaceBid.BillingAddress3 = m_dasBillingInfo.Tables[1].Rows[0]["Address3"].ToString();
        apbPlaceBid.BillingCity = m_dasBillingInfo.Tables[1].Rows[0]["City"].ToString();
        apbPlaceBid.BillingCountry = m_dasBillingInfo.Tables[1].Rows[0]["Country"].ToString();
        apbPlaceBid.BillingRegion = m_dasBillingInfo.Tables[1].Rows[0]["Region"].ToString();
        apbPlaceBid.BillingPostalCode = m_dasBillingInfo.Tables[1].Rows[0]["PostalCode"].ToString();
    }
	void apbPlaceBid_OneTimeBidValidRequest(object sender, EventArgsWithCancel e)
	{
		MarketPlaceDataLayer mdlDataLayer = DataLayerFactory.GetMarketPlace();
		Controls_AuctionPlaceBid abbBid = (Controls_AuctionPlaceBid)sender;
		DataSet dasResult = mdlDataLayer.AucValidateOneTimeBid(AuctionID, abbBid.Bid);

		if (dasResult.Tables[0].Rows[0]["Mess"].ToString() != "")
		{
			abbBid.SetBidErrorMessage(dasResult.Tables[0].Rows[0]["Mess"].ToString());
			e.Cancel = true;
		}
	}
	void apbPlaceBid_AutoBidValidRequest(object sender, EventArgsWithCancel e)
	{
		MarketPlaceDataLayer mdlDataLayer = DataLayerFactory.GetMarketPlace();
		Controls_AuctionPlaceBid abbBid = (Controls_AuctionPlaceBid)sender;
		DataSet dasResult = mdlDataLayer.AucValidateAutoBid(AuctionID, abbBid.Bid);

		if (dasResult.Tables[0].Rows[0]["Mess"].ToString() != "")
		{
			abbBid.SetBidErrorMessage(dasResult.Tables[0].Rows[0]["Mess"].ToString());
			e.Cancel = true;
		}
	}
	void apbPlaceBid_HighBidRequest(object sender, EventArgsWithHighBid e)
	{
		if (m_dasAuction != null)
		{
			e.HighBid = Convert.ToDouble(m_dasAuction.Tables[0].Rows[0]["DisplayDollar"]);
		}
	}
	void apbPlaceBid_ReturnClick(object sender, EventArgs e)
	{
		Response.Redirect("AuctionDetail.aspx?aucID=" + this.AuctionID.ToString());
	}
	void apbPlaceBid_FinishClick(object sender, EventArgsWithCancel e)
	{
		MarketPlaceDataLayer mdlDataLayer = DataLayerFactory.GetMarketPlace();
		Controls_AuctionPlaceBid abbBid = (Controls_AuctionPlaceBid)sender;

		if (abbBid.IsOneTimeBid)
		{
			try
			{
                mdlDataLayer.AucInsertNewBid(AuctionID, abbBid.Bid, abbBid.Voucher, Username);
			}
			catch (Exception ex)
			{
                if (ex.Message.Contains("Error:HB"))
                {
                    /* MPC 2010-07-29
                     * This line of code prevents the error message from being displayed
                     * 
                     * Also, this error is being displayed when an employee bids, we need
                     * to look at the usp_AucInsertNewBid stored procedure when we have 
                     * some time or when we re-write this.
                     */
                    //abbBid.ActivateBidTab();
                    abbBid.BidErrorText = "Your bid amount is less than or equal to an existing autobid. Please choose another amount.";
                    e.Cancel = true;
                    return;
                }
                else
                {
                    abbBid.BidErrorText = "We encountered an error.  "+ex.Message;
                    e.Cancel = true;
                    return;
                }
			}
		}
		else
		{
			try
			{
                mdlDataLayer.AucInsertAutoBid(AuctionID, abbBid.Bid, abbBid.Voucher, Username);
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("Error:AB"))
                {
                    /* MPC 2010-07-29
                     * This line of code prevents the error message from being displayed
                     */
					//abbBid.ActivateBidTab();
					abbBid.BidErrorText = "Your bid amount matches an existing bid. Please choose another amount.";
					e.Cancel = true;
					return;
				}
			}
		}
		lblTitle.Text = "Bid Confirmation: " + lblTitle.Text;
	}
	void apbPlaceBid_BidNextClick(object sender, EventArgsWithCancel e)
	{
		Controls_AuctionPlaceBid abbBid = (Controls_AuctionPlaceBid)sender;

		if (m_dasBillingInfo == null)
		{
			throw new Exception("Billing info is null.");
		}

        if (m_dasBillingInfo.Tables[0].Rows.Count <= 0 || 
            m_dasBillingInfo.Tables[1] == null ||
            m_dasBillingInfo.Tables[1].Rows.Count <= 0 ||
            string.IsNullOrEmpty(m_dasBillingInfo.Tables[1].Rows[0]["CardNum"].ToString()))
		{
			abbBid.ActivateBillingTab("Now that we have your bid please enter your billing information.");
			e.Cancel = true;
		}
	}
	void apbPlaceBid_SaveClick(object sender, EventArgs e)
	{
		Controls_AuctionPlaceBid abbBid = (Controls_AuctionPlaceBid)sender;

        string ccNum = abbBid.CCNum.Contains("*") ? abbBid.GetClearCCNumber() : abbBid.CCNum;
        string ccCode = abbBid.CCSecNum;

        if (!Luhn.ValidateCCNum(ccNum, abbBid.CCType))
		{
            /* MPC 2010-07-29
             * make sure we display the actual cc data that we have in the database
             */
            SetBillingInformation(abbBid);
            abbBid.SetBillingErrorMessage("<br />The credit card number you entered appears to be invalid. Please verify your credit card number and try again.");
			abbBid.ActivateBillingTab("Invalid credit card number.");
            
			return;
		}

		MarketPlaceDataLayer mdlDataLayer = DataLayerFactory.GetMarketPlace();

        
		mdlDataLayer.AucUpdateBillingInfo(
             Username,
			 abbBid.CCName,
			 abbBid.PhoneNumber,
			 "",
			 abbBid.Email,
             abbBid.CCType,
             ccNum,
			 abbBid.CCMonth,
			 abbBid.CCYear,
			 ccCode,
			 abbBid.BillingAddress1,
			 abbBid.BillingAddress2,
			 abbBid.BillingAddress3,
			 abbBid.BillingCity,
			 abbBid.BillingRegion,
			 abbBid.BillingCountry,
			 abbBid.BillingPostalCode);

		abbBid.SetBidErrorMessage("Your billing information has been saved.  Please ensure that your bid is correct.  Then click next.");

		abbBid.ActivateBidTab();
	}
	void apbPlaceBid_CancelClick(object sender, EventArgs e)
	{
		Response.Redirect("AuctionDetail.aspx?aucID=" + this.AuctionID.ToString());
	}

    //Does this control even exist?
    //protected void AddNoCustomerRecordControl()
    //{
    //    Control ctrTemp = LoadControl("NoCustomerRecord.ascx");
    //    plaDetails.Controls.Add(ctrTemp);
    //}

}
