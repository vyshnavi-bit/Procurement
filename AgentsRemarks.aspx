<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AgentsRemarks.aspx.cs" Inherits="AgentsRemarks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        
        
          
         .ddl
        {
            border:2px solid #7d6754;
            border-radius:5px;
            padding:3px;
            -webkit-appearance: none; 
            background-image:url('Images/Arrowhead-Down-01.png');
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            text-overflow: '';/*In Firefox*/
        }
        
        
        
         .textbox { 
    background: #FFF url(http://html-generator.weebly.com/files/theme/input-text-9.png) no-repeat 4px 4px; 
    border: 1px solid #999; 
    outline:0; 
    padding-left: 25px;
    height:25px; 
    width: 275px; 
  } 
        
        
        inputs:-webkit-input-placeholder {
    color: #b5b5b5;
}

inputs-moz-placeholder {
    color: #b5b5b5;
}

.inputs {
    outline: none;
    display: block;
    width: 350px;
    padding: 4px 8px;
    border: 1px dashed #DBDBDB;
    color: #3F3F3F;
    font-family: Andalus;
    font-size: medium;
    -webkit-border-radius: 2px;
    -moz-border-radius: 2px;
    border-radius: 2px;
    -webkit-transition: background 0.2s linear, box-shadow 0.6s linear;
    -moz-transition: background 0.2s linear, box-shadow 0.6s linear;
    -o-transition: background 0.2s linear, box-shadow 0.6s linear;
    transition: background 0.2s linear, box-shadow 0.6s linear;
}

.inputs:focus {
    background: #F7F7F7;
    border: dashed 1px #969696;
    -webkit-box-shadow: 2px 2px 7px #E8E8E8 inset;
    -moz-box-shadow: 2px 2px 7px #E8E8E8 inset;
    box-shadow: 2px 2px 7px #E8E8E8 inset;
}

.buttonclass
{
padding-left: 10px;
font-weight: bold;
}
.buttonclass:hover
{
color: white;
background-color:Orange;
}


.columnscss
{
width:25px;
font-weight:bold;
font-family:Verdana;
}



        
.ddlstyle 
{
color:rgb(153, 0, 51);
Font-Family:Andalus;
font-size:medium;
vertical-align :middle;
}
        
 
        
         .ddl2
        {
            border:2px solid #7d6754;
            border-radius:5px;
            padding:3px;
            -webkit-appearance: none; 
            background-image:url('Images/Arrowhead-Down-01.png');
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            text-overflow: '';
            font-size: medium;
            font-family: Andalus;
            color: #800000;
            font-weight: 700;
        }
        
        
        

        .style5
        {
            font-family: Andalus;
            color: #990033;
            font-size: medium;
        }

        
        
        
    </style>








</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
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

    
    
    
       
    <br />

    <center>
    
                        <asp:Panel ID="Panel1" runat="server" Width="600px" BorderColor="#00FFCC" 
                            BorderStyle="Inset" BorderWidth="2px">
                            &nbsp;&nbsp;<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label4" runat="server" 
                                style="font-weight: 700;" Text="From" CssClass="style5"></asp:Label>
                            <asp:TextBox ID="txt_FromDate" runat="server" CssClass="ddl2" Height="20px" 
                                ontextchanged="txt_FromDate_TextChanged" Width="150px"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
                            <asp:CalendarExtender ID="txt_FromDate_CalendarExtender2" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
                            &nbsp;&nbsp;
                            <asp:Label ID="Label5" runat="server" 
                                style="font-weight: 700;" Text="To" CssClass="style5"></asp:Label>
                            &nbsp;<asp:TextBox ID="txt_ToDate" runat="server" CssClass="ddl2" Height="20px" 
                                ontextchanged="txt_ToDate_TextChanged" Width="150px"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_ToDate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                                TargetControlID="txt_ToDate">
                            </asp:CalendarExtender>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /> &nbsp;&nbsp;&nbsp;&nbsp;<table class="style1">
                                <tr>
                                    <td width="40%" align="right">
                                        <asp:Label ID="Label6" runat="server" 
                                            style="font-weight: 700;" Text="Plant_Name" CssClass="style5"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                                            CssClass="ddl2" Font-Bold="True" Font-Size="Medium" Height="29px" 
                                            onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="200px">
                                            <asp:ListItem>---------Select---------</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:CheckBox ID="CheckBox1" runat="server" CssClass="ddlstyle" 
                                            oncheckedchanged="CheckBox1_CheckedChanged" Text="AgentWise" AutoPostBack="True"  />
                                        <asp:DropDownList ID="ddl_plantcode" runat="server" AutoPostBack="True" 
                                            CssClass="tb8" Font-Bold="True" Font-Size="Large" Height="16px" 
                                            onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Visible="False" 
                                            Width="37px">
                                            <asp:ListItem>---------Select---------</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align=right style="color: #CC0000">
                                        <br />
                                        <asp:Label ID="Label7" runat="server" 
                                            style="font-weight: 700;" Text="Agent Id" CssClass="style5"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddl_Agentname" runat="server" 
                                            CssClass="ddl2" Font-Bold="True" Font-Size="Medium" Height="29px" 
                                            Width="200px">
                                            <asp:ListItem>---------Select---------</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                         <center>   <asp:Button ID="btn_ok" runat="server" BackColor="#00CCFF" BorderStyle="Double" 
                                CssClass="button2222" Font-Bold="True" ForeColor="White" Height="30px" 
                                onclick="btn_ok_Click" Text="OK" />
                                <br />
                                </center>
                            </asp:Panel>

                        </center>
                        <br />
                        <center>
    <asp:Panel ID="Panel3" runat="server">
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                                AutoDataBind="True" 
    GroupTreeImagesFolderUrl="" Height="962px" ToolbarImagesFolderUrl="" 
                                ToolPanelView="None" ToolPanelWidth="200px" 
            Width="1047px" oninit="CrystalReportViewer1_Init" />
    </asp:Panel>
               </center>

                </ContentTemplate>
           <Triggers>
<asp:PostBackTrigger ControlID="btn_ok" />
</Triggers>
        </asp:UpdatePanel>

           

 <br />
 <br />
            <br />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

