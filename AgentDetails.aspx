<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AgentDetails.aspx.cs" Inherits="AgentDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
         .ddl
        {
            border:2px solid #7d6754;
            border-radius:5px;
            padding:3px;
            -webkit-appearance: none; 
            background-image:url('Images/Arrowhead-Down-01.png');
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            text-overflow: '';/*In Firefox*/
        }
<link href="style/Menu.css" rel="stylesheet" type="text/css" id="stylesheet" />
    .textbox { 
    background: #FFF url(http://html-generator.weebly.com/files/theme/input-text-9.png) no-repeat 4px 4px; 
    border: 1px solid #999; 
    outline:0; 
    padding-left: 25px;
    height:25px; 
    width: 275px; 
  } 
   inputs:-webkit-input-placeholder {
    color: #b5b5b5;
}

inputs-moz-placeholder {
    color: #b5b5b5;
}

.inputs {
    outline: none;
    display: block;
    width: 350px;
    padding: 4px 8px;
    border: 1px dashed #DBDBDB;
    color: #3F3F3F;
    font-family: Andalus;
    font-size: medium;
    -webkit-border-radius: 2px;
    -moz-border-radius: 2px;
    border-radius: 2px;
    -webkit-transition: background 0.2s linear, box-shadow 0.6s linear;
    -moz-transition: background 0.2s linear, box-shadow 0.6s linear;
    -o-transition: background 0.2s linear, box-shadow 0.6s linear;
    transition: background 0.2s linear, box-shadow 0.6s linear;
}

.inputs:focus {
    background: #F7F7F7;
    border: dashed 1px #969696;
    -webkit-box-shadow: 2px 2px 7px #E8E8E8 inset;
    -moz-box-shadow: 2px 2px 7px #E8E8E8 inset;
    box-shadow: 2px 2px 7px #E8E8E8 inset;
}

.buttonclass
{
padding-left: 10px;
font-weight: bold;
}
.buttonclass:hover
{
color: white;
background-color:Orange;
}


