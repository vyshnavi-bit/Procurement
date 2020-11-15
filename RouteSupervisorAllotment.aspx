<%@ Page Title="OnlineMilkTest|RouteSupervisorAllotment" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RouteSupervisorAllotment.aspx.cs" Inherits="RouteSupervisorAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link type="text/css" href="App_Themes/StyleSheet.css" rel="Stylesheet" />
    <style type="text/css">

        
                       
        
        
        
        .style12
        {
            font-family: Andalus;
            color: #000000;
        }
                        
                       
        
        
        
        .style13
        {
            height: 45px;
        }
                        
                       .style111
        {
            width: 80%;
        }
                        
                       
        
        
        
            
        
        
        
    .buttonclass
{
padding-left: 10px;
font-weight: bold;
            height: 26px;
        }
        
        
        
        
        
        .style14
        {
            width: 100%;
        }
        
        
        
        
        
        .style16
        {
            width: 100%;
        }
        
           .style1filesset
        {
            width: 30%;
        }
   
        </style>

        <script type="text/javascript">
            function Showalert() {
                alert('Call JavaScript function from codebehind');
            }
</script>
    
    
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
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;ROUTESUPERVISOR ALLOTMENT</p>
            </td>
        </tr>
        <tr>
            <td width="100%" height="3px">
            </td>
        </tr>
        <tr>
            <td width="100%" class="line" height="1px">
            </td>
        </tr>
        <tr>
            <td width="100%" height="7">
            </td>
        </tr>
        </table>
<br />
<center>
<div ALIGN=center>
<fieldset class="style1filesset">
<legend class="fontt">&nbsp;ROUTESUPERVISOR ALLOTMENT</legend>
<table border="0" width="100%" id="table12" cellspacing="1">
           <tr>
                    
                   <td align="right"  width="25%">

                                      &nbsp;</td>
     <td width="25%" style="text-align: left"> 
                  
                                      <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                                          onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
                                          RepeatDirection="Horizontal">
                                          <asp:ListItem Value="1">All</asp:ListItem>
                                          <asp:ListItem Value="2">Current</asp:ListItem>
                                      </asp:RadioButtonList>
                  
                   </td>
        </tr>
           <tr>
                    
                   <td align="right"  width="25%">

                                      <asp:Label ID="Label43" runat="server" CssClass="style12" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Plant Name"></asp:Label>
        </td>
     <td width="25%" style="text-align: left"> 
                  
                                      <asp:DropDownList ID="ddl_PlantName" runat="server" CssClass="tb10" 
                                          Font-Size="Small" Height="25px" Width="130px" AutoPostBack="True" 
                                          onselectedindexchanged="ddl_PlantName_SelectedIndexChanged">
                                      </asp:DropDownList>
                  
                   </td>
        </tr>
           <tr align="right">
                    
                   <td width="25%" class="style13">

                                      <asp:Label ID="Label44" runat="server" CssClass="style12" EnableTheming="False" 
                                          Font-Size="Small" style="font-family: Andalus; font-size: medium;" 
                                          Text="Supervisor Name"></asp:Label>
                   </td>
     <td  align="left" width="25%" class="style13"> 
            <asp:DropDownList ID="ddl_SupervisorName" runat="server" 
        Width="130px" CssClass="tb10" 
        ></asp:DropDownList>
               
                   </td>
        </tr>
           <tr align="center">
                    
                   <td width="25%" colspan="2" style="width: 50%">

          
                        <asp:DropDownList ID="ddl_Plantcode" runat="server" CssClass="style6" 
                            Font-Size="X-Small" Height="20px" Visible="False" Width="70px">
                        </asp:DropDownList>

       
        
    
  
            
                                      <asp:Button ID="btn_Save" runat="server" CssClass="buttonclass" 
                                          onclick="btn_Save_Click" OnClientClick="return validate();" 
                            Text="Save" />
                                      <asp:Button ID="btn_Reset" runat="server" CssClass="buttonclass" 
                                          onclick="btn_Reset_Click" OnClientClick="return validate();" 
                            Text="Reset" />
                                      <asp:Button ID="Button4" runat="server" BorderColor="#663300" 
                                          BorderStyle="Inset" BorderWidth="1px" CssClass="buttonclass" Font-Bold="True" 
                                           OnClientClick="return PrintPanel();" 
                            Text="Print" />
                                      <asp:Button ID="Button3" runat="server" CssClass="buttonclass" 
                                          onclick="Button3_Click" Text="Export" />

       
        
    
  
                   </td>
        </tr>
        </table>
        </fieldset>
        </div>
