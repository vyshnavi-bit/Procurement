<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Sms_Report.aspx.cs" Inherits="Sms_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<script runat="server">

  
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="stylesheet" />

<style type="text/css">
.stylesfiledset
{
    
    width:40%;
}
</style>


 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
   <div>
   <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">    </asp:ToolkitScriptManager>   

</div>
 <br />
   <div ALIGN=center>
   <fieldset class=stylesfiledset>
            <legend style="color: #3399FF">SMS REPORT </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">
            <tr>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Height="16px" Width="61px" Visible="false"></asp:TextBox>
            </td>
            </tr>
             <tr>
            <td>
                <asp:TextBox ID="TextBox2" runat="server" Height="16px" Width="61px" Visible="false"></asp:TextBox>
            </td>
            </tr>
             <tr>
                    <td>
                     <asp:DropDownList ID="ddl_Plantcode" AutoPostBack="true" runat="server" 
                       Visible="false" Height="16px" Width="29px"> </asp:DropDownList>                        
                    </td>
                     <td align="right">
                       Plant_Name:
                    </td>
                    <td >
                  
                    </td>
                    <td  align="left">
                    	<asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
        Width="170px" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" ></asp:DropDownList>
                    </td>
                </tr>  
                  <tr>
                    <td>                                    
                    </td>
                     <td>                   
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                    </td>
                </tr> 
                 <tr>
                    <td>                                    
                    </td>
                     <td align="right"> 
                     From :                  
                    </td>
                    <td  >                  
                    </td>
                    <td  align="left"> 
                     <asp:TextBox ID="txt_FromDate" runat="server" ></asp:TextBox>
                         <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>                   	
                    </td>
                </tr> 
                  <tr>
                    <td>                                    
                    </td>
                     <td>                   
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                    </td>
                </tr> 
            <tr>
                    <td>
                    	
                    </td>
                     <td  align="right">
                       To :
                    </td>
                    <td >                         
                    </td>
                    <td  align="left">
                        <asp:TextBox ID="txt_ToDate" runat="server"  ></asp:TextBox></td>
                               <td width="12%">
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
                    </td>
                </tr>   
                                      
                
            </table>
            <table border="0" width="100%" id="table5" cellspacing="1" align="center">
                <tr>
                    <td >
                        &nbsp;</td>
                    <td align="center" >
                    <asp:Button ID="btn_SmsReport" runat="server" Text="SmsReport"  BackColor="#6F696F"
                     ForeColor="White" Width="110px" style="height: 26px" 
                            onclick="btn_SmsReport_Click"  />                                              
                    </td>
                    <td align="right">
                        &nbsp;</td>
                </tr>
            </table>
 <br /> 
   </fieldset>
   </div>
    <br />
  <br />

  <table WIDTH=100%>
  <tr  align=center>
  <td>
  
      <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" ToolPanelView="None"/>
  
  </td>
  </tr>
  </table>

      </asp:Content>
