<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="rmksccwiseapprove.aspx.cs" Inherits="rmksccwiseapprove" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

.legendpermission
{ background-color:#FFFAFA;
	margin-left:50px;
   margin-right:30px;
 width :818px;
	
}
.legendloadimg
{ background-color:#FFFAFA;
  margin-left :250px;
  margin-right:300px;  
  width :500px;  
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
 width:100%;
  
}
.cellsize
{ height:auto;
  width:auto;
    
}

        .style2
        {
            height: 551px;
            width: 848px;
        }
    .checkpanelsize
{  margin-left:12px;
	margin-top:30px;
	margin-bottom:0px;
	}
    </style>
    <script type="text/javascript">
        //
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
        prm.add_beginRequest(BeginRequestHandler);
        // Raised after an asynchronous postback is finished and control has been returned to the browser.
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            //Shows the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.show();
            }
        }

        function EndRequestHandler(sender, args) {
            //Hide the modal popup - the update progress
            var popup = $find('<%= modalPopup.ClientID %>');
            if (popup != null) {
                popup.hide();
            }
        }       

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
      <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server" />
<div  >
<asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
<div style="position: fixed; text-align: center; height: 10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: White; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup"  />
</div> 
       
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
            <div align="center">  
            <br />   
             <asp:DropDownList ID="ddl_Plantcode" AutoPostBack="true" runat="server" Visible="false"></asp:DropDownList>
                <asp:CheckBox ID="Chk_InDec" runat="server" AutoPostBack="True" Checked="True" CssClass="fontt" 
                    oncheckedchanged="Chk_InDec_CheckedChanged" Text="Increase" 
                    Enabled="True" />
                &nbsp;
                   <asp:Label ID="lbl_PlantName" runat="server" Text="Plant:" CssClass="fontt"></asp:Label>
                   <asp:DropDownList ID="ddl_Plantname" AutoPostBack="true" runat="server" 
                    Width="154px" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged">
                                    </asp:DropDownList>
                <asp:Label ID="lbl_date" runat="server" Text="Remarks Date:" CssClass="fontt"></asp:Label>
                       <asp:TextBox ID="txt_RemmarksDate" runat="server"  ></asp:TextBox>
                         <asp:Label ID="lbl_ses" runat="server" Text="Session:" CssClass="fontt"></asp:Label>
                         <asp:RadioButton ID="rd_sesAm" Text="AM" runat="server" 
                    CssClass="fontt" Checked="True" AutoPostBack="True" 
                    oncheckedchanged="rd_sesAm_CheckedChanged"/>
                         <asp:RadioButton ID="rd_sesPm" Text="PM" runat="server" 
                    CssClass="fontt" AutoPostBack="True" 
                    oncheckedchanged="rd_sesPm_CheckedChanged"/>
                         <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_RemmarksDate"
                                PopupButtonID="txt_RemmarksDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
              
                <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="load"  BackColor="#6F696F"  ForeColor="White"  Width="50px" Height="26px"/>
                <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Save"  BackColor="#6F696F"  ForeColor="White"  Width="50px" Height="26px" OnClientClick="return confirm('Do u want to Save the Records With the efect of max Allowed modify fat is 0.1, Regarding Any Issue Please Contact the Manager?');"/>
                <br /> 
                </div>
               
                <div >
                    <fieldset class="fontt"> 
                        <legend class="fontt">SELECT MENU</legend>
                        <table width="100%">
                            <tr align ="center" >                               
                                <td ID="checkpanel" class="style2" >
                                    <asp:Panel ID="Panel1" runat="server"  Height="800px"    width="100%">                                
                                        <asp:Table ID="Table2" runat="Server" BorderColor="White" BorderWidth="1" 
                                            CaptionAlign="Top" CellPadding="1" CellSpacing="1" Height="40px" Width="500px">
                                            <asp:TableRow ID="TableRow1" runat="Server" BorderWidth="1" Width="500px">
                                                <asp:TableCell ID="TableCell2" runat="Server" BorderWidth="1">
                                                <asp:Table ID="Table1" runat="Server" BorderColor="White" BorderWidth="1" 
                                                    CaptionAlign="Top" CellPadding="1" CellSpacing="1" Height="40px" Width="500px">
                                                    <asp:TableRow ID="TableRow12" runat="Server" BackColor="#3399CC" 
                                                        BorderColor="Silver" BorderWidth="1" ForeColor="white" Width="500px">
                                                          <asp:TableCell ID="TableCell22" runat="Server" BorderWidth="2" >
                                                        <asp:CheckBox ID="MChk_Sno" runat="server" Width="60px" Text="SNo" Enabled="false"  Font-Bold="true"/>                                                       
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell4" runat="Server" BorderWidth="2" >
                                                        <asp:CheckBox ID="MChk_Menu1" runat="server" Width="90px" AutoPostBack="True" Text="SampleNo" Enabled="false" oncheckedchanged="MChk_Menu1_CheckedChanged" Font-Bold="true"/>                                                       
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell5" runat="Server" BorderWidth="2" >
                                                        <asp:CheckBox ID="MChk_Menu2" runat="server" Width="80px" AutoPostBack="True" Text="AgentId" Enabled="false" oncheckedchanged="MChk_Menu2_CheckedChanged" Font-Bold="true"/>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell6" runat="Server" BorderWidth="2"  >
                                                        <asp:CheckBox ID="MChk_Menu3" runat="server" Width="80px" AutoPostBack="True" Text="Amkg" Enabled="false" oncheckedchanged="MChk_Menu3_CheckedChanged" Font-Bold="true"/>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell7" runat="Server" BorderWidth="2" Width="500px">
                                                        <asp:CheckBox ID="MChk_Menu4"  runat="server" Width="65px" AutoPostBack="True" Text="Afat" Enabled="false" oncheckedchanged="MChk_Menu4_CheckedChanged" Font-Bold="true"/>                                                       
                                                        </asp:TableCell>
                                                         <asp:TableCell ID="TableCell8" runat="Server" BorderWidth="2" Width="500px">
                                                          <asp:CheckBox ID="MChk_Menu5"  runat="server" Width="65" AutoPostBack="True" Text="Asnf" Enabled="false" oncheckedchanged="MChk_Menu5_CheckedChanged" Font-Bold="true" />                                                   
                                                        </asp:TableCell>
                                                         <asp:TableCell ID="TableCell20" runat="Server" BorderWidth="2" Width="500px">
                                                          <asp:Label ID="Label1" runat="server" Text=".........."></asp:Label>
                                                        </asp:TableCell>
                                                         <asp:TableCell ID="TableCell10" runat="Server" BorderWidth="2" Width="500px">
                                                          <asp:CheckBox ID="MChk_Menu6"  runat="server" Width="90px" AutoPostBack="True" Text="mAgentId" Enabled="false" oncheckedchanged="MChk_Menu6_CheckedChanged" Font-Bold="true"  />
                                                        </asp:TableCell>
                                                         <asp:TableCell ID="TableCell12" runat="Server" BorderWidth="2" Width="500px">
                                                          <asp:CheckBox ID="MChk_Menu7"  runat="server" Width="80px" AutoPostBack="True" Text="Mmkg" oncheckedchanged="MChk_Menu7_CheckedChanged"  Font-Bold="true"  />
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell16" runat="Server" BorderWidth="2" Width="500px">
                                                         <asp:CheckBox ID="MChk_Menu8"  runat="server" Width="70px" AutoPostBack="True" Text="Mfat" oncheckedchanged="MChk_Menu8_CheckedChanged"  Font-Bold="true"   />
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell18" runat="Server" BorderWidth="2" Width="500px">
                                                        <asp:CheckBox ID="MChk_Menu9"  runat="server" Width="70px" AutoPostBack="True" Text="Msnf" oncheckedchanged="MChk_Menu9_CheckedChanged" Font-Bold="true"   />
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                    <asp:TableRow ID="TableRow2" runat="Server" BackColor="#fffafa" BorderColor="Silver" 
                                                        BorderWidth="1">
                                                         <asp:TableCell ID="TableCell23" runat="Server" BorderWidth="1" >
                                                        <asp:CheckBoxList ID="CheckBoxList_Sno" runat="server" BorderWidth="0" Font-Bold="true">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell1" runat="Server" BorderWidth="1" >
                                                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderWidth="0" Font-Bold="true">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell3" runat="Server" BorderWidth="1" >
                                                        <asp:CheckBoxList ID="CheckBoxList2" runat="server" BorderWidth="0" Font-Bold="true">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell14" runat="Server" BorderWidth="1" >
                                                        <asp:CheckBoxList ID="CheckBoxList3" runat="server" BorderWidth="0"  Font-Bold="true">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell15" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList4" runat="server" BorderWidth="0"  Font-Bold="true">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>                                                         
                                                         <asp:TableCell ID="TableCell9" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList5" runat="server" BorderWidth="0"  Font-Bold="true">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                         <asp:TableCell ID="TableCell21" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList10" runat="server" BorderWidth="0"  Font-Bold="true">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                         <asp:TableCell ID="TableCell11" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList6" runat="server" Enabled="false" Visible="false" BorderWidth="0"  Font-Bold="true">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell13" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList7"  runat="server" BorderWidth="0"  Font-Bold="true">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                        <asp:TableCell ID="TableCell17" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList8" runat="server" BorderWidth="0"  Font-Bold="true">
                                                        </asp:CheckBoxList>
                                                        </asp:TableCell>
                                                         <asp:TableCell ID="TableCell19" runat="Server" BorderWidth="1">
                                                        <asp:CheckBoxList ID="CheckBoxList9" runat="server" BorderWidth="0" Font-Bold="true">
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
    
</asp:Content>


