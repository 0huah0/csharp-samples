<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_BidConfirm"
    Codefile="BidConfirm.ascx.cs" %>
<%@ Register TagPrefix="aht" TagName="Address_Default" Src="Address_Default.ascx" %>
<p>
    <asp:Label ID="lblDirections" runat="server" CssClass="Normal"></asp:Label>
</p>
<p>
</p>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td colspan="2">
            Billing Info
            <hr />
        </td>
    </tr>
    <tr>
        <td style="width: 50%;" valign="top">
            <table cellpadding="3" cellspacing="0" width="100%">
                <!--
                <tr>
                    <td width="50%">
                        <asp:Label ID="lblDisplayName" Text="Display Name" HelpText="This is the name all users will see if you have a winning bid." Suffix=":" runat="server" />
                    </td>
                    <td width="50%">
                        <asp:TextBox ID="txtDisplayName" runat="server" MaxLength="129"></asp:TextBox>
                    </td>
                </tr>
                -->
                <tr>
                    <td style="width: 40%;">
                        <asp:Label ID="lblCCName" Text="Name" runat="server" />
                    </td>
                    <td style="width: 40%;">
                        <asp:Label ID="txtCCName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmail" Text="Email Address" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="txtEmail" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPhoneNumber" Text="Phone Number" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="txtPhoneNumber" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCreditCard" Text="Credit Card" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="txtCreditCard" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblNumber" Text="Number" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="txtNumber" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblExpDate" Text="Exp Date" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="txtMonth" runat="server"></asp:Label>
                        /
                        <asp:Label ID="txtYear" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSecNumber" Text="Security Code" runat="server" />
                    </td>
                    <td>
                        <asp:Label ID="txtSecNumber" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%;" valign="top">
            <table cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 30%;">
                        <asp:Label ID="lblAddress1" runat="server" Text="Address" />
                    </td>
                    <td  style="width: 70%;">
                        <asp:Label ID="txtAddress1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="txtAddress2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="txtAddress3" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCity" runat="server" Text="City" />
                    </td>
                    <td>
                        <asp:Label ID="txtCity" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCountry" runat="server" Text="Country" />
                    </td>
                    <td>
                        <asp:Label ID="txtCountry" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRegion" runat="server" Text="Region" />
                    </td>
                    <td>
                        <asp:Label ID="txtRegion" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPostalCode" runat="server" Text="Postal Code" />
                    </td>
                    <td>
                        <asp:Label ID="txtPostalCode" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
           Bid Summary
            <hr />
        </td>
    </tr>
    <tr>
        <td style="width: 50%;" valign="top">
            <table cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td colspan="2" style="width: 50%;">
                        <asp:Label ID="lblBidType" runat="server" CssClass="AucBidType"></asp:Label>
                    </td>
                </tr>
                <tr id="trNextBidMessage" runat="server">
                    <td colspan="2">
                        <asp:Label ID="lblNextBidMessage" runat="server" CssClass="NormalRed"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblBid" runat="server" Text="Bid" />
                    </td>
                    <td>
                        <asp:Label ID="txtBid" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblVoucher" runat="server" Text="Certificate Number" />
                    </td>
                    <td>
                        <asp:Label ID="txtVoucher" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 50%;" valign="top">
            <table cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 50%;">
                        <asp:Label ID="lblTermsText" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
