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

public partial class AuctionThumb : System.Web.UI.UserControl
{
    public delegate void ThumbClickEventHandler(
        object objSender,
        ThumbEventArgs teaArgs);

    public event ThumbClickEventHandler ThumbClick;

    public int AucID
    {
        get
        {
            if (ViewState["AucID"] == null)
                return -1;
            else
                return Convert.ToInt32(ViewState["AucID"]);
        }

        set
        {
            ViewState["AucID"] = value;
        }
    }

    public string AucName
    {
        get
        {
            return btnAucName.Text;
        }
        set
        {
            btnAucName.Text = value;
        }
    }

    public string AucSubName
    {
        get
        {
            return lblAucSubName.Text;
        }
        set
        {
            lblAucSubName.Text = value;
        }
    }

    public string ImagePath
    {
        get
        {
            return ViewState["ImagePath"].ToString();
            //return btnImage.ImageUrl;
        }
        set
        {
            ViewState["ImagePath"] = value;

            btnThumb.Text = "<img src=\"" + value + "\" border=\"0\" />";

            //imgImage.Src = value;
            //btnImage.ImageUrl = value;
        }
    }

    public string ImageAlt
    {
        get
        {
            return "";
        }
        set
        {
            //imgImage.Alt = value;
        }
    }

    protected void ThumbClicked(
        ThumbEventArgs e)
    {
        if (ThumbClick != null)
        {
            ThumbClick(this, e);
        }
    }

    public void SetAsClosed(
        double dblWinningBid)
    {
        lblMoneyText.Text = "Winning Bid: " + dblWinningBid.ToString("$###,###,##0");
        lblStatusText.Text = "Auction Closed";
        lblStatusText.CssClass = "AucThumbStatusClosed";
    }

    public void SetToBeOpened(
        double dblStartingBid,
        DateTime datOpenDate)
    {
        lblMoneyText.Text = "Starting Bid: " + dblStartingBid.ToString("$###,###,##0");
        lblStatusText.Text = "Upcoming Auction";
        lblStatusText.CssClass = "AucThumbStatusUpcoming";

        lblUpTime.Text = datOpenDate.ToShortDateString() + " " + datOpenDate.ToShortTimeString();

        tdUpTD.Visible = true;
    }

    public void SetAsOpen(
        double dblHighBid,
        DateTime datNow,
        DateTime datCloseDate)
    {
        lblMoneyText.Text = "Current High Bid: " + dblHighBid.ToString("$###,###,##0");
        lblStatusText.Text = "Time Left: "
            + StaticMethods.GetTimeDiff(
            datNow, datCloseDate);
        //lblStatusText.CssClass = "AucThumbStatusOpen";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAucName_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("btnAucName_Click");

        ThumbEventArgs teaArgs;

        teaArgs = new ThumbEventArgs(this.AucID);

        ThumbClicked(teaArgs);
    }

    protected void btnThumb_Click(object sender, EventArgs e)
    {
        //System.Diagnostics.Debug.WriteLine("btnThumb_Click");

        ThumbEventArgs teaArgs;
        teaArgs = new ThumbEventArgs(this.AucID);
        ThumbClicked(teaArgs);
    }
}
