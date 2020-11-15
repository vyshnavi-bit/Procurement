<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Despatch_Rpt.aspx.cs" Inherits="Despatch_Rpt" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
        .style3
        {
            width: 100%;
        }
        
        
        <script type="text/javascript">
function displayPopup(message)
{
   pleasecheckurcode;
}
</script>
        
      
      
        
        
        
        
    .buttonclass
{
padding-left: 10px;
font-weight: bold;
}
        
        
         
         .stylefiledset
        {
           width:40%; 
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
        




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height: 10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>
 <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel2" runat="server"  >
            <ContentTemplate>

    
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%">
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; DESPATCH AND ACKNOWLEDGEMENT REPORT
                </p>
            </td>
        </tr>
        </table>

 <center>
         
   <div class=stylefiledset>
   <fieldset class="fontt">   
            <legend style="color: #3399FF">Despatch Report</legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
            <td>
                &nbsp;</td>
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
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="BottomRight">
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
                              <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"  >
                                   </asp:CalendarExtender>   
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
                                          	
 <asp:DropDownList ID="ddl_PlantName" 
        runat="server" Width="200px" Font-Bold="True" Font-Size="Large" 
                             onselectedindexchanged="ddl_PlantName_SelectedIndexChanged" >
       
    </asp:DropDownList>

                    </td>
                </tr> 
                  
                 <tr align="center">
                    <td colspan="4">                                    
                        <asp:RadioButtonList ID="RdoSelect" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">Despatch</asp:ListItem>
                            <asp:ListItem Value="2">AcknowlegeMent</asp:ListItem>
                            <asp:ListItem Value="3">Difference</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>  
                     
                
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_PlantID" runat="server" AutoPostBack="True" 
                            Enabled="False" Height="17px" Visible="False" Width="24px">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                    </td>
                    <td align="left">
                        <asp:Button ID="Button2" runat="server" BackColor="#00CCFF" 
                            BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="30px" 
                            onclick="Button2_Click" Text="OK" Width="70px" />
                        <asp:Button ID="btn_print" runat="server" BackColor="#00CCFF" 
                            BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="30px" 
                            OnClientClick="return PrintPanel();" Text="Print" />
                        <asp:Button ID="btn_export" runat="server" BackColor="#00CCFF" 
                            BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="30px" 
                            onclick="btn_export_Click" Text="Export" />
                    </td>
                </tr>
                     
                
            </table>            
            <center>
            <asp:Label ID="lblmsg" runat="server"  Text="Label" ForeColor="Red" fontsize="16px" 
                    Font-Bold="True" Font-Size="Large"></asp:Label>
            </center>
          
   </fieldset>
   </div>    
 </center>
     <asp:Panel id="pnlContents" runat = "server">
    <table class="style3">
        <tr align="center">
            <td>
                <asp:Image ID="Image1" runat="server" Height="75px" 
                    ImageUrl="~/Image/VLogo.png" Width="75px" /> 

            </td>
        </tr>
        <tr align="center">
            <td>
            <div style="width: 100%; float: left; border: 1px solid #d6d6d6; border-radius: 5px 5px 5px 5px;
        background-color: #F8F8FF">
                <table class="style3">
                    <tr>
                        <td align="center">                           
                            <asp:GridView ID="GridView1" runat="server" CssClass="gridview2" 
                                Font-Size="Small" onrowdatabound="GridView1_RowDataBound" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                             <asp:GridView ID="GridView2" runat="server" CssClass="gridview2"                              
                                Font-Size="Small" ShowFooter="true" 
                                onrowdatabound="GridView2_RowDataBound"  >
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
            </td>
        </tr>
    </table>


    </asp:Panel> 
          </ContentTemplate>
       
          <Triggers>
<asp:PostBackTrigger ControlID="btn_export" />
</Triggers>

 </asp:UpdatePanel>


    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" ToolPanelView="None"/>


</asp:Content>

