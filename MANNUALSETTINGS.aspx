<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MANNUALSETTINGS.aspx.cs" Inherits="MANNUALSETTINGS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style5
        {
            height: 32px;
        }
        .style6
        {
            width: 291px;
        }
        .button2222
        {}
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <center style="background-color: #FFFFFF">
        <br />
           <div align="right">
                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="fontt" 
                                            NavigateUrl="~/ManualEntryReport.aspx">Manual Report</asp:HyperLink></div>
    <fieldset style="background-color: #CCFFFF" class="style6">
   
   <table  width="350px" align="center">
   
   <tr>
   
   <td align="right">
       <asp:Label ID="Label1" runat="server" Text="Ref_Id" Font-Size="Small" 
           EnableTheming="False" style="font-family: Andalus; font-size: medium;" 
           Visible="False"></asp:Label>
   </td>

    
   <center> 
   <td align="left">
     
                          `</em></strong><asp:TextBox ID="tid" runat="server" CssClass="tb10" 
                              Enabled="False" Visible="False"></asp:TextBox>
   </td>
   
   </tr>




   <tr>
   
   <td align="right" >
     <asp:Label ID="Label2" runat="server" Text="Plant Name" Font-Size="Small" 
          style="font-family: Andalus; font-size: medium;" ></asp:Label>   
   </td>
   <td align="LEFT">

                 <asp:DropDownList ID="ddl_Plantname" runat="server" CssClass="tb10" 
                     Width="175px" AutoPostBack="True" 
                     onselectedindexchanged="ddl_Plantname_SelectedIndexChanged">
                     <asp:ListItem>--------Select--------</asp:ListItem>
                 </asp:DropDownList>

   </td>
   
   </tr>






   <tr>
   
   <td align="right" class="style5">
       <asp:Label ID="Label10" runat="server" Text="Given Date" Font-Size="Small" 
           style="font-family: Andalus; font-size: medium;"></asp:Label>
   </td>
   <td align="left" class="style5">
     
                          <asp:TextBox ID="txt_date" runat="server" TabIndex="4" 
                              CssClass="tb10"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_date_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="TopRight" 
                                TargetControlID="txt_date">
                            </asp:CalendarExtender>
                          </em></strong>
   </td>
   
   </tr>



   <tr>
   
   <td align="right">
       <asp:Label ID="Label11" runat="server" Text="Manual Date" Font-Size="Small" 
          style="font-family: Andalus; font-size: medium;"></asp:Label>
   </td>
   <td align="left">
     
                          <asp:TextBox ID="txt_mandate" runat="server" TabIndex="4" 
                              CssClass="tb10"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_mandate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="TopRight" 
                                TargetControlID="txt_mandate">
                            </asp:CalendarExtender>
                          </em></strong>
   </td>
   
   </tr>



   <tr>
   
   <td align="right">
       <asp:Label ID="Label12" runat="server" Text="Sessions" Font-Size="Small" 
          style="font-family: Andalus; font-size: medium;"></asp:Label>
   </td>
   <td align=left>

                 <asp:DropDownList ID="ddl_sess" runat="server" CssClass="tb10" Width="170px" 
                     onselectedindexchanged="ddl_Plantname_SelectedIndexChanged">
                     <asp:ListItem>--------Select--------</asp:ListItem>
                     <asp:ListItem>AM</asp:ListItem>
                     <asp:ListItem>PM</asp:ListItem>
                     <asp:ListItem></asp:ListItem>
                 </asp:DropDownList>

   </td>
   
   </tr>




   <tr>
   
   <td align="right">
       <asp:Label ID="Label5" runat="server" Text="Permission Requester" Font-Size="Small" 
          style="font-family: Andalus; font-size: medium;"></asp:Label>
   </td>
   <td align="left">
       <asp:TextBox ID="txt_requst" runat="server" CssClass="tb10"></asp:TextBox>
   </td>
   
   </tr>




   <tr>
   
   <td align="right">
       <asp:Label ID="Label6" runat="server" Text="Permission Given By" Font-Size="Small" 
style="font-family: Andalus; font-size: medium;"></asp:Label>
   </td>
   <td ALIGN="left">
       <asp:TextBox ID="txt_giver" runat="server" CssClass="tb10"></asp:TextBox>
   </td>
   
   </tr>




   <tr>
   
   <td align="right">
       <asp:Label ID="Label13" runat="server" Text="Reason For Manual" 
           Font-Size="Small"  style="font-family: Andalus; font-size: medium;" ></asp:Label>
   </td>
   <td ALIGN="left">
       <asp:TextBox ID="txt_Reason" runat="server" CssClass="tb10"></asp:TextBox>
   </td>
   
   </tr>




   <tr>
   
   <td align="right">

                 <asp:DropDownList ID="ddl_Plantcode" runat="server" CssClass="tb10" 
                     Height="20px" Width="70px" AutoPostBack="True" 
                     onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
           Font-Size="X-Small" Visible="False">
                     <asp:ListItem>--------Select--------</asp:ListItem>
                 </asp:DropDownList>

       </td>
   <td ALIGN="left">   
         <center>  
             <asp:Button ID="Button1" runat="server" CssClass="button2222" Text="Save" 
                 onclick="Button1_Click" Width="75px" />  </center>
   </td>
   
   </tr>




   </table>
   
   
   
    </fieldset> 
       <br />
       <br />
    </center>

 
   <center>  
   
   
   
   
   
   
    <strong __designer:mapid="1550">
                  <em __designer:mapid="15ce">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
        BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        DataKeyNames="Tid" Font-Size="X-Small" 
        onpageindexchanging="GridView1_PageIndexChanging" 
        onrowdeleting="GridView1_RowDeleting" PageSize="8" EnableViewState="False" 
            onrowcancelingedit="GridView1_RowCancelingEdit1" 
            onrowediting="GridView1_RowEditing" onrowupdating="GridView1_RowUpdating" 
            CssClass="gridview1" Font-Italic="False">
        <Columns>
            <asp:BoundField DataField="Tid" HeaderText="Tid" ReadOnly="True" 
                SortExpression="Tid" />
            <asp:BoundField DataField="Plant_Code" HeaderText="Plant_code" 
                SortExpression="Plant_Code" ReadOnly="True">
            <ControlStyle Width="45px" />
            <FooterStyle Width="45px" />
            <HeaderStyle Width="45px" />
            <ItemStyle Width="45px" />
            </asp:BoundField>
            <asp:BoundField DataField="PermissionDate" HeaderText="PermissionDate" 
                SortExpression="PermissionDate" >
            <ControlStyle Width="75px" />
            <FooterStyle Width="75px" />
            <HeaderStyle Width="75px" />
            <ItemStyle Width="75px" />
            </asp:BoundField>
            <asp:BoundField DataField="ManualDate" HeaderText="ManualDate" ReadOnly="True" 
                SortExpression="ManualDate">
            <ControlStyle Width="8px" />
            <FooterStyle Width="8px" />
            <HeaderStyle Width="8px" />
            <ItemStyle Width="8px" />
            </asp:BoundField>
            <asp:BoundField DataField="ReasonForMannual" HeaderText="ReasonForMannual" 
                SortExpression="ReasonForMannual" ReadOnly="True" >
            <ControlStyle Width="125px" />
            <FooterStyle Width="125px" />
            <HeaderStyle Width="125px" />
            <ItemStyle Width="125px" />
            </asp:BoundField>
            <asp:BoundField DataField="RequsterName" HeaderText="RequsterName" 
                SortExpression="RequsterName" >
            <ControlStyle Width="75px" />
            <FooterStyle Width="75px" />
            <HeaderStyle Width="75px" />
            <ItemStyle Width="75px" />
            </asp:BoundField>
            <asp:BoundField DataField="GivererName" HeaderText="GivererName" ReadOnly="True" 
                SortExpression="GivererName" >
            <ControlStyle Width="75px" />
            <FooterStyle Width="75px" />
            <HeaderStyle Width="75px" />
            <ItemStyle Width="75px" />
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
    </em></strong>
       <br />
   
   
   
   
   
   
      </center>


    
    </asp:Content>


