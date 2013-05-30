<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_BidForm" Codefile="BidForm.ascx.cs" %>

<p style="width:100%; text-align:center; font-size:large;">
<asp:Label ID="lblErrorMessage" runat="server" CssClass ="AUC_RedBoldText"></asp:Label>
</p>
<table cellpadding="0" cellspacing="0" width="100%" class ="AUC_Border" >
    <tr>
        <td style="width: 45%" valign="top">
            <asp:RadioButtonList ID="rblBidType" runat="server" CellPadding="3" CellSpacing="0" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="rblBidType_SelectedIndexChanged">
                <asp:ListItem Value="OneTime" Selected="True">I would like to enter a one time bid.</asp:ListItem>
                <asp:ListItem Value="Auto">I would like to enter an automatic bid.</asp:ListItem>
            </asp:RadioButtonList>
            <table cellpadding="3" cellspacing="0" width="100%">
                <tr id="trNextAutoBid" runat="server">
                    <td colspan="2">
                        Your next bid will be <asp:Label ID="lblNextAutoBid" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="5%">
                        <asp:Label ID="lblBid" Text="Bid" runat="server" />
                    </td>
                    <td width="40%">
                        $<asp:TextBox ID="txtBid" runat="server" MaxLength="16" CssClass="NormalTextBox"></asp:TextBox>
            			<asp:RequiredFieldValidator id="reqBid" runat="server" CssClass="NormalRed" ControlToValidate="txtBid" ErrorMessage="<br>Bid ss Required." Display="Dynamic"></asp:RequiredFieldValidator>
            			<asp:RegularExpressionValidator ID="reqBid2" runat="server" CssClass="NormalRed" ControlToValidate="txtBid" ErrorMessage="<br>Bid is invalid." Display="Dynamic" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                    </td>                    
                </tr>                
                
            </table>
        </td>
        <td style="width: 55%" valign="top">
            <table width ="100%" cellpadding ="0" cellspacing="0">        
            <tr>
                    <td  width ="39%">
                        <asp:Label ID="Label1" Text="Gift Certificate Number" runat="server" />
                    </td>
                    <td width ="61%"class="Normal">
                        <asp:TextBox ID="txtVoucher" runat="server" MaxLength="50" CssClass="NormalTextBox"></asp:TextBox>
                    </td>
            </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <br />
            <b>Please note:</b> All bids must exceed the current high bid by at least $10. When placing your bid, please enter whole numbers only. The system will not accept currency symbols, commas, and decimal points.
            <br /><br />
        </td>
    </tr>
</table>
<p><asp:CheckBox ID="chkTerms" runat="server" Text="I agree to the <a href='<%= terms_url %>' target='_blank'>Terms & Conditions</a> for this auction." />
</p>