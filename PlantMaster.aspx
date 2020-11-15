<%@ Page Title="OnlineMilkTest|PlantMaster" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="PlantMaster.aspx.cs" Inherits="PlantMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="stylesheet" />
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
            width: 180px;
            font-family: Arial;
            text-align: right;
        }
        .style4
        {
            width: 199px;
            text-align: right;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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


    <table  >
        <tr>
            <td>
           <div class="form-style-9" style="height: auto">
<div class="form-style-3">
<table width=100% style="height: auto">

<tr  valign=top ALIGN=LEFT>
<td WIDTH="98%" height="20px">

<fieldset  height:"50" style="height: auto"><legend>Plant Master Bill Copy</legend>


    <table class="style2">
        <tr>
            <td class="style3">
                            <asp:Label ID="lbl_plantcode" runat="server" Text="Plant Code" CssClass="style1"></asp:Label>
                        </td>
            <td>
                            <asp:TextBox ID="txt_plantcode" runat="server" Enabled="False" Width="175px"></asp:TextBox>
                        </td>
        </tr>
        <tr>
            <td class="style3">
                            <asp:Label ID="lbl_plantname" runat="server" Text="Plant Name" CssClass="style1"></asp:Label>
                        </td>
            <td>
                            <asp:TextBox ID="txt_Plantname" runat="server" TabIndex="1" Width="175px"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator2" ControlToValidate="txt_Plantname"
                                ValidationExpression="[a-z A-Z]*" ErrorMessage="*" Display="dynamic"> </asp:RegularExpressionValidator>
                        </td>
        </tr>
        <tr>
            <td class="style3">
                            <asp:Label ID="lbl_plantaddress" runat="server" Text="Address" CssClass="style1"></asp:Label>
                        </td>
            <td>
                            <asp:TextBox ID="txt_plantaddress" runat="server" TabIndex="2" Height="40px" 
                                TextMode="MultiLine" Width="175px"></asp:TextBox>
                        </td>
        </tr>
        <tr>
            <td class="style3">
                            <asp:Label ID="lbl_plantphoneno" runat="server" Text=" Phone No" CssClass="style1"></asp:Label>
                        </td>
            <td>
                            <asp:TextBox ID="txt_plantphoneno" runat="server" TabIndex="3" Width="175px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_plantphoneno"
                                ErrorMessage="*" ValidationExpression="^[0-9,.]{1,12}$"></asp:RegularExpressionValidator>
                        </td>
        </tr>
        <tr>
            <td class="style3">
                            <asp:Label ID="lbl_plantphoneno0" runat="server" Text=" Mail Id" CssClass="style1"></asp:Label>
                        </td>
            <td>
                            <asp:TextBox ID="txt_pmail" runat="server" TabIndex="3" Width="175px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txt_pmail"
                                ErrorMessage="*" ValidationExpression="^([a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]){1,70}$"></asp:RegularExpressionValidator>
                        </td>
        </tr>
        <tr>
            <td class="style3">
                            <asp:Label ID="lbl_Managername" runat="server" Text="Manager Name" CssClass="style1"></asp:Label>
                        </td>
            <td>
                            <asp:TextBox ID="txt_manager" runat="server" TabIndex="4" Width="175px"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator3" ControlToValidate="txt_Plantname"
                                ValidationExpression="[a-z A-Z]*" ErrorMessage="*" Display="dynamic"> </asp:RegularExpressionValidator>
                        </td>
        </tr>
        <tr>
            <td class="style3">
                            <asp:Label ID="lbl_Mobileno" runat="server" 
                                Style="color: #333300; font-family: Arial;">Mobile No</asp:Label>
                        </td>
            <td>
                            <asp:TextBox ID="txt_manaphone" runat="server" TabIndex="5" Width="175px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txt_plantphoneno"
                                ErrorMessage="*" ValidationExpression="^[0-9,.]{1,12}$"></asp:RegularExpressionValidator>
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

</div>
                
                
                
                
                
                
                </td>
            <td valign="top">
                <div class="form-style-9" style="height: 324px">
                    <div class="form-style-3">
                        <table style="height: auto" width="100%">
                            <tr align="LEFT">
                                <td height="20px" width="98%">
                                    <fieldset height:"50"="" style="height: auto">
                                        <legend>Company Master</legend>
                                        <table class="style2">
                                            <tr>
                                                <td class="style4">
                            <asp:Label ID="lbl_companycode" runat="server" Text="Company Code" CssClass="style1"></asp:Label>
                                                </td>
                                                <td>
                            <asp:TextBox ID="txt_companycode" runat="server" TabIndex="6" Width="150px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style4">
                            <asp:Label ID="lbl_companyname" runat="server" Text="Company Name" CssClass="style1"></asp:Label>
                                                </td>
                                                <td>
                            <asp:TextBox ID="txt_companyname" runat="server" TabIndex="7" Width="175px"></asp:TextBox>
                            <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="txt_companyname"
                                ValidationExpression="[a-z A-Z]*" ErrorMessage="*" Display="dynamic"> </asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style4">
                            <asp:Label ID="lbl_companyaddress" runat="server" Text="Address" CssClass="style1"></asp:Label>
                                                </td>
                                                <td>
                            <asp:TextBox ID="txt_companyaddress" runat="server" TabIndex="8" TextMode="MultiLine"
                                Height="40px" Enabled="False" Width="175px" ontextchanged="txt_companyaddress_TextChanged"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style4">
                            <asp:Label ID="lbl_companyphoneno" runat="server" Text="Phone No" CssClass="style1"></asp:Label>
                                                </td>
                                                <td>
                            <asp:TextBox ID="txt_companyphoneno" runat="server" TabIndex="9" Width="175px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txt_companyphoneno"
                                ErrorMessage="*" ValidationExpression="^[0-9,.]{1,12}$"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style4">
                            <asp:Label ID="lbl_plantphoneno1" runat="server" Text=" Mail Id" CssClass="style1"></asp:Label>
                                                </td>
                                                <td>
                            <asp:TextBox ID="txt_cmail" runat="server" TabIndex="3" Width="175px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txt_cmail"
                                ErrorMessage="*" ValidationExpression="^([a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]){1,70}$"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style4">
                            <asp:Label ID="lbl_companytinno" runat="server" Text="TIN No" CssClass="style1"></asp:Label>
                                                </td>
                                                <td>
                            <asp:TextBox ID="txt_companytinno" runat="server" TabIndex="11" Width="175px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_companytinno"
                                ErrorMessage="*" Display="dynamic" ValidationExpression="^[a-zA-Z,.]{1,30}$"> </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style4">
                            <asp:Label ID="lbl_companycstno" runat="server" Text="Cst no" CssClass="style1" Visible="False"></asp:Label>
                                                </td>
                                                <td>
                            <asp:TextBox ID="txt_companycstno" runat="server" TabIndex="12" Visible="False" Enabled="False" 
                                                        Width="175px">01</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_companycstno"
                                ErrorMessage="*" Display="dynamic" ValidationExpression="^[a-zA-Z,.]{1,30}$"> </asp:RequiredFieldValidator>
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
                </div>
            </td>
        </tr>
        </table>

        <table width="100%">
        <tr>
        <td style="text-align: center">
        
        
                    <asp:Button ID="Button1" runat="server" Text="Save"   CssClass="form93"     TabIndex="7" OnClick="Button1_Click" />
        
        
        </td>
        </tr>
        </table>

    <div style="width: 100%;">
        <div class="legendcompany">
            &nbsp;</div>
        <br />
        <center>
        <div width="80%">
            <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
           
                <ContentTemplate>
                
                    <mcn:DataPagerGridView ID="gvProducts" runat="server" OnRowDataBound="RowDataBound"
                        AutoGenerateColumns="False" AllowPaging="True" CssClass="gridview1"
                        CellPadding="0" BorderWidth="0px" GridLines="None" DataSourceID="SqlDataSource1"
                        PageSize="5" Font-Size="Small">
                        <Columns>
                            <asp:BoundField HeaderText="Company ID" DataField="Company_Code" SortExpression="Company_Code"
                                HeaderStyle-CssClass="first" ItemStyle-CssClass="first">
                                <HeaderStyle CssClass="first" />
                                <ItemStyle CssClass="first" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Company_Name" HeaderText="Company Name" SortExpression="Company_Name" />
                            <asp:BoundField DataField="Company_Address" HeaderText="CompanyAddress" SortExpression="Company_Address" />
                            <asp:BoundField DataField="Plant_Code" HeaderText="Plant ID" SortExpression="Plant_Code" />
                           
                            <asp:BoundField DataField="Plant_Name" HeaderText="Plant Name" SortExpression="Plant_Name" />
                           
                            <asp:BoundField DataField="Plant_Address" HeaderText="PlantAddress" SortExpression="Plant_Address" />
                            <asp:BoundField DataField="TinNo" HeaderText="TinNo" SortExpression="TinNo" />
                        </Columns>
                        <PagerSettings Visible="False" />
                        <RowStyle CssClass="row" />
                    </mcn:DataPagerGridView>
                    
                    <div class="pager">
                        <asp:DataPager ID="pager" runat="server" PageSize="8" PagedControlID="gvProducts">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="‹Previous"
                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="current"
                                    NextPreviousButtonCssClass="command" />
                                <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="Next›"
                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </ContentTemplate>
              
            </asp:UpdatePanel>
           </div>
    </center>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>"
            SelectCommand="select t1.Company_Code,t1.company_Name ,t1.Company_Address,t2.Plant_Code,t2.Plant_Name,t2.Plant_Address,t1.Tinno from
(select* from Company_Master) as t1
left join
(select * from Plant_Master) as t2 on t1.Company_Code=t2.Company_Code ORDER BY Company_Name DESC">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="Plant_code" Name="Plant_code" SessionField="Plant_Code" />
                <asp:SessionParameter DefaultValue="Company_code" Name="Company_code" SessionField="Company_code" />
            </SelectParameters>
        </asp:SqlDataSource>
        <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />


        </div>
</ContentTemplate>


<%--<Triggers>
    <asp:PostBackTrigger ControlID="Button7" />
</Triggers>--%>
</asp:UpdatePanel>
  
    </form>


</asp:Content>
