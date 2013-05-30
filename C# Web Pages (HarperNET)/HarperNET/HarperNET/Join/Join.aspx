<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Join.aspx.cs" Inherits="Join_Join" Culture="auto" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Andrew Harper</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="UserNameForm">
        <ul>
            <li>
                <asp:Label ID="lblUserName" runat="server" Text="Select User Name: *"></asp:Label>
                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtUserName" runat="server" Text=""></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="txtUserName" runat="server" ValidationExpression="^[a-zA-Z0-9]{7,40}$"
                    Text="User name must be at least 7 characters."></asp:RegularExpressionValidator>
            </li>
            <li>
                <asp:Label ID="lblPassword" runat="server" Text="Select Password: *"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtPassword" runat="server" Text=""></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="txtPassword" runat="server" ValidationExpression="^[a-zA-Z0-9]{7,40}$"
                    Text=""></asp:RegularExpressionValidator>
            </li>
            <li>
                <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password: *"></asp:Label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtConfirmPassword" runat="server"
                    Text=""></asp:RequiredFieldValidator>
                <asp:CompareValidator ControlToValidate="txtConfirmPassword" ControlToCompare="txtPassword"
                    runat="server" Text=""></asp:CompareValidator>
            </li>
        </ul>
    </div>
    <div id="YourInfo">
        <ul>
            <li>
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="ddlTitle" runat="server">
                </asp:DropDownList>
            </li>
            <li>
                <asp:Label ID="lblFirstName" runat="server" Text="First Name: *"></asp:Label>
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtFirstName" runat="server"
                    Text=""></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblLastName" runat="server" Text="Last Name: *"></asp:Label>
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtLastname" runat="server"
                    Text=""></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblSuffix" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="ddlSuffix" runat="server">
                </asp:DropDownList>
            </li>
            <li>
                <asp:Label ID="lblAddressLine1" runat="server" Text="Address 1: *"></asp:Label>
                <asp:TextBox ID="txtAddressLine1" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtAddressLine1" runat="server"
                    Text=""></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblAddressLine2" runat="server" Text="Address 2:"></asp:Label>
                <asp:TextBox ID="txtAddressLine2" runat="server"></asp:TextBox>
            </li>
            <li>
                <asp:Label ID="lblCity" runat="server" Text="City: *"></asp:Label>
                <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtCity" runat="server"
                    Text=""></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblCountry" runat="server" Text="Country: *"></asp:Label>
                <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true" 
                    AutoPostBack="true" OnSelectedIndexChanged = "loadRegions">
                <asp:ListItem Text="" Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </li>
            <li>
                <asp:Label ID="lblState" runat="server" Text="State: *" Visible="false"></asp:Label>
                <asp:Label ID="lblRegion" runat="server" Text="Region:" Visible="false"></asp:Label>
                <asp:DropDownList ID="ddlRegion" runat="server" Visible="false" AppendDataBoundItems="true">
                </asp:DropDownList>
                <asp:TextBox ID="txtRegion" runat="server" Visible="false"></asp:TextBox>
            </li>
            <li>
                <asp:Label ID="lblPostal" runat="server" Text="ZIP/Postal Code: *"></asp:Label>
                <asp:TextBox ID="txtPostal" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtPostal" runat="server"
                    Text=""></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtPhone" runat="server"
                    Text=""></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblEmail" runat="server" Text="Email Address: *"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtEmail" runat="server"
                    Text=""></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="txtEmail" runat="server" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                    Text=""></asp:RegularExpressionValidator>
            </li>
        </ul>
    </div>
    <div id="Payment">
        <ul>
            <li>
                <asp:Label ID="lblCardType" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="ddlCardType" runat="server" OnSelectedIndexChanged="setCardType" AutoPostBack = "true">
                    <asp:ListItem Text=""></asp:ListItem>
                    <asp:ListItem Text="Visa"></asp:ListItem>
                    <asp:ListItem Text="Mastercard"></asp:ListItem>
                    <asp:ListItem Text="American Express"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ControlToValidate="ddlCardType" runat="server"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:Label ID="lblCardNumber" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtCardNumber" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtCardNumber" runat="server"
                    Text=""></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID = "cardValidator" ControlToValidate="TxtCardNumber" runat="server"
                     Text=""></asp:RegularExpressionValidator>
            </li>
            <li>
                <asp:Label ID="lblExpireDate" runat="server" Text=""></asp:Label>
                <asp:DropDownList ID="ddlExpireMonth" runat="server" AppendDataBoundItems="true">
                </asp:DropDownList>
                /
                <asp:DropDownList ID="ddlExpireYear" runat="server" AppendDataBoundItems="true">
                </asp:DropDownList>
            </li>
        </ul>
    </div>
    <asp:Button ID="btnSumbit" runat="server" Text=""/>
    </form>
</body>
</html>
