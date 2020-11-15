<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PlantDumpingTime.aspx.cs" Inherits="PlantDumpingTime" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
        }
        .style3
        {
            width: 100%;
            height: 49px;
        }
    </style>


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
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>







</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="form-style-9" style="height: auto">
<div class="form-style-3" style="height: auto">
<table width=100% style="height: 93px">

<tr  ALIGN=LEFT>
<td WIDTH="98%" style="height: auto">

<fieldset style="height: auto"  ><legend>Plant  Summary Report </legend>
    <table class="style1">
         <tr width="100%">
                <td align="center"  width: 15%">
                    <span>Date </span>
                    <asp:TextBox ID="txt_FromDate" runat="server"   Width="75px"></asp:TextBox>
                    <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                        Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                        TargetControlID="txt_FromDate">
                    </asp:CalendarExtender>
                </td>
                <td align="center"  width: 25%" >

    
                <asp:RadioButtonList ID="rdolist" runat="server" 
                    RepeatDirection="Horizontal" 
                        onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Value="1">AM</asp:ListItem>
                    <asp:ListItem Value="2">PM</asp:ListItem>
                </asp:RadioButtonList>

    
                    </td>
                <td align="center"  width: 25%">

    
                    <asp:CheckBox ID="rtoroute" runat="server" AutoPostBack="True" 
                        oncheckedchanged="rtoroute_CheckedChanged" Text="Route" />

    
                </td>
            </tr>
         <tr width="100%">
                <td align="center" style="width: 100%" width="50%" colspan="3">

    
                    <span>&nbsp;</span>Plant Name<asp:DropDownList ID="ddl_Plantname" runat="server" 
                               CssClass="tb10" Height="30px" 
                               onselectedindexchanged="ddl_charttype_SelectedIndexChanged" 
                     Width="200px">
                           </asp:DropDownList>
           
    
                </td>
            </tr>
         <tr width="100%">
                <td align="center" style="width: 100%" width="50%" colspan="3">

    
                    <table class="style1">
                        <tr align="center" >
                            <td class="style2">
           
    
                    &nbsp;<asp:Label ID="Label1" runat="server" Text="Route Name"></asp:Label>
           
    
                    <asp:DropDownList ID="ddl_route" runat="server" 
                               CssClass="tb10" Height="30px" 
                               onselectedindexchanged="ddl_charttype_SelectedIndexChanged" 
                     Width="200px">
                               <asp:ListItem Value="0">---SELECT----</asp:ListItem>
                               <asp:ListItem>PieChart</asp:ListItem>
                               <asp:ListItem>BarChart</asp:ListItem>
                               <asp:ListItem>LineChart</asp:ListItem>
                               <asp:ListItem>ColumnChart</asp:ListItem>
                               <asp:ListItem>AreaChart</asp:ListItem>
                               <asp:ListItem>SteppedAreaChart</asp:ListItem>
                           </asp:DropDownList>
           
    
                            </td>
                        </tr>
                    </table>
           
    
                </td>
            </tr>
         <tr width="100%">
                <td align="center" width="50%" colspan="3" class="style3">

    
                    <span>&nbsp;Chart Type </span>
                           <asp:DropDownList ID="ddl_charttype" runat="server" 
                               CssClass="tb10" Height="30px" 
                               onselectedindexchanged="ddl_charttype_SelectedIndexChanged" 
                     Width="200px">
                               <asp:ListItem Value="0">---SELECT----</asp:ListItem>
                               <asp:ListItem>PieChart</asp:ListItem>
                               <asp:ListItem>BarChart</asp:ListItem>
                               <asp:ListItem>LineChart</asp:ListItem>
                               <asp:ListItem>ColumnChart</asp:ListItem>
                               <asp:ListItem>AreaChart</asp:ListItem>
                               <asp:ListItem>SteppedAreaChart</asp:ListItem>
                           </asp:DropDownList>
           
    
                </td>
            </tr>
         <tr width="100%">
                <td align="center" style="width: 100%" width="50%" colspan="3">

    
               <asp:Button ID="Button5" runat="server" CssClass="form93" Font-Bold="False"   
                    Font-Size="X-Small"  xmlns:asp="#unknown"   
                    OnClientClick="return confirmationSave();" onclick="Button5_Click" Text="Get Report" 
                  TabIndex="6" />          
     <asp:Button ID="Button7" runat="server" CssClass="form93" Font-Bold="False"   
                    Font-Size="X-Small"  xmlns:asp="#unknown"    OnClientClick="return PrintPanel();" 
                    onclick="Button7_Click" Text="Print" 
                  TabIndex="6" />
           
    
               <asp:Button ID="Button8" runat="server" CssClass="form93" Font-Bold="False"   
                    Font-Size="X-Small"  xmlns:asp="#unknown"   
                    onclick="Button8_Click" Text="Export" 
                  TabIndex="6" />
           
    
                </td>
            </tr>
         </table>
 </fieldset>


</td>
 
</tr>

    </table>
</div>
    
           
</div> 
<table width=100% align="center">
<tr>
<td align="center">
    
                                               <asp:GridView ID="GridView2" runat="server" 
                               CssClass="gridcls" Font-Bold="True" 
                                                   ForeColor="White" 
        GridLines="Both" onrowcreated="GridView1_RowCreated" 
                                                   
        onrowdatabound="GridView1_RowDataBound" Width="60%" 
                               Font-Size="12px">

                                                   <EditRowStyle BackColor="#999999" />
                                                   <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
                                                   <HeaderStyle BackColor="#f4f4f4" Font-Bold="False" Font-Italic="False" 
                                                       Font-Names="Raavi" Font-Size="Small" ForeColor="Black" />
                                                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                   <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
                                                   <AlternatingRowStyle HorizontalAlign="Center" />
                                                   <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                   <Columns>
                                                   

                                                    
									   <asp:TemplateField HeaderText="SNo.">
										   <ItemTemplate>
											   <%# Container.DataItemIndex + 1 %>
										   </ItemTemplate>
									   </asp:TemplateField>
								 


                                                   
                                                   
                                                   </Columns>
                                               </asp:GridView>
    
    
</td>
</tr>
</table>
  
   <asp:Panel id="pnlContents" align="center" runat = "server">
     <div>
   
     <asp:Panel id="Panel1" runat = "server">
    
    <center>
    <div>
   
  
        <asp:Literal ID="lt" runat="server"  ></asp:Literal>
        <div id="chart_div" ></div>

          <asp:Literal ID="lt1" runat="server"  ></asp:Literal>
        <div id="chart_div1" ></div>
  </div>   
   
   </center>  
   </asp:Panel>
   </div>
    </asp:Panel>
          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
