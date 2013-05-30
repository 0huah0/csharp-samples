<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Redeem.aspx.cs" Inherits="Referral_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><asp:Literal ID="Literal1" runat="server" Text="Andrew Harper - Redeem Referral" /></title>
<link rel="Stylesheet" type="text/css" href="referral.css"/>
    
<script type="text/javascript">
    function validateCountry(source, args) {
        var ddlCountry = document.getElementById('ddlCountries');
        if (ddlCountry.selectedIndex == 0) 
            args.IsValid = false;
        else
            args.IsValid =true;
    }
    
    function validateRegion(source, args) {
        var ddlRegion = document.getElementById('ddlRegion');
        var txtRegion = document.getElementById('txtRegionNotListed');        
        if (ddlRegion.selectedIndex == 0 && txtRegion.value == "")
            args.IsValid = false;
        else
            args.IsValid = true;
    }

    function validateState(source, args) {
        var ddlRegion = document.getElementById('ddlRegion');
        if (ddlRegion.selectedIndex == 0)
            args.IsValid = false;
        else
            args.IsValid = true;
    }

</script>

</head>
<body>
    <div id="page-wrapper">
        <div id="page">
            <div id="header">
                <div class="section clearfix">
                    <a href="/" title="Home" rel="home" id="logo">
                        <img src="http://www.andrewharper.com/sites/default/files/andrew_harper_logo.gif"
                            alt="Home" /></a>
                    <div id="name-and-slogan">
                        <div id="site-slogan">
                        </div>
                    </div>
                    <!-- /#name-and-slogan -->
                </div>
            </div>
            <div id="main-wrapper">
                <div id="main">
                    <div id="content">
                        <img alt="Welcome to Andrew Harper" src="images/Redemption_header.jpg" border="0"
                            class="header-image" /><br />
                        <asp:PlaceHolder ID="lblErrorMessage" runat="server" Visible="false"></asp:PlaceHolder>
                        <form id="form1" runat="server">
                        <div class="one-column">
                            <h1 class="header-text">
                                To activate your complimentary six month Andrew Harper Premier Online membership:</h1>
                            <ol>
                                <li>Enter your contact information and a user name and password in the form to the right</li>
                                <li>Click &ldquo;Send&rdquo;</li>
                                <li>You will receive an email confirming your activation and providing your membership
                                    number</li>
                            </ol>
                            <p class="orange-bold">
                                Questions?</p>
                            <b>View our <a href="http://andrewharper.com/faq">Frequently Asked Questions</a> or</br>
                                contact Andrew Harper Member Services.</b></br>
                            <div class="contact-box">
                                <div class="contact-field">
                                    Email</div>
                                <a href="mailto:Membership@AndrewHarper.com">Membership@AndrewHarper.com</a></br>
                                <div class="contact-field">
                                    Phone</div>
                                (866) 831-4314 USA, +1 (512) 904-7342 International </br>
                                <div class="contact-field">
                                    Hours</div>
                                Monday-Friday, 8 a.m.-6 p.m. (CST)
                            </div>
                        </div>
                        <div class="one-column">
                            <i>All fields marked with a * are required.</i> 
                            
                            <ul class="primary-list"><b>Please enter your information.</b>
                                <li>
                                    <asp:Label ID="Label1" CssClass="redemption-form-label" runat="server" Text="First Name: *"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="validator" ID="req_first_name" Display="Dynamic"
                                        runat="server" ControlToValidate="first_name" Text="First Name field is required."></asp:RequiredFieldValidator><br />
                                    <asp:TextBox CssClass="redemption-form-text" ID="first_name" runat="server" MaxLength="64"
                                        Width="400"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="Label2" CssClass="redemption-form-label" runat="server" Text="Last Name: *"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="validator" ID="req_last_name" Display="Dynamic"
                                        runat="server" ControlToValidate="last_name" Text="Last Name field is required."></asp:RequiredFieldValidator><br />
                                    <asp:TextBox CssClass="redemption-form-text" ID="last_name" runat="server" MaxLength="64"
                                        Width="400"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="Label3" CssClass="redemption-form-label" runat="server" Text="Email Address: *"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="validator" ID="req_email_address" Display="Dynamic"
                                        runat="server" ControlToValidate="email_address" Text="Email Address field is required."></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass=""
                                        Display="Dynamic" runat="server" ControlToValidate="email_address" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                                        Text="Email address is not valid."></asp:RegularExpressionValidator><br />
                                    <asp:TextBox CssClass="redemption-form-text" ID="email_address" runat="server" MaxLength="64"
                                        Width="400"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="Label4" CssClass="redemption-form-label" runat="server" Text="Country: *"></asp:Label>
                                    <asp:CustomValidator CssClass="validator" ID="CustomValidator1" runat="server" Display="Dynamic"
                                        ClientValidationFunction="validateCountry" Text="Country field is required."></asp:CustomValidator><br />                                    
                                    <asp:DropDownList CssClass="redemption-form-dropdown" ID="ddlCountries" runat="server"
                                        OnSelectedIndexChanged="loadRegions" AutoPostBack="true" AppendDataBoundItems="true">
                                        <asp:ListItem Value="-1" Text="">  </asp:ListItem>
                                    </asp:DropDownList>
                                </li>
                                <li>
                                    <asp:Label ID="Label6" CssClass="redemption-form-label" runat="server" Text="Address 1: *"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="validator" ID="req_address" Display="Dynamic"
                                        runat="server" ControlToValidate="address_line_1" Text="Address 1 field is required."></asp:RequiredFieldValidator><br />
                                    <asp:TextBox CssClass="redemption-form-text" ID="address_line_1" runat="server" MaxLength="64"
                                        Width="400"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="Label7" CssClass="redemption-form-label" runat="server" Text="Address 2:"></asp:Label>
                                    <asp:TextBox CssClass="redemption-form-text" ID="address_line_2" runat="server" MaxLength="64"
                                        Width="400"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="Label8" CssClass="redemption-form-label" runat="server" Text="City: *"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="validator" Display="Dynamic" ID="req_city"
                                        runat="server" ControlToValidate="city" Text="City field is required."></asp:RequiredFieldValidator><br />
                                    <asp:TextBox CssClass="redemption-form-text" ID="city" runat="server" MaxLength="32"
                                         Width="400"></asp:TextBox>
                                </li>
                                <li>
                                    <%--show either state or region, depending on country selection--%>
                                    <asp:Label ID="lblState" CssClass="redemption-form-label" runat="server" Visible="true"
                                        Text="State: *"></asp:Label>                                    
                                    <asp:Label ID="lblRegion" CssClass="redemption-form-label" runat="server" Visible="false"
                                        Text="Region:"></asp:Label>                                   
                                    <asp:CustomValidator CssClass="validator" ID="req_ddlRegion" Display="Dynamic" runat="server"
                                        ClientValidationFunction="validateRegion" Text="Region field is required."
                                        Enabled="false"></asp:CustomValidator>     
                                    <asp:CustomValidator CssClass="validator" ID="req_ddlState" Display="Dynamic" runat="server"
                                        ClientValidationFunction="validateState" Text="State field is required."
                                        Enabled="false"></asp:CustomValidator>     
                                    <asp:RequiredFieldValidator CssClass="validator" ID="req_txtRegion" Display="Dynamic"
                                        ControlToValidate="txtRegion" runat="server" Enabled="false" Text="Region field is required."></asp:RequiredFieldValidator><br />
                                    <br />
                                    <asp:DropDownList CssClass="redemption-form-dropdown" ID="ddlRegion" runat="server"
                                        Visible="true" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                    <asp:TextBox CssClass="redemption-form-text" ID="txtRegion" runat="server" Visible="false"></asp:TextBox><br />
                                    <asp:Label ID="lblRegionNotListed" CssClass="redemption-form-label" runat="server"
                                        Visible="false" Text="State/Region not listed"></asp:Label>
                                    <asp:TextBox CssClass="redemption-form-text" ID="txtRegionNotListed" runat="server"
                                        Visible="false" Width="400"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="Label9" CssClass="redemption-form-label" runat="server" Text="ZIP/Postal Code: *"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="validator" ID="req_postal" Display="Dynamic"
                                        runat="server" ControlToValidate="postal" Text="ZIP/Postal Code field is required."></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:TextBox CssClass="redemption-form-text" ID="postal" runat="server" MaxLength="32"
                                        Width="400"></asp:TextBox>
                                </li>
                            </ul>
                            <ul id="user-name-form">
                                <li>
                                    <asp:Label CssClass="redemption-form-label" ID="lblUserName" runat="server" Text="Select User Name: *"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="validator" ControlToValidate="txtUserName"
                                        Display="Dynamic" runat="server" Text="User Name is required."></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" CssClass="validator"
                                        Display="Dynamic" ControlToValidate="txtUserName" runat="server" ValidationExpression="^[\S]{3,70}$"
                                        Text="User name must be at least 3 characters."></asp:RegularExpressionValidator><br />
                                    <asp:TextBox CssClass="redemption-form-text" ID="txtUserName" runat="server" Width="400"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label CssClass="redemption-form-label" ID="lblPassword" runat="server" Text="Select Password: *"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="validator" ControlToValidate="txtPassword"
                                        Display="Dynamic" runat="server" Text="Password is required."></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" CssClass="validator"
                                        Display="Dynamic" ControlToValidate="txtPassword" runat="server" ValidationExpression="^[\S\s]{7,40}$"
                                        Text="Your password must contain at least 7 characters."></asp:RegularExpressionValidator><br />
                                    <asp:TextBox CssClass="redemption-form-text" ID="txtPassword" runat="server" TextMode="Password" Width="400"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label CssClass="redemption-form-label" ID="lblConfirmPassword" runat="server"
                                        Text="Confirm Password: *"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" CssClass="validator" Display="Dynamic"
                                        ControlToValidate="txtConfirmPassword" runat="server" Text="Confirm Password is required."></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" CssClass="validator" Display="Dynamic"
                                        ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword" runat="server"
                                        Text="Passwords do not match."></asp:CompareValidator><br />
                                    <asp:TextBox CssClass="redemption-form-text" ID="txtConfirmPassword" runat="server"
                                        TextMode="Password" Width="400"></asp:TextBox>
                                </li>
                            </ul>
                            <p>
                            <asp:Button CssClass="submit-button" ID="btn_redeem_submit" runat="server" Text=""
                                OnClick="btnSumbit_click" /></p>
                            <p>Andrew Harper maintains a comprehensive information security program that is designed to ensure the privacy and confidentiality of your Personal Information and of those you refer. <a href="http://www.andrewharper.com/privacy-policy" target="_blank">View our Privacy Policy.</a></p>
                        </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
