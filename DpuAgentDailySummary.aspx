<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DpuAgentDailySummary.aspx.cs" Inherits="DpuAgentDailySummary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<style type="text/css">
        #table4
        {
            width: 34%;
        }
    </style>
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
                            <asp:TextBox ID="txt_FromDate" runat="server" CssClass="tb10" Height="20px" 
                                ontextchanged="txt_FromDate_TextChanged" Width="125px" Enabled="False" 
								Font-Size="12px"></asp:TextBox>
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
                            <asp:TextBox ID="txt_ToDate" runat="server" CssClass="tb10" Height="20px" 
                                ontextchanged="txt_ToDate_TextChanged" Width="125px" Enabled="False" 
								Font-Size="12px"></asp:TextBox>
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
                                CssClass="tb10" Font-Bold="True" Font-Size="12px" Height="25px" 
                                onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="150px">
                                <asp:ListItem>---------Select---------</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label7" runat="server" style="font-size: small; color: #FF9966; font-weight: 700;" 
                                Text="Agent Code" Visible="False"></asp:Label>
                            &nbsp;
                            <asp:DropDownList ID="ddl_agentcode" runat="server" AutoPostBack="true" 
                                Font-Bold="True" Font-Size="Large" Height="30px" 
                                onselectedindexchanged="ddl_agentcode_SelectedIndexChanged" Width="200px" 
                                CssClass="tb8" Visible="False">
                                <asp:ListItem>---------Select---------</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                                Height="16px" Visible="false" Width="29px">
                            </asp:DropDownList>
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
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                                AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
                                BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                PageSize="20" CssClass="gridview2" DataKeyNames="Tid" 
                                onpageindexchanging="GridView1_PageIndexChanging" 
                                onrowcancelingedit="GridView1_RowCancelingEdit" 
                                onrowediting="GridView1_RowEditing" onrowupdating="GridView1_RowUpdating" 
                                onselectedindexchanged="GridView1_SelectedIndexChanged" 
                                onrowdatabound="GridView1_RowDataBound" Font-Size="12px" 
								EnableViewState="False">
                                <Columns>
                                    <asp:BoundField DataField="Tid" HeaderText="Tid" SortExpression="Tid" 
                                        ReadOnly="True" />
                                    <asp:BoundField DataField="Date" HeaderText=" Date" 
                                        SortExpression="Date" ReadOnly="True" >
                                    <ControlStyle Width="45px" />
                                    <FooterStyle Width="45px" />
                                    <HeaderStyle Width="45px" />
                                    <ItemStyle Width="45px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="shift" HeaderText="shift" 
                                        SortExpression="shift" ReadOnly="True" >
                                    <ControlStyle Width="8px" />
                                    <FooterStyle Width="8px" />
                                    <HeaderStyle Width="8px" />
                                    <ItemStyle Width="8px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="agent_code" HeaderText="Agentcode" 
										SortExpression="agent_code" ReadOnly="True" />
                                    <asp:BoundField DataField="producer_code" HeaderText="producercode" 
										SortExpression="producer_code" ReadOnly="True" />
                                    <asp:BoundField DataField="Fat" HeaderText="Fat" 
                                        SortExpression="Fat" />
                                    <asp:BoundField DataField="Snf" HeaderText="Snf" SortExpression="Snf" />
									<asp:BoundField DataField="Milk_Kg" HeaderText="MilkKg" 
										SortExpression="Milk_Kg" />
                                    <asp:CommandField ShowEditButton="True" />
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
                            


                        </asp:Panel>
                        </center>

                         <br />
               

           

 <br />
 <br />
            <br />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

