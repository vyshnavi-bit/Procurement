<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProcurementImport.aspx.cs" Inherits="ProcurementImport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="App_Themes/EditGrid.css" rel="stylesheet" />
    
    <style type="text/css">
        .style1
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            width: 10%;
        }
        .style2
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            width: 28%;
        }
        .style3
        {
            color: #3399FF;
        }
        #table12
        {
            width: 103%;
            margin-left: 18px;
        }
        .style4
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            color: #3399FF;
            width: 9%;
        }
        .style6
        {
            font-family: Verdana,Arial,Helvetica,sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            text-decoration: none;
            word-spacing: normal;
            letter-spacing: normal;
            text-transform: none;
            text-decoration: none;
            BACKGROUND: none;
            color: black;
            width: 34%;
        }
        .style7
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            color: #3399FF;
            width: 10%;
        }
        .style8
        {
            font-family: Verdana,Arial,Helvetica,sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            text-decoration: none;
            word-spacing: normal;
            letter-spacing: normal;
            text-transform: none;
            text-decoration: none;
            BACKGROUND: none;
            color: black;
            width: 36%;
        }
        .style9
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            width: 5%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div id="main0">
        <table border="0" cellpadding="0" cellspacing="1" width="100%">
            <tr>
                <td width="100%">
                    <br />
                    <p style="line-height: 150%">
                        <font class="subheading">&nbsp;&nbsp;PROCUREMENT UPDATE</font></p>
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
            <center>
                <table border="0" cellpadding="0" cellspacing="1" width="100%" 
                    style="margin-top: 22px; margin-bottom: 13px">
                    <tr>
                        <td class="style6">
                            <br />
                        </td>
                        <td class="style4">
                        <table border="0" id="table12" cellspacing="1">
                            <tr>
                                <td width="25%" align="Left">
                                    Plantname
                                </td>
                            </tr>
                        </table>
                        </td>
                        <td class="fontt" width="20%">
                                    <asp:DropDownList ID="ddl_Plantname" AutoPostBack="true" runat="server" 
                                        OnSelectedIndexChanged="ddl_Plantname_SelectedIndexChanged" Width="154px">
                                    </asp:DropDownList>
                                   
                        </td>
                        <td class="td1">
                                    <asp:DropDownList ID="ddl_Plantcode" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_Plantcode_SelectedIndexChanged"
                                        Visible="false">
                                    </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </center>
                </td>
            </tr>
            <tr>
                <td class="td1">
        <table border="0" cellpadding="0" cellspacing="1" width="100%" 
            style="height: 30px; margin-bottom: 14px">
            <tr>
                <td class="style8">
                </td>
                       <td class="style7">
       
                         &nbsp;&nbsp;&nbsp; To Date</td>
                             
                               <td width="15%" class="fontt" align="right">
                              <asp:TextBox ID="txt_ToDate" runat="server" 
                                       style="margin-left: 13px"  ></asp:TextBox>
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="MM/dd/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
                         
                </td>
                <td class="style9">
                    &nbsp;</td>
                <td class="style2">
                    <asp:RadioButton ID="rbam" runat="server" AutoPostBack="True" Checked="True" 
                 Font-Bold="True" ForeColor="#3399FF"  oncheckedchanged="rbam_CheckedChanged"
                 Text="AM" CssClass="style3" />
                    <asp:RadioButton ID="rbpm" runat="server" AutoPostBack="True" 
                 Font-Bold="True" ForeColor="#3399FF" oncheckedchanged="rbpm_CheckedChanged" 
                 Text="PM" CssClass="style3" />
                </td>
                <td class="td1">
                </td>
            </tr>
        </table>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="1" width="100%" 
            style="height: 30px; margin-bottom: 14px; margin-left: 105px;">
            <tr>
                <td class="td1">
                </td>
                <td class="style1">
           <asp:Button ID="btn_show" runat="server" 
               onclick="btn_delete_Click"  
            Text="OK" BackColor="#6F696F" Font-Bold="False" 
               ForeColor="White" Width="86px" TabIndex="1" style="margin-left: 31px" />
                </td>
                <td class="style2">
                    &nbsp;</td>
                <td class="td1">
                </td>
            </tr>
        </table>
        <br />
        <div>
        </div>
         
        
&nbsp;&nbsp;
        
        <br />
        <center>
            <div class="grid">
                <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <mcn:DataPagerGridView ID="GridView1" runat="server" 
                    AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" CssClass="datatable" 
                    onrowcancelingedit="GridView1_RowCancelingEdit" onrowediting="GridView1_RowEditing" 
                    onrowupdating="GridView1_RowUpdating" Width="615px">
                            <Columns>
                                <asp:BoundField HeaderText="Tid" DataField="Tid" SortExpression="Tid"
                            HeaderStyle-CssClass="first" ItemStyle-CssClass="first" readonly="true">
                                <HeaderStyle CssClass="first" />
                                <ItemStyle CssClass="first" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Agent Id" DataField="Agent_id" SortExpression="Agent_id" readonly="true">
                                </asp:BoundField>
                                <asp:BoundField DataField="Milk_kg" HeaderText="Milk_Kg"  SortExpression="Milk_kg" />
                                <asp:BoundField DataField="Fat" HeaderText="FAT" 
            SortExpression="Fat" />
                                <asp:BoundField DataField="Snf" HeaderText="SNF" 
                             SortExpression="Snf" />
                                
                                <asp:BoundField DataField="Remark" HeaderText="Remark" 
                                    SortExpression="Remark" />
                                
                                <asp:CommandField ShowEditButton="True" ButtonType="Button" />
                                
                            </Columns>
                            <PagerSettings Visible="False" />
                            <RowStyle CssClass="row" />
                        </mcn:DataPagerGridView>
                        <div class="pager">
                            <asp:DataPager ID="pager" runat="server" PageSize="8" PagedControlID="GridView1">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="previous"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                ShowLastPageButton="false" ShowNextPageButton="false" />
                                    <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="current"
                                NextPreviousButtonCssClass="command" />
                                    <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="next"
                                RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                ShowLastPageButton="true" ShowNextPageButton="true" />
                                </Fields>
                            </asp:DataPager>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </center>
        <asp:SqlDataSource ID="Procurementupda" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>" 
                    
            
            SelectCommand="select [Tid],[Agent_id],[Milk_kg],[Fat],[Snf],[Remark] from [Procurementimport]   where Remarkstatus=1 ">
        </asp:SqlDataSource>
        <br />
        <div align="center">
            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                <tr>
                    <td  class="td1">
                    </td>
                    <td class="altd1">
                        &nbsp;</td>
                    <td class="altd2">
                    </td>
                    <td class="altd1">
                        &nbsp;</td>
                    <td class="td1">
                    </td>
                </tr>
            </table>
        </div>
        <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
    </div>
</asp:Content>

