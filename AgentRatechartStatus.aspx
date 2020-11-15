<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgentRatechartStatus.aspx.cs" Inherits="AgentRatechartStatus" %>
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
    <br />
    <center style="background-color: #FFFFFF">

    <fieldset style="background-color: #CCFFFF" class="style6">
   
   <table  width="400px" align="center">
      
   <tr>
   
   <td align="right" >

                 <asp:DropDownList ID="ddl_Plantcode" runat="server" CssClass="tb10" 
                     Height="20px" Width="70px" AutoPostBack="True" 
                     onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
           Font-Size="X-Small" Visible="False">
                     <asp:ListItem>--------Select--------</asp:ListItem>
                 </asp:DropDownList>

     <asp:Label ID="Label2" runat="server" Text="Plant Name" Font-Size="Small" 
          style="font-family: Andalus; font-size: medium;" ></asp:Label>   
   </td>
   <td align="LEFT">

                 <asp:DropDownList ID="ddl_Plantname" runat="server" CssClass="tb10" 
                     Height="24px" Width="140px" AutoPostBack="True" 
                     onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
           Font-Size="Small">
                     <asp:ListItem>--------Select--------</asp:ListItem>
                 </asp:DropDownList>

   </td>
   
   </tr>






   <tr>
   
       <td align="right">
       <asp:Label ID="Label14" runat="server" Text="Agent Id" Font-Size="Small" 
          style="font-family: Andalus; font-size: medium;"></asp:Label>
       </td>
   
   <td align="left">
     
                 <asp:DropDownList ID="ddl_agentcode" runat="server" CssClass="tb10" 
                     Height="24px" Width="140px" AutoPostBack="True" 
                     onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
           Font-Size="Small">
                     <asp:ListItem>--------Select--------</asp:ListItem>
                 </asp:DropDownList>

                            <td align="left" class="style5">
     
                          &nbsp;</td>
   
   </tr>






   <tr>
   
       <td align="right">
     <center>  </center>
     
       </td>
   
   <td align="left">
     
                          </em></strong>
    <left>   <asp:Label ID="Label10" runat="server" Text="From Date" Font-Size="Small" 
           style="font-family: Andalus; font-size: medium;"></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   <asp:Label ID="Label11" runat="server" Text="To Date" Font-Size="Small" 
          style="font-family: Andalus; font-size: medium;"></asp:Label>
                          <br />
     
                          <asp:TextBox ID="txt_date" runat="server" TabIndex="4" Width="110px" 
                              CssClass="tb10" Font-Size="Small" 
               ontextchanged="txt_date_TextChanged"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_date_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="TopRight" 
                                TargetControlID="txt_date">
                            </asp:CalendarExtender>
                          </left>
     
                          <asp:TextBox ID="txt_mandate" runat="server" TabIndex="4" Width="110px" 
                              CssClass="tb10" Font-Size="Small"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_mandate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="TopRight" 
                                TargetControlID="txt_mandate">
                            </asp:CalendarExtender>
                            </td>
   
   <td align="left" class="style5">
     
                          &nbsp;</td>
   
   </tr>




   <tr>
   
   <td align="right">

                 &nbsp;</td>
   <td ALIGN="left">   
         <left>  
             <asp:Button ID="Button1" runat="server" CssClass="button2222" Text="Show" 
                 onclick="Button1_Click" Width="75px" />    
             <asp:Button ID="Button2" runat="server" CssClass="button2222" Text="Excel" 
                 onclick="Button2_Click" Width="75px" />  </left>
   </td>
   
   </tr>




   </table>
   
   
   
    </fieldset> 
        <br />
      
    </center>

 
   <center>  
   
   
   
   
   
   
    <strong __designer:mapid="1550">
                  <em __designer:mapid="15ce">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" 
        DataKeyNames="Agent_id" Font-Size="X-Small" PageSize="30" 
           EnableViewState="False" Font-Italic="False" 
           onpageindexchanging="GridView1_PageIndexChanging" 
           onrowcancelingedit="GridView1_RowCancelingEdit"  
           HeaderStyle-ForeColor="orange">
        <Columns>
            <asp:BoundField DataField="Agent_id" HeaderText="Agent Id" ReadOnly="True" 
                SortExpression="Agent_id" >
            <ControlStyle Width="60px" />
            <FooterStyle Width="60px" />
            <HeaderStyle Width="60px" />
            <ItemStyle Width="60px" HorizontalAlign="left" />
          
            </asp:BoundField>
            <asp:BoundField DataField="Ratechartname" HeaderText="Ratechart Name" 
                SortExpression="Ratechartname" >
            <ControlStyle Width="175px" />
            <FooterStyle Width="175px" />
            <HeaderStyle Width="175px" />
           
             <ItemStyle Width="175px" HorizontalAlign="center" />
            </asp:BoundField>
            <asp:BoundField DataField="Prdate" HeaderText="Date" 
                SortExpression="Prdate" ReadOnly="True">
            <ControlStyle Width="75px" />
            <FooterStyle Width="75px" />
            <HeaderStyle Width="75px" />
            <ItemStyle Width="75px" />
            </asp:BoundField>
            <asp:BoundField DataField="RateStatus" HeaderText="RateStatus" 
                SortExpression="RateStatus" />
        </Columns>
        <FooterStyle Font-Size="Small" />

<HeaderStyle ForeColor="Orange"></HeaderStyle>
    </asp:GridView>
    </em></strong>
       <br />
   
   
   
   
   
   
      </center>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

