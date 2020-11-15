<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginDefault.aspx.cs" Inherits="LoginDefault"
    Title="OnlineMilkTest|Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Login Form</title>
    <link rel="stylesheet" href="css/normalize.css">
    <style>
      /* NOTE: The styles were added inline because Prefixfree needs access to your styles and they must be inlined if they are on local disk! */
      @import url(http://fonts.googleapis.com/css?family=Open+Sans);
.btn { display: inline-block; *display: inline; *zoom: 1; padding: 4px 10px 4px; margin-bottom: 0; font-size: 13px; line-height: 18px; color: #333333; text-align: center;text-shadow: 0 1px 1px rgba(255, 255, 255, 0.75); vertical-align: middle; background-color: #f5f5f5; background-repeat: repeat-x;
                border: 1px solid #e6e6e6;
                -webkit-border-radius: 4px;
                -moz-border-radius: 4px;
                border-radius: 4px;
                -webkit-box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.2), 0 1px 2px rgba(0, 0, 0, 0.05);
                -moz-box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.2), 0 1px 2px rgba(0, 0, 0, 0.05);
                box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.2), 0 1px 2px rgba(0, 0, 0, 0.05);
                cursor: pointer;
                margin-left: .3em;
                background-image: linear-gradient(top, #ffffff, #e6e6e6);
            }
