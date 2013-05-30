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

public partial class Controls_Address_Default : System.Web.UI.UserControl
{
    const string ADDRESS1 = "Address1 is required.";
    const string COUNTRY = "Country is required.";
    const string STATE = "State is required.";
    const string CITY = "City is required.";
    const string ZIP = "Zip/Postal Code is required.";
    protected const string USA_VALUE = "USA";

    public string Address1
    {
        get
        {
            return this.txtAddress1.Text;
        }
        set
        {
            this.txtAddress1.Text = value;
        }
    }

    public int Address1MaxLen
    {
        get
        {
            return this.txtAddress1.MaxLength;
        }
        set
        {
            this.txtAddress1.MaxLength = value;
        }
    }

    public string Address2
    {
        get
        {
            return this.txtAddress2.Text;
        }
        set
        {
            this.txtAddress2.Text = value;
        }
    }

    public int Address2MaxLen
    {
        get
        {
            return this.txtAddress2.MaxLength;
        }
        set
        {
            this.txtAddress2.MaxLength = value;
        }
    }

    public string Address3
    {
        get
        {
            return this.txtAddress3.Text;
        }
        set
        {
            this.txtAddress3.Text = value;
        }
    }

    public int Address3MaxLen
    {
        get
        {
            return this.txtAddress3.MaxLength;
        }
        set
        {
            this.txtAddress3.MaxLength = value;
        }
    }

    public string Country
    {
        get
        {
            return this.ddlCountry.SelectedValue;
        }
        set
        {
            StaticMethods.SetDropDownValue(
                ddlCountry, value);

            ModifyForUSA();
        }
    }

    public string Region
    {
        get
        {
            if (Country == USA_VALUE)
            {
                return ddlState.SelectedValue;
            }
            else
            {
                return this.txtRegion.Text;
            }
        }

        set
        {
            if (Country == USA_VALUE)
            {
                StaticMethods.SetDropDownValue(
                    this.ddlState, value);
            }
            else
            {
                this.txtRegion.Text = value;
            }
        }
    }

    public string City
    {
        get
        {
            return this.txtCity.Text;
        }

        set
        {
            this.txtCity.Text = value;
        }
    }

    public int CityMaxLen
    {
        get
        {
            return this.txtCity.MaxLength;
        }
        set
        {
            this.txtCity.MaxLength = value;
        }
    }

    public string PostalCode
    {
        get
        {
            return this.txtPostalCode.Text;
        }

        set
        {
            this.txtPostalCode.Text = value;
        }
    }

    public int PostalCodeMaxLen
    {
        get
        {
            return this.txtPostalCode.MaxLength;
        }
        set
        {
            this.txtPostalCode.MaxLength = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
            Reset();
    }

    protected void SetUSASettings(
        bool bolIsUSA)
    {
        this.trRegion.Visible = !bolIsUSA;
        this.trState.Visible = bolIsUSA;
        this.reqZip.Visible = bolIsUSA;
    }

    protected void Reset()
    {
        //this.ddlCountry.SelectedIndex = 0;

        ModifyForUSA();
    }

    protected void ModifyForUSA()
    {
        if (ddlCountry.SelectedValue == USA_VALUE)
        {
            SetUSASettings(true);
        }
        else
        {
            SetUSASettings(false);
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModifyForUSA();
    }

	 public void SetCountryTable(DataTable tblCountries, string strDropDownValue, string strDropDownText)
	 {
		 ddlCountry.DataSource = tblCountries;
		 ddlCountry.DataValueField = strDropDownValue;
		 ddlCountry.DataTextField = strDropDownText;
		 ddlCountry.DataBind();

		 ddlCountry.Items.Insert(0, new ListItem("", ""));
	 }

	 public void SetUSStateTable(DataTable tblStates, string strDropDownValue, string strDropDownText)
	 {
		 ddlState.DataSource = tblStates;
		 ddlState.DataValueField = strDropDownValue;
		 ddlState.DataTextField = strDropDownText;
		 ddlState.DataBind();

		 ddlState.Items.Insert(0, new ListItem("", ""));
	 }

    public string RequiredPrefix
    {
        get
        {
            if (ViewState["RequiredPrefix"] != null)
                return "";
            else
                return ViewState["RequiredPrefix"].ToString();
        }
        set
        {
            ViewState.Add("RequiredPrefix", value);

            SetErrorMessages(value);
        }

    }

    protected void SetErrorMessages(string strPrefix)
    {
        if (strPrefix != "")
            strPrefix = strPrefix + " ";

        reqAddress1.ErrorMessage = strPrefix + ADDRESS1;
        reqCountry.ErrorMessage = strPrefix + COUNTRY;
        reqState.ErrorMessage = strPrefix + STATE;
        reqCity.ErrorMessage = strPrefix + CITY;
        reqZip.ErrorMessage = strPrefix + ZIP;
    }
}
