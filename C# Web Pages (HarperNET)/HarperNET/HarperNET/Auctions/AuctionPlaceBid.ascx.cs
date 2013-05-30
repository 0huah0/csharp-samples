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

public partial class Controls_AuctionPlaceBid : System.Web.UI.UserControl
{
    public string terms_url = ConfigurationManager.AppSettings["terms_url"];
	protected const double INCEMENT_BID = 10.0;

	public event EventHandler BidMade;
	public event EventHandlerWithCancel BidNextClick;
	public event EventHandler CancelClick;
	public event EventHandler SaveClick;
	public event EventHandlerWithCancel FinishClick;
	public event EventHandler ReturnClick;
	public event EventHandlerWithHighBid HighBidRequest;
	public event EventHandlerWithCancel OneTimeBidValidRequest;
	public event EventHandlerWithCancel AutoBidValidRequest;
	#region properties
	public bool IsOneTimeBid
	{
		get
		{
			return bfrBidForm.IsOneTimeBid;
		}
		set
		{
			bfrBidForm.IsOneTimeBid = value;
		}
	}

	public int Bid
	{
		get
		{
			return bfrBidForm.Bid;
		}
		set
		{
			bfrBidForm.Bid = value;
		}
	}

	public string Voucher
	{
		get
		{
			return bfrBidForm.Voucher;
		}
		set
		{
			bfrBidForm.Voucher = value;
		}
	}



	public string CCName
	{
		get
		{
			return binBillingInfo.CCName;
		}
		set
		{
			binBillingInfo.CCName = value;
		}
	}

	public string Email
	{
		get
		{
			return binBillingInfo.Email;
		}
		set
		{
			binBillingInfo.Email = value;
		}
	}

	public string PhoneNumber
	{
		get
		{
			return binBillingInfo.PhoneNumber;
		}
		set
		{
			binBillingInfo.PhoneNumber = value;
		}
	}

	public string CCNum
	{
		get
		{
            return binBillingInfo.ObfCCNum;
		}
		set
		{
            binBillingInfo.ObfCCNum = value;
		}
	}

	public string CCSecNum
	{
		get
		{
			return binBillingInfo.CCSecNum;
		}
		set
		{
			binBillingInfo.CCSecNum = value;
		}
	}

	public int CCMonth
	{
		get
		{
			return binBillingInfo.CCMonth;
		}
		set
		{
			binBillingInfo.CCMonth = value;
		}
	}

	public int CCYear
	{
		get
		{
			return binBillingInfo.CCYear;
		}
		set
		{
			binBillingInfo.CCYear = value;
		}
	}

	public string CCType
	{
		get
		{
			return binBillingInfo.CCType;
		}
		set
		{
			binBillingInfo.CCType = value;
		}
	}

	public string BillingAddress1
	{
		get
		{
			return binBillingInfo.BillingAddress1;
		}
		set
		{
			binBillingInfo.BillingAddress1 = value;
		}
	}

	public string BillingAddress2
	{
		get
		{
			return binBillingInfo.BillingAddress2;
		}
		set
		{
			binBillingInfo.BillingAddress2 = value;
		}
	}

	public string BillingAddress3
	{
		get
		{
			return binBillingInfo.BillingAddress3;
		}
		set
		{
			binBillingInfo.BillingAddress3 = value;
		}
	}

	public string BillingCity
	{
		get
		{
			return binBillingInfo.BillingCity;
		}
		set
		{
			binBillingInfo.BillingCity = value;
		}
	}

	public string BillingCountry
	{
		get
		{
			return binBillingInfo.BillingCountry;
		}
		set
		{
			binBillingInfo.BillingCountry = value;
		}
	}

	public string BillingRegion
	{
		get
		{
			return binBillingInfo.BillingRegion;
		}
		set
		{
			binBillingInfo.BillingRegion = value;
		}
	}

	public string BillingPostalCode
	{
		get
		{
			return binBillingInfo.BillingPostalCode;
		}
		set
		{
			binBillingInfo.BillingPostalCode = value;
		}
	}

	public string BidErrorText
	{
		get
		{
			return lblBidError.Text;
		}
		set
		{
			lblBidError.Text = value;
			if (value.Length == 0)
				lblBidError.Visible = false;
			else
				lblBidError.Visible = true;
		}
	}

	public double NextAutoBid
	{
		set
		{
			bfrBidForm.NextAutoBid = value;
		}
	}
	#endregion

	#region events
	protected void OnHighBidRequest(EventArgsWithHighBid e)
	{
		if (HighBidRequest != null)
		{
			HighBidRequest(this, e);
		}
	}

