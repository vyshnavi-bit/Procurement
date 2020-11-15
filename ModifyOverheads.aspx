<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ModifyOverheads.aspx.cs" Inherits="ModifyOverheads" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style5
        {
            font-family: Andalus;
        }
        .style6
        {
            width: 446px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <div>
    
   <center>    <strong>    
            <asp:Label ID="Label5" runat="server" Text="Modifying Overheads" 
        CssClass="style5"></asp:Label>   
       <center>

           &nbsp;<asp:Panel ID="Panelshow" runat="server">
           <fieldset style="background-color: #CCFFFF" width=50%>
      
               <table class="style6">
                   <tr width=25%>
                       <td align=right width=25%>
                   <strong>    
                   <asp:Label ID="Label22" runat="server" CssClass="style5" Text="Plant Code" 
                               Font-Bold="False"></asp:Label>
       </strong>
                       </td>
                       <td   align=left width=25%>
                           <strong>
                           <em>
                          <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                              onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" TabIndex="8" 
                              Width="190px" CssClass="tb10">
                          </asp:DropDownList>
                          </em></strong>
                       </td>
                   </tr>
                   <tr width=25%>
                       <td  align=right width=25%>
                   <strong>    
                   <asp:Label ID="Label23" runat="server" CssClass="style5" Text="From Date" 
                               Font-Bold="False"></asp:Label>
       </strong>
                       </td>
                       <td  align=left width=25%>
                            <asp:TextBox ID="txt_FromDate" runat="server" CssClass="tb10" Height="20px" 
                               Width="185px"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
                            <asp:CalendarExtender ID="txt_FromDate_CalendarExtender2" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
                       </td>
                   </tr>
                   <tr width=25%>
                       <td  align=right width=25%>
                           <strong>
                           <em>
                          <asp:DropDownList ID="ddl_Plantcode" runat="server" Enabled="False" 
                              Visible="False">
                          </asp:DropDownList>
                          </em></strong>
                       </td>
                       <td  align=left width=25%>
                           <strong>    
          <asp:Button ID="btn_Save" runat="server" Text="Show"  BackColor="#6F696F" 
                    ForeColor="White"  onclick="btn_Save_Click" TabIndex="10" Width="58px" 
                       CssClass="button2222" />
                       
       </strong>
                       </td>
                   </tr>
               </table>
      
           </fieldset>
           </asp:Panel>
           <br />
           <asp:Panel ID="PanelHIDE" runat="server">
           <fieldset style="background-color: #CCFFFF" width=50%>
      
               <table class="style6">
                   <tr width=25%>
                       <td align=right width=25%>
                   <strong>    
                   <asp:Label ID="lbl_ref" runat="server" CssClass="style5" Text="Reference Id" 
                               Font-Bold="False"></asp:Label>
       </strong>
                       </td>
                       <td align="left" width=25%>
                           <strong>    
       <asp:TextBox ID="txt_ref" runat="server" CssClass="tb10" Enabled="False" Width="125px"></asp:TextBox>
       </strong>
                       </td>
                   </tr>
                   <tr width=25%>
                       <td align=right width=25%>
                   <strong>    
                   <asp:Label ID="voucherty" runat="server" CssClass="style5" Text="Voucher Type" 
                               Font-Bold="False"></asp:Label>
       </strong>
                       </td>
                       <td align="left" width=25%>
                           <strong>    

                 <asp:DropDownList ID="vouchertype" runat="server" CssClass="tb10" 
                     Height="21px" Width="125px" AutoPostBack="True" 
           Font-Size="X-Small">
                     <asp:ListItem Value="CR">CR</asp:ListItem>
                     <asp:ListItem Value="CP">CP</asp:ListItem>
                 </asp:DropDownList>

       </strong>
                       </td>
                   </tr>
                   <tr width=25%>
                       <td  align=right width=25%>
                   <strong>    
                   <asp:Label ID="AMT" runat="server" CssClass="style5" Text="Amount" 
                               Font-Bold="False"></asp:Label>
       </strong>
                       </td>
                       <td  align=left width=25%>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>

                            <strong>    
       <asp:TextBox ID="txt_amount" runat="server" CssClass="tb10" Width="125px"></asp:TextBox>
       </strong>

                       </td>
                   </tr>
                   <tr width=25%>
                       <td  align=right width=25%>
                   <strong>    
                   <asp:Label ID="lblnarr" runat="server" CssClass="style5" Text="Narration" 
                               Font-Bold="False"></asp:Label>
       </strong>
                       </td>
                       <td  align=left width=25%>

                            <strong>    
       <asp:TextBox ID="txt_narration" runat="server" CssClass="tb10" TextMode="MultiLine" 
                                Width="181px"></asp:TextBox>
       </strong>

                       </td>
                   </tr>
                   <tr width=25%>
                       <td  align=right width=25%>
                           &nbsp;</td>
                       <td  align=left width=25%>
                           <strong>    
             <asp:Button ID="editsave" runat="server" Text="Save"  BackColor="#6F696F" 
                    ForeColor="White"  onclick="editsave_Click" TabIndex="10" Width="58px" 
                       CssClass="button2222" />
                           <asp:Button ID="Cancel" runat="server" BackColor="#6F696F" 
                               CssClass="button2222" ForeColor="White" onclick="Cancel_Click" TabIndex="10" 
                               Text="Cancel" Width="58px" />
       </strong>
                       </td>
                   </tr>
               </table>
      
           </fieldset>
           </asp:Panel>

      
       </center>
       </strong>
        </center>
    
    
    
       &nbsp;</center>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <strong __designer:mapid="1550">
                  <em __designer:mapid="15ce">
                  <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <center>
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        DataKeyNames="Reference_No" Font-Size="X-Small" 
        onpageindexchanging="GridView1_PageIndexChanging" 
        onrowdeleting="GridView1_RowDeleting" PageSize="8" EnableViewState="False" 
            onrowcancelingedit="GridView1_RowCancelingEdit1" 
            onrowediting="GridView1_RowEditing" onrowupdating="GridView1_RowUpdating" 
            CssClass="gridview1" Font-Italic="False" AutoGenerateSelectButton="True" 
                          onselectedindexchanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="Reference_No" HeaderText="Reference_No" ReadOnly="True" 
                SortExpression="Reference_No" />
            <asp:BoundField DataField="EntryDate" HeaderText="EntryDate" 
                SortExpression="EntryDate" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            <asp:BoundField DataField="LedgerName" HeaderText="LedgerName" 
                SortExpression="LedgerName" >
            <ControlStyle Width="75px" />
            <FooterStyle Width="75px" />
            <HeaderStyle Width="75px" />
            <ItemStyle Width="75px" />
            </asp:BoundField>
            <asp:BoundField DataField="Voucher_Type" HeaderText="VoucherType" ReadOnly="True" 
                SortExpression="Voucher_Type">
            <ControlStyle Width="8px" />
            <FooterStyle Width="8px" />
            <HeaderStyle Width="8px" />
            <ItemStyle Width="8px" />
            </asp:BoundField>
            <asp:BoundField DataField="Narration" HeaderText="Narration" 
                SortExpression="Narration" ReadOnly="True" >
            <ControlStyle Width="125px" />
            <FooterStyle Width="125px" />
            <HeaderStyle Width="125px" />
            <ItemStyle Width="125px" />
            </asp:BoundField>
            <asp:BoundField DataField="Reference_Name" HeaderText="Reference_Name" 
                SortExpression="Reference_Name" >
            <ControlStyle Width="75px" />
            <FooterStyle Width="75px" />
            <HeaderStyle Width="75px" />
            <ItemStyle Width="75px" />
            </asp:BoundField>
            <asp:BoundField DataField="CreditAmount" HeaderText="CreditAmount" ReadOnly="True" 
                SortExpression="CreditAmount" >
            <ControlStyle Width="75px" />
            <FooterStyle Width="75px" />
            <HeaderStyle Width="75px" />
            <ItemStyle Width="75px" />
            </asp:BoundField>
            <asp:BoundField DataField="DebitAmount" HeaderText="DebitAmount" 
                SortExpression="DebitAmount" >
            <ControlStyle Width="90px" />
            <FooterStyle Width="90px" />
            <HeaderStyle Width="90px" />
            <ItemStyle Width="90px" />
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#99CCCC" Font-Size="Small" ForeColor="#003399" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
            <RowStyle BackColor="White" ForeColor="#003399" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <SortedAscendingCellStyle BackColor="#EDF6F6" />
            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
            <SortedDescendingCellStyle BackColor="#D6DFDF" />
            <SortedDescendingHeaderStyle BackColor="#002876" />
    </asp:GridView>
    </center>
        <br />
    </em></strong></div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

