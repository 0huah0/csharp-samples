<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Confirmation.aspx.cs" Inherits="Referral_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <asp:Literal ID="Literal1" runat="server" Text="Andrew Harper - Confirm Referral" />
    </title>
    <link rel="Stylesheet" type="text/css" href="referral.css" />
</head>
<body>
    <div id="page-wrapper">
        <div id="page">
            <div id="header">
                <div class="section clearfix">
                    <a href="/" title="Home" rel="home" id="logo">
                        <img src="http://www.andrewharper.com/sites/default/files/andrew_harper_logo.gif"
                            alt="Home" /></a>
                    <div id="name-and-slogan">
                        <div id="site-slogan">
                        </div>
                    </div>
                    <!-- /#name-and-slogan -->
                </div>
            </div>
            <hr />
            <div id="main-wrapper">
                <div id="main">
                    <div id="content">
                        <form id="form1" class="form-content" style="text-align: center;" runat="server">
                        <asp:PlaceHolder ID="phContent" runat="server" Visible="true"></asp:PlaceHolder>                        
                        <a href="http://www.andrewharper.com">Click here</a> to continue browsing AndrewHarper.com
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