.columnscss
{
width:25px;
font-weight:bold;
font-family:Verdana;
}



        
.ddlstyle 
{
color:rgb(33,33,00);
Font-Family:Book Antiqua;
font-size:12px;
vertical-align :middle;
}
        
 
        
         .ddl2
        {
            border:2px solid #7d6754;
            border-radius:5px;
            padding:3px;
            -webkit-appearance: none; 
            background-image:url('Images/Arrowhead-Down-01.png');
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            text-overflow: '';/*In Firefox*/
        }
        
        
        

        .style4
        {
            color: #333300;
            }


        
        
        
        
     
        
        
        
        
        
        
        
 
        
        
        
        
        

        .GridViewStyle1 {border:1px solid #ddd; border-collapse:collapse; font-family:Arial, sans-serif; table-layout:auto; font-size:14px; }
/*Header*/
.HeaderStyle {border:1px, solid, #ddd; background-color:#938ede; }
 
.HeaderStyle th {padding:5px 0px 5px 0px; color:#333; text-align:center; }
/*Row*/
tr.RowStyle{text-align:center; background-color:#ffffff; }
 
tr.AlternatingRowStyle {text-align:center; background-color:#7fefae;}
 
tr.RowStyle:hover {cursor:pointer; background-color:#f69542;}
 
tr.AlternatingRowStyle:hover {cursor:pointer; background-color:#f69542;}
/*Footer*/
.FooterStyle {background-color:#938ede; height:25px;}
/*Pager*/
.PagerStyle table { margin:auto;border:none;}
 
tr.PagerStyle {text-align:center; background-color:#ddd;}
 
.PagerStyle table td {border:1px; padding:5px; }
 
.PagerStyle a {border:1px solid #fff; padding:2px 5px 2px 5px; color:#333; text-decoration:none;}
 
.PagerStyle span {padding:2px 5px 2px 5px; color:#000; font-weight:bold; border:2px solid #938ede;}
        

        .style7
        {
            width: 100%;
        }
        

        .style8
        {
            height: 590px;
            width: 347px;
        }
        

        .style9
        {
            height: 269px;
        }
        

        .style10
        {
            height: 30px;
        }
        

        </style>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            height: 100%;
        }
        .modal
        {
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            background-color: black;
            z-index: 100;
            opacity: 0.8;
            filter: alpha(opacity=60);
            -moz-opacity: 0.8;
            min-height: 100%;
        }
        #divImage
        {
            display: none;
            z-index: 1000;
            position: fixed;
            top: 0;
            left: 0;
            background-color: White;
            height: 550px;
            width: 600px;
            padding: 3px;
            border: solid 1px black;
        }
    </style>
    <%-- OnClick="btnSubmit_Click" Text="Submit" />

                                       <asp:Button ID="Button1" runat="server" align="left"   Text="Submit"  OnClientClick ="javascript:validate()" />
                                      OnClick="btnSubmit_Click" Text="Submit" />--%><%-- OnClick="btnSubmit_Click" Text="Submit" />

                                       <asp:Button ID="Button1" runat="server" align="left"   Text="Submit"  OnClientClick ="javascript:validate()" />
                                      OnClick="btnSubmit_Click" Text="Submit" />--%>
    <script language="javascript" type="text/javascript">
        function validate() {
            var summary = "";
            summary += isvaliduser()
            summary += isvalidFirstname();
            summary += isvalidaddress()
            summary += isvalidAdhar()
            summary += isvalidBankName()
            summary += isvalidAccountNumber()
            summary += isvalidIfsc()
            summary += isvalidFirstname();
            summary += isvalidLocation();
            summary += isvaliduser()
            summary += isvalidBranchName()
            summary += isvalidMobileNumber()






            if (summary != "") {
                alert(summary);
                return false;
            }
            else {
                return true;
            }

        }
        function isvaliduser() {
            var uid;
            var temp = document.getElementById("<%=ddl_Plantcode.ClientID %>");
            uid = temp.value;
            if (uid == "") {
                return ("Please Select Your Plant Code" + "\n");
            }
            else {
                return "";
            }
        }
        function isvalidFirstname() {
            var uid;
            var temp = document.getElementById("<%=ddl_Routename.ClientID %>");
            uid = temp.value;
            if (uid == "") {
                return ("Please enter Routename" + "\n");
            }
            else {
                return "";
            }
        }
        function isvalidaddress() {
            var uid;
            var temp = document.getElementById("<%=txt_address.ClientID %>");
            uid = temp.value;
            if (uid == "") {
                return ("Please enter Address" + "\n");
            }
            else {
                return "";
            }
        }

        function isvalidAdhar() {
            var uid;
            var temp = document.getElementById("<%=txt_aadthar.ClientID %>");
            uid = temp.value;
            if (uid == "") {
                return ("Please enter Aathar Number" + "\n");
            }
            else {
                return "";
            }
        }

        function isvalidBankName() {
            var uid;
            var temp = document.getElementById("<%=txt_bank.ClientID %>");
            uid = temp.value;
            if (uid == "") {
                return ("Please enter Bank Name" + "\n");
            }
            else {
                return "";
            }
        }


        function isvalidAccountNumber() {
            var uid;
            var temp = document.getElementById("<%=txt_accountno.ClientID %>");
            uid = temp.value;
            if (uid == "") {
                return ("Please enter Account Number" + "\n");
            }
            else {
                return "";
            }
        }


        function isvalidIfsc() {
            var uid;
            var temp = document.getElementById("<%=txt_ifscno.ClientID %>");
            uid = temp.value;
            if (uid == "") {
                return ("Please enter Ifsc" + "\n");
            }
            else {
                return "";
            }
        }


        function isvalidBranchName() {
            var uid;
            var temp = document.getElementById("<%=txt_branchname.ClientID %>");
            uid = temp.value;
            if (uid == "") {
                return ("Please enter Branch Name" + "\n");
            }
            else {
                return "";
            }


            function isvalidMobileNumber() {
                var uid;
                var temp = document.getElementById("<%=txt_mobile.ClientID %>");
                uid = temp.value;
                if (uid == "") {
                    return ("Please enter Mobile Number" + "\n");
                }
                else {
                    return "";



                }
            }



            function isvalidBranchName() {
                var uid;
                var temp = document.getElementById("<%=txt_branchname.ClientID %>");
                uid = temp.value;
                if (uid == "") {
                    return ("Please enter BranchName" + "\n");
                }
                else {
                    return "";



                }
            }
        }

    </script>
    <script type="text/javascript">
        function LoadDiv(url) {
            var img = new Image();
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            var imgLoader = document.getElementById("imgLoader");
            imgLoader.style.display = "block";
            img.onload = function () {
                imgFull.src = img.src;
                imgFull.style.display = "block";
                imgLoader.style.display = "none";
            };
            img.src = url;
            var width = document.body.clientWidth;
            if (document.body.clientHeight > document.body.scrollHeight) {
                bcgDiv.style.height = document.body.clientHeight + "px";
            }
            else {
                bcgDiv.style.height = document.body.scrollHeight + "px";
            }
            imgDiv.style.left = (width - 650) / 2 + "px";
            imgDiv.style.top = "20px";
            bcgDiv.style.width = "100%";

            bcgDiv.style.display = "block";
            imgDiv.style.display = "block";
            return false;
        }
        function HideDiv() {
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            if (bcgDiv != null) {
                bcgDiv.style.display = "none";
                imgDiv.style.display = "none";
                imgFull.style.display = "none";
            }
        }
    </script>
    <%--      <asp:BoundField DataField="Plant_Name" HeaderText="PName" 
                                    SortExpression="Plant_Name" /> --%><%--      <asp:BoundField DataField="Plant_Name" HeaderText="PName" 
                                    SortExpression="Plant_Name" /> --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ToolkitScriptManager>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 1%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: Gray; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..."
                    ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <%--      <asp:BoundField DataField="Plant_Name" HeaderText="PName" 
                                    SortExpression="Plant_Name" /> --%>
            <center>
                <panel width="70%">
      
      <fieldset width="90%" bgcolor="#CCFFFF">
      
      
          <table width=100% style="margin-bottom: 4px; height: 1000px;">
              <tr>
                  <td class="style9" colspan="2">
                      <asp:Panel ID="Panel9" runat="server" BorderStyle="Inset">
                          <fieldset>
                              <table class="style7">
                                  <tr>
                                      <td width="20%">
                                          &nbsp;</td>
                                      <td align="center" colspan="2">
                                          <asp:Label ID="Label18" runat="server" CssClass="style4" EnableTheming="False" 
                                              Font-Size="Small" 
                                              style="font-family: Andalus; font-size: medium; font-weight: 700;" 
                                              Text="Agent Information "></asp:Label>
                                      </td>
                                      <td width="20%">
                                          <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
                                              EnableViewState="False" onclick="LinkButton1_Click" 
                                              PostBackUrl="~/AgentInformationReport.aspx" style="font-family: Andalus">Show Reports</asp:LinkButton>
                                          <br />
                                          <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                              EnableViewState="False" onclick="LinkButton1_Click" 
                                              PostBackUrl="~/AgentDetailsUpdateReport.aspx" style="font-family: Andalus">AgentDetailsUpdate</asp:LinkButton>
                                               <br />
                                               <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" 
                                              EnableViewState="False" onclick="LinkButton1_Click" 
                                              PostBackUrl="~/AgentDetailsReports.aspx" style="font-family: Andalus">Agent Profile</asp:LinkButton>

                                      </td>
                                  </tr>
                                  <tr>
                                      <td width="20%">
                                          &nbsp;</td>
                                      <td align="right">
                                          <asp:Label ID="Label43" runat="server" CssClass="style4" EnableTheming="False" 
                                              Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                              Text="Plant Name"></asp:Label>
                                      </td>
                                      <td align="left" width="35%">
                                          <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                                              class="ddl2" CssClass="tb10" Height="30px" 
                                              onselectedindexchanged="ddl_Plantname_SelectedIndexChanged1" Width="230px">
                                          </asp:DropDownList>
                                      </td>
                                      <td width="20%">
                                          <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="True" 
                                              CssClass="tb10" Font-Size="X-Small" Height="20px" 
                                              onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Visible="False" 
                                              Width="70px">
                                          </asp:DropDownList>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          <asp:Panel ID="Panel6" runat="server" Height="23px" Visible="False" 
                                              Width="43px">
                                              <asp:Image ID="ImgPreview" runat="server" BorderStyle="Inset" Height="16px" 
                                                  ImageUrl="~/1428594084_camera-128.png" Width="16px" />
                                          </asp:Panel>
                                      </td>
                                      <td align="right">
                                          <asp:Label ID="Label41" runat="server" CssClass="style4" EnableTheming="False" 
                                              Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                              Text="Route Name"></asp:Label>
                                      </td>
                                      <td align="left">
                                          <asp:DropDownList ID="ddl_Routename" runat="server" AutoPostBack="True" 
                                              class="ddl2" CssClass="tb10" Height="30px" 
                                              onselectedindexchanged="ddl_Routename_SelectedIndexChanged" Width="230px">
                                          </asp:DropDownList>
                                      </td>
                                      <td>
                                          <asp:DropDownList ID="ddl_routeid" runat="server" AutoPostBack="True" 
                                              CssClass="tb10" Font-Size="X-Small" Height="20px" 
                                              onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Visible="False" 
                                              Width="150px">
                                          </asp:DropDownList>
                                      </td>
                                  </tr>
                                  <tr>
                                      <td>
                                          &nbsp;</td>
                                      <td align="right">
                                          <asp:Label ID="Label21" runat="server" CssClass="style4" EnableTheming="False" 
                                              Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                              Text="Agent Id/Can No"></asp:Label>
                                      </td>
                                      <td align="left">
                                          <asp:DropDownList ID="ddl_Agentid" runat="server" AutoPostBack="True" 
                                              class="ddl2" CssClass="tb10" Height="30px" 
                                              onselectedindexchanged="ddl_Agentid_SelectedIndexChanged" Width="230px">
                                          </asp:DropDownList>
                                      </td>
                                      <td>
                                          &nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td>
                                          &nbsp;</td>
                                      <td align="right">
                                          <asp:Label ID="Label20" runat="server" CssClass="style4" EnableTheming="False" 
                                              Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                              Text="Agent Name"></asp:Label>
                                      </td>
                                      <td align="left">
                                          <asp:TextBox ID="txt_AgentName" runat="server" CssClass="tb10" Enabled="False" 
                                              Width="225px" Height="20px"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                              ControlToValidate="txt_AgentName" ErrorMessage="Fill" style="color: #FF0000"></asp:RequiredFieldValidator>
                                      </td>
                                      <td>
                                          &nbsp;</td>
                                  </tr>
                                  <tr>
                                      <td>
                                          &nbsp;</td>
                                      <td align="right">
                                          <asp:Label ID="Label32" runat="server" CssClass="style4" EnableTheming="False" 
                                              Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                              Text="Address"></asp:Label>
                                      </td>
                                      <td align="left">
                                          <asp:TextBox ID="txt_address" runat="server" CssClass="tb10" Height="40px" 
                                              TextMode="MultiLine" Width="225px"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                              ControlToValidate="txt_address" ErrorMessage="Fill" style="color: #FF0000"></asp:RequiredFieldValidator>
                                      </td>
                                      <td>
                                          &nbsp;</td>
                                  </tr>
                              </table>
                          </fieldset>
                          <asp:Label ID="lbl_label" runat="server" 
                              style="color: #33CC33; font-weight: 700; font-family: Andalus;" Text="Label"></asp:Label>
                      </asp:Panel>
                      </td>
              </tr>
              <tr valign="top">
                  <td class="style8">
                      <asp:Panel ID="Panel7" runat="server" BorderStyle="Inset" Height="590px" 
                          Width="572px">
                          <center style="text-align: right">
                          <fieldset style="height: 570px">
                              <table class="style8" align="center">
                                  <tr align="CENTER">
                                      <td align="right">
                                          &nbsp;</td>
                                      <td align="left">
                                          &nbsp;</td>
                                  </tr>
                                  <tr align="CENTER">
                                      <td align="right">
                                          <asp:Label ID="Label1" runat="server" CssClass="style4" EnableTheming="False" 
                                              Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                              Text="Joining Date"></asp:Label>
                                      </td>
                                      <td align="left">
                                          <asp:TextBox ID="txt_joining" runat="server" CssClass="tb10" Height="20px" 
                                              ontextchanged="txt_FromDate_TextChanged1" Width="150px"></asp:TextBox>
                                          <asp:CalendarExtender ID="txt_joining_CalendarExtender1" runat="server" 
                                              Format="dd/MM/yyyy" PopupButtonID="txt_joining" PopupPosition="TopRight" 
                                              TargetControlID="txt_joining">
                                          </asp:CalendarExtender>
                                      </td>
                                  </tr>
                                  <tr width="25%">
                                      <td align="right">
                                          <asp:Label ID="Label2" runat="server" CssClass="style4" EnableTheming="False" 
                                              Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                              Text="Aadhar NO"></asp:Label>
                                          <br />
                                          <asp:Label ID="lblmessage" runat="server" Text="Label" 
                                              style="font-size: small; font-family: Andalus"></asp:Label>
                                      </td>
                                      <td align="left">
                                          <asp:TextBox ID="txt_aadthar" runat="server" CssClass="tb10" Width="150px" 
                                              Height="20px"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                              ControlToValidate="txt_aadthar" ErrorMessage="Fill" style="color: #FF0000"></asp:RequiredFieldValidator>
                                          <asp:Image ID="Image1" runat="server" Height="30px" Width="30px" />
                                          <br />
                                          <asp:FileUpload ID="FileUpload1" runat="server" Width="150px"/>
                                          <asp:Button ID="uploadthar" runat="server" CausesValidation="False" 
                                              onclick="uploadthar_Click" Text="Upload" />
                                          <br />
                                      </td>
                                  </tr>
                                  <tr width="25%">
                                      <td align="right">
                                          <asp:Label ID="Label3" runat="server" CssClass="style4" EnableTheming="False" 
                                              Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                              Text="Ration NO"></asp:Label>
                                          <br />
                                          <asp:Label ID="lblmessage1" runat="server" Text="Label" 
                                              style="font-size: small; font-family: Andalus"></asp:Label>
                                      </td>
                                      <td align="left">
                                          <asp:TextBox ID="txt_rationno" runat="server" CssClass=" tb10" Width="150px" 
                                              Height="20px"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                              ControlToValidate="txt_rationno" ErrorMessage="Fill" style="color: #FF0000"></asp:RequiredFieldValidator>
                                          <asp:Image ID="Image2" runat="server" Height="30px" Width="30px" />
                                          <br />
                                          <asp:FileUpload ID="FileUpload2" runat="server" Width="150px" />
                                         
                                              <asp:Button ID="uploadration" runat="server" CausesValidation="False" 
                                              onclick="uploadration_Click" Text="Upload" />
                                          <br />

<br />






                                      </td>
                                  </tr>
                                  <tr>
                                      <td align="right">
                                          <asp:Label ID="Label4" runat="server" CssClass="style4" EnableTheming="False" 
                                              Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                              Text="Voter Id"></asp:Label>
                                          <br />
                                          <asp:Label ID="lblmessage2" runat="server" Text="Label" 
                                              style="font-size: small; font-family: Andalus"></asp:Label>
                                      </td>
                                      <td align="left">
                                          <asp:TextBox ID="txt_voteid" runat="server" CssClass="tb10" Width="150px" 
                                              Height="20px"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                              ControlToValidate="txt_voteid" ErrorMessage="Fill" style="color: #FF0000"></asp:RequiredFieldValidator>
                                          <asp:Image ID="Image3" runat="server" Height="30px" Width="30px" />
                                          <br />
                                          <asp:FileUpload ID="FileUpload3" runat="server" Width="150px" />
                                          <asp:Button ID="uploadvoter" runat="server" CausesValidation="False" 
                                              onclick="uploadvoter_Click" Text="Upload" />
                                          <br />
                                      </td>
                                  </tr>
                                  <tr width="25%">
                                      <td align="right">
                                          <asp:Label ID="Label5" runat="server" CssClass="style4" EnableTheming="False" 
                                              Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                              Text="PanCard No"></asp:Label>
                                          <br />
                                          <asp:Label ID="lblmessage3" runat="server" Text="Label" 
                                              style="font-size: small; font-family: serif"></asp:Label>
                                      </td>
                                      <td align="left">
                                          <asp:TextBox ID="txt_pancard" runat="server" CssClass="tb10" Width="150px" 
                                              Height="20px"></asp:TextBox>
                                          &nbsp;&nbsp;&nbsp;&nbsp;
                                          <asp:Image ID="Image4" runat="server" Height="30px" Width="30px" />
                                          <br />
                                          <asp:FileUpload ID="FileUpload4" runat="server" Width="150px" />
                                          <asp:Button ID="uploadpancard" runat="server" CausesValidation="False" 
                                              onclick="uploadpancard_Click" Text="Upload" />
                                          <br />
                                      </td>
                                  </tr>
                                  <tr width="25%">
                                      <td align="right">
                                          <asp:Label ID="Label6" runat="server" CssClass="style4" EnableTheming="False" 
                                              Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                              Text="Nominee Name"></asp:Label>
                                          <br />
                                      </td>
                                      <td align="left">
                                          <asp:TextBox ID="txt_guardian" runat="server" CssClass="tb10" Width="200px" 
                                              Height="20px"></asp:TextBox>
                                      </td>
                                  </tr>
                                  <tr width="25%">
                                      <td align="right">
                                          <asp:Label ID="Label44" runat="server" CssClass="style4" EnableTheming="False" 
                                              Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                              Text="DateOfBirth"></asp:Label>
                                      </td>
                                      <td align="left">
                                          <asp:TextBox ID="txt_dob" runat="server" CssClass="tb10" Height="20px" 
                                             Width="150px"></asp:TextBox>
                                          <asp:CalendarExtender ID="txt_dob_CalendarExtender" runat="server" 
                                              Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="TopRight" 
                                              TargetControlID="txt_dob">
                                          </asp:CalendarExtender>
                                      </td>
                                  </tr>
                                  <tr width="25%">
                                      <td align="right">
                                          <asp:Label ID="Label45" runat="server" CssClass="style4" EnableTheming="False" 
                                              Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                              Text="MarriageDate"></asp:Label>
                                      </td>
                                      <td align="left">
                                          <asp:TextBox ID="txt_wedd" runat="server" CssClass="tb10" Height="20px" 
                                              Width="150px"></asp:TextBox>
                                          <asp:CalendarExtender ID="txt_wedd_CalendarExtender" runat="server" 
                                              Format="dd/MM/yyyy" PopupButtonID="txt_wedd" PopupPosition="TopRight" 
                                              TargetControlID="txt_wedd">
                                          </asp:CalendarExtender>
                                      </td>
                                  </tr>
                                  <tr width="25%">
                                      <td align="right">
                                          <asp:Label ID="Label46" runat="server" CssClass="style4" EnableTheming="False" 
                                              Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                              Text="Mail Id"></asp:Label>
                                          <br />
                                      </td>
                                      <td align="left">
                                          <asp:TextBox ID="Mailid" runat="server" CssClass="tb10" Height="20px" 
                                              Width="200px"></asp:TextBox>
                                          <br />
                                          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                              ControlToValidate="Mailid" ErrorMessage="Not Proper MailId" 
                                              style="font-family: Andalus; color: #FF0000" 
                                              ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                      </td>
                                  </tr>
                              </table>
                          </fieldset> 
                          </center>
                      </asp:Panel>
                  </td>
                  <td class="style8">
                      <asp:Panel ID="Panel8" runat="server" BorderStyle="Inset" Height="590px" 
                          Width="500px">
                      <fieldset style="height: 570px">
                      
                      
                      
                          <table class="style8">
                              <tr align="center">
                                  <td align="CENTER">
                                      <asp:Label ID="Label7" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Bank Name"></asp:Label>
                                  </td>
                                  <td align="left">
                                      <asp:TextBox ID="txt_bank" runat="server" CssClass="tb10" Enabled="False" 
                                          ontextchanged="txt_mgrname15_TextChanged" Width="150px" Height="20px"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                          ControlToValidate="txt_bank" ErrorMessage="Fill" style="color: #FF0000"></asp:RequiredFieldValidator>
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      <asp:Label ID="Label8" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Account No"></asp:Label>
                                      <br />
                                      <asp:Label ID="lblmessage4" runat="server" Text="Label" 
                                          style="font-size: small; font-family: serif"></asp:Label>
                                  </td>
                                  <td align="left">
                                      <asp:TextBox ID="txt_accountno" runat="server" CssClass="tb10" Enabled="False" 
                                          Width="150px" Height="20px"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                                          ControlToValidate="txt_accountno" ErrorMessage="Fill" style="color: #FF0000"></asp:RequiredFieldValidator>
                                      <asp:Image ID="Image5" runat="server" Height="30px" Width="30px" />
                                      <br />
                                      <asp:FileUpload ID="FileUpload5" runat="server" Width="150px" />
                                      <asp:Button ID="uploadaccount" runat="server" CausesValidation="False" 
                                          onclick="uploadaccount_Click" Text="Upload" />
                                      <br />
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      <asp:Label ID="Label9" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Ifsc No"></asp:Label>
                                  </td>
                                  <td align="left">
                                      <asp:TextBox ID="txt_ifscno" runat="server" CssClass="tb10" Enabled="False" 
                                          Width="150px" Height="20px" ontextchanged="txt_ifscno_TextChanged"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                          ControlToValidate="txt_ifscno" ErrorMessage="Fill" style="color: #FF0000"></asp:RequiredFieldValidator>
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      <asp:Label ID="Label10" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Branch Name"></asp:Label>
                                  </td>
                                  <td align="left">
                                      <asp:TextBox ID="txt_branchname" runat="server" CssClass="tb10" Enabled="False" 
                                          Width="150px" Height="20px"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                          ControlToValidate="txt_branchname" ErrorMessage="Fill" style="color: #FF0000"></asp:RequiredFieldValidator>
                                  </td>
                              </tr>
                              <tr>
                                  <td align="right">
                                      <asp:Label ID="Label11" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Mobile"></asp:Label>
                                  </td>
                                  <td align="left">
                                      <asp:TextBox ID="txt_mobile" runat="server" CssClass="tb10" Enabled="False" 
                                          Width="150px"></asp:TextBox>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                          ControlToValidate="txt_mobile" ErrorMessage="Fill" style="color: #FF0000"></asp:RequiredFieldValidator>
                                  </td>
                              </tr>
                              <tr>
                                  <td class="style10">
                                      <asp:Label ID="Label23" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Agent Image"></asp:Label>
                                      <br />
                                      <asp:Label ID="lblmessage5" runat="server" Text="Label" 
                                          style="font-size: small; font-family: serif"></asp:Label>
                                  </td>
                                  <td align="left" class="style10">
                                      <right>
                                          <asp:TextBox ID="txt_pancard123" runat="server" CssClass="ddl2" Visible="False" 
                                              Width="150px"></asp:TextBox>
                                          <asp:Image ID="Image6" runat="server" Height="30px" style="text-align: right" 
                                              Width="30px" />
                                      </right>
                                      <br />
                                      <asp:FileUpload ID="fileUpd" runat="server" Height="28px" Width="150px" />
                                      <asp:Button ID="uploadimage" runat="server" CausesValidation="False" 
                                          onclick="uploadimage_Click" Text="Upload" />
                                      <br />
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                          ErrorMessage="Fill" style="color: #FF0000" Visible="False"></asp:RequiredFieldValidator>
                                  </td>
                              </tr>
                              <tr>
                                  <td>
                                      &nbsp;</td>
                                  <td>
                                      <asp:Button ID="btnUploadImage" runat="server" CssClass="buttonclass" 
                                          OnClick="btnUploadImage_Click" Text="Submit" Height="26px" />
                                      <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                                          OnClientClick="return validate();" Text="Submit" Visible="False" />
                                      <br />
                                  </td>
                              </tr>
                          </table>
                      
                      
                      
                      </fieldset>
                      </asp:Panel>
                  </td>
              </tr>
              </table>
      

      </fieldset>
      
          <br />
      
      </panel>
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle1"
                    HeaderStyle-BackColor="#61A6F8" AllowPaging="True" PageSize="5" Font-Size="XX-Small"
                    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCreated="GridView1_RowCreated">
                    <Columns>
                        <%--  <asp:Image ID="Image" runat="server" Width="100px" Height="120px" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "Image") %>' />--%>
                        <%--      <asp:BoundField DataField="Plant_Name" HeaderText="PName" 
                                    SortExpression="Plant_Name" /> --%>
                        <asp:BoundField DataField="Agent_Id" HeaderText="CanNO" SortExpression="Agent_Id" />
                        <asp:BoundField DataField="Agent_Name" HeaderText="Name" SortExpression="Agent_Name" />
                        <%--    <asp:BoundField DataField="Route_Name" HeaderText="RName" 
                                    SortExpression="Route_Name" /> --%>
                        <%--   <asp:BoundField DataField="JoiningDate" HeaderText="DOJ" 
                                    SortExpression="JoiningDate" />  --%>
                        <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                        <%--   <asp:BoundField DataField="AadharNo" HeaderText="AadharNo" 
                                    SortExpression="AadharNo" />  --%>
                        <asp:BoundField DataField="BankName" HeaderText="BankName" SortExpression="BankName" />
                        <asp:BoundField DataField="BankAccNo" HeaderText="AccNo" SortExpression="BankAccNo" />
                        <asp:BoundField DataField="IfscNo" HeaderText="IfscNo" SortExpression="IfscNo" />
                        <asp:BoundField DataField="BranchName" HeaderText="BranName" SortExpression="BranchName" />
                        <asp:TemplateField HeaderText="AgentImage">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("Image")%>'
                                    Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Aadharimage">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl='<%# Eval("Aadharimage")%>'
                                    Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rationimage ">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl='<%# Eval("Rationimage")%>'
                                    Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="voterimage">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl='<%# Eval("voterimage")%>'
                                    Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="pancardimage">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl='<%# Eval("pancardimage")%>'
                                    Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Accountimage">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl='<%# Eval("Accountimage")%>'
                                    Width="75px" Height="75px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#61A6F8"></HeaderStyle>
                </asp:GridView>
            </center>
            <div id="divBackground" class="modal">
            </div>
            <div id="divImage">
                <table style="height: 100%; width: 100%">
                    <tr align="center">
                        <td valign="middle" align="center">
                            <img id="imgLoader" alt="" src="images/loader.gif" />
                            <img id="imgFull" alt="" src="" style="display: none; height: 500px; width: 590px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="bottom">
                            <input id="btnClose" type="button" value="close" onclick="HideDiv()" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="uploadvoter"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="uploadration"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="uploadpancard"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="uploadaccount"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="uploadimage"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="uploadthar"></asp:PostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
