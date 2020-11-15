<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Newuser.aspx.cs" Inherits="Newuser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    
    <style type="text/css">
        .search
        {
            background: url(find.png) no-repeat;
            padding-left: 18px;
            border:1px solid #ccc;
            margin-top: 0px;
        }
        .style7
        {
            height: 1px;
            width: 196px;
            font-size: small;
            font-family: Andalus;
        }
        .style8
        {
            font-family: Tahoma;
            border-left-color: #A0A0A0;
            border-right-color: #C0C0C0;
            border-top-color: #A0A0A0;
            border-bottom-color: #C0C0C0;
            padding: 1px;
            font-size: small;
        }
        .style11
        {
            font-weight: bold;
        }
    </style>

    
      <style type="text/css">
          .form-style-9{
    max-width: 900px;
    background: #FAFAFA;
    padding: 20px;
    margin:20px auto;
    box-shadow: 1px 1px 25px rgba(0, 0, 0, 0.35);
    border-radius: 10px;
 
         height:729px;
         width: 751px;
     }
.form-style-9 ul{
    padding:0;
    margin:0;
    list-style:none;
}
.form-style-9 ul li{
    display: block;
    margin-bottom: 10px;
    min-height: 35px;
         height: 37px;
         width: 81px;
         margin-right: 0px;
            text-align: left;
        }
.form-style-9 ul li  .field-style{
    box-sizing: border-box;
    -webkit-box-sizing: border-box;
    -moz-box-sizing: border-box;
    padding: 8px;
    outline: none;
    border: 1px solid #B0CFE0;
    -webkit-transition: all 0.30s ease-in-out;
    -moz-transition: all 0.30s ease-in-out;
    -ms-transition: all 0.30s ease-in-out;
    -o-transition: all 0.30s ease-in-out;

}.form-style-9 ul li  .field-style:focus{
    box-shadow: 0 0 5px #B0CFE0;
    border:1px solid #B0CFE0;
}
.form-style-9 ul li .field-split{
    width: 49%;
}
.form-style-9 ul li .field-full{
    width:70%;
}
.form-style-9 ul li input.align-left{
    float:left;
}
.form-style-9 ul li input.align-right{
    float:right;
}
.form-style-9 ul li textarea{
    width: 80%;
    height: 100px;
}
.form-style-9 ul li input[type="button"],
.form-style-9 ul li input[type="submit"] {
    -moz-box-shadow: inset 0px 1px 0px 0px #3985B1;
    -webkit-box-shadow: inset 0px 1px 0px 0px #3985B1;
    box-shadow: inset 0px 1px 0px 0px #3985B1;
    background-color: #216288;
    border: 1px solid #17445E;
    display: inline-block;
    cursor: pointer;
    color: #FFFFFF;
    padding: 8px 18px;
    text-decoration: none;
    font: 12px Arial, Helvetica, sans-serif;
            text-align: right;
        }
