<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TestGrid.aspx.cs" Inherits="TestGrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link type="text/css" href="App_Themes/StyleSheet.css" rel="Stylesheet" />
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%" colspan="2"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;VEHICLE MASTER
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
   
   <div class="legagentsms">
   <fieldset class="fontt">   
            <legend style="color: #3399FF">VehicleDistance Report </legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">
            <tr>
            <td>
                <asp:TextBox ID="TextBox1" runat="server" Height="16px" Width="61px" Visible="false"></asp:TextBox>
            </td>
            </tr>            
             <tr>
                    <td>
                     <asp:DropDownList ID="ddl_Plantcode" AutoPostBack="true" runat="server" 
                       Visible="false" Height="16px" Width="29px"> </asp:DropDownList>                        
                    </td>
                     <td align="right">
                       Plant_Name:
                    </td>
                    <td >
                  
                    </td>
                    <td  align="left">
                    	<asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="true" 
        Width="170px" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" ></asp:DropDownList>
                    </td>
                </tr>  
                  <tr>
                    <td>                                    
                    </td>
                     <td>                   
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                    </td>
                </tr> 
                 <tr>
                    <td>                                    
                    </td>
                     <td align="right"> 
                     From :                  
                    </td>
                    <td  >                  
                    </td>
                    <td  align="left"> 
                     <asp:TextBox ID="txt_FromDate" runat="server" ></asp:TextBox>
                         <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="MM/dd/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>                   	
                    </td>
                </tr> 
                  <tr>
                    <td>                                    
                    </td>
                     <td>                   
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                    </td>
                </tr> 
            <tr>
                    <td>
                    	
                    </td>
                     <td  align="right">
                       To :
                    </td>
                    <td >                         
                    </td>
                    <td  align="left">
                        <asp:TextBox ID="txt_ToDate" runat="server"  ></asp:TextBox></td>
                               <td width="12%">
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="MM/dd/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
                    </td>
                </tr>   
                 <tr>
                    <td>                                    
                    </td>
                     <td>                   
                    </td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                    <asp:Button ID="btn_Ok" runat="server" Text="OK"  BackColor="#6F696F"
                     ForeColor="White" Width="50px" style="height: 26px" onclick="btn_Ok_Click" 
                             />                                              
                    </td>
                </tr>  
                     
                
            </table>
            <br />            
            
 <br /> 
   </fieldset>
   </div>
   
          <br />

<div align="center">
 <div class="grid">
    <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" 
        DataKeyNames="id" OnPageIndexChanging="GridView1_PageIndexChanging" 
        onrowcancelingedit="gvProducts_RowCancelingEdit" 
        onrowdeleting="gvProducts_RowDeleting" onrowediting="gvProducts_RowEditing" onrowupdating="gvProducts_RowUpdating" >
     <Columns>
        <asp:BoundField DataField="Truck_Id" HeaderText="Truck_Id" />
        <asp:BoundField DataField="Tdistance" HeaderText="Tdistance" />
        <asp:BoundField DataField="Pdate" HeaderText="Pdate" />       
       
        <asp:CommandField ShowEditButton="true" />
        <asp:CommandField ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>
   
       
        </div>   
        </div>
    
<br />
    <br />
    
  



<uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
</asp:Content>