</center>
            <center>
               
                <table class="style16">
                    <tr align=center>
                        <td>
           <div >   
    <asp:Table ID="Table2" runat="Server" BorderColor="White" 
        CaptionAlign="Top" CellPadding="1" CellSpacing="1" Height="40px" 
        style="margin-left: 0px" >
        <asp:TableRow ID="TableRow1" runat="Server" BorderWidth="1" >
            <asp:TableCell ID="TableCell22" runat="Server" BorderWidth="1">
         <asp:Table ID="Table1" runat="Server" BorderColor="White" BorderWidth="1" 
                CellPadding="1" 
                CellSpacing="1" CaptionAlign="Top" Height="40px" >
                
                
                
<asp:TableRow ID="Title_TableRow" runat="Server" BorderWidth="1" BackColor="#3399CC" ForeColor="white" BorderColor="Silver">
                       
<asp:TableCell ID="TableCell6" runat="Server" BorderWidth="2"  > 
<asp:CheckBox ID="MChk_RouteName" Text="Route_Name" runat="server" AutoPostBack="True" oncheckedchanged="MChk_RouteName_CheckedChanged" />                    
</asp:TableCell>


                
</asp:TableRow>



                
<asp:TableRow ID="TableRow2" runat="Server" BorderWidth="1" BorderColor="Silver" BackColor="#ffffec">
  
  <asp:TableCell ID="TableCell1" runat="Server" BorderWidth="1" >
  <asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderWidth="1" > </asp:CheckBoxList> 
</asp:TableCell>
                           
                

                
                
</asp:TableRow>
            
</asp:Table>     
                  
</asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    </div> 
                        </td>
                    </tr>
                </table>

                </center>
    
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>


 <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel2" runat="server"  >
            <ContentTemplate>

    

        <center>
     <asp:Panel id="pnlContents" runat = "server">
    <table class="style3">
        <tr align="center">
            <td class="style14">
            
<center>

<fieldset class="style111"> 
<table style="width: 850px">
<tr>
<td align="center">
    <asp:Image ID="Image1" runat="server" Height="75px" 
        ImageUrl="~/Image/VLogo.png" Width="75px" />
    <table class="style14">
        <tr align="center">
            <td>
                <asp:GridView ID="GridView1" runat="server" 
                    AutoGenerateColumns="true" BackColor="White" BorderColor="#CCCCCC" 
                    BorderStyle="None" BorderWidth="1px" CausesValidation="false" CellPadding="3" 
                    Font-Size="12px" onpageindexchanging="GridView1_PageIndexChanging" 
                    onrowcreated="GridView1_RowCreated" onrowdatabound="GridView1_RowDataBound" 
                    PageSize="100">
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                    <Columns>
                        <asp:TemplateField HeaderText="SNo">
                            <ItemTemplate>
                                <%#Container.DataItemIndex + 1 %>
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
         </center>
        
</fieldset>
</div>
<center>
    
            
        
    </asp:Panel>
        </div>
    </center>
<br />
    <br />
    
    <asp:SqlDataSource ID="newagentgrid" runat="server" ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>"
        SelectCommand="SELECT F1.*,R1.Route_Name AS RouteName FROM 
(SELECT Scode,Sname,ISNULL(Rid,'') AS Rid FROM
(SELECT Supervisor_Code AS Scode,SupervisorName AS Sname FROM Supervisor_Details  where Company_Code=@Company_Code AND Plant_Code=@Plant_Code) AS SD
LEFT JOIN 
(SELECT Supervisor_Code,Route_Id AS Rid FROM Supervisor_RouteAllotment WHERE Company_Code=@Company_Code AND Plant_Code=@Plant_Code ) AS SA ON SD.Scode=SA.Supervisor_Code ) F1
LEFT JOIN
(SELECT Route_ID AS RRid,CONVERT(Nvarchar(15), Route_ID)+'_'+Route_Name AS Route_Name  FROM Route_Master Where Company_Code=@Company_Code AND Plant_Code=@Plant_Code) AS R1  ON F1.Rid=R1.RRid  ORDER BY Scode" >
        
        <SelectParameters>
            <asp:SessionParameter DefaultValue="Company_code" Name="Company_code" SessionField="Company_code" Type="Int32" />          
            <asp:SessionParameter DefaultValue="Plant_Code" Name="Plant_Code" SessionField="Plant_Code" Type="String" />
        </SelectParameters>         
    </asp:SqlDataSource>

<uc1:uscMsgBox ID="uscMsgBox1" runat="server" />



   
            </ContentTemplate>
           <Triggers>
<asp:PostBackTrigger ControlID="Button3" />
</Triggers>
        </asp:UpdatePanel>

</asp:Content>

