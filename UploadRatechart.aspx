<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UploadRatechart.aspx.cs" Inherits="UploadRatechart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<link type="text/css" href="App_Themes/StyleSheet.css" rel="Stylesheet" />

    <script type="text/javascript">
    	function confirmation() {
    		if (confirm('Are you sure you want to Save ?')) {
    			return true;
    		} else {
    			return false;
    		}
    	}
   </script>

    <style type="text/css">
		.style1
		{
			width: 100%;
		}
	</style>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
<%--
<asp:UpdateProgress ID="UpdateProgress1" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>



 <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>


  
--%>
<div>
	<table class="style1">
		<tr align="center">
			<td colspan="2">



				<br />



				<asp:RadioButtonList ID="RadioButtonList1" runat="server" 
					RepeatDirection="Horizontal" style="font-family: serif; height: 27px;">
					<asp:ListItem Value="1">ChartMaster</asp:ListItem>
					<asp:ListItem Value="2">Ratechart</asp:ListItem>
				</asp:RadioButtonList>
                                    <asp:Button ID="btn_Insert11" runat="server" Text="Show" 
                    BackColor="Green" BorderStyle="Double"
                                        Font-Bold="True" ForeColor="White" Height="26px" 
                    Width="92px"
                                        OnClick="btn_Insert11_Click" />
			</td>
		</tr>
		<tr align="center">
			<td colspan="2">



<body>
<asp:Panel ID="Panel1" runat="server">

<form id="form1" >
<div>
<b>Please Select Excel File: </b>
<asp:FileUpload ID="fileupload1" runat="server" />&nbsp;&nbsp;
<asp:Button ID="Button1" runat="server" Text="Import Data" 
		OnClick="Button1_Click" />
<br />
<asp:Label ID="Label1" runat="server" Visible="False" Font-Bold="True" ForeColor="#009933"></asp:Label><br />
<asp:GridView ID="GridView1" runat="server" Font-Size="14px" PageSize="100">
<HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
<Columns>
                            <asp:TemplateField HeaderText="SNo">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
</asp:GridView>
</div>
</form>
                                    <asp:Button ID="btn_Insert" runat="server" Text="Insert Data" 
                    BackColor="Green" BorderStyle="Double"
                                        Font-Bold="True" ForeColor="White" Height="26px" 
                    Width="92px"
                                        OnClick="btn_Insert_Click" />
</body>

</asp:Panel>
<asp:Panel ID="Panel2" runat="server">

<form id="form2" >
<div>
<b>Please Select Excel File: </b>
<asp:FileUpload ID="fileupload3" runat="server" />&nbsp;&nbsp;
<asp:Button ID="Button2" runat="server" Text="Import Data" 
		OnClick="Button2_Click" />
<br />
<asp:Label ID="Label2" runat="server" Visible="False" Font-Bold="True" 
		ForeColor="#009933"></asp:Label><br />
<asp:GridView ID="GridView2" runat="server" Font-Size="14px" PageSize="100" 
		onselectedindexchanged="GridView2_SelectedIndexChanged">
<HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
<Columns>
                            <asp:TemplateField HeaderText="SNo">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
</asp:GridView>
</div>
</form>
                                    <asp:Button ID="btn_Insert3" runat="server" Text="Insert Data" 
                    BackColor="Green" BorderStyle="Double"
                                        Font-Bold="True" ForeColor="White" Height="26px" 
                    Width="92px"
                                        OnClick="btn_Insert3_Click" />
</body>

</asp:Panel>
		
	</table>
<br />
	<br />
</div>
<%--</ContentTemplate>
   <Triggers>
<asp:PostBackTrigger ControlID="Button1" />
<asp:PostBackTrigger ControlID="Button2" />
<asp:PostBackTrigger ControlID="btn_Insert" />
<asp:PostBackTrigger ControlID="btn_Insert3" />
</Triggers>

	</asp:UpdatePanel>
--%>



<uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
</asp:Content>