.btn:hover, .btn:active, .btn.active, .btn.disabled, .btn[disabled] { background-color: #e6e6e6; }
.btn-large { padding: 9px 14px; font-size: 15px; line-height: normal; -webkit-border-radius: 5px; -moz-border-radius: 5px; border-radius: 5px; }
.btn:hover { color: #333333; text-decoration: none; background-color: #e6e6e6; background-position: 0 -15px; -webkit-transition: background-position 0.1s linear; -moz-transition: background-position 0.1s linear; -ms-transition: background-position 0.1s linear; -o-transition: background-position 0.1s linear; transition: background-position 0.1s linear; }
.btn-primary, .btn-primary:hover { text-shadow: 0 -1px 0 rgba(0, 0, 0, 0.25); color: #ffffff; }
.btn-primary.active { color: rgba(255, 255, 255, 0.75); }
.btn-primary { background-color: #4a77d4; background-repeat: repeat-x;
                border: 1px solid #3762bc;
                text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.4);
                box-shadow: inset 0 1px 0 rgba(255, 255, 255, 0.2), 0 1px 2px rgba(0, 0, 0, 0.5);
                background-image: linear-gradient(top, #6eb6de, #4a77d4);
            }
.btn-primary:hover, .btn-primary:active, .btn-primary.active, .btn-primary.disabled, .btn-primary[disabled] { filter: none; background-color: #4a77d4; }
.btn-block { width: 97%; 
display:block;
                font-weight: 700;
            }
 
* { -webkit-box-sizing:border-box; -moz-box-sizing:border-box; -ms-box-sizing:border-box; -o-box-sizing:border-box; box-sizing:border-box; }
 
html { width: 100%; height:100%; overflow:hidden; }
 
body { 
	width: 100%;
	height:100%;
	font-family: 'Open Sans', sans-serif;
	background: #092756;
	background: -moz-radial-gradient(0% 100%, ellipse cover, rgba(104,128,138,.4) 10%,rgba(138,114,76,0) 40%),-moz-linear-gradient(top,  rgba(57,173,219,.25) 0%, rgba(42,60,87,.4) 100%), -moz-linear-gradient(-45deg,  #670d10 0%, #092756 100%);
	background: -webkit-radial-gradient(0% 100%, ellipse cover, rgba(104,128,138,.4) 10%,rgba(138,114,76,0) 40%), -webkit-linear-gradient(top,  rgba(57,173,219,.25) 0%,rgba(42,60,87,.4) 100%), -webkit-linear-gradient(-45deg,  #670d10 0%,#092756 100%);
	background: -o-radial-gradient(0% 100%, ellipse cover, rgba(104,128,138,.4) 10%,rgba(138,114,76,0) 40%), -o-linear-gradient(top,  rgba(57,173,219,.25) 0%,rgba(42,60,87,.4) 100%), -o-linear-gradient(-45deg,  #670d10 0%,#092756 100%);
	background: -ms-radial-gradient(0% 100%, ellipse cover, rgba(104,128,138,.4) 10%,rgba(138,114,76,0) 40%), -ms-linear-gradient(top,  rgba(57,173,219,.25) 0%,rgba(42,60,87,.4) 100%), -ms-linear-gradient(-45deg,  #670d10 0%,#092756 100%);
	background: -webkit-radial-gradient(0% 100%, ellipse cover, rgba(104,128,138,.4) 10%,rgba(138,114,76,0) 40%), linear-gradient(to bottom,  rgba(57,173,219,.25) 0%,rgba(42,60,87,.4) 100%), linear-gradient(135deg,  #670d10 0%,#092756 100%);
	filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#3E1D6D', endColorstr='#092756',GradientType=1 );
}
.login { 
	position: absolute;
	top: 50%;
	left: 50%;
	margin: -150px 0 0 -150px;
	width:300px;
	height:237px;
}
.login h1 { color: #fff; text-shadow: 0 0 10px rgba(0,0,0,0.3); letter-spacing:1px; text-align:center;
                font-family: "Plantagenet Cherokee";
                font-size: x-large;
            }
 
input { 
	        border-style: none;
                border-color: inherit;
                border-width: medium;
                margin-bottom: 10px; 
	            outline: none;
	            padding: 10px;
	            font-size: 13px;
	color: #000000;
	            text-shadow: 1px 1px 1px rgba(0,0,0,0.3);
	            border-radius: 4px;
	            box-shadow: inset 0 -5px 45px rgba(100,100,100,0.2), 0 1px 1px rgba(255,255,255,0.2);
	            -webkit-transition: box-shadow .5s ease;
	            -moz-transition: box-shadow .5s ease;
	            -o-transition: box-shadow .5s ease;
	            -ms-transition: box-shadow .5s ease;
	            transition: box-shadow .5s ease;
}
input:focus { box-shadow: inset 0 -5px 45px rgba(100,100,100,0.4), 0 1px 1px rgba(255,255,255,0.2); }
 
            .style1
            {
                width: 100%;
            }
 
            .style2
            {
                color: #FFFFFF;
                font-family: Tahoma;
                font-size: large;
            }
 
            .style3
            {
                font-family: "Traditional Arabic";
                font-size: x-large;
                border-left-color: #A0A0A0;
                border-right-color: #C0C0C0;
                border-top-color: #A0A0A0;
                border-bottom-color: #C0C0C0;
                padding: 1px;
            }
 
                                                                                                                                                                                                                                                                                                                                                                                                                                        .style4
                                                                                                                                                                                                                                                                                                                                                                                                                                        {
                                                                                                                                                                                                                                                                                                                                                                                                                                            height: 70px;
                                                                                                                                                                                                                                                                                                                                                                                                                                        }
 
    </style>
    <script src="js/prefixfree.min.js"></script>
</head>
<body>
    <body>
        <form runat="server">
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        <table align="center" class="style1" style="width: 294px; height: 100px" border="1px">
            <tr align="center">
                <td class="style2">
                    <span class="style3"><strong>Login</strong></span><br />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:TextBox ID="txtLoginId" runat="server" ReadOnly="false" placeholder="Username"
                        Width="280px" TabIndex="1"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            runat="server" ErrorMessage="*" ControlToValidate="txtLoginId"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr align="center">
                <td class="style4">
                    <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" TextMode="Password"
                        TabIndex="2" Width="280px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                            runat="server" ErrorMessage="*" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Submit" class="btn btn-primary btn-block btn-large"
                        OnClick="Button1_Click" TabIndex="3" />
                </td>
            </tr>
        </table>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
        </form>
    </body>
</html>
