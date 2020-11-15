<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Frm_DBbackup.aspx.cs" Inherits="Frm_DBbackup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <style type="text/css">
    body
    {
        font-family: Arial;
        font-size: 8pt;
    }
    .Pager span
    {
        text-align: center;
        color:  #3AC0F2;
        display: inline-block;
        width: 20px;
        background-color: #3AC0F2;
        margin-right: 3px;
        line-height: 150%;
        border: 1px solid #3AC0F2;
    }
    .Pager a
    {
        text-align: center;
        display: inline-block;
        width: 20px;
        background-color: #3AC0F2;
        color: #fff;
        border: 1px solid #3AC0F2;
        margin-right: 3px;
        line-height: 150%;
        text-decoration: none;
    }
 
        #table4
        {
            height: 320px;
        }
 
        .style5
        {
            height: 38px;
        }
        .style6
        {
            width: 350px;
        }
        .style7
        {
            font-size: medium;
        }
 
        </style>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server" />

  <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height:10%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>


<asp:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"    >
            <ContentTemplate>

   
       <center style="background-color: #FFFFFF">
        <br />
        <div align="right"> </div>
    <fieldset style="background-color: #CCFFFF"class="style6">
   
   <table align="center"  width="400px"  bgcolor="White">
    <tr>
   
   <td align="right" >
     <asp:Label ID="Label6" runat="server" Text="Plant Name" Font-Size="Small" 
           CssClass="style7" style="font-family: Andalus; font-size: medium" ></asp:Label>   
   </td>
   <td align="LEFT">

  <asp:DropDownList ID="ddl_PlantName" runat="server" Width="140px" Font-Size="12px"
                     Height="24px" Font-Bold="true">
    </asp:DropDownList>

       <asp:DropDownList ID="ddl_TableName" Width="100px" runat="server" Visible="false">
       </asp:DropDownList>

   </td>
   
   </tr>






   <tr>
   
   <td align="right" class="style5">
       <asp:Label ID="Label10" runat="server" Text="From Date" Font-Size="Small" 
           CssClass="style7" style="font-family: Andalus; font-size: medium"></asp:Label>
   </td>
   <td align="left" class="style5">
     
                          <asp:TextBox ID="txt_FromDate" runat="server" TabIndex="4" Width="150px" 
                              CssClass="tb10" Font-Size="X-Small"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="BottomRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>
                          </em></strong>
   </td>
   
   </tr>



   <tr>
   
   <td align="right">
       <asp:Label ID="Label11" runat="server" Text="To Date" Font-Size="Small" 
           CssClass="style7" style="font-family: Andalus; font-size: medium"></asp:Label>
   </td>
   <td align="left">
     
                          <asp:TextBox ID="txt_ToDate" runat="server" TabIndex="4" Width="150px" 
                              CssClass="tb10" Font-Size="X-Small"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_ToDate_CalendarExtender" runat="server" 
                                Format="dd/MM/yyyy" PopupButtonID="txt_dob" PopupPosition="BottomRight" 
                                TargetControlID="txt_ToDate">
                            </asp:CalendarExtender>
                          </em></strong>

                                     <asp:Label ID="Lbl_selectedReportItem" runat="server" visible="false"></asp:Label>    
   </td>
   
   </tr>




   <tr>
   
   <td >
                             
       </td>
       <td align="left">
       <asp:RadioButtonList ID="rbtLstReportItems"   RepeatDirection="Horizontal" 
                 RepeatLayout="Table"  runat="server"  Font-Bold="false" Font-Size="14px"
               onselectedindexchanged="rbtLstReportItems_SelectedIndexChanged" >
                 <asp:ListItem Text="Procurement" Value="1"></asp:ListItem>
                  <asp:ListItem Text="ccprocurement" Value="2"></asp:ListItem>                                      
                </asp:RadioButtonList>
         
                                                  
       </td>   
   </tr>
    <tr>
   
   <td align="left">

             &nbsp;</td>
   <td ALIGN="left">   
        
         <left> 
          <asp:Button ID="btn_Generate" runat="server" BackColor="#006699" 
             ForeColor="White"  Height="26px"   BorderStyle="Double" 
                 Font-Bold="True"    Text="BackUp" onclick="btn_Generate_Click" />
             <asp:Button ID="btn_Delete" runat="server" BackColor="#006699" 
                 BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                  Text="Delete" onclick="btn_Delete_Click" />
             <asp:Button ID="btn_Import" runat="server" BackColor="#006699" 
                 BorderStyle="Double" Font-Bold="True" ForeColor="White" Height="26px" 
                  Text="Import" onclick="btn_Import_Click" />
         </left>

   </td>
   
   </tr>




   </table>
  
   
   
    </fieldset> 
       <br />
       <br />
    </center>
     <div  ><asp:Label ID="Lbl_Errormsg" runat="server" ForeColor="Red"  Font-Size="Large" ></asp:Label></div>

    </ContentTemplate>


        </asp:UpdatePanel>
       
        <br />
         <br />
          <br />
 <br />
  <br />
   <br />
    <br />
     <br />
      <br />
       <br />
        <br />
         <br />
           <br />
        <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

