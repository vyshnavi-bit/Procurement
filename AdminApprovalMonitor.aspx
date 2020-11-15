<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"   EnableEventValidation="true"   CodeFile="AdminApprovalMonitor.aspx.cs" Inherits="AdminApprovalMonitor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style4
        {
            height: 24px;
        }
        
        .style103
        {
            width: 313px;
        }
        
        .gridview2
        {
            width: 797px;
        }
    </style>
    <script language="javascript" type="text/javascript">
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form>
    <body>
        <html>
        <div>
            <center>
                <br />
                <br />
            </center>
        </div>
        <div align="center">
            <center style="border-style: none">
                <asp:Panel ID="Panel1" Width="99%" runat="server" BorderWidth="2px">
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                          BackColor="White" BorderColor="#CCCCCC" BorderStyle="Solid"
                        Font-Size="Small" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                        DataKeyNames="plant_code" OnRowUpdating="GridView1_RowUpdating" 
                        EnableViewState="False" ondatabound="GridView1_DataBound" 
                        onrowcommand="GridView1_RowCommand" 
                        onrowdatabound="GridView1_RowDataBound" CssClass="gridview2">


                         <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="#660033" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />


                        <Columns>
                            <asp:TemplateField HeaderText="SNO" ItemStyle-Width="50">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="plant_code" HeaderText="PCode" ItemStyle-Width="50" SortExpression="plant_code"
                                ReadOnly="True">
                                <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="plant_name" HeaderText="PName" ItemStyle-Width="100" SortExpression="plant_name"
                                ReadOnly="True">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>


                            <asp:TemplateField HeaderText ="Data">
    <ItemTemplate >
    <asp:Label ID="Data1" Text ='<%#Bind("Data") %>' runat ="server" />
    </ItemTemplate>
    <EditItemTemplate>
        <asp:DropDownList ID="Data1"  AppendDataBoundItems="True" runat="server" >
         <asp:ListItem Value="0">0</asp:ListItem>
         <asp:ListItem Value="1">1</asp:ListItem>
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateField> 


                       



                                   <asp:TemplateField HeaderText ="Loan">
    <ItemTemplate >
    <asp:Label ID="Loanstatus" Text ='<%#Bind("Loanstatus") %>' runat ="server" />
    </ItemTemplate>
    <EditItemTemplate>
        <asp:DropDownList ID="Loanstatus1"  AppendDataBoundItems="True" runat="server" >
         <asp:ListItem Value="0">0</asp:ListItem>
         <asp:ListItem Value="1">1</asp:ListItem>
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateField> 





                                   <asp:TemplateField HeaderText ="Deduct">
    <ItemTemplate >
    <asp:Label ID="DedStatus" Text ='<%#Bind("Deductionstatus") %>' runat ="server" />
    </ItemTemplate>
    <EditItemTemplate>
        <asp:DropDownList ID="DedStatus1"  AppendDataBoundItems="True" runat="server" >
         <asp:ListItem Value="0">0</asp:ListItem>
         <asp:ListItem Value="1">1</asp:ListItem>
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateField> 






                                   <asp:TemplateField HeaderText ="Despatch">
    <ItemTemplate >
    <asp:Label ID="DespStatus" Text ='<%#Bind("DespatchStatus") %>' runat ="server" />
    </ItemTemplate>
    <EditItemTemplate>
        <asp:DropDownList ID="DespStatus1"  AppendDataBoundItems="True" runat="server" >
         <asp:ListItem Value="0">0</asp:ListItem>
         <asp:ListItem Value="1">1</asp:ListItem>
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateField> 




                                   <asp:TemplateField HeaderText ="Close">
    <ItemTemplate >
    <asp:Label ID="ClosStatus" Text ='<%#Bind("ClosingStatus") %>' runat ="server" />
    </ItemTemplate>
    <EditItemTemplate>
        <asp:DropDownList ID="ClosStatus1"  AppendDataBoundItems="True" runat="server" >
         <asp:ListItem Value="0">0</asp:ListItem>
         <asp:ListItem Value="1">1</asp:ListItem>
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateField> 





                                   <asp:TemplateField HeaderText ="Trans">
    <ItemTemplate >
    <asp:Label ID="TranStatus" Text ='<%#Bind("TransportStatus") %>' runat ="server" />
    </ItemTemplate>
    <EditItemTemplate>
        <asp:DropDownList ID="TranStatus1"  AppendDataBoundItems="True" runat="server" >
         <asp:ListItem Value="0">0</asp:ListItem>
         <asp:ListItem Value="1">1</asp:ListItem>
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateField> 



                           


                                   <asp:TemplateField HeaderText ="Status">
    <ItemTemplate >
    <asp:Label ID="Status" Text ='<%#Bind("Status") %>' runat ="server" />
    </ItemTemplate>
    <EditItemTemplate>
        <asp:DropDownList ID="Status1"  AppendDataBoundItems="True" runat="server" >
         <asp:ListItem Value="0">0</asp:ListItem>
         <asp:ListItem Value="1">1</asp:ListItem>
          <asp:ListItem Value="2">2</asp:ListItem>
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateField> 
                                   <asp:TemplateField HeaderText ="view">
    <ItemTemplate >
    <asp:Label ID="viewstatus" Text ='<%#Bind("buttonviewstatus") %>' runat ="server" />
    </ItemTemplate>
    <EditItemTemplate>
        <asp:DropDownList ID="viewstatus1"  AppendDataBoundItems="True" runat="server" >
           <asp:ListItem Value="1">1</asp:ListItem>
          <asp:ListItem Value="2">2</asp:ListItem>
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateField> 

 <asp:TemplateField HeaderText ="DPU">
    <ItemTemplate >
    <asp:Label ID="DPU" Text ='<%#Bind("Dpustatus") %>' runat ="server" />










    </ItemTemplate>
    <EditItemTemplate>
        <asp:DropDownList ID="DPU1"  AppendDataBoundItems="True" runat="server" >
           <asp:ListItem Value="1">1</asp:ListItem>
          <asp:ListItem Value="2">2</asp:ListItem>
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateField> 



                            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" ReadOnly="True" />
                            <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                        </Columns>
                        <FooterStyle BackColor="#3AC0F2" Font-Bold="True" ForeColor="Tomato" />
                        <HeaderStyle BackColor="#3AC0F2" ForeColor="#FFFF99" />
                    </asp:GridView>
                    <br />
                </asp:Panel>
                <br />
            </center>
        </div>
        <div align="center">
            <br />
            <br />
        </div>
    </form>
    </body> </html>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
