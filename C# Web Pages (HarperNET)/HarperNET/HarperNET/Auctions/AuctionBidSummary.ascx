<%@ Control Language="C#" AutoEventWireup="true" 
Inherits="Controls_AuctionBidSummary" Codefile="AuctionBidSummary.ascx.cs" %>

<table width="100%" cellpadding="0" cellspacing="0">
    <tr id="trTitleRow" runat="server">
        <td style="height: 22px;">
          
                <asp:Label ID="lblTitle" runat="server" CssClass="AucSubTitle" Text="Auctions"></asp:Label>
            
        </td>
    </tr>
    <tr id="trSep" runat="server">
        <td>
            <hr />
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td>
            <span class="auctiondet">
                <asp:Label ID="lblAuctionNum" runat="server" CssClass="auctiondet"></asp:Label>
          
        </td>
        <td align="right">
            <asp:Button ID="btnPlaceBid1" runat="server" Text="Place Bid >>" OnClick="btnPlaceBid1_Click" CssClass="DefaultButton" Width="100px" />
        </td>
    </tr>
    <tr>
        <td style="width: 40%" valign="top">
            <table cellpadding="3" cellspacing="0" width="100%">
                <tr id="trMoney" runat="server">
                    <td width="40%" class="auctiondetb">
                        <asp:Label ID="lblMoneyLabel" Text="High Bid" runat="server" />
                    </td>
                    <td width="60%" class="auctiondet">
                        <asp:Label ID="lblMoney" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trTopBidder" runat="server">
                    <td class="auctiondetb">
                        <asp:Label ID="lblTopBidderLabel" Text="High Bidder" runat="server" />
                    </td>
                    <td class="auctiondet">
                        <asp:Label ID="lblTopBidder" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auctiondetb" width="50%">
                        <asp:Label ID="lblQuantityLabel" Text="Packages" runat="server" />
                    </td>
                    <td class="auctiondet" width="50%">
                        <asp:Label ID="lblQuantity" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auctiondetb">
                        <asp:Label ID="lblRetailLabel" Text="Retail Value" runat="server" />
                    </td>
                    <td class="auctiondet">
                        <asp:Label ID="lblRetail" runat="server"></asp:Label>*
                    </td>
                </tr>
                <tr>
                    <td class="auctiondetb">
                        <asp:Label ID="lblStartingBidLabel" Text="Starting Bid" runat="server" />
                    </td>
                    <td class="auctiondet">
                        <asp:Label ID="lblStartingBid" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
        <td style="width: 60%" valign="top">
            <table cellpadding="3" cellspacing="0" width="100%">
                <tr>
                    <td class="auctiondetb">
                        <asp:Label ID="lblStatusLabel" Text="Status" runat="server" />
                    </td>
                    <td  class="auctiondet">
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trTimeLeft" runat="server">
                    <td class= "auctiondetb">
                        <asp:Label ID="lblTimeLeftLabel" Text="Time Remaining" runat="server" />
                    </td>
                    <td class= "auctiondet">
                        <asp:Label ID="lblTimeLeft" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trDateOpen" runat="server" visible="false">
                    <td class="auctiondetb">
                        <asp:Label ID="lblDateOpensLabel" Text="Date Opens" runat="server" />
                    </td>
                    <td class="auctiondet">
                        <asp:Label ID="lblOpenDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auctiondetb">
                        <asp:Label ID="lblDateClosesLabel" Text="Date Closes" runat="server" />
                    </td>
                    <td class="auctiondet">
                        <asp:Label ID="lblCloseDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr id="trLocalTime" runat="server">
                    <td class="auctiondetb">
                        <asp:Label ID="lblLocalTimeLabel" Text="Harper Official Time" runat="server" />
                    </td>
                    <td class="auctiondet">
                        <asp:Label ID="lblLocalTime" runat="server"></asp:Label>
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
        <td colspan="2" class="auctiondet">
        * Retail value is based on a property's standard room or package rates at the time we first publish the offer. 
           <!-- <b>Special Notes:</b> <asp:Label ID="lblNotes" runat="server"></asp:Label>-->
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
</table>