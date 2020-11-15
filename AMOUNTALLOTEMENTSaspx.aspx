<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AMOUNTALLOTEMENTSaspx.aspx.cs" Inherits="AMOUNTALLOTEMENTSaspx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style7
        {
            text-align: center;
        }
        .tb8
        {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>  
    <br />
    <div align="right">
      <asp:HyperLink ID="HyperLink1" CssClass="fontt" runat="server" NavigateUrl="~/AmountForRoute.aspx">Plant Amount Allot</asp:HyperLink>
    </div>
    <div class="legagentsms">
        <fieldset class="fontt">
            <legend style="color: #3399FF">Admin Amount Allotment </legend>
            <table id="table4" align="center" border="0" cellspacing="1" width="100%">
                <tr>
                    <td  >
                      
                    </td>
                </tr>
                 <tr>
                    <td>
                        &nbsp;</td>
                    <td align="right">
                        &nbsp;<asp:Label ID="Label3" runat="server" Text="Avail_Balance"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td align="left">
                      
                                <strong>
                                <asp:TextBox ID="Txt_Availamount" runat="server" 
              Height="22px" Width="130px" 
                                    Enabled="False"></asp:TextBox>
                                </strong>
                      
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td align="right">
                        &nbsp;<asp:Label ID="Label10" runat="server" Text="Ref_No"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td align="left">
                      
          <asp:TextBox ID="txt_tid" runat="server" Width="130px" Enabled="False"></asp:TextBox>
                      
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label11" runat="server" Text="User ID/Name"></asp:Label>
                    </td>
                    <td align="right">
                    </td>
                    <td align="left">
          <asp:TextBox ID="txt_userid" runat="server" Width="130px" Enabled="False" Height="18px"></asp:TextBox>
          <asp:TextBox ID="txt_name" runat="server"  Width="100px" Height="19px" 
                            Enabled="False" Visible="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label12" runat="server" Text="Date/Time"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td align="left">
                            <asp:TextBox ID="txt_FromDate" runat="server" CssClass="tb8" Height="19px" 
                                ontextchanged="txt_FromDate_TextChanged" Width="130px"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
                            <asp:CalendarExtender ID="txt_FromDate_CalendarExtender2" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
          <asp:TextBox ID="txt_time" runat="server" Width="100px" Enabled="False" 
              Height="18px" Visible="False"></asp:TextBox>
                                </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label13" runat="server" Text="Amount"></asp:Label>
                    </td>
                    <td align="right">
                    </td>
                    <td align="left">
          <asp:TextBox ID="txt_amount" runat="server" Width="130px"></asp:TextBox>
                                </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                        <asp:Label ID="Label14" runat="server" Text="Description"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td align="left">
          <asp:TextBox ID="txt_description" runat="server" Height="77px" Width="243px" 
              TextMode="MultiLine"></asp:TextBox>
                                </td>
                    <td width="12%">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                                <asp:TextBox ID="txt_previous" runat="server" 
              ontextchanged="txt_previous_TextChanged" Visible="False" Height="16px" Width="24px"></asp:TextBox>
                    </td>
                    <td align="right">
                    </td>
                    <td align="left">
          <asp:Button ID="Button1" runat="server" Text="Save" CssClass="button2222" 
              onclick="Button1_Click" />
                    </td>
                </tr>
            </table>
            <br />
        </fieldset>
    </div>
    <left>
    <div class="style7">
      <left>  &nbsp;</left>&nbsp;</letf><br />
 <center> 
   <asp:Panel ID="Panel1" runat="server">
     <br />
     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
         CssClass="gridview1" Font-Size="Small" PageSize="5" AllowPaging="True" 
         onpageindexchanging="GridView1_PageIndexChanging">
         <Columns>
             <asp:BoundField DataField="Tid" HeaderText="Tid" ReadOnly="True" 
                 SortExpression="Tid" />
             <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
             <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
             <asp:BoundField DataField="Time" HeaderText="Time" ReadOnly="True" 
                 SortExpression="Time" />
             <asp:BoundField DataField="Amount" HeaderText="Amount" 
                 SortExpression="Amount" />
             <asp:BoundField DataField="Description" HeaderText="Description" 
                 SortExpression="Description" />           
         </Columns>
     </asp:GridView>
     <br />
    </asp:Panel>
    </div>    
  </center>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

