<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_BillingInfo" Codefile="BillingInfo.ascx.cs" %>
<%@ Register TagPrefix="aht" TagName="Address_Default" 
Src="Address_Default.ascx" %>


<table cellpadding="0" cellspacing="0" width="100%" class ="AUC_Border" >
<tr><td>
<asp:Label ID="lblDirections" runat="server" CssClass="Normal"></asp:Label>
<asp:Label ID="lblErrorMessage" runat="server" Text="" CssClass="NormalRed" Visible=false></asp:Label>
<br />
</td></tr>
<tr>
<td>
<table cellpadding="0" cellspacing="0" width="100%" >
    <tr>
        <td style="width: 50%;" valign="top">
            <table cellpadding="3" cellspacing="0" width="100%">
            
                <!--
                <tr>
                    <td width="50%" class="SubHead">
                        <asp:Label ID="lblDisplayName" Text="Display Name" HelpText="This is the name all users will see if you have a winning bid." Suffix=":" runat="server" />
                    </td>
                    <td width="50%" class="Normal">
                        <asp:TextBox ID="txtDisplayName" runat="server" MaxLength="129"></asp:TextBox>
                    </td>
                </tr>
                -->
                <tr>
                    <td  style="width: 40%;" class="SubHead">
                    
                        <asp:Label ID="lblCCName" Text="Name" runat="server" />
                    </td>
                    <td  style="width: 60%;" class="Normal">
                        <asp:TextBox ID="txtCCName" runat="server" MaxLength="64" CssClass="NormalTextBox"></asp:TextBox>
            			<asp:RequiredFieldValidator id="reqCCName" runat="server" CssClass="NormalRed" ControlToValidate="txtCCName" ErrorMessage="<br>Name is required." Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="SubHead">
                        <asp:Label ID="lblEmail" Text="Email Address" runat="server" />
                    </td>
                    <td class="Normal">
                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="32" CssClass="NormalTextBox"></asp:TextBox>
            			<asp:RequiredFieldValidator id="reqEmail" runat="server" CssClass="NormalRed" ControlToValidate="txtEmail" ErrorMessage="<br>Email is required." Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="SubHead">
                        <asp:Label ID="lblPhoneNumber" Text="Phone Number" runat="server" />
                    </td>
                    <td class="Normal">
                        <asp:TextBox ID="txtPhoneNumber" runat="server" MaxLength="32" CssClass="NormalTextBox"></asp:TextBox>
            			<asp:RequiredFieldValidator id="reqPhoneNumber" runat="server" CssClass="NormalRed" ControlToValidate="txtPhoneNumber" ErrorMessage="<br>Phone Number is required." Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="SubHead">
                        <asp:Label ID="lblCreditCard" Text="Credit Card" runat="server" />
                    </td>
                    <td class="Normal">
                        <asp:DropDownList ID="ddlCreditCard" runat="server" CssClass="NormalTextBox">
                            <asp:ListItem Text="Mastercard">Mastercard</asp:ListItem>
                            <asp:ListItem Text="Visa">Visa</asp:ListItem>
                            <asp:ListItem Text="American Express">American Express</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="SubHead">
                        <asp:Label ID="lblNumber" Text="Number" runat="server" />
                    </td>
                    <td class="Normal">
                        <asp:TextBox ID="txtNumber" runat="server" MaxLength="16" CssClass="NormalTextBox"></asp:TextBox>
            			<asp:RequiredFieldValidator id="reqNumber" runat="server" CssClass="NormalRed" ControlToValidate="txtNumber" ErrorMessage="<br>Credit Card is required." Display="Dynamic"></asp:RequiredFieldValidator>
            			<%--<asp:RegularExpressionValidator ID="reqNumber2" runat="server" CssClass="NormalRed" ControlToValidate="txtNumber" ErrorMessage="<br>Credit Card is invalid." Display="Dynamic" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td class="SubHead">
                        <asp:Label ID="lblExpDate" Text="Exp Date" runat="server" />
                    </td>
                    <td class="Normal">
                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="NormalTextBox">
                            <asp:ListItem Value="" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1">01</asp:ListItem>
                            <asp:ListItem Value="2">02</asp:ListItem>
                            <asp:ListItem Value="3">03</asp:ListItem>
                            <asp:ListItem Value="4">04</asp:ListItem>
                            <asp:ListItem Value="5">05</asp:ListItem>
                            <asp:ListItem Value="6">06</asp:ListItem>
                            <asp:ListItem Value="7">07</asp:ListItem>
                            <asp:ListItem Value="8">08</asp:ListItem>
                            <asp:ListItem Value="9">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="NormalTextBox">
                        </asp:DropDownList>
            			<asp:RequiredFieldValidator id="reqMonth" runat="server" CssClass="NormalRed" ControlToValidate="ddlMonth" ErrorMessage="<br>Credit Card Month is required." Display="Dynamic"></asp:RequiredFieldValidator>
            			<asp:RequiredFieldValidator id="reqYear" runat="server" CssClass="NormalRed" ControlToValidate="ddlYear" ErrorMessage="<br>Credit Card Year is required." Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="SubHead">
                        <asp:Label ID="lblSecNumber" Text="Security Code" HelpText="Enter the security code on your credit card." Suffix=":" runat="server" />
                    </td>
                    <td class="Normal">
                        <asp:TextBox ID="txtSecNumber" runat="server" MaxLength="8" CssClass="NormalTextBox"></asp:TextBox>
            			<asp:RequiredFieldValidator id="reqSecNumber" runat="server" CssClass="NormalRed" ControlToValidate="txtSecNumber" ErrorMessage="<br>Security Code is required." Display="Dynamic"></asp:RequiredFieldValidator>
            			<%--<asp:RegularExpressionValidator ID="reqSecNumber2" runat="server" CssClass="NormalRed" ControlToValidate="txtSecNumber" ErrorMessage="<br>Security Code is invalid." Display="Dynamic" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
            </table>
        </td>
        <td  style="width: 50%;" valign="top">
            <aht:Address_Default id="addBillingAddress" runat="server"></aht:Address_Default>
        </td>
    </tr>
</table>
</td></tr></table>