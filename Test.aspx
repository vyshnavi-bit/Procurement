<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Display Progress Using PageRequestManager</title>  
    <style type="text/css">


.legendpermission
{ background-color:#FFFAFA;
	margin-left:50px;
    margin-right:30px;
    width :818px;	
}
.legendloadimg
{ background-color:#FFFAFA;
  margin-left :300px;
  margin-right:30px;  
  width :800px;  
}
 .fontt
{
	font-family:   Verdana,Arial,Helvetica,sans-serif;
    font-size: 9pt;
    font-weight: normal;
    font-style: normal;
}
 .fontt
{
	font-family:   Verdana,Arial,Helvetica,sans-serif;
    font-size: 9pt;
    font-weight: normal;
    font-style: normal;
	color: #3399FF;
}
        .style1
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            height: 551px;
        }
        .treefont
{
	 font-style:normal;
	 font-weight:300;
	 background-color:#C8C8C8;
	 color:white;
	margin-top: 1px;
}
#checkpanel
{
 height:auto;
 border:solid 1px gray;
 float:right;
  
}

        .style2
        {
            height: 551px;
        }
    .checkpanelsize
{  margin-left:12px;
	margin-top:30px;
	margin-bottom:0px;
	}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="UpdatePanel1"
        runat="server">           
            <ProgressTemplate >                             
                <div align="center" class="legendloadimg">
                 <img alt="progress" src="loading.gif" border="1" height="100px" width="100px"/>
                 Processing... 
                 </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
       
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>                
                <br />
                <asp:Button ID="btnInvoke" runat="server" Text="Click"
                    onclick="btnInvoke_Click" />
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
                <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="load" />
                <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Save" />
                <asp:Button ID="btn_netconnection" runat="server" 
                    onclick="btn_netconnection_Click" Text="Netconnection" />
                <asp:Label ID="lbltxt" runat="server" Text="Label"></asp:Label>
                <br />
                <div class="legendmodifyprocuredata">
                    <fieldset class="fontt">
                        <legend class="fontt">SELECT MENU</legend>
                        <table>
                            <tr>
                                <td class="style1" style=" border:solid 1px gray;">
                                    <asp:Panel ID="Panel2" runat="server">
                                        <asp:TreeView ID="TreeView1" runat="server" BackColor="Snow" 
                                            CssClass="treefont" Height="360px">
                                            <Nodes>
                                                <asp:TreeNode Text="MASTER" Value="MASTER">
                                                    <asp:TreeNode Text="CompanyMaster" Value="CompanyMaster"></asp:TreeNode>
                                                    <asp:TreeNode Text="StateMaster" Value="StateMaster"></asp:TreeNode>
                                                    <asp:TreeNode Text="RouteMaster" Value="RouteMaster"></asp:TreeNode>
                                                    <asp:TreeNode Text="BankMaster" Value="BankMaster"></asp:TreeNode>
                                                    <asp:TreeNode Text="AgentMaster" Value="AgentMaster"></asp:TreeNode>
                                                    <asp:TreeNode Text="RateChart" Value="RateChart"></asp:TreeNode>
                                                    <asp:TreeNode Text="PortSetting" Value="PortSetting"></asp:TreeNode>
                                                    <asp:TreeNode Text="PermissionSetting" Value="PermissionSetting"></asp:TreeNode>
                                                </asp:TreeNode>
                                                <asp:TreeNode Text="PROCUREMENT" Value="PROCUREMENT">
                                                    <asp:TreeNode Text="Weigher" Value="Weigher"></asp:TreeNode>
                                                    <asp:TreeNode Text="Analyzer" Value="Analyzer"></asp:TreeNode>
                                                </asp:TreeNode>
                                                <asp:TreeNode Text="TRANSACTION" Value="TRANSACTION">
                                                    <asp:TreeNode Text="RateChartView" Value="RateChartView"></asp:TreeNode>
                                                    <asp:TreeNode Text="LoanEntry" Value="LoanEntry"></asp:TreeNode>
                                                    <asp:TreeNode Text="DeductionEntry" Value="DeductionEntry"></asp:TreeNode>
                                                </asp:TreeNode>
                                                <asp:TreeNode Text="REPORT" Value="REPORT">
                                                    <asp:TreeNode Text="Bill" Value="Bill"></asp:TreeNode>
                                                </asp:TreeNode>
                                            </Nodes>
                                        </asp:TreeView>
                                    </asp:Panel>
                                </td>
                                <td ID="checkpanel" class="style2">
                                    <asp:Panel ID="Panel3" runat="server" CssClass="checkpanelsize1" Height="900px" 
                                        Width="700px">
                                        <asp:Table ID="Table2" runat="Server" BorderColor="White" BorderWidth="1" 
                                            CaptionAlign="Top" CellPadding="1" CellSpacing="1" Height="40px" Width="500px">
                                            <asp:TableRow ID="TableRow1" runat="Server" BorderWidth="1" Width="500px">
                                                <asp:TableCell ID="TableCell2" runat="Server" BorderWidth="1">
                                                <asp:Table ID="Table1" runat="Server" BorderColor="White" BorderWidth="1" 
                                                    CaptionAlign="Top" CellPadding="1" CellSpacing="1" Height="40px" Width="500px">
                                                    <asp:TableRow ID="TableRow12" runat="Server" BackColor="#3399CC" 
                                                        BorderColor="Silver" BorderWidth="1" ForeColor="white" Width="500px">
                                                        <asp:TableCell ID="TableCell4" runat="Server" BorderWidth="2" Width="500px">
                                                        <asp:CheckBox ID="MChk_Menu1" runat="server" AutoPostBack="True" Text="SampleNo" oncheckedchanged="MChk_Menu1_CheckedChanged" />                                                       
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell5" runat="Server" BorderWidth="2" Width="500px">
                                                        <asp:CheckBox ID="MChk_Menu2" runat="server" AutoPostBack="True" Text="AgentId" oncheckedchanged="MChk_Menu2_CheckedChanged" />
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell6" runat="Server" BorderWidth="2" Width="500px">
                                                        <asp:CheckBox ID="MChk_Menu3" runat="server" AutoPostBack="True" Text="Amkg" oncheckedchanged="MChk_Menu3_CheckedChanged" />
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell7" runat="Server" BorderWidth="2" Width="500px">
                                                        <asp:CheckBox ID="MChk_Menu4"  runat="server" AutoPostBack="True" Text="Afat" oncheckedchanged="MChk_Menu4_CheckedChanged" />                                                       
                                                        </asp:TableCell>
                                                         <asp:TableCell ID="TableCell8" runat="Server" BorderWidth="2" Width="500px">
                                                          <asp:CheckBox ID="MChk_Menu5"  runat="server" AutoPostBack="True" Text="Asnf" oncheckedchanged="MChk_Menu5_CheckedChanged"  />                                                   
                                                        </asp:TableCell>
                                                         <asp:TableCell ID="TableCell20" runat="Server" BorderWidth="2" Width="500px">
                                                          <asp:Label ID="Label1" runat="server" Text=".........."></asp:Label>
                                                        </asp:TableCell>
                                                         <asp:TableCell ID="TableCell10" runat="Server" BorderWidth="2" Width="500px">
                                                          <asp:CheckBox ID="MChk_Menu6"  runat="server" AutoPostBack="True" Text="mAgentId" oncheckedchanged="MChk_Menu6_CheckedChanged"   />
                                                        </asp:TableCell>
                                                         <asp:TableCell ID="TableCell12" runat="Server" BorderWidth="2" Width="500px">
                                                          <asp:CheckBox ID="MChk_Menu7"  runat="server" AutoPostBack="True" Text="Mmkg" oncheckedchanged="MChk_Menu7_CheckedChanged"    />
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell16" runat="Server" BorderWidth="2" Width="500px">
                                                         <asp:CheckBox ID="MChk_Menu8"  runat="server" AutoPostBack="True" Text="Mfat" oncheckedchanged="MChk_Menu8_CheckedChanged"     />
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell18" runat="Server" BorderWidth="2" Width="500px">
                                                        <asp:CheckBox ID="MChk_Menu9"  runat="server" AutoPostBack="True" Text="Msnf" oncheckedchanged="MChk_Menu9_CheckedChanged"  />
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow2" runat="Server" BackColor="#fffafa" BorderColor="Silver" 
                                                        BorderWidth="1">
                                                        <asp:TableCell ID="TableCell1" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderWidth="1">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell3" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList2" runat="server" BorderWidth="1">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell14" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList3" runat="server" BorderWidth="1">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell15" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList4" runat="server" BorderWidth="1">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>                                                         
                                                         <asp:TableCell ID="TableCell9" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList5" runat="server" BorderWidth="1">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                         <asp:TableCell ID="TableCell21" runat="Server" BorderWidth="1">

                                                        <asp:CheckBoxList ID="CheckBoxList10" runat="server" BorderWidth="1">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                         <asp:TableCell ID="TableCell11" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList6" runat="server" BorderWidth="1">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell13" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList7" runat="server" BorderWidth="1">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell17" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList8" runat="server" BorderWidth="1">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                         <asp:TableCell ID="TableCell19" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList9" runat="server" BorderWidth="1">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>                                             
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>        
    </div>
                                                            
                                                   
    </form>
</body>
</html>
 