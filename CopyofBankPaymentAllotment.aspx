<%@ Page Title="OnlineMilkTest|BankPaymentAllotment" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CopyofBankPaymentAllotment.aspx.cs" Inherits="CopyofBankPaymentAllotment" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DropDownCheckBoxes" Namespace="Saplin.Controls" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

.legendpermission
{ background-color:#FFFAFA;
	margin-left:50px;
   margin-right:30px;
 width :818px;
	
}
div#spinner
{
    display: none;
    width:100px;
    height: 100px;
    position: fixed;
    top: 50%;
    left: 50%;
    text-align:center;
    margin-left: -50px;
    margin-top: -100px;
    z-index:2;
    overflow: auto;
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
    font-weight: normal;
    font-style: normal;
}
 .fontt
{
	font-weight: normal;
    font-style: normal;
	color: #000000;
            font-size: small;
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
 float:left;
  
}
.cellsize
{ height:auto;
  width:auto;
    
}

  .checkpanelsize
{  margin-left:12px;
	margin-top:30px;
	margin-bottom:0px;
}

.OverlayEffect
    {
        background-color: black;
        filter: alpha(opacity=70);
        opacity: 0.7;
        width: 100%;
        height: 100%;
        z-index: 400;
        position: absolute;
        top: 0;
        left: 0;
    }
	  	
        .style2
        {
            width: 79%;
            text-align: left;
        }
	  	
        </style>   
    <script type="text/javascript" src="jquery.min.js"></script>
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


