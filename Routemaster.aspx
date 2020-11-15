<%@ Page Title="OnlineMilkTest|Route Master" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="Routemaster.aspx.cs" Inherits="Routemaster" %>

<%--<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="stylesheet" />
    <style type="text/css">
        .style22
        {
            width: 1501px;
            color: #003300;
            font-family: Andalus;
            font-size: medium;
        }
        .style23
        {
            width: 100%;
            height: 17px;
        }
    </style>
    <style type="text/css">
        .style1
        {
            color: #333300;
            font-family: Andalus;
            font-size: medium;
        }
        .style2
        {
            width: 100%;
        }
    </style>
    <style type="text/css">
        .form-style-9
        {
            max-width: 450px;
            background: #FAFAFA;
            padding: 20px;
            margin: 20px auto;
            box-shadow: 1px 1px 25px rgba(0, 0, 0, 0.35);
            border-radius: 10px;
            height: 197px;
            width: 493px;
        }
        .form-style-9 ul
        {
            padding: 0;
            margin: 0;
            list-style: none;
        }
        .form-style-9 ul li
        {
            display: block;
            margin-bottom: 10px;
            min-height: 35px;
            height: 37px;
            width: 81px;
            margin-right: 0px;
            text-align: left;
        }
        .form-style-9 ul li .field-style
        {
            box-sizing: border-box;
            -webkit-box-sizing: border-box;
            -moz-box-sizing: border-box;
            padding: 8px;
            outline: none;
            border: 1px solid #B0CFE0;
            -webkit-transition: all 0.30s ease-in-out;
            -moz-transition: all 0.30s ease-in-out;
            -ms-transition: all 0.30s ease-in-out;
            -o-transition: all 0.30s ease-in-out;
        }
        .form-style-9 ul li .field-style:focus
        {
            box-shadow: 0 0 5px #B0CFE0;
            border: 1px solid #B0CFE0;
        }
        .form-style-9 ul li .field-split
        {
            width: 49%;
        }
        .form-style-9 ul li .field-full
        {
            width: 70%;
        }
        .form-style-9 ul li input.align-left
        {
            float: left;
        }
        .form-style-9 ul li input.align-right
        {
            float: right;
        }
        .form-style-9 ul li textarea
        {
            width: 80%;
            height: 100px;
        }
        .form-style-9 ul li input[type="button"], .form-style-9 ul li input[type="submit"]
        {
            -moz-box-shadow: inset 0px 1px 0px 0px #3985B1;
            -webkit-box-shadow: inset 0px 1px 0px 0px #3985B1;
            box-shadow: inset 0px 1px 0px 0px #3985B1;
            background-color: #216288;
            border: 1px solid #17445E;
            display: inline-block;
            cursor: pointer;
            color: #FFFFFF;
            padding: 8px 18px;
            text-decoration: none;
            font: 12px Arial, Helvetica, sans-serif;
            text-align: right;
        }
        .form-style-9 ul li input[type="button"]:hover, .form-style-9 ul li input[type="submit"]:hover
        {
            background: linear-gradient(to bottom, #2D77A2 5%, #337DA8 100%);
            background-color: #28739E;
        }
    </style>
    <style type="text/css">
.form-style-3{
    max-width: 450px;
    font-family: "Lucida Sans Unicode", "Lucida Grande", sans-serif;
}
.form-style-3 label{
    display:block;
  <%--  margin-bottom: 10px;--%>;
        height: 34px;
        text-align: left;
    }
.form-style-3 label > span{
    float: left;
    width: 100px;
    color: DARK;
    font-weight: bold;
    font-size: 13px;
    text-shadow: 1px 1px 1px #fff;
        text-align: right;
    }
.form-style-3 fieldset{
    border-radius: 10px;
    -webkit-border-radius: 10px;
    -moz-border-radius: 10px;
    margin: 0px 0px 10px 0px;
    border: 1px solid #FFD2D2;
    padding: 20px;
    background: #FFF4F4;
    box-shadow: inset 0px 0px 15px #FFE5E5;
    -moz-box-shadow: inset 0px 0px 15px #FFE5E5;
    -webkit-box-shadow: inset 0px 0px 15px #FFE5E5;
}
.form-style-3 fieldset legend{
    color: DARK;
    border-top: 1px solid #FFD2D2;
    border-left: 1px solid #FFD2D2;
    border-right: 1px solid #FFD2D2;
    border-radius: 5px 5px 0px 0px;
    -webkit-border-radius: 5px 5px 0px 0px;
    -moz-border-radius: 5px 5px 0px 0px;
    background: #FFF4F4;
    padding: 0px 8px 3px 8px;
    box-shadow: -0px -1px 2px #F1F1F1;
    -moz-box-shadow:-0px -1px 2px #F1F1F1;
    -webkit-box-shadow:-0px -1px 2px #F1F1F1;
    font-weight: normal;
    font-size: 12px;
}
.form-style-3 textarea{
    width:250px;
    height:100px;
}
.form-style-3 input[type=text],
.form-style-3 input[type=date],
.form-style-3 input[type=datetime],
.form-style-3 input[type=number],
.form-style-3 input[type=search],
.form-style-3 input[type=time],
.form-style-3 input[type=url],
.form-style-3 input[type=email],
.form-style-3 select, 
.form-style-3 textarea{
    border-radius: 3px;
    -webkit-border-radius: 3px;
    -moz-border-radius: 3px;
    border: 1px solid #FFC2DC;
    outline: none;
    color: DARK;
    padding: 5px 8px 5px 8px;
    box-shadow: inset 1px 1px 4px #FFD5E7;
    -moz-box-shadow: inset 1px 1px 4px #FFD5E7;
    -webkit-box-shadow: inset 1px 1px 4px #FFD5E7;
    background: #FFEFF6;
    width:50%;
}
.form-style-3  input[type=submit],
.form-style-3  input[type=button]{
    background: #EB3B88;
    border: 1px solid #C94A81;
    padding: 5px 15px 5px 15px;
    color: #FFCBE2;
    box-shadow: inset -1px -1px 3px #FF62A7;
    -moz-box-shadow: inset -1px -1px 3px #FF62A7;
    -webkit-box-shadow: inset -1px -1px 3px #FF62A7;
    border-radius: 3px;
    border-radius: 3px;
    -webkit-border-radius: 3px;
    -moz-border-radius: 3px;    
    font-weight: bold;
}
.required{
    color:red;
    font-weight:normal;
}
</style>
    <style type="text/css">
        .button
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            background-color: #4CAF50; /* Green */
            color: white;
            padding: 10px 26px;
            text-align: right;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
            font-weight: 700;
        }
        
        .style3
        {
            width: 180px;
            font-family: Arial;
            text-align: right;
        }
        
        .style24
        {
            width: 180px;
            font-family: Arial;
            text-align: right;
            height: 32px;
        }
        .style25
        {
            height: 32px;
        }
    </style>
    <style type="text/css">
        .body
        {
            margin: 0;
            padding: 0;
            font-family: Arial;
        }
        .modal
        {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
        .center
        {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        .center img
        {
            height: 128px;
            width: 128px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form1">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="modal">
                <div class="center">
                    <asp:Image ID="Image1" ImageUrl="waiting.gif" AlternateText="Processing" runat="server" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div align="center">
                <table class="style23">
                    <tr>
                        <td>
                            <div class="form-style-9" style="height: 367px">
                                <div class="form-style-3">
                                    <table width="100%" style="height: auto">
                                        <tr valign="top" align="LEFT">
                                            <td width="98%" height="20px">
                                                <fieldset style="height: 269px">
                                                    <legend>Plant Route Master</legend>
                                                    <table class="style2">
                                                        <tr>
                                                            <td class="style3">
                                                                <asp:Label ID="lbl_plantname" runat="server" Text="Plant Name" CssClass="style22"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_Plantname_SelectedIndexChanged"
                                                                    Width="170px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style3">
                                                                <asp:Label ID="lbl_Routeid" runat="server" Text="Route Id " CssClass="style22"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Routeid" runat="server" ReadOnly="True" BackColor="White" Width="150px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style3">
                                                                <asp:Label ID="lbl_Routename" runat="server" Text="Route Name " CssClass="style22"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Routename" runat="server" TabIndex="1" Width="150px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style3">
                                                                <asp:Label ID="lbl_Routedistance" runat="server" Text="Route Distance " CssClass="style22"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Routedistance" runat="server" TabIndex="2" Width="150px"></asp:TextBox>
                                                                km
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style3">
                                                                <asp:Label ID="lbl_Routeaddeddate" runat="server" Text="Route Added Date " CssClass="style22"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_Routeaddeddate" runat="server" Enabled="False" TabIndex="3"
                                                                    Width="150px"></asp:TextBox>
                                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_Routeaddeddate"
                                                                    PopupButtonID="popupcal" Format="MM/dd/yyyy" PopupPosition="TopRight">
                                                                </asp:CalendarExtender>
                                                                <asp:ImageButton ID="popupcal" runat="server" ImageUrl="~/calendar.gif" Height="20px"
                                                                    TabIndex="1" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style3">
                                                                <asp:Label ID="lbl_RouteCurrentstatus" runat="server" Text="Route Current status "
                                                                    CssClass="style22"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txt_RouteCurrentstatus" runat="server" Enabled="False" TabIndex="4"
                                                                    Width="150px"></asp:TextBox>
                                                                <asp:CheckBox ID="Chk_CurrentStatus" Text="." runat="server" Width="50PX" Height="15PX"
                                                                    AutoPostBack="True" OnCheckedChanged="Chk_CurrentStatus_CheckedChanged" TabIndex="3" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style24">
                                                                <asp:Label ID="lbl_Phoneno" runat="server" Text="Supervisor PhoneNo" CssClass="style22"></asp:Label>
                                                            </td>
                                                            <td class="style25">
                                                                <asp:TextBox ID="txt_PhoneNo" runat="server" TabIndex="5" Width="150px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="style3">
                                                                <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_plantcode_SelectedIndexChanged"
                                                                    Visible="False">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table class="style1">
                                                    </table>
                                                </fieldset>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <table class="style23">
                                    <tr>
                                        <td style="text-align: center">
                                            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="form93" OnClick="btn_Save_Click"
                                                TabIndex="6" />
                                            <asp:Button ID="btn_Reset" runat="server" Text="Reset" CssClass="form93" OnClick="btn_Reset_Click"
                                                TabIndex="7" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <strong><em>
                                <center>
                                    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        DataKeyNames="Table_Id" OnPageIndexChanging="GridView1_PageIndexChanging"
                                        EnableViewState="False" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                                        OnRowUpdating="GridView1_RowUpdating" CssClass="gridcls" Font-Italic="False"
                                        Font-Size="12px">
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
                                        <HeaderStyle BackColor="#f4f4f4" Font-Bold="False" Font-Italic="False" Font-Names="Raavi"
                                            Font-Size="Small" ForeColor="Black" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
                                        <AlternatingRowStyle HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <Columns>
                                            <asp:BoundField DataField="Table_ID" HeaderText="Table_ID" ReadOnly="True" SortExpression="Table_ID" />
                                            <asp:BoundField DataField="Route_ID" HeaderText="Route_ID" SortExpression="Route_ID"
                                                ReadOnly="True"></asp:BoundField>
                                            <asp:BoundField DataField="Route_Name" HeaderText="Route_Name" SortExpression="Route_Name">
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Plant_Code" HeaderText="Plant_Code" SortExpression="Plant_Code"
                                                ReadOnly="True"></asp:BoundField>
                                            <asp:BoundField DataField="Tot_Distance" HeaderText="Tot_Distance" ReadOnly="True"
                                                SortExpression="Tot_Distance"></asp:BoundField>
                                            <asp:BoundField DataField="Added_Date" HeaderText="Added_Date " SortExpression="Added_Date"
                                                ReadOnly="True"></asp:BoundField>
                                            <asp:BoundField DataField="Phone_No" HeaderText="Phone_No" SortExpression="Phone_No" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ReadOnly="false">
                                            </asp:BoundField>
                                            <asp:CommandField ShowEditButton="True" />
                                            <asp:CommandField ShowDeleteButton="True" />
                                        </Columns>
                                    </asp:GridView>
                                </center>
                            </em></strong>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
            </div>
        </ContentTemplate>
        <%--<Triggers>
    <asp:PostBackTrigger ControlID="Button7" />
</Triggers>--%>
    </asp:UpdatePanel>
    </form>
</asp:Content>
