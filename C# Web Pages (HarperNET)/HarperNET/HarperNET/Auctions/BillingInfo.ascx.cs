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

public partial class Controls_BillingInfo : System.Web.UI.UserControl
{
    protected const string DEFAULT_DIRS = "Please update your billing information.";
    
    #region Properties
    public string CCName
    {
        get
        {
            return txtCCName.Text.Trim();
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
            return txtEmail.Text.Trim();
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
            return txtPhoneNumber.Text.Trim();
        }
        set
        {
            txtPhoneNumber.Text = value;
        }
    }

    private string ClearCCNum;

    /**
     * 1/25/2012 - MPC: No longer displaying entire credit card on screen
     * Use GetClearCCNum() if you absolutely need to display it
     */
    public string ObfCCNum
    {
        get
        {
            return txtNumber.Text.Trim();
        }
        set
        {
            txtNumber.Text = string.Empty;
            if (value.Trim().Length > 4)
            {
                txtNumber.Text = new string('*', value.Trim().Length - 4) + value.Substring(value.Trim().Length - 4, 4);
            }
            ClearCCNum = value;
        }
    }

    public string CCSecNum
    {
        get
        {
            return txtSecNumber.Text.Trim();
        }
        set
        {
            txtSecNumber.Text = value;
        }
    }

    public int CCMonth
    {
        get
        {
            if (ddlMonth.SelectedIndex == 0)
            {
                return -1;
            }

            return Convert.ToInt32(ddlMonth.SelectedValue);
        }

        set
        {
            StaticMethods.SetDropDownValue(ddlMonth, value.ToString());
        }
    }

    public int CCYear
    {
        get
        {
            if (ddlYear.SelectedIndex == 0)
            {
                return -1;
            }

            return Convert.ToInt32(ddlYear.SelectedValue);
        }

        set
        {
            StaticMethods.SetDropDownValue(ddlYear, value.ToString());
        }
    }

    public string CCType
    {
        get
        {
            return ddlCreditCard.SelectedValue;
        }

        set
        {
            StaticMethods.SetDropDownValue(ddlCreditCard, value);
        }
    }

    public string BillingAddress1
    {
        get
        {
            return addBillingAddress.Address1;
        }
        set
        {
            addBillingAddress.Address1 = value;
        }
    }

    public string BillingAddress2
    {
        get
        {
            return addBillingAddress.Address2;
        }
        set
        {
            addBillingAddress.Address2 = value;
        }
    }

    public string BillingAddress3
    {
        get
        {
            return addBillingAddress.Address3;
        }
        set
        {
            addBillingAddress.Address3 = value;
        }
    }

    public string BillingCity
    {
        get
        {
            return addBillingAddress.City;
        }
        set
        {
            addBillingAddress.City = value;
        }
    }

    public string BillingCountry
    {
        get
        {
            return addBillingAddress.Country;
        }
        set
        {
            addBillingAddress.Country = value;
        }
    }

    public string BillingRegion
    {
        get
        {
            return addBillingAddress.Region;
        }
        set
        {
            addBillingAddress.Region = value;
        }
    }

    public string BillingPostalCode
    {
        get
        {
            return addBillingAddress.PostalCode;
        }
        set
        {
            addBillingAddress.PostalCode = value;
        }
    }

    public string Directions
    {
        get
        {
            return lblDirections.Text;
        }
        set
        {
            lblDirections.Text = value;
        }
    }
    #endregion

    public string GetClearCCNum()
    {
        return ClearCCNum;
    }

    public void SetUSStateTable(DataTable tblStates, string strDropDownValue, string strDropDownText)
    {
        addBillingAddress.SetUSStateTable(tblStates, strDropDownValue, strDropDownText);
    }

    public void SetCountryTable(DataTable tblCountry, string strDropDownValue, string strDropDownText) 
	 {
        addBillingAddress.SetCountryTable(tblCountry, strDropDownValue, strDropDownText);
    }

	public void SetErrorMessage(string strErrorMessage)
	 {
		 lblErrorMessage.Visible = true;
		 lblErrorMessage.Text = strErrorMessage;
	 }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            SetDefaultDirs();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        addBillingAddress.RequiredPrefix = "</br>";
    }

    public void SetDefaultDirs()
    {
        Directions = DEFAULT_DIRS;
        SetErrorMessage(string.Empty);
    }

    public void PopulateYearDropDown()
    {
        int intCounter;

        ddlYear.Items.Add(new ListItem("", ""));

        for (intCounter = DateTime.Now.Year; intCounter < (DateTime.Now.Year + 20); intCounter++)
        {
            ddlYear.Items.Add(new ListItem(intCounter.ToString(), intCounter.ToString()));
        }
    }

}
