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

public partial class Controls_AuctionBidSummary : System.Web.UI.UserControl
{
    public event EventHandler PlaceBidClick;

    public string AuctionNumber
    {
        get
        {
            return lblAuctionNum.Text;
        }
        set
        {
            lblAuctionNum.Text = value;
        }
    }

    public string Notes
    {
        get
        {
            return lblNotes.Text;
        }
        set
        {
            lblNotes.Text = value;
        }
    }

    public int Quantity
    {
        get
        {
            return Convert.ToInt32(lblQuantity.Text);
        }
        set
        {
            lblQuantity.Text = value.ToString();
        }
    }

    public double Retail
    {
        get
        {
            return Convert.ToDouble(lblRetail);
        }
        set
        {
            lblRetail.Text = value.ToString("$###,###,##0");
        }
    }

    public double StartingBid
    {
        get
        {
            return Convert.ToDouble(lblStartingBid);
        }
        set
        {
            lblStartingBid.Text = value.ToString("$###,###,##0");
        }
    }

    public DateTime DateCloses
    {
        set
        {
            lblCloseDate.Text = value.ToLongDateString() + " " + value.ToLongTimeString();
        }
    }

    public bool ShowBidButton
    {
        get
        {
            return btnPlaceBid1.Visible;
        }
        set
        {
            btnPlaceBid1.Visible = value && Session["MemberId"] != null && Session["MemberId"].ToString() != string.Empty;
        }
    }

    protected void OnPlaceBidClick(EventArgs e)
    {
        if (this.PlaceBidClick != null)
        {
            PlaceBidClick(this, e);
        }
    }

    protected void Page_Load(object sender, EventArgs e){}

    public void SetAsClosed(
        double dblWinningBid,
        string strWinnerBidder)
    {
        lblMoneyLabel.Text = "Winning Bid";
        lblMoney.Text = dblWinningBid.ToString("$###,###,##0");

        lblTopBidderLabel.Text = "Winning Bidder";
        lblTopBidder.Text = strWinnerBidder;

        lblStatus.Text = "Closed";
        lblStatus.CssClass = "AucThumbStatusClosed";

        trTimeLeft.Visible = false;
        trLocalTime.Visible = false;

        btnPlaceBid1.Visible = false;
    }

    public void SetToBeOpened(
        DateTime datNow,
        DateTime datOpens)
    {
        trMoney.Visible = false;
        trTopBidder.Visible = false;

        lblStatus.Text = "To Be Opened";
        lblStatus.CssClass = "AucThumbStatusUpcoming";

        trTimeLeft.Visible = false;

        lblLocalTime.Text = datNow.ToLongDateString() + " " + datNow.ToLongTimeString();

        trDateOpen.Visible = true;

        lblOpenDate.Text = datOpens.ToLongDateString() + " " + datOpens.ToLongTimeString();

        btnPlaceBid1.Visible = false;
    }

    public void SetAsOpen(
        double dblHighBid,
        string strHighBidder,
        DateTime datNow,
        DateTime datCloseDate)
    {
        lblMoneyLabel.Text = "High Bid";
        lblMoney.Text = dblHighBid.ToString("$###,###,##0");

        lblTopBidderLabel.Text = "High Bidder";
        lblTopBidder.Text = strHighBidder;

        lblStatus.Text = "Open";
        lblStatus.CssClass = "AucThumbStatusOpen";

        lblTimeLeft.Text = StaticMethods.GetTimeDiff(
            datNow, datCloseDate);

        lblLocalTime.Text = datNow.ToLongDateString() + " " + datNow.ToLongTimeString();
    }

    protected void btnPlaceBid1_Click(object sender, EventArgs e)
    {
        OnPlaceBidClick(e);
    }

    public static void PopulateBidSummary(
        Controls_AuctionBidSummary absSummary,
        DataSet dasAuction)
    {
        double dblDisplayDollarValue;
        DateTime datOpenDate;
        DateTime datCloseDate;
        DateTime datLocalTime;

        absSummary.AuctionNumber = dasAuction.Tables[0].Rows[0]["aucNum"].ToString();
        absSummary.Quantity = Convert.ToInt32(dasAuction.Tables[0].Rows[0]["aucQuantity"]);
        absSummary.Retail = Convert.ToDouble(dasAuction.Tables[0].Rows[0]["adeRetail"]);
        absSummary.StartingBid = Convert.ToDouble(dasAuction.Tables[0].Rows[0]["aucStartingBid"]);
        absSummary.DateCloses = Convert.ToDateTime(dasAuction.Tables[0].Rows[0]["aucCloseDate"]);
        absSummary.Notes = dasAuction.Tables[0].Rows[0]["aucNotes"].ToString();

        dblDisplayDollarValue = Convert.ToDouble(dasAuction.Tables[0].Rows[0]["DisplayDollar"]);
        datLocalTime = Convert.ToDateTime(dasAuction.Tables[0].Rows[0]["LocalTime"]);

        absSummary.ShowBidButton = false;
        if (dasAuction.Tables[0].Rows[0]["StatusCode"].ToString() == "OPEN")
        {
            absSummary.ShowBidButton = true; //will only actually set to true if memberid is in session
            datOpenDate = Convert.ToDateTime(dasAuction.Tables[0].Rows[0]["aucOpenDate"]);
            datCloseDate = Convert.ToDateTime(dasAuction.Tables[0].Rows[0]["aucCloseDate"]);

            absSummary.SetAsOpen(
                dblDisplayDollarValue,
                dasAuction.Tables[0].Rows[0]["DisplayName"].ToString(),
                datLocalTime,
                datCloseDate);
        }
        else if (dasAuction.Tables[0].Rows[0]["StatusCode"].ToString() == "UPCOMING")
        {
            datOpenDate = Convert.ToDateTime(dasAuction.Tables[0].Rows[0]["aucOpenDate"]);
            absSummary.SetToBeOpened(datLocalTime, datOpenDate);
        }
        else if ((dasAuction.Tables[0].Rows[0]["StatusCode"].ToString() == "WON") || (dasAuction.Tables[0].Rows[0]["StatusCode"].ToString() == "WON-NOWIN"))
        {
            absSummary.SetAsClosed(dblDisplayDollarValue, dasAuction.Tables[0].Rows[0]["DisplayName"].ToString());
        }
    }
}
