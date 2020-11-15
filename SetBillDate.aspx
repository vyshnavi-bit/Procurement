<%@ Page Title="OnlineMilkTest|SetBillDate" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SetBillDate.aspx.cs" Inherits="SetBillDate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="App_Themes/EditGrid.css" rel="stylesheet" />
     <style type="text/css">
        .style1
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            width: 10%;
        }
        .style2
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            width: 28%;
        }
        .style3
        {
            color: #3399FF;
        }
        
        
.stylesfiledset
{
    
    width:40%;
}
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <br />
   <div  ALIGN=center>
   <fieldset   class=stylesfiledset>
            <legend style="color: #3399FF">Set BillDate</legend>
            <table border="0" width="100%" id="table4" cellspacing="1" align="center">
            <tr>
            <td>
                &nbsp;</td>
            </tr>            
             <tr>
                    <td>
                     <asp:DropDownList ID="ddl_Plantcode" AutoPostBack="true" runat="server" 
                       Visible="false" Height="16px" Width="29px"> </asp:DropDownList>                        
                    </td>
                     <td align="right">
                         <asp:Label ID="Lbl_PlantName" runat="server" Text="Plant Name"></asp:Label>
&nbsp;</td>
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
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>                   	
                    </td>
                </tr> 
                  <tr>
                    <td>                                    
                    </td>
                     <td align="right">                   
                       To :</td>
                    <td  align="right">
                  
                    </td>
                    <td  align="left">                    	
                        <asp:TextBox ID="txt_ToDate" runat="server"  ></asp:TextBox>                    	
                    </td>
                </tr> 
            <tr>
                    <td>
                    	
                    </td>
                     <td  align="right">
                         BillDescriptions</td>
                    <td >                         
                    </td>
                    <td  align="left">
                        <asp:TextBox ID="txt_BillDescription" runat="server" Height="28px" 
                            Width="197px"  ></asp:TextBox>                                 
                    </td>
                               <td width="12%">
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
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
                     
                
            </table>          
            <table border="0" width="100%" id="table5" cellspacing="1" align="center">
                <tr>
                    <td >
                        &nbsp;</td>
                    <td align="center" >
                    <asp:Button ID="btn_SetBilldate" runat="server" Text="Save"  BackColor="#6F696F"
                     ForeColor="White" Width="70px" style="height: 26px" 
                            onclick="btn_SetBilldate_Click"/>                                              

             <asp:Button ID="btn_Export" runat="server" Text="Export" 
                    BackColor="#6F696F" ForeColor="White" TabIndex="10" 
                    Width="56px" onclick="btn_Export_Click"/>
                    </td>
                    <td align="right">
                        &nbsp;</td>
                </tr>
            </table>
 <br /> 
   </fieldset>
   </div>
   <br />
   <br />
   <div align="center"> 
 <div class="pager">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="datatable"
        onrowcancelingedit="GridView1_RowCancelingEdit" Width="615px" onrowediting="GridView1_RowEditing" 
         onrowupdating="GridView1_RowUpdating" Font-Bold="true" PageSize="20">        
     <Columns>     
        <asp:BoundField DataField="Tid" HeaderText="SNO" HeaderStyle-BackColor="LightCoral"  HeaderStyle-Width="1px" HeaderStyle-Font-Size="Small"  />
        <asp:BoundField DataField="Bill_frmdate" HeaderText="FROM" HeaderStyle-BackColor="LightCoral"  HeaderStyle-Width="1px" HeaderStyle-Font-Size="Small" />
        <asp:BoundField DataField="Bill_todate" HeaderText="TO" HeaderStyle-BackColor="LightCoral"  HeaderStyle-Width="1px" HeaderStyle-Font-Size="Small" />
         <asp:BoundField DataField="PaymentFlag" HeaderText="PayFlag" HeaderStyle-BackColor="LightCoral"  HeaderStyle-Width="1px" HeaderStyle-Font-Size="Small" />    
        <asp:BoundField DataField="Descriptions" HeaderText="Descriptions" HeaderStyle-BackColor="LightCoral"  HeaderStyle-Width="2px" HeaderStyle-Font-Size="Small" />   
        <asp:BoundField DataField="UpdateStatus" HeaderText="Update" HeaderStyle-BackColor="LightCoral"  HeaderStyle-Width="1px" HeaderStyle-Font-Size="Small" />
        <asp:BoundField DataField="ViewStatus" HeaderText="View" HeaderStyle-BackColor="LightCoral"  HeaderStyle-Width="1px" HeaderStyle-Font-Size="Small" />
        <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-BackColor="LightCoral"  HeaderStyle-Width="1px" HeaderStyle-Font-Size="Small" />
       
        <asp:CommandField ShowEditButton="true" ButtonType="Image"  EditImageUrl="~/Image/edit-icon.png" UpdateImageUrl="~/Image/Editok.jpg" HeaderStyle-BackColor="LightCoral"
            CancelImageUrl="~/Image/Delete.jpg" HeaderText="Edit" HeaderStyle-Width="1px" HeaderStyle-Font-Size="Small"/>
        <asp:CommandField ShowDeleteButton="true" ButtonType="Image" HeaderStyle-Width="1px" HeaderStyle-Font-Size="Small" HeaderStyle-BackColor="LightCoral" HeaderText="Delete" DeleteImageUrl="~/Image/Delete.jpg" />
        </Columns>
        
    </asp:GridView>  
      
        </div>   
        </div> 
        <br />
        <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
</asp:Content>


