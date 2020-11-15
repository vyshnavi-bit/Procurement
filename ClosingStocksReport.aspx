<%@ Page Title="Online Milk Test|Closing Stock" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ClosingStocksReport.aspx.cs" Inherits="ClosingStocksReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .stylefiledset
        {
           width:40%; 
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
 

    
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%" colspan="2"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;CLOSING STOCK DETAILS
                </p>
            </td>
        </tr>
        <tr>
            <td width="100%" height="3px" colspan="2">
            </td>
        </tr>
        <tr>
            <td width="100%" class="line" height="1px" colspan="2">
            </td>
        </tr>
        <tr>
            <td width="100%" height="7" colspan="2">
                
            </td>
        </tr>
        </table>
 </ContentTemplate>
 </asp:UpdatePanel>
 <br />
 <center>
 <div class="stylefiledset">
   <fieldset class="fontt">   
            <legend style="color: #3399FF">Closing Stock Details</legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">            
             <tr>
                     <td align="right">
                       &nbsp;<asp:Label ID="Label2" runat="server" Text="From"></asp:Label>      
                    </td>
                    <td style="text-align: left" >
                  
                        <asp:TextBox ID="txt_FromDate" runat="server"></asp:TextBox>
                  
                    </td>
                </tr>  
                  <tr>
                     <td align="right">                   
                         <asp:Label ID="Label3" runat="server" Text="To"></asp:Label> 
                           
                    </td>
                    <td  align="left">
                  
                        <asp:TextBox ID="txt_ToDate" runat="server"></asp:TextBox>
                  
                    </td>
                </tr> 
                 <tr>
                     <td align="right"> 
             <asp:Label ID="Label1" runat="server" Text="Plant_Name"></asp:Label>      
                    </td>
                    <td style="text-align: left"  >                  
                        <asp:DropDownList ID="ddl_PlantName" runat="server" AutoPostBack="True" 
                            Width="200px" Font-Bold="True" Font-Size="Large" 
                            onselectedindexchanged="ddl_PlantName_SelectedIndexChanged">
                        </asp:DropDownList>
                     </td>
                </tr> 
                  
            <tr>
                     <td  align="left">
         
                         <asp:DropDownList ID="ddl_PlantID" runat="server" AutoPostBack="True" 
                             Height="16px" Visible="False" Width="26px">
                         </asp:DropDownList>
                    </td>
                    <td style="text-align: left" >                         
                        <asp:Button ID="Button2" runat="server" BackColor="#6F696F" ForeColor="White" 
                            onclick="Button2_Click" Text="OK" Width="80px" />
                    </td>
                </tr>   
                
                
            </table>
            <br />
          
   </fieldset>
   </div>  
   </center>
   



    <table width="100%" ALIGN="center">
        <tr>
            <td>

   



    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" EnableDatabaseLogonPrompt="False" 
        EnableParameterPrompt="False" ToolPanelView="None" style="text-align: center" 
                    onunload="CrystalReportViewer1_Unload"/>


            </td>
        </tr>
    </table>


</asp:Content>


