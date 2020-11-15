<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Accounts_TransactionReport.aspx.cs" Inherits="Accounts_TransactionReport" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager> 

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <br />

        </ContentTemplate>
 </asp:UpdatePanel>
  <div align="left">
      <asp:HyperLink ID="HyperLink1" CssClass="fontt" runat="server" NavigateUrl="~/OverHeadEntry.aspx">Back</asp:HyperLink>
    </div>
 <div class="legagentsms">
   <fieldset class="fontt">   
            <legend style="color: #3399FF">Transaction Reports </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">  
             <tr>
                    <td>                                    
                    </td>
                     <td>                   
         
                         &nbsp;</td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                        <asp:CheckBox ID="Chk_All" runat="server" AutoPostBack="True" Checked="True" 
                            oncheckedchanged="Chk_All_CheckedChanged" Text="ALL" />
                    </td>
                </tr>            
            
              <tr>
                    <td>                                    
                    </td>
                     <td align="left"> 
             <asp:Label ID="Label5" runat="server" Text="Plant Name"></asp:Label>      
                    </td>
                    <td  >                  
                        &nbsp;</td>
                    <td  align="left">                 
                       <asp:DropDownList ID="ddl_PlantName" runat="server" 
                           Width="170px" >
                       </asp:DropDownList>
                      
                    </td>
                </tr> 
             <tr>
                    <td>
                        &nbsp;</td>
                     <td align="left">
                       &nbsp;<asp:Label ID="Label2" runat="server" Text="From"></asp:Label>      
                    </td>
                    <td >
                  
                        &nbsp;</td>
                    <td  align="left">
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
                                <asp:TextBox ID="txt_FromDate" runat="server"  ></asp:TextBox>

                            </td>
                </tr>  
                  <tr>
                    <td>                                    
                    </td>
                     <td align="left">                   
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
                     <td>                   
         
             <asp:Label ID="Label6" runat="server" Text="Heads Name"></asp:Label>      
                     </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                      
                       <asp:DropDownList ID="ddl_HeadName" runat="server" AutoPostBack="true" 
                           Width="170px" onselectedindexchanged="ddl_HeadName_SelectedIndexChanged">
                       </asp:DropDownList>
                      
                        <asp:CheckBox ID="Chk_Iou" runat="server"  Checked="True" 
                             Text="IOU" />
                      
                        <asp:CheckBox ID="Chk_IouAll" runat="server"  Checked="True" 
                             Text="IOUALL" />
                      
                    </td>
                </tr>  
                 <tr>
                    <td>                                    
                    </td>
                     <td align="left"> 
             <asp:Label ID="Label1" runat="server" Text="Name of Ledger"></asp:Label>      
                    </td>
                    <td  >                  
                        &nbsp;</td>
                    <td  align="left">                 
                       <asp:DropDownList ID="ddl_SubHeadName" runat="server" Width="170px">
                       </asp:DropDownList>                      
                      
                    </td>
                </tr> 
                 <tr>
                    <td>                                    
                    </td>
                     <td align="left"> 
                      
             <asp:Label ID="Label4" runat="server" Text="Type"></asp:Label>      
                      
                       </td>
                    <td  >                  
                        &nbsp;</td>
                    <td  align="left">                         
                      
                       <asp:DropDownList ID="ddl_Type" runat="server" Width="167px" Height="43px" 
                              onselectedindexchanged="ddl_Type_SelectedIndexChanged">
                           <asp:ListItem Value="Credit">Credit</asp:ListItem>
                           <asp:ListItem Value="Debit">Debit</asp:ListItem>
                           <asp:ListItem Value="Due">IOU</asp:ListItem>
                       </asp:DropDownList>                      
                      
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
 &nbsp;<asp:Button ID="btn_Ok" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="Generate" Width="70px" Height="26px" onclick="btn_Ok_Click" Font-Bold="True" />
           
                    </td>
                </tr>  
                     
                
            </table>
            <br />
          
   </fieldset>
   </div>   

<br />


  <table>
       <tr>
    <td width="15%"></td>
            <td width="16%" align="left">
        
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                    AutoDataBind="True" GroupTreeImagesFolderUrl="" Height="1202px" 
                    ToolbarImagesFolderUrl="" ToolPanelView="None" ToolPanelWidth="200px" 
                    Width="1104px" />
        
            </td>
            </tr>
       </table>
</asp:Content>

