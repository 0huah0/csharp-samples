<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ForgotPassword_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Password.css" rel="stylesheet" type="text/css" />
    <title>Andrew Harper - Forgot Password</title>
</head>
<body>
    <form id="form1" runat="server">
    
    <asp:Label ID="lblMessage" runat="server"></asp:Label>
    <asp:Panel ID="pnlForm" runat="server">
        <asp:Label ID="lblInstructions" runat="server">Forgot your username and/or password? Simply provide the email address attached to your Andrew Harper membership below, and we'll send it to this address.</asp:Label>
        <fieldset>
            <legend>Forgot Password</legend>
            <ol>   
                <li><label>Email Address:</label><asp:TextBox runat="server" ID="email" Width="200"></asp:TextBox></li>
            </ol>
        </fieldset>  
        <asp:Label ID="Label1" runat="server">If you have not set up a user name and password, please click <a href="http://www.andrewharper.com/create" target="_top">here</a>. 
        If you have any difficulty, please email <a href="mailto:Membership@AndrewHarper.com">Membership@AndrewHarper.com</a> 
        or call (866) 831-4314 from the U.S or +1 (512) 904-7342 internationally.</asp:Label>  
        <asp:Button ID="cmdUpdate" runat="server" onclick="SubmitClicked" Text="Send" />
    </asp:Panel>
    <asp:Label ID="lblConfirmation" runat="server"></asp:Label>
   
    </form>
</body>
</html>
