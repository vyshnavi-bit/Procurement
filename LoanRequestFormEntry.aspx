<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LoanRequestFormEntry.aspx.cs" Inherits="LoanRequestFormEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
    .button {
    border-style: none;
        border-color: inherit;
        border-width: medium;
        background-color: #4CAF50; /* Green */
        color: white;
        padding: 10px 26px;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        font-size: 16px;
        margin: 4px 2px;
        cursor: pointer;
        font-weight: 700;
    }

.button2 {background-color: #008CBA;} /* Blue */
.button3 {background-color: #f44336;} /* Red */
.button4 {background-color: #e7e7e7; color: black;} /* Gray */
.button5 {background-color: #555555;} /* Black */
.text1 {
    border: 2px solid rgb(173, 204, 204);
    height: 31px;
    width: 223px;
    box-shadow: 0 0 27px rgb(204, 204, 204) inset;
    transition: 500ms all ease;
    padding: 3px 3px 3px 3px;
        text-align: left;
    }


    </style>





    <style>
        /* Style The Dropdown Button */
.dropbtn {
        border-style: none;
            border-color: inherit;
            border-width: medium;
            background-color: #4CAF50;
            color: white;
            padding: 16px;
    font-size: xx-small;
            cursor: pointer;
}

/* The container <div> - needed to position the dropdown content */
.dropdown {
    position: relative;
    display: inline-block;
            top: 0px;
            left: 0px;
        }

/* Dropdown Content (Hidden by Default) */
.dropdown-content {
    display: none;
    position: absolute;
    background-color: #f9f9f9;
    min-width: 160px;
    box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
}

/* Links inside the dropdown */
.dropdown-content a {
    color: black;
    padding: 12px 16px;
    text-decoration: none;
    display: block;
}

/* Change color of dropdown links on hover */
.dropdown-content a:hover {background-color: #f1f1f1}

/* Show the dropdown menu on hover */
.dropdown:hover .dropdown-content {
    display: block;
}

/* Change the background color of the dropdown button when the dropdown content is shown */
.dropdown:hover .dropbtn {
    background-color: #3e8e41;
}
</style>

  <style>
      .text1 {
    border: 2px solid rgb(173, 204, 204);
    height: 31px;
    width: 400px;
    box-shadow: 0 0 27px rgb(204, 204, 204) inset;
    transition: 500ms all ease;
    padding: 3px 3px 3px 3px;
    }



.text2 {
    border: 2px solid rgb(173, 204, 204);
    height: 31px;
    width: 100%;
    box-shadow: 0 0 27px rgb(204, 204, 204) inset;
    transition: 500ms all ease;
    padding: 3px 3px 3px 3px;
        text-align: left;
    }


      .table-hover
      {
          text-align: right;
      }
      .style1
    {
        text-align: center;
    }
      </style>



        <script type = "text/javascript">
            function PrintPanel() {
                var panel = document.getElementById("<%=pnlContents.ClientID %>");
                var printWindow = window.open('', '', 'height=400,width=800');
                //       printWindow.document.write('<html><head><title>DIV Contents</title>');
                printWindow.document.write('</head><body >');
                printWindow.document.write(panel.innerHTML);
                printWindow.document.write('</body></html>');
                printWindow.document.close();
                setTimeout(function () {
                    printWindow.print();
                }, 100);
                return false;
            }
    </script>
      <script language="javascript" type="text/javascript">


          //       function isNumber(evt)
          //       
          //        {
          //           var iKeyCode = (evt.which) ? evt.which : evt.keyCode
          //           if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
          //               return false;

          //           return true;

          //       }


          function isNumber(evt, element) {

              var charCode = (evt.which) ? evt.which : event.keyCode

              if (
            (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
            (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
            (charCode < 48 || charCode > 57))
                  return false;

              return true;
          }



          function confirmationSave()
           {
               if (confirm('are you  want to Save ?'))
              
              {
                  return true;
              }
              else {
                  return false;
              }
          }  

</script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">

    </asp:ToolkitScriptManager>
  
    <table  align="center" width="100%" style="border: thin ridge #008080">
        <tr align="center">
            <td align="CENTER" style="text-align: center">

    
                                                  <asp:DropDownList ID="ddl_AgentId" 
                    runat="server" 
                                                      Height="16px" 
                                                      Visible="False" Width="35px">
                                                  </asp:DropDownList>
                
    
                            <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                                Height="16px" Visible="false" Width="29px">
                            </asp:DropDownList>

    
                <asp:Label ID="Label4" runat="server" Font-Size="Medium" Text="Plant Name"></asp:Label>

    
                <asp:DropDownList ID="ddl_Plantname" runat="server" 
                     Font-Bold="True" Font-Size="Medium" Height="30px" 
                    onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="230px" 
                    CssClass="tb10" AutoPostBack="True">
                </asp:DropDownList>
                
    
                <asp:Label ID="Label5" runat="server" Font-Size="Medium" Text="Agent Code"></asp:Label>

    
      <asp:DropDownList ID="ddl_AgentName" runat="server" 
                        Height="30px" CssClass="tb10" Width="230px" AutoPostBack="True" 
                                                      onselectedindexchanged="ddl_AgentName_SelectedIndexChanged" TabIndex="1">
      </asp:DropDownList>



  
                            Date<asp:TextBox ID="txt_FromDate" runat="server" class="text1" Height="25px" 
                                Width="125px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                                TargetControlID="txt_FromDate">
                            </asp:CalendarExtender>



  
            </td>
        </tr>
        </table>
         
    <center>
         
           <asp:Panel id="pnlContents"  align="center" runat = "server"  Width="625px">
                 
               <table width=70% align="center" class="text2">
                   </tr>
                   <tr align="right">
                        <td valign="top" style="width: 20%">
                            Closing Balance</td>
                        <td style="width:15%" align="left" valign="top">
                            <asp:TextBox ID="txt_closing" runat="server" class="text1" CssClass="text1" 
                                Enabled="False" Height="20px" onkeypress="javascript:return isNumber(event)" 
                                TabIndex="56" Width="125px"></asp:TextBox>
                        </td>
                        <td style="width: 25%">
                            Total Loans</td>
                        <td style="width: 12%" valign="top" WIDTH="50%">
                            <asp:TextBox ID="txt_totloan" runat="server" class="text1" CssClass="text1" 
                                Height="20px" onkeypress="javascript:return isNumber(event)" Width="125px" 
                                Enabled="False"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txt_totloan" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </td>
                   </tr>
                   <tr align="right">
                       <td style="width: 25%" valign="top">
                           Completed Loans</td>
                       <td style="width:15%" align="left" valign="top">
                           <asp:TextBox ID="txt_CompleteLoan" runat="server" class="text1" 
                               CssClass="text1" Height="20px" onkeypress="javascript:return isNumber(event)" 
                               Width="125px" Enabled="False"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                               ControlToValidate="txt_CompleteLoan" ErrorMessage="*"></asp:RequiredFieldValidator>
                       </td>
                       <td style="width: 15%" valign="top" class="table-hover">
                           Pending Loans</td>
                       <td style="width: 12%" valign="top" WIDTH="50%">
                           <asp:TextBox ID="txt_pendingloan" runat="server" class="text1" 
                               CssClass="text1" Height="20px" onkeypress="javascript:return isNumber(event)" 
                               Width="125px" Enabled="False"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                               ControlToValidate="txt_pendingloan" ErrorMessage="*"></asp:RequiredFieldValidator>
                       </td>
                   </tr>
                   <tr align="right">
                       <td style="width: 25%" valign="top">
                           Interest Percent</td>
                       <td align="left" style="width:15%" valign="top">
                           <asp:TextBox ID="txt_interpercent" runat="server" class="text1" 
                               CssClass="text1" Height="20px" onkeypress="javascript:return isNumber(event)" 
                               Width="125px">1</asp:TextBox>
                       </td>
                       <td class="table-hover" style="width: 15%" valign="top">
                           Requsting&nbsp; Amount</td>
                       <td style="width: 12%" valign="top" WIDTH="50%">
                           <asp:TextBox ID="txt_requerst" runat="server" class="text1" CssClass="text1" 
                               Height="20px" onkeypress="javascript:return isNumber(event)" Width="125px" 
                               TabIndex="3"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                               ControlToValidate="txt_requerst" ErrorMessage="*"></asp:RequiredFieldValidator>
                       </td>
                   </tr>
                   <tr align="right">
                       <td style="width: 25%" valign="top">
                           No Of EMI</td>
                       <td style="width:15%" align="left" valign="top">
                           <asp:TextBox ID="txt_emi" runat="server" AutoPostBack="True" class="text1" 
                               CssClass="text1" Height="20px" onkeypress="javascript:return isNumber(event)" 
                               ontextchanged="txt_emi_TextChanged" Width="125px" TabIndex="4"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                               ControlToValidate="txt_emi" ErrorMessage="*"></asp:RequiredFieldValidator>
                       </td>
                       <td style="width: 15%" valign="top" class="table-hover">
                           Total&nbsp; Amount</td>
                       <td style="width: 12%; text-align: left;" valign="top" WIDTH="50%">
                           <asp:TextBox ID="txt_totamount" runat="server" class="text1" CssClass="text1" 
                               Enabled="False" Height="20px" onkeypress="javascript:return isNumber(event)" 
                               Width="125px" TabIndex="45"></asp:TextBox>
                       </td>
                   </tr>
                   <tr align="right">
                       <td style="width: 25%" valign="top">
                           Tot Interest Amount</td>
                       <td align="left" style="width:15%" valign="top">
                           <asp:TextBox ID="txt_totintamount" runat="server" class="text1" 
                               CssClass="text1" Enabled="False" Height="20px" 
                               onkeypress="javascript:return isNumber(event)" Width="125px"></asp:TextBox>
                       </td>
                       <td class="table-hover" style="width: 15%" valign="top">
                           RecoverAmounty
                       </td>
                       <td style="width: 12%; text-align: left;" valign="top" WIDTH="50%">
                           <asp:TextBox ID="txt_installAmount" runat="server" class="text1" 
                               CssClass="text1" Enabled="False" Height="20px" 
                               onkeypress="javascript:return isNumber(event)" Width="125px" TabIndex="56"></asp:TextBox>
                       </td>
                   </tr>
                   </tr>
                   <tr align="right">
                       <td class="style1" colspan="4" valign="top">
                           Description<asp:TextBox ID="txt_description" runat="server" class="text1" 
                               Height="50px" TabIndex="5" TextMode="MultiLine" Width="175px"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                               ControlToValidate="txt_description" ErrorMessage="*"></asp:RequiredFieldValidator>
                       </td>
                   </tr>
               </table>
              </asp:Panel>
               <asp:Button ID="Button5" runat="server" CssClass="button" Font-Bold="True"   Font-Size="10px"  xmlns:asp="#unknown"   OnClientClick="return confirmationSave();" onclick="Button5_Click" Text="Save" TabIndex="6" />
     


               <br />
   
    </center>
    <table ALIGN="center" width="100%">
    <tr align="center" class="text2">
    <td align="center">
    
                           <asp:GridView ID="GridView2" runat="server" 
                               CssClass="table table-striped table-bordered table-hover" Font-Size="Medium" 
                PageSize="20" onpageindexchanging="GridView2_PageIndexChanging" 
                               onrowcancelingedit="GridView2_RowCancelingEdit">
                               <HeaderStyle ForeColor="#660066" />
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
    </center>
            <br />
            <br />                
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

