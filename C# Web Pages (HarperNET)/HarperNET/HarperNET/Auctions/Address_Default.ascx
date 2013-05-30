<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_Address_Default" Codefile="Address_Default.ascx.cs" %>
<table cellpadding="3" cellspacing="0" width="100%">
    <tr>
        <td class="FormLabel">
            <asp:RequiredFieldValidator ID="reqAddress1" runat="server" ControlToValidate="txtAddress1" Display="Dynamic" Text="*" ErrorMessage="Address1 is required."></asp:RequiredFieldValidator>
            Address 1:
        </td>
        <td class="FormInput">
            <asp:TextBox ID="txtAddress1" runat="server" CssClass="DefaultTextBox" MaxLength="40"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="FormLabel">
            &nbsp;
        </td>
        <td class="FormInput">
            <asp:TextBox ID="txtAddress2" runat="server" CssClass="DefaultTextBox" MaxLength="40"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="FormLabel">
            &nbsp;
        </td>
        <td class="FormInput">
            <asp:TextBox ID="txtAddress3" runat="server" CssClass="DefaultTextBox" MaxLength="40"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="FormLabel">
            <asp:RequiredFieldValidator ID="reqCountry" runat="server" ControlToValidate="ddlCountry" Display="Dynamic" Text="*" ErrorMessage="Country is required." InitialValue=""></asp:RequiredFieldValidator>
            Country:
        </td>
        <td class="FormInput">
            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" CssClass="DefaultDropDown"></asp:DropDownList>
        </td>
    </tr>
    <tr id="trRegion" runat="server">
        <td class="FormLabel">
            Region/Province:
        </td>
        <td class="FormInput">
            <asp:TextBox ID="txtRegion" runat="server" CssClass="DefaultTextBox" MaxLength="40"></asp:TextBox>
        </td>
    </tr>
    <tr id="trState" runat="server">
        <td class="FormLabel">
            <asp:RequiredFieldValidator ID="reqState" runat="server" ControlToValidate="ddlState" Display="Dynamic" Text="*" ErrorMessage="State is required." InitialValue=""></asp:RequiredFieldValidator>
            State:
        </td>
        <td class="FormInput">
            <asp:DropDownList ID="ddlState" runat="server" CssClass="DefaultDropDown">
                <asp:ListItem Value=""></asp:ListItem>
                <asp:ListItem value="AL">Alabama</asp:ListItem>
                <asp:ListItem value="AK">Alaska</asp:ListItem>
                <asp:ListItem value="AZ">Arizona</asp:ListItem>
                <asp:ListItem value="AR">Arkansas</asp:ListItem>
                <asp:ListItem value="CA">California</asp:ListItem>
                <asp:ListItem value="CO">Colorado</asp:ListItem>
                <asp:ListItem value="CT">Connecticut</asp:ListItem>
                <asp:ListItem value="DE">Delaware</asp:ListItem>
                <asp:ListItem value="DC">District of Columbia</asp:ListItem>
                <asp:ListItem value="FL">Florida</asp:ListItem>
                <asp:ListItem value="GA">Georgia</asp:ListItem>
                <asp:ListItem value="HI">Hawaii</asp:ListItem>
                <asp:ListItem value="ID">Idaho</asp:ListItem>
                <asp:ListItem value="IL">Illinois</asp:ListItem>
                <asp:ListItem value="IN">Indiana</asp:ListItem>
                <asp:ListItem value="IA">Iowa</asp:ListItem>
                <asp:ListItem value="KS">Kansas</asp:ListItem>
                <asp:ListItem value="KY">Kentucky</asp:ListItem>
                <asp:ListItem value="LA">Louisiana</asp:ListItem>
                <asp:ListItem value="ME">Maine</asp:ListItem>
                <asp:ListItem value="MD">Maryland</asp:ListItem>
                <asp:ListItem value="MA">Massachusetts</asp:ListItem>
                <asp:ListItem value="MI">Michigan</asp:ListItem>
                <asp:ListItem value="MN">Minnesota</asp:ListItem>
                <asp:ListItem value="MS">Mississippi</asp:ListItem>
                <asp:ListItem value="MO">Missouri</asp:ListItem>
                <asp:ListItem value="MT">Montana</asp:ListItem>
                <asp:ListItem value="NE">Nebraska</asp:ListItem>
                <asp:ListItem value="NV">Nevada</asp:ListItem>
                <asp:ListItem value="NH">New Hampshire</asp:ListItem>
                <asp:ListItem value="NJ">New Jersey</asp:ListItem>
                <asp:ListItem value="NM">New Mexico</asp:ListItem>
                <asp:ListItem value="NY">New York</asp:ListItem>
                <asp:ListItem value="NC">North Carolina</asp:ListItem>
                <asp:ListItem value="ND">North Dakota</asp:ListItem>
                <asp:ListItem value="OH">Ohio</asp:ListItem>
                <asp:ListItem value="OK">Oklahoma</asp:ListItem>
                <asp:ListItem value="OR">Oregon</asp:ListItem>
                <asp:ListItem value="PA">Pennsylvania</asp:ListItem>
                <asp:ListItem value="RI">Rhode Island</asp:ListItem>
                <asp:ListItem value="SC">South Carolina</asp:ListItem>
                <asp:ListItem value="SD">South Dakota</asp:ListItem>
                <asp:ListItem value="TN">Tennessee</asp:ListItem>
                <asp:ListItem value="TX">Texas</asp:ListItem>
                <asp:ListItem value="UT">Utah</asp:ListItem>
                <asp:ListItem value="VT">Vermont</asp:ListItem>
                <asp:ListItem value="VA">Virginia</asp:ListItem>
                <asp:ListItem value="WA">Washington</asp:ListItem>
                <asp:ListItem value="WV">West Virginia</asp:ListItem>
                <asp:ListItem value="WI">Wisconsin</asp:ListItem>
                <asp:ListItem value="WY">Wyoming</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="FormLabel">
            <asp:RequiredFieldValidator ID="reqCity" runat="server" ControlToValidate="txtCity" Display="Dynamic" Text="*" ErrorMessage="City is required."></asp:RequiredFieldValidator>
            City:
        </td>
        <td class="FormInput">
            <asp:TextBox ID="txtCity" runat="server" CssClass="DefaultTextBox" MaxLength="30"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="FormLabel">
            <asp:RequiredFieldValidator ID="reqZip" runat="server" ControlToValidate="txtPostalCode" Display="Dynamic" Text="*" ErrorMessage="Zip/Postal Code is required." InitialValue=""></asp:RequiredFieldValidator>
            Zip/Postal Code:
        </td>
        <td class="FormInput">
            <asp:TextBox ID="txtPostalCode" runat="server" CssClass="DefaultTextBox" MaxLength="50"></asp:TextBox>
        </td>
    </tr>
</table>