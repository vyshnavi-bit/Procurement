<%@ Page Title="OnlineMilkTest|Bankmaster" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Bankmaster.aspx.cs" Inherits="Bankmaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MCN.WebControls" Namespace="MCN.WebControls" TagPrefix="mcn" %>
<%@ Register Src="MessageBoxUsc/Message.ascx" TagName="uscMsgBox" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link type="text/css" href="App_Themes/StyleSheet.css" rel="stylesheet" />
     <style type="text/css">
         .style1
         {
             color: #000000;
             font-family: Tahoma;
             font-size: small;
         }
     </style>



  <style type="text/css">
          .form-style-9{
    max-width: 550px;
    background: #FAFAFA;
    padding: 20px;
    margin:20px auto;
    box-shadow: 1px 1px 25px rgba(0, 0, 0, 0.35);
    border-radius: 10px;
 
         height:440px;
         width: 488px;
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
    width:50%;
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
            .style2
            {
                width: 99%;
            }
        </style>










</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <form id="form1" >
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
    <div class="modal">
        <div class="center">
       
            <asp:Image ID="Image1" ImageUrl="waiting.gif" AlternateText="Processing" runat="server" />
        </div>
    </div>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div align="center">


 <table align=center style="height: 449px"  >
     <tr>
         <td valign=top >


<div class="form-style-9" style="height: 343px">
<div class="form-style-3">
<table style="height: auto; width: 102%;">

<tr  valign=top ALIGN=LEFT>
<td WIDTH="98%" height="20px">

<fieldset  height:"50" style="height: auto; width: 446px;"><legend>Bank Master With Ifsc</legend>


    <table class="style2">
        <tr>
            <td class="style3">
                       &nbsp;</td>
            <td class="style3">
                    &nbsp;</td>
            <td>
                       
                        <strong><em>
                        <asp:DropDownList ID="ddl_Plantcode" runat="server" Enabled="False" 
                            Visible="False">
                        </asp:DropDownList>
                        </em></strong>
                        </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style3">
                <asp:Label ID="lbl_PlantName" runat="server" CssClass="style1" 
                    Text="Plant Name"></asp:Label>
            </td>
            <td>
                <strong><em>
                <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                    CssClass=" " onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
                    TabIndex="8" Width="205px">
                </asp:DropDownList>
                </em></strong>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <asp:TextBox ID="txt_BankId" runat="server" AutoPostBack="true" 
                    BackColor="White" Enabled="False" ReadOnly="True" Visible="False"></asp:TextBox>
            </td>
            <td class="style3">
                <asp:Label ID="lbl_bankid" runat="server" CssClass="style1" Text="Bank ID" 
                    Visible="False"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddl_BankId" runat="server" 
                    onselectedindexchanged="ddl_BankId_SelectedIndexChanged" Visible="False" 
                    Width="80px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txt_BankId" Display="dynamic" ErrorMessage="*" 
                    ValidationExpression="^[a-zA-Z,.]{1,30}$"> </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style3">
                          <asp:TextBox ID="txt_Bankname" runat="server" TabIndex="1" 
                    Visible="False"></asp:TextBox>
                        </td>
            <td class="style3">
                       <asp:Label ID="lbl_Bankname" runat="server" Text="Bank Name" 
                    CssClass="style1"></asp:Label>
                        </td>
            <td>
                        <asp:DropDownList ID="ddl_BankName" runat="server"  Width="205px" 
                            onselectedindexchanged="ddl_BankName_SelectedIndexChanged" TabIndex="8" 
                              AutoPostBack="True">
                        </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  
                            ControlToValidate="txt_Bankname" validationExpression="[a-z A-Z]*" 
                            ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style3">
                     <asp:Label ID="lbl_Branchname" runat="server" Text="Branch Name" 
                    CssClass="style1"> </asp:Label>
                        </td>
            <td>
                          <asp:TextBox ID="txt_Branchname" runat="server" TabIndex="2" 
                              AutoPostBack="True" ontextchanged="txt_Branchname_TextChanged" 
                              Width="190px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_Branchname"
                                                ErrorMessage="*" Display="dynamic"  
                              validationExpression="[a-z A-Z]*"> </asp:RequiredFieldValidator>
                        </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style3">
                    <asp:Label ID="lbl_Ifsccode" runat="server" Text="IFSC Code" 
                    CssClass="style1"></asp:Label>
           
                        </td>
            <td>
                       <asp:TextBox ID="txt_Ifsccode" runat="server" TabIndex="4" Width="190px" ></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_Ifsccode"
                                                ErrorMessage="*" Display="dynamic" 
                           ValidationExpression="^[a-zA-Z,.]{1,11}$"> </asp:RequiredFieldValidator>
                          
                        </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style3">
                    
                         
           <asp:Label ID="lbl_Branchcode" runat="server" Text="Branch Code " CssClass="style1"></asp:Label>      
         
                        </td>
            <td>
                           
                           <asp:TextBox ID="txt_Branchcode" runat="server" TabIndex="3" Width="190px"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                               ControlToValidate="txt_Branchcode" ErrorMessage="*" 
                               ValidationExpression="^[a-zA-Z,.]{1,6}$" ></asp:RequiredFieldValidator>
                        </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;</td>
            <td class="style3">
                   <asp:Label ID="lbl_Phoneno" runat="server" Text="Phone Number" 
                    CssClass="style1"></asp:Label>
           
                        </td>
            <td>
                       <asp:TextBox ID="txt_Phoneno" runat="server"  TabIndex="5" Width="190px"></asp:TextBox>
          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
            ControlToValidate="txt_Phoneno" ErrorMessage="*" 
            ValidationExpression="^[0-9,.]{1,10}$"></asp:RegularExpressionValidator>
                          
                        </td>
        </tr>
        </table>


    <table class="style1">
        
    </table>
 </fieldset>


</td>
 
</tr>
<tr align="center">
<td>
 <asp:Button ID="btn_Save" runat="server" CssClass="form93" Font-Bold="true" 
                  Font-Size="X-Small" onclick="btn_Save_Click" TabIndex="6" Text="save" 
                  xmlns:asp="#unknown" />
</td>
</tr>

</table>
</div>

 
</div>




           

            </td>
     </tr>
     </table>
          <br />

          <table width=100%>
          <tr align="center">
          <td>
             
              <asp:GridView ID="GridView1" runat="server" CssClass="gridcls" Font-Bold="True" 
                  Font-Size="14px" ForeColor="White" onrowcreated="GridView1_RowCreated" 
                  onrowdatabound="GridView1_RowDataBound" ShowFooter="True" Width="60%">
                  <EditRowStyle BackColor="#999999" />
                  <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
                  <HeaderStyle BackColor="#f4f4f4" Font-Bold="False" Font-Italic="False" 
                      Font-Names="Raavi" Font-Size="Small" ForeColor="Black" />
                  <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                  <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
                  <AlternatingRowStyle HorizontalAlign="Center" />
                  <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                  <Columns>
                      <asp:TemplateField HeaderText="SNo.">
                          <ItemTemplate>
                              <%# Container.DataItemIndex + 1 %>
                          </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
              </asp:GridView>
              </td>
          </tr>
          </table>
    
    <br />
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AMPSConnectionString %>" 
        SelectCommand="SELECT * FROM BANK_MASTER WHERE([Plant_code]=@Plant_code) AND ([Company_code]=@Company_code)
 ORDER BY Bank_ID DESC">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="Plant_code" Name="Plant_code" 
                SessionField="Plant_Code" />
            <asp:SessionParameter DefaultValue="Company_code" Name="Company_code" 
                SessionField="Company_code" />
        </SelectParameters>
    </asp:SqlDataSource>

 <uc1:uscMsgBox ID="uscMsgBox1" runat="server" />
                    </table>



    </div>
</ContentTemplate>


<%--<Triggers>
    <asp:PostBackTrigger ControlID="Button7" />
</Triggers>--%>
</asp:UpdatePanel>
  
    </form>




  




</asp:Content>


