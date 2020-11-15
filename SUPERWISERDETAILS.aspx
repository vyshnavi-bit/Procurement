<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SUPERWISERDETAILS.aspx.cs" Inherits="SUPERWISERDETAILS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .style1
        {
            width: 99%;
        }
        .tb10
        {
        }
        .style34
        {
            background-image: url('style/images/form_bg.jpg');
            background-repeat: repeat-x;
            border: 0px solid #d1c7ac;
            color: #333333;
            padding: 3px;
            margin-right: 4px;
            margin-bottom: 8px;
            font-family: Andalus;
            font-size: small;
        }
        .style57
        {
            height: 26px;
            text-align: left;
        }
        .style58
        {
            font-style: normal;
        }
        .style84
        {
            width: 100%;
        }
        </style>

  <style type="text/css">
        .style1
        {
            color: #333300;
            font-family: Andalus;
            font-size: medium;
        }
        .style2
        {
            width: 100%;
        }
    </style>

      <style type="text/css">
          .form-style-9{
    max-width: 450px;
    background: #FAFAFA;
    padding: 20px;
    margin:20px auto;
    box-shadow: 1px 1px 25px rgba(0, 0, 0, 0.35);
    border-radius: 10px;
 
         height:197px;
         width: 493px;
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

        .style3
        {
            font-family: Arial;
            text-align: right;
        }
        .style85
        {
            width: 99%;
            font-family: Andalus;
            font-size: medium;
            color: #333300;
        }
        .style86
        {
            width: 99%;
            font-family: Andalus;
            font-size: medium;
            color: #333300;
            font-style: normal;
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





      <table class="style84">
          <tr>
              <td >

 <div class="form-style-9" style="height: auto">
<div class="form-style-3">
<table width=100% style="height: auto">

<tr  valign=top ALIGN=LEFT>
<td WIDTH="98%" height="20px">

<fieldset  height:"50" style="height: auto"><legend>CC SuperVisor Details</legend>


    <table class="style2">
        <tr>
            <td class="style3">
      <em>
     
                          <asp:Label ID="lbl_bankid01" runat="server" CssClass="style86" Text="Plant code"></asp:Label>
      </em>
                        </td>
            <td>
                            <strong>
                            <em>
                          <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                              onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" TabIndex="8" 
                              Width="205px" CssClass=" ">
                          </asp:DropDownList>
                          </em></strong>
                        </td>
        </tr>
        <tr>
            <td class="style3">
      <em>
     
                          <asp:Label ID="lbl_bankid1" runat="server" CssClass="style86" 
                              Text="Supervisor No"></asp:Label>
      </em>
                        </td>
            <td>
                            <strong>
                            <em>
                          <asp:TextBox ID="tid" runat="server" TabIndex="2" Enabled="False" Width="190px" 
                              CssClass=" "></asp:TextBox>
                          </em></strong>
                        </td>
        </tr>
        <tr>
            <td class="style3">
      <em>
     
                          <asp:Label ID="lbl_bankid2" runat="server" CssClass="style86" Text="Name"></asp:Label>
      </em>
                        </td>
            <td>
                            <strong>
                            <em>
                          <asp:TextBox ID="txt_name" runat="server" 
                              ForeColor="DarkGoldenrod" TabIndex="3" Width="190px" CssClass=" "></asp:TextBox>
                          </em></strong>
                        </td>
        </tr>
        <tr>
            <td class="style3">
      <em>
     
                          <asp:Label ID="lbl_bankid3" runat="server" CssClass="style86" Text="Dob"></asp:Label>
      </em>
                        </td>
            <td>
                            <strong>
                            <em>
                          <asp:TextBox ID="txt_dob" runat="server" TabIndex="4" Width="190px" 
                              CssClass=" "></asp:TextBox>
                            <asp:CalendarExtender ID="txt_dob_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="TopRight" 
                                TargetControlID="txt_dob">
                            </asp:CalendarExtender>
                          </em></strong>
                        </td>
        </tr>
        <tr>
            <td class="style3">
      <em>
     
                          <asp:Label ID="lbl_bankid4" runat="server" CssClass="style86" Text="Address"></asp:Label>
      </em>
                        </td>
            <td>
                            <strong>
                            <em>
                          <asp:TextBox ID="txt_address" runat="server" 
                              ForeColor="DarkGoldenrod" Height="50px" TabIndex="5" TextMode="MultiLine" 
                              Width="190px" CssClass=" "></asp:TextBox>
                          </em></strong>
                        </td>
        </tr>
        <tr>
            <td class="style3">
      <em>
     
                          <asp:Label ID="lbl_bankid5" runat="server" CssClass="style86" Text="Mobile"></asp:Label>
      </em>
                        </td>
            <td>
                            <strong>
                            <em>
                          <asp:TextBox ID="txt_mobile" runat="server" 
                              ForeColor="DarkGoldenrod" TabIndex="6" Width="190px" CssClass=" "></asp:TextBox>
                          </em></strong>
                        </td>
        </tr>
        <tr>
            <td class="style3">
      <em>
     
                          <asp:Label ID="lbl_bankid6" runat="server" CssClass="style86" Text="Bank Name"></asp:Label>
      </em>
                        </td>
            <td>
                            <strong>
                            <em>
                          <asp:DropDownList ID="ddl_bankname" runat="server" AutoPostBack="True" 
                              onselectedindexchanged="ddl_bankname_SelectedIndexChanged" TabIndex="7" 
                              Width="205px" CssClass=" ">
                          </asp:DropDownList>
                          </em></strong>
                        </td>
        </tr>
        <tr>
            <td class="style3">
      <em>
     
                          <asp:Label ID="lbl_bankid7" runat="server" CssClass="style86" Text="Ifsc code"></asp:Label>
      </em>
                        </td>
            <td>
                            <strong>
                            <em>
                          <asp:DropDownList ID="ddl_ifsccode" runat="server" AutoPostBack="True" 
                              onselectedindexchanged="ddl_ifsccode_SelectedIndexChanged" TabIndex="8" 
                              Width="205px" CssClass=" ">
                          </asp:DropDownList>
                          </em></strong>
                        </td>
        </tr>
        <tr>
            <td class="style3">
      <em>
     
                          <asp:Label ID="lbl_bankid8" runat="server" CssClass="style86" Text="A/c Num"></asp:Label>
      </em>
                        </td>
            <td>
                            <strong>
                            <em>
                          <asp:TextBox ID="txt_account" runat="server" 
                              ForeColor="DarkGoldenrod" TabIndex="9" Width="190px" CssClass=" "></asp:TextBox>
                          </em></strong>
                        </td>
        </tr>
        <tr>
            <td class="style3">
      <em>
     
                          <asp:Label ID="lbl_bankid9" runat="server" CssClass="style86" Text="Added Date"></asp:Label>
      </em>
                        </td>
            <td>
                            <strong>
                            <em>
                          <asp:TextBox ID="txt_adddate" runat="server" 
                              Enabled="False" TabIndex="10" Width="190px" CssClass=" "></asp:TextBox>
                          </em></strong>
                        </td>
        </tr>
        <tr>
            <td class="style3">
                          <asp:Label ID="lbl_bankid10" runat="server" CssClass="style85" Text="PAN Num"></asp:Label>
                        </td>
            <td>
                            <strong>
                            <em>
                          <asp:TextBox ID="txt_pannumber" runat="server" 
                              ForeColor="DarkGoldenrod" TabIndex="11" Width="190px" CssClass=" "></asp:TextBox>
                          </em></strong>
                        </td>
        </tr>
        <tr>
            <td class="style3">
                          <asp:Label ID="lbl_bankid11" runat="server" CssClass="style85" 
                              Text="Qualification"></asp:Label>
                        </td>
            <td>
                            <strong>
                            <em>
                          <asp:TextBox ID="txt_qualifications" runat="server" 
                              ForeColor="DarkGoldenrod" TabIndex="12" Width="190px" CssClass=" "></asp:TextBox>
                          </em></strong>
                        </td>
        </tr>
        <tr>
            <td class="style3">
                          <asp:Label ID="lbl_bankid12" runat="server" CssClass="style85" 
                              Text="Description"></asp:Label>
                        </td>
            <td>
                            <strong>
                            <em>
                          <asp:TextBox ID="txt_description" runat="server" 
                              ForeColor="DarkGoldenrod" Height="38px" TabIndex="13" TextMode="MultiLine" 
                              Width="190px" CssClass=" "></asp:TextBox>
                          </em></strong>
                        </td>
        </tr>
        <tr>
            <td class="style3" colspan="2">
                            <strong>
                            <em>
                          <asp:DropDownList ID="ddl_Plantcode" runat="server" Enabled="False" 
                              Visible="False">
                          </asp:DropDownList>
                          <asp:DropDownList ID="ddl_ccode" runat="server" AutoPostBack="True" 
                              Font-Size="Small" Height="20px" 
                              onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Visible="False" 
                              Width="204px">
                          </asp:DropDownList>
                          </em></strong>
                        </td>
        </tr>
        </table>


    <table class="style1">
        
    </table>
 </fieldset>


</td>
 
</tr>

</table>
</div>

<table width=100%>
<tr width=100% align="center">
<td widh=100%>


    <strong>
      <em><span class="style58">
                          <asp:Label ID="Label40" runat="server" CssClass="style34" 
                              ForeColor="DarkGoldenrod" style="text-align: right; " Text="C " 
                              Visible="False"></asp:Label>
                          <asp:TextBox ID="txt_bank" runat="server" Visible="False" Width="150px"></asp:TextBox>
                          <asp:Button ID="btn_Ok" runat="server"   Height="29px"  CssClass="form93"    onclick="btn_Ok_Click" Text="Save" TabIndex="14" />
                          </span>
                          <asp:DropDownList ID="ddl_bankid" runat="server" Enabled="False" 
                              Visible="False">
                          </asp:DropDownList>
                          </em></strong>


</td>

</tr>
</table>
</div>

                  
                  </td>
          </tr>
          </table>
      <br />


    <div style="background-color: #FFFFFF; border-width: thin; border-color: #00FFFF">
    <panel>
      <table class="style1">
              <tr>
              <td>
    <center style="font-weight: 700; font-style: italic">  
                  <strong>
                  <em>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        DataKeyNames="Supervisor_Code" Font-Size="X-Small" 
        onpageindexchanging="GridView1_PageIndexChanging" 
        onrowdeleting="GridView1_RowDeleting" PageSize="8" EnableViewState="False" 
            onrowcancelingedit="GridView1_RowCancelingEdit1" 
            onrowediting="GridView1_RowEditing" onrowupdating="GridView1_RowUpdating" 
           Font-Italic="False">


              <EditRowStyle BackColor="#999999" />
                                                   <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
                                                   <HeaderStyle BackColor="#f4f4f4" Font-Bold="False" Font-Italic="False"  Font-Names="Raavi" Font-Size="Small" ForeColor="Black" />
                                                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                   <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
                                                   <AlternatingRowStyle HorizontalAlign="Center" />
                                                   <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />



        <Columns>
            <asp:BoundField DataField="Supervisor_Code" HeaderText="SupID" ReadOnly="True" 
                SortExpression="Supervisor_Code" />
            <asp:BoundField DataField="Plant_Code" HeaderText="Pcode" 
                SortExpression="Plant_Code" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            <asp:BoundField DataField="SupervisorName" HeaderText="Super Name" 
                SortExpression="SupervisorName" >
            <ControlStyle Width="75px" />
            <FooterStyle Width="75px" />
            <HeaderStyle Width="75px" />
            <ItemStyle Width="75px" />
            </asp:BoundField>
            <asp:BoundField DataField="Dob" HeaderText="Dob" ReadOnly="True" 
                SortExpression="Dob">
            <ControlStyle Width="8px" />
            <FooterStyle Width="8px" />
            <HeaderStyle Width="8px" />
            <ItemStyle Width="8px" />
            </asp:BoundField>
            <asp:BoundField DataField="Address" HeaderText="Address" 
                SortExpression="Address" ReadOnly="True" >
            <ControlStyle Width="125px" />
            <FooterStyle Width="125px" />
            <HeaderStyle Width="125px" />
            <ItemStyle Width="125px" />
            </asp:BoundField>
            <asp:BoundField DataField="Mobile" HeaderText="Mobile" 
                SortExpression="Mobile" >
            <ControlStyle Width="75px" />
            <FooterStyle Width="75px" />
            <HeaderStyle Width="75px" />
            <ItemStyle Width="75px" />
            </asp:BoundField>
            <asp:BoundField DataField="Bank_name" HeaderText="Bank Name" ReadOnly="True" 
                SortExpression="Bank_name" >
            <ControlStyle Width="75px" />
            <FooterStyle Width="75px" />
            <HeaderStyle Width="75px" />
            <ItemStyle Width="75px" />
            </asp:BoundField>
            <asp:BoundField DataField="IfscCode" HeaderText="IfscCode" 
                SortExpression="IfscCode" >
            <ControlStyle Width="90px" />
            <FooterStyle Width="90px" />
            <HeaderStyle Width="90px" />
            <ItemStyle Width="90px" />
            </asp:BoundField>
            <asp:BoundField DataField="AccountNumber" HeaderText="A/c Number" 
                SortExpression="AccountNumber" >
            <ControlStyle Width="90px" />
            <FooterStyle Width="80px" />
            <HeaderStyle Width="90px" />
            <ItemStyle Width="90px" />
            </asp:BoundField>
            <asp:BoundField DataField="Pannumber" HeaderText="PAN Num" 
                SortExpression="Pannumber" >
            <ControlStyle Width="90px" />
            <FooterStyle Width="90px" />
            <HeaderStyle Width="90px" />
            <ItemStyle Width="90px" />
            </asp:BoundField>
            <asp:CommandField ShowEditButton="True" />
            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
   <%--     <FooterStyle BackColor="#99CCCC" Font-Size="Small" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />--%>
    </asp:GridView>
    </em></strong>
    <br />
    </center>
                  </td>



   
        </div>
</ContentTemplate>


<%--<Triggers>
    <asp:PostBackTrigger ControlID="Button7" />
</Triggers>--%>
</asp:UpdatePanel>
  
    </form>

              
              
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

