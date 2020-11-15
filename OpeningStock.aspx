<%@ Page Title="Online Milk Test|OpeningStock"  Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OpeningStock.aspx.cs" Inherits="OpeningStock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

 
 <link type="text/css" href="App_Themes/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

        
   
        
   
    
   
          
          





<div style="width:100%;">
	<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%" colspan="2"><br />
                <p class="subheading" style="line-height: 150%">
                     
                    OPENING STOCK DETAILS</p>
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
        <tr>
            <td width="100%" height="7" colspan="2">
            </td>
        </tr>
        
        <tr>
            <td width="100%" colspan="2">
            </td>
    </tr>
    </table>
      </div>
      <div style="width:100%;">

    <table border="0" width="100%" id="Datetable" cellspacing="1">
        <tr>
            <td width="35%">
            </td>
            <td width="28%" class="fontt">
            </td>
            <td width="20%" class="fontt" align="right">
                Date
            </td>
            <td width="12%">
               <asp:TextBox ID="txt_fromdate" runat="server" ></asp:TextBox>
                                
            </td>
            <td width="5%">
                <asp:ImageButton ID="popupcal1" runat="server" ImageUrl="~/calendar.gif" Height="20px" />
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_fromdate"
                                PopupButtonID="popupcal1" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
            </td>
        </tr>
    </table>

                  
   </div>
   <br />
   
  
   
   	<div align="center" style="width:100%;">
   	<fieldset  class="openingstocklegend">
    <legend class="fontt">OPENING STOCK</legend>
    <table border="0"  id="table2"  width="100%">
     
    <tr>
    <td width="10%"></td>
     <td  style="width:20%;" align="left" class="fontt">&nbsp;</td>
     <td align="left" class="style1">
     <asp:TextBox ID="txt_stockdetails" runat="server" TabIndex="1" Visible="False">godown details</asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="txt_stockdetails" ValidationExpression="^[a-zA-Z''-'\s]{1,40}$"></asp:RequiredFieldValidator>
         
        
        
        </td>
  
     <td width="20%" align="left" class="fontt">
         <asp:TextBox ID="txt_Fatkg" runat="server" TabIndex="9" Visible="False"></asp:TextBox>
        </td>
  
    </tr>
    <tr>
    <td width="7%"></td>
     <td width="10%" align="left" class="fontt">PLANTNAME</td>
     <td align="left" class="style1">
     <asp:TextBox ID="txt_PlantName" runat="server" TabIndex="2" Enabled="False"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ControlToValidate="txt_PlantName" ValidationExpression="^[a-zA-Z''-'\s]{1,40}$"></asp:RequiredFieldValidator>
        
         
        </td>
  
     <td width="30%" align="left" class="fontt">
         <asp:TextBox ID="txt_Snfkg" runat="server" TabIndex="10" Visible="False"></asp:TextBox>
        </td>
  
    </tr>

     
    
 
    
    
  
    <tr>
   <td width="7%"></td>
    <td width="10%" align="left" class="fontt"> MILK KG</td>
     <td align="left" class="style1">
    <asp:TextBox ID="txt_Milkkg" runat="server" TabIndex="3" >0</asp:TextBox>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txt_Milkkg" ValidationExpression="^[0-9,.]{1,10}$"></asp:RequiredFieldValidator >
         
        </td>
   
    
    
     <td width="30%" align="left" class="fontt">
         &nbsp;</td>
   
    
    
    </tr>
  
 
    <tr>
  <td width="7%"></td>
     <td width="10%" align="left" class="fontt">FAT</td>
     <td align="left" class="style1">
     <asp:TextBox ID="txt_fat" runat="server" TabIndex="4" 
             ontextchanged="txt_fat_TextChanged">0</asp:TextBox>
     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txt_fat" ValidationExpression="^[0-9,.]{1,10}$"></asp:RequiredFieldValidator >
      </td>
   
     <td width="30%" align="left" class="fontt">
         &nbsp;</td>
   
    </tr>
  
    <tr>
    <td width="7%"></td>
    <td width="10%" align="left" class="fontt">SNF</td>
    <td align="left" class="style1">
    <asp:TextBox ID="txt_SNF" runat="server" TabIndex="5" AutoPostBack="True" 
            ontextchanged="txt_SNF_TextChanged">0</asp:TextBox>

    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txt_SNF" ValidationExpression="^[0-9,.]{1,10}$"></asp:RequiredFieldValidator >
    </td>
   
    
    <td width="30%" align="left" class="fontt">
        &nbsp;</td>
   
    
    </tr>
    <tr>
   <td width="7%"></td>
   <td width="10%" align="left" class="fontt">CLR</td>
   <td align="left" class="style1">
    <asp:TextBox ID="txt_Clr" runat="server" TabIndex="6">0</asp:TextBox>

     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="txt_Clr" ValidationExpression="^[0-9,.]{1,10}$"></asp:RequiredFieldValidator >
   </td>
  
   <td width="30%" align="left" class="fontt">
       &nbsp;</td>
  
    </tr>

   
    
   
    <tr>
    <td width="7%"></td>
     <td width="10%" align="left" class="fontt">&nbsp;</td>
     <td align="left" class="fontt">
      <asp:TextBox ID="txt_rate" runat="server" TabIndex="7" AutoPostBack="True" 
             ontextchanged="txt_rate_TextChanged" Visible="False">23.50</asp:TextBox> 
       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txt_rate" ValidationExpression="^[0-9,.]{1,10}$"></asp:RequiredFieldValidator >

       
        
        </td>
  
     <td width="30%" align="left" class="fontt">
         &nbsp;</td>
  
    </tr>
  

   <tr>
    <td width="7%"></td>
     <td width="10%" align="left" class="fontt">&nbsp;</td>
     <td align="left" class="fontt">
     <asp:TextBox ID="txt_Amount" runat="server" TabIndex="8" Visible="False">45000</asp:TextBox> &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="txt_Amount" ValidationExpression="^[0-9,.]{1,10}$"></asp:RequiredFieldValidator >
      
        
        </td>
  
     <td width="30%" align="left" class="fontt">
         &nbsp;</td>
  
    </tr>
    
  
     
    
    </table>
    <table width="100%">
    <tr>
     <td width="40%"></td>
     <td width="7%" align="left" class="fontt">
    
  
    <asp:Button ID="btn_Save" runat="server" onclick="btn_Save_Click" 
        Text="Save" Width="50px" style="height: 26px" OnClientClick="return confirm('Are you sure you want to Lock this Values?');"  BackColor="#6F696F" Font-Bold="False" ForeColor="White" TabIndex="8"  />
    
        </td>
    <td width="15%" align="right" class="fontt">
    
  
        &nbsp;</td>
     <td width="38%"></td>
    </tr>
    <tr>
    <td width="30%"></td>
   <td width="10%"></td>
   <td width="12%" align="center"> 
   
    <%-- <asp:HyperLink ID="HyperLink2" runat="server"  
