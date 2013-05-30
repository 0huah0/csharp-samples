using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HarperLINQ;

public partial class Join_Join : System.Web.UI.Page
{
    List<int> months = new List<int>() {1,2,3,4,5,6,7,8,9,10,11,12};
    List<int> years = new List<int>(Enumerable.Range(DateTime.Now.Year,10));
    ListItem blank = new ListItem("", "-1");
    protected void Page_Load(object sender, EventArgs e)
    {
        loadCountries();
        ddlExpireMonth.Items.Clear();
        ddlExpireMonth.Items.Add(blank);
        ddlExpireMonth.DataSource = months;
        ddlExpireMonth.DataBind();
        ddlExpireYear.Items.Clear();
        ddlExpireYear.Items.Add(blank);
        ddlExpireYear.DataSource = years;
        ddlExpireYear.DataBind();
    }

    private void loadCountries()
    {
        if (!IsPostBack)
        {
            List<Country> countries = HarperLINQ.Country.GetCountries();
            ddlCountry.DataSource = countries;
            ddlCountry.DataTextField = "cntName";
            ddlCountry.DataValueField = "cntName";
            ddlCountry.DataBind();
        }
    }

    protected void loadRegions(object sender, EventArgs e)
    {
        string countryName = ddlCountry.SelectedItem.Value;
        List<string> regions = HarperLINQ.State.GetRegions(countryName);
        ddlRegion.Items.Clear();
        ddlRegion.Items.Add(blank);
        ddlRegion.DataSource = regions;
        ddlRegion.DataBind();
        
        if (countryName == "U.S.A.")
        {
            lblState.Visible = true;
            lblRegion.Visible = false;
            ddlRegion.Visible = true;
            txtRegion.Visible = false;
        }

        else if (regions.Count == 0)
        {
            lblState.Visible = false;
            lblRegion.Visible = true;
            ddlRegion.Visible = false;
            txtRegion.Visible = true;
        }

        else
        {
            lblState.Visible = false;
            lblRegion.Visible = true;
            ddlRegion.Visible = true;
            txtRegion.Visible = false;
        }
    }

    protected void setCardType(object sender, EventArgs e)
    {
        string cardType = ddlCardType.SelectedValue;
        
        switch (cardType)
        {
            case "Visa":
                cardValidator.ValidationExpression = "^4[0-9]{12}(?:[0-9]{3})?$";
                break;
            case "Mastercard":
                cardValidator.ValidationExpression = "^5[1-5][0-9]{14}$";
                break;
            case "American Express":
                cardValidator.ValidationExpression = "^3[47][0-9]{13}$";
                break;
            default:
                break;
        }
    }
}
