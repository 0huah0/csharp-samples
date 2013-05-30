<%@ Control Language="C#" AutoEventWireup="true" 
Inherits="Controls_AuctionPlaceBid" Codefile="AuctionPlaceBid.ascx.cs" %>
<%@ Register Src="BillingInfo.ascx" TagName="BillingInfo" TagPrefix="aht" %>
<%@ Register Src="BidForm.ascx" TagName="BidForm" TagPrefix="aht" %>
<%@ Register Src="BidConfirm.ascx" TagName="BidConfirm" TagPrefix="aht" %>



<table cellpadding="0" cellspacing="0" width="100%" id="tblMainBid" runat="server">
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td width="150" id="tdBidTab" runat="server" class ="AUC_TabActive">
                        <asp:LinkButton ID="btnEnterBid" runat="server" Text="Enter Bid" OnClick="btnEnterBid_Click"></asp:LinkButton>
                    </td>
                    <td width="180" id="tdBillingTab" runat="server" class="AUC_TabInactive">
                        <asp:LinkButton ID="btnBillingInfo" runat="server" Text="Modify Billing Info" OnClick="btnBillingInfo_Click" CausesValidation="false"></asp:LinkButton>
                    </td>
                    <td class="AUC_TabSep" width="400">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trBidForm" runat="server">
        <td valign="top" class="AucTabPage">
            <aht:BidForm ID="bfrBidForm" runat="server" />
            <p></p>
            <asp:Button ID="btnBidNext" runat="server" Text="Next >>" CssClass="DefaultButton" OnClick="btnBidNext_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="DefaultButton" OnClick="btnCancel_Click" CausesValidation="false" />
        </td>
    </tr>
    <tr id="trBillingInfo" runat="server">
        <td valign="top" class="AucTabPage">
            <aht:BillingInfo ID="binBillingInfo" runat="server" />
            <p></p>
            <asp:Button ID="btnSaveBilling" runat="server" Text="Save" CssClass="DefaultButton" OnClick="btnSaveBilling_Click" />
        </td>
    </tr>
</table>

<table cellpadding="0" cellspacing="0" width="100%" id="tblConfirm" runat="server" visible="false">
    <tr>
        <td>
            <asp:Label ID="lblBidError" CssClass="AUC_RedBoldText" runat="server" Text="" Visible="false"></asp:Label>
            <br />
            <br />
            <br />
            <asp:Button ID="btnBack1" runat="server" Text="<< Back" CssClass="DefaultButton" OnClick="btnBack1_Click" />
            <asp:Button ID="btnFinish1" runat="server" Text="Finish" CssClass="DefaultButton" OnClick="btnFinish1_Click" />
            <span class="AUC_RedBoldText"><-- You must click finish to complete your bid.</span>
            <span class="NormalRed">
            <br />
            <br />
            You are agreeing to a binding contract.  As stated in our Terms & Conditions, by submitting your final bid you are committed to purchase the package if you are the winning bidder. Once your bid is submitted, your bid is final.
            <br />
            <br />
            </span>
            <aht:BidConfirm id="bcnBidConfirm" runat="server"></aht:BidConfirm>
            <p></p>
            <asp:Button ID="btnBack2" runat="server" Text="<< Back" CssClass="DefaultButton" OnClick="btnBack2_Click" />
            <asp:Button ID="btnFinish2" runat="server" Text="Finish" CssClass="DefaultButton" OnClick="btnFinish2_Click" />
            &nbsp;<span class="AUC_RedBoldText"><-- You must click finish to complete your bid.</span>
            <span class="NormalRed">
            <br />
            <br />
            You are agreeing to a binding contract.  As stated in our <a href="<%= terms_url %>" target="_blank">Terms &amp; Conditions</a>, by submitting your final bid you are committed to purchase the package if you are the winning bidder. Once your bid is submitted, your bid is final.
            <br />
            <br />
            </span>
        </td>
    </tr>
</table>

<table cellpadding="0" cellspacing="0" width="100%" id="tblSummary" runat="server" visible="false">
    <tr>
        <td>
            <asp:Button ID="btnReturn1" runat="server" Text="Return" CssClass="DefaultButton" OnClick="btnReturn1_Click" />
            <p class="Normal">
            <span class="AucSuccessMessage">
            <b>
            Thank you for your participation in the Andrew Harper eAuction program!<br />
            When the auction closes, we will send you an automatic congratulations email if you are a top bidder.                
            </b>
            <p>
                <b>Please note:</b> We will send all email correspondence to <b><%= binBillingInfo.Email %></b>.
                <br />
                If this email address is incorrect, please contact our Travel Office at (800) 375-4685.
            </p>
            </span>
            </p>
            <p class="Normal">
            <span class="AucSuccessMessage">
            
            </span>
            </p>
            <aht:BidConfirm id="bcnBidSummary" runat="server"></aht:BidConfirm>
            <p></p>
            <asp:Button ID="btnReturn2" runat="server" Text="Return" CssClass="DefaultButton" OnClick="btnReturn2_Click" />
        </td>
    </tr>
</table>


<p></p>
