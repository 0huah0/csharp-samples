<%@ page language="C#" autoeventwireup="true" CodeFile="Refer.aspx.cs" inherits="Referral_Create" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <asp:Literal ID="Literal1" runat="server" Text="Andrew Harper - Refer a Friend" />
    </title>
	<link href="https://cloud.webtype.com/css/d83a4f2c-b91f-4c69-8d2d-3745c7ffa244.css" rel="stylesheet" type="text/css" />
    <link rel="Stylesheet" type="text/css" href="referral.css" />
       
   <script type="text/javascript">
        function addEmail() {
            var counter = 1;
            var lblName;
            var lblEmail;
            var lblNameMissing;
            var lblInvalidEmail;
            
            counter++;
            lblName = "Your Friend's Name: ";
            lblEmail = "Your Friend's Email Address: ";
            lblNameMissing = "Please enter the name of the person you would like to refer.";
            lblInvalidEmail = "Please enter the email of the person you would like to refer.";
            var container = document.getElementById('emails');
            var newElement = document.createElement('li');
            newElement.innerHTML = '<label class="referral-name-label" >' + lblName + ' </label><br /><input class ="create-form-text" id="referral_name_' + counter + '" name="referral_name_' + counter + '" maxLength="64"></input><label class="validator referral-name-error" id="missingNameMessage' + counter + '" name="missingNameMessage' + counter + '" style="display:none">' + lblNameMissing + '</label><br /><label class="referral-email-label" >' + lblEmail + ' </label><br /><input class ="create-form-text" id="email_' + counter + '" name="email_' + counter + '" maxLength="64"></input><label class="validator referral-name-error" id="invalidEmailMessage' + counter + '" name="invalidEmailMessage' + counter + '" style="display:none">' + lblInvalidEmail + '</label>';
            container.appendChild(newElement);
            return false;
        }

        //This function does not work for all of the fields
        function validateAllEmails(source, args) {
            var regex = new RegExp('^[0-9a-zA-Z]+@[0-9a-zA-Z]+[\.]{1}[0-9a-zA-Z]+[\.]?[0-9a-zA-Z]+$');
            var isValid;            
            var index = 1;
            var currentEmail = document.forms['form1']['email_' + index];
            var currentName = document.forms['form1']['referral_name_' + index];
            var errorMessage;
            var nullCount = 0;
            if(currentEmail.value != null && currentEmail.value != "")
            {
                nullCount++;
            }
            if(currentName.value != null && currentName.value != "")
            {
                nullCount++;
            }
            while (nullCount < 2) //at least one of the values is NOT null or empty so run the test
            {
                //Test Name exists
                errorMessage = document.getElementById('missingNameMessage' + index);
                if (currentName.value == "" || currentName.value == null) //name is null or empty 
                {
                    errorMessage.setAttribute('style', '');
                    args.IsValid = false;
                }
                else {
                    errorMessage.setAttribute('style', 'display:none');
                }
                
                //Test Email exists                
                errorMessage = document.getElementById('missingEmailMessage' + index);
                if (currentEmail.value == "" || currentEmail.value == null) //email is null or empty 
                {
                    errorMessage.setAttribute('style', '');
                    args.IsValid = false;
                }
                else {
                    errorMessage.setAttribute('style', 'display:none');
                }
                
                //Test email format
                isValid = regex.test(currentEmail.value);
                errorMessage = document.getElementById('invalidEmailMessage' + index);
                if (!isValid) //email does not pass format regex
                {
                    errorMessage.setAttribute('style', '');
                    args.IsValid = false;
                }
                else {
                    errorMessage.setAttribute('style', 'display:none');
                    args.IsValid = true;
                }
                
                index++;
                currentEmail = document.forms['form1']['email_' + index];
                currentName = document.forms['form1']['referral_name_' + index];
                
                nullCount = 0;
                if(currentEmail.value != null && currentEmail.value != "")
                {
                    nullCount++;
                }
                if(currentName.value != null && currentName.value != "")
                {
                    nullCount++;
                }
            }
        }
    </script>
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
            <div id="main-wrapper">
                <div id="main">
                    <div id="content">
                        <form id="form1" runat="server" class="form-content">
                        <img alt="Refer a Friend" src="images/ReferralLandingPage_header.jpg" class="header-image" />
                        <asp:PlaceHolder ID="lblErrorMessage" runat="server" Visible="false"></asp:PlaceHolder>
                        <div class="two-column">
                            <h2 class="header-text">
                                Share your love of travel with friends and family! Your referrals will receive a complimentary six-month Andrew Harper Premier Online membership with all benefits and privileges.
                            </h2>
                            <h2 class="header-text">
                                So invite them to join the Andrew Harper community today!
                            </h2>
                        </div>
                        <div class="one-column larger">
                            <b>To refer your friends and family:</b>
                            <ol>
                                <li>Enter your name in the form to the right</li>
                                <li>Then enter your friend's name and email address</li>
                                <li>To refer more than one person, click &ldquo;Add Another Friend&rdquo;</li>
                                <li>Your friends will receive an email invitation from you and Andrew Harper alerting
                                    them of your referral and prompting them to activate their complimentary six month
                                    membership.</li>
                                <li>Click &ldquo;Send&rdquo;</li>
                            </ol>
                            <p class="orange-bold">
                                Questions?</p>
                            <b>View our <a href="http://andrewharper.com/faq">Frequently Asked Questions</a> or</br> contact Andrew Harper Member Services.</b></br>
                            <div class="contact-box">
                                <div class="contact-field">
                                    Email</div>
                                <a href="mailto:Membership@AndrewHarper.com">Membership@AndrewHarper.com</a></br>
                                <div class="contact-field">
                                    Phone</div>
                                (866) 831-4314 USA, +1 (512) 904-7342 International </br>
                                <div class="contact-field">
                                    Hours</div>
                                Monday-Friday, 8 a.m.-6 p.m. (CST)
                            </div>
                        </div>
                        <div class="one-column">
                            <i>All fields are required.</i>
                            <ul><b>Please enter your name.</b>

                                <li>
                                    <asp:Label ID="Label1" CssClass="donor-name-label" runat="server" Text="Your Name:"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="validator donor-name-error" ID="req_donor_name"
                                        Display="Dynamic" ControlToValidate="donor_name" runat="server" EnableClientScript="true"
                                        Text="Please enter the first and last name associated with your membership."></asp:RequiredFieldValidator><br />
                                    <asp:TextBox ID="donor_name" CssClass="create-form-text" runat="server" MaxLength="128" Width="400"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:CheckBox CssClass="cc-donor-checkbox" ID="chk_cc_donor" AutoPostBack="false"
                                        runat="server" Text="Click to receive a copy of the referral email" />
                                </li>
                            </ul>
                            
                            <ul id="emails"><b>Please enter your friends' names and email addresses.</b>
                                <li>
                                    <asp:Label ID="referral_name_label" CssClass="referral-name-label" runat="server"
                                        Text="Your Friend's Name:"></asp:Label>
                                    <asp:RequiredFieldValidator ID="req_referral_name_1" ControlToValidate="referral_name_1"
                                        Display="Dynamic" runat="server" EnableClientScript="true" Text="Please enter the name of the person you would like to refer."></asp:RequiredFieldValidator><br />
                                    <asp:Label runat="server" CssClass="validator referral-name-error" ID='missingNameMessage1'
                                        Style="display: none" Text="Please enter the name of the person you would like to refer."></asp:Label>
                                    <asp:TextBox ID="referral_name_1" CssClass="create-form-text" runat="server" MaxLength="128" Width="400"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="referral_email_label" CssClass="referral-email-label" runat="server"
                                        Text="Your Friend's Email Address:"></asp:Label>
                                    <asp:Label runat="server" CssClass="validator referral-email-error" ID="invalidEmailMessage1"
                                        Style="display: none" Text="Email address is not valid."></asp:Label>
                                    <asp:RequiredFieldValidator ID="req_email_1" ControlToValidate="email_1" Display="Dynamic"
                                        runat="server" EnableClientScript="true" Text="Please enter the email of the person you would like to refer."></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="referral-email-error"
                                        Display="Dynamic" runat="server" ControlToValidate="email_1" ValidationExpression="^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
                                        Text="Email address is not valid."></asp:RegularExpressionValidator><br />
                                    
                                    <asp:TextBox CssClass="create-form-text" ID="email_1" runat="server" MaxLength="320" Width="400"></asp:TextBox>
                                </li>
                                <li>
                                    <a onclick="return addEmail()" class="text-click">+ Add Another Friend</a><br />
                                </li>                                
                                <%-- Note:
                                        Validator not preventing postback unless break inserted so trying to see if adding a control to validate prevents this
                                        Unable to get CustomValidator2 to properly validate all the controls                                
                                <li>                                
                                    <asp:CustomValidator ID="CustomValidator2" runat="server" Display="Dynamic" ClientValidationFunction="validateAllEmails" ControlToValidate="referral_name_1" OnServerValidate="ServerValidation"></asp:CustomValidator>
                                </li>
                                --%>
                            </ul>
                            <p>
                                <asp:Button ID="btn_create_submit" CssClass="submit-button" runat="server" Text=""
                                    OnClick="btnSubmit_click" />
                            </p>
                            <p>Andrew Harper maintains a comprehensive information security program that is designed to ensure the privacy and confidentiality of your Personal Information and of those you refer. <a href="http://www.andrewharper.com/privacy-policy" target="_blank">View our Privacy Policy.</a></p>
                        </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
