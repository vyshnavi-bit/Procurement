<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PLANTOVERALLREPORT.aspx.cs" Inherits="PLANTOVERALLREPORT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            color: #000000;
            font-weight: 700;
        }
        .style2
        {
            text-align: right;
        }
    </style>
  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;Total Abstract</p>
            </td>
        </tr>
        <tr>
            <td width="100%" height="3px">
            </td>
        </tr>
        <tr>
            <td width="100%" class="line" height="1px">
            </td>
        </tr>
        <tr>
            <td width="100%" height="7">
                
            </td>
        </tr>
        </table>
 </ContentTemplate>
 </asp:UpdatePanel>
 

   <br />
   <table width=40% ALIGN="center">
   <tr>
   <td bgcolor="#FFEFF6">
   <fieldset class="fontt">   
            <legend style="color: #3399FF; width: 479px;"><span class="style1">TotalAbstract</span> </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
                     <td class="style2">
                       &nbsp;<asp:Label ID="Label2" runat="server" Text="From" CssClass="style1"></asp:Label>      
                    </td>
                    <td  align="left">                                
                                <asp:TextBox ID="txt_FromDate" runat="server"  ></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>

                            </td>
                </tr>  
                  <tr>
                     <td class="style2">                   
                         <asp:Label ID="Label3" runat="server" Text="To" CssClass="style1"></asp:Label> 
                         <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>     
                    </td>
                    <td  align="left">                    	
                              <asp:TextBox ID="txt_ToDate" runat="server"  ></asp:TextBox>                    	
                    </td>
                </tr> 
                 <tr>
                     <td class="style2"> 
             <asp:Label ID="Label1" runat="server" Text="Plant_Name" CssClass="style1"></asp:Label>      
                    </td>
                    <td  align="left"> 
                                      	
                    	<asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
        Width="200px" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
                 Font-Bold="True" Font-Size="Large" ></asp:DropDownList>

                    </td>
                </tr> 
                 <tr>
                     <td class="style2"> 
             <asp:Label ID="mtype" runat="server" Text="Milk Type" CssClass="style1"></asp:Label>      
                    </td>
                    <td  align="left">
                                      	
                    	<asp:RadioButtonList ID="miltype" runat="server" RepeatDirection="Horizontal" 
                            CssClass="style1">
                            <asp:ListItem Value="1">Cow</asp:ListItem>
                            <asp:ListItem Value="2">Buffalo</asp:ListItem>
                        </asp:RadioButtonList>

                    </td>
                </tr> 
                  <tr>
                     <td class="style2">                   
         <asp:Label ID="lbl_RouteName" runat="server" Text="Route Name" CssClass="style1"></asp:Label>

                    </td>
                    <td align="left">        	

             <asp:CheckBox ID="chk_Allloan" runat="server" AutoPostBack="True" 
                 Checked="True" oncheckedchanged="chk_Allloan_CheckedChanged" Text="ALL" 
                            CssClass="style1" />
   
             <asp:CheckBox ID="chk_Buff" runat="server"  Text="Buff" Visible="False" 
                            CssClass="style1" />
   
             <asp:CheckBox ID="chk_print" runat="server" Text="Print" CssClass="style1"  />
   
                    </td>
                </tr> 
            <tr>
                     <td class="style2">
         
             <asp:Label ID="lbl_RouteID" runat="server" Text="Route ID" Visible="False" 
                             CssClass="style1"></asp:Label>

                    </td>
                    <td  align="left">
         <asp:DropDownList ID="ddl_RouteID" 
        runat="server" Width="29px" 
        onselectedindexchanged="ddl_RouteID_SelectedIndexChanged" 
        AutoPostBack="True" Enabled="False" Visible="False" Font-Size="Large" Height="16px">
    </asp:DropDownList>
 <asp:DropDownList ID="ddl_RouteName" 
        runat="server" Width="27px" 
        onselectedindexchanged="ddl_RouteName_SelectedIndexChanged" 
         AutoPostBack="True" Font-Size="Large" Height="16px" 
       >
    </asp:DropDownList>
                    </td>
                </tr>   
                 <tr>
                     <td>                   
         
                     <asp:DropDownList ID="ddl_Plantcode" AutoPostBack="true" runat="server" 
                       Visible="false" Height="16px" Width="29px" > </asp:DropDownList>                        

                    </td>
                    <td  align="left">                    	

 <asp:Button ID="Button1" runat="server" onclick="Button1_Click"
            Text="OK" Width="70px" Height="30px" CssClass="form93" />
                        <asp:Button ID="btn_Export" runat="server"
            Text="Export" Width="100px" Height="30px" onclick="btn_Export_Click" CssClass="form93"  />
           
                        <asp:Button ID="ExportToExcel" runat="server" 
         BackColor="#6F696F"  ForeColor="White"
            Text="Export" Width="70px" Height="26px" onclick="ExportToExcel_Click" Visible="False"  />
           
                    </td>
                </tr>  
                     
                
            </table>
            <br />
          
   </fieldset>
   </td>
   </tr>
 </table>
   

   <center>

            <asp:GridView ID="GridView1" runat="server" Font-Size="10pt">
			</asp:GridView>

            </center>




<br />


  <center>

        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" EnableDatabaseLogonPrompt="False" 
          EnableParameterPrompt="False" ToolPanelView="None" 
            onunload="CrystalReportViewer1_Unload"/>
    </center>

   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

