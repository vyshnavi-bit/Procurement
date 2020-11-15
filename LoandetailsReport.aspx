<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LoandetailsReport.aspx.cs" Inherits="LoandetailsReport" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<head>

<style type="text/css">

 
        .Grid
        {
            border: 1px solid #525252;
        font-family: Andalus;
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
        
               .gridview1
               {
                   font-family: Andalus;
               }
               .gridview2
               {
                   font-family: Arial;
                   font-size: small;
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
 <div style="position: fixed; text-align: center; height: 10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>


 <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
  <div class="legagentsms">
  <br />
   <fieldset class="fontt">   
            <legend style="color: #3399FF">Loan Deatils Reports</legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
            <td>
                &nbsp;</td>
            </tr>
             <tr>
                    <td>
                        &nbsp;</td>
                     <td align="right" style="font-family: Arial">
                         <asp:Label ID="Label5" runat="server" Text="Plant Name" CssClass="style2"></asp:Label>
                    </td>
                    <td >
                  
                    </td>
                    <td  align="left">
                                <asp:DropDownList ID="ddl_PlantName" runat="server" Width="172px" Height="30px">
                                </asp:DropDownList>

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

                        <asp:Button ID="btn_ok" runat="server" BackColor="#00CCFF" BorderStyle="Double" 
                            Font-Bold="True" ForeColor="White" onclick="btn_ok_Click" Text="OK" 
                            CssClass="button2222" Height="30px" />
           <asp:Button ID="btn_print" runat="server" BackColor="#00CCFF" ForeColor="White" Text="Print" 
                            Height="30px" BorderStyle="Double" Font-Bold="True" 
                            OnClientClick = "return PrintPanel();" CssClass="button2222" 
                            onclick="btn_print_Click"  />

                        <asp:Button ID="Button2" runat="server" BackColor="#00CCFF" 
                            BorderStyle="Double" CssClass="button2222" Font-Bold="True" ForeColor="White" 
                            Height="30px" onclick="Button2_Click" Text="Export" />

                    </td>
                               <td width="12%">
                                 
                    </td>
                </tr>   
                     
                
            </table>
            
            <br />
          
   </fieldset>
     
   </div>  
    <div  align="center">
    <asp:Panel id="pnlContents" runat = "server"> 

   <table >
   <tr> 
   <td style='text-align:left;vertical-align:top'>
   <div align="right">
   </div>
    </td>
   <td style='text-align:center;vertical-align:top'>
       <asp:Label ID="Label6" runat="server" Text="Loan Deatails" 
           style="font-family: Andalus"></asp:Label>
       <br />
       <asp:GridView ID="GridView1" runat="server" CssClass="gridview2" 
           Font-Size="14px" onrowdatabound="GridView1_RowDataBound">
       </asp:GridView>
   </td>
   <td style='text-align:left;vertical-align:top'>
       <br />
   </td>
   </tr>
   </table>      
        
    </asp:Panel>
            <br />
           
             
         </div> 
          </ContentTemplate>

          <Triggers>
          
          <asp:PostBackTrigger ControlID="Button2" />
          
          </Triggers>

        </asp:UpdatePanel>
     
</asp:Content>

