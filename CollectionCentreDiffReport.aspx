<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CollectionCentreDiffReport.aspx.cs" Inherits="CollectionCentreDiffReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>

      <div class="legagentsms">
   <fieldset class="fontt">   
            <legend style="color: #3399FF">Difference Report </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
            <td>
                &nbsp;</td>
            </tr>
             <tr>
                    <td>
                        &nbsp;</td>
                     <td align="right">
                                      <asp:Label ID="Label43" runat="server" CssClass="style4" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Plant Name"></asp:Label>
                    </td>
                    <td >
                  
                        &nbsp;</td>
                    <td  align="left">
                                      <asp:DropDownList ID="ddl_PlantName" runat="server"  
                                          class="ddl2" CssClass="ddl2" Height="30px" 
                                           Width="200px">
                                      </asp:DropDownList>
                            </td>
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
                         <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>                          
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                              <asp:TextBox ID="txt_ToDate" runat="server"  ></asp:TextBox>     
                              <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>                    	
                    </td>
                </tr> 
                 <tr>
                    <td>                                    
                    </td>
                     <td align="right"> 
                         &nbsp;</td>
                    <td  >                  
                        &nbsp;</td>
                    <td  align="left"> 
                                            	
                    	<asp:CheckBox ID="Chk_single" Text="Single" runat="server" AutoPostBack="True" 
                            oncheckedchanged="Chk_single_CheckedChanged" />
                                            	
                    	<asp:CheckBox ID="Chk_All" Text="ALL" runat="server" AutoPostBack="True" 
                            oncheckedchanged="Chk_All_CheckedChanged" />
                     </td>
                </tr> 
                  
            <tr>
                    <td>
                    	
                    </td>
                     <td  align="right">
         
             <asp:Label ID="Label1" runat="server" Text="Agent_Id"></asp:Label>      
                    </td>
                    <td >                         
                    </td>
                    <td  align="left">
                                            	
                    	<asp:DropDownList ID="ddl_AgentId" runat="server" Font-Bold="True" 
                            Font-Size="Large">
                            <asp:ListItem>1302</asp:ListItem>
                            <asp:ListItem>1403</asp:ListItem>
                            <asp:ListItem>1404</asp:ListItem>
                            <asp:ListItem>1621</asp:ListItem>
                            <asp:ListItem>2202</asp:ListItem>
                            <asp:ListItem>2203</asp:ListItem>
                            <asp:ListItem>2204</asp:ListItem>
                            <asp:ListItem>2205</asp:ListItem>
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
                  
                        &nbsp;</td>
                    <td  align="left">                    	
    <asp:Button ID="btn_GetDiffdata" runat="server"   BackColor="#00CCFF"  ForeColor="White"
            Text="OK" Width="70px" Font-Bold="True" BorderStyle="Double" 
    Height="30px" onclick="btn_GetDiffdata_Click"  />

                    </td>
                </tr>  
                     
                
            </table>

                              
                  


  <center>  <asp:Label ID="Lbl_Errormsg" runat="server" Font-Size="Large" 
    ForeColor="Red" style="font-family: Andalus"></asp:Label>
    </center>
            <br />
          
   </fieldset>
   </div>

                              
                  


    <br />
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" ToolPanelView="None"/>
    

   
  
<br />
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
  
</asp:Content>

