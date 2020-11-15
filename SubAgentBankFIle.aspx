<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SubAgentBankFIle.aspx.cs" Inherits="SubAgentBankFIle" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
}
.form-style-3 label{
    display:block;
  <%--  margin-bottom: 10px;--%>;
        height: 34px;
        text-align: left;
    }
      </style>


          <style type="text/css">
              .form-style-9{
    max-width: 450px;
    background: #FAFAFA;
    padding: 20px;
    margin:20px auto;
    box-shadow: 1px 1px 25px rgba(0, 0, 0, 0.35);
    border-radius: 10px;
 
         height:315px;
         width: 564px;
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

<script language="javascript" type="text/javascript">


    function isNumber(evt) {
        var iKeyCode = (evt.which) ? evt.which : evt.keyCode
        if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
            return false;

        return true;

    }

    function isNumber1(evt, element) {

        var charCode = (evt.which) ? evt.which : event.keyCode

        if (
            (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
            (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
            (charCode < 48 || charCode > 57))
            return false;

        return true;
    }


    function isstring(evt) {
        var pwd = (evt.which) ? evt.which : evt.keyCode;
        if (pwd.match("^[a-z]*$")) {

        }
        else {
            alert('Not alphanumeric');
        }
    }



    function Allvalidate() {
        var summary = "";
        summary += isvalidamount();
        summary += isvalidFirstname();
        if (summary != "") {
            alert(summary);
            return false;
        }
        else {
            return false;
        }
    }




    function confirmationupdate() {
        if (confirm('Are you  want to Update ?')) {
            return true;
        }
        else {
            return false;
        }
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
        margin-right: 0px;
        }
    </style>




    <style type="text/css"> 
        .completionList {
        border:solid 1px Gray;
        margin:0px;
        padding:3px;
        height: 120px;
        overflow:auto;
        background-color: #FFFFFF;     
        } 
        .listItem {
        color: #191919;
        } 
        .itemHighlighted {
        background-color: #ADD6FF;       
        }
        .gridcls
        {
            margin-bottom: 0px;
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
      <script src="jquery.min.js" type="text/javascript"></script>  
    <script src="jquery-ui.min.js" type="text/javascript"></script> 
   
    


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="form1" >
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
    <div class="modal">
        <div class="center">
       
            <asp:Image ID="Image1" ImageUrl="waiting.gif" AlternateText="Processing" 
                runat="server" Height="128px" Width="130px" />
        </div>
    </div>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div align="center">
  
                
      <div class="form-style-9">
<div class="form-style-3">
<table style="height: 220px; width: 100%;">

<tr  ALIGN=LEFT>
<td WIDTH="98%" height="20px">

<fieldset  height:"50" style="height: auto"><legend>Sub Agent Bank File </legend>


    <table width="100%">
        <tr>
            <td align="right" width="50%">
                <strong style="font-size: small">Plant Name</strong></td>
            <td align="left" width="50%">
                <label for="field3">
                <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                    CLASS="field4" onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" 
                    Width="200px">
                </asp:DropDownList>
                </label>
            </td>
        </tr>
        <tr>
            <td align="right" width="50%">
                <strong style="font-size: small">&nbsp;Bill Date</strong></td>
            <td align="left" width="50%">
                <label for="field3">
                <asp:DropDownList ID="ddl_BillDate" runat="server" CLASS="field4" Width="200px">
                </asp:DropDownList>
                </label>
            </td>
        </tr>
        <tr>
            <td align="right" width="50%">
                <strong style="font-size: small">Agnet Id</strong></td>
            <td align="left" width="50%">
                <label for="field3">
                <asp:DropDownList ID="ddl_getagent" runat="server" AutoPostBack="True" 
                    CLASS="field4" onselectedindexchanged="ddl_Agentid_SelectedIndexChanged" 
                    Width="200px">
                </asp:DropDownList>
                </label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="width: 100%" width="50%">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                    RepeatDirection="Horizontal" 
                    style="font-size: small; font-weight: 700;" Width="300px" 
                    AutoPostBack="True" 
                    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Value="1">HdfcToHdfc</asp:ListItem>
                    <asp:ListItem Value="2">HdfcToOther</asp:ListItem>
                    <asp:ListItem Value="3">All Bank</asp:ListItem>
                    <asp:ListItem Value="4">Kotack</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2" style="width: 100%" width="50%">
                <asp:Label ID="lbl_ktk" runat="server" Text="Bank A/c"></asp:Label>
                <asp:DropDownList ID="ddl_kotack" runat="server" AutoPostBack="true" 
                    Font-Bold="True" Font-Size="Large" Visible="true" Width="190px">
                    <asp:ListItem Value="425044000438">425044000438</asp:ListItem>
                    <asp:ListItem>328044039913</asp:ListItem>
                    <asp:ListItem>334044049195</asp:ListItem>
                    <asp:ListItem>337044040029</asp:ListItem>
                    <asp:ListItem>334044032411</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
 </fieldset>


</td>
 
</tr>

</table>
</div>

<table style="width: 553px" >
        <tr width="100%" align=center>
            <td align="CENTER" style="text-align: left" >
           
               <asp:Button ID="Button6" runat="server" CssClass="form93" Font-Bold="true"   
                    Font-Size="X-Small"  xmlns:asp="#unknown"   
                    OnClientClick="return confirmationSave();" onclick="Button6_Click" Text="Get Details" 
                  TabIndex="6" />
           
                              
                  <asp:Button ID="Button10" runat="server" CssClass="form93" Font-Bold="true" 
                    Font-Size="X-Small" onclick="Button10_Click" TabIndex="6" Text="Bank Export" 
                    xmlns:asp="#unknown" />
           
                              
                  <asp:Button ID="Button11" runat="server" CssClass="form93" Font-Bold="true" 
                    Font-Size="X-Small" Text="Kotack" xmlns:asp="#unknown" 
                    onclick="Button11_Click" />
           
                              
                  <asp:Button ID="Button12" runat="server" CssClass="form93" Font-Bold="true" 
                    Font-Size="X-Small" onclick="Button12_Click" Text="Export" 
                    xmlns:asp="#unknown" />
           
                              
                  <asp:Button ID="Button9" runat="server" CssClass="form93" Font-Bold="True" 
                                           OnClientClick="return PrintPanel();" 
                          Font-Size="10px"  Text="Print" onclick="Button9_Click" />
                              
                <br />
                              
           </td>
        </tr>
        </table>
 
</div>          
      
   
 
   <br />
     <asp:Panel id="pnlContents" align="center" runat = "server">
    <table ALIGN="center" width="100%">
        
    <tr align="center" >
    <td align="center">
    <asp:Panel ID="news" runat="server">
    
                                              
                                               <asp:GridView ID="GridView1" runat="server" CssClass="gridcls" Font-Bold="True" 
                                                   Font-Size="14px" ForeColor="White" ShowFooter="True" Width="60%" onrowdatabound="GridView1_RowDataBound" 
                                                   onrowcreated="GridView1_RowCreated">
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
    <br />
    
                                               <asp:GridView ID="GridView2" runat="server" CssClass="gridcls" Font-Bold="True" 
                                                   Font-Size="14px" ForeColor="White" onrowdatabound="GridView2_RowDataBound" 
                                                   ShowFooter="True" Width="60%" onrowcreated="GridView2_RowCreated">
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
    
                                               <br />
                                               <asp:GridView ID="GridView7" runat="server" CssClass="gridcls" Font-Bold="True" 
                                                   Font-Size="14px" ForeColor="White" onrowcreated="GridView2_RowCreated" 
                                                   onrowdatabound="GridView2_RowDataBound" ShowFooter="True" Width="125%">
                                                   <EditRowStyle BackColor="#999999" />
                                                   <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
                                                   <HeaderStyle BackColor="#f4f4f4" Font-Bold="False" Font-Italic="False" 
                                                       Font-Names="Raavi" Font-Size="Small" ForeColor="Black" />
                                                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                   <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
                                                   <AlternatingRowStyle HorizontalAlign="Center" />
                                                   <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                  <%-- <Columns>
                                                       <asp:TemplateField HeaderText="SNo.">
                                                           <ItemTemplate>
                                                               <%# Container.DataItemIndex + 1 %>
                                                           </ItemTemplate>
                                                       </asp:TemplateField>
                                                   </Columns>--%>
                                               </asp:GridView>
    
                                               <asp:GridView ID="GridView5" runat="server" CssClass="gridview1" 
                                                   Font-Size="X-Small" AllowPaging="True" AutoGenerateColumns="False" 
                                                   BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
                                                   CellPadding="4" EnableViewState="False" Font-Italic="False" PageSize="2">
                                                   <Columns>
                                                       <asp:BoundField DataField="ACCOUNT" HeaderText="ACCOUNT" ReadOnly="True" 
                                                           SortExpression="ACCOUNT">
                                                           <ControlStyle Width="45px" />
                                                           <FooterStyle Width="45px" />
                                                           <HeaderStyle Width="45px" />
                                                           <ItemStyle Width="45px" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="C" HeaderText="C" ReadOnly="True" SortExpression="C">
                                                           <ControlStyle Width="45px" />
                                                           <FooterStyle Width="45px" />
                                                           <HeaderStyle Width="45px" />
                                                           <ItemStyle Width="45px" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" ReadOnly="True" 
                                                           SortExpression="AMOUNT">
                                                           <ControlStyle Width="45px" />
                                                           <FooterStyle Width="45px" />
                                                           <HeaderStyle Width="45px" />
                                                           <ItemStyle Width="45px" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="NARRATION" HeaderText="NARRATION" ReadOnly="True" 
                                                           SortExpression="NARRATION">
                                                           <ControlStyle Width="45px" />
                                                           <FooterStyle Width="45px" />
                                                           <HeaderStyle Width="45px" />
                                                           <ItemStyle Width="45px" />
                                                       </asp:BoundField>
                                                   </Columns>
                                                   <FooterStyle BackColor="#99CCCC" ForeColor="#003399" Font-Size="Small" />
                                                   <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                                   <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                                   <RowStyle BackColor="White" ForeColor="#003399" />
                                                   <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                    
                                                   <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                   <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                   <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                   <SortedDescendingHeaderStyle BackColor="#002876" />
                                                    
                                               </asp:GridView>
    
                                               <asp:GridView ID="GridView6" runat="server" AllowPaging="True" 
                                                   AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
                                                   BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="gridview1" 
                                                   EnableViewState="False" Font-Italic="False" Font-Size="X-Small" PageSize="2">
                                                   <Columns>
                                                       <asp:BoundField DataField="TranType" HeaderText="TranType" ReadOnly="True" 
                                                           SortExpression="TranType">
                                                           <ControlStyle Width="45px" />
                                                           <FooterStyle Width="45px" />
                                                           <HeaderStyle Width="45px" />
                                                           <ItemStyle Width="45px" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="ACCOUNT" HeaderText="ACCOUNT" ReadOnly="True" 
                                                           SortExpression="ACCOUNT">
                                                           <ControlStyle Width="45px" />
                                                           <FooterStyle Width="45px" />
                                                           <HeaderStyle Width="45px" />
                                                           <ItemStyle Width="45px" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="AMOUNT" HeaderText="AMOUNT" ReadOnly="True" 
                                                           SortExpression="AMOUNT">
                                                           <ControlStyle Width="45px" />
                                                           <FooterStyle Width="45px" />
                                                           <HeaderStyle Width="45px" />
                                                           <ItemStyle Width="45px" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="AgentName" HeaderText="AgentName" ReadOnly="True" 
                                                           SortExpression="AgentName">
                                                           <ControlStyle Width="45px" />
                                                           <FooterStyle Width="45px" />
                                                           <HeaderStyle Width="45px" />
                                                           <ItemStyle Width="45px" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Agent_Id" HeaderText="Agent_Id" ReadOnly="True" 
                                                           SortExpression="Agent_Id">
                                                           <ControlStyle Width="45px" />
                                                           <FooterStyle Width="45px" />
                                                           <HeaderStyle Width="45px" />
                                                           <ItemStyle Width="45px" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="PayDate" HeaderText="PayDate" ReadOnly="True" 
                                                           SortExpression="PayDate">
                                                           <ControlStyle Width="45px" />
                                                           <FooterStyle Width="45px" />
                                                           <HeaderStyle Width="45px" />
                                                           <ItemStyle Width="45px" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Ifsccode" HeaderText="Ifsccode" ReadOnly="True" 
                                                           SortExpression="Ifsccode">
                                                           <ControlStyle Width="45px" />
                                                           <FooterStyle Width="45px" />
                                                           <HeaderStyle Width="45px" />
                                                           <ItemStyle Width="45px" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="BankName" HeaderText="BankName" ReadOnly="True" 
                                                           SortExpression="BankName">
                                                           <ControlStyle Width="45px" />
                                                           <FooterStyle Width="45px" />
                                                           <HeaderStyle Width="45px" />
                                                           <ItemStyle Width="45px" />
                                                       </asp:BoundField>
                                                       <asp:BoundField DataField="Pmail" HeaderText="Pmail" ReadOnly="True" 
                                                           SortExpression="Pmail">
                                                           <ControlStyle Width="45px" />
                                                           <FooterStyle Width="45px" />
                                                           <HeaderStyle Width="45px" />
                                                           <ItemStyle Width="45px" />
                                                       </asp:BoundField>
                                                   </Columns>
                                                   <FooterStyle BackColor="#99CCCC" Font-Size="Small" ForeColor="#003399" />
                                                   <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                                                   <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                                   <RowStyle BackColor="White" ForeColor="#003399" />
                                                   <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                                   <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                                   <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                                   <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                                   <SortedDescendingHeaderStyle BackColor="#002876" />
                                               </asp:GridView>
    
                                               <br />
    
                                              
                                               <br />
    
    </asp:Panel>
    </td>
    
    </tr>
    
    </table>
  
            <br />
    </center>
            <br />
            <br />         
            

  </asp:Panel>
</ContentTemplate>


<Triggers>
  <%--  <asp:PostBackTrigger ControlID="Button8" />--%>
    <asp:PostBackTrigger ControlID="Button9" />   
     <asp:PostBackTrigger ControlID="Button6" />    
      <asp:PostBackTrigger ControlID="Button10" />
      <asp:PostBackTrigger ControlID="Button12" />
      
     </Triggers>
</asp:UpdatePanel>
  
    </form>            
            
      
            
            
            
                   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
