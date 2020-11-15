<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DeleteProcurementData.aspx.cs" Inherits="DeleteProcurementData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <style type="text/css">
        #table4
        {
            width: 34%;
        }
    </style>
    
    <%-- <script type="text/javascript">

         function PrintGridData()
          {
             var prtGrid = document.getElementById('<%=ReportGridView1.ClientID %>');

             //window.print();
             prtGrid.border = 0;
             //GridView1.Attributes(style) = "page-break-after:always"         
             var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
             prtwin.document.write(prtGrid.outerHTML);
             prtwin.document.close();
             prtwin.focus();
             prtwin.print();
             prtwin.close();
         }
</script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>       
    <br />

    <center>
    
                        <asp:Panel ID="Panel1" runat="server" Width="600px" BorderColor="#00FFCC" 
                            BorderStyle="Inset" BorderWidth="2px">
                            &nbsp;&nbsp;<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label4" runat="server" 
                                style="font-size: small; color: #FF9966; font-weight: 700;" Text="From"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                            <asp:TextBox ID="txt_FromDate" runat="server" CssClass="tb8" Height="20px" 
                                ontextchanged="txt_FromDate_TextChanged" Width="200px" Enabled="False"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
                            <asp:CalendarExtender ID="txt_FromDate_CalendarExtender2" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
                            &nbsp;&nbsp;
                            <asp:Label ID="Label5" runat="server" 
                                style="font-size: small; color: #FF9966; font-weight: 700;" Text="To"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txt_ToDate" runat="server" CssClass="tb8" Height="20px" 
                                ontextchanged="txt_ToDate_TextChanged" Width="200px" Enabled="False"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_ToDate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                                TargetControlID="txt_ToDate">
                            </asp:CalendarExtender>
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label6" runat="server" 
                                style="font-size: small; color: #FF9966; font-weight: 700;" Text="Plant_Name"></asp:Label>
&nbsp;
                            <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                                CssClass="tb8" Font-Bold="True" Font-Size="Large" Height="29px" 
                                onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="200px">
                                <asp:ListItem>---------Select---------</asp:ListItem>
                            </asp:DropDownList>
                           
                            <br />
                            <asp:RadioButtonList ID="rbtLstReportItems" runat="server" 
                                RepeatDirection="Horizontal" RepeatLayout="Table" style="font-family: serif">
                                <asp:ListItem Text="Procurement" Value="1"></asp:ListItem>
                                <asp:ListItem Text="DpuProcurement" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>
                           
                            <br />
                            <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                                Height="16px" Visible="false" Width="29px">
                            </asp:DropDownList>
                               <br />
                          <center>  
                                <center>  <asp:Button ID="btn_Ok" runat="server" CssClass="button2222" 
                                onclick="btn_Ok_Click" Text="Show" />  </center>
                            
                              <asp:TextBox ID="txt_assigndate" runat="server" CssClass="tb8" Enabled="False" 
                                  Height="20px" ontextchanged="txt_FromDate_TextChanged" Visible="False" 
                                  Width="200px"></asp:TextBox>
                              <asp:CalendarExtender ID="txt_assigndate_CalendarExtender" runat="server" 
                                  Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                  TargetControlID="txt_assigndate">
                              </asp:CalendarExtender>
                              <asp:CalendarExtender ID="txt_assigndate_CalendarExtender2" runat="server" 
                                  Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                  TargetControlID="txt_assigndate">
                              </asp:CalendarExtender>
                            
                            </center>
                         
                        </asp:Panel>
                        <br />
                        </center>
                           <center>
                           
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
                                BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="GridViewStyle" 
                                DataKeyNames="Prdate" Font-Size="15px" 
                                onpageindexchanging="GridView1_PageIndexChanging" 
                                onrowcancelingedit="GridView1_RowCancelingEdit" 
                                onrowdatabound="GridView1_RowDataBound" onrowediting="GridView1_RowEditing" 
                                onrowupdating="GridView1_RowUpdating" 
                                onselectedindexchanged="GridView1_SelectedIndexChanged" PageSize="32" 
                                   onrowdeleting="GridView1_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="prdate" HeaderText="Date" ReadOnly="True" 
                                        SortExpression="prdate" />
                                    <asp:BoundField DataField="Plant_code" HeaderText="Plant_code" 
                                        SortExpression="Plant_code">
                                    </asp:BoundField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                <RowStyle BackColor="White" ForeColor="#003399" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                <SortedDescendingHeaderStyle BackColor="#002876" />
                            </asp:GridView>
                               <asp:Button ID="Button1" runat="server" onclick="Button1_Click1" 
                                   Text="Button" Visible="False" />
                                  
                           <%--/    <asp:Button ID="Button1" runat="server" onclick="PrintGridData()" Text="prtGrid" />--%>
                            </center>
                            <br />
                        
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

