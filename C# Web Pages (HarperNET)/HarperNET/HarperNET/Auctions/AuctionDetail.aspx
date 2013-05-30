<%@ Page Language="C#"  AutoEventWireup="true"
    Inherits="App_MarketPlace_AuctionDetail"
    Title="Andrew Harper Auction Detail" 
    Codefile="AuctionDetail.aspx.cs" 
    MasterPageFile="~/Auctions/AuctionFrame.master" %>

<%@ Register Src="AuctionBidSummary.ascx" TagName="AuctionBidSummary" TagPrefix="uc1" %>
<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" cellpadding="10" cellspacing="0">
        <tr>
            <td valign="top" style="width: 45%">
             <h2>
                    <asp:Label ID="lblTitle" runat="server" Text="Harper Auctions"></asp:Label></h2>
                <span class="auction_location">
                    <asp:Label ID="lblSubTitle" runat="server" Text="All auctions close at 5:00 PM EST."></asp:Label></span>
            </td>
            <td valign="top" align="right" style="width: 45%">
<asp:Button ID="btnPlaceBid1" runat="server" Text="Place Bid »" OnClick="btnPlaceBid1_Click" CssClass="DefaultButton" Width="100px" />
                <br />
                <asp:LinkButton ID="btnReturn1" Text="« Return to Harper Auctions" runat="server"
                    OnClick="btnReturn1_Click"></asp:LinkButton>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder ID="plaDetails" runat="server"></asp:PlaceHolder>
    <table width="100%" cellpadding="10" cellspacing="0">
        <tr>
            <td align="right">
                <asp:LinkButton ID="btnReturn2" Text="« Return to Harper Auctions" runat="server"
                    OnClick="btnReturn2_Click"></asp:LinkButton>
            </td>
        </tr>
    </table>
</asp:Content>
