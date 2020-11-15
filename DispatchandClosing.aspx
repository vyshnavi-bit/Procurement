<%@ Page  Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DispatchandClosing.aspx.cs" Inherits="DispatchandClosing" Title="Online Milk Test|Despatch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="stylesheet" />
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
        .style6
        {
            width: 410px;
            height: 59px;
        }
        .style7
        {
            height: 59px;
        }
        .style11
        {
            width: 62px;
            height: 59px;
        }
        .style13
        {
            color: #3399FF;
        }
        .style15
    {
        font-weight: normal;
    }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
<div style="width:100%;">
	<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%">
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;<span class="style15"><strong>DISPATCH</strong></span>
                </p>
            </td>
        </tr>
        
        <tr>
            <td width="100%" class="line" height="1px">
            </td>
        </tr>
        </table>
      </div>
      <div style="width:100%;">    
    </div>
    <div style="width:100%;">
                  <table   width="100%">
                  <tr>
                  <td width="18%">&nbsp;</td>
                   <td width="10%" class="fontt">
										&nbsp;</td>
										
                   <td width="10%" class="fontt" align="right">
                                      &nbsp;</td>
										
                    <td width="15%" class="fontt" align="right">

                       

                      <asp:CheckBox ID="despatchstock_chk" runat="server" AutoPostBack="True" 
              oncheckedchanged="despatchstock_chk_CheckedChanged" 
               Text="Despatch" CssClass="style13" Font-Bold="False" Checked="True"  />  


      
                            </td>
										
                    <td width="10%" class="fontt" align="right">


      
          <asp:CheckBox ID="closestock_chk" runat="server" AutoPostBack="True" 
              oncheckedchanged="closestock_chk_CheckedChanged" Text="Closing" 
                          CssClass="style13" Font-Bold="False" />
                                      </td>
                       <td width="9%" class="fontt">
       
                              &nbsp;</td>
                             
                               <td width="15%" class="fontt" align="right">
                                   &nbsp;</td>
                                <td width="71%">
                                    &nbsp;</td>
                         
                       </tr>
                  <tr>
                  <td width="18%">&nbsp;</td>
                   <td width="10%" class="fontt">
										&nbsp;</td>
										
                   <td width="10%" class="fontt" align="right">
                                      <asp:Label ID="lbl_session10" runat="server" ForeColor="#3399FF" 
                          Text="Date" Font-Bold="False" style="font-size: small"></asp:Label>
                                      </td>
										
                    <td width="15%" class="fontt" align="right">

                       
                              
                 <asp:TextBox ID="txt_fromdate" runat="server" TabIndex="12"></asp:TextBox>
                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
                             
                            </td>
										
                    <td width="10%" class="fontt" align="right">
                                      <asp:Label ID="lbl_session9" runat="server" ForeColor="#3399FF" 
                          Text="Avail Milk" Font-Bold="False" style="font-size: small"></asp:Label>
                                      </td>
                       <td width="9%" class="fontt">
       
                           <asp:TextBox ID="txt_avail" 
                          runat="server" Height="21px" 
              Width="126px" style="text-align: right"></asp:TextBox>
         
                              </td>
                             
                               <td width="15%" class="fontt" align="right">
                                   &nbsp;</td>
                                <td width="71%">
                                    <asp:Label 
                                          ID="lbl_session4" runat="server" ForeColor="#3399FF" 
                                          Text="Ses" Visible="False"></asp:Label>
                                      <asp:DropDownList ID="dl_Session1" runat="server" Height="16px" 
                                          onselectedindexchanged="dl_Session_SelectedIndexChanged" TabIndex="3" 
                                          Width="30px" Visible="False">
                                          <asp:ListItem Value="AM">AM</asp:ListItem>
                                          <asp:ListItem Value="PM">PM</asp:ListItem>
                                      </asp:DropDownList>
                                      </td>
                         
                       </tr>
                       <tr>
                  <td width="18%">&nbsp;</td>
                   <td width="10%" class="fontt">

                     <asp:DropDownList ID="ddl_Plantcode" AutoPostBack="true" runat="server" 
                       Visible="false" Height="16px" Width="29px"> </asp:DropDownList>                        
                           </td>
										
                   <td width="10%" class="fontt" align="right">
                                      <asp:Label ID="Label1" runat="server" ForeColor="#3399FF" 
                          Text="From" Font-Bold="True" style="font-size: small"></asp:Label>
                                      </td>
										
                    <td width="15%" class="fontt" align="right">                      

                    <asp:DropDownList ID="DDL_Plantfrom" runat="server" TabIndex="1" Height="21px" 
            Width="140px" Enabled="true" Font-Bold="False" 
                            ForeColor="Black" AutoPostBack="True" 
                            onselectedindexchanged="DDL_Plantfrom_SelectedIndexChanged"  >
        
    </asp:DropDownList>
                            </td>
										
                    <td width="10%" class="fontt" align="right">
                                      <asp:Label ID="lbl_ToPlant" runat="server" ForeColor="#3399FF" 
                          Text="To" Font-Bold="True" style="font-size: small"></asp:Label>
                                      </td>
                       <td width="9%" class="fontt">
       
                              <asp:DropDownList ID="DDl_Plantto" runat="server" TabIndex="2" Height="21px" 
                                       Width="126px" >

        <asp:ListItem Value="0">KALASTHIRI</asp:ListItem>
       
       
                                  <asp:ListItem Value="1">SANGAM</asp:ListItem>
                                  <asp:ListItem Value="2">VIRA</asp:ListItem>
       
       
                                  <asp:ListItem>ONGOLE</asp:ListItem>
       
       
    </asp:DropDownList>
         
                              </td>
                             
                               <td width="15%" class="fontt" align="right">
                                   &nbsp;</td>
                                <td width="71%">
                                    &nbsp;</td>
                         
                       </tr>
                         
                   </table>
   </div>




     
      <div style="width:100%;">
                  <table   width="100%">          
                  <tr>
                  <td>
                      <table class="style4">
                          <tr>
                              <td class="style11">
                                  &nbsp;</td>
                              <td class="style6">
                                  <asp:Panel ID="Panel1" runat="server" Height="255px" style="color: #FFFFFF" 
                                      Width="350px" BorderStyle="Double" ForeColor="#333300" 
                                      BorderColor="White" EnableViewState="False" ViewStateMode="Disabled">

                                      <div class="legdespatch">
 <fieldset class="fontt">
    <legend class="fontt">Despatch</legend>
       <table border="0" width="100%" id="table12" cellspacing="1">                          
            <tr>
           <td width="20%" class="style1" align="right" > 
                                      <asp:Label ID="lbl_session" runat="server" 
                   ForeColor="#3399FF" Text="Session"></asp:Label>
           </td>  
           <td width="30%" class="style1">  
               <asp:DropDownList ID="dl_Session" runat="server" Height="23px" 
                   onselectedindexchanged="dl_Session_SelectedIndexChanged" TabIndex="3" 
                   Width="58px">
                   <asp:ListItem Value="AM">AM</asp:ListItem>
                   <asp:ListItem Value="PM">PM</asp:ListItem>
               </asp:DropDownList>
           </td>
          
           </tr>
            <tr>
           <td width="20%" class="style1" align="right"> 
                                      <asp:Label ID="lbl_session0" runat="server" 
                   ForeColor="#3399FF" Text="Milk Kg"></asp:Label>
           </td>  
           <td width="30%" class="style1">  
                                      <asp:TextBox ID="txt_Milkkg" runat="server" 
                                          ontextchanged="txt_Milkkg_TextChanged" TabIndex="4" height="21px" 
                                          width="126px"></asp:TextBox>
           </td> 
           </tr>
            <tr>
           <td width="20%" class="style1" align="right"> 
                                      <asp:Label ID="lbl_session1" runat="server" 
                   ForeColor="#3399FF" Text="Fat"></asp:Label>
           </td>  
           <td width="30%" class="style1">  
                                      <asp:TextBox ID="txt_fat" runat="server" TabIndex="5" height="21px" 
                                          width="126px"></asp:TextBox>
           </td> 
           </tr>
            <tr>
           <td width="20%" class="style1" align="right"> 
                                      <asp:Label ID="lbl_session3" runat="server" ForeColor="#3399FF" Text="Clr"></asp:Label>
           </td>  
           <td width="30%" class="style1">  
                                      <asp:TextBox ID="txt_Clr" runat="server" 
                                          ontextchanged="txt_Clr_TextChanged" TabIndex="6" height="21px" 
                                          width="126px" CausesValidation="True" AutoPostBack="True"></asp:TextBox>
           </td> 
           </tr>
            <tr>
           <td width="20%" class="style1" align="right"> 
                                      <asp:Label ID="lbl_session2" runat="server" 
                   ForeColor="#3399FF" Text="Snf"></asp:Label>
           </td>  
           <td width="30%" class="style1">  
                                      <asp:TextBox ID="txt_SNF" runat="server" AutoPostBack="True" height="21px" 
                                          ontextchanged="txt_SNF_TextChanged" TabIndex="7" width="126px"></asp:TextBox>
           </td> 
           </tr>
           <tr>
           <td width="20%" class="style1" style="text-align: right"> 
               <asp:Label ID="lbl_session11" runat="server" ForeColor="#3399FF" Text="Rate"></asp:Label>
           </td>  
           <td width="30%" class="style1">  
                                      <asp:TextBox ID="txt_rate" runat="server" AutoPostBack="True" Height="21px" 
                                          ontextchanged="txt_rate_TextChanged" TabIndex="8" Width="126px"></asp:TextBox>
           </td> 
           </tr>
           <tr>
               <td class="style1" style="text-align: right" width="20%">
                   <asp:Label ID="lbl_session12" runat="server" ForeColor="#3399FF" Text="Amount"></asp:Label>
               </td>
               <td class="style1" width="30%">
                   <asp:TextBox ID="txt_Amount" runat="server" Enabled="False" Height="21px" 
                       TabIndex="9" Width="126px">0</asp:TextBox>
               </td>
           </tr>
           <tr>
               <td class="style1" style="text-align: right" width="20%">
                   <asp:Label ID="lbl_session13" runat="server" ForeColor="#3399FF" 
                       Text="Tanker Num"></asp:Label>
               </td>
               <td class="style1" width="30%">
                   <asp:TextBox ID="txt_tankar" runat="server" Height="21px" TabIndex="9" 
                       Width="126px"></asp:TextBox>
               </td>
           </tr>
           <tr>
               <td class="style1" style="text-align: right" width="20%">
                   &nbsp;</td>
               <td class="style1" width="30%">
                   <asp:Button ID="btn_Save" runat="server" BackColor="#6F696F" Font-Bold="False" 
                       ForeColor="White" Height="21px" onclick="btn_Save_Click" 
                       OnClientClick="return confirm('Are you sure you want to Save this record?');" 
                       style="font-size: small;" TabIndex="10" Text="DISPATCH" Width="75px" />
               </td>
           </tr>
           <tr>
           <td width="20%" class="style1"> 
               &nbsp;</td>  
           <td width="30%" class="style1">  
       
     <asp:Label ID="lbl_plantnane" runat="server" Text="PLANTNAME" Visible="False"></asp:Label>
        
           </td> 
           </tr>
            </table>
 </fieldset>
