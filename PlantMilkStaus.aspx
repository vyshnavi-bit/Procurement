<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="PlantMilkStaus.aspx.cs" Inherits="PlantMilkStaus" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">

.perdaymilkstatus
{
    background-color:#FFFAFA;
    margin-left:80px;
    width:700px;
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
                    text-align: center;
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
                    text-align: center;
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
 
</style>
<script type="text/javascript">
    function PrintPanels() {
        var panel = document.getElementById("<%=pnlpopup.ClientID %>");
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                <table border="0" width="100%" id="table1" cellspacing="1" align="center">
                    <tr valign="top">
                        <td >
                            <fieldset class="fontt">
                                <legend style="color: #3399FF">Plant MilkStatus </legend>
                                <table border="0" width="100%" id="table2" cellspacing="1" align="center">                                
                                <tr>
                                <td width="10%">
                                <asp:Label ID="Label2" runat="server" Text="From"></asp:Label>
                                </td>
                                  <td width="10%">
                                  <asp:TextBox ID="txt_FromDate" runat="server" CssClass="DText" Enabled="true"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="txt_FromDate"
                                                PopupPosition="BottomRight" TargetControlID="txt_FromDate">
                                            </asp:CalendarExtender>
                                </td>
                                <td width="10%">
                                <asp:Label ID="Label3" runat="server" Text="To"></asp:Label>
                                </td>
                                 <td width="10%">
                                  <asp:TextBox ID="txt_ToDate" runat="server" CssClass="DText" Enabled="true"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" PopupButtonID="txt_ToDate"
                                                PopupPosition="BottomRight" TargetControlID="txt_ToDate">
                                            </asp:CalendarExtender>
                                 </td>
                                 <td width="30%">
                                  <asp:RadioButtonList ID="Rdl_Plantype" runat="server" RepeatDirection="Horizontal" 
                                         AutoPostBack="True" onselectedindexchanged="Rdl_Plantype_SelectedIndexChanged">
                                      <asp:ListItem Value="3">ALL</asp:ListItem>
                                      <asp:ListItem Value="1">Cow</asp:ListItem>
                                      <asp:ListItem Value="2">Buffalo</asp:ListItem>
                                    </asp:RadioButtonList>
                                    </td>
                                    <td "30%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                <td></td>
                                </tr>

                                <tr >
                                <td colspan="6">
                                
                                 
                                </td>
                                </tr>                                  
                                 
                                </table>
                               
                                <br />
                            </fieldset>
                        </td>
                        
                    </tr>
                </table>       
              </div>
            <div class="perdaymilkstatus">
                <asp:Panel ID="pnl_Plant" runat="server"  Style="display: inline">
                    <table border="0" id="table3" cellspacing="1" align="center">
                        <tr valign="top">
                            <td>
                                <div class="EU_TableScroll" id="showData" style="display: block">
                                    <asp:GridView ID="gv_PlantMilkDetail" runat="server" AutoGenerateColumns="true" AutoGenerateSelectButton="false"
                                        Font-Size="12px" HeaderStyle-BackColor="Silver" CssClass="EU_DataTable" OnSelectedIndexChanging="gv_PlantMilkDetail_SelectedIndexChanged">
                                        <Columns>
                                            <asp:CommandField ButtonType="Button" SelectText="Route" ShowSelectButton="True" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>

            <asp:Button ID="btn_modsample" runat="server" style="display:none" />
              <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btn_modsample"  PopupControlID="pnlpopup" 
 BackgroundCssClass="modalBackground"> </asp:ModalPopupExtender>
           <div class="perdaymilkstatus">  
            
          <asp:Panel ID="pnlpopup" runat="server"  CssClass="modalPopup"  style="display:inline" >
          <div align="right">
    <asp:Button ID="btn_Mclose" runat="server" BackColor="#00CCFF" BorderStyle="Double" Font-Size="10"  ForeColor="White" Text="X" onclick="btn_Mclose_Click" /> 
  </div>
  <div align="center">
  <asp:Label ID="Lbl_PlantName" runat="server" ForeColor="Red" Text="PlantName :"></asp:Label>
  <asp:Label ID="lbl_Plantcode" runat="server" ForeColor="Red"></asp:Label>
  </div>
  
            <table border="0"  id="table4" cellspacing="1" align="center">
                <tr valign="top">
                    <td >                   
                        <div class="EU_TableScroll" id="Div1" style="display: block">
                            <asp:GridView ID="gv_PlantRouteMilkDetail" runat="server" 
                                AutoGenerateColumns="true" AutoGenerateSelectButton="false"
                                Font-Size="12px" HeaderStyle-BackColor="Silver" CssClass="EU_DataTable" 
                                onselectedindexchanging="gv_PlantRouteMilkDetail_SelectedIndexChanging" >
                                <Columns>
                                    <asp:CommandField ButtonType="Button" SelectText="Agent" ShowSelectButton="True" />
                                </Columns>
                            </asp:GridView>
                          
                    </td>
                </tr>
            </table>
             </asp:Panel>
            </div> 

                             
               <asp:Button ID="btn_modAgent" runat="server" style="display:none" />
              <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btn_modAgent"  PopupControlID="pnl_Agent" 
 BackgroundCssClass="modalBackground"> </asp:ModalPopupExtender>
            <div class="perdaymilkstatus"> 
            
          <asp:Panel ID="pnl_Agent" runat="server" CssClass="modalPopup"  style="display:inline">
          <div align="right">
    <asp:Button ID="btn_Aclose" runat="server" BackColor="#00CCFF" BorderStyle="Double" 
                  Font-Size="10"  ForeColor="White" Text="X" onclick="btn_Aclose_Click" /> 
  </div>
   <div align="center">
   <asp:Label ID="Lbl_RouteName" runat="server" ForeColor="Red" Text="RouteName :"></asp:Label>
  <asp:Label ID="Lbl_Routecode" runat="server" ForeColor="Red"></asp:Label>
  </div>
            <table border="0"  id="Agenttable" cellspacing="1" align="center">
                <tr valign="top">
                    <td >
                        <div class="EUA_TableScroll" id="Div2" style="display: block">
                            <asp:GridView ID="gv_PlantAgentMilkDetail" runat="server" AutoGenerateColumns="true" AutoGenerateSelectButton="false"
                                Font-Size="12px" HeaderStyle-BackColor="Silver" CssClass="EUA_DataTable" >
                                
                            </asp:GridView>
                    </td>
                </tr>
            </table>
             </asp:Panel>
              </div>     
              
           
            <div align="center">
                <asp:Label ID="Lbl_Errormsg" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
        <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
      <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
