<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DispatchAcknowledgement.aspx.cs" Inherits="DispatchAcknowledgement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="stylesheet" />
    <style type="text/css">
        .style4
        {
            width: 100%;
        }
        .style5
        {
            color: Black;
            font-family: Andalus;
        }
        .style6
        {
            font-family: Andalus;
            font-size: medium;
        }
        .style7
        {
            height: 30px;
        }
        .style8
        {
            width: 274px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <right>    
     <table class="style4">
         <tr align="right">
             <td>    
     <asp:LinkButton ID="LinkButton1" runat="server" 
         style="font-weight: 700; text-align: right" CausesValidation="False" 
                     PostBackUrl="~/DispatchnewACK.aspx" Visible="False">Acknowledgement</asp:LinkButton>
             </td>
         </tr>
     </table>
     </right>   
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 1%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: Gray; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..."
                    ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 45%; left: 50%;" />            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <center>      
        <asp:Panel ID="Panel1" runat="server" Width=50% BackColor="#CCFFCC">
               <table class="style4">
            <tr>
                <td align="center" class="style7">
                    <asp:Label ID="Label1" runat="server"   ForeColor="#FF8000" 
                        style="font-size: medium; font-family: Andalus; color: #CC0000; font-weight: 700;" 
                        Text="Despatch Acknowledgement  Entry Form"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table class="style4">                       
                        <tr>
                            <td class="style7">
                                &nbsp;</td>
                            <td align="left" width=25%>
                                <asp:Label ID="lbl_session10" runat="server" CssClass="style6" 
                                    Font-Bold="False" ForeColor="#3399FF" style="color: #000000;" Text="Date"></asp:Label>
                            </td>
                            <td align="left" class="style7">
                                <asp:TextBox ID="txt_fromdate" runat="server"  height="21px" width="126px"
                                  TabIndex="12"></asp:TextBox>



                                      <asp:CalendarExtender ID="txt_date_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="TopRight" 
                                TargetControlID="txt_fromdate">
                            </asp:CalendarExtender>


                            </td>
                            <td class="style7">
                                <asp:Label ID="lbl_session14" runat="server" CssClass="style6" 
                                    Font-Bold="False" ForeColor="#3399FF" style="color: #000000;" Text="DCNumber"></asp:Label>
                                <br />
                                <asp:DropDownList ID="DDl_Dcnumber" runat="server" AutoPostBack="True" 
                                    Height="21px" onselectedindexchanged="DDl_Dcnumber_SelectedIndexChanged" 
                                    TabIndex="18" Width="126px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                &nbsp;</td>
                            <td align="left">
                                &nbsp;
                                <asp:Label ID="Label2" runat="server" CssClass="style5" ForeColor="#3399FF" 
                                    style="font-size: medium; color: #000000;" Text="From Plant"></asp:Label>
                            </td>
                            <td align="left" class="style7">
                                <asp:DropDownList ID="DDL_Plantfrom" runat="server" AutoPostBack="True" 
                                    Enabled="true" Font-Bold="False" ForeColor="Black" Height="21px" 
                                    onselectedindexchanged="DDL_Plantfrom_SelectedIndexChanged" TabIndex="41" 
                                    Width="140px">
                                </asp:DropDownList>
                            </td>
                            <td class="style7">
                                <asp:Label ID="lbl_session9" runat="server" CssClass="style6" Font-Bold="False" 
                                    ForeColor="#3399FF" style="color: #000000;" Text="Avail Milk" 
                                    Visible="False"></asp:Label>
                                <asp:TextBox ID="txt_avail" runat="server" Height="21px" 
                                    style="text-align: right" Visible="False" Width="28px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td align="left">
                               
                                <asp:Label ID="lbl_ToPlant" runat="server" CssClass="style5" 
                                    ForeColor="#3399FF" style="font-size: medium; color: #000000;" 
                                    Text="To Plant"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="DDl_Plantto" runat="server" Height="21px" TabIndex="18" 
                                    Width="126px">
                                    <asp:ListItem Value="0">KALASTHIRI</asp:ListItem>
                                    <asp:ListItem Value="1">SANGAM</asp:ListItem>
                                    <asp:ListItem Value="2">VIRA</asp:ListItem>
                                    <asp:ListItem Value="3">ONGOLE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lbl_plantnane" runat="server" Font-Size="XX-Small" 
                                    Text="PLANTNAME" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lbl_session" runat="server" CssClass="style6" 
                                    ForeColor="#3399FF" style="color: #000000" Text="Session"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="dl_Session" runat="server" Height="23px" 
                                     TabIndex="19" 
                                    Width="58px">
                                    <asp:ListItem Value="AM">AM</asp:ListItem>
                                    <asp:ListItem Value="PM">PM</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lbl_session13" runat="server" CssClass="style6" ForeColor="#3399FF" 
                                    style="color: #000000" Text="Tanker Num"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_tankar" runat="server" Height="21px" TabIndex="1" 
                                    Width="126px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                                    ControlToValidate="txt_tankar" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" 
                                    ControlToValidate="txt_tankar" ErrorMessage="Numeric only..." 
                                    ValidationExpression="^[0-9,a-z,A-Z]{1,15}$"></asp:RegularExpressionValidator>
                               </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lbl_tc" runat="server" CssClass="style6" ForeColor="#3399FF" 
                                    style="color: #000000" Text="DC  Number"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_tcnum" runat="server" height="21px" TabIndex="2" 
                                    width="126px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                                    ControlToValidate="txt_tcnum" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" 
                                    ControlToValidate="txt_tcnum" ErrorMessage="Numeric only..." 
                                    ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="style7">
                                </td>
                            <td align="left">
                                <asp:Label ID="lbl_mkg" runat="server" CssClass="style6" ForeColor="#3399FF" 
                                    style="color: #000000" Text="Milk Kg"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_Milkkg" runat="server" height="21px" 
                                    ontextchanged="txt_Milkkg_TextChanged" TabIndex="3" width="126px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txt_Milkkg" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" class="style7">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator11" 
                                    runat="server" ControlToValidate="txt_Milkkg" ErrorMessage="Numeric only..." 
                                    ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                                </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lbl_session1" runat="server" CssClass="style6" 
                                    ForeColor="#3399FF" style="color: #000000" Text="Fat"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_fat" runat="server" height="21px" TabIndex="4" 
                                    width="126px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="txt_fat" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator12" 
                                    runat="server" ControlToValidate="txt_fat" ErrorMessage="Numeric only..." 
                                    ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lbl_session3" runat="server" CssClass="style6" 
                                    ForeColor="#3399FF" style="color: #000000" Text="Clr"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_Clr" runat="server" AutoPostBack="True" height="21px" 
                                    ontextchanged="txt_Clr_TextChanged" TabIndex="5" width="126px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                                    ControlToValidate="txt_Clr" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator13" 
                                    runat="server" ControlToValidate="txt_Clr" ErrorMessage="Numeric only..." 
                                    ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lbl_session2" runat="server" CssClass="style6" 
                                    ForeColor="#3399FF" style="color: #000000" Text="Snf"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_SNF" runat="server" AutoPostBack="True" height="21px" 
                                    ontextchanged="txt_SNF_TextChanged" TabIndex="6" width="126px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                    ControlToValidate="txt_SNF" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" 
                                    runat="server" ControlToValidate="txt_SNF" ErrorMessage="Numeric only..." 
                                    ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lbl_session11" runat="server" 
                                    ForeColor="#3399FF" style="color: #000000; font-family: Andalus;" 
                                    Text="Rate"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_rate" runat="server" AutoPostBack="True" Height="21px" 
                                    ontextchanged="txt_rate_TextChanged" TabIndex="7" Width="126px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                    ControlToValidate="txt_rate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" 
                                    runat="server" ControlToValidate="txt_rate" ErrorMessage="Numeric only..." 
                                    ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td align="left">
                                <asp:Label ID="lbl_session12" runat="server" CssClass="style6" 
                                    ForeColor="#3399FF" style="color: #000000" Text="Amount"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_Amount" runat="server" Enabled="False" Height="21px" 
                                    TabIndex="8" Width="126px">0</asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                    ControlToValidate="txt_Amount" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" 
                                    runat="server" ControlToValidate="txt_Amount" ErrorMessage="Numeric only..." 
                                    ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td class="style8">
                                <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                                    Height="16px" Visible="false" Width="29px">
                                </asp:DropDownList>
                                <asp:TextBox ID="txt_PlantName" runat="server" Enabled="False" Height="16px" 
                                    TabIndex="10" Visible="False" Width="43px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btn_Save1" runat="server" BackColor="#6F696F" CssClass="button" 
                                    Font-Bold="False" ForeColor="White" Height="25px" onclick="btn_Save_Click" 
                                    style="font-size: small; font-family: Andalus;" TabIndex="9" Text="SAVE" 
                                    Width="75px" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr align="center">
                <td align="left">
                    <asp:Label ID="lbl_msg" runat="server" 
                        style="font-weight: 700;" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
         </asp:Panel>
         </center>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <table class="style4">
                <tr>
                    <td colspan="2">
                        <asp:Panel ID="Panel3" runat="server">
                            <center>
                        <br />
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                            CssClass="gridview2" Font-Size="14px"                             
                             DataKeyNames="Sno" 
                            onrowdatabound="GridView1_RowDataBound" 
                            onrowdeleting="GridView1_RowDeleting" 
                            BorderStyle="Inset" BorderWidth="1px" PageSize="15">
                            <Columns>
                             <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno" />
                               <asp:BoundField DataField="Plant_To" HeaderText="Plant_To" SortExpression="Plant_To" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                <asp:BoundField DataField="tcnumber" HeaderText="DcNo" SortExpression="tcnumber" />
                <asp:BoundField DataField="Tanker_No" HeaderText="TankerNo"  SortExpression="Tanker_No" />
                <asp:BoundField DataField="MilkKg" HeaderText="MilkKg" SortExpression="MilkKg" />
                <asp:BoundField DataField="Fat" HeaderText="Fat" SortExpression="Fat" />
                <asp:BoundField DataField="Snf" HeaderText="Snf" SortExpression="Snf" />                   
                <asp:BoundField DataField="Clr" HeaderText="Clr" SortExpression="Clr" /> 


                                <asp:CommandField ShowDeleteButton="True" />




                            </Columns>
                        </asp:GridView>
                        </center>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <%-- <Triggers>   
    
 <asp:PostBackTrigger ControlID ="btn_Save1" />    
    </Triggers>--%>



    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