</div>
                                                                           
                                  </asp:Panel>
                              </td>

                              <td class="style7">
                                  <asp:Panel ID="Panel2" runat="server" Height="249px" style="margin-left: 0px; color: #FFFFFF;" 
                                      Width="350px" BorderStyle="Double" ForeColor="Silver" 
                                      BorderColor="White">                          
                                    

                                    <div class="legclose">
 <fieldset class="fontt">
    <legend class="fontt">Close</legend>
    <table border="0" width="100%" id="table1" cellspacing="1">                          
           <tr>
           <td width="20%" class="style1"> 
           </td>  
           <td width="30%" class="style1">  
           </td> 
           </tr>
            <tr>
           <td width="20%" class="style1"> 
                                      <asp:Label ID="lbl_session5" runat="server" 
                   ForeColor="#3399FF" Text="Milk Kg"></asp:Label>
           </td>  
           <td width="30%" class="style1">  
                                      <asp:TextBox ID="txt_avail1" runat="server" Height="21px" 
                                          style="text-align: left" Width="126px" 
                                          ontextchanged="txt_avail1_TextChanged" 
                   TabIndex="15"></asp:TextBox>
           </td> 
           </tr>
            <tr>
           <td width="20%" class="style1"> 
                                      <asp:Label ID="lbl_session6" runat="server" 
                   ForeColor="#3399FF" Text="Fat"></asp:Label>
           </td>  
           <td width="30%" class="style1">  
                                      <asp:TextBox ID="txt_fat1" runat="server" TabIndex="16" Height="21px" 
                                          Width="126px"></asp:TextBox>
           </td> 
           </tr>
            <tr>
           <td width="20%" class="style1"> 
                                      <asp:Label ID="lbl_session8" runat="server" 
                   ForeColor="#3399FF" Text="Clr"></asp:Label>
           </td>  
           <td width="30%" class="style1">  
                                      <asp:TextBox ID="txt_Clr1" runat="server" AutoPostBack="True" 
                                          ontextchanged="txt_Clr1_TextChanged" TabIndex="17" Height="21px" 
                                          Width="126px" CausesValidation="True"></asp:TextBox>
           </td> 
           </tr>
            <tr>
           <td width="20%" class="style1"> 
                                      <asp:Label ID="lbl_session7" runat="server" ForeColor="#3399FF" Text="Snf"></asp:Label>
           </td>  
           <td width="30%" class="style1">  
                                      <asp:TextBox ID="txt_SNF1" runat="server" AutoPostBack="True" Height="21px" 
                                          ontextchanged="txt_SNF1_TextChanged" TabIndex="18" Width="126px"></asp:TextBox>
           </td> 
           </tr>
            <tr>
           <td width="20%" class="style1"> 
               &nbsp;</td>  
           <td width="30%" class="style1">  
                                      <asp:Button ID="Button1" runat="server" 
                   BackColor="#6F696F" Font-Bold="False" 
                                          ForeColor="White" onclick="Button1_Click" 
                                          OnClientClick="return confirm('Are you sure you want to Save this record?');" 
                                          style="font-size: small;" TabIndex="19" 
                   Text="CLOSING" Height="21px" 
                                          Width="75px" />
           </td> 
           </tr>
            <tr>
           <td width="20%" class="style1"> 
      <asp:TextBox ID="txt_PlantName" runat="server" TabIndex="10" Enabled="False" 
             Visible="False" Height="16px" Width="43px"></asp:TextBox>
      
           </td>  
           <td width="30%" class="style1">  
      
      <asp:Label ID="lbl_Stockdetails" runat="server" Text="STOCK" Visible="False"></asp:Label>

    
