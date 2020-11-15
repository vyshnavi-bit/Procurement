<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LoanDueRecovery.aspx.cs" Inherits="LoanDueRecovery" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="App_Themes/EditGrid.css" rel="stylesheet" />
    
    <style type="text/css">
        .txtClass
        {
           width:150px;
           height:30px; 
           padding-left:10px;
           border:1px solid gray;
           border-collapse:collapse;
        }
       .classfont
          {
              width:40%;
              text-align:center;
          }

        .style1
        {
            text-align: left;
        }
        .style2
        {
            text-align: right;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="updPanel"
        runat="server">
            <ProgressTemplate>           
            <img alt="progress" src="ajax-loader.gif"/>
               Processing...           
            </ProgressTemplate>
        </asp:UpdateProgress>
        


            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                <tr>
                    <td width="100%" colspan="2">
                      
                        <p class="subheading" style="line-height: 150%">
                            &nbsp;&nbsp;LOANDUE RECOVERY
                        </p>
                    </td>
                </tr>
                <tr>
                    <td width="100%" height="3px" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td width="100%" class="line" height="1px" colspan="2">
                    </td>
                </tr>
                <tr align=center>
                    <td width="100%" height="7" colspan="2">
                  
                  <table width=50% ALIGN=center>
        <tr width=50%>
        <th  width=25% class="style2">
            Reference No :
        </th>
        <th  width=25% class="style1">
    <asp:TextBox ID="txt_RefNo" runat="server" ontextchanged="txt_RefNo_TextChanged" ></asp:TextBox>                   
        <asp:RegularExpressionValidator ID="RegularExpressionValidator12" 
            runat="server" ControlToValidate="txt_RefNo" ErrorMessage="Numeric only..." 
            ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
        </th>
        </tr>
          <tr width=50%>
        <th  width=25% class="style2">
            LoanRecovery Date :
        </th>
        <th  width=25% class="style1">
    <asp:TextBox ID="txt_LoanRecoveryDate" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_LoanRecoveryDate"
                                PopupButtonID="txt_LoanRecoveryDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                    </asp:CalendarExtender>
        </th>
        </tr>
          <tr width=50%>
        <th  width=25% class="style2">
            PlantName :
        </th>
        <th  width=25% class="style1">
    <asp:DropDownList ID="ddl_Plantname" AutoPostBack="true" runat="server"  
             onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
            Width="170px">
                                    </asp:DropDownList>    
    <asp:DropDownList ID="ddl_Plantcode" 
            AutoPostBack="true" runat="server" 
             Width="15px" Visible="false">
                                    </asp:DropDownList>                                      
        </th>
        </tr>
          <tr width=50%>
        <th  width=25% class="style2">
            RouteName :
        </th>
        <th  width=25% class="style1">
    <asp:DropDownList ID="cmb_RouteName" AutoPostBack="true" runat="server"               
            Width="170px" onselectedindexchanged="cmb_RouteName_SelectedIndexChanged">   </asp:DropDownList>    
    <asp:DropDownList ID="cmb_RouteID" AutoPostBack="true" runat="server" 
             Width="7px" Visible="false">   </asp:DropDownList>                                      
        </th>
        </tr>
          <tr width=50%>
        <th  width=25% class="style2">
            AgentName :
        </th>
        <th  width=25% class="style1">
    <asp:DropDownList ID="ddl_AgentName" AutoPostBack="true" runat="server"               
            Width="170px" onselectedindexchanged="ddl_AgentName_SelectedIndexChanged" >   </asp:DropDownList>    
    <asp:DropDownList ID="ddl_AgentId" AutoPostBack="true" runat="server" 
             Width="7px" Visible="false">   </asp:DropDownList>                                      
        </th>
        </tr>
          <tr width=50%>
        <th  width=25% class="style2">
            LoanId :
        </th>
        <th  width=25% class="style1">
        <asp:TextBox ID="txt_LoanId" runat="server" 
            ontextchanged="txt_LoanId_TextChanged"></asp:TextBox>                                     
        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" 
            ControlToValidate="txt_LoanId" ErrorMessage="Numeric only..." 
            ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
        </th>
        </tr>
          <tr width=50%>
        <th  width=25% class="style2">
            InstalAmount :
        </th>
        <th  width=25% class="style1">
        <asp:TextBox ID="txt_InstalAmount" runat="server" ></asp:TextBox>                                     
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="txt_InstalAmount" ErrorMessage="Numeric only..." 
            ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
        </th>
        </tr>
          <tr width=50%>
        <th  width=25% class="style2">
            DueBalance :
        </th>
        <th  width=25% class="style1">
        <asp:TextBox ID="txt_DueBalance" runat="server" 
            ontextchanged="txt_DueBalance_TextChanged"></asp:TextBox>                                     
        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" 
            ControlToValidate="txt_DueBalance" ErrorMessage="Numeric only..." 
            ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
        </th>
        </tr>
          <tr width=50%>
        <th  width=25% class="style2">
            DueRecovery Amount :
        </th>
        <th  width=25% class="style1">
        <asp:TextBox ID="txt_DueRecoveryAmount" runat="server" AutoPostBack="true" 
            ontextchanged="txt_DueRecoveryAmount_TextChanged"></asp:TextBox>                                     
        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" 
            runat="server" ControlToValidate="txt_DueRecoveryAmount" 
            ErrorMessage="Numeric only..." ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
        </th>
        </tr>
          <tr width=50%>
        <th  width=25% class="style2">
            Balance :
        </th>
        <th  width=25% class="style1">
        <asp:TextBox ID="txt_Balance" runat="server" 
            ontextchanged="txt_Balance_TextChanged" style="height: 22px"></asp:TextBox>                                     
        <asp:RegularExpressionValidator ID="RegularExpressionValidator11" 
            runat="server" ControlToValidate="txt_Balance" 
            ErrorMessage="Numeric only..." ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
        </th>
        </tr>
          <tr width=50%>
        <th  width=25% class="style2">
            Remarks :
        </th>
        <th  width=25% class="style1">
        <asp:TextBox ID="txt_Remarks" runat="server" Width="351px"></asp:TextBox>                                     
        </th>
        </tr>
        </table>

                    </td>
                </tr>
            </table>
        
   
    <br />

    <table width="100%">
        <tr>
            <td width="42%">
                &nbsp;</td>
            <td width="5%" align="center">
                <asp:Button ID="btn_Save" runat="server"   
            BackColor="#6F696F"  ForeColor="White"
            Text="SUBMIT" Width="68px" TabIndex="2" onclick="btn_Save_Click" 
                      />        
       
            </td>
            <td width="5%" align="center">
         <asp:Button ID="btn_clear" runat="server"   
            BackColor="#6F696F"  ForeColor="White"
            Text="RESET" Width="62px" TabIndex="2" onclick="btn_clear_Click"/>
       
           </td>
         <td width="48%" style="float:left;">
             &nbsp;</td>

        </tr>
    </table>
   

    <br />
    <center>
    <div class="grid">
    <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
    <mcn:DataPagerGridView ID="GridView1" runat="server"
                    AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" CssClass="datatable"                 
                    onrowdeleting="GridView1_RowDeleting" >
                     <Columns>
                     <asp:BoundField HeaderText="Ref_Id" DataField="LoanDueRef_Id" SortExpression="LoanDueRef_Id"
                            HeaderStyle-CssClass="first" ItemStyle-CssClass="first">
                        <HeaderStyle CssClass="first" />
                        <ItemStyle CssClass="first" />
                        </asp:BoundField>
                    
        <asp:BoundField DataField="Agent_Id" HeaderText="AgentId " 
            SortExpression="Agent_Id" />        
        <asp:BoundField DataField="LoanRecovery_Date" HeaderText="LRecoDate" 
                             SortExpression="LoanRecovery_Date" />        
        <asp:BoundField DataField="loan_Id" HeaderText="loanId " 
            SortExpression="loan_Id" />
            <asp:BoundField DataField="LoanDue_Balance" HeaderText="LDueBal" 
            SortExpression="LoanDue_Balance" />
            <asp:BoundField DataField="LoanDueRecovery_Amount" HeaderText="LDueRecAmt" 
            SortExpression="LoanDueRecovery_Amount" />            
            <asp:BoundField DataField="LoanBalance" HeaderText="LBalance" 
            SortExpression="LoanBalance" />
            <asp:BoundField DataField="Remarks" HeaderText="Remarks" 
            SortExpression="Remarks" />
           <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
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
     <asp:SqlDataSource ID="RateChartViewDS" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>" 
                    
            
            SelectCommand="SELECT [LoanDueRef_Id],[Agent_Id],[LoanRecovery_Date],loan_Id,CAST([LoanDue_Balance] AS DECIMAL(18,2)) AS [LD_B],CAST(LoanDueRecovery_Amount) AS DECIMAL(18,2) AS LDR_A,CAST(LoanBalance) AS DECIMAL(18,2) AS LB,Remarks FROM [LoanDue_Recovery]"
            ProviderName="<%$ ConnectionStrings:AMPSConnectionString.ProviderName %>">
                </asp:SqlDataSource>
    
    <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
</asp:Content>


