<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation ="false"   CodeFile="BankPaymentApproval.aspx.cs" Inherits="BankPaymentApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link type="text/css" href="App_Themes/StyleSheet.css" rel="Stylesheet" />

    <%--     <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" />--%>



    <style type="text/css">
        
   

        .style8
        {
            width: 100%;
        }
        
        
        
        
        
         .blink
 
        {
 
            -webkit-animation-name: blink;
 
            -webkit-animation-iteration-count: infinite;
 
            -webkit-animation-timing-function: cubic-bezier(1.0,0,0,1.0);
 
            -webkit-animation-duration: 1s;
            color: #FFFFFF;
            font-weight: 700;
            background-color: #FF0000;
            font-family: Tahoma;
            font-size: small;
        }
        
  .button {
  padding: 15px 25px;
  font-size: 24px;
  text-align: center;
  cursor: pointer;
  outline: none;
  color: #fff;
  background-color: #4CAF50;
  border: none;
  border-radius: 15px;
  box-shadow: 0 9px #999;
}

.button:hover {background-color: #3e8e41}

.button:active {
  background-color: #3e8e41;
  box-shadow: 0 5px #666;
  transform: translateY(4px);
}
        
        
        
        
        
        
        
    </style>

    

    <script type="text/javascript">
        function confirmationupdate() {
            if (confirm('Are you sure you want tos Remark update ?')) {
                return true;
            } else {
                return false;
            }
        }

        function confirmationmupdate() {
            if (confirm('Are you sure you want tos Master update ?')) {
                return true;
            } else {
                return false;
            }
        }
   </script>


 <%--     <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" />--%>




         <link type="text/css" href="App_Themes/StyleSheet.css" rel="stylesheet" />

      <style type="text/css">
          .form-style-9{
    max-width: 550px;
    background: #FAFAFA;
    padding: 20px;
    margin:20px auto;
    box-shadow: 1px 1px 25px rgba(0, 0, 0, 0.35);
    border-radius: 10px;
 
         height:440px;
         width: 512px;
     }
.form-style-9 ul{
    padding:0;
    margin:0;
    list-style:none;
}
.form-style-9 ul li{
    display: block;
    margin-bottom: 10px;
    min-height: 35px;
         height: 37px;
         width: 81px;
         margin-right: 0px;
            text-align: left;
        }
.form-style-9 ul li  .field-style{
    box-sizing: border-box;
    -webkit-box-sizing: border-box;
    -moz-box-sizing: border-box;
    padding: 8px;
    outline: none;
    border: 1px solid #B0CFE0;
    -webkit-transition: all 0.30s ease-in-out;
    -moz-transition: all 0.30s ease-in-out;
    -ms-transition: all 0.30s ease-in-out;
    -o-transition: all 0.30s ease-in-out;

}.form-style-9 ul li  .field-style:focus{
    box-shadow: 0 0 5px #B0CFE0;
    border:1px solid #B0CFE0;
}
.form-style-9 ul li .field-split{
    width: 49%;
}
.form-style-9 ul li .field-full{
    width:70%;
}
.form-style-9 ul li input.align-left{
    float:left;
}
.form-style-9 ul li input.align-right{
    float:right;
}
.form-style-9 ul li textarea{
    width: 80%;
    height: 100px;
}
.form-style-9 ul li input[type="button"],
.form-style-9 ul li input[type="submit"] {
    -moz-box-shadow: inset 0px 1px 0px 0px #3985B1;
    -webkit-box-shadow: inset 0px 1px 0px 0px #3985B1;
    box-shadow: inset 0px 1px 0px 0px #3985B1;
    background-color: #216288;
    border: 1px solid #17445E;
    display: inline-block;
    cursor: pointer;
    color: #FFFFFF;
    padding: 8px 18px;
    text-decoration: none;
    font: 12px Arial, Helvetica, sans-serif;
            text-align: right;
        }
.form-style-9 ul li input[type="button"]:hover,
.form-style-9 ul li input[type="submit"]:hover {
    background: linear-gradient(to bottom, #2D77A2 5%, #337DA8 100%);
    background-color: #28739E;
}
</style>
<style type="text/css">
.form-style-3{
    max-width: 450px;
    font-family: "Lucida Sans Unicode", "Lucida Grande", sans-serif;
}
.form-style-3 label{
    display:block;
  <%--  margin-bottom: 10px;--%>;
        height: 34px;
        text-align: left;
    }
.form-style-3 label > span{
    float: left;
    width: 100px;
    color: DARK;
    font-weight: bold;
    font-size: 13px;
    text-shadow: 1px 1px 1px #fff;
        text-align: right;
    }
.form-style-3 fieldset{
    border-radius: 10px;
    -webkit-border-radius: 10px;
    -moz-border-radius: 10px;
    margin: 0px 0px 10px 0px;
    border: 1px solid #FFD2D2;
    padding: 20px;
    background: #FFF4F4;
    box-shadow: inset 0px 0px 15px #FFE5E5;
    -moz-box-shadow: inset 0px 0px 15px #FFE5E5;
    -webkit-box-shadow: inset 0px 0px 15px #FFE5E5;
}
.form-style-3 fieldset legend{
    color: DARK;
    border-top: 1px solid #FFD2D2;
    border-left: 1px solid #FFD2D2;
    border-right: 1px solid #FFD2D2;
    border-radius: 5px 5px 0px 0px;
    -webkit-border-radius: 5px 5px 0px 0px;
    -moz-border-radius: 5px 5px 0px 0px;
    background: #FFF4F4;
    padding: 0px 8px 3px 8px;
    box-shadow: -0px -1px 2px #F1F1F1;
    -moz-box-shadow:-0px -1px 2px #F1F1F1;
    -webkit-box-shadow:-0px -1px 2px #F1F1F1;
    font-weight: normal;
    font-size: 12px;
        width: 126px;
    }
.form-style-3 textarea{
    width:250px;
    height:100px;
}
.form-style-3 input[type=text],
.form-style-3 input[type=date],
.form-style-3 input[type=datetime],
.form-style-3 input[type=number],
.form-style-3 input[type=search],
.form-style-3 input[type=time],
.form-style-3 input[type=url],
.form-style-3 input[type=email],
.form-style-3 select, 
.form-style-3 textarea{
    border-radius: 3px;
    -webkit-border-radius: 3px;
    -moz-border-radius: 3px;
    border: 1px solid #FFC2DC;
    outline: none;
    color: DARK;
    padding: 5px 8px 5px 8px;
    box-shadow: inset 1px 1px 4px #FFD5E7;
    -moz-box-shadow: inset 1px 1px 4px #FFD5E7;
    -webkit-box-shadow: inset 1px 1px 4px #FFD5E7;
    background: #FFEFF6;
    }
.form-style-3  input[type=submit],
.form-style-3  input[type=button]{
    background: #EB3B88;
    border: 1px solid #C94A81;
    padding: 5px 15px 5px 15px;
    color: #FFCBE2;
    box-shadow: inset -1px -1px 3px #FF62A7;
    -moz-box-shadow: inset -1px -1px 3px #FF62A7;
    -webkit-box-shadow: inset -1px -1px 3px #FF62A7;
    border-radius: 3px;
    border-radius: 3px;
    -webkit-border-radius: 3px;
    -moz-border-radius: 3px;    
    font-weight: bold;
}
.required{
    color:red;
    font-weight:normal;
}
</style>

    <style type="text/css">

    .button {
    border-style: none;
        border-color: inherit;
        border-width: medium;
        background-color: #4CAF50; /* Green */
        color: white;
        padding: 10px 26px;
        text-align: right;
        text-decoration: none;
        display: inline-block;
        font-size: 16px;
        margin: 4px 2px;
        cursor: pointer;
        font-weight: 700;
    }

        </style>

        <style type="text/css">
.body
{
    margin: 0;
    padding: 0;
    font-family: Arial;
}
.modal
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;
}
.center
{
    z-index: 1000;
    margin: 300px auto;
    padding: 10px;
    width: 130px;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center img
{
    height: 128px;
    width: 128px;
}
    .style9
    {
        width: 398px;
    }
    .style10
    {
        width: 345px;
    }
    .gridcls
    {
                text-align: center;
            }
            .style11
            {
                font-weight: bold;
            }
            </style>

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    <form id="form1" >
 

    <table class="style8">
        <tr>
            <td align="center">


               <table style="height: 140px; width: 49%">

    <tr align="left">
    <td align="left">
        <div   style="height: 203px" width=100%>
            <div class="form-style-3">
                <table style="height: auto" width="100%">
                    <tr align="LEFT" valign="top">
                        <td height="20px" width="98%">
                            <fieldset height:"50"="" style="height: 126px">
                                <legend>Approval  Master</legend>
                                <center>
                                <table>
                                <tr align="center">
                                <td>
                                 <table style="width: 353px">
                                    <tr>
                                        <td style="font-size: small; text-align: right;" class="style10">
                                            Plant Name</td>
                                        <td class="style9">
                                            <asp:DropDownList ID="ddl_Plantname" runat="server" CssClass="tb10" 
                                                Font-Bold="True" Font-Size="Medium" Height="30px" Width="198px" 
                                                onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
                                                AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: small; text-align: right;" class="style10">
                                            Bill Date</td>
                                        <td class="style9">
                                           <asp:DropDownList ID="ddl_BillDate" runat="server" 
                               ViewStateMode="Enabled" 
                                                Width="205px" 
                                                onselectedindexchanged="ddl_BillDate_SelectedIndexChanged1">
                           </asp:DropDownList>
                                            
                                            </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: small; text-align: right;" class="style10">
                                            &nbsp;</td>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text="Show Details"  
                                                onclick="Button1_Click"  />
                                            
                                            </td>
                                    </tr>
                                    </table>
                                </td>
                                </tr>
                                </table>
                                </center>


                               
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    
    </td>
    
    
    </tr>
    
    </table>
                
                
                
                </td>
        </tr>
        </table>
    

      <table align=center width=100% >
      <tr align="LEFT" valign=top>
      <td colspan="4">
          <table  width=50%></table>
            
                    <tr>
                                                        <td width=12%>

<asp:Label ID="Label11" runat="server" Text="Total  Amount:" CssClass="style11"></asp:Label>

                                                        </td>
                                                         <td width=12%>

<asp:Label ID="txt_tot" runat="server" style="font-weight: 700"></asp:Label>

                                                        </td>
                                                       <td width=12%>

<asp:Label ID="Label3" runat="server" Text="Bank " CssClass="style11"></asp:Label>

                                                        </td>
                                                         <td width=12%>

<asp:Label ID="txt_bank" runat="server" style="font-weight: 700"></asp:Label>

                                                        </td>
                                                         <td width=12%>

<asp:Label ID="Label5" runat="server" Text="Cash:" CssClass="style11"></asp:Label>

<asp:Label ID="txt_cashamt" runat="server" style="font-weight: 700"></asp:Label>

                                                        </td>
                                                     <td width=12%>

<asp:Label ID="Label7" runat="server" Text="Alloteed Amount:" CssClass="style11"></asp:Label>

                                                        </td>
                                                        <td>

<asp:Label ID="txt_allote" runat="server" ></asp:Label>



                                                        </td>
                                                        <td>

<asp:Label ID="Label12" runat="server" Text="Pending:"></asp:Label>

<asp:Label ID="txt_pending" runat="server"   Width="50px" style="color: #FF0000"></asp:Label>



                                                        </td>
                                                    </tr>
            
                    <tr align="center">
                                                        <td width=50% style="text-align: CENTER" colspan="8">



                                                            <br />

                                                            <br />
<center style="font-weight: 700">
                      <asp:GridView ID="GridView1" runat="server"  CssClass="serh-grid"
                          onselectedindexchanged="GridView1_SelectedIndexChanged" 
                          AutoGenerateColumns="False">
                           <HeaderStyle ForeColor="#660066" HorizontalAlign="Right" />
                           <FooterStyle ForeColor="#660066" HorizontalAlign="Right" />
                      <Columns>
                                <asp:BoundField DataField="Plant_Name" HeaderText="Plant Name" 
                                    SortExpression="Plant_Name" />
                                <asp:BoundField DataField="UpdatedTime" HeaderText="UpdatedTime" 
                                    SortExpression="UpdatedTime" />
                                <asp:BoundField DataField="BankFileName" HeaderText="BankFileName" 
                                    SortExpression="BankFileName" />
                                <asp:BoundField DataField="NetAmount" HeaderText="NetAmount" 
                                    SortExpression="NetAmount" />
                                     <asp:TemplateField HeaderText="Total" SortExpression="Total">
        <ItemTemplate>
              <asp:LinkButton runat="server" ID="Total"  CssClass="linkNoUnderline" ForeColor="Brown" Text='<%# Eval("Total") %>'  OnClick="Total_Click"></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>
        <asp:TemplateField HeaderText="Completed" SortExpression="Total">
        <ItemTemplate>
              <asp:LinkButton runat="server" ID="Completed"  CssClass="linkNoUnderline" ForeColor="Brown" Text='<%# Eval("Completed") %>'  OnClick="Completed_Click"></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>
        <asp:TemplateField HeaderText="Pending" SortExpression="Total">
        <ItemTemplate>
              <asp:LinkButton runat="server" ID="Pending"  CssClass="linkNoUnderline" ForeColor="Brown" Text='<%# Eval("Pending") %>'  OnClick="Pending_Click"></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>

                           <%--     <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" />--%>
                               <%-- <asp:BoundField DataField="Completed" HeaderText="Completed" 
                                    SortExpression="Completed" />
                                <asp:BoundField DataField="Pending" HeaderText="Pending" 
                                    SortExpression="Pending" />--%>
                            </Columns>
                      </asp:GridView>
                      </center>
                                                            <br />
<center style="padding: inherit; display: inline">
                      <table width=100%>
                          <tr width=100%>
                              <td  width=100%>

                                  
                                  <asp:Panel ID="pa" runat="server" Width=100%>
                                      <table class="style8">
                                          <tr>
                                              <td>

                                                  <asp:Label ID="lbl_agentid" runat="server" Text=" Agent Id" CssClass="style11"></asp:Label>
                                                  <asp:TextBox ID="txt_agentid" runat="server" Enabled="False"></asp:TextBox>
                                                  </td>
                                              <td>
                                                  <asp:Label ID="lbl_ifsc" runat="server" CssClass="style11" Text="Ifsc Code"></asp:Label>
                                                  <asp:TextBox ID="txt_ifsc" runat="server" Width="150px"></asp:TextBox>
                                                  </td>
                                              <td>
                                                  <asp:Label ID="lbl_accno" runat="server" CssClass="style11" Text="Account No"></asp:Label>
                                              </td>
                                              <td>
                                                  <asp:TextBox ID="txt_acno" runat="server" Width="150px"></asp:TextBox>
                                              </td>
                                              <td>
                                                  <asp:Label ID="lbl_remark" runat="server" CssClass="style11" Text="Remarks"></asp:Label>
                                                  <asp:TextBox ID="txt_txtremarks" runat="server" Width="200px"></asp:TextBox>
                                              </td>
                                              <td>
                                                  <asp:Button ID="Remark_update" runat="server" CssClass="form93"  OnClientClick="return confirmationupdate();"
                                                      onclick="Remark_update_Click" TabIndex="6" Text="Remark Update" />
                                                  <asp:Button ID="Master_update" runat="server" CssClass="form93"  OnClientClick="return confirmationmupdate();"
                                                      onclick="Master_update_Click" TabIndex="6" Text="Master Update" />
                                              </td>
                                          </tr>
                                      </table>
                                  </asp:Panel>

                                 </td>
                          </tr>
                          </table>
                      <br />
                      <asp:GridView ID="GridView2" runat="server" CssClass="serh-grid" 
                          onselectedindexchanged="GridView2_SelectedIndexChanged" 
                          AutoGenerateColumns="False">
                       <HeaderStyle ForeColor="#660066" HorizontalAlign="Right" />
                           <Columns>
                               <asp:CommandField ButtonType="Button" ShowSelectButton="True" />

                                 <asp:BoundField DataField="Agent_Id" HeaderText="AgentId,"    SortExpression="Agent_Id," />
                                 <asp:BoundField DataField="Agent_Name" HeaderText="AgentName" SortExpression="Agent_Name" />
                                <asp:BoundField DataField="Bank_Name" HeaderText="BankName"    SortExpression="Bank_Name" />
                                <asp:BoundField DataField="Ifsccode" HeaderText="Ifsccode"     SortExpression="Ifsccode" />
                                <asp:BoundField DataField="Account_no" HeaderText="Ac/No"      SortExpression="Account_no" />    
                                <asp:BoundField DataField="NetAmount" HeaderText="NetAmount"   SortExpression="NetAmount" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks"   SortExpression="Remarks" />
                                <asp:TemplateField>   
                                <ItemTemplate>
                                <asp:CheckBox ID="chkRow" runat="server" />
                                </ItemTemplate>
                                   <HeaderTemplate>
                                 <asp:CheckBox ID="chkHeader" runat="server" />
                                   </HeaderTemplate>     
                                 </asp:TemplateField>







                          </Columns>
                           <FooterStyle ForeColor="#660066" HorizontalAlign="Right" />
                      </asp:GridView>
                      <br />
                        <asp:GridView ID="GridView3" runat="server" CssClass="serh-grid">
                       <HeaderStyle ForeColor="#660066" HorizontalAlign="Right" />
                           <FooterStyle ForeColor="#660066" HorizontalAlign="Right" />
                            <Columns>
                                                       <asp:TemplateField HeaderText="SNo.">
                                                           <ItemTemplate>
                                                               <%# Container.DataItemIndex + 1 %>
                                                           </ItemTemplate>
                                                       </asp:TemplateField>
                                                     </Columns>
                      </asp:GridView>

                      </center>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right" colspan="6">

                        <asp:Button ID="btn_update" runat="server"   CssClass="form93"   
              onclick="btn_update_Click" TabIndex="6" Text="Update Details" />
      
                        <asp:Button ID="btn_excelexport" runat="server"   CssClass="form93"   
              onclick="btn_excelexport_Click" TabIndex="6" Text="Excel Export" />
      
                                                        </td>
                                                    </tr>
                                                    </table>
                                            </td>
          
      </tr>
              <tr>
                  <td>
                                     
                                </td>
              </tr>
              </table>
      
                    </td>
      
      </tr>
      <tr align="center">
      <td>
    
          <br />
          <br />
    
                    </td>
      
      </tr>
      <tr align="right">
      <td>
    
                    </td>
      
      </tr>
      </table>
    
    
    <br />
    

 
 


  
    </form>
               
            
            
            
            

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>