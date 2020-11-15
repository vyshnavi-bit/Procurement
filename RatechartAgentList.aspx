<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RatechartAgentList.aspx.cs" Inherits="RatechartAgentList" %>
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
               .style1
               {
                   font-family: Andalus;
                   font-size: medium;
                   color: #000000;
               }
               .style2
               {
                   height: 17px;
               }
               .style3
               {
                   font-family: Andalus;
                   font-size: medium;
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
  <%--<asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height: 10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:transparent; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" 
                AlternateText="Loading ..." ToolTip="Loading ..." 
                
                style="padding: 10px;position:fixed; background-color:yellowgreen;  top:0%; left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>  --%> 

<asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height: 10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>



 <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
  <div class="legagentsms">
   <fieldset class="fontt">   
            <legend style="color: #3399FF">PlantRatechartuseage </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
            <td class="style2">
                 </td>
            </tr>
             <tr>
                    <td>
                        &nbsp;</td>
                     <td align="right">
                         <asp:Label ID="Label5" runat="server" Text="Plant Name" CssClass="style1"></asp:Label>
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
                     <td align="right">                   
                         <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"  >
                                   </asp:CalendarExtender>     
                         <asp:Label ID="Label2" runat="server" Text="From" CssClass="style1"></asp:Label>
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                              <asp:TextBox ID="txt_FromDate" runat="server" Height="25px" 
                                  style="font-family: Andalus; font-size: small" Width="170px"></asp:TextBox>
                    </td>
                </tr> 
                 <tr>
                    <td>                                    
                    </td>
                     <td align="right"> 
                         <asp:Label ID="Label3" runat="server" Text="To" CssClass="style1"></asp:Label>
                    </td>
                    <td  >                  
                        &nbsp;</td>
                    <td  align="left"> 
                         <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="BottomRight">
                        </asp:CalendarExtender>                   	
                    	 <asp:TextBox ID="txt_ToDate" runat="server" 
                             style="font-size: small; font-family: Andalus" Width="170px"></asp:TextBox>

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
                                           
                        <asp:CheckBoxList ID="Chk_ratechartemode" runat="server" 
                            RepeatDirection="Horizontal" CssClass="style1">
                            <asp:ListItem>Plant</asp:ListItem>
                            <asp:ListItem>Route</asp:ListItem>
                            <asp:ListItem>Agent</asp:ListItem>
                        </asp:CheckBoxList>
                                           
                      </td>
                </tr> 
            <tr>
                    <td>
                    	
                    </td>
                     <td  align="right">
         
                         &nbsp;</td>
                    <td >                         
                    </td>
                    <td  align="left">

                        <asp:Button ID="btn_ok" runat="server" BackColor="#00CCFF" BorderStyle="Double" 
                            Font-Bold="True" ForeColor="White" onclick="btn_ok_Click" Text="Load" 
                            CssClass="button2222" Height="30px" />
                        <asp:Button ID="btn_print" runat="server" BackColor="#00CCFF" 
                            BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="30px" 
                            OnClientClick="return PrintPanel();" Text="Print" CssClass="button2222" />

                        <asp:Button ID="btn_okPRINT" runat="server" BackColor="#00CCFF" 
                            BorderStyle="Double" CssClass="button2222" Font-Bold="True" ForeColor="White" 
                            Height="30px" onclick="btn_okPRINT_Click" Text="Load" />

                    </td>
                               <td width="12%">
                                
                    </td>
                </tr>   
                 <tr>
                    <td>                                    
                    </td>
                     <td align="right">                   
         
                         <asp:Label ID="Label6" runat="server" Text="Ratechart Name" CssClass="style1"></asp:Label>
                     </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                        <asp:DropDownList ID="ddl_RateChartName" runat="server" Width="172px" Height="30px">
                        </asp:DropDownList>
                        <asp:Button ID="btn_Get" runat="server" BackColor="#00CCFF" 
                            BorderStyle="Double" Font-Bold="True" ForeColor="White"  
                            Text="Get" onclick="btn_Get_Click" CssClass="button2222" Height="30px" />
                     </td>
                </tr>  
                     
                
            </table>
            
            <br />
          
   </fieldset>
   <asp:Label ID="Lbl_Errormsg" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
  
   </div>  
    <div  align="center">
    <asp:Panel id="pnlContents" runat = "server"> 

   <table >
   <tr> 
   <td style='text-align:left;vertical-align:top'>
   <div align="right">
       <asp:Image ID="Image1"  runat="server" ImageUrl="~/Image/VLogo.png" Width="50%" Height="50%" />
   </div>
   </td>
   <td style='text-align:left;vertical-align:top'>
 
   <div align="center">
    <asp:Label ID="Label8" runat="server" Font-Size="Large" ForeColor="Green" 
           CssClass="style3" ></asp:Label>
    <asp:Label ID="Label4" runat="server" Font-Size="Large" ForeColor="Green" 
           CssClass="style3" ></asp:Label>
     <div align="center">
   <asp:Label ID="Label7" runat="server" Font-Size="Large" ForeColor="Green" 
             CssClass="style3" ></asp:Label>
   </div>
   <div align="center">
   <asp:Label ID="Label1" runat="server" Font-Size="Large" ForeColor="Green" 
           CssClass="style3" ></asp:Label>   
   </div>      
    <asp:GridView ID="GridView1" runat="server" Font-Size="13px" Font-Bold="true"  
           HeaderStyle-BackColor="Silver" AllowPaging="true" PageSize="25" 
           onpageindexchanging="GridView1_PageIndexChanging">
       </asp:GridView>
       </div>
   </td>
  
   </tr>
   </table>      
        
    </asp:Panel>
            <br />
           
             
         </div> 
          </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>