<script type="text/javascript">
    $("[id*=chkHeader]").live("click", function () {
        var chkHeader = $(this);
        var grid = $(this).closest("table");
        $("input[type=checkbox]", grid).each(function () {
            if (chkHeader.is(":checked")) {
                $(this).attr("checked", "checked");
                $("td", $(this).closest("tr")).addClass("selected");
            } else {
                $(this).removeAttr("checked");
                $("td", $(this).closest("tr")).removeClass("selected");
            }
        });
    });
    $("[id*=chkRow]").live("click", function () {
        var grid = $(this).closest("table");
        var chkHeader = $("[id*=chkHeader]", grid);
        if (!$(this).is(":checked")) {
            $("td", $(this).closest("tr")).removeClass("selected");
            chkHeader.removeAttr("checked");
        } else {
            $("td", $(this).closest("tr")).addClass("selected");
            if ($("[id*=chkRow]", grid).length == $("[id*=chkRow]:checked", grid).length) {
                chkHeader.attr("checked", "checked");
            }
        }
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>       
<div  >
<asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>       
        <div style="position: fixed; text-align: center; height: 10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: Gray; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>
<asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup"  />
</div>  
       
        <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
            <br />
            <div style="width:100%;">
                  <table   width="100%">
                  <tr>
                  <td width="18%"></td>
                   <td width="10%" class="fontt">
										From Date:</td>
										
                    <td width="15%" class="fontt" align="right">
                                <asp:TextBox ID="txt_FromDate" runat="server" Enabled="False"  ></asp:TextBox>

                            </td>
                                <td width="5%">
                                 
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
                              
                               </td>
                       <td width="9%" class="fontt">
       
                         To Date:</td>
                             
                               <td width="15%" class="fontt" align="right">
                              <asp:TextBox ID="txt_ToDate" runat="server" Enabled="False"  ></asp:TextBox></td>
                               <td width="12%">
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
                         
                          </td> 
                          <td width="20%">
                              <asp:HyperLink ID="HyperLink1" runat="server" CssClass="fontt" 
                                  NavigateUrl="~/BankPaymentSelectedList.aspx">Go to Selected List</asp:HyperLink>
                      </td>    
                       </tr>
                   </table>
   </div>
    <center>
    <table align=center width="50%" >
    <tr>
    <td>
    
    <fieldset align="center" width="100%">
                <table align="center" style="width: 100%">
                    <tr>
                        <td align="right" width="35%">
                            <asp:Label ID="lbl_totbillamount" runat="server" Text="Total Bill Amount"></asp:Label>
                        </td>
                        <td align="left" class="style2">
                            <asp:TextBox ID="txt_totbillamount" runat="server" Enabled="False" 
                                Font-Bold="True" Font-Size="Large" Height="20px" style="font-weight: 700" 
                                Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="25%">
                            <asp:Label ID="lbl_bank" runat="server" Text="Bank Amount"></asp:Label>
                        </td>
                        <td align="left" class="style2">
                            <asp:TextBox ID="txt_bank" runat="server" Enabled="False" Font-Bold="True" 
                                Font-Size="Large" Height="20px" style="font-weight: 700" Width="100px"></asp:TextBox>
                            Cash<asp:TextBox ID="txt_cash" runat="server" Enabled="False" Font-Bold="True" 
                                Font-Size="Large" Height="20px" style="font-weight: 700" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="25%">
                            <asp:Label ID="lbl_assign" runat="server" Text="Payed Amount"></asp:Label>
                        </td>
                        <td align="left" class="style2">
                            <asp:TextBox ID="txt_assignamt" runat="server" Enabled="False" Font-Bold="True" 
                                Font-Size="Large" Height="20px" style="font-weight: 700" Width="100px" 
                                ontextchanged="txt_assignamt_TextChanged"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="25%">
                            &nbsp;<asp:Label ID="lbl_assign1" runat="server" Text="Assigning Amount"></asp:Label>
                        </td>
                        <td align="left" class="style2">
                            <asp:TextBox ID="txt_assignamt1" runat="server" Enabled="False" 
                                Font-Bold="True" Font-Size="Large" Height="20px" 
                                ontextchanged="txt_assignamt_TextChanged" style="font-weight: 700" 
                                Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="25%">
                            <asp:Label ID="lbl_balanceAmount" runat="server" Text="Balance Amount"></asp:Label>
                        </td>
                        <td align="left" class="style2">
                            <asp:TextBox ID="txt_balance" runat="server" Font-Bold="True" Font-Size="Large" 
                                Height="20px" Width="100px" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="25%">
                            <asp:DropDownList ID="ddl_RouteID" runat="server" AutoPostBack="True" 
                                Enabled="False" Font-Bold="True" Font-Size="Smaller" Height="16px" 
                                Visible="False" Width="26px">
                            </asp:DropDownList>
                            <asp:Label ID="lbl_PlantName" runat="server" CssClass=" " Text="Plant"></asp:Label>
                        </td>
                        <td align="left" class="style2">
                            <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
                                Font-Bold="True" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
                                Width="150px" Height="25px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lbl_BankName" runat="server" Text="Bank Name"></asp:Label>
                            <asp:CheckBox ID="Chk_Cash" runat="server" AutoPostBack="True" 
                                oncheckedchanged="Chk_Cash_CheckedChanged" Text="Cash" />
                            <asp:DropDownList ID="ddl_BankName" runat="server" AutoPostBack="True" 
                                Font-Bold="True" Height="16px" 
                                onselectedindexchanged="ddl_BankName_SelectedIndexChanged" Width="37px">
                            </asp:DropDownList>
                        </td>
                        <td class="style2">
                
                          
            
                          
            
                          
                
                          
            
                          
            
                          
            
                          
            
                          
                
                          
            
                          
            
                         
                      
            
                            <asp:DropDownCheckBoxes ID="ddchkCountry" runat="server" 
                                AddJQueryReference="True" Font-Bold="true" Font-Size="9px" 
                                OnSelectedIndexChanged="ddchkCountry_SelectedIndexChanged" 
                                Style="top: 0px; left: 82px; height: 12px;" UseButtons="True" 
                                UseSelectAllNode="True">
                                <Style DropDownBoxBoxHeight="1000" 
                                    DropDownBoxBoxWidth="240" SelectBoxWidth="240" />
                                <Texts SelectBoxCaption="Select BankName" />
                            </asp:DropDownCheckBoxes>
                
                          
            
                          
            
                          
                
                          
            
                          
            
                          
            
                          
            
                          
                
                          
            
                          
            
                         
                      
            
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lbl_RouteName" runat="server" Text="Route Name"></asp:Label>
                        </td>
                        <td class="style2">
                            <asp:CheckBox ID="chk_Allroute" runat="server" AutoPostBack="True" 
                                Checked="True" oncheckedchanged="chk_Allroute_CheckedChanged" Text="AllRoute" />
                            <br />
                            <asp:DropDownList ID="ddl_RouteName" runat="server" AutoPostBack="True" 
                                Font-Bold="True" onselectedindexchanged="ddl_RouteName_SelectedIndexChanged" 
                                Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;vertical-align:middle" width="25%">
                            <asp:Label ID="Label1" runat="server" Text="FileName"></asp:Label>
                            <br />
                            <asp:CheckBox ID="chk_OldFileName" runat="server" AutoPostBack="True" 
                                oncheckedchanged="chk_OldFileName_CheckedChanged" Text="OldFileName" />
                        </td>
                        <td align="left" class="style2">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txt_FileName" ErrorMessage="Enter FileName" Font-Size="10px" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <br />
                            <asp:TextBox ID="txt_FileName" runat="server" Font-Bold="True" Height="16px" 
                                style="font-weight: 700" Width="150px"></asp:TextBox>
                            <br />
                            <asp:DropDownList ID="ddl_oldfilename" runat="server" AutoPostBack="True" 
                                Font-Bold="True" onselectedindexchanged="ddl_oldfilename_SelectedIndexChanged" 
                                Width="150px">
                            </asp:DropDownList>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="15%">
                            <asp:Label ID="Label2" runat="server" Text="Amount Range"></asp:Label>
                        </td>
                        <td align="left" class="style2">
                            <asp:TextBox ID="amtRange" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="15%">
                            <asp:TextBox ID="Label11" runat="server" CssClass="tb10" Enabled="true" 
                                Height="25px" Visible="False" Width="142px"></asp:TextBox>
                        </td>
                        <td align="left" class="style2">
                            <asp:Button ID="btn_load" runat="server" BackColor="#6F696F" ForeColor="White" 
                                Height="26px" onclick="btn_load_Click" Text="Load" Width="70px" />
                            <asp:Button ID="btn_Check" runat="server" BackColor="#6F696F" ForeColor="White" 
                                Height="26px" onclick="btn_Check_Click" Text="Check" Width="70px" />
                            <asp:Button ID="btn_save" runat="server" BackColor="#6F696F" ForeColor="White" 
                                Height="26px" onclick="btn_save_Click" Text="Save" Width="70px" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="15%">
                            &nbsp;</td>
                        <td align="left" class="style2">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Size="Large" 
                                ForeColor="#CC3300" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="15%">
                            <asp:TextBox ID="txt_fileName123" runat="server" CssClass="tb10" Enabled="true" 
                                Height="25px" Visible="False" Width="142px"></asp:TextBox>
                            <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                                Height="17px" onselectedindexchanged="ddl_Plantcode_SelectedIndexChanged" 
                                Visible="false" Width="49px">
                            </asp:DropDownList>
                        </td>
                        <td align="right" class="style2">
                            <asp:TextBox ID="txt_Allotamount" runat="server" Enabled="False" 
                                Font-Bold="True" Font-Size="Large" Height="20px" style="font-weight: 700" 
                                Visible="False" Width="100px"></asp:TextBox>
                            <asp:Label ID="lbl_Alottedamount" runat="server" Text="Allotted Amount" 
                                Visible="False"></asp:Label>
                            <asp:Button ID="btn_updatestatus" runat="server" BackColor="#00CCFF" 
                                BorderStyle="Double" Font-Bold="True" ForeColor="White" 
                                onclick="btn_updatestatus_Click" Text="Update" Width="70px" 
                                style="text-align: right" />
                            <asp:Button ID="btn_delete" runat="server" BackColor="#00CCFF" 
                                BorderStyle="Double" Font-Bold="True" ForeColor="White" 
                                onclick="btn_delete_Click" Text="Delete" Width="70px" 
                                style="text-align: right" />
                            <br />
                        </td>
                    </tr>
                </table>
            </fieldset>


    </td>
    </tr>
    </table>
        <caption>
            
        </caption>
        </tr>

   </center>  
 <div align="center"> <asp:Label ID="lbl_ErrorMsg" runat="server" Font-Bold="True" 
         ForeColor="#CC0000" ></asp:Label></div>      
            
         
          <center>
          <table > <tr align="center" valign=top> <td style="height: auto"  >   
        <fieldset style="width: 650px; height: auto;">
            <legend style="color: #3399FF">Agent List</legend>                 
                        <table align="CENTER" style="width: 650px; height: auto;" >
                            <tr >                              
                                <td  >
                                    <asp:Panel ID="Panel3" runat="server" CssClass="checkpanelsize1" 
                                        Width="650px" align="center">
                                        <br />
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"   
                                            FooterStyle-BackColor="#3AC0F2" FooterStyle-Font-Bold="true" 
                                            FooterStyle-ForeColor="Tomato" HeaderStyle-BackColor="#3AC0F2" 
                                            HeaderStyle-ForeColor="White"> 
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNO" ItemStyle-Width="50">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" />
                                                </asp:TemplateField>
                                                   <asp:BoundField DataField="proAid" HeaderText="AgentId" ItemStyle-Width="50">
                                                    <ItemStyle Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Agent_Name" HeaderText="AgentName" ItemStyle-Width="50">
                                                    <ItemStyle Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="netpay" HeaderText="Amount" ItemStyle-Width="75">
                                                    <ItemStyle Width="75px" />
                                                </asp:BoundField>
                                                
                                                <asp:TemplateField HeaderText="EditAmount">
                                                    <ItemTemplate>
                                                       <asp:TextBox ID="txtchkamount" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRow" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#3AC0F2" Font-Bold="True" ForeColor="Tomato" />
                                            <HeaderStyle BackColor="#3AC0F2" ForeColor="White" />
                                        </asp:GridView>
                                        <br />
                                        <%--<asp:Table ID="Table2" runat="Server" BorderColor="White" BorderWidth="1" 
                                            CaptionAlign="Top" CellPadding="1" CellSpacing="1" Height="40px" Width="294px" 
                                            style="font-size: small">
                                            <asp:TableRow ID="TableRow1" runat="Server" BorderWidth="1" Width="300px">
                                                <asp:TableCell ID="TableCell2" runat="Server" BorderWidth="1"> <asp:Table ID="Table1" runat="Server" BorderColor="White" BorderWidth="1" CaptionAlign="Top" CellPadding="1" CellSpacing="1" Height="40px" Width="300px"><asp:TableRow ID="TableRow12" runat="Server" BackColor="#3399CC" BorderColor="Silver" BorderWidth="1" ForeColor="white" Width="150px"><asp:TableCell ID="TableCell4" runat="Server" BorderWidth="2" > <asp:CheckBox ID="MChk_Menu1" runat="server" Width="170px" AutoPostBack="True" Enabled="false"  Text="Agent_Id" oncheckedchanged="MChk_Menu1_CheckedChanged"  /></asp:TableCell><asp:TableCell ID="TableCell5" runat="Server" BorderWidth="2" > <asp:CheckBox ID="MChk_Menu2" runat="server" Width="120px" AutoPostBack="True" Text="Amount" oncheckedchanged="MChk_Menu2_CheckedChanged" /></asp:TableCell></asp:TableRow><asp:TableRow ID="TableRow2" runat="Server" BackColor="#fffafa" BorderColor="Silver" BorderWidth="1"><asp:TableCell ID="TableCell1" runat="Server" BorderWidth="1" > <asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderWidth="0" Enabled="false" ></asp:CheckBoxList></asp:TableCell><asp:TableCell ID="TableCell3" runat="Server" BorderWidth="1" > <asp:CheckBoxList ID="CheckBoxList2" runat="server" BorderWidth="0"></asp:CheckBoxList></asp:TableCell></asp:TableRow></asp:Table></asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>--%>
                                    </asp:Panel>
                                     
                                </td>
                            </tr>
                        </table>
                  
              </fieldset>
              </td></tr>  </table>
              </center>
    
        

            </ContentTemplate>
        </asp:UpdatePanel>  
  </asp:Content>