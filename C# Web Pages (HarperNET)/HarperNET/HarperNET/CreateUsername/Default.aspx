<%@ page language="C#" CodeFile="Default.aspx.cs"  autoeventwireup="true" inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Andrew Harper - Create User Name</title>
    <link href="css/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">  
    
<div style="width:100%;text-align:center;">
    <table cellpadding="3" style="width:60%;margin:5px auto;text-align:left;">
        <tr>
            <td style="text-align:center;" colspan="2">                        
                <asp:Label ID="lblMessage" CssClass="ErrorMessage" runat="server"></asp:Label>
                <asp:ValidationSummary ID="sumErrors" runat="server" />
            </td>
        </tr> 
    </table>
    <table cellpadding="3" style="width:60%;margin:5px auto;text-align:left;">  
        <tr>
            <td colspan="2">
                <div class="formintro">
                    <h1>Create a User Name and Password</h1>
                    <p>This page will help you convert your existing Andrew Harper member number to a user name and password. 
                    You will need a valid email address and your zip code. 
                    Your member number can be found on your receipt page for online orders, printed on your membership card 
                    or on the top line of your mailing label.</p>
                </div>
            </td>
        </tr>     
        <tr>
            <td width="58%" height="25" align="right" class="FormLabel">
                <asp:RequiredFieldValidator ID="reqLastName" runat="server" ControlToValidate="txtMemberId" Display="Dynamic" Text="*" ErrorMessage="Member Number is required."></asp:RequiredFieldValidator>
                Member Number:
            </td>
            <td width="42%" height="25" class="FormInput">
              <asp:TextBox ID="txtMemberId" runat="server" CssClass="DefaultTextBox" MaxLength="40"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="58%" height="25" align="right" class="FormLabel">
                <asp:RequiredFieldValidator ID="reqZip" runat="server" ControlToValidate="txtZip" Display="Dynamic" Text="*" ErrorMessage="Zip/Postal Code is required."></asp:RequiredFieldValidator>
                Zip/Postal Code:
            </td>
            <td width="42%" height="25" class="FormInput">
                <asp:TextBox ID="txtZip" runat="server" CssClass="DefaultTextBox" MaxLength="40"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnLookUp" runat="server" Text="Lookup" CssClass="DefaultButton" OnClick="btnLookUp_Click" />
            </td>
        </tr>
    </table>    
</div>
<asp:Label ID="submittoken" runat="server" Visible="false"></asp:Label>
    </form>
</body>
</html>