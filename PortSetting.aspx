<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PortSetting.aspx.cs" Inherits="PortSetting" Title="OnlineMilkTest|PortSetting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
<div>
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
          <td width="100%" colspan="2">
          <p style="line-height: 150%">
          <font class="subheading">&nbsp;&nbsp;PORT TYPE
          </font></p>
          </td>
        </tr>
        <tr>
          <td width="100%" height="3px" colspan="2"></td>
        </tr>
        <tr>
          <td width="100%" class="line" height="1px" colspan="2"></td>
        </tr>
        <tr>
          <td width="100%" height="7" colspan="2"></td>
        </tr>
        <tr>
          <td width="100%" colspan="2"> 
          </td></tr></table>
<fieldset class="ratechartlegend">
<legend class="fontt">PORT SETTING</legend>
<br />
<table width="100%" style="margin-left:40px;">
<tr>
<td class="fontt">Port Name</td>
<td class="fontt">
    <asp:TextBox ID="txtportname" runat="server"></asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ControlToValidate="txtportname" validationExpression="^[0-9,.]{1,2}$" ErrorMessage="*"></asp:RequiredFieldValidator>

    
    </td>
</tr>
<tr>
<td class="fontt">Baud Rate</td>
<td class="fontt">
    <asp:TextBox ID="txtbaudrate" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ControlToValidate="txtbaudrate" validationExpression="^[0-9,.]{1,4}$" ErrorMessage="*"></asp:RequiredFieldValidator>

    
                                        </td>
</tr>
<tr>
<td class="fontt">Choose Type</td>
<td><asp:DropDownList ID="Typedrp" runat="server">
<asp:ListItem>Weigher</asp:ListItem>
<asp:ListItem>Analyzer</asp:ListItem>
    </asp:DropDownList>
</td>
    
</tr>
</table>
<br />
<table width="100%" style="margin:5px 10px 10px 10px;">
<tr>
<td width="50%"></td>
   <td width="10%"> 
   <asp:Button ID="Button1" runat="server" Text="Save" BackColor="#6F696F" ForeColor="White" OnClientClick="return confirm('Are you sure you want to Save this record?');"  onclick="Button1_Click" />
           
</td>
<td width="10%">    &nbsp;</td>
    <td width="30%"></td>
</tr>
</table>
</fieldset>
<br />
<br />
<center>
      <div class="grid">
        <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <mcn:DataPagerGridView ID="GridView1" runat="server" OnRowDataBound="RowDataBound"
                    AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" CssClass="datatable" 
                    
                    onrowcancelingedit="GridView1_RowCancelingEdit1" 
                    onrowdeleting="GridView1_RowDeleting1" onrowediting="GridView1_RowEditing" 
                    onrowupdating="GridView1_RowUpdating1" PageSize="2" >
                     <Columns>
                     <asp:BoundField HeaderText="S_ID" DataField="Table_ID" SortExpression="Table_ID"
                            HeaderStyle-CssClass="first" ItemStyle-CssClass="first">
                        <HeaderStyle CssClass="first" />
                        <ItemStyle CssClass="first" />
                        </asp:BoundField>
    <asp:BoundField HeaderText="Port Name" DataField="Port_Name" SortExpression="Port_Name">
                        
                        </asp:BoundField>
   
    <asp:BoundField DataField="Baud_Rate" HeaderText="Baud Rate" 
            SortExpression="Baud_Rate" />
        <asp:BoundField DataField="Centre_ID" HeaderText="CentreID" 
            SortExpression="Centre_ID" />
             <asp:BoundField DataField="M_Type" HeaderText="Type" 
            SortExpression="M_Type" />
                         <asp:CommandField ShowEditButton="True" />
                         
            </Columns>
 <PagerSettings Visible="False" />
                    <RowStyle CssClass="row" />
                </mcn:DataPagerGridView>
               
               
               
                
               
               
               
                <asp:SqlDataSource ID="PortDS" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>" 
                    SelectCommand="SELECT * FROM [Port_Setting] WHERE ([Centre_ID] = @Centre_ID)">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="Centre_ID" Name="Centre_ID" 
                            SessionField="User_ID" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>
               
               
               
                
               
               
               
<div class="pager">
                    <asp:DataPager ID="pager" runat="server" PageSize="8" PagedControlID="GridView1">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="Previous"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                ShowLastPageButton="false" ShowNextPageButton="false" />
                            <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="current"
                                NextPreviousButtonCssClass="command" />
                            <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="Next"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                ShowLastPageButton="true" ShowNextPageButton="true" />
                        </Fields>
                    </asp:DataPager>
                </div>
                </ContentTemplate>
        </asp:UpdatePanel>
        </div>
        <br />
        <br />
         </center>
</div><uc1:uscMsgBox ID="uscMsgBox1" runat="server" /> 
</asp:Content>


