<%@ Page Title="OnlineMilkTest|TransportDistanceChanged" Language="C#" MasterPageFile="~/MasterPage.master"
    AutoEventWireup="true" CodeFile="TransportDistanceChange.aspx.cs" Inherits="TransportDistanceChange" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="Stylesheet" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            //       printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 100);
            return false;
        }
    </script>
    <style type="text/css">
        .DText
        {
            width: 150px;
            height: 20px;
            border-radius: 5px;
            border: 1px solid #CCC;
            outline: none;
            border-color: #9ecaed;
            box-shadow: 0 0 10px #9ecaed;
            padding: 8px;
            font-weight: 200;
            font-size: 15px;
            font-family: Verdana;
        }
    
.btn {
    background:#a00;
    color:#fff;
  
    padding: 2px 2px;
    border-radius: 2px;
    -moz-border-radius: 2px;
    -webkit-border-radius: 2px;
  
    text-decoration: none;
    font: bold 6px Verdana, sans-serif;
    text-shadow: 0 0 1px #000;
    box-shadow: 0 2px 2px #aaa;
    -moz-box-shadow: 0 2px 2px #aaa;
    -webkit-box-shadow: 0 2px 2px #aaa;
     height:30px;
     width :85px;
    }
    
              
        
         .style1
        {
            text-align: center;
        }
    
              
        
         .style2
        {
            width: 100%;
        }
    
              
        
         </style>




         

        <%-- <script type="text/javascript" >
             window.onload = function () {
                 setInterval('changestate()', 2000);
             };
             var currentState = 'show';
             function changestate() {
                 if (currentState == 'show') {
                     document.getElementById('<%= btn_lock.ClientID %>').style.display = "none";
                     currentState = 'hide';
                 }
                 else {
                     document.getElementById('<%= btn_lock.ClientID %>').style.display = "block";
                     currentState = 'show';
                 }
             }
</script>--%>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 10%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: Gray; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..."
                    ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 38%; left: 50%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <div class="main">
            <center>
                
                <table align="center" width=40%>
                    <tr align="center">
                        <td>
                          
                                <fieldset class="fontt">
                                    <legend style="color: #3399FF">VehicleDistance Report </legend>
                                    <table ID="table4" align="center" border="0" cellspacing="1" width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TextBox1" runat="server" Height="16px" Visible="false" 
                                                    Width="61px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                                                    Height="16px" Visible="false" Width="29px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                Plant_Name:
                                            </td>
                                            <td>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
                                                    EnableTheming="True" Font-Bold="True" Font-Size="Large" 
                                                    OnSelectedIndexChanged="ddl_Plantname_SelectedIndexChanged" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                From :
                                            </td>
                                            <td>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txt_FromDate" runat="server" CssClass="DText"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                                    PopupButtonID="txt_FromDate" PopupPosition="BottomRight" 
                                                    TargetControlID="txt_FromDate">
                                                </asp:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txt_ToDate" runat="server" Visible="False"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style1" colspan="4">
                                                &nbsp;
                                                <asp:Button ID="btn_Ok" runat="server" BackColor="Orange" BorderStyle="Double" 
                                                    Font-Bold="True" ForeColor="White" Height="26px" OnClick="btn_Ok_Click" 
                                                    Style="height: 26px" Text="Show" Width="50px" />
                                                <asp:Button ID="btn_Insert" runat="server" BackColor="Green" 
                                                    BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                                                    OnClick="btn_Insert_Click" Style="height: 26px" Text="Insert" Width="50px" />
                                                <asp:Button ID="btn_print" runat="server" BackColor="#00CCFF" 
                                                    BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                                                    OnClientClick="return PrintPanel();" Text="Print" />
                                                <asp:Button ID="btn_delete" runat="server" BackColor="Red" BorderStyle="Double" 
                                                    Font-Bold="True" ForeColor="White" Height="26px" onclick="btn_delete_Click" 
                                                    Style="height: 26px" Text="Delete" Width="50px" />
                                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                                                    PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                                                    TargetControlID="txt_ToDate">
                                                </asp:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4">
                                                <strong>
                                                <asp:Button ID="btn_lock" runat="server" BorderStyle="Groove" BorderWidth="1px" 
                                                    CausesValidation="False" CssClass="btn" Font-Bold="True" Height="30px" 
                                                    onclick="btn_lock_Click" 
                                                    style="font-weight: 700; font-family: Andalus; font-size: small;" 
                                                    Text="Approval" Width="89px" />
                                                </strong>
                                            </td>
                                        </tr>
                                    </table>
                                    <div align="left">
                                        <asp:Label ID="Lbl_Errormsg" runat="server" Font-Size="Large"></asp:Label>
                                    </div>
                                    <br />
                                </fieldset>
                         
                        </td>
                    </tr>
                </table>
                </center>
                <br />
                <asp:Panel ID="pnlContents" runat="server">
                    <div Width="100%" align="center">
                    <center>
                        <div class="fontt" ALIGN="center">
                            <mcn:DataPagerGridView ID="gvProducts" runat="server" AutoGenerateColumns="false"
                                CssClass="datatable" OnRowCancelingEdit="gvProducts_RowCancelingEdit" Width="615px"
                                OnRowDeleting="gvProducts_RowDeleting" OnRowEditing="gvProducts_RowEditing" OnRowUpdating="gvProducts_RowUpdating"
                                Font-Bold="true" PageSize="20">
                                <Columns>
                                    <asp:BoundField DataField="Truck_Id" HeaderStyle-CssClass="first" HeaderText="TruckId"
                                        ItemStyle-CssClass="first" SortExpression="Truck_Id" HeaderStyle-Width="1px">
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Tdistance" HeaderText="Distance" SortExpression="Tdistance"
                                        HeaderStyle-Width="1px" />
                                    <asp:BoundField DataField="Vtsdisance" HeaderText="Vtsdisance" SortExpression="Vtsdisance"
                                        HeaderStyle-Width="1px" />
                                    <asp:BoundField DataField="Admindisance" HeaderText="Admindisance" SortExpression="Admindisance"
                                        HeaderStyle-Width="1px" />
                                    <asp:BoundField DataField="Pdate" HeaderText="Date" SortExpression="Pdate" HeaderStyle-Width="1px" />
                                    <asp:BoundField DataField="Route_Name" HeaderText="RouteID/Name" SortExpression="Route_Name"
                                        HeaderStyle-Width="1px" ReadOnly="true" />
                                    <asp:BoundField DataField="Vehicle_No" HeaderText="Truck_Id/No" SortExpression="Vehicle_No"
                                        HeaderStyle-Width="1px" ReadOnly="true" />
                                    <asp:CommandField ShowEditButton="true" ButtonType="Image" EditImageUrl="~/Image/edit-icon.png"
                                        UpdateImageUrl="~/Image/Editok.jpg" CancelImageUrl="~/Image/Delete.jpg" HeaderText="Edit"
                                        HeaderStyle-Width="1px" HeaderStyle-Font-Size="Small" />
                                    <asp:CommandField ShowDeleteButton="true" ButtonType="Image" HeaderStyle-Width="1px"
                                        HeaderStyle-Font-Size="Small" HeaderText="Delete" DeleteImageUrl="~/Image/Delete.jpg" />
                                </Columns>
                                <PagerSettings Visible="False" />
                                <RowStyle CssClass="row" />
                            </mcn:DataPagerGridView>
                        </div>
                        </center>
                    </div>
                </asp:Panel>
                <br />
                <br />
                <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
