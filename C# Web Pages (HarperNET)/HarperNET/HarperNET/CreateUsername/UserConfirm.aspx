<%@ page language="C#" CodeFile="UserConfirm.aspx.cs" autoeventwireup="true" inherits="UserConfirm" %>

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
                <asp:Label ID="lblMessage" CssClass="ErrorMessage" style="text-align:center;" runat="server"></asp:Label>
                <asp:ValidationSummary ID="sumErrors" runat="server" />
            </td>
        </tr>
    </table>
    <table cellpadding="3" style="width:50%;margin:5px auto;text-align:left;" runat="server" id="memberdata">
        <tr>
            <td colspan="2">
                <div>
                    <h1>Confirm Your Information</h1>
                    <p>The following is the information that we have found for your membership.</p>
                </div>
            </td>            
        </tr>
        <tr>
            <td width="31%" class="FormLabel">
                Member Number:
            </td>
            <td width="69%">
                <asp:Label ID="lblPassport" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="FormLabel">
                First Name:
            </td>
            <td>
                <asp:Label ID="lblFirstName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="FormLabel">
                Last Name:
            </td>
            <td>
                <asp:Label ID="lblLastName" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="FormLabel">
                Address:
            </td>
            <td>
                <asp:Label ID="lblAddress1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="FormLabel">
            </td>
            <td>
                <asp:Label ID="lblAddress2" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="FormLabel">
                City:
            </td>
            <td>
                <asp:Label ID="lblCity" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="FormLabel">
                Postal Code / Zip Code:
            </td>
            <td>
                <asp:Label ID="lblZip" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;
                
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center" valign="top" >
                <p><b>Is this your information?</b></p>
              <div>  <asp:Button ID="CorrectInformation" 
                            runat="server" 
                            CssClass="DefaultButton" 
                            Text="Yes" 
                            OnClick="CorrectInformation_Click" /> </div>
            </td>
        </tr>        
        <tr>
            <td style="text-align:center;" colspan="2">
                <p>If this is not your information, please contact us directly at <a href=\"mailto:membership@andrewharper.com\">membership@andrewharper.com</a>,<br /> (866) 831-4314 or (630) 734-4610.</p>
            </td>
        </tr> 
    </table>
</div>
    </form>
</body>
</html>
