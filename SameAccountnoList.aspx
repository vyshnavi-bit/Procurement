<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SameAccountnoList.aspx.cs" Inherits="SameAccountnoList" %>


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
        .fieldset
        {
            
            width:60%;
        }
             .style15
    {
        width:60%;
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
        
               #table4
               {
                   width: 96%;
               }
               
                .ddl
        {
            border:2px solid #7d6754;
            border-radius:5px;
            padding:3px;
            -webkit-appearance: none; 
            background-image:url('Images/Arrowhead-Down-01.png');
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            text-overflow: '';/*In Firefox*/
        }
               
               
               
               
                  
         .textbox { 
    background: #FFF url(http://html-generator.weebly.com/files/theme/input-text-9.png) no-repeat 4px 4px; 
    border: 1px solid #999; 
    outline:0; 
    padding-left: 25px;
    height:25px; 
    width: 275px; 
  } 
        
        
        inputs:-webkit-input-placeholder {
    color: #b5b5b5;
}

inputs-moz-placeholder {
    color: #b5b5b5;
}

.inputs {
    outline: none;
    display: block;
    width: 350px;
    padding: 4px 8px;
    border: 1px dashed #DBDBDB;
    color: #3F3F3F;
    font-family: Andalus;
    font-size: medium;
    -webkit-border-radius: 2px;
    -moz-border-radius: 2px;
    border-radius: 2px;
    -webkit-transition: background 0.2s linear, box-shadow 0.6s linear;
    -moz-transition: background 0.2s linear, box-shadow 0.6s linear;
    -o-transition: background 0.2s linear, box-shadow 0.6s linear;
    transition: background 0.2s linear, box-shadow 0.6s linear;
}

.inputs:focus {
    background: #F7F7F7;
    border: dashed 1px #969696;
    -webkit-box-shadow: 2px 2px 7px #E8E8E8 inset;
    -moz-box-shadow: 2px 2px 7px #E8E8E8 inset;
    box-shadow: 2px 2px 7px #E8E8E8 inset;
}

.buttonclass
{
padding-left: 10px;
font-weight: bold;
}
.buttonclass:hover
{
color: white;
background-color:Orange;
}


.columnscss
{
width:25px;
font-weight:bold;
font-family:Verdana;
}



        
.ddlstyle 
{
color:rgb(33,33,00);
Font-Family:Book Antiqua;
font-size:12px;
vertical-align :middle;
}
        
 
        
         .ddl2
        {
            border:2px solid #7d6754;
            border-radius:5px;
            padding:3px;
            -webkit-appearance: none; 
            background-image:url('Images/Arrowhead-Down-01.png');
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            text-overflow: '';
            font-size: medium;
        }
        
        
        

                       
               
               
        
               .style17
               {
                   color: #800000;
                   font-weight: bold;
                   font-family: Andalus;
                   font-size: medium;
               }
        
        
        

                       
               
               
        
               </style>
 <%--    <script type = "text/javascript">
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
    </script>--%>

</head>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server" />


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
  <div>
  <br />
  <center>
<%--   <fieldset class="fieldset" width="55%" style="padding: inherit; border-style: inset">  --%>
     <fieldset  class="style15" style="background-color: #CCFFFF" style="padding: inherit; border-style: inset">   
            <legend style="color: #3399FF; width: 252px; font-family: Andalus;">Agent Same Account Number</legend>


            <table id="table4" width=100% cellspacing="1" align="center">    
                     
                
                    <tr>
                        <td  align="center" width="30%" colspan="2" style="width: 60%">
                        <asp:Label ID="Label7" runat="server" CssClass="style2" Text="From"></asp:Label>
                        <asp:TextBox ID="txt_FromDate" runat="server" Font-Size="Small" Height="20px" 
                            Width="125px"></asp:TextBox>
                        <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                            Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="BottomRight"
                            TargetControlID="txt_FromDate">
                        </asp:CalendarExtender>
                        <asp:CalendarExtender ID="txt_FromDate_CalendarExtender2" runat="server" 
                            Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="BottomRight" 
                            TargetControlID="txt_FromDate">
                        </asp:CalendarExtender>
                        <asp:Label ID="Label8" runat="server"  Text="To" 
                            style="font-size: medium; font-family: Andalus"></asp:Label>
                        <asp:TextBox ID="txt_ToDate" runat="server" Font-Size="Small" Height="20px" 
                            Width="125px"></asp:TextBox>
                        <asp:CalendarExtender ID="txt_ToDate_CalendarExtender" runat="server" 
                            Format="dd/MM/yyyy" PopupButtonID="txt_ToDate"  PopupPosition="BottomRight" 
                            TargetControlID="txt_ToDate">
                        </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="25%">
                            <right>
                                <asp:Label ID="Label5" runat="server" 
                                    style="font-size: medium; font-family: Andalus" Text="Plant Name"></asp:Label>
                            </right>
                        </td>
                        <td align="left" width="25%">
                            <asp:DropDownList ID="ddl_Plantname" runat="server" 
                                Font-Size="Small" Height="30px" 
                                onselectedindexchanged="ddl_PlantName_SelectedIndexChanged" Width="175px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="30%">
                            <asp:Label ID="Label111" runat="server" 
                                style="font-family: Andalus; font-size: medium" Text="Agent Type"></asp:Label>
                        </td>
                        <td align="left" width="30%">
                            <asp:DropDownList ID="ddl_Agentname" runat="server" Font-Size="Small" 
                                Height="30px" Width="175px">
                                <asp:ListItem Value="0">-----------Select---------------</asp:ListItem>
                                <asp:ListItem Value="1">AccountName</asp:ListItem>
                                <asp:ListItem Value="2">AccountNumber</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="Button1" runat="server" BackColor="#00CCFF" 
                                BorderStyle="Double" CssClass="button2222" Font-Bold="True" ForeColor="White" 
                                Height="30px" OnClientClick="return PrintPanel();" Text="Print" 
                                Visible="False" />
                            <asp:Button ID="Button2" runat="server" BackColor="#00CCFF" 
                                BorderStyle="Double" CssClass="button2222" Font-Bold="True" ForeColor="White" 
                                Height="30px" OnClientClick="return PrintPanel();" Text="Print" 
                                Visible="False" />
                        </td>
                    </tr>
                    <tr align="center" width="50%">
                        <td colspan="2">
                            <asp:DropDownList ID="ddl_plantcode" runat="server" Height="23px" 
                                Visible="False" Width="47px">
                            </asp:DropDownList>
                            <asp:Button ID="btn_ok" runat="server" CssClass="buttonclass" 
                                onclick="btn_ok_Click" Text="OK" />
                            <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Export" 
                                CssClass="buttonclass" />
                           <%-- <asp:Button ID="btn_print" runat="server" CssClass="buttonclass" 
                                onclick="btn_print_Click1" OnClientClick="return PrintPanel();" Text="Print" />--%>
                        </td>
                    </tr>
                </caption>
                     
                
            </table>
            
            <br />
          
   </fieldset>
     </center>
   </div>  
    <div  align="center">

  

<%--
    <asp:Panel id="pnlContents" runat = "server"> --%>

   <table >
   <tr> 
   <td style='text-align:left;vertical-align:top'>
   <div align="right">
       <br />
   </div>
    </td>
   <td style='text-align:center;vertical-align:top'>
       <asp:Label ID="Lbl_msg" runat="server" Text="Label" CssClass="style17"></asp:Label>
       <br />
       <asp:Label ID="Lbl_msg11" runat="server" Text="Label" CssClass="style17"></asp:Label>
       <br />
       <asp:GridView ID="GridView1" runat="server"  Font-Size="14px" PageSize="100" 
           AutoGenerateColumns="False" HeaderStyle-BackColor="Brown" 
           onrowcreated="GridView1_RowCreated" onrowdatabound="GridView1_RowDataBound" >
        <Columns>
               <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno">
                   <HeaderStyle HorizontalAlign="Center" />
                   <ItemStyle HorizontalAlign="Left" />
               </asp:BoundField>
               <asp:BoundField DataField="AgentId" HeaderText="AgentId" 
                   SortExpression="AgentId">
                   <HeaderStyle HorizontalAlign="Center" />
                   <ItemStyle HorizontalAlign="Left" Width="80px" />
               </asp:BoundField>
               <asp:BoundField DataField="AgentName" HeaderText="AgentName" 
                   SortExpression="AgentName">
                     <ControlStyle Width="250px" />
                   <HeaderStyle HorizontalAlign="Center" />
                   <ItemStyle HorizontalAlign="Left" Width="250px" />
               </asp:BoundField>
               <asp:BoundField DataField="AgentAccountNo" HeaderText="AccountNo" 
                   SortExpression="AgentAccountNo">
                    <ControlStyle Width="150px" />
                    <FooterStyle Width="150px" />
                   <HeaderStyle HorizontalAlign="Center" Width="150px" />
                   <ItemStyle HorizontalAlign="Left" Width="150px" />
               </asp:BoundField>
               <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount">
                   <HeaderStyle HorizontalAlign="Center" />
                   <ItemStyle HorizontalAlign="Left" Width="80px" />
               </asp:BoundField>
               <asp:BoundField HeaderText="Remarks">
                   <ControlStyle Width="500px" />
                   <HeaderStyle HorizontalAlign="Center" />
                   <ItemStyle HorizontalAlign="Left" Width="350px" />
               </asp:BoundField>
           </Columns>





<HeaderStyle BackColor="Brown"></HeaderStyle>





       </asp:GridView>
   </td>
   <td style='text-align:left;vertical-align:top'>
       <br />
   </td>
   </tr>
   </table>      
        
    <%--</asp:Panel>--%>
            <br />
          
             
         </div> 
   
                </ContentTemplate>
           <Triggers>
<asp:PostBackTrigger ControlID="Button3" />
</Triggers>
        </asp:UpdatePanel>

</asp:Content>