.form-style-9 ul li input[type="button"]:hover,
.form-style-9 ul li input[type="submit"]:hover {
    background: linear-gradient(to bottom, #2D77A2 5%, #337DA8 100%);
    background-color: #28739E;
}
</style>
<style type="text/css">
.form-style-3{
    max-width: 450px;
    font-family: "Lucida Sans Unicode", "Lucida Grande", sans-serif;
}
.form-style-3 label{
    display:block;
  <%--  margin-bottom: 10px;--%>;
        height: 34px;
        text-align: left;
    }
.form-style-3 label > span{
    float: left;
    width: 100px;
    color: DARK;
    font-weight: bold;
    font-size: 13px;
    text-shadow: 1px 1px 1px #fff;
        text-align: right;
    }
.form-style-3 fieldset{
    border-radius: 10px;
    -webkit-border-radius: 10px;
    -moz-border-radius: 10px;
    margin: 0px 0px 10px 0px;
    border: 1px solid #FFD2D2;
    padding: 20px;
    background: #FFF4F4;
    box-shadow: inset 0px 0px 15px #FFE5E5;
    -moz-box-shadow: inset 0px 0px 15px #FFE5E5;
    -webkit-box-shadow: inset 0px 0px 15px #FFE5E5;
}
.form-style-3 fieldset legend{
    color: DARK;
    border-top: 1px solid #FFD2D2;
    border-left: 1px solid #FFD2D2;
    border-right: 1px solid #FFD2D2;
    border-radius: 5px 5px 0px 0px;
    -webkit-border-radius: 5px 5px 0px 0px;
    -moz-border-radius: 5px 5px 0px 0px;
    background: #FFF4F4;
    padding: 0px 8px 3px 8px;
    box-shadow: -0px -1px 2px #F1F1F1;
    -moz-box-shadow:-0px -1px 2px #F1F1F1;
    -webkit-box-shadow:-0px -1px 2px #F1F1F1;
    font-weight: normal;
    font-size: 12px;
}
.form-style-3 textarea{
    width:250px;
    height:100px;
}
.form-style-3 input[type=text],
.form-style-3 input[type=date],
.form-style-3 input[type=datetime],
.form-style-3 input[type=number],
.form-style-3 input[type=search],
.form-style-3 input[type=time],
.form-style-3 input[type=url],
.form-style-3 input[type=email],
.form-style-3 select, 
.form-style-3 textarea{
    border-radius: 3px;
    -webkit-border-radius: 3px;
    -moz-border-radius: 3px;
    border: 1px solid #FFC2DC;
    outline: none;
    color: DARK;
    padding: 5px 8px 5px 8px;
    box-shadow: inset 1px 1px 4px #FFD5E7;
    -moz-box-shadow: inset 1px 1px 4px #FFD5E7;
    -webkit-box-shadow: inset 1px 1px 4px #FFD5E7;
    background: #FFEFF6;
    width:50%;
        margin-top: 0px;
    }
.form-style-3  input[type=submit],
.form-style-3  input[type=button]{
    background: #EB3B88;
    border: 1px solid #C94A81;
    padding: 5px 15px 5px 15px;
    color: #FFCBE2;
    box-shadow: inset -1px -1px 3px #FF62A7;
    -moz-box-shadow: inset -1px -1px 3px #FF62A7;
    -webkit-box-shadow: inset -1px -1px 3px #FF62A7;
    border-radius: 3px;
    border-radius: 3px;
    -webkit-border-radius: 3px;
    -moz-border-radius: 3px;    
    font-weight: bold;
}
.required{
    color:red;
    font-weight:normal;
}
    .style13
    {
        width: 100%;
        height: 406px;
    }
    .style14
    {
        font-size: small;
        font-weight: 700;
        text-align: right;
    }
</style>


   <style type="text/css">

    .button {
    border-style: none;
        border-color: inherit;
        border-width: medium;
        background-color: #4CAF50; /* Green */
        color: white;
        padding: 10px 26px;
        text-align: right;
        text-decoration: none;
        display: inline-block;
        font-size: 16px;
        margin: 4px 2px;
        cursor: pointer;
        font-weight: 700;
    }

        </style>

        <style type="text/css">
.body
{
    margin: 0;
    padding: 0;
    font-family: Arial;
}
.modal
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;
}
.center
{
    z-index: 1000;
    margin: 300px auto;
    padding: 10px;
    width: 130px;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center img
{
    height: 128px;
    width: 128px;
}
            .style15
            {
                font-size: small;
                text-align: right;
            }
        </style>


</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <form id="form1" >
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
    <div class="modal">
        <div class="center">
       
            <asp:Image ID="Image1" ImageUrl="waiting.gif" AlternateText="Processing" runat="server" />
        </div>
    </div>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div align="center">
    <table >
        <tr>
            <td>


               <table width="500PX" style="height: 700px; width: 850px">

    <tr>
    <td valign=top style="width: 750px">
        <div class="form-style-9" style="width: 900px">
            <div class="form-style-3">
                <table style="height: 676px; width: 884px;">
                    <tr align="LEFT" valign="top">
                        <td height="700px" width="98%" style="width: 850px">
                            <fieldset  height="700px" style="height: auto">
                                <legend>New User</legend>
                                <table class="style13" style="width: 850px">
                                    <tr>
                                        <td class="style14">
                                            <strong>First Name:</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="tb10" Height="20px" 
                                                Width="150px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" 
                                                ControlToValidate="txtFirstName" ErrorMessage="First Name can't be left blank" 
                                                SetFocusOnError="True">*
         </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="style14">
                                            <strong>Last Name:</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="tb10" Height="20px" 
                                                Width="150px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="txtLastName" ErrorMessage="Last Name can't be left blank" 
                                                SetFocusOnError="True">*
        </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style15">
                                            <strong>UserName</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtUserName" runat="server" AutoPostBack="True" 
                                                CssClass="tb10" Height="20px" ontextchanged="txtUserName_TextChanged" 
                                                Width="150px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" 
                                                ControlToValidate="txtUserName" ErrorMessage="UserName can't be left blank" 
                                                SetFocusOnError="True">*
       </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="style14">
                                            <strong>Password</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtPwd" runat="server" CssClass="tb10" 
                                                Height="20px" ontextchanged="txtUserName_TextChanged" Width="150px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPwd" runat="server" 
                                                ControlToValidate="txtPwd" ErrorMessage="Password can't be left blank" 
                                                SetFocusOnError="True">*
       </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style14">
                                            Confirm Password:</td>
                                        <td>
                                            <asp:TextBox ID="txtRePwd" runat="server" CssClass="tb10" 
                                                Height="20px" ontextchanged="txtUserName_TextChanged" Width="150px"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                                ControlToCompare="txtPwd" ControlToValidate="txtRePwd" 
                                                ErrorMessage="CompareValidator">*</asp:CompareValidator>
                                        </td>
                                        <td  class="style14">
                                           Gender</td>
                                        <td>
                                            <asp:RadioButtonList ID="rdoGender" runat="server" Height="20px" 
                                                onselectedindexchanged="rdoGender_SelectedIndexChanged1" 
                                                RepeatDirection="Horizontal" 
                                                style="text-align: left; font-size: small; font-weight: 700;" Width="100%">
                                                <asp:ListItem>Male</asp:ListItem>
                                                <asp:ListItem>Female</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="style14">
                                            Address</td>
                                        <td>
                                            <asp:TextBox ID="txtAdress" runat="server" CssClass="tb10" Height="30px" 
                                                TextMode="MultiLine" Width="150px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAddress" runat="server" 
                                                ControlToValidate="txtAdress" ErrorMessage="Address can't be left blank" 
                                                SetFocusOnError="True">*
        </asp:RequiredFieldValidator>
                                        </td>
                                        <td  class="style14">
                                            Email ID:</td>
                                        <td>
                                            <asp:TextBox ID="txtEmailID" runat="server" CssClass="tb10" Height="20px" 
                                                Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="style14">
                                            Role</td>
                                        <td>
                                            <asp:DropDownList ID="drp_role" runat="server" CssClass="tb10" Height="30px" 
                                                Width="150px">
                                                <asp:ListItem Value="-1">--------Select------</asp:ListItem>
                                                <asp:ListItem Value="9">SpecialAdmin</asp:ListItem>
                                                <asp:ListItem Value="7">Super Administrator</asp:ListItem>
                                                <asp:ListItem Value="6">Administrator</asp:ListItem>
                                                <asp:ListItem Value="5">Manager</asp:ListItem>
                                                <asp:ListItem Value="4">Accounts</asp:ListItem>
                                                <asp:ListItem Value="3">Finance</asp:ListItem>
                                                <asp:ListItem Value="2">User</asp:ListItem>
                                                <asp:ListItem Value="1">End User</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td  class="style14">
                                            Status</td>
                                        <td>
                                            <asp:DropDownList ID="drp_status" runat="server" CssClass="tb10" Height="30px" 
                                                Width="150px">
                                                <asp:ListItem Value="1">Active</asp:ListItem>
                                                <asp:ListItem Value="0">InActive</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  class="style14">
                                            PlantName</td>
                                        <td>
                                            <asp:DropDownList ID="ddl_Plantname" runat="server" CssClass="tb10" 
                                                Height="30px" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
                                                PopupPosition="Bottomright" Width="150px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style14">
                                            Description</td>
                                        <td>
                                            <asp:TextBox ID="txt_desc" runat="server" CssClass="tb10" Height="30px" 
                                                TextMode="MultiLine" Width="150px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                ControlToValidate="txt_desc" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                                                ShowMessageBox="True" ShowSummary="False" />
                                        </td>
                                        <td>
                                            <span class="icon"><i class="fa fa-search">
                                            <asp:DropDownList ID="ddl_Plantcode" runat="server" Height="1px" 
                                                Visible="False" Width="150px">
                                                <asp:ListItem Value="-1">--------Select------</asp:ListItem>
                                                 <asp:ListItem Value="9">Special Administrator</asp:ListItem>
                                                <asp:ListItem Value="7">Super Administrator</asp:ListItem>
                                                <asp:ListItem Value="6">Administrator</asp:ListItem>
                                                <asp:ListItem Value="5">Manager</asp:ListItem>
                                                <asp:ListItem Value="4">Accounts</asp:ListItem>
                                                <asp:ListItem Value="5">Finance</asp:ListItem>
                                                <asp:ListItem Value="2">User</asp:ListItem>
                                                <asp:ListItem Value="1">End User</asp:ListItem>
                                            </asp:DropDownList>
                                            </i></span>
                                        </td>
                                        <td>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                                ControlToCompare="txtPwd" ControlToValidate="txtRePwd" ErrorMessage="not match" 
                                                Operator="Equal" SetFocusOnError="True"></asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                ControlToValidate="rdoGender" ErrorMessage="Gender can't be left blank" 
                                                SetFocusOnError="True">*
         </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="4">
                                            <asp:Panel ID="Panel3" runat="server" BorderStyle="Groove" BorderWidth="1px" 
                                                Width="70%">
                                                <table class="style7">
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            <strong>Role</strong></td>
                                                        <td>
                                                            <strong>count</strong></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;</td>
                                                        <td align="left">
                                                            9.Special Admin</td>
                                                        <td>
                                                            <asp:Label ID="splad" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;</td>
                                                        <td align="left">
                                                            7.Super Admin</td>
                                                        <td>
                                                            <asp:Label ID="sp" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;</td>
                                                        <td align="left">
                                                            6.Admininistrator</td>
                                                        <td>
                                                            <asp:Label ID="admin" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;</td>
                                                        <td align="left">
                                                            5.Manager</td>
                                                        <td>
                                                            <asp:Label ID="man" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;</td>
                                                        <td align="left">
                                                            4.Accounts</td>
                                                        <td>
                                                            <asp:Label ID="acc" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;</td>
                                                        <td align="left">
                                                            3.Finance</td>
                                                        <td>
                                                            <asp:Label ID="fin" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;</td>
                                                        <td align="left">
                                                            2.User</td>
                                                        <td>
                                                            <asp:Label ID="usr" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            &nbsp;</td>
                                                        <td align="left">
                                                            1.End User</td>
                                                        <td>
                                                            <asp:Label ID="end" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr align=center>
                                        <td colspan="4">
                                            <span class="style8"><strong>Search ID</strong></span><asp:TextBox ID="search" 
                                                runat="server" AutoPostBack="True" CssClass="style11" 
                                                ontextchanged="search_TextChanged" placeholder="Search..."></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>


            </div>
            <table  width=100% >
                <tr align="center">
                    <td>
                        <asp:Button ID="Newusr" runat="server" CssClass="form93" 
                            onclick="Newusr_Click" Text="NewUser" />
                        <asp:Button ID="btnSave" runat="server" CssClass="form93" 
                            onclick="btnSave_Click" Text="Sign Up" />
                        <asp:Button ID="btnUpdate" runat="server" CssClass="form93" 
                            onclick="btnUpdate_Click" Text="Update" />
                    </td>
                </tr>
            </table>
        </div>
    
    </td>
    
    
    </tr>
    
    </table>
                
                
                
                </td>
        </tr>
        </table>
    

      <table width=100%>
      <tr align="center">
      <td>
      
                    <asp:Panel ID="Panel1" runat="server" Height="100%" Width="69%">
                        <center>
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True"  ShowFooter=false
                                AutoGenerateColumns="False" CssClass="gridcls" Font-Size="Medium" 
                                onpageindexchanging="GridView1_PageIndexChanging" 
                                onrowcancelingedit="GridView1_RowCancelingEdit" 
                                onrowdatabound="GridView1_RowDataBound" 
                                onselectedindexchanged="GridView1_SelectedIndexChanged">
                                 <EditRowStyle BackColor="#999999" />
                                                  
                                                   <HeaderStyle BackColor="#f4f4f4" Font-Bold="False" Font-Italic="False" 
                                                       Font-Names="Raavi" Font-Size="Small" ForeColor="Black" />
                                                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                   <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
                                                   <AlternatingRowStyle HorizontalAlign="Center" />
                                                  
                                <Columns>
                                    <asp:TemplateField HeaderText="SNO" ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UserId" HeaderText="UserId" 
                                        SortExpression="UserId" />
                                    <asp:BoundField DataField="UserName" HeaderText="UserName" 
                                        SortExpression="UserName" />
                                    <asp:BoundField DataField="Gender" HeaderText="Gender" 
                                        SortExpression="Gender" />
                                    <asp:BoundField DataField="Address" HeaderText="Address" 
                                        SortExpression="Address" />
                                    <asp:BoundField DataField="Role" HeaderText="Role" SortExpression="Role" />
                                    <asp:CommandField ButtonType="Button" ShowSelectButton="True" 
                                        UpdateText="Edit" />
                                </Columns>
                            </asp:GridView>
                        </center>
                       
                    </asp:Panel>
          </td>
      
      </tr>
      </table>
 <%-- <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height:10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
        </div>
</ProgressTemplate>
</asp:UpdateProgress>
       --%>
    
    <br />
    

 
 



        </div>
</ContentTemplate>


<Triggers>
    <asp:PostBackTrigger ControlID="btnSave" />
     <asp:PostBackTrigger ControlID="btnUpdate" />
    <asp:PostBackTrigger ControlID="Newusr" />
</Triggers>
</asp:UpdatePanel>
  
    </form>





</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

