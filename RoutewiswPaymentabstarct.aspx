<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RoutewiswPaymentabstarct.aspx.cs" Inherits="RoutewiswPaymentabstarct" Title="OnlineMilkTest|RoutewiseAbstract" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            text-align: right;
        }
        .style2
        {
            font-weight: bold;
            color: #000000;
        }
        .style3
        {
            color: #000000;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>       
       

<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%" colspan="2">
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;ROUTEWISE PAYMENT ABSTRACT</p>
            </td>
        </tr>
        <tr>
            <td width="100%" height="3px" colspan="2">
            </td>
        </tr>
        <tr>
            <td width="100%" class="line" height="1px" colspan="2">
            </td>
        </tr>
        <tr>
            <td width="100%" height="7" colspan="2">
                
            </td>
        </tr>
        </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <br />

        </ContentTemplate>
 </asp:UpdatePanel>
 
 <table ALIGN="center" width="40%" bgcolor="#FFEFF6">
 <tr valign=top align=center>
 <td>
   <fieldset class="fontt">   
            <legend style="color: #3399FF">RoutewiseAbstract </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
                     <td class="style1">
                       &nbsp;<asp:Label ID="Label2" runat="server" Text="From" CssClass="style2"></asp:Label>      
                    </td>
                    <td  align="left">
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
                                <asp:TextBox ID="txt_FromDate" runat="server"  ></asp:TextBox>

                            </td>
                </tr>  
                  <tr>
                     <td class="style1">                   
                         <asp:Label ID="Label3" runat="server" Text="To" CssClass="style2"></asp:Label> 
                         <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>     
                    </td>
                    <td  align="left">                    	
                              <asp:TextBox ID="txt_ToDate" runat="server"  ></asp:TextBox>                    	
                    </td>
                </tr> 
                 <tr>
                     <td class="style1"> 
             <asp:Label ID="Label1" runat="server" Text="Plant_Name" CssClass="style2"></asp:Label>      
                    </td>
                    <td  align="left"> 
                         <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>                   	
                    	<asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
        Width="200px" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
                 Font-Bold="True" Font-Size="Large" ></asp:DropDownList>

                    </td>
                </tr> 
                 <tr>
                     <td class="style1"> 
             <asp:Label ID="Label4" runat="server" Text="Bill Date" CssClass="style2"></asp:Label>      
                    </td>
                    <td  align="left"> 
                    	<asp:DropDownList ID="ddl_BillDate" runat="server" AutoPostBack="true" 
        Width="200px" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
                 Font-Bold="True" Font-Size="Large" Visible="False" ></asp:DropDownList>

                    </td>
                </tr> 
                  <tr>
                     <td class="style1">                   
         <asp:Label ID="lbl_RouteName" runat="server" Text="Route Name" CssClass="style2"></asp:Label>

                    </td>
                    <td  align="left">                    	

             <asp:CheckBox ID="chk_Allroute" runat="server" AutoPostBack="True" 
                 Checked="True" oncheckedchanged="chk_Allroute_CheckedChanged" 
                 Text="All Route" CssClass="style3" />

             <asp:CheckBox ID="chk_Buff" runat="server"  Text="Buff" Visible="False" 
                            CssClass="style3" />
   
                    </td>
                </tr> 
            <tr>
                     <td class="style1">
         
             <asp:Label ID="lbl_RouteID" runat="server" Text="Route ID" Visible="False" 
                             CssClass="style2"></asp:Label>

                    </td>
                    <td  align="left">
 <asp:DropDownList ID="ddl_RouteName" 
        runat="server" Width="190px" 
        onselectedindexchanged="ddl_RouteName_SelectedIndexChanged" 
         AutoPostBack="True" Font-Size="Large" 
       >
    </asp:DropDownList>
                    </td>
                </tr>   
                 <tr>
                     <td>                   
         
         <asp:DropDownList ID="ddl_RouteID" 
        runat="server" Width="39px" 
        onselectedindexchanged="ddl_RouteID_SelectedIndexChanged" 
        AutoPostBack="True" Enabled="False" Visible="False" Height="16px">
    </asp:DropDownList>
         
                     <asp:DropDownList ID="ddl_Plantcode" AutoPostBack="true" runat="server" 
                       Visible="false" Height="16px" Width="29px" > </asp:DropDownList>                        

                    </td>
                    <td  align="left">                    	
 <asp:Button ID="btn_Ok" runat="server"
            Text="OK" Width="70px" Height="30px" onclick="btn_Ok_Click" CssClass="form93" />
 <asp:Button ID="btn_Export" runat="server"  ForeColor="White"
            Text="Export" Width="100px" Height="30px" onclick="btn_Export_Click" CssClass="form93"  />
           
                    </td>
                </tr>  
                     
                
            </table>
            <br />
          
   </fieldset>
   </td>
   </tr>
   </table>
    

<br />


       <center>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" OnUnload="CrystalReportViewer1_Unload"
        EnableParameterPrompt="False" ToolPanelView="None"/>
  </center>
  
</asp:Content>


