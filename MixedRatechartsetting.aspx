<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MixedRatechartsetting.aspx.cs" Inherits="MixedRatechartsetting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<link type="text/css" href="App_Themes/StyleSheet.css" rel="Stylesheet" />

    <style type="text/css">

    .style1
{
    width:500px;
    text-align:center;
    background-color:Gray;
    
    
}
        .style2
        {
            font-family: Andalus;
        }
        .style6
        {
            text-align: left;
        }
        </style>

    <script type="text/javascript">
    	function confirmation() {
    		if (confirm('Are you sure you want to Save ?')) {
    			return true;
    		} else {
    			return false;
    		}
    	}
   </script>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

       
  <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height:10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>  
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td  align ="center" width="100%">
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;FAT SNF MIXED RATE CHART SETTINGS</p>
            </td>
        </tr>
        </table>  
        <center>
        <table WIDHT="50%" ALIGN="center">
        <tr align="center">
        <td>
   <div class="legendvichle">
                <br />
                <fieldset class="style1">
              </legend>

        <table style="background-color: #FFFFFF">
        
        <tr align="right">
        
                   <td width="25%">

        <asp:Label ID="lbl_TruckId0" runat="server" Text="Plant Code" CssClass="style2"></asp:Label>

         </td>
          <td width="25%"  align="left"> 

                <asp:DropDownList ID="ddl_PlantName" runat="server" AutoPostBack="True" 
					CssClass="tb10" Font-Size="12px" 
					onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" TabIndex="2" 
					Width="149px">
				</asp:DropDownList>
                   </td>
        </tr>
        
        	<tr align="right">
				<td width="25%">
					<asp:Label ID="lbl_TruckId1" runat="server" CssClass="style2" Text="Chart Name"></asp:Label>
				</td>
				<td class="style6" width="25%%">
					<asp:DropDownList ID="dpu_chart" runat="server" AutoPostBack="True" 
						CssClass="tb10" Font-Size="12px" TabIndex="2" Width="149px">
					</asp:DropDownList>
				</td>
			</tr>
      
        <tr>
                   <td width="25%">

          
                        <asp:DropDownList ID="ddl_Plantcode" runat="server" CssClass="style6" 
                            Font-Size="X-Small" Height="20px" Visible="False" Width="70px">
                        </asp:DropDownList>

       
        
    
  
            
            </td>
           
           <td width="25%" class="style6"> 
         

<asp:Button ID="Button1" runat="server" Text="Show"  Height="26px"  BackColor="Green" 
				   ForeColor="White" onclick="btn_Insert_Click" BorderStyle="Double" 
				   Font-Bold="True" Style="height: 26px" Width="50px"/>
   
       
        
    
  
            
           </td>
        
        </tr>
      
        </table>
        
         
        </fieldset>
       
        </div>
        </td>
        </tr>
        </table>
        </center> 
           

<div align="center">

   
        
           
                <CENTER>
               


                    <br />
                    <br />

                    <table width=50% ALIGN="center"> 
                    <tr align="center">
                    <th>
                    
                    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
						AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" 
						BorderStyle="None" BorderWidth="1px" CausesValidation="false" CellPadding="3" 
						DataKeyNames="Table_id" Font-Size="12px" 
						onpageindexchanging="GridView2_PageIndexChanging" 
						onrowcancelingedit="GridView2_RowCancelingEdit" 
						onrowediting="GridView2_RowEditing" onrowupdating="GridView2_RowUpdating" 
						PageSize="15" onrowdatabound="GridView2_RowDataBound">
						<FooterStyle BackColor="White" ForeColor="#000066" />
						<HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
						<PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
						<RowStyle ForeColor="#000066" />
						<SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
						<SortedAscendingCellStyle BackColor="#F1F1F1" />
						<SortedAscendingHeaderStyle BackColor="#007DBB" />
						<SortedDescendingCellStyle BackColor="#CAC9C9" />
						<SortedDescendingHeaderStyle BackColor="#00547E" />
						<Columns>
							<asp:TemplateField HeaderText="SNo">
								<ItemTemplate>
									<%#Container.DataItemIndex + 1 %>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:BoundField DataField="Table_ID" HeaderText="Table_ID" ReadOnly="True" 
								SortExpression="Table_ID" />
							<asp:BoundField DataField="Chart_Name" HeaderText="Chart_Name" ReadOnly="True" 
								SortExpression="Chart_Name">
								<ControlStyle Width="100px" />
							</asp:BoundField>
							<asp:BoundField DataField="Chart_Type" HeaderText="Chart_Type" ReadOnly="True" 
								SortExpression="Chart_Type" />
							<asp:BoundField DataField="Min_Fat" HeaderText="Min_Fat" ReadOnly="True" 
								SortExpression="Min_Fat">
								<ControlStyle Width="100px" />
							</asp:BoundField>
							<asp:BoundField DataField="Min_Snf" HeaderText="Min_Snf" ReadOnly="True" 
								SortExpression="Min_Snf" />
							<asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" 
								ReadOnly="True">
								<ControlStyle Width="100px" />
							</asp:BoundField>
							<asp:CheckBoxField DataField="Active" HeaderText="Active" 
								SortExpression="Active" Text="Active" />
							<asp:CommandField ShowEditButton="True" />
						</Columns>
					</asp:GridView>
                    </th>
                    </tr>
                   </table>

					
                    <br />
					<br />
					<br />
					<br />
					<br />
					<br />
					<br />
					<br />
                    </left>
                </ContentTemplate>
            </asp:UpdatePanel>
       
    

</div>
<uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
</asp:Content>
