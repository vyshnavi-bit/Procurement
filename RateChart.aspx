<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RateChart.aspx.cs" Inherits="RateChart" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<style type="text/css">
    
   .style1
   {
       width:90%;
       
  
      style="background-color: #CCFFFF";
   } 
   
      
     .style5
    {
        font-family: Andalus;
        font-size: small;
        
    }
    
    
    .style6
    {
        font-family: Andalus;
        font-size: medium;
    }
    
    .style15
    {
        width:25%;
        
    }
     .style17
    {
        width:15%;
        
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
    
    
    .style213
    {
        width: 100%;
    }
    
    
    .style214
    {
        color: #FF0000;
    }
    
    
    .style215
    {
        font-size: small;
        font-family: Andalus;
    }
    
    
    .style216
    {
        width: 15%;
        font-family: Andalus;
        font-size: small;
        height: 26px;
    }
    .style217
    {
        height: 26px;
    }
    
    
        .style218
        {
            font-size: medium;
        }
    
    
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">




    <asp:UpdateProgress ID="UpdateProgress" runat="server"   >
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height:10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>


<asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"    style="background-color: #CCFFFF" >
            <ContentTemplate>




    <div style="background-color= #CCFFCC; width:100%;"  >
<div id="main">
   
   	<div align="center" style="width:100%;"   style="background-color: #CCFFCC";>
  	<fieldset  class="style1" style="background-color:  #CCFFCC">
 
   	<legend class="fontt">RATECHART CREATION</legend>

    <asp:Panel runat=server>
    <table border="0"  id="table2"  width="100%">

        <caption>
            <table  class="style213">
                <tr align="left"  >
                    <td  class="style5" colspan="3">
                        <span class="style6">FromDate</span><asp:TextBox ID="txt_fromdate" 
                            runat="server" CssClass="tb10" Height="25px" Width="100px"></asp:TextBox>
                        <asp:CalendarExtender ID="txt_fromdate_CalendarExtender" runat="server" 
                            Format="MM/dd/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                            TargetControlID="txt_fromdate">
                        </asp:CalendarExtender>
                        <span class="style6">ToDate</span><asp:TextBox ID="txt_todate" runat="server" 
                            CssClass="tb10" Height="25px" Width="100px"></asp:TextBox>
                        MM/DD/YYYYY</td>
                    <td valign=top rowspan="15" >
                   <asp:Panel ID=panel123 runat=server  BorderWidth="1px" Width=292px BorderStyle=Inset BorderColor=Crimson >
              <center>         <asp:GridView ID="GridView1" runat="server" AllowPaging="True" Height="79px" PageSize="30"   HeaderStyle-BackColor=White HeaderStyle-ForeColor=Brown
                            style="text-align: center; font-size:small; font-family: Andalus;" 
                            Font-Size="8px">
                            <Columns>
                            <asp:TemplateField>
                            
                            <HeaderTemplate>
                            Sno
                                                     
                            </HeaderTemplate>
                            
                            <ItemTemplate>
                            <asp:Label ID="lblSRNO" runat="server"  Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                            
                            </Columns>
                        </asp:GridView></center>
                        </asp:Panel>
                    </td>
                </tr>
                 <tr align="left">
                     <td class="style5">
                         <asp:Label ID="Label1" runat="server" 
                             style="font-family: Andalus; font-size: medium" Text="Plant Name"></asp:Label>
                     </td>
                     <td align="left" class="style15">
                         <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
                             onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="175px">
                         </asp:DropDownList>
                     </td>
                     <td class="style17">
                     </td>
                </tr>
                 <tr align="left"  >
                    <td  class="style216">
                        <asp:Label ID="Label2" runat="server" 
                            style="font-family: Andalus; font-size: medium" Text="Milk Type"></asp:Label>
                    </td>
                    <td class="style217">
                        <asp:RadioButton ID="rd_cow" runat="server" AutoPostBack="True" 
                            oncheckedchanged="rd_cow_CheckedChanged" TabIndex="2" Text="Cow" 
                            CssClass="style215" />
                        <asp:RadioButton ID="rd_buffalo" runat="server" AutoPostBack="True" 
                            oncheckedchanged="rd_buffalo_CheckedChanged" TabIndex="3" Text="Buffalo" 
                            CssClass="style215" />
                     <panel>
                                    <td align="left" class="style217">
                                        </td>
                                </panel>
                      </td>
                </tr>
                <tr align="left"  >
                    <td  class="style5">
                        <asp:Label ID="Label3" runat="server" 
                            style="font-family: Andalus; font-size: medium" Text="Chart Type"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="dl_charttype" runat="server" TabIndex="4">
                            <asp:ListItem Value="0">-SelectChartType-</asp:ListItem>
                            <asp:ListItem Value="1">TS</asp:ListItem>
                            <asp:ListItem Value="2">FAT</asp:ListItem>
                            <asp:ListItem Value="3">SNF</asp:ListItem>
                            <asp:ListItem Value="4">FATSNF</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
               <tr align="left"  >
                    <td  class="style5">
                        <asp:Label ID="Label4" runat="server" 
                            style="font-family: Andalus; font-size: medium" Text="State Name"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="dl_statename" runat="server" 
                            onselectedindexchanged="dl_statename_SelectedIndexChanged" TabIndex="5">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddl_stateid0" runat="server" 
                            onselectedindexchanged="ddl_stateid_SelectedIndexChanged" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr align="left"  >
                    <td  class="style5">
                        <asp:Label ID="Label5" runat="server" 
                            style="font-family: Andalus; " Text="Chart Name" CssClass="style218"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_chartname" runat="server" CssClass="tb10" Height="20px" 
                            TabIndex="6"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="txt_chartname" CssClass="style214" 
                            ErrorMessage="Character only." style="font-size: xx-small" 
                            ValidationExpression="^[0-9a-zA-Z,.]{1,30}$">
         </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr align="left"  >
                    <td  class="style5">
                        <asp:Label ID="Label6" runat="server" 
                            style="font-family: Andalus; " Text="Minimum Fat" CssClass="style218"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_minfat" runat="server" CssClass="tb10" Height="20px" 
                            TabIndex="7"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" 
                            ControlToValidate="txt_minfat" CssClass="style214" 
                            ErrorMessage="Numeric only..." style="font-size: small; font-family: Andalus" 
                            ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                 <tr align="left"  >
                    <td  class="style5">
                        <asp:Label ID="Label7" runat="server" 
                            style="font-family: Andalus; " Text="Minimum Snf" CssClass="style218"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_minsnf" runat="server" CssClass="tb10" Height="20px" 
                            TabIndex="8"></asp:TextBox>
                     </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                            ControlToValidate="txt_minsnf" CssClass="style214" 
                            ErrorMessage="Numeric only..." style="font-size: small; font-family: Andalus" 
                            ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                     </td>
                </tr>
                <tr align="left"  >
                    <td  class="style5">
                        <asp:Label ID="Label8" runat="server" 
                            style="font-family: Andalus; " Text="Range From" CssClass="style218"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_fromrangevalue" runat="server" CssClass="tb10" 
                            Height="20px" TabIndex="9"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                            ControlToValidate="txt_fromrangevalue" CssClass="style214" 
                            ErrorMessage="Numeric only..." style="font-size: small; font-family: Andalus" 
                            ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr align="left"  >
                    <td  class="style5">
                        <asp:Label ID="Label9" runat="server" 
                            style="font-family: Andalus; " Text="Range To" CssClass="style218"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_torangevalue" runat="server" CssClass="tb10" Height="20px" 
                            TabIndex="10"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" 
                            runat="server" ControlToValidate="txt_torangevalue" CssClass="style214" 
                            ErrorMessage="Numeric only..." style="font-size: small; font-family: Andalus" 
                            ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                 <tr align="left"  >
                    <td  class="style5">
                        <asp:Label ID="Label10" runat="server" 
                            style="font-family: Andalus; " Text="Rate" CssClass="style218"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_rate" runat="server" CssClass="tb10" Height="20px" 
                            TabIndex="11"></asp:TextBox>
                     </td>
                    <td>
                        <strong><span class="style215">Rs</span></strong>&nbsp;<asp:RegularExpressionValidator 
                            ID="RegularExpressionValidator11" runat="server" ControlToValidate="txt_rate" 
                            CssClass="style214" ErrorMessage="Numeric only..." 
                            style="font-size: small; font-family: Andalus" 
                            ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                     </td>
                </tr>
               <tr align="left"  >
                    <td  class="style5">
                        <asp:Label ID="Label11" runat="server" 
                            style="font-family: Andalus; " Text="Comm" CssClass="style218"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_commissionamount" runat="server" CssClass="tb10" 
                            Height="20px" TabIndex="12"></asp:TextBox>
                    </td>
                    <td>
                        <span class="style215"><strong>Rs</strong></span>&nbsp;<asp:RegularExpressionValidator 
                            ID="RegularExpressionValidator12" runat="server" 
                            ControlToValidate="txt_commissionamount" CssClass="style214" 
                            ErrorMessage="Numeric only..." style="font-size: small; font-family: Andalus" 
                            ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr align="left"  >
                    <td  class="style5">
                        <asp:Label ID="Label12" runat="server" 
                            style="font-family: Andalus; " Text="Bonus Amt" CssClass="style218"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_bonusamount" runat="server" CssClass="tb10" Height="20px" 
                            TabIndex="13"></asp:TextBox>
                    </td>
                    <td>
                        <span class="style215"><strong>Rs</strong></span>&nbsp;<asp:RegularExpressionValidator 
                            ID="RegularExpressionValidator13" runat="server" 
                            ControlToValidate="txt_bonusamount" CssClass="style214" 
                            ErrorMessage="Numeric only..." style="font-size: small; font-family: Andalus" 
                            ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr align="left">
                    <td class="style5">
                        <asp:DropDownList ID="ddl_stateid" runat="server" 
                            onselectedindexchanged="ddl_stateid_SelectedIndexChanged" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnCreateratechart" runat="server" CssClass="buttonclass" 
                            Font-Bold="true" onclick="btnCreateratechart_Click" 
                            OnClientClick="return confirm('Are you sure you want to Create this record?');" 
                            style="font-weight: 700; font-family: Andalus; font-size: small;" TabIndex="14" 
                            Text="Create" Width="55px" />
                        <asp:Button ID="btn_Save" runat="server" BorderStyle="Groove" BorderWidth="1px" 
                            CssClass="buttonclass" onclick="btn_Save_Click" 
                            OnClientClick="return confirm('Are you sure you want to Save this record?');" 
                            style="font-weight: 700; font-family: Andalus; font-size: small;" TabIndex="15" 
                            Text="Save" Width="50px" />
                        <asp:Button ID="Button1" runat="server" BorderStyle="Groove" BorderWidth="1px" 
                            CssClass="buttonclass" Font-Bold="true" onclick="Button1_Click" 
                            style="font-weight: 700; font-family: Andalus; font-size: small;" TabIndex="16" 
                            Text="View RateChart" Width="120px" />
                    	<br />
					<center>	<asp:LinkButton ID="LinkButton1" runat="server" 
							PostBackUrl="~/MixedRatechartsetting.aspx" style="font-family: Andalus">Current Chart Settings</asp:LinkButton>  
						<br />
						<asp:LinkButton ID="LinkButton2" runat="server" 
							PostBackUrl="~/UploadRatechart.aspx" style="font-family: Andalus">Upload Rate Chart</asp:LinkButton>
						</center>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                            Height="16px" Visible="false" Width="29px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr align="left">
                    <td class="style5" colspan="3">

                     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
                                EnablePageMethods="true">
                            </asp:ToolkitScriptManager>
                            <asp:HiddenField ID="htnbox" runat="server" />
                       </td>
                </tr>
            </table>
           

            <caption>
                <tr>
                    <td align="left" class="style15" width="30%">
                        <table width="100%">
                           <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
                                EnablePageMethods="true">
                            </asp:ToolkitScriptManager>
                            <asp:HiddenField ID="htnbox" runat="server" />--%>
                        </table>
                    </td>
                </tr>
            </caption>
        </caption>
    
    </table>
    </asp:Panel>

    <table>
    </table>
    </fieldset>
	</div>
    <div>
        <uc1:uscMsgBox ID="uscMsgBox1" runat="server" /> 
    </div>

    <br />
    
		
    
      
</div>
 
</div> 
<uc1:uscMsgBox ID="uscMsgBox2" runat="server" /> 


  </ContentTemplate>
        </asp:UpdatePanel>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

