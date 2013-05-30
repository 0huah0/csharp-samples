using System;
using System.Web.UI;
using System.Configuration;

public partial class Controls_BidForm : UserControl
{
	//public event EventHandler BidTypeEventHandler;
    public string TERMS_ONETIME = "I agree to the <a href='" + ConfigurationManager.AppSettings["terms_url"] + "' target='_blank' class='ContentGrn'>Terms & Conditions</a> for this auction.";
    public string TERMS_AUTO = "If outbid, I agree my bid will increase by $10 increments until my maximum bid is reached.  I agree to the <a href='" + ConfigurationManager.AppSettings["terms_url"] + "' target='_blank' class='ContentGrn'>Terms & Conditions</a> for this auction.";

    #region properties
    public string Terms_OneTime
    {
        get
        {
            return TERMS_ONETIME;
        }
    }

    public string Terms_Auto
    {
        get
        {
            return TERMS_AUTO;
        }
    }

    public int Bid
    {
        get
        {
            return Convert.ToInt32(txtBid.Text);
        }
        set
        {
            txtBid.Text = value.ToString();
        }
    }

    public string Voucher
    {
        get
        {
            return txtVoucher.Text.ToString();
        }
        set
        {
            txtVoucher.Text = value.ToString();
        }
    }

    public bool AgreeToTerms
    {
        get
        {
            return chkTerms.Checked;
        }
        set
        {
            chkTerms.Checked = value;
        }
    }

    public double NextAutoBid
    {
        set
        {
            lblNextAutoBid.Text = value.ToString("$###,###,##0");
        }
    }

    public bool IsOneTimeBid
    {
        get
        {
            if (rblBidType.SelectedValue == "Auto")
                return false;
            else
                return true;
        }

        set
        {
            if (value)
                rblBidType.SelectedIndex = 1;
            else
                rblBidType.SelectedIndex = 0;

            ManageAutoBid();
        }
    }
    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMessage.Visible = false;

        ManageAutoBid();
    }

    public void SetErrorMessage(string strErrorMessage)
    {
        lblErrorMessage.Visible = true;
        lblErrorMessage.Text = strErrorMessage;
    }

    protected void ManageAutoBid()
    {
        if (rblBidType.SelectedValue == "Auto")
        {
            if (!String.IsNullOrEmpty(lblNextAutoBid.Text))
                trNextAutoBid.Visible = true;
            lblBid.Text = "Maximum Bid";
            chkTerms.Text = TERMS_AUTO;
            reqBid.ErrorMessage = "<br>Maximum Bid is required.";
            reqBid2.ErrorMessage = "<br>Maximum Bid is invalid.";
        }
        else
        {
            trNextAutoBid.Visible = false;
            lblBid.Text = "Bid";
            chkTerms.Text = TERMS_ONETIME;
            reqBid.ErrorMessage = "<br>Bid is required.";
            reqBid2.ErrorMessage = "<br>Bid is invalid.";
        }
    }

	protected void rblBidType_SelectedIndexChanged(object sender, EventArgs e)
	{
		ManageAutoBid();
	}
}
