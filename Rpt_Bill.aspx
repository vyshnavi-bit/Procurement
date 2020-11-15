<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Rpt_Bill.aspx.cs" Inherits="Rpt_Bill" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style4
        {
            color: #000000;
            font-weight: bold;
            font-size: small;
        }
    </style>

      <style type="text/css">
              .form-style-9{
    max-width: 450px;
    background: #FAFAFA;
    padding: 10px;
    margin:20px auto;
    box-shadow: 1px 1px 25px rgba(0, 0, 0, 0.35);
    border-radius: 10px;
 
         height:100px;
         width: 505px;
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
        height: 30px;
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
    padding: 10px;
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
        font-size: small;
        font-weight: 700;
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
    .style5
    {
        width: 100%;
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
        .style4
        {
            height: 42px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" >
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>  
<%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
   <div align="center">--%>
    <center>
    <table>
       
            <tr valign=top>
                <td>
                    <div class="form-style-9" style="height: 216px">
                        <div class="form-style-3">
                            <table style="height: auto; width: 95%;">
                                <tr ALIGN="LEFT">
                                    <td height="20px" WIDTH="98%">
                                        <fieldset height:"50"="" style="height: auto">
                                            <legend>Agent Bill Copy </legend>
                                            <table class="style5">
                                                <tr>
                                                    <td colspan="2" style="text-align: center" WIDTH="25%">
                                                        <asp:Label ID="Label2" runat="server" CssClass="style4" Text="From" 
                                                            Visible="False"></asp:Label>
                                                        <asp:TextBox ID="txt_FromDate" runat="server" Width="100px" 
                                                            Wrap="False"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                                            PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                                            TargetControlID="txt_FromDate">
                                                        </asp:CalendarExtender>
                                                        <asp:Label ID="Label3" runat="server" CssClass="style4" Text="To" 
                                                            Visible="False"></asp:Label>
                                                        <asp:TextBox ID="txt_ToDate" runat="server" Width="100px" 
                                                            Wrap="False"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" 
                                                            PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                                                            TargetControlID="txt_ToDate">
                                                        </asp:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: center" WIDTH="50%">
                                                        <asp:Label ID="Label1" runat="server" CssClass="style4" Text="Plant_Name"></asp:Label>
                                                        <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
                                                            Font-Bold="True" Font-Size="Large" 
                                                            onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="200px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center" colspan="2">
                                                        <asp:Label ID="Label5" runat="server" CssClass="style4" Text="Bill   date" 
                                                            Visible="False"></asp:Label>
                                                    
                                                        <asp:DropDownList ID="ddl_BillDate" runat="server" CLASS="field4" 
                                                            onselectedindexchanged="ddl_BillDate_SelectedIndexChanged" Width="200px" 
                                                            Font-Bold="True" Font-Size="Large" Visible="False">
                                                        </asp:DropDownList>
                                                      
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="Chk_MilkType" runat="server" Checked="True" 
                                                            style="font-weight: 700; color: #000000; text-align: right;" Text="" 
                                                            Visible="False" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label4" runat="server" 
                                                            style="font-weight: 700; font-size: small" Text="Buff" Visible="False"></asp:Label>
                                                        <asp:DropDownList ID="txt_PlantPhoneNo" runat="server" Visible="False">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                                                            Height="16px" Visible="false" Width="29px">
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lbl_RouteID" runat="server" CssClass="style4" Text="RID" 
                                                            Visible="False"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table style="width:100%; height: 36px;">
                            <tr align="center" width="100%">
                                <td align="CENTER">
                                    <asp:Button ID="btn_ok" runat="server" CssClass="form93" Height="30px" 
                                        onclick="btn_ok_Click" Text="OK" Width="70px" />
                                    <asp:Button ID="btn_Export" runat="server" CssClass="form93" Height="30px" 
                                        onclick="btn_Export_Click" Text="Export" Width="100px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <center>
                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
                            EnableParameterPrompt="False" onunload="CrystalReportViewer1_Unload" 
                            ToolPanelView="None"/>
                    </center>
                </td>
            </tr>
</table>
  
</ContentTemplate>
<%--

<Triggers>
    <asp:PostBackTrigger ControlID="btn_ok" />
       <asp:PostBackTrigger ControlID="btn_Export" />
     <asp:PostBackTrigger ControlID="CrystalReportViewer1" />
</Triggers>
  </asp:UpdatePanel>--%>

    </form>           




</asp:Content>