<asp:TextBox ID="txt_stockdetails" runat="server" TabIndex="11" Visible="False" Height="18px" 
                   Width="78px">godown  details</asp:TextBox>
      
        
           </td> 
           </tr>
           </table>
    </fieldset>
</div>

                                      </asp:Panel>
                              </td>
                          </tr>
                         
                          </table>
                      </td>
                       </tr>
                      
                   </table>
   </div>

      
      
      

   
  
   
     <br />
     <br />
     <br />

      
      
      

   
  
   
    <br />
    <div align="center">
        <div class="grid">
            <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <mcn:DataPagerGridView ID="GridView1" runat="server" OnRowDataBound="RowDataBound"
                        AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" CssClass="datatable"
                        CellPadding="0" BorderWidth="0px" 
                        PageSize="5" onrowdeleting="GridView1_RowDeleting" 
                        onselectedindexchanged="GridView1_SelectedIndexChanged">
                         <Columns>
                       <asp:BoundField DataField="Tid" HeaderText="SNO" SortExpression="Tid" />
                <asp:BoundField DataField="Plant_To" HeaderText="Plant_To" SortExpression="Plant_To" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                <asp:BoundField DataField="MilkKg" HeaderText="MilkKg" SortExpression="MilkKg" />
                <asp:BoundField DataField="Fat" HeaderText="Fat" SortExpression="Fat" />
                <asp:BoundField DataField="Snf" HeaderText="Snf" SortExpression="Snf" />                   
                <asp:BoundField DataField="Clr" HeaderText="Clr" SortExpression="Clr" />    
                 <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
            </Columns>

                        <PagerSettings Visible="False" />
                        <RowStyle CssClass="row" />

                    </mcn:DataPagerGridView>
                    <div class="pager">
                         <asp:DataPager ID="pager" runat="server" PageSize="20" PagedControlID="GridView1">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="‹Previous"
                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="current"
                                    NextPreviousButtonCssClass="command" />
                                <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="Next›"
                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
     <br />    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>"
      SelectCommand="SELECT [Tid],[Plant_To], CONVERT(VARCHAR(10),[Date],103) AS [Date],[MilkKg], [Fat], [Snf], [Rate], [Amount], [Clr] FROM [Despatchnew] "
        ProviderName="<%$ ConnectionStrings:AMPSConnectionString.ProviderName %>">        
        
    </asp:SqlDataSource>
  </div>
    <div align="center">
        <div class="grid">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <mcn:DataPagerGridView ID="GridView2" runat="server" OnRowDataBound="RowDataBound"
                        AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" CssClass="datatable"
                        CellPadding="0" BorderWidth="0px"
                        PageSize="2" onrowdeleting="GridView2_RowDeleting">
                         <Columns>
                       <asp:BoundField DataField="Tid" HeaderText="SNO" SortExpression="Tid" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                <asp:BoundField DataField="MilkKg" HeaderText="MilkKg" SortExpression="MilkKg" />
                <asp:BoundField DataField="Fat" HeaderText="Fat" SortExpression="Fat" />
                <asp:BoundField DataField="Snf" HeaderText="Snf" SortExpression="Snf" />
                <asp:BoundField DataField="Clr" HeaderText="Clr" SortExpression="Clr" />              
                    
                 <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
            </Columns>
                        <PagerSettings Visible="False" />
                        <RowStyle CssClass="row" />
                    </mcn:DataPagerGridView>
                    <div class="pager">
                        <asp:DataPager ID="DataPager1" runat="server" PageSize="18" PagedControlID="GridView2">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonCssClass="command" FirstPageText="«" PreviousPageText="‹Previous"
                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                    ShowLastPageButton="false" ShowNextPageButton="false" />
                                <asp:NumericPagerField ButtonCount="7" NumericButtonCssClass="command" CurrentPageLabelCssClass="current"
                                    NextPreviousButtonCssClass="command" />
                                <asp:NextPreviousPagerField ButtonCssClass="command" LastPageText="»" NextPageText="Next›"
                                    RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                    ShowLastPageButton="true" ShowNextPageButton="true" />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    

   
    <br />
    
    <asp:SqlDataSource ID="newagentgrid1" runat="server" ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>"

    SelectCommand="SELECT [Tid],CONVERT(VARCHAR(10),[Date],103) AS [Date], [MilkKg], [Fat], [Snf], [Clr], [Rate], [Amount] FROM [Stock_Milk]  ORDER BY Tid DESC">
     
    </asp:SqlDataSource>
    
