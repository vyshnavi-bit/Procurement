<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LoanRequertFormVerifyRm.aspx.cs" Inherits="LoanRequertFormVerifyRm" %>
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
        text-align: center;
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
    text-align: left;
          margin-top: 0px;
          margin-bottom: 0px;
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
          text-align: left;
      }
      </style>



 
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

</script>
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">

    </asp:ToolkitScriptManager>
  
   
    <table ALIGN="center" width="100%">
    <tr align="center" class="text2">
    <td align="center">

    <br />
                           <asp:GridView ID="GridView2" runat="server" 
                               
            CssClass="table table-striped table-bordered table-hover" Font-Size="Small" 
                PageSize="20" AutoGenerateColumns="False" 
                               onselectedindexchanged="GridView2_SelectedIndexChanged">
                               <HeaderStyle ForeColor="#660066" />
                               <Columns>
                                   <asp:TemplateField HeaderText="SNo.">
                                       <ItemTemplate>
                                           <%# Container.DataItemIndex + 1 %>
                                       </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:BoundField DataField="RefId" HeaderText="RefId" SortExpression="RefId" />
                                   <asp:BoundField DataField="plant_code" HeaderText="pcode" 
                                       SortExpression="plant_code" />
                                   <asp:BoundField DataField="plant_name" HeaderText="PlantName" 
                                       SortExpression="plant_name"></asp:BoundField>
                                   <asp:BoundField DataField="Agent_id" HeaderText="Agentid" 
                                       SortExpression="Agent_id"></asp:BoundField>
                                        <asp:BoundField DataField="Agent_Name" HeaderText="AgentName" 
                                       SortExpression="Agent_Name"></asp:BoundField>
                                   <asp:BoundField DataField="RequestingDate" HeaderText=" RequestDate" 
                                       SortExpression="RequestingDate"></asp:BoundField>
                                   <asp:BoundField DataField="TotLoan" HeaderText="TotLoan" 
                                       SortExpression="TotLoan"></asp:BoundField>
                                   <asp:BoundField DataField="CompletedLoan" HeaderText="CompLoan" 
                                       SortExpression="CompletedLoan"></asp:BoundField>
                                   <asp:BoundField DataField="PendingLoan" HeaderText="PendLoan" 
                                       SortExpression="PendingLoan"></asp:BoundField>
                                   <asp:BoundField DataField="LoanAmount" HeaderText="LoanAmount" 
                                       SortExpression="LoanAmount"></asp:BoundField>
                                   <asp:BoundField DataField="Emi" HeaderText="Emi" SortExpression="Emi">
                                   </asp:BoundField>
                                   <asp:BoundField DataField="Rateperinterest" HeaderText="RatePerinst" 
                                       SortExpression="Rateperinterest" />
                                   <asp:BoundField DataField="TotalinterestAmount" HeaderText="TotInstAmt" 
                                       SortExpression="TotalinterestAmount" />
                                   <asp:BoundField DataField="TotalAmount" HeaderText="TotAmt" 
                                       SortExpression="TotalAmount" />
                                   <asp:BoundField DataField="InstallAmount" HeaderText="InstAmt" 
                                       SortExpression="InstallAmount" />
                                   <asp:BoundField DataField="Description" HeaderText="Description" 
                                       SortExpression="Description"></asp:BoundField>
                                  <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkRow" runat="server" />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHeader" runat="server" />
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                               </Columns>
                           </asp:GridView>
    
    
                                <asp:Button ID="Button2" runat="server" 
            CssClass="button" onclick="Button2_Click" 
                                    Text="Verified" Font-Size="10px" 
            Font-Bold="True" />
                            
    
                                <asp:Button ID="Button3" runat="server" 
            CssClass="button" onclick="Button3_Click" 
                                    Text="Reject" Font-Size="10px" Font-Bold="True" />
                            
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
