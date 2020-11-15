<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="DashBoard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
<script type="text/javascript" language="JavaScript" src="FusionCharts/FusionCharts.js"></script>
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
      <script type="text/javascript" language="JavaScript">

          function myJS(myVar) {
              window.alert(myVar);
          }
      
      </script>
      <link id="Link1"  rel="Stylesheet" type="text/css" href="SampleStyleSheet1.css" runat="server" media="screen" />
  <style type="text/css">

.perdaymilkstatus
{
    background-color:#FFFAFA;
    margin-left:5px;
    
}
 .DText
    {
width: 110px;
border-radius: 5px;
padding: 2px;

}

    .modalBackground1
{
background-color: White;
}

.modalPopup1
{
background-color: #FFFFFF;
filter: alpha(opacity=40);
opacity: 0.7;
xindex:-1;
}

/*----------------data Table GridView Css----------------------*/
        .EU_TableScroll
        {
      /*----------max-height: 300px;-------------------*/       
            overflow: auto;
            border:1px solid #ccc;
           
        }
        .EU_DataTable
        {
            border-collapse: collapse;
            width:100%;
        }
            .EU_DataTable tr th
            {
                background-color: #3c454f;
                color: #ffffff;
                padding: 10px 5px 10px 5px;
                border: 1px solid #cccccc;
                font-family: Arial, Helvetica, sans-serif;
                font-size: 12px;
                font-weight: normal;
                text-transform:capitalize;
            }
            .EU_DataTable tr:nth-child(2n+2)
            {
                background-color: #f3f4f5;
            }

            .EU_DataTable tr:nth-child(2n+1) td
            {
                background-color: #d6dadf;
                color: #454545;
            }
            .EU_DataTable tr td
            {
                padding: 5px 10px 5px 10px;
                color: #454545;
                font-family: Arial, Helvetica, sans-serif;
                font-size: 11px;
                border: 1px solid #cccccc;
                vertical-align: middle;
            }
                .EU_DataTable tr td:first-child
                {
                    text-align: left;
                }
        /*-----------------------------------------------------------------*/
        /*----------------Agent data Table GridView Css----------------------*/
        .EUA_TableScroll
        {
      /*----------max-height: 300px;-------------------*/       
            overflow: auto;
            border:1px solid #ccc;
            max-height: 500px;
           
        }
        .EUA_DataTable
        {
            border-collapse: collapse;
            width:100%;
        }
            .EUA_DataTable tr th
            {
                background-color: #3c454f;
                color: #ffffff;
                padding: 10px 5px 10px 5px;
                border: 1px solid #cccccc;
                font-family: Arial, Helvetica, sans-serif;
                font-size: 12px;
                font-weight: normal;
                text-transform:capitalize;
            }
            .EUA_DataTable tr:nth-child(2n+2)
            {
                background-color: #f3f4f5;
            }

            .EUA_DataTable tr:nth-child(2n+1) td
            {
                background-color: #d6dadf;
                color: #454545;
            }
            .EUA_DataTable tr td
            {
                padding: 5px 10px 5px 10px;
                color: #454545;
                font-family: Arial, Helvetica, sans-serif;
                font-size: 11px;
                border: 1px solid #cccccc;
                vertical-align: middle;
            }
                .EUA_DataTable tr td:first-child
                {
                    text-align: left;
                }
        /*-----------------------------------------------------------------*/
        .modalBackground
    {
        background-color: #3c454f;
        filter: alpha(opacity=60);
        opacity: 0.6;
    }
    .modalPopup
    {
        background-color: #FFFFFF;       
        border: 3px solid #0DA9D0;
        border-radius: 12px;
        padding:0
      
    }
    .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        border-top-left-radius: 6px;
        border-top-right-radius: 6px;
    }
    .modalPopup .body
    {
        min-height: 50px;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
    }
    .modalPopup .footer
    {
        padding: 6px;
    }
    .modalPopup .yes, .modalPopup .no
    {
        height: 23px;
        color: White;
        line-height: 23px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
        border-radius: 4px;
    }
    .modalPopup .yes
    {
        background-color: #2FBDF1;
        border: 1px solid #0DA9D0;
    }
    .modalPopup .no
    {
        background-color: #9F9F9F;
        border: 1px solid #5C5C5C;
    }
 
      #chart_div
      {
          width: 293px;
      }
 
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
    
   

    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 10%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: Gray; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..."
                    ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 38%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

 <div class="perdaymilkstatus">    
 <fieldset class="fontt">
<legend style="color: #3399FF">Dash Board</legend>             
                 
<table border="0"  id="Agenttable" cellspacing="1" align="center">
 <tr>
                    <td valign="top" >
                    <table >
                    <tr>
                    <td align="center">
                     <asp:RadioButtonList ID="rdocheck" runat="server" AutoPostBack="True"                                        
                                        RepeatDirection="Horizontal" 
                            onselectedindexchanged="rdocheck_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Sessions</asp:ListItem>
                                        <asp:ListItem Value="2">PerDay</asp:ListItem>
                                        <asp:ListItem Value="3">Period</asp:ListItem>
                                    </asp:RadioButtonList>
                    </td>
                    </tr>
                    <tr align="center">
                                <td >
                                    <asp:Label ID="lbl_Sess" runat="server" Text="Session"></asp:Label>
                                    <asp:DropDownList ID="ddl_Sessions" runat="server" 
                                        Width="60px">                                      
                                        <asp:ListItem Value="1">AM</asp:ListItem>
                                        <asp:ListItem Value="2">PM</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                    <tr>
                    <td  align="center">                     
                      <asp:RadioButtonList ID="Rdl_Plantype" runat="server" RepeatDirection="Horizontal" 
                                         AutoPostBack="True" onselectedindexchanged="Rdl_Plantype_SelectedIndexChanged">
                                      <asp:ListItem Value="3">ALL</asp:ListItem>
                                      <asp:ListItem Value="1">Cow</asp:ListItem>
                                      <asp:ListItem Value="2">Buffalo</asp:ListItem>
                                    </asp:RadioButtonList>
                    </td>
                    </tr>
                    <tr>
