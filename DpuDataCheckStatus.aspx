<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DpuDataCheckStatus.aspx.cs" Inherits="DpuDataCheckStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
    
    
    
    <style type="text/css">
        .style3
        {
            width: 100%;
        }
        .style10
        {
            font-family: Tahoma;
        }
        .style19
        {
            height: 10px;
        }
        .style22
        {
            font-family: Andalus;
            font-size: small;
        }
        .style23
        {
            font-family: Andalus;
        }
        
          .style24
        {
            width:33%;
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

        
        
        
        
        
        
        
        
        
        
        
        .style24
        {
            font-family: Andalus;
            color: #CC0066;
        }
        .style25
        {
            color: #990000;
        }

        
        
        
        
        
        
        
        
        
        
        
        .style26
        {
            font-family: Andalus;
            font-size: medium;
        }

        
        
        
        
        
        
        
        
        
        
        
        .style27
        {
            width: 12%;
        }

        
        
        
        
        
        
        
        
        
        
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>



    <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height:1%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>


<asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"    style="background-color: #CCFFFF" >
            <ContentTemplate>


    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <span class="style25"><strong style="font-size: small"><span class="style26">
        Verify&nbsp;Dpu Data</span> </strong></span>
    </p>
    <p>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong> 
                                <asp:Label ID="Label18" runat="server" 
                    style="font-weight: 700; " 
                    Text="From Date" CssClass="style22"></asp:Label>
                                </strong>
                                <asp:TextBox ID="txt_FromDate" runat="server" 
                                    ontextchanged="txt_FromDate_TextChanged" CssClass="ddl2" Width="125px"  ></asp:TextBox>

                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="MM/dd/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
                              
                               &nbsp;&nbsp;<asp:Label ID="Label19" runat="server" 
                    style="font-weight: 700; " 
                    Text="To Date" CssClass="style22"></asp:Label>
                                &nbsp;
                              <asp:TextBox ID="txt_ToDate" runat="server" 
                                    ontextchanged="txt_ToDate_TextChanged" CssClass="ddl2" Width="125px"  ></asp:TextBox>
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="MM/dd/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
                         
                                   &nbsp;&nbsp;&nbsp; 
                <asp:Label ID="Label20" runat="server" 
                    style="font-size: small; font-weight: 700; font-family: Andalus" 
                    Text="Plant Name"></asp:Label>
                                <asp:DropDownList ID="ddl_Plantname" runat="server" 
                    Font-Bold="True" Font-Size="Small" 
                    OnSelectedIndexChanged="ddl_Plantname_SelectedIndexChanged" 
            Width="120px" Height="20px">
                </asp:DropDownList>
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Button ID="Button7" runat="server" onclick="Button7_Click" 
        Text="ALL STATUS" style="font-weight: 700; font-family: Andalus; font-size: small;" 
                                    BorderStyle="Groove" BorderWidth="1px" Font-Bold="True" 
                                    Width="89px" CssClass="buttonclass" />
          
    <p>
                             
       
   
    <table   class="style3">
        <tr align="center">
            <td align="center" width="12%">
                &nbsp;</td>
            <td align="center" width="12%">
                &nbsp;</td>
            <td align="center" width="12%">
                &nbsp;</td>
            <td align="center" width="12%">
                <asp:Label ID="Label6" runat="server" CssClass="style23" 
                    style="font-size: small; font-weight: 700; " Text="Send Rows"></asp:Label>
                <br />
                <asp:TextBox ID="TextBox5" runat="server" BorderStyle="Inset" BorderWidth="1px" 
                    CssClass="ddl2" Font-Bold="True" Font-Size="Large" ForeColor="Green" 
                    Height="25px" style="font-weight: 700" Width="90px"></asp:TextBox>
                <br />
            </td>
            <td align="center" class="style27">
                <asp:Label ID="Label7" runat="server" CssClass="style24" 
                    style="font-size: small; font-weight: 700; " Text="Approval Rows"></asp:Label>
                <br />
                <asp:TextBox ID="TextBox6" runat="server" BorderStyle="Inset" BorderWidth="1px" 
                    CssClass="ddl2" Font-Bold="True" Font-Size="Large" ForeColor="Green" 
                    Height="25px" style="font-weight: 700" Width="90px"></asp:TextBox>
            </td>
            <td align="center" width="12%">
                &nbsp;</td>
            <td align="center" width="12%">
                &nbsp;</td>
            <td align="center" width="12%">
                &nbsp;</td>
        </tr>
       
    </table>
        <p>
            <table class="style3">
                <tr>
                    <td align="center" class="style24" valign="top">
                        <strong>
                        <asp:Label ID="Label14" runat="server" CssClass="style24" 
                            style="font-size: small; font-weight: 700; " Text="Zero Values"></asp:Label>
                        <br />
                        <asp:Label ID="Label1" runat="server" CssClass="style24" 
                            style="font-size: small; font-weight: 700; " Text="Zero Values"></asp:Label>
                        </strong>
                    </td>
                    <td align="center" class="style" valign="top">
                        <span class="style10"><strong>
                        <asp:Label ID="Label23" runat="server" CssClass="style24" 
                            style="font-size: small; font-weight: 700; " Text="Send Sessions"></asp:Label>
                        <span align="center" class="style24">
                        <br />
                        <asp:Label ID="Label24" runat="server" CssClass="style24" 
                            style="font-size: small; font-weight: 700; " Text="Send Sessions"></asp:Label>
                        </span></strong></span>
                    </td>
                    <td align="center" class="style19" valign="top">
                        <span class="style24"><strong>
                        <asp:Label ID="Label17" runat="server" CssClass="style24" 
                            style="font-size: small; font-weight: 700; " Text="Approval Sessions"></asp:Label>
                        <br />
                        <asp:Label ID="Label4" runat="server" CssClass="style24" 
                            style="font-size: small; font-weight: 700; " Text="Approval Sessions"></asp:Label>
                        </strong></span>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="style24" valign="top">
                        <strong>
                        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="3" CssClass="gridview1" Font-Size="10px" 
                            onpageindexchanging="GridView1_PageIndexChanging" PageSize="32" Width="126px">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                        </strong>
                    </td>
                    <td align="center" class="style" valign="top">
                        <span class="style10"><strong>
                        <asp:GridView ID="GridView3" runat="server" AllowPaging="True" 
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="3" CssClass="gridview1" Font-Size="10px" 
                            onpageindexchanging="GridView3_PageIndexChanging" PageSize="32" Width="126px">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                        </strong></span>
                    </td>
                    <td align="center" class="style19" valign="top">
                        <span class="style10"><strong>
                        <asp:GridView ID="GridView4" runat="server" AllowPaging="True" 
                            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                            CellPadding="3" CssClass="gridview1" Font-Size="10px" 
                            onpageindexchanging="GridView4_PageIndexChanging" PageSize="32" Width="126px">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                        </strong></span>
                    </td>
                </tr>
            </table>
            <br />
        </p>


      </ContentTemplate>
        </asp:UpdatePanel>


    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table class="style3">
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="."></asp:Label>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

