<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Rpt_AccountsTotalAbstract.aspx.cs" Inherits="Rpt_AccountsTotalAbstract" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%"><br />
                <p class="subheading" style="line-height: 150%">
                    Accounts TotalAbstract</p>
            </td>
        </tr>
        <tr>
            <td width="100%" height="3px">
            </td>
        </tr>
        <tr>
            <td width="100%" class="line" height="1px">
            </td>
        </tr>
        <tr>
            <td width="100%" height="7">
                
            </td>
        </tr>
        </table>
 </ContentTemplate>
 </asp:UpdatePanel>
 

   <br />
   <div class="legagentsms">
   <fieldset class="fontt">   
            <legend style="color: #3399FF">TotalAbstract </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
            <td>
                &nbsp;</td>
            </tr>
             <tr>
                    <td>
                        &nbsp;</td>
                     <td align="right">
                       &nbsp;<asp:Label ID="Label2" runat="server" Text="From"></asp:Label>      
                    </td>
                    <td >
                  
                    </td>
                    <td  align="left">                                
                                <asp:TextBox ID="txt_FromDate" runat="server"  ></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>

                            </td>
                </tr>  
                  <tr>
                    <td>                                    
                    </td>
                     <td align="right">                   
                         <asp:Label ID="Label3" runat="server" Text="To"></asp:Label> 
                         <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>     
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                              <asp:TextBox ID="txt_ToDate" runat="server"  ></asp:TextBox>                    	
                    </td>
                </tr> 
                 <tr>
                    <td>                                    
                    </td>
                     <td align="right"> 
             <asp:Label ID="Label1" runat="server" Text="Plant_Name"></asp:Label>      
                    </td>
                    <td  >                  
                        &nbsp;</td>
                    <td  align="left"> 
                                      	
                    	<asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
        Width="200px" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
                 Font-Bold="True" Font-Size="Large" ></asp:DropDownList>

                    </td>
                </tr> 
                  <tr>
                    <td>                                    
                    </td>
                     <td>                   
         <asp:Label ID="lbl_RouteName" runat="server" Text="Route Name"></asp:Label>

                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	

             <asp:CheckBox ID="chk_Allloan" runat="server" AutoPostBack="True" 
                 Checked="True" oncheckedchanged="chk_Allloan_CheckedChanged" Text="ALL" />
   
             <asp:CheckBox ID="chk_Buff" runat="server"  Text="Buff" />
   
                    </td>
                </tr> 
            <tr>
                    <td>
                    	
                    </td>
                     <td  align="left">
         
             <asp:Label ID="lbl_RouteID" runat="server" Text="Route ID" Visible="False"></asp:Label>

                    </td>
                    <td >                         
                    </td>
                    <td  align="left">
         <asp:DropDownList ID="ddl_RouteID" 
        runat="server" Width="29px" 
        onselectedindexchanged="ddl_RouteID_SelectedIndexChanged" 
        AutoPostBack="True" Enabled="False" Visible="False" Font-Size="Large" Height="16px">
    </asp:DropDownList>
 <asp:DropDownList ID="ddl_RouteName" 
        runat="server" Width="27px" 
        onselectedindexchanged="ddl_RouteName_SelectedIndexChanged" 
         AutoPostBack="True" Font-Size="Large" Height="16px" 
       >
    </asp:DropDownList>
                    </td>
                               <td width="12%">
                                
                    </td>
                </tr>   
                 <tr>
                    <td>                                    
                    </td>
                     <td>                   
         
                     <asp:DropDownList ID="ddl_Plantcode" AutoPostBack="true" runat="server" 
                       Visible="false" Height="16px" Width="29px" > </asp:DropDownList>                        

                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	

 <asp:Button ID="Button1" runat="server" onclick="Button1_Click"  
         BackColor="#6F696F"  ForeColor="White"
            Text="OK" Width="50px" Height="26px" />
                        <asp:Button ID="btn_Export" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="Export" Width="70px" Height="26px" onclick="btn_Export_Click"  />
           
                    </td>
                </tr>  
                     
                
            </table>
            <br />
          
   </fieldset>
   </div>   
   

   <center>

            </center>




<br />


  <table>
       <tr>
    <td width="15%">&nbsp;</td>
            <td width="16%" align="left">
        
                &nbsp;</td>
            </tr>
       </table>
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
          EnableParameterPrompt="False" ToolPanelView="None" PrintMode="ActiveX"/>
</asp:Content>

