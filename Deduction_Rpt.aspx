<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Deduction_Rpt.aspx.cs" Inherits="Deduction_Rpt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    body
    {
        font-family: Arial;
        font-size: 8pt;
    }
    .Pager span
    {
        text-align: center;
        color:  #3AC0F2;
        display: inline-block;
        width: 20px;
        background-color: #3AC0F2;
        margin-right: 3px;
        line-height: 150%;
        border: 1px solid #3AC0F2;
    }
    .Pager a
    {
        text-align: center;
        display: inline-block;
        width: 20px;
        background-color: #3AC0F2;
        color: #fff;
        border: 1px solid #3AC0F2;
        margin-right: 3px;
        line-height: 150%;
        text-decoration: none;
    }
 
        #table4
        {
            height: 320px;
        }
 
        .style5
        {
            height: 38px;
        }
        .style6
        {
            width: 350px;
        }
        .style7
        {
            font-size: medium;
        }
 
        .style9
        {
            font-size: medium;
            font-family: Andalus;
        }
 
        .style11
        {
            color: #000000;
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server" />
   
       <center style="background-color: #FFFFFF">
        <br />
    <fieldset style="background-color: #CCFFFF"class="style6">
   
   <table align="center" width="400px">
   
   


   <tr>
   
   <td align="right" >
     <asp:Label ID="Label6" runat="server" Text="Plant Name" Font-Size="Small" 
           CssClass="style7" style="font-family: Andalus; font-size: medium" ></asp:Label>   
   </td>
   <td align="LEFT">

  <asp:DropDownList ID="ddl_PlantName" runat="server" Width="140px" CssClass="tb10" 
                     Height="24px" Font-Size="Small">
    </asp:DropDownList>

   </td>
   
   </tr>






   <tr>
   
   <td align="right" class="style5">
       <asp:Label ID="Label10" runat="server" Text="From Date" Font-Size="Small" 
           CssClass="style7" style="font-family: Andalus; font-size: medium"></asp:Label>
   </td>
   <td align="left" class="style5">
     
                          <asp:TextBox ID="txt_FromDate" runat="server" TabIndex="4" Width="150px" 
                              CssClass="tb10" Font-Size="X-Small"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="TopRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
                          </em></strong>
   </td>
   
   </tr>



   <tr>
   
   <td align="right">
       <asp:Label ID="Label11" runat="server" Text="To Date" Font-Size="Small" 
           CssClass="style7" style="font-family: Andalus; font-size: medium"></asp:Label>
   </td>
   <td align="left">
     
                          <asp:TextBox ID="txt_ToDate" runat="server" TabIndex="4" Width="150px" 
                              CssClass="tb10" Font-Size="X-Small"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_ToDate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="TopRight" 
                                TargetControlID="txt_ToDate">
                            </asp:CalendarExtender>
                          </em></strong>
   </td>
   
   </tr>




   <tr>
   
   <td align="center" colspan="2">

            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                oncheckedchanged="deduc_allot_CheckedChanged" Text="Allot Deduc" 
                Checked="True" Enabled="False" Visible="False" />
            <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" 
                oncheckedchanged="deduct_pen_CheckedChanged" Text="Pending Deduc" 
                Checked="True" Enabled="False" Visible="False" />
            <asp:CheckBox ID="CheckBox3" runat="server" AutoPostBack="True" 
                oncheckedchanged="deduct_details_CheckedChanged" Text="Deduct Deta" 
                Checked="True" Enabled="False" Visible="False" />

       </td>
   
   </tr>




   <tr>
   
   <td align="left">

             &nbsp;</td>
   <td ALIGN="left">   
         <left> 

             <asp:Button ID="btn_GetData" runat="server" BackColor="#00CCFF" 
                 ForeColor="White" Text="GetData" Height="26px" BorderStyle="Double" 
                 Font-Bold="True" onclick="btn_GetData_Click" CssClass="button2222" />
             <asp:Button ID="btn_print" runat="server" BackColor="#00CCFF" ForeColor="White" 
                            Text="Print" Height="26px" BorderStyle="Double" 
                 Font-Bold="True"   OnClientClick = "return PrintPanel();" 
                 CssClass="button2222" onclick="btn_print_Click"  />
         </left>
            <asp:CheckBox ID="CheckBox4" runat="server" AutoPostBack="True" 
                Text="All Details" oncheckedchanged="CheckBox4_CheckedChanged" 
             Visible="False" />

   </td>
   
   </tr>




   </table>
   
   
   
    </fieldset> 
       <br />
       <br />
    </center>

    <asp:Panel id="pnlContents" runat = "server">
   
    <fieldset width="100%">
    
    
        <table align="right">
            <tr valign="top">
                <td  align="center" width="34%" colspan="4" style="width: 68%">

                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="style9">

           <center>         
               <asp:Panel ID="Panel1" HorizontalAlign="Center" runat="server">
                   <span class="style9">
           <center>        <asp:Image ID="Image1" runat="server" Height="50px" 
                       ImageUrl="~/Image/VLogo.png" Width="100px" /> 
                   <br />
                   <asp:Label ID="Label12" runat="server" 
                       style="font-weight: 700; font-size: medium; " 
                       Text="Deduction Recovery Report" CssClass="style11"></asp:Label>
                   <br />
                   <asp:Label ID="Label13" runat="server" CssClass="style11" 
                       style="font-weight: 700; font-size: small" Text="Plant Name"></asp:Label>
                   <asp:Label ID="lblpname" runat="server" 
                       style="font-weight: 700; text-align: left;" Text="Label" 
                       Width="250px" CssClass="style11"></asp:Label> <span class="style9">
               <br />
               <center>
               <asp:Label ID="lbldate" runat="server" 
                   style="font-weight: 700; text-align: center;" Width="500px" CssClass="style11"></asp:Label></center>
               :</span></center> 
                   </span>
               </asp:Panel>
                    </center>
        
                    </span><tr valign="top">
                        <td colspan="4" style="width: 68%" width="34%">
                            <span class="style9">
                            <center>
                                <asp:Label ID="lblamount" runat="server" style="text-align: center" 
                                    Text="Allot Amount"></asp:Label>
                            </center>
                            <center>
                                <asp:GridView ID="DeductionAllotted" runat="server" AutoGenerateColumns="False" 
                                    CaptionAlign="Top" Font-Size="X-Small" HeaderStyle-BackColor="Silver" 
                                    HeaderStyle-ForeColor="Black" RowStyle-BackColor="Transparent" 
                                    ShowFooter="true" style="font-family: Arial">
                                    <Columns>
                                        <asp:BoundField DataField="agent_id" HeaderText="AgentId" 
                                            ItemStyle-Width="40px" />
                                        <asp:BoundField DataField="billadvance" HeaderText="billadv" 
                                            ItemStyle-Width="60px" />
                                        <asp:BoundField DataField="Ai" HeaderText="Ai" ItemStyle-Width="60px" />
                                        <asp:BoundField DataField="Feed" HeaderText="Feed" ItemStyle-Width="60px" />
                                        <asp:BoundField DataField="can" HeaderText="can" ItemStyle-Width="60px" />
                                        <asp:BoundField DataField="recovery" HeaderText="recovery" 
                                            ItemStyle-Width="60px" />
                                        <asp:BoundField DataField="others" HeaderText="others" ItemStyle-Width="60px" />
                                    </Columns>
                                </asp:GridView>
                            </center>
                            </span>
                            <tr>
                                <td valign="top" width="34%">
                                    <span class="style9">
                                    <center>
                                        <asp:Label ID="Lbldeduct" runat="server" style="text-align: center" 
                                            Text="Pending Amount"></asp:Label>
                                    </center>
                                    <left>
                                        <asp:GridView ID="DeductionPending" runat="server" AutoGenerateColumns="false" 
                                            CaptionAlign="Top" Font-Size="X-Small" HeaderStyle-BackColor="Silver" 
                                            HeaderStyle-ForeColor="Black" position="absolute" 
                                            RowStyle-BackColor="Transparent" ShowFooter="true" style="font-family: Arial">
                                            <Columns>
                                                <asp:BoundField DataField="agent_id" HeaderText="AgentId" 
                                                    ItemStyle-Width="40px" />
                                                <asp:BoundField DataField="billadvance" HeaderText="billadv" 
                                                    ItemStyle-Width="60px" />
                                                <asp:BoundField DataField="Ai" HeaderText="Ai" ItemStyle-Width="60px" />
                                                <asp:BoundField DataField="Feed" HeaderText="Feed" ItemStyle-Width="60px" />
                                                <asp:BoundField DataField="can" HeaderText="can" ItemStyle-Width="60px" />
                                                <asp:BoundField DataField="recovery" HeaderText="recovery" 
                                                    ItemStyle-Width="60px" />
                                                <asp:BoundField DataField="others" HeaderText="others" ItemStyle-Width="60px" />
                                            </Columns>
                                        </asp:GridView>
                                    </left>
                                    </span>
                                    <td valign="top" width="34%">
                                        &nbsp;<td valign="top" width="34%">
                                            &nbsp;<td valign="top" width="34%">
                                                <span class="style9">
                                                <asp:Label ID="Lbldeduct1" runat="server" style="text-align: center" 
                                                    Text="Total Recovery"></asp:Label>
                                                </span>
                                                <br />
                                                <asp:GridView ID="DeductionRecovery1" runat="server" 
                                                    AutoGenerateColumns="False" CaptionAlign="Top" Font-Size="X-Small" 
                                                    HeaderStyle-BackColor="Silver" HeaderStyle-ForeColor="Black" 
                                                    RowStyle-BackColor="Transparent" ShowFooter="True" 
                                                    style="font-family: Arial">
                                                    <Columns>
                                                        <asp:BoundField DataField="agent_id" HeaderText="AgentId" 
                                                            ItemStyle-Width="40px" >
                                                        <ItemStyle Width="40px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="billadvance" HeaderText="billadv" 
                                                            ItemStyle-Width="60px" >
                                                        <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Ai" HeaderText="Ai" ItemStyle-Width="60px" >
                                                        <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Feed" HeaderText="Feed" ItemStyle-Width="60px" >
                                                        <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="can" HeaderText="can" ItemStyle-Width="60px" >
                                                        <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="recovery" HeaderText="recovery" 
                                                            ItemStyle-Width="60px" >
                                                        <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="others" HeaderText="others" ItemStyle-Width="60px" >
                                                        <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="RDeduction_RecoveryDate" HeaderText="Recoverydate" 
                                                            ItemStyle-Width="60px" SortExpression="RDeduction_RecoveryDate" >
                                                        <ItemStyle Width="60px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Rdeductiondate" HeaderText="AdeductDate" 
                                                            SortExpression="Rdeductiondate" />
                                                    </Columns>
                                                    <HeaderStyle BackColor="Silver" ForeColor="Black" />
                                                    <RowStyle BackColor="Transparent" />
                                                </asp:GridView>
                                            </td>
                                        </td>
                                    </td>
                                </td>
                            </tr>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div align="center" style="display:none">
                                <right>
                                    <span class="style9">
                                    <center>
                                        <asp:Label ID="ldl_deducdetails" runat="server" style="text-align: center" 
                                            Text="Deduction Details"></asp:Label>
                                    </center>
                                    </span>
                                    <span class="style9">
                                    <asp:GridView ID="DeductionRecovery" runat="server" AutoGenerateColumns="false" 
                                        CaptionAlign="Top" Font-Size="X-Small" HeaderStyle-BackColor="Silver" 
                                        HeaderStyle-ForeColor="Black" RowStyle-BackColor="Transparent" 
                                        ShowFooter="true" style="font-family: Arial" Visible="false">
                                        <Columns>
                                            <asp:BoundField DataField="agent_id" HeaderText="AgentId" 
                                                ItemStyle-Width="40px" />
                                            <asp:BoundField DataField="billadvance" HeaderText="billadv" 
                                                ItemStyle-Width="60px" />
                                            <asp:BoundField DataField="Ai" HeaderText="Ai" ItemStyle-Width="60px" />
                                            <asp:BoundField DataField="Feed" HeaderText="Feed" ItemStyle-Width="60px" />
                                            <asp:BoundField DataField="can" HeaderText="can" ItemStyle-Width="60px" />
                                            <asp:BoundField DataField="recovery" HeaderText="recovery" 
                                                ItemStyle-Width="60px" />
                                            <asp:BoundField DataField="others" HeaderText="others" ItemStyle-Width="60px" />
                                            <asp:BoundField DataField="Rcoverydate" HeaderText="Recoverydate" 
                                                ItemStyle-Width="60px" />
                                        </Columns>
                                    </asp:GridView>
                                    </span>
                                </right>
                            </div>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </td>
            </tr>
        </table>
        
    </fieldset>
   
    </asp:Panel>




    <span class="style9" __designer:mapid="30">
       
                                </span>




 <br />
   
</asp:Content>

