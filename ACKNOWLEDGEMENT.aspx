<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ACKNOWLEDGEMENT.aspx.cs" Inherits="ACKNOWLEDGEMENT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>       
    <asp:LinkButton ID="LinkButton1" runat="server" 
        PostBackUrl="~/Dispatchnew.aspx" style="font-weight: 700">Back</asp:LinkButton>
    <br />

    <center>
    
                        <asp:Panel ID="Panel1" runat="server" Width="600px" BorderColor="#00FFCC" 
                            BorderStyle="Inset" BorderWidth="2px">
                            &nbsp;&nbsp;<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label4" runat="server" 
                                style="font-size: small; color: #660033; font-weight: 700;" Text="From"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                            <asp:TextBox ID="txt_FromDate" runat="server" CssClass="tb10" Height="20px" 
                                ontextchanged="txt_FromDate_TextChanged" Width="125px"></asp:TextBox>
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
                                style="font-size: small; color: #660033; font-weight: 700;" Text="To"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txt_ToDate" runat="server" CssClass="tb10" Height="20px" 
                                ontextchanged="txt_ToDate_TextChanged" Width="125px"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_ToDate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                                TargetControlID="txt_ToDate">
                            </asp:CalendarExtender>
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label 
                                ID="Label6" runat="server" 
                                style="font-size: small; color: #660033; font-weight: 700;" 
                                Text="Plant_Name"></asp:Label>
&nbsp;
                            <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                                CssClass="tb10" Font-Bold="True" Font-Size="Small" Height="30px" 
                                onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="200px">
                                <asp:ListItem>---------Select---------</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                                Height="16px" onselectedindexchanged="ddl_Plantcode_SelectedIndexChanged" 
                                Visible="false" Width="29px">
                            </asp:DropDownList>
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                <br />
                          <center>  
                              <asp:Button ID="btn_Ok" runat="server" CssClass="button2222" 
                                onclick="btn_Ok_Click" Text="Show" />
                            
                            </center>
                            <br />
                        </asp:Panel>

                        </center>
                        <br />
                        <br />
                        <center>
                        <asp:Panel ID="Panel2" runat="server" Width="824px">
                            <div class="style1">
                            <center>
                                <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                    AutoGenerateColumns="False" BackColor="White" 
                                    BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="gridview2" 
                                    DataKeyNames="Tid" Font-Size="12px" ondatabound="GridView1_DataBound" 
                                    onpageindexchanging="GridView1_PageIndexChanging" 
                                    onrowcancelingedit="GridView1_RowCancelingEdit" 
                                    onrowdatabound="GridView1_RowDataBound" onrowediting="GridView1_RowEditing" 
                                    onrowupdating="GridView1_RowUpdating"  HeaderStyle-BackColor="Brown" HeaderStyle-ForeColor="white"
                                    onselectedindexchanged="GridView1_SelectedIndexChanged" PageSize="20" 
                                    EnableViewState="False">
                                    <Columns>
                                        <asp:BoundField DataField="Tid" HeaderText="Tid" ReadOnly="True" 
                                            SortExpression="Tid" />
                                        <asp:BoundField DataField="tcNumber" HeaderText="TcNumber" ReadOnly="True" 
                                            SortExpression="tcNumber">
                                        <ControlStyle Width="45px" />
                                        <FooterStyle Width="45px" />
                                        <HeaderStyle Width="45px" />
                                        <ItemStyle Width="45px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Date" HeaderText="Date" ReadOnly="True" 
                                            SortExpression="Date" />
                                        <asp:BoundField DataField="Milkkg" HeaderText="Mkg" ReadOnly="True" 
                                            SortExpression="Milkkg" />
                                        <asp:BoundField DataField="Fat" HeaderText="Fat" ReadOnly="True" 
                                            SortExpression="Fat" />
                                        <asp:BoundField DataField="snf" HeaderText="Snf" ReadOnly="True" 
                                            SortExpression="snf" />
                                        <asp:BoundField DataField="Rate" HeaderText="Rate" ReadOnly="True" 
                                            SortExpression="Rate" />
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" ReadOnly="True" 
                                            SortExpression="Amount" />
                                        <asp:BoundField DataField="Ack_milkkg" HeaderText="Ack Mkg" 
                                            SortExpression="Ack_milkkg" />
                                        <asp:BoundField DataField="Ack_fat" HeaderText="Ack fat" 
                                            SortExpression="Ack_fat" />
                                        <asp:BoundField DataField="Ack_snf" HeaderText="Ack snf" 
                                            SortExpression="Ack_snf" />
                                        <asp:BoundField DataField="Ack_clr" HeaderText="Ack clr" 
                                            SortExpression="Ack_clr" ReadOnly="True" />
                                        <asp:BoundField DataField="status" HeaderText="Status" 
                                            SortExpression="status" ReadOnly="True" />
                                        <asp:CommandField ShowEditButton="True" />
                                    </Columns>
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                              
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                              
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#330099" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                </asp:GridView>
                                </center>
                            </div>
                          </asp:Panel>
                        </center>

                         <br />
 
 <br />
 <br />
            <br />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

