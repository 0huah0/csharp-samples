<%@ page language="C#" CodeFile="CreateUserName.aspx.cs"  autoeventwireup="true" inherits="CreateUserName" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Andrew Harper - Create User Name</title>
    <link href="css/styles.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">  
              
<div style="width:100%;text-align:center;">
    <table cellpadding="3" style="margin:5px auto;text-align:left;">
        <tr>
            <td style="text-align:left;" colspan="2">                        
                <asp:Label ID="lblMessage" CssClass="ErrorMessage" runat="server"></asp:Label>
                <asp:PlaceHolder ID="phSuccess" runat="server"></asp:PlaceHolder>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            &nbsp;</td>
        </tr>
    </table>
    <table width="60%" cellpadding="3" id="memberdata" style="margin:5px auto; text-align:left;" runat="server">
        <tr>
            <td colspan="2">
                
                    <h1>Create a User Name and Password</h1>
                    <p>Please create a user name and password for your account.  
                    Please also provide a correct email address for your account.  
                    An email address will ensure that we can contact you if you forget your password.</p>
                
            </td>            
        </tr>
        <tr>
            <td class="FormLabel">
                User Name:
            </td>
            <td>
                <asp:TextBox ID="txtUserName" runat="server" MaxLength="64" CssClass="DefaultTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqUserName" runat="server" 
                    ControlToValidate="txtUserName" Display="Dynamic" 
                    Text="<br />User Name is required">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FormLabel">
                Password:
            </td>
            <td>                
                <asp:TextBox ID="txtPassword" runat="server" MaxLength="64" CssClass="DefaultTextBox" TextMode="Password"></asp:TextBox>
                (at least 7 characters)
                <asp:RegularExpressionValidator ID="reqPassword3" 
                    runat="server" ControlToValidate="txtPassword" 
                    Display="Dynamic" Text="<br />Password must be at least 7 characters" 
                    ValidationExpression="^.{7,}">
                </asp:RegularExpressionValidator>                
                <asp:RequiredFieldValidator ID="reqPassword1" 
                    runat="server" ControlToValidate="txtPassword" 
                    Display="Dynamic" Text="<br />Password is required">
                </asp:RequiredFieldValidator>                
            </td>
        </tr>
        <tr>
            <td class="FormLabel">
                Confirm Password:
            </td>
            <td>
                <asp:TextBox ID="txtConfirmPassword" runat="server" MaxLength="64" CssClass="DefaultTextBox" TextMode="Password"></asp:TextBox>
                <asp:CompareValidator ID="comPassword" 
                    runat="server" ControlToValidate="txtConfirmPassword" 
                    ControlToCompare="txtPassword" Display="Dynamic" 
                    Text="<br />Password and Confirm Password must match">
                </asp:CompareValidator>
                
                <asp:RequiredFieldValidator ID="reqPassword2" 
                    runat="server" ControlToValidate="txtConfirmPassword" 
                    Display="Dynamic" Text="<br />Confirm Password is required">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="FormLabel">                
                Email Address:
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="64" CssClass="DefaultTextBox"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqEmail1" 
                    runat="server" ControlToValidate="txtEmail" 
                    Display="Dynamic" Text="<br />Email is required">
                </asp:RequiredFieldValidator>                
                <asp:RegularExpressionValidator ID="regEmail" 
                    runat="server" ControlToValidate="txtEmail" 
                    Display="Dynamic" Text="<br />Email format is incorrect" 
                    ValidationExpression="^.+\@.+\..+$">
                </asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="FormLabel">
                Confirm Email:
            </td>
            <td>
                <asp:TextBox ID="txtConfirmEmail" runat="server" MaxLength="64" CssClass="DefaultTextBox"></asp:TextBox>                               
                <asp:RequiredFieldValidator ID="reqConfirmEmail" runat="server" 
                    ControlToValidate="txtConfirmEmail" Display="Dynamic" 
                    Text="<br />Confirm Email is required.">
                </asp:RequiredFieldValidator>                
                <asp:RegularExpressionValidator ID="reqConfirmEmail2" 
                    runat="server" ControlToValidate="txtConfirmEmail" Display="Dynamic" 
                    Text="<br />Confirm Email format is incorrect." 
                    ValidationExpression="^.+\@.+\..+$">
                </asp:RegularExpressionValidator>
                <asp:CompareValidator ID="comEmail" runat="server" 
                    ControlToValidate="txtConfirmEmail" 
                    ControlToCompare="txtEmail" Display="Dynamic" 
                    Text="<br />Email and Confirm Email must match.">
                </asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="right">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="orange_button" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
</div>

</form>
</body>
</html>