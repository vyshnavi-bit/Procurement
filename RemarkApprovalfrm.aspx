<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RemarkApprovalfrm.aspx.cs" Inherits="RemarkApprovalfrm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 2%;
        }
        .style2
        {
            width: 100%;
        }
        .style16
        {
            height: 44px;
            width: 894px;
        }
        .style17
        {
            width: 894px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
    <table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr>
            <td width="100%">
                <p class="subheading" style="line-height: 150%">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;RRMARKS&nbsp; UPDATES</p>
            </td>
        </tr>
        <tr>
            <td width="100%" height="3px">
            </td>
        </tr>
        <tr>
            <td width="100%" class="line" height="1px">
                &nbsp;</td>
        </tr>
        <tr>
            <td width="100%" height="7">
                
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text=" Date" 
                    ForeColor="#3399FF" style="font-size: small"></asp:Label>
                                        &nbsp;<asp:TextBox ID="txt_FromDate1" runat="server"  ></asp:TextBox>

                        <asp:CalendarExtender ID="txt_FromDate1_CalendarExtender" 
                    runat="server" TargetControlID="txt_FromDate1"
                                PopupButtonID="txt_FromDate1" Format="MM/dd/yyyy" 
                    PopupPosition="TopRight">
                        </asp:CalendarExtender>
                              
                           <asp:Label ID="Label4" runat="server" Text="Session" 
                    ForeColor="#0099FF" style="font-size: small"></asp:Label>
                <asp:DropDownList ID="DropDownList1" 
                    runat="server" Height="16px" 
                    onselectedindexchanged="DropDownList1_SelectedIndexChanged" Width="82px">
                    <asp:ListItem>-----Select------</asp:ListItem>
                    <asp:ListItem>AM</asp:ListItem>
                    <asp:ListItem>PM</asp:ListItem>
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label5" runat="server" style="font-size: small; color: #3399FF" 
                    Text="Plant Code"></asp:Label>
&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="ddl_plantcode" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="ddl_plantcode_SelectedIndexChanged">
                </asp:DropDownList>
                
                <br />
                
            </td>
        </tr>
        </table>
        
        
<div style="width:100%;">
                  <table   width="100%">
                  <tr>
                  <td width="18%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                      &nbsp;<asp:RadioButton ID="RadioButton1" runat="server" ForeColor="#0066FF" 
                    Text="Session Wise" style="font-size: small" Visible="False" />
                      <asp:RadioButton ID="RadioButton3" runat="server" ForeColor="#0066FF" 
                    Text="Day Wise" style="font-size: small" Visible="False" />
                
                      </td>
                   <td width="10%" class="fontt">
										<asp:Label ID="Label1" runat="server" Text="From Date" Visible="False"></asp:Label>
                                        :</td>
										
                    <td width="15%" class="fontt" align="right">
                                <asp:TextBox ID="txt_FromDate" runat="server" Visible="False"  ></asp:TextBox>

                            </td>
                                <td class="style1">
                                 
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="MM/dd/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>
                              
                               </td>
                       <td width="9%" class="fontt">
       
                           <asp:Label ID="Label2" runat="server" Text="To Date" Visible="False"></asp:Label>
                           :</td>
                             
                               <td width="15%" class="fontt" align="right">
                              <asp:TextBox ID="txt_ToDate" runat="server" Visible="False"  ></asp:TextBox></td>
                               <td width="12%">
                                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="MM/dd/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
                         
                <asp:RadioButton ID="RadioButton2" runat="server" ForeColor="#0066FF" 
                    Text="Date Wise" style="font-size: small" Visible="False" 
                                       oncheckedchanged="RadioButton2_CheckedChanged" />
                
                          </td> 
                          <td width="20%"></td>    
                       </tr>
                   </table>
   </div>
   

    <table class="style2">
        <tr>
            <td class="style16">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label6" runat="server" Text="No Of Remarks" 
                    Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label7" runat="server" style="color: #FF0000" Text="Label" 
                    Visible="False"></asp:Label>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1" Width="873px" 
                    PageSize="40">
        <Columns>
            <asp:BoundField DataField="Tid" HeaderText="Sno" SortExpression="Tid">
            <ItemStyle Width="15px" />
            </asp:BoundField>
            <asp:BoundField DataField="Agent_id" HeaderText="Agent_id" 
                SortExpression="Agent_id">
            <HeaderStyle Width="15px" />
            </asp:BoundField>

           
          <asp:BoundField DataField="Prdate" HeaderText="Prdate" ItemStyle-Width="12px"
                SortExpression="Prdate" ReadOnly="True" >
            <HeaderStyle Width="12px" />

<ItemStyle Width="12px"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="Fat" HeaderText="Fat"  SortExpression="Fat" >
            <HeaderStyle Width="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="Snf" HeaderText="Snf" SortExpression="Snf" >
            <HeaderStyle Width="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="Milk_kg" HeaderText="Milk_kg" SortExpression="Milk_kg" >
            <HeaderStyle Width="12px" />
            </asp:BoundField>
            <asp:BoundField DataField="Sessions" HeaderText="Sessions" SortExpression="Sessions" >
            <HeaderStyle Width="10px" />
            </asp:BoundField>




            <asp:TemplateField HeaderText="approval" >

                <ItemTemplate>
                    <asp:CheckBox ID="approval" runat="server" Text='<%# Eval("approval") %>' />
                </ItemTemplate>
                <HeaderStyle Width="15px" />
            </asp:TemplateField>

           
        </Columns>
      
       
    </asp:GridView>
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TextBox2" runat="server" 
                    Visible="False" Width="16px"></asp:TextBox>
                &nbsp;&nbsp;<asp:TextBox ID="TextBox3" runat="server" Visible="False" 
                    Width="16px"></asp:TextBox>
                &nbsp;<asp:TextBox ID="TextBox4" runat="server" Visible="False" Width="16px"></asp:TextBox>
                &nbsp;<asp:TextBox ID="TextBox5" runat="server" Visible="False" Width="16px"></asp:TextBox>
                &nbsp;<asp:TextBox ID="TextBox6" runat="server" Visible="False" Width="16px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;     
                                        &nbsp;&nbsp;
                                                              
                            &nbsp;&nbsp;&nbsp;
       
                           &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;  <asp:Button ID="Button1" runat="server" Text="Button" 
                      onclick="Button1_Click" /> 
            </td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </tr>
        <tr>
            <td class="style17">
              <center>  </center>
            </td>
        </tr>
    </table>
   

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>" 
        
        
        
        
        SelectCommand="SELECT [Tid], [Agent_id],FORMAT(Prdate,'dd-MM-yyyy') as Prdate, [Fat], [Snf], [Milk_kg], [Sessions],[approval] FROM [Procurementimport] WHERE (([Sessions] = @Sessions) AND ([Prdate] = @Prdate) AND ([Remarkstatus] = @Remarkstatus)  AND  ([plant_code]=@plant_code))  order by agent_id asc">
        <SelectParameters>
            <asp:ControlParameter ControlID="DropDownList1" 
                DefaultValue="AM" Name="Sessions" PropertyName="SelectedValue" 
                Type="String" />
            <asp:ControlParameter ControlID="txt_FromDate1" DbType="Date" 
                DefaultValue="2-15-2014" Name="Prdate" PropertyName="Text" />
            <asp:Parameter DefaultValue="1" Name="Remarkstatus" Type="Int32" />
            <asp:ControlParameter ControlID="ddl_plantcode" DefaultValue="154" 
                Name="plant_code" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
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
   <br />
<center>
<div>
                     <br />
    

</div>
</center>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

