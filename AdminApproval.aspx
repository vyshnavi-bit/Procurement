<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminApproval.aspx.cs" Inherits="AdminApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">
       
 




    </script>




    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
          EnablePageMethods="true">
      </asp:ToolkitScriptManager>

  <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height:10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>


<asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>





<form>
<body>
<html>
    <div>
<center>


    <asp:Label ID="lblmsg" runat="server" Text="Label"></asp:Label>


    <br />


    <br />

</center>

</div>
<div  align="center">
  <asp:Panel id="pnlContents" runat = "server"> 
<center style="border-style: none">
<asp:Panel ID="Panel1"  Width="99%" runat="server"    BorderWidth="2px">
       <br />
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
           CssClass="gridview2" FooterStyle-BackColor="#3AC0F2" 
           FooterStyle-Font-Bold="true" FooterStyle-ForeColor="Tomato" 
           HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White" 
           OnRowDataBound="GridView1_RowDataBound" 
           onselectedindexchanged="GridView1_SelectedIndexChanged" 
           onselectedindexchanging="GridView1_SelectedIndexChanging" 
           BorderStyle="Solid" Font-Size="Small">
           <Columns>
               <asp:TemplateField HeaderText="SNO" ItemStyle-Width="50">
                   <ItemTemplate>
                       <%#Container.DataItemIndex + 1 %>
                   </ItemTemplate>
                   <ItemStyle Width="50px" />
               </asp:TemplateField>
               <asp:BoundField DataField="plant_code" HeaderText="PlantCode" 
                   ItemStyle-Width="50" SortExpression="plant_code">
               <ItemStyle Width="50px" />
               </asp:BoundField>
               <asp:BoundField DataField="plant_name" HeaderText="PlantName" 
                   ItemStyle-Width="100" SortExpression="plant_name">
               <ItemStyle Width="100px" />
               </asp:BoundField>
               <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data" />
               <asp:BoundField DataField="Loanstatus" HeaderText="Loan" 
                   SortExpression="Loanstatus" />
               <asp:BoundField DataField="Deductionstatus" HeaderText="Deductions" 
                   SortExpression="Deductionstatus" />
               <asp:BoundField DataField="DespatchStatus" HeaderText="Despatch" 
                   SortExpression="DespatchStatus" />
               <asp:BoundField DataField="ClosingStatus" HeaderText="Closing" 
                   SortExpression="ClosingStatus" />
               <asp:BoundField DataField="TransportStatus" HeaderText="Transport" 
                   SortExpression="TransportStatus" />
               <asp:BoundField DataField="Status" HeaderText="Status" 
                   SortExpression="Status" />
                   <asp:BoundField DataField="Date" HeaderText="Date" 
                   SortExpression="Date" />



               <asp:CommandField ButtonType="Button" ControlStyle-BackColor="Orange" 
                   FooterStyle-Font-Bold="true" HeaderText="Bill Proceed" 
                   SelectText="Proceed" ShowSelectButton="True" >
               <ControlStyle BackColor="Orange" />
               <FooterStyle Font-Bold="True" />
               </asp:CommandField>
           </Columns>
           <FooterStyle BackColor="#3AC0F2" Font-Bold="True" ForeColor="Tomato" />
           <HeaderStyle BackColor="#3AC0F2" ForeColor="White" />
       </asp:GridView>
       <br />
   </asp:Panel>


    <br />
  </center>
  </asp:panel>
</div>




        </ContentTemplate>
<%--                        <Triggers>
<asp:PostBackTrigger ControlID="Proceed" />
</Triggers>--%>
        </asp:UpdatePanel>
       

<div  align="center">
    <%--<asp:Timer ID="Timer1" runat="server" Interval="50000" ontick="Timer1_Tick">
    </asp:Timer>--%>
    <br />
    <br />
  
</div>
</form>
</body>
</html>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
