<%@ Page Title="OnlineMilkTest|TotalSummary" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TotalSummary.aspx.cs" Inherits="TotalSummary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type = "text/javascript">
     function PrintPanel() {
         var panel = document.getElementById("<%=pnlContents.ClientID %>");
         var printWindow = window.open('', '', 'height=400,width=800');
         //       printWindow.document.write('<html><head><title>DIV Contents</title>');
         printWindow.document.write('</head><body >');
         printWindow.document.write(panel.innerHTML);
         printWindow.document.write('</body></html>');
         printWindow.document.close();
         setTimeout(function () {
             printWindow.print();
         }, 100);
         return false;
     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%" colspan="2"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;TOTAL SUMMARY
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
 
 <table align="center" width=40%>
 <tr>
 <td>
    <fieldset class="fontt">   
            <legend style="color: #3399FF">Total Summary </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
                     <td align="right">
                       &nbsp;<asp:Label ID="Label2" runat="server" Text="From"></asp:Label>      
                    </td>
                    <td  align="left">
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
                                <asp:TextBox ID="txt_FromDate" runat="server"  ></asp:TextBox>
                                 <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>

                            </td>
                </tr>  
                  <tr>
                     <td align="right">                   
                         <asp:Label ID="Label3" runat="server" Text="To"></asp:Label> 
                         <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>     
                    </td>
                    <td  align="left">                    	
                              <asp:TextBox ID="txt_ToDate" runat="server"  ></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
                    </td>
                </tr> 
                 <tr>
                     <td align="right"> 
             <asp:Label ID="Label1" runat="server" Text="Plant_Name"></asp:Label>      
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
                     <td  align="left">
         
                         &nbsp;</td>
                    <td  align="left">
                        &nbsp;</td>
                </tr>   
                 <tr>
                     <td>                   
         
             <asp:DropDownList ID="ddl_plantcode" 
        runat="server" Width="26px"    
        AutoPostBack="True" Enabled="False" Visible="False" Height="16px">
    </asp:DropDownList>

                    </td>
                    <td  align="left">                    	

     <asp:Button ID="btn_ok" runat="server"  CssClass="form93"   Font-Size="10px" Font-Bold="True"  
            Text="OK"  onclick="btn_ok_Click" /> 

                <asp:Button ID="Button9" runat="server" CssClass="form93" Font-Bold="True" 
                    Font-Size="10px" onclick="Button9_Click" OnClientClick="return PrintPanel();" 
                    Text="Print" />
                              
             <asp:Button ID="btn_Export" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="Export" Width="70px" Height="26px" onclick="btn_Export_Click" Visible="False" />

                    </td>
                </tr>  
                     
                
            </table>
            <br />
          
   </fieldset>
   </td>
    </tr>
   </table>
    
    <table width=100%> 
    <tr width=100%>
    <td align="center">
         <asp:Panel id="pnlContents" align="center" runat = "server">
    <asp:GridView ID="GridView1"
CssClass="table table-striped table-bordered table-hover" ShowFooter="True"
   runat="server" PageSize="20" Font-Size="Large" onrowcreated="GridView1_RowCreated" 
                                    onrowdatabound="GridView1_RowDataBound" 
            AutoGenerateColumns="False">
   <Columns>
									   <asp:TemplateField HeaderText="SNo.">
										   <ItemTemplate>
											   <%# Container.DataItemIndex + 1 %>
										   </ItemTemplate>
									   </asp:TemplateField>
								       <asp:BoundField DataField="Bank_name" HeaderText="Bankname" 
                                           SortExpression="Bank_name" />
                                       <asp:BoundField DataField="NetAmount" HeaderText="NetAmount" 
                                           SortExpression="NetAmount" />
								   </Columns>
                        <HeaderStyle ForeColor="#660066" />
                        <FooterStyle  ForeColor="#660066" Font-Bold=true/>
                    </asp:GridView>
                    </asp:Panel>
    </td>
    </tr>
    </table>




<br />
<center>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" 
        ToolPanelView="None" onunload="CrystalReportViewer1_Unload" />
        </center>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

