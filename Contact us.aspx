<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Contact us.aspx.cs" Inherits="Contact_us" Title="OnlineMilkTest|Contact Us" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel="Stylesheet" type="text/css" href="style/Home.css" media="screen"/>
<style type="text/css">
    .style1
    {
        font-family: Verdana,Arial,Helvetica,sans-serif;
        font-size: 9pt;
        font-weight: normal;
        font-style: normal;
        text-decoration: none;
        word-spacing: normal;
        letter-spacing: normal;
        text-transform: none;
        text-decoration: none;
        BACKGROUND: none;
        color: black;
        width: 12%;
        height: 40px;
    }
    #table3
    {
        margin-left: 0px;
        margin-top: 7px;
    }
    .style2
    {
        color: #65A93E;
        font-family: "Lucida Sans", sans-serif;
        font-weight: bold;
        font-style: normal;
        font-size: 12pt;
        margin-left: 20px;
    }
    .style3
    {
        color: #5194E2;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <br />
 <div id="main">
 <p><font class="style2">Contact Us</font></p>
 <div class="line"></div>
 <font size="2" class="style3">
 <dl class="fontt">
            <dd style="font-weight: bold">ONEZERO Solution</dd><br />
            <dd>No.33, II Floor, Anna Salai (Muthu Mariamman Koil Street cutting),</dd><br />
            <dd>Puducherry - 605001.</dd><br />
            <dd>INDIA</dd></dl>
            
            <dl class="fontt">
            <dd>Off Tel: +91 - 413 - 4201510</dd><br />
            <dd>E-mail: contact@10solution.com / one0solution@gmail.com</dd>
 </dl>
 
 </font>
 <br />
 <br />
 
 
 
 <div class="legcontact">

 
 <fieldset class="fontt">
<legend>Feedback / Query</legend> 

<table width="100%" id="table3"  >
 <tr>
       
       <td class="style1">
           Company Name:</td>
       
       <td class="style1">

<asp:TextBox ID="TxtCmp" runat="server" TabIndex="1" Width="150px"/>


</td>
</tr>
       <tr>
       
       <td class="style1">Name<em style="color: #FF0000">*</em>:</td>
       
       <td class="style1">

<asp:TextBox ID="txtName" runat="server" TabIndex="2" Width="150px"/>
<asp:RequiredFieldValidator ID="RV1" runat="server" 
                            ControlToValidate="txtName" 
                            ErrorMessage="Please Enter Your Name" 
                            SetFocusOnError="True">*
</asp:RequiredFieldValidator>
</td></tr>
<tr>
       
       <td class="style1">Phone/Mobile No<em style="color: #FF0000">*</em>:</td>
       
       <td class="style1">

<asp:TextBox ID="txtPh" runat="server" TabIndex="3" Width="150px"/>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtMail" 
                            ErrorMessage="Your Phone Number" 
                            SetFocusOnError="True">*
</asp:RequiredFieldValidator><br />
</td></tr>
<tr>
       
       <td class="style1">Email<em style="color: #FF0000">*</em>:</td>
       
       <td class="style1">

<asp:TextBox ID="txtMail" runat="server" TabIndex="4" Width="200px" />
<asp:RequiredFieldValidator ID="RV2" runat="server" 
                            ControlToValidate="txtMail" 
                            ErrorMessage="Your Email Address" 
                            SetFocusOnError="True">*
</asp:RequiredFieldValidator><br />
</td></tr>
<tr>
       
       <td class="style1">Subject<em style="color: #FF0000">*</em>:</td>
       
       <td class="style1">
           <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
               TabIndex="5">
           <asp:ListItem>Choose your Subject</asp:ListItem>
           <asp:ListItem>Sales</asp:ListItem>
           <asp:ListItem>Support</asp:ListItem>
           <asp:ListItem>Queries</asp:ListItem>
           </asp:DropDownList>

<asp:RequiredFieldValidator ID="RV3" runat="server" 
                            ControlToValidate="DropDownList1" 
                            ErrorMessage="Reason to contact us" 
                            SetFocusOnError="True">*
</asp:RequiredFieldValidator><br />
</td></tr>
<tr>
       
       <td class="altd1">Feedback / Query:</td>
       
       <td class="altd1">


<asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" TabIndex="6" Height="50px" Width="200px"/>
<asp:RequiredFieldValidator ID="RV4" runat="server" 
                            ControlToValidate="txtMessage" 
                            ErrorMessage="Please write your feedback" 
                            SetFocusOnError="True">*
</asp:RequiredFieldValidator><br />
</td>
</tr>
<tr>   
       <td class="altd1">
       
</td>
         <td class="altd1">
         <img height="30" alt="" src="Turing.aspx" width="80"/>
        </td>
        </tr>
<tr>
     <td class="altd1">
       
</td><td class="altd1">
 <asp:TextBox ID="txtTuring" runat="server" TabIndex="7" Width="150px"></asp:TextBox><br /><br />
        
        
        <asp:Label  runat="server" ID="capthast"  
             Text="Enter the above code" Width="300px"></asp:Label></td>
</tr>  
       <tr>
       <td class="altd1"></td>
       <td class="altd1" align="right">
<asp:Button ID="btnSubmit" runat="server" BackColor="#6F696F" ForeColor="White"  Text="Submit" onclick="btnSubmit_Click" TabIndex="7"/>
          

<asp:ValidationSummary ID="ValidationSummary1" 
                       runat="server"/>
                       </td>
                       </tr>
                       </table>
</fieldset>
        
<div class="fontt">
<asp:Label ID="Label1" runat="server" Text="" ForeColor="Green" />
<em style="color: #FF0000">*</em> Required fields</div>
</div>

<uc1:uscMsgBox ID="uscMsgBox1" runat="server" />


<br />
<br />
</div>
</asp:Content>


