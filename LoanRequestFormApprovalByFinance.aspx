<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LoanRequestFormApprovalByFinance.aspx.cs" Inherits="LoanRequestFormApprovalByFinance" %>
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




   
    <table ALIGN="center" width="100%">
        
    <tr align="center" class="text2">
    <td align="center">
    
                           <asp:GridView ID="GridView2" runat="server" 
                               
            CssClass="table table-striped table-bordered table-hover" Font-Size="Small" 
                PageSize="20" AutoGenerateColumns="False">
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
                                   <asp:BoundField DataField="LoanAmount" HeaderText="LoanAmount" 
                                       SortExpression="LoanAmount"></asp:BoundField>
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
    
    
                                <asp:GridView ID="GridView3" runat="server" 
                               CssClass="table table-striped table-bordered table-hover" Font-Size="Small" 
                               PageSize="20">
                                    <HeaderStyle ForeColor="#660066" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                           </asp:GridView>
    
    
                                <br />
    
    
                                <asp:Button ID="Button2" runat="server" CssClass="button" onclick="Button2_Click" 
                                    Text="Verified" Font-Size="10px" Font-Bold="True" />
                            
    
                                <asp:Button ID="Button3" runat="server" CssClass="button" onclick="Button3_Click" 
                                    Text="Export" Font-Size="10px" Font-Bold="True" />
                            
    </td>
    
    </tr>
    
    </table>
  
            <br />
    </center>
            <br />
            <br />     
            
            
    
        </div>
</ContentTemplate>


<Triggers>
    <asp:PostBackTrigger ControlID="Button2" />
    <asp:PostBackTrigger ControlID="Button3" />
</Triggers>
</asp:UpdatePanel>
  
    </form>        
            
            
                       
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
