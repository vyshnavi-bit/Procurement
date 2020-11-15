<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="OrgFormAllotement.aspx.cs" Inherits="OrgFormAllotement" %>
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
 
         height:197px;
         width: 553px;
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

<script type="text/javascript">
    $(document).ready(function () {
        $('#verticalTab').easyResponsiveTabs({
            type: 'vertical',
            width: 'auto',
            fit: true
        });
    });
</script>

<script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
<script src="js/easyResponsiveTabs.js" type="text/javascript"></script>
<link type="text/css" rel="stylesheet" href="css/easy-responsive-tabs.css" />
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
    height: 128px;
    width: 128px;
}
    </style>




    <%-- <asp:TextBox ID="TextBox2" runat="server" MaxLength="8" 
                              onkeypress="javascript:return isNumber1(event)" Width="201px"></asp:TextBox>--%><%-- <asp:TextBox ID="TextBox2" runat="server" MaxLength="8" 
                              onkeypress="javascript:return isNumber1(event)" Width="201px"></asp:TextBox>--%><%-- <asp:TextBox ID="TextBox2" runat="server" MaxLength="8" 
                              onkeypress="javascript:return isNumber1(event)" Width="201px"></asp:TextBox>--%><%-- <asp:TextBox ID="TextBox2" runat="server" MaxLength="8" 
                              onkeypress="javascript:return isNumber1(event)" Width="201px"></asp:TextBox>--%><%-- <asp:TextBox ID="TextBox2" runat="server" MaxLength="8" 
                              onkeypress="javascript:return isNumber1(event)" Width="201px"></asp:TextBox>--%>

  <style type="text/css">    
    
ul,li,body
{
     margin:0;
     padding:0;
}
 
/* MultiView Tab Using Menu Control */
 
.tabs
    {
    position:relative;
    top:1px;       
    z-index:2;     
    }
   
    .tab
    {
        border:1px solid black;
        background-image:url(images/navigation.jpg);
        background-repeat:repeat-x;
        color:White;       
        padding:2px 10px; 
    }
   
    .selectedtab
    {
    background:none;
    background-repeat:repeat-x;
    color:black; 
   }
   
   
