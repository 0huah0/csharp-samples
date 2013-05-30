<%@ Control Language="C#" AutoEventWireup="true" Inherits="AuctionThumb" 
Codefile="AuctionThumb.ascx.cs" %>
<table width="100%">
    <tr>
        <td align="center" valign="top" cellpadding="0" cellspacing="0">
            <span class="auction_title">
                <asp:LinkButton ID="btnAucName" runat="server" CssClass="AucThumbNameLink" OnClick="btnAucName_Click" ></asp:LinkButton>
            </span><br/>            
			<span class="auction_location">
                <asp:Label ID="lblAucSubName" CssClass="AucThumbSubNameLink" runat="server"></asp:Label>
            </span>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top">
            <asp:LinkButton ID="btnThumb" runat="server" Text="Image" OnClick="btnThumb_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top">
            <span class="auction_small">
                <asp:Label ID="lblMoneyText" CssClass="AucThumbMoney" runat="server"></asp:Label>
            </span>
        </td>
    </tr>
    <tr>
         <td align="center" valign="top">
            <span class="auction_small">
                <asp:Label ID="lblStatusText" runat="server"></asp:Label>
            </span>
        </td>
    </tr>
    <tr>
         <td align="center" valign="top" id="tdUpTD" runat="server" visible="false" style="height: 21px">
            <span class="auction_small">
                Opens: <asp:Label ID="lblUpTime" runat="server"></asp:Label>
            </span>
        </td>
    </tr>
</table>