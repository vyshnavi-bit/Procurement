<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StockDistibuteToPlant.aspx.cs" Inherits="StockDistibuteToPlant" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="Stylesheet" />

    <style type="text/css">

    .style1
{
    width:600px;
    text-align:center;
   
    
    
}
        .style2
        {
            font-family: Andalus;
        }
        .style6
        {
            text-align: left;
        }
        .style8
        {
            height: 30px;
        }
        .style9
        {
            text-align: left;
            height: 30px;
        }
        .style10
        {
            height: 42px;
        }
        .style11
        {
            text-align: left;
            height: 42px;
        }
        .style12
        {
            height: 22px;
        }
        .style13
        {
            text-align: left;
            height: 22px;
        }
    </style>

  



    <script type="text/javascript">
        function confirmation() {
            if (confirm('Are you sure you want to Save ?')) {
                return true;
            } else {
                return false;
            }
        }
   </script>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

       
  <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height:1%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
              
  
   

    
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td  align ="center" width="100%">
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;STOCK&nbsp; ALLOTMENT&nbsp; TO PLANT</p>
            </td>
        </tr>
        </table>
   <center>
   <div class="legendvichle">
                <fieldset class="style1">
              </legend>

        <table bgcolor="White">
        
        <tr align="right">
        
                   <td width="20%" class="style8">

        <asp:Label ID="lbl_TruckId12" runat="server" Text="Plant Code" CssClass="style2"></asp:Label>

         </td>
          <td width="25%" class="style9"> 

                <asp:DropDownList ID="ddl_PlantName" runat="server" Height="30px" 
                    Width="175px"  Font-Size="12px" Font-Bold="true"
                    onselectedindexchanged="ddl_PlantName_SelectedIndexChanged" 
                    AutoPostBack="True">
                </asp:DropDownList>
                   </td>
                   <td class="style9" width="25%">
                       </td>
        </tr>
        
        <tr align="right" >
        
                   <td width="20%" class="style10">

        <asp:Label ID="lbl_TruckId" runat="server" Text="Meterial Type" CssClass="style2"></asp:Label>

         </td>
          <td width="25%%" class="style11"> 

              <asp:DropDownList ID="ddl_materialName" runat="server"  Font-Bold="true"
                Font-Size="12px" Height="30px" Width="175px" 
                  AutoPostBack="True" 
                  onselectedindexchanged="ddl_materialName_SelectedIndexChanged">
              </asp:DropDownList>

          </td>
                   <td class="style11" width="25%%">
                       </td>
        </tr>
        <tr align="right">
                   <td width="20%" class="style12">

                       <asp:Label ID="lbl_TruckId1" runat="server" CssClass="style2" 
                           Text="Meterial Name"></asp:Label>
   
            
            </td>
           
           <td width="25%" class="style13"> 

               <asp:DropDownList ID="ddl_materialName1" runat="server" Font-Bold="true"
                    Font-Size="12px" Height="30px" Width="175px" 
                   AutoPostBack="True" 
                   onselectedindexchanged="ddl_materialName1_SelectedIndexChanged">
               </asp:DropDownList>

           </td>
        
                   <td class="style13" width="25%">
                       <asp:TextBox ID="availtxt_qty" runat="server" class="input" CssClass="tb10" 
                           Font-Bold="True" Font-Size="Medium" Height="20px" ID:NAME="" Width="49px" 
                           Enabled="False"></asp:TextBox>
                       <asp:TextBox ID="availtxt_rate" runat="server" class="input" CssClass="tb10" 
                           Font-Bold="True" Font-Size="Medium" Height="20px" ID:NAME="" Width="52px" 
                           Visible="False"></asp:TextBox>
                   </td>
        
        </tr>

         <tr align="right">
                   <td width="20%">
                       <asp:Label ID="lbl_TruckId2" runat="server" CssClass="style2" Text="Qty"></asp:Label>
  
            
            </td>
           
           <td width="25%" class="style6"> 

             <asp:TextBox ID="txt_qty" runat="server" class="input" CssClass="tb10" 
                   Font-Bold="True" Font-Size="Medium" Height="25px" ID:NAME="" Width="150px">0</asp:TextBox>
   

           </td>
        
                   <td class="style6" width="25%">
                       &nbsp;</td>
        
        </tr>

        <tr>
                   <td width="25%">

          
                        <asp:DropDownList ID="ddl_Plantcode" runat="server" CssClass="style6" 
                            Font-Size="X-Small" Height="20px" Visible="False" Width="70px">
                        </asp:DropDownList>

       
        
    
  
            
            </td>
           
           <td width="25%" class="style6"> 
         

          

               <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" BackColor="#006699" ForeColor="White" Font-Size="12px" Font-Bold="true"
                   OnClientClick="return validate();" Text="Submit" style="height: 26px" />
            
            <asp:Button ID="Button2" runat="server" Text="Edit"  Height="25px" BackColor="#006699" ForeColor="White" Font-Size="12px" Font-Bold="true"
                   onclick="Button2_Click" TabIndex="8" />
   
       
        
    
  
            
           </td>
        
                   <td class="style6" width="25%">
                       &nbsp;</td>
        
        </tr>
      
        </table>
        
         
        </fieldset>
       
           <center>    <asp:Label ID="lblmsg" runat="server" Text="Label"></asp:Label> 
               <br />
              
                </center> 
       
        </div>
        </center>

<div align="center">
           
                <CENTER>
                   
                       <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                       AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" 
                       BorderStyle="None" BorderWidth="1px" CausesValidation="false" 
                       CellPadding="3" Font-Size="12px" 
                       PageSize="15" onpageindexchanging="GridView1_PageIndexChanging" 
                       onrowcancelingedit="GridView1_RowCancelingEdit" 
                        onrowediting="GridView1_RowEditing" onrowupdating="GridView1_RowUpdating">
                       <FooterStyle BackColor="White" ForeColor="#000066" />
                       <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                       <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                       <RowStyle ForeColor="#000066" />
                       <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                       <SortedAscendingCellStyle BackColor="#F1F1F1" />
                       <SortedAscendingHeaderStyle BackColor="#007DBB" />
                       <SortedDescendingCellStyle BackColor="#CAC9C9" />
                       <SortedDescendingHeaderStyle BackColor="#00547E" />
                       <Columns>
                           <asp:TemplateField HeaderText="SNo">
                               <ItemTemplate>
                                   <%#Container.DataItemIndex + 1 %>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:BoundField DataField="GHeader" HeaderText="GHeader" 
                               SortExpression="GHeader" />
                           <asp:BoundField DataField="GSHeader" HeaderText="GSHeader" 
                               SortExpression="GSHeader" />
                           <asp:BoundField DataField="qty" HeaderText="qty" SortExpression="qty" />
                           <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" 
                               SortExpression="AddedDate" />
                           <asp:CommandField ShowEditButton="True" />
                       </Columns>
                   </asp:GridView>
               
</center>

                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    </left>
                </ContentTemplate>
            </asp:UpdatePanel>
      
    
    <br />
    

</div>
<uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
</asp:Content>