NavigateUrl="~/NewRateChartViewer.aspx" ImageUrl= "Image/viewn.gif" Visible="False"></asp:HyperLink>--%>
            
           </td>
    <td width="38%"></td>
    
    </tr>
    </table>
     </fieldset>
     </div>



    <%--<table>
     <tr>
   <td width="50%">
   <asp:GridView ID="GridView1" runat="server"             
        style="text-align: center" AllowPaging="True" 
            Height="79px" PageSize="5" 
           AutoGenerateColumns="False" DataKeyNames="Tid" 
           DataSourceID="SqlDataSource1" CellPadding="4" EnableModelValidation="True" 
           ForeColor="#333333" GridLines="None" >
       <AlternatingRowStyle BackColor="White" />
       <Columns>
           <asp:BoundField DataField="Tid" HeaderText="Tid" InsertVisible="False" 
               ReadOnly="True" SortExpression="Tid" />
           <asp:BoundField DataField="Datee" HeaderText="Date" SortExpression="Datee" DataFormatString="{0:d}" />
           <asp:BoundField DataField="Fat" HeaderText="Fat" SortExpression="Fat" />
           <asp:BoundField DataField="MilkKg" HeaderText="MilkKg" 
               SortExpression="MilkKg" />
           <asp:BoundField DataField="Snf" HeaderText="Snf" SortExpression="Snf" />
           <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
           <asp:BoundField DataField="Amount" HeaderText="Amount" 
               SortExpression="Amount" />
           <asp:BoundField DataField="Storage_Name" HeaderText="Storage_Name" 
               SortExpression="Storage_Name" />
           <asp:BoundField DataField="Plant_Name" HeaderText="Plant_Name" 
               SortExpression="Plant_Name" />
           <asp:BoundField DataField="Fat_Kg" HeaderText="Fat_Kg" 
               SortExpression="Fat_Kg" />
           <asp:BoundField DataField="Snf_Kg" HeaderText="Snf_Kg" 
               SortExpression="Snf_Kg" />
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
           SelectCommand="SELECT [Tid], [Datee], [Fat], [MilkKg], [Snf], [Rate], [Amount], [Storage_Name], [Plant_Name], [Fat_Kg], [Snf_Kg] FROM [Stock_openingmilk]">
       </asp:SqlDataSource>
   
       
   
      
   
   </td>      
    </tr>
    </table>--%>
   
	
  
    <br />
    <br />
    <center>
        <div class="grid">
            <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <mcn:DataPagerGridView ID="GridView1" runat="server" OnRowDataBound="RowDataBound"
                        AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" CssClass="datatable"
                        CellPadding="0" BorderWidth="0px" GridLines="None" DataSourceID="NEWOPSTOCK"
                        PageSize="5">
                        <Columns>
           <asp:BoundField DataField="Tid" HeaderText="Tid" InsertVisible="False" 
               ReadOnly="True" SortExpression="Tid" />
           <asp:BoundField DataField="Datee" HeaderText="Date" SortExpression="Datee" DataFormatString="{0:d}" />
           <asp:BoundField DataField="Fat" HeaderText="Fat" SortExpression="Fat" />
           <asp:BoundField DataField="MilkKg" HeaderText="MilkKg" 
               SortExpression="MilkKg" />
           <asp:BoundField DataField="Snf" HeaderText="Snf" SortExpression="Snf" />
           <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
           <asp:BoundField DataField="Amount" HeaderText="Amount" 
               SortExpression="Amount" />
           <asp:BoundField DataField="Storage_Name" HeaderText="Storage_Name" 
               SortExpression="Storage_Name" />
           <asp:BoundField DataField="Plant_Name" HeaderText="Plant_Name" 
               SortExpression="Plant_Name" />
           <asp:BoundField DataField="Fat_Kg" HeaderText="Fat_Kg" 
               SortExpression="Fat_Kg" />
           <asp:BoundField DataField="Snf_Kg" HeaderText="Snf_Kg" 
               SortExpression="Snf_Kg" />
       </Columns>
                        <PagerSettings Visible="False" />
                        <RowStyle CssClass="row" />
                    </mcn:DataPagerGridView>
                    <div class="pager">
                        <asp:DataPager ID="pager" runat="server" PageSize="5" PagedControlID="GridView1">
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
    </center>
    <br />
    <br />
    
    <asp:SqlDataSource ID="NEWOPSTOCK" runat="server" ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>"
     SelectCommand="SELECT [Tid], CONVERT(VARCHAR(10),[Datee],103) AS[Datee], [Fat], [MilkKg], [Snf], [Rate], [Amount], [Storage_Name], [Plant_Name], [Fat_Kg], [Snf_Kg] FROM [Stock_openingmilk]  WHERE (([Company_code] = @Company_code) AND ([Plant_Code] = @Plant_Code)) ORDER BY Tid DESC">
        
        
        <SelectParameters>
            <asp:SessionParameter DefaultValue="Company_code" Name="Company_code" SessionField="Company_code"
                Type="Int32" />          
            <asp:SessionParameter DefaultValue="Plant_Code" Name="Plant_Code" 
                SessionField="Plant_Code" Type="String" />
            
        </SelectParameters>
        
      
        
    </asp:SqlDataSource>
    
    <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
    
   

      
    
        
   
    

    

   

   

    

    
</asp:Content>

