<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DpuBill.aspx.cs" Inherits="DpuBill" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">

.styles12
{
    width:40%;
    
    
}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
<br />
<br />
<center>
 <div class="styles12">
   <fieldset class="fontt">   
            <legend style="color: #3399FF">Dpu Bill </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
            <td>
                &nbsp;</td>
            </tr>
             <tr>
                    <td>
                        &nbsp;</td>
                     <td align="right">
                       &nbsp;<asp:Label ID="Label2" runat="server" Text="From" Visible="False"></asp:Label>      
                    </td>
                    <td >
                  
                    </td>
                    <td  align="left">
                                <asp:TextBox ID="txt_FromDate" runat="server" Visible="False" 
                                    ></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
                            </td>
                </tr>  
                  <tr>
                    <td>                                    
                    </td>
                     <td align="right">                   
                         <asp:Label ID="Label3" runat="server" Text="To" Visible="False"></asp:Label>                          
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                              <asp:TextBox ID="txt_ToDate" runat="server" Visible="False"  ></asp:TextBox>     
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
                        &nbsp;</td>
                     <td align="right"> 
                         &nbsp;</td>
                    <td  >                  
                        &nbsp;</td>
                    <td  align="left"> 
                                            	
                    	<label __designer:mapid="192" for="field3">
    <asp:DropDownList ID="ddl_BillDate" runat="server" CLASS="field4" 
        onselectedindexchanged="ddl_BillDate_SelectedIndexChanged" Width="200px">
    </asp:DropDownList>
         </label>                   

                    </td>
                </tr> 
                  <tr>
                    <td>                                    
                    </td>
                     <td>                   
             <asp:Label ID="lbl_RouteID" runat="server" Text="RID" Visible="False"></asp:Label>

                     <asp:DropDownList ID="ddl_Plantcode" AutoPostBack="true" runat="server" 
                       Visible="false" Height="16px" Width="29px"> </asp:DropDownList>                        
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                        <asp:CheckBox ID="Chk_MilkType" runat="server" Checked="True" Text="Buff" 
                            Visible="False" />
                      </td>
                </tr> 
            <tr>
                    <td>
                    	
                    </td>
                     <td  align="left">
         
                         &nbsp;</td>
                    <td >                         
                    </td>
                    <td  align="left">
                        <asp:DropDownList ID="txt_PlantPhoneNo" runat="server" Visible="False">
                        </asp:DropDownList>
                    </td>
                               <td width="12%">
                                 
                    </td>
                </tr>   
                 <tr>
                    <td>                                    
                    </td>
                     <td>                   
         
                         &nbsp;</td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
     <asp:Button ID="btn_ok" runat="server"  
         BackColor="#6F696F"  ForeColor="White"
            Text="OK" Width="50px" onclick="btn_ok_Click" /> 

             <asp:Button ID="btn_Export" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="Export" Width="70px" Height="26px" onclick="btn_Export_Click"  />

                    </td>
                </tr>  
                     
                
            </table>
            <br />
          
   </fieldset>
   </div>
   </center>
<br />


  <table width="100%" ALIGN="center">
       <tr align="center">
    <td width="15%" style="width: 31%">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" ToolPanelView="None" 
            onunload="CrystalReportViewer1_Unload"/>
           </td>
            </tr>
       </table>
        </asp:Content>