	protected void OnSaveClick(EventArgs e)
	{
		if (SaveClick != null)
		{
			SaveClick(this, e);
		}
	}

	protected void OnReturnClick(EventArgs e)
	{
		if (ReturnClick != null)
		{
			ReturnClick(this, e);
		}
	}

	protected void OnBidNextClick(EventArgsWithCancel e)
	{
		if (BidNextClick != null)
		{
			BidNextClick(this, e);
		}
	}

	protected void OnBidMade(EventArgs e)
	{
		if (BidMade != null)
		{
			BidMade(this, e);
		}
	}

	protected void OnCancelClick(EventArgs e)
	{
		if (CancelClick != null)
		{
			CancelClick(this, e);
		}
	}

	protected void OnFinishClick(
		 EventArgsWithCancel e)
	{
		if (FinishClick != null)
		{
			FinishClick(this, e);
		}
	}

	protected void OnAutoBidValidRequest(EventArgsWithCancel e)
	{
		if (AutoBidValidRequest != null)
		{
			AutoBidValidRequest(this, e);
		}
	}

	protected void OnOneTimeBidValidRequest(EventArgsWithCancel e)
	{
		if (OneTimeBidValidRequest != null)
		{
			OneTimeBidValidRequest(this, e);
		}
	}
	#endregion