.tabcontents
    {
    border:1px solid black;
    padding:10px;
    width:100%;
    height:100%;  
    background-color:white;        
   
    }
 
   </style>

 <%-- <asp:TextBox ID="TextBox2" runat="server" MaxLength="8" 
                              onkeypress="javascript:return isNumber1(event)" Width="201px"></asp:TextBox>--%>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
    <form id="form1">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <br />
    <br />
    <div>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Menu1" />                
                </Triggers>
                <ContentTemplate>
                <asp:Label ID="TimeLabel" runat="server" Text="" />       
                <center>                
         <asp:Menu ID="Menu1" Orientation="Horizontal"  StaticMenuItemStyle-CssClass="tab"
         StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server"
            onmenuitemclick="Menu1_MenuItemClick">
            <Items>
            <asp:MenuItem  Text="Add Plant" Value="0" Selected="true"></asp:MenuItem>
            <asp:MenuItem Text="Add Manager" Value="1"></asp:MenuItem>          
            <asp:MenuItem Text="Add System" Value="2"></asp:MenuItem>          
            <asp:MenuItem Text="Add Employee" Value="3"></asp:MenuItem>          
            <asp:MenuItem Text="Add Supervisor" Value="4"></asp:MenuItem>          
            <asp:MenuItem Text="Add RouteName" Value="5"></asp:MenuItem>          
            <asp:MenuItem Text="Add Casualus" Value="6"></asp:MenuItem>          
            <asp:MenuItem Text="Add Security" Value="7"></asp:MenuItem>       
              <asp:MenuItem Text="Edit Casualus" Value="8"></asp:MenuItem>       
            </Items>    
             <StaticMenuItemStyle CssClass="tab" />
             <StaticSelectedStyle CssClass="selectedtab" />
         </asp:Menu>
         </center>     
       <div class="tabcontents">
        <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
        <asp:View ID="View1" runat="server">
        

        <table width=50% align="center">
        <tr width=50%>
       
        <td  width=50% align=right>
            <strong style="font-size: small; text-align: center;">Plant Name</strong></td>
        <td width=50%>
            <label for="field3">
            <asp:DropDownList ID="ddl_Plantname" runat="server"
                CLASS="field4" 
                Width="200px">
            </asp:DropDownList>
            </label>
        </td>
        </tr>
            <tr width="50%">
                <td align="right" width="50%">
                    Description</td>
                <td width="50%">
                    <asp:TextBox ID="txt_plandesc" runat="server" MaxLength="8"  TextMode="MultiLine" 
                        Width="201px"></asp:TextBox>
                </td>
            </tr>
            <tr width="50%">
                <td align="center" colspan="2" width="50%">
                    <asp:Button ID="Save" runat="server" CssClass="form93" Font-Bold="true" 
                        Font-Size="X-Small" onclick="Save_Click" 
                        OnClientClick="return confirmationSave();" TabIndex="6" Text="Save Details" 
                        xmlns:asp="#unknown" />
                </td>
            </tr>
        </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
          <table width=50% align="center">
        <tr width=50%>
       
        <td  width=50% align=right>
            <strong style="font-size: small; text-align: center;">Plant Name</strong></td>
        <td width=50%>
            <label for="field3">
            <asp:DropDownList ID="ddl_Plantname1" runat="server"
                CLASS="field4" 
                Width="200px">
            </asp:DropDownList>
            </label>
        </td>
        </tr>
              <tr width="50%">
                  <td align="right" width="50%">
                   <strong style="font-size: small; text-align: center;">Manager</strong>
                       </td>
                  <td width="50%">
                      <label for="field3">
                      <asp:DropDownList ID="ddl_manager" runat="server" CLASS="field4" 
                          Width="200px">
                          <asp:ListItem Value="1">Manager</asp:ListItem>
                      </asp:DropDownList>
                      </label>
                  </td>
              </tr>
              <tr width="50%">
                  <td align="right" width="50%">
                      <strong style="font-size: small; text-align: center;">Manager Name</strong>
                  </td>
                  <td width="50%">
                      <asp:TextBox ID="txt_ManagerName" runat="server" Width="201px"></asp:TextBox>
                  </td>
              </tr>
              <tr width="50%">
                  <td align="right" width="50%">
                      Description</td>
                  <td width="50%">
                      <asp:TextBox ID="txt_plandesc1" runat="server"  
                          TextMode="MultiLine" Width="201px"></asp:TextBox>
                  </td>
              </tr>
            <tr width="50%">
                <td align="center" colspan="2" width="50%">
                    <asp:Button ID="Button1" runat="server" CssClass="form93" Font-Bold="true" 
                        Font-Size="X-Small" onclick="Button6_Click" 
                        OnClientClick="return confirmationSave();" TabIndex="6" Text="Save Details" 
                        xmlns:asp="#unknown" />
                </td>
            </tr>
        </table>
        </asp:View>       
          <asp:View ID="View3" runat="server">
          
         <table width=50% align="center">
        <tr width=50%>
       
        <td  width=50% align=right>
            <strong style="font-size: small; text-align: center;">Plant Name</strong></td>
        <td width=50%>
            <label for="field3">
            <asp:DropDownList ID="ddl_Plantname2" runat="server"
                CLASS="field4" 
                Width="200px" AutoPostBack="True" 
                onselectedindexchanged="ddl_Plantname2_SelectedIndexChanged">
            </asp:DropDownList>
            </label>
        </td>
        </tr>
              <tr width="50%">
                  <td align="right" width="50%">
                   <strong style="font-size: small; text-align: center;">Manager</strong>
                       </td>
                  <td width="50%">
                      <label for="field3">
                      <asp:DropDownList ID="ManName" runat="server" CLASS="field4" 
                          Width="200px">
                          <asp:ListItem Value="1">Manager</asp:ListItem>
                      </asp:DropDownList>
                      </label>
                  </td>
              </tr>
              <tr width="50%">
                  <td align="right" width="50%">
                      <strong style="font-size: small; text-align: center;">System Name </strong>
                  &nbsp;</td>
                  <td width="50%">
                      <asp:TextBox ID="txt_sysname" runat="server" Width="201px"></asp:TextBox>
                  </td>
              </tr>
             <tr width="50%">
                 <td align="right" width="50%">
                     Description</td>
                 <td width="50%">
                     <asp:TextBox ID="txt_plandesc2" runat="server" TextMode="MultiLine" 
                         Width="201px"></asp:TextBox>
                 </td>
             </tr>
            <tr width="50%">
                <td align="center" colspan="2" width="50%">
                    <asp:Button ID="Button2" runat="server" CssClass="form93" Font-Bold="true" 
                        Font-Size="X-Small" onclick="Button2_Click" 
                        OnClientClick="return confirmationSave();" TabIndex="6" Text="Save Details" 
                        xmlns:asp="#unknown" />
                </td>
            </tr>
        </table>


        </asp:View>       

          <asp:View ID="View4" runat="server">
              <table align="center" width="50%">
                  <tr width="50%">
                      <td align="right" width="50%">
                          <strong style="font-size: small; text-align: center;">Plant Name</strong></td>
                      <td width="50%">
                          <label for="field3">
                          <asp:DropDownList ID="ddl_Plantname3" runat="server" CLASS="field4" 
                              Width="200px" AutoPostBack="True" 
                              onselectedindexchanged="ddl_Plantname3_SelectedIndexChanged">
                          </asp:DropDownList>
                          </label>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="right" width="50%">
                          <strong style="font-size: small; text-align: center;">System Name</strong>
                      </td>
                      <td width="50%">
                          <label for="field3">
                          <asp:DropDownList ID="ddlsysname" runat="server" CLASS="field4" 
                              Width="200px">
                          </asp:DropDownList>
                          </label>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="right" width="50%">
                          Staff<strong style="font-size: small; text-align: center;"> Name </strong>&nbsp;</td>
                      <td width="50%">
                         <%-- <asp:TextBox ID="TextBox2" runat="server" MaxLength="8" 
                              onkeypress="javascript:return isNumber1(event)" Width="201px"></asp:TextBox>--%>
                               <asp:TextBox ID="sysopname" runat="server"  Width="201px"></asp:TextBox>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="right" width="50%">
                          Description</td>
                      <td width="50%">
                          <asp:TextBox ID="txt_empdesc" runat="server" MaxLength="8" TextMode="MultiLine" 
                              Width="201px"></asp:TextBox>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="center" colspan="2" width="50%">
                          <asp:Button ID="Button7" runat="server" CssClass="form93" Font-Bold="true" 
                              Font-Size="X-Small" onclick="Button7_Click" 
                              OnClientClick="return confirmationSave();" TabIndex="6" Text="Save Details" 
                              xmlns:asp="#unknown" />
                      </td>
                  </tr>
              </table>
        </asp:View>       
          <asp:View ID="View5" runat="server">
         <table align="center" width="50%">
                  <tr width="50%">
                      <td align="right" width="50%">
                          <strong style="font-size: small; text-align: center;">Plant Name</strong></td>
                      <td width="50%">
                          <label for="field3">
                          <asp:DropDownList ID="ddl_Plantname4" runat="server" CLASS="field4" 
                              Width="200px" AutoPostBack="True" 
                              onselectedindexchanged="ddl_Plantname4_SelectedIndexChanged">
                          </asp:DropDownList>
                          </label>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="right" width="50%">
                          Route
                      </td>
                      <td width="50%">
                          <label for="field3">
                          <asp:DropDownList ID="ddl_route" runat="server" CLASS="field4" 
                              Width="200px">
                              <asp:ListItem Value="1">Route</asp:ListItem>
                          </asp:DropDownList>
                          </label>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="right" width="50%">
                           Supervisor<strong style="font-size: small; text-align: center;">Name </strong>&nbsp;</td>
                      <td width="50%">
                          <label for="field3">
                          <asp:DropDownList ID="ddl_superwise" runat="server" CLASS="field4" 
                              Width="200px">
                          </asp:DropDownList>
                          </label>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="right" width="50%">
                          Description</td>
                      <td width="50%">
                          <asp:TextBox ID="txt_supervisordesc" runat="server" MaxLength="8" 
                              TextMode="MultiLine" Width="201px"></asp:TextBox>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="center" colspan="2" width="50%">
                          <asp:Button ID="Button3" runat="server" CssClass="form93" Font-Bold="true" 
                              Font-Size="X-Small" onclick="Button3_Click" 
                              OnClientClick="return confirmationSave();" TabIndex="6" Text="Save Details" 
                              xmlns:asp="#unknown" />
                      </td>
                  </tr>
              </table>
        </asp:View>     
          <asp:View ID="View6" runat="server">
       <table align="center" width="50%">
                  <tr width="50%">
                      <td align="right" width="50%">
                          <strong style="font-size: small; text-align: center;">Plant Name</strong></td>
                      <td width="50%">
                          <label for="field3">
                          <asp:DropDownList ID="ddl_Plantname5" runat="server" CLASS="field4" 
                              Width="200px" onselectedindexchanged="ddl_Plantname5_SelectedIndexChanged" 
                              AutoPostBack="True">
                          </asp:DropDownList>
                          </label>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="right" width="50%">
                          Supervisor Name
                      </td>
                      <td width="50%">
                          <label for="field3">
                          <asp:DropDownList ID="supervisorr" runat="server" CLASS="field4" 
                              Width="200px">
                              <asp:ListItem Value="1">Route</asp:ListItem>
                          </asp:DropDownList>
                          </label>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="right" width="50%">
                           Route<strong style="font-size: small; text-align: center;"> Name </strong>&nbsp;</td>
                      <td width="50%">
                          <label for="field3">
                          <asp:DropDownList ID="Drop_Routename" runat="server" CLASS="field4" 
                              Width="200px">
                              <asp:ListItem Value="1">Route</asp:ListItem>
                          </asp:DropDownList>
                          </label>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="right" width="50%">
                          Description<strong style="font-size: small; text-align: center;"> </strong>&nbsp;&nbsp;</td>
                      <td width="50%">
                          <asp:TextBox ID="txt_employee" runat="server" MaxLength="8" 
                              TextMode="MultiLine" Width="201px"></asp:TextBox>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="center" colspan="2" width="50%">
                          <asp:Button ID="Button4" runat="server" CssClass="form93" Font-Bold="true" 
                              Font-Size="X-Small" onclick="Button4_Click" 
                              OnClientClick="return confirmationSave();" TabIndex="6" Text="Save Details" 
                              xmlns:asp="#unknown" />
                      </td>
                  </tr>
              </table>
        </asp:View>       
          <asp:View ID="View7" runat="server">
          <table align="center" width="50%">
                  <tr width="50%">
                      <td align="right" width="50%">
                          <strong style="font-size: small; text-align: center;">Plant Name</strong></td>
                      <td width="50%">
                          <label for="field3">
                          <asp:DropDownList ID="ddl_Plantname6" runat="server" CLASS="field4" 
                              Width="200px">
                          </asp:DropDownList>
                          </label>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="right" width="50%">
                          &nbsp;Name
                      </td>
                      <td width="50%">
                          <label for="field3">
                          <asp:DropDownList ID="dd_casulas" runat="server" CLASS="field4" 
                              Width="200px">
                              <asp:ListItem Value="1">Casuals</asp:ListItem>
                          </asp:DropDownList>
                          </label>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="right" width="50%">
                           Casual<strong style="font-size: small; text-align: center;"> Name </strong>&nbsp;</td>
                      <td width="50%">
                          <asp:TextBox ID="Casuals" runat="server" Width="201px"></asp:TextBox>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="right" width="50%">
                          Description</td>
                      <td width="50%">
                          <asp:TextBox ID="txt_Casuladesc" runat="server" MaxLength="8" 
                              TextMode="MultiLine" Width="201px"></asp:TextBox>
                      </td>
                  </tr>
                  <tr width="50%">
                      <td align="center" colspan="2" width="50%">
                          <asp:Button ID="Button5" runat="server" CssClass="form93" Font-Bold="true" 
                              Font-Size="X-Small" onclick="Button5_Click" 
                              OnClientClick="return confirmationSave();" TabIndex="6" Text="Save Details" 
                              xmlns:asp="#unknown" />
                      </td>
                  </tr>
              </table>
        </asp:View>  
            <asp:View ID="View8" runat="server">
                <table align="center" width="50%">
                    <tr width="50%">
                        <td align="right" width="50%">
                            <strong style="font-size: small; text-align: center;">Plant Name</strong></td>
                        <td width="50%">
                            <label for="field3">
                            <asp:DropDownList ID="ddl_Plantname7" runat="server" CLASS="field4" 
                                Width="200px">
                            </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr width="50%">
                        <td align="right" width="50%">
                            &nbsp;Name
                        </td>
                        <td width="50%">
                            <label for="field3">
                            <asp:DropDownList ID="DropDownList7" runat="server" CLASS="field4" 
                                Width="200px">
                                <asp:ListItem Value="1">Security</asp:ListItem>
                            </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr width="50%">
                        <td align="right" width="50%">
                            <strong style="font-size: small; text-align: center;">Security&nbsp; Name </strong>&nbsp;</td>
                        <td width="50%">
                            <asp:TextBox ID="Security" runat="server"  Width="201px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr width="50%">
                        <td align="right" width="50%">
                            Description</td>
                        <td width="50%">
                            <asp:TextBox ID="txt_securitydesc" runat="server" MaxLength="8" 
                                TextMode="MultiLine" Width="201px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr width="50%">
                        <td align="center" colspan="2" width="50%">
                            <asp:Button ID="Button8" runat="server" CssClass="form93" Font-Bold="true" 
                                Font-Size="X-Small" onclick="Button8_Click" 
                                OnClientClick="return confirmationSave();" TabIndex="6" Text="Save Details" 
                                xmlns:asp="#unknown" />
                        </td>
                    </tr>
                </table>
        </asp:View>   
        <asp:View ID="View9" runat="server">
                <table align="center" width="50%">
                    <tr width="50%">
                        <td align="right" width="50%">
                            <strong style="font-size: small; text-align: center;">Plant Name</strong></td>
                        <td width="50%">
                            <label for="field3">
                            <asp:DropDownList ID="ddl_Plantname8" runat="server" CLASS="field4" 
                                Width="200px" onselectedindexchanged="ddl_Plantname8_SelectedIndexChanged" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr width="50%">
                        <td align="right" width="50%">
                            &nbsp;Name
                        </td>
                        <td width="50%">
                            <label for="field3">
                            <asp:DropDownList ID="CasualName1" runat="server" CLASS="field4" 
                                Width="200px">
                            </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr width="50%">
                        <td align="right" width="50%">
                            <strong style="font-size: small; text-align: center;">Security&nbsp; Name </strong>&nbsp;</td>
                        <td width="50%">
                            <label for="field3">
                            <asp:DropDownList ID="CasualName2" runat="server" CLASS="field4" Width="200px">
                            </asp:DropDownList>
                            </label>
                        </td>
                    </tr>
                    <tr width="50%">
                        <td align="right" width="50%">
                            Description</td>
                        <td width="50%">
                            <asp:TextBox ID="editCasual" runat="server" MaxLength="8" 
                                TextMode="MultiLine" Width="201px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr width="50%">
                        <td align="center" colspan="2" width="50%">
                            <asp:Button ID="Button9" runat="server" CssClass="form93" Font-Bold="true" 
                                Font-Size="X-Small" onclick="Button9_Click" 
                                OnClientClick="return confirmationSave();" TabIndex="6" Text="Save Details" 
                                xmlns:asp="#unknown" />
                        </td>
                    </tr>
                </table>
        </asp:View>
        
                      
        </asp:MultiView>
        </div> 
        </ContentTemplate>
        </asp:UpdatePanel>       
    </div>
    </form>
           
                   
</asp:Content>
 
