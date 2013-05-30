<%@ Page Language="C#"  AutoEventWireup="true"
    Inherits="App_MarketPlace_BidPage" Title="Andrew Harper Auctions"
    MasterPageFile="~/Auctions/AuctionFrame.master" Codefile="BidPage.aspx.cs" EnableViewState="true" %>

<%@ Register Src="AuctionPlaceBid.ascx" TagName="AuctionPlaceBid" TagPrefix="uc1" %>
<%@ Register Src="AuctionBidSummary.ascx" TagName="AuctionBidSummary" TagPrefix="uc2" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="95%" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top" style="width: 70%; height: 63px;">
                <h1>
                    <asp:Label ID="lblTitle" runat="server" Text="Harper Auctions"></asp:Label></h1>
                <asp:Label ID="lblSubTitle" runat="server" Text="All auctions close at x time."></asp:Label>
            </td>
            <td valign="top" align="right" style="width: 30%; height: 63px;">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:PlaceHolder ID="plaDetails" runat="server"></asp:PlaceHolder>
</asp:Content>
