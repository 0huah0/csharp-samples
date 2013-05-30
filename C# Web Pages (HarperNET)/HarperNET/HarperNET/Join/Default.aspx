<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Register_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <h1>Header</h1>
            images, etc...  HTML block
        </div>
        <div>
            <h2>Select an offer</h2>
            <ul>
                <li><input type="radio" id="offerpremier" checked>Premier $350</input></li>
                <li><input type="radio" id="offerpremieronline">Premier Online $250</input></li>
                <li><input type="radio" id="offerstandard">Standard $195</input></li>
            </ul>
        </div>
        <div>
            <h2>Is this a gift?</h2>
            <ul>
                <li><input type="radio" id="giftyes" value="Yes" checked>Yes</input></li>
                <li><input type="radio" id="giftno" value="No">No</input></li>
            </ul>
        </div>
        <div>
            <h2>Gift Recipient Information</h2>
            <ul>
                <li><label>*Prefix</label><input type="text" /><label>validator msg</label></li>
                <li><label>*First Name</label><input type="text" /><label>validator msg</label></li>
                <li><label>*Last Name</label><input type="text" /><label>validator msg</label></li>
                <li><label>Suffix</label><input type="text" /><label>validator msg</label></li>
                <li><label>Business name (if applicable)</label><input type="text" /><label>validator msg</label></li>
                <li><label>*Country</label><input type="text" /><label>validator msg</label></li>
                <li><label>*Address Line 1</label><input type="text" /><label>validator msg</label></li>
                <li><label>Address Line 2</label><input type="text" /><label>validator msg</label></li>
                <li><label>*City</label><input type="text" /><label>validator msg</label></li>
                <li><label>*State</label><input type="text" /><label>validator msg</label></li>
                <li><label>*Zip</label><input type="text" /><label>validator msg</label></li>
                <li><label>Phone Number</label><input type="text" /><label>validator msg</label></li>
                <li><label>Mobile Phone Number</label><input type="text" /><label>validator msg</label></li>
                <li><label>*Email Address</label><input type="text" /><label>validator msg</label></li>
            </ul>
        </div>
        <div>
            <h2>Your Information</h2>
            <ul>
                <li><label>*Prefix</label><input type="text" /><label>validator msg</label></li>
                <li><label>*First Name</label><input type="text" /><label>validator msg</label></li>
                <li><label>*Last Name</label><input type="text" /><label>validator msg</label></li>
                <li><label>Suffix</label><input type="text" /><label>validator msg</label></li>
                <li><label>Business name (if applicable)</label><input type="text" /><label>validator msg</label></li>
                <li><label>*Country</label><input type="text" /><label>validator msg</label></li>
                <li><label>*Address Line 1</label><input type="text" /><label>validator msg</label></li>
                <li><label>Address Line 2</label><input type="text" /><label>validator msg</label></li>
                <li><label>*City</label><input type="text" /><label>validator msg</label></li>
                <li><label>*State</label><input type="text" /><label>validator msg</label></li>
                <li><label>*Zip</label><input type="text" /><label>validator msg</label></li>
                <li><label>Phone Number</label><input type="text" /><label>validator msg</label></li>
                <li><label>Mobile Phone Number</label><input type="text" /><label>validator msg</label></li>
                <li><label>*Email Address</label><input type="text" /><label>validator msg</label></li>
            </ul>
        </div>
        <div>
            <h2>Billing Information</h2>
            <input type="checkbox" id="billinginfosame" value="yes">Same as above</input>
            <ul>
                <li><label>*Prefix</label><input type="text" /><label>validator msg</label></li>
                <li><label>*First Name</label><input type="text" /><label>validator msg</label></li>
                <li><label>*Last Name</label><input type="text" /><label>validator msg</label></li>
                <li><label>Suffix</label><input type="text" /><label>validator msg</label></li>
                <li><label>Business name (if applicable)</label><input type="text" /><label>validator msg</label></li>
                <li><label>*Country</label><input type="text" /><label>validator msg</label></li>
                <li><label>*Address Line 1</label><input type="text" /><label>validator msg</label></li>
                <li><label>Address Line 2</label><input type="text" /><label>validator msg</label></li>
                <li><label>*City</label><input type="text" /><label>validator msg</label></li>
                <li><label>*State</label><input type="text" /><label>validator msg</label></li>
                <li><label>*Zip</label><input type="text" /><label>validator msg</label></li>
                <li><label>Phone Number</label><input type="text" /><label>validator msg</label></li>
                <li><label>Mobile Phone Number</label><input type="text" /><label>validator msg</label></li>
                <li><label>*Email Address</label><input type="text" /><label>validator msg</label></li>
            </ul>
        </div>
        <div>
            <h2>Method of payment</h2>
            <ul>
                <li><label>Card Type</label><label>validator msg</label></li>
                <li><input type="radio" id="amex" value="American Express">American Express</input></li>
                <li><input type="radio" id="visa" value="Visa">Visa</input></li>
                <li><input type="radio" id="master" value="Mastercard">Mastercard</input></li>
                <li><label>Card Number</label><input type="text" /><label>validator msg</label></li>
                <li><label>Exp. Month</label><input type="text" /><label>validator msg</label></li>
                <li><label>Exp. Year</label><input type="text" /><label>validator msg</label></li>
            </ul>
        </div>
    </div>
    </form>
</body>
</html>
