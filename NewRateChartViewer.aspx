<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewRateChartViewer.aspx.cs" Inherits="NewRateChartViewer" Title="OnlineMilkTest|View Ratechart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="App_Themes/EditGrid.css" rel="stylesheet" />
    
    <style type="text/css">
        .style3
        {
            color: #3399FF;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div id="main">

	<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
          <td width="100%" colspan="2">
          <br />
          <p style="line-height: 150%"><font class="subheading">&nbsp;&nbsp;RATECHART VIEW
          </font></p>
          </td>
        </tr>
        <tr>
          <td width="100%" height="3px" colspan="2"></td>
        </tr>
        <tr>
          <td width="100%" class="line" height="1px" colspan="2"></td>
        </tr>
        <tr>
          <td width="100%" height="7" colspan="2">&nbsp;</td>
        </tr>
        <tr>
          <td class="td1"> 
              &nbsp;</td></tr></table>
    
 <div class="legagentsms">
   <fieldset class="fontt">   
            <legend style="color: #3399FF">RateChart View </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
            <td>
                &nbsp;</td>
            </tr>
             <tr>
                    <td>
                        &nbsp;</td>
                     <td align="right">
                       &nbsp;<asp:Label ID="Label2" runat="server" Text="Plant Name"></asp:Label>      
                    </td>
                    <td >
                  
                    </td>
                    <td  align="left">
                    	<asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
        Width="200px" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
                 Font-Bold="True" Font-Size="Large" ></asp:DropDownList>

                            </td>
                </tr>  
                  <tr>
                    <td>                                    
                    </td>
                     <td align="right">                   
             <asp:RadioButton ID="rbcow" runat="server" AutoPostBack="True" Checked="True" 
                 Font-Bold="True" ForeColor="#3399FF" oncheckedchanged="rbcow_CheckedChanged" 
                 Text="Cow" CssClass="style3" />
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
             <asp:RadioButton ID="rbbuff" runat="server" AutoPostBack="True" 
                 Font-Bold="True" ForeColor="#3399FF" oncheckedchanged="rbbuff_CheckedChanged" 
                 Text="Buffalo" CssClass="style3" />
                    </td>
                </tr> 
                 <tr>
                    <td>                                    
                    </td>
                     <td align="right"> 
                         <asp:Label ID="Label1" runat="server" Text="RateChart Name"></asp:Label>      
                    </td>
                    <td  >                  
                        &nbsp;</td>
                    <td  align="left"> 
                         <asp:DropDownList ID="dl_RatechartName" 
                 runat="server" AutoPostBack="True" 
            onselectedindexchanged="dl_RatechartName_SelectedIndexChanged" Width="200px" 
                 AppendDataBoundItems="True" Font-Bold="True" Font-Size="Large">
        </asp:DropDownList>

                    </td>
                </tr> 
                  
            <tr>
                    <td>
                    	
                    </td>
                     <td  align="left">
         
             <asp:DropDownList ID="ddl_plantcode" 
        runat="server" Width="26px"    
        AutoPostBack="True" Enabled="False" Visible="False" Height="16px">
    </asp:DropDownList>

                    </td>
                    <td >                         
                    </td>
                    <td  align="left">
             <asp:Button ID="Button1" runat="server" 
              
            Text="Create Ratechart" BackColor="#6F696F" Font-Bold="False" 
               ForeColor="White" Width="120px" onclick="Button1_Click1" TabIndex="2" />
           
                    </td>
                               <td width="12%">
                                   &nbsp;</td>
                </tr>                     
                
            </table>
            <br />
          
   </fieldset>
   </div>   
       
        <br /> 

        <br />
        <center>
      <div class="grid">
        <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
               
               <center>
<div class="pager">
                    <mcn:DataPagerGridView ID="GridView1" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" CssClass="datatable" 
                        onrowcancelingedit="GridView1_RowCancelingEdit" OnRowDataBound="RowDataBound" 
                        onrowdeleting="GridView1_RowDeleting" onrowediting="GridView1_RowEditing" 
                        onrowupdating="GridView1_RowUpdating" Width="615px" 
                        onpageindexchanging="GridView1_PageIndexChanging" PageSize="30">
                        <Columns>
                            <asp:BoundField DataField="Table_ID" HeaderStyle-CssClass="first" 
                                HeaderText="S_ID" ItemStyle-CssClass="first" SortExpression="Table_ID">
                            <HeaderStyle CssClass="first" />
                            <ItemStyle CssClass="first" />
                            </asp:BoundField>
                            <asp:BoundField DataField="From_RangeValue" HeaderText="From Range" 
                                SortExpression="From_RangeValue" />
                            <asp:BoundField DataField="To_RangeValue" HeaderText="To Range" 
                                SortExpression="To_RangeValue" />
                            <asp:BoundField DataField="Rate" HeaderText="Rate" SortExpression="Rate" />
                            <asp:BoundField DataField="Comission_Amount" HeaderText="Com Amount" 
                                SortExpression="Com_Amount" />
                            <asp:BoundField DataField="Bouns_Amount" HeaderText="Bouns Amount" 
                                SortExpression="Bouns_Amount" />
                            <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
                        </Columns>
                        <PagerSettings Visible="False" />
                        <RowStyle CssClass="row" />
                    </mcn:DataPagerGridView>
                </div>
                </center>
                </ContentTemplate>
        </asp:UpdatePanel>
        </div>
         </center>
         <asp:SqlDataSource ID="RateChartViewDS" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>"       
            SelectCommand="SELECT [Table_ID],[From_RangeValue], [To_RangeValue],CAST([Rate] AS DECIMAL (18,2)) AS [Rate],CAST([Comission_Amount] AS DECIMAL(18,2)) AS [Comission_Amount], CAST([Bouns_Amount] AS DECIMAL(18,2)) AS [Bouns_Amount] FROM [Rate_Chart]" 
            ProviderName="<%$ ConnectionStrings:AMPSConnectionString.ProviderName %>">
                </asp:SqlDataSource>
     
        <br />
        <div align="center">
       <table border="0" cellpadding="0" cellspacing="1" width="100%">
       <tr>
       <td  class="td1"></td>
       <td class="altd1"> 
           <asp:Button ID="btn_delete" runat="server" 
               onclick="btn_delete_Click"  
            Text="Delete Ratechart" BackColor="#6F696F" Font-Bold="False" 
               ForeColor="White" Width="120px" TabIndex="1" 
               OnClientClick="return confirm('Are you sure you want to Delete this record?');" 
               Visible="False" /></td>
            <td class="altd2"></td>
            <td class="altd1">
                &nbsp;</td>
            <td class="td1">
             <asp:Button ID="btn_Export" runat="server" 
              
            Text="Export" BackColor="#6F696F" Font-Bold="False" 
               ForeColor="White" Width="120px" onclick="btn_Export_Click" Visible="False"  />
           
            </td>
       </tr>
        
        
        
        </table></div><uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
        </div>
</asp:Content>

