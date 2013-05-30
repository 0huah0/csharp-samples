<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_Section_html" Codefile="Section_html.ascx.cs" %>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr id="trTitleRow" runat="server">
        <td>
            <asp:Label ID="lblTitle" runat="server"></asp:Label>
        </td>
    </tr>
    <tr id="trSep" runat="server">
        <td>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <span id="spnContent" runat="server"></span>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>