<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LedgerCreation.aspx.cs" Inherits="LedgerCreation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <style type="text/css">
        .tb10
        {}
        .style1
        {
            font-family: Andalus;
        }
        .style3
        {
            width: 242px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    
        <strong __designer:mapid="1550">
        <asp:LinkButton ID="LinkButton1" runat="server" align="left" onclick="LinkButton1_Click" 
            style="text-align: left; font-family: Andalus">Back</asp:LinkButton> </strong>

    
    <center style="background-color: #FFFFFF; margin-left: 40px;">
        &nbsp;<asp:Label ID="Label3" runat="server" Text="Accounting Ledger Creation" 
            
            style="font-family: Andalus; font-size: medium; color: #FF3399; text-decoration: underline"></asp:Label>
        <br />
    <fieldset style="background-color: #CCFFFF; width: 331px;">
   
   <table align="center" style="width: 409px">  
 
   <tr>
   
   <td align="left" >
     <asp:Label ID="Label2" runat="server" Text="Plant Name" Font-Size="Small" 
           CssClass="style1" ></asp:Label>   
   </td>
   <td align="left" class="style3">
    <asp:CheckBox ID="chk_Allplant" runat="server" Text="Plant" AutoPostBack="True" 
                     oncheckedchanged="chk_Allplant_CheckedChanged" />
    <br />
                 <asp:DropDownList ID="ddl_Plantname" runat="server" CssClass="tb10" 
                     Height="39px" Width="191px" 
                     onselectedindexchanged="ddl_Plantname_SelectedIndexChanged">
                     <asp:ListItem>--------Select--------</asp:ListItem>
                 </asp:DropDownList>
                
                

   </td>
   
   </tr>
  
   <tr>
   
   <td align="left" class="style5">
       <asp:Label ID="Label10" runat="server" Text="Date" Font-Size="Small" 
           CssClass="style1"></asp:Label>
   </td>
   <td align="left" class="style3">
     
                          <asp:TextBox ID="txt_date" runat="server" TabIndex="4" Width="150px" 
                              CssClass="tb10" Font-Size="X-Small" Enabled="False" Height="24px"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_date_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="TopRight" 
                                TargetControlID="txt_date">
                            </asp:CalendarExtender>
                          </em></strong>
   </td>
   
   </tr>
    <tr>
   
   <td align="left" >
     <asp:Label ID="Label4" runat="server" Text="Head of Account" Font-Size="Small" 
           CssClass="style1" ></asp:Label>   
   </td>
   <td align="left" class="style3">
                       <asp:DropDownList ID="ddl_HeadName" runat="server" CssClass="tb10" Height="39px" Width="191px" onselectedindexchanged="ddl_HeadName_SelectedIndexChanged" > </asp:DropDownList>                      
                       </td>
   
   </tr>


   <tr>
   
   <td align="left">
       <asp:Label ID="Label12" runat="server" Text="Ledger Name" Font-Size="Small" 
           CssClass="style1"></asp:Label>
   </td>
   <td align=left class="style3">

       <asp:TextBox ID="txt_ledger" runat="server" CssClass="tb10" Height="30px" 
           Width="150px" ontextchanged="txt_ledger_TextChanged"></asp:TextBox>

   </td>
   
   </tr>




   <tr>
   
   <td align="left">
       <asp:Label ID="Label13" runat="server" Text="Description" 
           Font-Size="Small" CssClass="style1"></asp:Label>
   </td>
   <td ALIGN="left" class="style3">
       <asp:TextBox ID="txt_desc" runat="server" CssClass="tb10" Height="40px" 
           TextMode="MultiLine" Font-Size="X-Small" Width="150px"></asp:TextBox>
   </td>
   
   </tr>




   <tr>
   
   <td align="left">

                 <asp:DropDownList ID="ddl_Plantcode" runat="server" CssClass="tb10" 
                     Height="20px" Width="70px" AutoPostBack="True" 
                     onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
           Font-Size="X-Small" Visible="False">
                     <asp:ListItem>--------Select--------</asp:ListItem>
                 </asp:DropDownList>

       <asp:TextBox ID="tid" runat="server" CssClass="tb10" Height="22px" 
           Width="54px" ontextchanged="txt_ledger_TextChanged" AutoPostBack="True" 
                     Visible="False"></asp:TextBox>

       </td>
   <td align="CENTER" class="style3">   
         <center> 
             <asp:Button ID="Button1" runat="server" CssClass="button2222" Text="Save" 
                 onclick="Button1_Click" />  </center>
   </td>
   
   </tr>




   </table>
   
   
   
    </fieldset>
        <br />
        <strong __designer:mapid="1550">
                  <em __designer:mapid="15ce">
        <br />
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        DataKeyNames="tid" Font-Size="X-Small" 
        onpageindexchanging="GridView1_PageIndexChanging" 
        onrowdeleting="GridView1_RowDeleting" PageSize="20" EnableViewState="False" 
            onrowcancelingedit="GridView1_RowCancelingEdit1" 
            onrowediting="GridView1_RowEditing" onrowupdating="GridView1_RowUpdating" 
            CssClass="gridview1" Font-Italic="False">
        <Columns>
            <asp:BoundField DataField="Tid" HeaderText="Tid" ReadOnly="True" 
                SortExpression="Tid" >
            <ControlStyle Width="50px" />
            <FooterStyle Width="50px" />
            <HeaderStyle Width="50px" />
            <ItemStyle Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="Plant_Code" HeaderText="Plant_code" 
                SortExpression="Plant_Code" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            <asp:BoundField DataField="LedgerName" HeaderText="LedgerName" ReadOnly="True" 
                SortExpression="LedgerName">
<ControlStyle Width="8px"></ControlStyle>

<FooterStyle Width="8px"></FooterStyle>

<HeaderStyle Width="8px"></HeaderStyle>

<ItemStyle Width="8px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Date" HeaderText="Date" 
                SortExpression="Date" ReadOnly="True" >
            <ControlStyle Width="50px" />
            <FooterStyle Width="50px" />
            <HeaderStyle Width="50px" />
            <ItemStyle Width="50px" />
            </asp:BoundField>
            <asp:BoundField DataField="Narration" HeaderText="Description" 
                SortExpression="Narration" >
            <ControlStyle Width="125px" />
            <FooterStyle Width="125px" />
            <HeaderStyle Width="115px" HorizontalAlign="Left" />
            <ItemStyle Width="115px" HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:CommandField ShowEditButton="True" />
            <asp:CommandField ShowDeleteButton="True" />
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
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </em></strong>
       <br />
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       <br />
    </center>

 
   <center>  
   
   
   
   
   
   
       <br />
   
   
   
   
   
   
      </center>







</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

