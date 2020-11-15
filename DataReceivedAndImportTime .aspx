<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DataReceivedAndImportTime .aspx.cs" Inherits="DataReceivedAndImportTime_" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<head>

<style type="text/css">

 
        .Grid
        {
            border: 1px solid #525252;
        }
        .Grid th
        {
            border: 1px solid #525252;
        }
        .Grid th
        {
            color: #fff;
            background-color: #3AC0F2;
        }
        </style>
           <style type="text/css">

    .disnone
    {
        display:none;
    }
        .serh-grid
        {
            width: 85%;
            border: 1px solid #6AB5FF;
            background: #fff;
            line-height: 14px;
            font-size: 11px;
            font-family: Verdana;
        }
        
         .grid-sltrow
        {
            background: #ddd;
            font-weight: bold;
        }
        .SubTotalRowStyle
        {
            border: solid 1px Black;
            background-color: #D8D8D8;
            font-weight: bold;
        }
        .GrandTotalRowStyle
        {
            border: solid 1px Gray;
            background-color: #000000;
            color: #ffffff;
            font-weight: bold;
        }
        .GroupHeaderStyle
        {
            border: solid 1px Black;
            background-color: #4682B4;
            color: #ffffff;
            font-weight: bold;
        }
        .serh-grid
        {
            width: 85%;
            border: 1px solid #6AB5FF;
            background: #fff;
            line-height: 14px;
            font-size: 11px;
            font-family: Verdana;
        }
         .modalPopup
{
background-color: #FFFFFF;
filter: alpha(opacity=40);
opacity: 0.7;
xindex:-1;
}
        
               .style2
               {
                   font-family: Andalus;
                   font-size: medium;
                   color: #000000;
               }
        
               .style3
               {
                   width: 100%;
                  
               }
         .styleCLASSFONT
               {
                   width: 40%;
                   text-align:center;
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

</head>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server" />
 <%-- <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:transparent; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" 
                AlternateText="Loading ..." ToolTip="Loading ..." 
                
                
                style="padding: 10px;background-color:yellowgreen; position:fixed;top:0%; left:50%; right: 451px;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>   --%>


<asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height: 1%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>


 <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
  <div  ALIGN=center>
   <fieldset class=styleCLASSFONT>
            <legend style="color: #3399FF">DataReceivedImportDetails </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
                     <td align="right">
                         <asp:Label ID="Label5" runat="server" Text="Plant Name" CssClass="style2"></asp:Label>
                    </td>
                    <td  align="left">
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="BottomRight">
                               </asp:CalendarExtender>
                                <asp:DropDownList ID="ddl_PlantName" runat="server" Width="172px" Height="30px">
                                </asp:DropDownList>

                            </td>
                    <td align="LEFT">
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" 
                            PopupButtonID="txt_ToDate" PopupPosition="BottomRight" 
                            TargetControlID="txt_ToDate">
                        </asp:CalendarExtender>
                    </td>
                    <td align="left" >
                  
                        &nbsp;</td>
                </tr>  
                <tr>
                    <td align="right">
                        <asp:Label ID="Label2" runat="server" CssClass="style2" Text="From"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_FromDate" runat="server" Enabled="False" Height="25px" 
                            style="font-size: small; font-family: Andalus" Width="170px"></asp:TextBox>
                    </td>
                    <td align="LEFT">
                        &nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                </tr>
                 <tr>
                    <td align="right">                                    
                        <asp:Label ID="Label3" runat="server" CssClass="style2" Text="To"></asp:Label>
                    </td>
                    <td  align="left">
                  
                        <asp:TextBox ID="txt_ToDate" runat="server" Enabled="False" Height="25px" 
                            style="font-size: small; font-family: Andalus" Width="170px"></asp:TextBox>
                  
                    </td>
                </tr> 
            <tr>
                    <td align="left">                                    
                        &nbsp;</td>
                    <td align="left">
                    	
                        <asp:Button ID="btn_ok" runat="server" BackColor="#00CCFF" BorderStyle="Double" 
                            CssClass="button2222" Font-Bold="True" ForeColor="White" Height="30px" 
                            onclick="btn_ok_Click" Text="OK" />
                        <asp:Button ID="btn_print" runat="server" BackColor="#00CCFF" 
                            BorderStyle="Double" CssClass="button2222" Font-Bold="True" ForeColor="White" 
                            Height="30px" OnClientClick="return PrintPanel();" Text="Print" />
                    	
                    </td>
                </tr>   
                     
                
            </table>
            
            <br />
          
   </fieldset>
     
   </div>  
    <div  align="center">
    <asp:Panel id="pnlContents" runat = "server"> 
        <table class="style3">
            <tr>
                <td align="right" colspan="2">
                    &nbsp;</td>
            </tr>
             <tr  valign="top">
                <td align="right">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        CssClass="gridview2" Font-Size="14px" onrowcreated="GridView1_RowCreated" 
                        PageSize="35"     >
                        <HeaderStyle Font-Bold="True" ForeColor="White"/>
                      


                        <Columns>
                            <asp:TemplateField HeaderText="SNo.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="pdate" HeaderText="ProureDate" 
                                SortExpression="pdate" />
                            <asp:BoundField DataField="Sess" HeaderText="Sess" SortExpression="Sess" />
                            <asp:BoundField DataField="Rdate" HeaderText="ReceivedData" 
                                SortExpression="Rdate" />
                        </Columns>
                    </asp:GridView>
                </td>
                <td align="left">
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                        CssClass="gridview2" Font-Size="14px" onrowcreated="GridView2_RowCreated" 
                        PageSize="35" onselectedindexchanged="GridView2_SelectedIndexChanged">
                        <HeaderStyle Font-Bold="True" ForeColor="White"/>
                        <Columns>
                            <asp:TemplateField HeaderText="SNo.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="pdate" HeaderText="ProcureDate" 
                                SortExpression="pdate" />
                            <asp:BoundField DataField="Sess" HeaderText="Session" SortExpression="Sess" />
                            <asp:BoundField DataField="Rdate" HeaderText="ImportDate" 
                                SortExpression="Rdate" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    <br />
        
    </asp:Panel>
            <br />
           
             
         </div> 
          </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