<td  align="center">
<asp:Label ID="Label4" runat="server" Text="From"></asp:Label> 
                <asp:TextBox ID="txt_frmdate" runat="server" CssClass="DText"   
                    ></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_frmdate"
                    PopupButtonID="txt_frmdate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                </asp:CalendarExtender>              
            <asp:Label ID="Label5" runat="server" Text="To"></asp:Label>  
                   <asp:TextBox ID="txt_todate" runat="server" CssClass="DText"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_todate"
                    PopupButtonID="txt_todate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                </asp:CalendarExtender>   
                <asp:Button ID="btn_Generate" runat="server" BackColor="#00CCFF" 
                  BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px"  Visible="true"  
                  Text="Generate" onclick="btn_Generate_Click"  /> 
</td>
</tr>
<tr>
                    <td align="center" style="border-width: thin; border-style: inset">                    
                        <div align="center">
                            <asp:Panel ID="Panel3" runat="server">
                                <asp:GridView ID="gv_PlantMilkDetail" runat="server" AutoGenerateColumns="true" 
                                    AutoGenerateSelectButton="false" Font-Size="12px" 
                                    HeaderStyle-BackColor="Silver" 
                                    OnSelectedIndexChanging="gv_PlantMilkDetail_SelectedIndexChanged" 
                                    CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:CommandField ButtonType="Button" SelectText="Route" 
                                            ShowSelectButton="True" />
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                            </asp:Panel>
                        </div>
                        </td>
                    </tr>
<tr>
<td>
<div align="center">
  <asp:Label ID="Lbl_PlantName" runat="server" Font-Bold="true" Font-Size="Large"  ForeColor="Red" Text="PlantName :"></asp:Label>
  <asp:Label ID="lbl_Plantcode" runat="server" Font-Bold="true" Font-Size="Large" Font-Underline="true" ForeColor="Red"></asp:Label>
  <div>
  <asp:Label ID="lbl_Milkkg" runat="server" Font-Bold="true" Font-Size="Large" Font-Underline="true" ForeColor="Green"></asp:Label>
  </div>
  </div>
</td>
</tr>
               <tr valign="top">
                    <td  align="center" style="border-width: thin; border-style: inset">
                         
                            <asp:GridView ID="gv_PlantRouteMilkDetail" runat="server"
                                Font-Size="12px" HeaderStyle-BackColor="Silver"  
                                
                                onrowdatabound="gv_PlantRouteMilkDetail_RowDataBound" 
                                onselectedindexchanging="gv_PlantRouteMilkDetail_SelectedIndexChanging" 
                                CellPadding="4" ForeColor="#333333" GridLines="None" >
                                 
                                
                                <AlternatingRowStyle BackColor="White" />
                                 
                                
                                <Columns>
                                    <asp:CommandField ButtonType="Button" SelectText="Agent" 
                                        ShowSelectButton="True" />
                                </Columns>
                                 
                                
                                <EditRowStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                 
                                
                            </asp:GridView>
                    </td>
                </tr>
                <tr valign="top">
                <td align="center" style="border-width: thin; border-style: inset">
                               
                    <asp:GridView ID="gv_PlantAgentMilkDetail1" runat="server" 
                      Font-Size="12px" HeaderStyle-BackColor="Silver" 
                        onrowdatabound="gv_PlantAgentMilkDetail_RowDataBound" 
                        onselectedindexchanged="gv_PlantAgentMilkDetail_SelectedIndexChanged" 
                        CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </td>
                </tr>
                <tr>
                <td align=center>
                <div>
        <asp:Literal ID="LtLine" runat="server" Visible="False"></asp:Literal>
    </div>   
    <div id="chart_divLine"></div>    

    <div>
      <asp:Literal ID="FCLiteralLine" runat="server" Visible="False"></asp:Literal>
    </div>
             
                </td>
                </tr>
                <tr>
                <td  valign="top" class="text" align="center">
                  <asp:Literal ID="FCLiteral1" runat="server" Visible="False"></asp:Literal>
                </td>
                </tr>
                 <tr>
                <td  valign="top" class="text" align="center">
                  <asp:Literal ID="FCLiteral2" runat="server" Visible="False"></asp:Literal>
                </td>
                </tr>
                 <tr>
                <td  valign="top" class="text" align="center">
                  <asp:Literal ID="FCLiteral3" runat="server" Visible="False"></asp:Literal>
                </td>
                </tr>
                <tr>
                <td valign="top" class="text" align="center">
                 <table>
            <tr>
            <td>
             <div>
        <asp:Literal ID="lt" runat="server" Visible="False"></asp:Literal>
    </div>
    <div id="chart_div">
    </div> 
            </td>
            </tr>
            </table>
                </td>
                </tr>
            </table>
             </fieldset>
             </div> 
                    </td>
                </tr>
            </table>
               <div align="center">
                <asp:Label ID="Lbl_Errormsg" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
            </div>


            


              </ContentTemplate>
    </asp:UpdatePanel>
    </asp:Content>
    