</div>
    <%--<table>
     <tr>
   <td>
   <asp:GridView ID="GridView1" runat="server"             
        style="text-align: center" AllowPaging="True" 
            Height="79px" PageSize="6" 
           AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CellPadding="4" 
           EnableModelValidation="True" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                <asp:BoundField DataField="Plant_From" HeaderText="Plant_From" 
                    SortExpression="Plant_From" />
                <asp:BoundField DataField="Plant_To" HeaderText="Plant_To" 
                    SortExpression="Plant_To" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                <asp:BoundField DataField="Session" HeaderText="Session" 
                    SortExpression="Session" />
                <asp:BoundField DataField="MilkKg" HeaderText="MilkKg" 
                    SortExpression="MilkKg" />
                <asp:BoundField DataField="Fat" HeaderText="Fat" SortExpression="Fat" />
                <asp:BoundField DataField="Snf" HeaderText="Snf" SortExpression="Snf" />
                <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" 
                    SortExpression="Amount" />
                <asp:BoundField DataField="Clr" HeaderText="Clr" SortExpression="Clr" />
               
                
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
   
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
           ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>" 
           SelectCommand="SELECT [Plant_From], [Plant_To], [Date], [Session], [MilkKg], [Fat], [Snf], [Rate], [Amount], [Clr] FROM [Despatchnew]">
       </asp:SqlDataSource>
   
      
   
   </td>      
    </tr>
    </table>
    <table>
    <tr>
    <td>
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource2" AllowPaging="True"  PageSize="5" 
            CellPadding="4" EnableModelValidation="True" ForeColor="#333333" 
            GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                <asp:BoundField DataField="MilkKg" HeaderText="MilkKg" 
                    SortExpression="MilkKg" />
                <asp:BoundField DataField="Fat" HeaderText="Fat" SortExpression="Fat" />
                <asp:BoundField DataField="Snf" HeaderText="Snf" SortExpression="Snf" />
                <asp:BoundField DataField="Clr" HeaderText="Clr" SortExpression="Clr" />
                <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                <asp:BoundField DataField="Storage_Name" HeaderText="Storage_Name" 
                    SortExpression="Storage_Name" />
                <asp:BoundField DataField="Plant_Name" HeaderText="Plant_Name" 
                    SortExpression="Plant_Name" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" 
                    SortExpression="Amount" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>" 
            SelectCommand="SELECT [Date], [MilkKg], [Fat], [Snf], [Clr], [Rate], [Storage_Name], [Plant_Name], [Amount] FROM [Stock_Milk]">
        </asp:SqlDataSource>
    </td>
    </tr>
    </table>--%>
	
    

     <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
    
</asp:Content>


