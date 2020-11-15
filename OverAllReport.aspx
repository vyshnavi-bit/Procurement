<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OverAllReport.aspx.cs" Inherits="OverAllReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                <tr>
                    <td width="100%" colspan="2">
                        <br />
                        <p class="subheading" style="line-height: 150%">
                            &nbsp;&nbsp;OVERALL REPORT
                        </p>
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
        </ContentTemplate>
    </asp:UpdatePanel>

  <div class="legagentsms">
   <fieldset class="fontt">   
            <legend style="color: #3399FF">OverAll Report</legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
            <td>
                &nbsp;</td>
            </tr>
             <tr>
                    <td>
                    	
                    </td>
                     <td  align="right">
         
                                    &nbsp;</td>
                    <td >                         
                    </td>
                    <td  align="left">

                        <asp:CheckBox ID="Chk_Allplant" runat="server" 
                            oncheckedchanged="Chk_Allplant_CheckedChanged" Text="ALL" 
                            AutoPostBack="True" Checked="True" />
                    </td>
                               <td width="12%">
                               
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
                      

                    <asp:TextBox ID="txt_FromDate" runat="server"  ></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
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
                               
                    <asp:TextBox ID="txt_ToDate" runat="server" TabIndex="1"  ></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                    </asp:CalendarExtender>

                <asp:Button ID="Button1" runat="server" onclick="Button1_Click"  
            BackColor="#6F696F"  ForeColor="White"
            Text="OK" Width="70px" TabIndex="2" />
        
                                
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
                                          	
                        <asp:DropDownList ID="ddl_Plantname"  runat="server" Width="200px" Font-Bold="True" Font-Size="Large">  </asp:DropDownList>

                    </td>
                </tr> 
                  
           
                 <tr>
                    <td>                                    
                    </td>
                     <td>                   
         
                                    &nbsp;</td>
                    <td  align="left">
                  
                    </td>
                    <td  align="left">                    	

                        &nbsp;</td>
                </tr>  
                     
                
            </table>
            <br />
          
   </fieldset>
   </div>  

   
    <div class="legagent">
        <fieldset class="fontt">
            <legend style="color: #3399FF">Plant List</legend>
            <asp:Panel ID="Panel3" runat="server">
                <asp:Table ID="Table2" runat="Server" BorderColor="White" BorderWidth="1" 
                    CaptionAlign="Top" CellPadding="1" CellSpacing="1" Height="40px" 
                    style="margin-left: 0px" Width="200px">
                    <asp:TableRow ID="TableRow1" runat="Server" BorderWidth="1" Width="250px">
                        <asp:TableCell ID="TableCell22" runat="Server" BorderWidth="1">
         <asp:Table ID="Table1" runat="Server" BorderColor="White" BorderWidth="1" 
                CellPadding="1" 
                CellSpacing="1" Width="250px" CaptionAlign="Top" Height="40px" >
                
                
                
<asp:TableRow ID="Title_TableRow" runat="Server" BorderWidth="1" BackColor="#3399CC" ForeColor="white" BorderColor="Silver">
                       
<asp:TableCell ID="TableCell6" runat="Server" BorderWidth="0" > 
     <asp:CheckBox ID="MChk_PlantName" Text="PlantName" runat="server" checked="true"
        AutoPostBack="True" oncheckedchanged="MChk_PlantName_CheckedChanged"/>         
</asp:TableCell>
                
</asp:TableRow>
                
<asp:TableRow ID="TableRow2" runat="Server" BorderWidth="0" BorderColor="Silver" BackColor="#ffffec">
  
  <asp:TableCell ID="TableCell1" runat="Server" BorderWidth="0">
  <asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderWidth="0"> </asp:CheckBoxList> 
</asp:TableCell>       
                
</asp:TableRow>
            
</asp:Table>     
                  
</asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </asp:Panel>
        </fieldset>
    </div>

   
    <br />
   
    <br />
    
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
           AutoDataBind="true"  EnableDatabaseLogonPrompt="False" 
        ToolPanelView="None" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

