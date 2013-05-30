<%@ Page Language="C#"  AutoEventWireup="true" Inherits="App_MarketPlace_ViewAll"
    MasterPageFile="~/Auctions/AuctionFrame.master"
     Title="Andrew Harper Auctions" 
     Codefile="ViewAll.aspx.cs" %>


<%@ Register Src="Section_html.ascx" TagName="Section_html" TagPrefix="uc1" %>
<%@ Register Src="AuctionThumb.ascx" TagName="AuctionThumb" TagPrefix="ah" %>

<asp:Content ID="content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%" cellpadding="5" cellspacing="0">
   <tr>
        <td align="left" valign="top" style="width: 60%">
        
        Auctions close weekly on Tuesdays at 4:00 p.m. CST.<br />
        <span class="aucterms"><a href="<%= terms_url %>" target="_blank">&raquo;View the Auctions Terms & Conditions Here</a></span></td>
        <td width="40%" align="right" valign="top">
<asp:DropDownList ID="ddlView" runat="server" CssClass="DefaultDropDown" AutoPostBack="true" OnSelectedIndexChanged="ddlView_SelectedIndexChanged">
                <asp:ListItem Text="Upcoming Auctions" Value="UPCOMING"></asp:ListItem>
                <asp:ListItem Text="Current Auctions" Value="OPEN" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Past Auctions" Value="WON"></asp:ListItem>
     </asp:DropDownList>&nbsp;&nbsp;          </td>        </tr>
   <tr>
     <td colspan="2" align="left" valign="top"><asp:DataList ID="lstThumbs" runat="server" RepeatDirection="Horizontal" Width="100%" RepeatColumns="3" OnItemCreated="lstThumbs_ItemCreated" DataKeyField="aucID">
    <ItemTemplate>
        <ah:AuctionThumb ID="thmThumb" runat="server" />
    </ItemTemplate>
    <ItemStyle Width="33%" Height="160"/>
    
</asp:DataList></td>
   </tr>
</table>


</asp:Content>

