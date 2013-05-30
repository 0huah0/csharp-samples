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

public partial class Controls_BidConfirm : System.Web.UI.UserControl
{
    public string CCName
    {
        get
        {
            return txtCCName.Text;
        }
        set
        {
            txtCCName.Text = value;
        }
    }

    public string Email
    {
        get
        {
            return txtEmail.Text;
        }
        set
        {
            txtEmail.Text = value;
        }
    }

    public string PhoneNumber
    {
        get
        {
            return txtPhoneNumber.Text;
        }
        set
        {
            txtPhoneNumber.Text = value;
        }
    }

    public string CCType
    {
        get
        {
            return txtCreditCard.Text;
        }
        set
        {
            txtCreditCard.Text = value;
        }
    }

    public string CCNum
    {
        get
        {
            return txtNumber.Text;
        }
        set
        {
            txtNumber.Text = value;
        }
    }

    public int CCMonth
    {
        get
        {
            return Convert.ToInt32(txtMonth.Text);
        }
        set
        {
            txtMonth.Text = StaticMethods.PadWithZerosLen2(value);
        }
    }

    public int CCYear
    {
        get
        {
            return Convert.ToInt32(txtYear.Text);
        }
        set
        {
            txtYear.Text = StaticMethods.PadWithZerosLen2(value);
        }
    }

    public string CCSecNum
    {
        get
        {
            return txtSecNumber.Text;
        }
        set
        {
            txtSecNumber.Text = value;
        }
    }

    public string BillingAddress1
    {
        get
        {
            return txtAddress1.Text;
        }
        set
        {
            txtAddress1.Text = value;
        }
    }

    public string BillingAddress2
    {
        get
        {
            return txtAddress2.Text;
        }
        set
        {
            txtAddress2.Text = value;
        }
    }

    public string BillingAddress3
    {
        get
        {
            return txtAddress3.Text;
        }
        set
        {
            txtAddress3.Text = value;
        }
    }

    public string BillingCity
    {
        get
        {
            return txtCity.Text;
        }
        set
        {
            txtCity.Text = value;
        }
    }

    public string BillingCountry
    {
        get
        {
            return txtCountry.Text;
        }
        set
        {
            txtCountry.Text = value;
        }
    }

    public string BillingRegion
    {
        get
        {
            return txtRegion.Text;
        }
        set
        {
            txtRegion.Text = value;
        }
    }

    public string BillingPostalCode
    {
        get
        {
            return txtPostalCode.Text;
        }
        set
        {
            txtPostalCode.Text = value;
        }
    }

    public void SetAsOneTimeBid(
        string strBidTypeText,
        string strBidValueLabel,
        int dblBidValue,
        string strVoucherLabel,
        string dbVoucherValue,
        string strTermsText)
    {
        lblBidType.Text = strBidTypeText;
        trNextBidMessage.Visible = false;
        lblNextBidMessage.Text = "";
        lblBid.Text = strBidValueLabel;
        txtBid.Text = dblBidValue.ToString("$###,###,##0");
        lblVoucher.Text = strVoucherLabel;
        txtVoucher.Text = dbVoucherValue;
        lblTermsText.Text = strTermsText;
    }

    public void SetAsAutoBid(
        string strBidTypeText,
        string strBidValueLabel,
        int dblBidValue,
        string strVoucherLabel,
        string dbVoucherValue,
        string strTermsText,
        string strNextBidMessage)
    {
        lblBidType.Text = strBidTypeText;
        trNextBidMessage.Visible = true;
        lblNextBidMessage.Text = strNextBidMessage;
        lblBid.Text = strBidValueLabel;
        txtBid.Text = dblBidValue.ToString("$###,###,##0");
        lblVoucher.Text = strVoucherLabel;
        txtVoucher.Text = dbVoucherValue;
        lblTermsText.Text = strTermsText;
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

}