	#region event handlers
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!IsPostBack)
		{
			ActivateBidTab();
		}
	}

	protected void btnEnterBid_Click(object sender, EventArgs e)
	{
		ActivateBidTab();
	}

	protected void btnBillingInfo_Click(object sender, EventArgs e)
	{
		ActivateBillingTab();
	}

	protected void btnBidNext_Click(object sender, EventArgs e)
	{
		EventArgsWithCancel eacEventArgs;

		if (!bfrBidForm.AgreeToTerms)
		{
			bfrBidForm.SetErrorMessage("You must agree to the Terms and Conditions to bid.");
			return;
		}

		eacEventArgs = new EventArgsWithCancel();

		if (bfrBidForm.IsOneTimeBid)
		{
			this.OnOneTimeBidValidRequest(eacEventArgs);

			if (eacEventArgs.Cancel)
				return;
		}
		else
		{
			this.OnAutoBidValidRequest(eacEventArgs);

			if (eacEventArgs.Cancel)
				return;
		}

		OnBidNextClick(eacEventArgs);

		if (eacEventArgs.Cancel)
			return;

        /*******************************************************************
         * if no CC #, show error and activate modify billing info tab
         *******************************************************************/
        


		ActivateConfirmSection();
	}

	protected void btnCancel_Click(object sender, EventArgs e)
	{
		OnCancelClick(e);
	}

	protected void btnSaveBilling_Click(object sender, EventArgs e)
	{
        OnSaveClick(e);
        if (!CCNum.Contains("*"))
        {
            binBillingInfo.ObfCCNum = CCNum;
        }
	}
	protected void FinishedClicked()
	{
		EventArgsWithCancel eacEventArgs;

		eacEventArgs = new EventArgsWithCancel();

		if (bfrBidForm.IsOneTimeBid)
		{
			this.OnOneTimeBidValidRequest(eacEventArgs);

			if (eacEventArgs.Cancel)
				return;
		}
		else
		{
			this.OnAutoBidValidRequest(eacEventArgs);

			if (eacEventArgs.Cancel)
				return;
		}

		OnFinishClick(eacEventArgs);

		if (eacEventArgs.Cancel)
			return;

		this.ActivateSummarySection();
	}

	protected void BackClicked()
	{
		ActivateBidSection();
	}

	protected void btnFinish1_Click(object sender, EventArgs e)
	{
		FinishedClicked();
	}
	protected void btnFinish2_Click(object sender, EventArgs e)
	{
		FinishedClicked();
	}

	protected void btnBack1_Click(object sender, EventArgs e)
	{
		BackClicked();
	}
	protected void btnBack2_Click(object sender, EventArgs e)
	{
		BackClicked();
	}

	protected void btnReturn1_Click(object sender, EventArgs e)
	{
		OnReturnClick(e);
	}
	protected void btnReturn2_Click(object sender, EventArgs e)
	{
		OnReturnClick(e);
	}
	#endregion

    public string GetClearCCNumber()
    {
        return binBillingInfo.GetClearCCNum();
    }

	public void SetBillingErrorMessage(string strMessage)
	{
		ActivateBillingTab();
		binBillingInfo.SetErrorMessage(strMessage);
	}


	public void SetBidErrorMessage(string strMessage)
	{
		ActivateBidSection();

		ActivateBidTab();

		bfrBidForm.SetErrorMessage(strMessage);
	}

	public void PopulateYearDropDown()
	{
		binBillingInfo.PopulateYearDropDown();
	}

	public void SetUSStateTable(DataTable tblStates, string strDropDownValue, string strDropDownText)
	{
		binBillingInfo.SetUSStateTable(tblStates, strDropDownValue, strDropDownText);
	}

	public void SetCountryTable(DataTable tblCountry, string strDropDownValue, string strDropDownText)
	{
		binBillingInfo.SetCountryTable(tblCountry, strDropDownValue, strDropDownText);
	}

	public void ActivateBidTab()
	{
		trBidForm.Visible = true;
		trBillingInfo.Visible = false;
		btnEnterBid.Enabled = false;
		btnBillingInfo.Enabled = true;

		tdBidTab.Attributes["class"] = "TC_TabActive";
		tdBillingTab.Attributes["class"] = "TC_TabInactive";

		ActivateBidSection();
	}

	public void ActivateBillingTab(string strDirections)
	{
		binBillingInfo.Directions = strDirections;
		ActivateBillingTabInternal();
	}

	public void ActivateBillingTab()
	{
		binBillingInfo.SetDefaultDirs();
		ActivateBillingTabInternal();
	}

	protected void ActivateBillingTabInternal()
	{
		trBidForm.Visible = false;
		trBillingInfo.Visible = true;
		btnEnterBid.Enabled = true;
		btnBillingInfo.Enabled = false;

		tdBidTab.Attributes["class"] = "TC_TabInactive";
		tdBillingTab.Attributes["class"] = "TC_TabActive";

	}

	protected void ActivateBidSection()
	{
		tblMainBid.Visible = true;
		tblConfirm.Visible = false;
		//tblSummary.Visible = false;
	}

	protected void ActivateSummarySection()
	{
		tblMainBid.Visible = false;
		tblConfirm.Visible = false;
		tblSummary.Visible = true;
	}

	protected void ActivateConfirmSection()
	{
		SetConfirmControl(bcnBidConfirm);
		SetConfirmControl(bcnBidSummary);

		tblMainBid.Visible = false;
		tblConfirm.Visible = true;
		//tblSummary.Visible = false;
	}

	protected void SetConfirmControl(Controls_BidConfirm bcnTarget)
	{
		EventArgsWithHighBid ahbHighBid = new EventArgsWithHighBid();
		double dblEstimatedBid;

		bcnTarget.CCName = binBillingInfo.CCName;
		bcnTarget.Email = binBillingInfo.Email;
		bcnTarget.PhoneNumber = binBillingInfo.PhoneNumber;
        bcnTarget.CCType = binBillingInfo.CCType;
        bcnTarget.CCNum = binBillingInfo.ObfCCNum;
		bcnTarget.CCMonth = binBillingInfo.CCMonth;
		bcnTarget.CCYear = binBillingInfo.CCYear;
		bcnTarget.CCSecNum = binBillingInfo.CCSecNum;
		bcnTarget.BillingAddress1 = binBillingInfo.BillingAddress1;
		bcnTarget.BillingAddress2 = binBillingInfo.BillingAddress2;
		bcnTarget.BillingAddress3 = binBillingInfo.BillingAddress3;
		bcnTarget.BillingCity = binBillingInfo.BillingCity;
		bcnTarget.BillingCountry = binBillingInfo.BillingCountry;
		bcnTarget.BillingRegion = binBillingInfo.BillingRegion;
		bcnTarget.BillingPostalCode = binBillingInfo.BillingPostalCode;

		if (bfrBidForm.IsOneTimeBid)
		{
			bcnTarget.SetAsOneTimeBid(
				 "You have entered a one time bid.",
				 "Bid",
				 bfrBidForm.Bid,
				 "Certificate Number",
				 bfrBidForm.Voucher,
				 bfrBidForm.Terms_OneTime);
		}
		else
		{
			OnHighBidRequest(ahbHighBid);

			dblEstimatedBid = ahbHighBid.HighBid + INCEMENT_BID;

			if (dblEstimatedBid > bfrBidForm.Bid)
			{
				dblEstimatedBid = bfrBidForm.Bid;
			}

			bcnTarget.SetAsAutoBid(
				 "You have entered an automatic bid.",
				 "Maximum Bid",
				 bfrBidForm.Bid,
				 "Certificate Number",
				 bfrBidForm.Voucher,
				 bfrBidForm.Terms_Auto,
				 "Your ESTIMATED next bid will be " + dblEstimatedBid.ToString("$###,###,##0") + ".");
		}

	}


}
