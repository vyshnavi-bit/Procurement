<%@ Page Title="OnlineMilkTest|RolePermission" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RolePermissionSetting.aspx.cs" Inherits="RolePermissionSetting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 9pt;
            font-weight: normal;
            font-style: normal;
            height: 551px;
        }
        .style2
        {
            height: 551px;
        }
        .cellbord
        {
          margin-top:1px;  
         
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%" colspan="2"><br />
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;ROLE PERMISSION SETTING
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
             <td width="100%" height="7" colspan="2" align="right">
                      <asp:HyperLink ID="HyperLink1" runat="server" CssClass="fontt" 
                          NavigateUrl="~/PermissionRoleList.aspx">Role List</asp:HyperLink>
            </td>
        </tr>
        </table>
          <br />
          
      <asp:Panel ID="Panel1" runat="server" Height="100px" CssClass="panel">
        <div id="selectrolepanel">
        <fieldset class="fontt">
        <legend class="fontt">SELECT ROLE</legend>
        

        <table border="0" width="100%" id="table12" cellspacing="1">
           <tr>
                    <td width="20%">
                    </td>
                    <td width="30%">
                      <asp:Label ID="select_lbl" runat="server" Text="Select Role"></asp:Label>
                    </td>
                    <td width="20%">
                          <asp:DropDownList 
                ID="ddl_role" runat="server" AutoPostBack="True" Width="110px" 
                              onselectedindexchanged="ddl_role_SelectedIndexChanged">
                <asp:ListItem Value="1">SuperAdmin</asp:ListItem>
                <asp:ListItem Value="2">Admin</asp:ListItem>
                <asp:ListItem Value="3">User</asp:ListItem>
                <asp:ListItem Value="4">EndUser</asp:ListItem>
            </asp:DropDownList></td>
                    
                </tr>
                
                <tr>
           <td width="20%"></td>
            <td width="20%"></td>
            <td width="16%" align="left">
           
                      <asp:Button ID="Button1" runat="server" Text="SAVE" onclick="Button1_Click" Height="25px" BackColor="#6F696F" ForeColor="White" OnClientClick="return confirm('Are You Sure Set The Permission?');"/>
</td>
           </tr>
              
              
        
                
            </table>
        </fieldset>
        </div>
          </asp:Panel>
        <%--<div id="selectrolepanel">
        <fieldset class="fontt">
        <legend>SelectRole</legend>
        

        
              
                

<div class="selectnew">
    <div class="lab">
    <asp:Label ID="select_lbl" runat="server" Text="Select Role"></asp:Label>
    </div>
    <div class="drop">
     <asp:DropDownList 
                ID="ddl_role" runat="server" AutoPostBack="True" Width="110px">
                <asp:ListItem Value="1">SuperAdmin</asp:ListItem>
                <asp:ListItem Value="2">Admin</asp:ListItem>
                <asp:ListItem Value="3">User</asp:ListItem>
                <asp:ListItem Value="4">EndUser</asp:ListItem>
            </asp:DropDownList>
            </div>
            <div class="butt">
            
            <asp:Button ID="Button1" runat="server" Text="SAVE" onclick="Button1_Click" Height="25px" BackColor="#6F696F" ForeColor="White" OnClientClick="return confirm('Are You Sure Set The Permission?');"/>
            </div>
    
</div>
<div class="clear"></div>


                
         
       
         
           
                      

              
              
        
                
        
        </fieldset>
        </div>--%>
         
          
          
          
         <div class="legendpermission">
         <fieldset class="fontt">
         <legend class="fontt">SELECT MENU</legend>
                         <table>
                         <tr>
                         <td class="style1" style=" border:solid 1px gray;">
                          <asp:Panel ID="Panel2" runat="server"  >
                     <asp:TreeView ID="TreeView1" runat="server" BackColor="Snow" CssClass="treefont" 
                                  Height="360px">
                     <Nodes>                     
                
<asp:TreeNode Text="MASTER" Value="MASTER">

<asp:TreeNode Text="CompanyMaster" Value="CompanyMaster"></asp:TreeNode>
                    
<asp:TreeNode Text="StateMaster" Value="StateMaster"></asp:TreeNode>
                    
<asp:TreeNode Text="RouteMaster" Value="RouteMaster"></asp:TreeNode>
                    
<asp:TreeNode Text="BankMaster" Value="BankMaster"></asp:TreeNode>
                    
<asp:TreeNode Text="AgentMaster" Value="AgentMaster"></asp:TreeNode>
                    
<asp:TreeNode Text="RateChart" Value="RateChart"></asp:TreeNode>
                    
<asp:TreeNode Text="PortSetting" Value="PortSetting"></asp:TreeNode>
                    
<asp:TreeNode Text="PermissionSetting" Value="PermissionSetting"></asp:TreeNode>
                    

                
</asp:TreeNode>
<asp:TreeNode Text="PROCUREMENT" Value="PROCUREMENT">
<asp:TreeNode Text="Weigher" Value="Weigher"></asp:TreeNode>
<asp:TreeNode Text="Analyzer" Value="Analyzer"></asp:TreeNode>

</asp:TreeNode>
                
<asp:TreeNode Text="TRANSACTION" Value="TRANSACTION">

 <asp:TreeNode Text="RateChartView" Value="RateChartView"></asp:TreeNode>
                    
<asp:TreeNode Text="LoanEntry" Value="LoanEntry"></asp:TreeNode>
                    
<asp:TreeNode Text="DeductionEntry" Value="DeductionEntry"></asp:TreeNode>
                                  
</asp:TreeNode>
                
<asp:TreeNode Text="REPORT" Value="REPORT">
<asp:TreeNode Text="Bill" Value="Bill"></asp:TreeNode>
                
</asp:TreeNode>
            
</Nodes>
        
</asp:TreeView>
  </asp:Panel>
</td>

<td id="checkpanel" class="style2" >
<asp:Panel ID="Panel3" runat="server" CssClass="checkpanelsize" Height="525px" Width="700px">

<asp:Table ID="Table2" runat="Server" BorderColor="White" BorderWidth="1" CellPadding="1" 
                CellSpacing="1"  CaptionAlign="Top" Height="40px" Width="500px">
                <asp:TableRow ID="TableRow1" runat="Server" BorderWidth="1" Width="500px">
                   
                  <asp:TableCell ID="TableCell2"  runat="Server"  BorderWidth="1">
         <asp:Table ID="Table1" runat="Server" BorderColor="White" BorderWidth="1" 
                CellPadding="1" 
                CellSpacing="1" Width="500px" CaptionAlign="Top" Height="40px" >
                
                
                
<asp:TableRow ID="TableRow2" runat="Server" BorderWidth="1" BackColor="#3399CC" ForeColor="white" BorderColor="Silver" Width="500px" style='text-align:center;vertical-align:middle'>
                
   <asp:TableCell ID="TableCell4"  runat="Server"  BorderWidth="2" >
   <asp:CheckBox ID="MChk_Menu1" Text="MASTERS" runat="server" AutoPostBack="True"  oncheckedchanged="CheckBox1_CheckedChanged1"   />
                    
</asp:TableCell>
                    
<asp:TableCell ID="TableCell5" runat="Server" BorderWidth="2"  >
<asp:CheckBox ID="MChk_Menu2" Text="PROCUREMENT" runat="server" AutoPostBack="True"  oncheckedchanged="CheckBox1_CheckedChanged" /> 
                    
</asp:TableCell>
                    
<asp:TableCell ID="TableCell6" runat="Server" BorderWidth="2" >
<asp:CheckBox ID="MChk_Menu3" runat="server" AutoPostBack="True" oncheckedchanged="CheckBox1_CheckedChanged2" Text="TRANSACTION"  />
                    
</asp:TableCell>
<asp:TableCell ID="TableCell7" runat="Server" BorderWidth="2" > 
<asp:CheckBox ID="MChk_Menu4" runat="server" AutoPostBack="True" OnCheckedChanged="MChk_Menu4_CheckedChanged" Text="REPORTS"  />  
                    
</asp:TableCell>
                
</asp:TableRow>




                
<asp:TableRow runat="Server" BorderWidth="1" BorderColor="Silver" BackColor="#fffafa" style='text-align:left;vertical-align:top'>
                <asp:TableCell runat="Server" > 
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" BorderWidth="1" Width="150px">
                </asp:CheckBoxList>
</asp:TableCell>
                
<asp:TableCell runat="Server" >
<asp:CheckBoxList ID="CheckBoxList2" runat="server"  BorderWidth="1" Width="150px" >
</asp:CheckBoxList>
</asp:TableCell>
                
<asp:TableCell runat="Server" >
<asp:CheckBoxList ID="CheckBoxList3" runat="server"  BorderWidth="1" Width="150px">
</asp:CheckBoxList>
</asp:TableCell>

<asp:TableCell runat="Server" >
<asp:CheckBoxList ID="CheckBoxList4" runat="server"  BorderWidth="1"  Width="150px">
</asp:CheckBoxList>
</asp:TableCell>
                
</asp:TableRow>
            
</asp:Table>     
                  
</asp:TableCell>
                </asp:TableRow>
                
         </asp:Table>
         </asp:Panel>
</td>
</tr>
</table>     
      </fieldset>
      
       </div>   
          
         <br />
         <br /> 
 
      
</asp:Content>


