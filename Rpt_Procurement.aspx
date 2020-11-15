<%@ Page Title="OnlineMilkTest|Bill" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Rpt_Procurement.aspx.cs" Inherits="Rpt_Procurement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
      .modalPopup
        {
            background-color: #FFFFFF;
            filter: alpha(opacity=40);
            opacity: 0.7;
            xindex: -1;
        }
        
</style>
 <script language="javascript" type="text/javascript">

     function confirmationclose() {
         if (confirm('are you  want to close ?')) {
             return true;
         }
         else {
             return false;
         }
     }
        
        </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
     <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: transparent; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..."
                    ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
<table align=center border="0" cellpadding="0" cellspacing="1" width="40%">
        <tr>
            <td width="100%"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;PROCEED BILLING
                </p>
            </td>
        </tr>
        <tr>
            <td width="100%" class="line" height="1px">
            </td>
        </tr>
        <tr width=50%>
            <td height="7" width="50%">

   <fieldset class="fontt">   
            <legend style="color: #3399FF">PROCEED BILLING </legend>
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
                                <asp:TextBox ID="txt_FromDate" runat="server" 
                                    ontextchanged="txt_FromDate_TextChanged" Enabled="False"  ></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
                            </td>
                </tr>  
                  <tr>
                    <td>                                    
                    </td>
                     <td align="right">                   
                         <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>                          
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                              <asp:TextBox ID="txt_ToDate" runat="server" Enabled="False"  ></asp:TextBox>     
                              <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>                    	
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
             <asp:Label ID="lbl_RouteID" runat="server" Text="RID" Visible="False"></asp:Label>

                     <asp:DropDownList ID="ddl_Plantcode" AutoPostBack="true" runat="server" 
                       Visible="false" Height="16px" Width="29px" 
                 onselectedindexchanged="ddl_Plantcode_SelectedIndexChanged"> </asp:DropDownList>                        
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
             <asp:CheckBox ID="chk_rate" runat="server" 
                 oncheckedchanged="CheckBox1_CheckedChanged" Text="Buff_Bill" Visible="False" />

        <asp:CheckBox ID="chk_Loandeduct" runat="server" AutoPostBack="True" 
            Text="Loan" Visible="False" />   

                    </td>
                </tr> 
            <tr>
                    <td>
                    	
                    </td>
                     <td  align="left">
         
 <asp:DropDownList ID="ddl_RouteName" 
        runat="server" Width="27px" 
        onselectedindexchanged="ddl_RouteName_SelectedIndexChanged" AutoPostBack="True" 
                 Visible="False" Height="16px" 
       >
    </asp:DropDownList>
     <asp:Button ID="Button1" runat="server" onclick="Button1_Click"  
         BackColor="#6F696F"  ForeColor="White"
            Text="WITH LOAN" Width="27px" Height="16px" Visible="False" />
                    </td>
                    <td >                         
                    </td>
                    <td  align="left">
                        <asp:DropDownList ID="txt_PlantPhoneNo" runat="server" Visible="False">
                        </asp:DropDownList>
                    </td>
                               
                </tr>   
                 <tr>
                    <td colspan="4" align="center">                                    
                        <asp:DropDownList ID="ddl_RouteID" runat="server" AutoPostBack="True" 
                            Enabled="False" Height="16px" 
                            onselectedindexchanged="ddl_RouteID_SelectedIndexChanged" Visible="False" 
                            Width="19px">
                        </asp:DropDownList>
                        <asp:Label ID="lbl_RouteName" runat="server" Text="RName" Visible="False"></asp:Label>
                        <asp:Button ID="Button2" runat="server" BackColor="#6F696F" ForeColor="White" 
                            onclick="Button2_Click" Text="OK" Visible="False" Width="50px" />
                        <asp:Button ID="btn_Export" runat="server" BackColor="#6F696F" 
                            ForeColor="White" Height="26px" onclick="btn_Export_Click" Text="Export" 
                            Visible="False" Width="70px" />
                        <asp:Button ID="btn_Proceed" runat="server" BackColor="#6F696F" 
                            ForeColor="White" onclick="btn_Proceed_Click" Text="Proceed" />
						<asp:Button ID="Button3" runat="server" onclick="btn_delete_Click"  
							Text="Delete" BackColor="#6F696F" 
							style="color: #FFFFFF; font-weight: 700; background-color: #FF0000;" />
						
                       

                    </td>
                </tr>  
                     
                
            </table>
            <asp:Label ID="Label4" runat="server" Font-Size="Large" ForeColor="Red" 
                Text="Label"></asp:Label>
            <br />
          
   </fieldset>


                &nbsp;</td>
        </tr>
        </table>
 

   <br />
   
<br />


       </ContentTemplate>
 </asp:UpdatePanel> 
 <table width=100%>
 <tr  align=center>
 <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" ToolPanelView="None"/>
 </tr>
 </table>
        
    

   
  
</asp:Content>

