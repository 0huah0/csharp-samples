<%@ Page Language="C#"  AutoEventWireup="true"
    Inherits="_Default" Title="Andrew Harper Auctions" 
    Codefile="Default.aspx.cs" MasterPageFile="~/Auctions/AuctionFrame.master" %>


<%@ Register Src="Section_html.ascx" TagName="Section_html" TagPrefix="uc1" %>
<%@ Register Src="AuctionThumb.ascx" TagName="AuctionThumb" TagPrefix="ah" %>

<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table width="100%" cellpadding="5" cellspacing="0">
   <tr>
        <td width="60%" align="left" valign="top">
         <em> Auctions close weekly on Tuesdays at 4:00 p.m. CST.</em></span>
        </td>
        <td width="40%" align="right" valign="top">
<asp:DropDownList ID="ddlView" runat="server" CssClass="DefaultDropDown" AutoPostBack="true" OnSelectedIndexChanged="ddlView_SelectedIndexChanged">
                <asp:ListItem Text="Upcoming Auctions" Value="UPCOMING"></asp:ListItem>
                <asp:ListItem Text="Current Auctions" Value="OPEN" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Past Auctions" Value="WON"></asp:ListItem>
     </asp:DropDownList>&nbsp;&nbsp;       <br /> <br />  </td>        </tr>
   <tr>
     <td colspan="2" align="left" valign="top"><asp:DataList ID="lstThumbs" runat="server" RepeatDirection="Horizontal" Width="100%" RepeatColumns="3" OnItemCreated="lstThumbs_ItemCreated" DataKeyField="aucID">
    <ItemTemplate>
        <ah:AuctionThumb ID="thmThumb" runat="server" />
    </ItemTemplate>
    <ItemStyle Width="33%" Height="160"/>
    
</asp:DataList></td>
   </tr>
</table>


<div style="padding-left:20px; padding-right:20px; padding-bottom:15px;" align="right">
    <br />
    <br />
   <strong> <span class="aucterms"><a href="<%= terms_url %>" target="_blank">&raquo;View the Auctions Terms & Conditions Here</a></span></strong></div>
</asp:Content>

