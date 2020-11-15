<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AccountsubHead.aspx.cs" Inherits="AccountsubHead" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link id="Link2" type="text/css" href="style/Menu.css"  rel="Stylesheet" runat="server"/>

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
            (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-" CHECK MINUS, AND ONLY ONE.
            (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “." CHECK DOT, AND ONLY ONE.
            (charCode < 48 || charCode > 57))
               return false;

           return true;
       }    

</script>


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
        font-size: medium;
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

.text1:hover,
.text1:focus {
    width: 260px;
    transition: 500ms all ease;
    background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyBpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNSBXaW5kb3dzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjYxRDEzQTBCMzI0MzExRTFBNDYzRkQ4Qzc3RDdBOTg5IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjYxRDEzQTBDMzI0MzExRTFBNDYzRkQ4Qzc3RDdBOTg5Ij4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6NjFEMTNBMDkzMjQzMTFFMUE0NjNGRDhDNzdEN0E5ODkiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6NjFEMTNBMEEzMjQzMTFFMUE0NjNGRDhDNzdEN0E5ODkiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz52VTCHAAABFUlEQVR42uxV0Q2DIBBV4z8bdAM26AZ1Ap3AToAT6AR1Ap2gbuAGzKD/TkCP5NmQRpQaSH96yQsJHO/k7t0ZK6WikJZEgS14gHSeZxX6BQuhwerL3pw6ACMIQkuQHsgluDQnM2ugN3rgrK33ha3INdbqBHn1wWFVUU7ghMKxLgt8Oe46yVQ7lrgoD/JdwDf/tg+ueK4mGDfOR5zV8D3VaBwFy6CM1VrsCfjYG81BclrPHVZp7HcoLN8LshdgNMg50pDh7In+uSCIsKUpnqZJWfQ8EB4gOlLRnXBzVVFvpIA5yJTBV241aeLSLI622aRmiho8k3uYRcM6LlLksEWzMA/DjqP4+oNLW5G9Wfz/J/88wEuAAQA9yExzBAEQqwAAAABJRU5ErkJggg==) no-repeat right;
    background-size: 25px 25px;
    background-position: 96% 62%;
    padding: 3px 32px 3px 3px;
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

.text1:hover,
.text1:focus {
    width: 260px;
    transition: 500ms all ease;
    background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyBpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNSBXaW5kb3dzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjYxRDEzQTBCMzI0MzExRTFBNDYzRkQ4Qzc3RDdBOTg5IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjYxRDEzQTBDMzI0MzExRTFBNDYzRkQ4Qzc3RDdBOTg5Ij4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6NjFEMTNBMDkzMjQzMTFFMUE0NjNGRDhDNzdEN0E5ODkiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6NjFEMTNBMEEzMjQzMTFFMUE0NjNGRDhDNzdEN0E5ODkiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz52VTCHAAABFUlEQVR42uxV0Q2DIBBV4z8bdAM26AZ1Ap3AToAT6AR1Ap2gbuAGzKD/TkCP5NmQRpQaSH96yQsJHO/k7t0ZK6WikJZEgS14gHSeZxX6BQuhwerL3pw6ACMIQkuQHsgluDQnM2ugN3rgrK33ha3INdbqBHn1wWFVUU7ghMKxLgt8Oe46yVQ7lrgoD/JdwDf/tg+ueK4mGDfOR5zV8D3VaBwFy6CM1VrsCfjYG81BclrPHVZp7HcoLN8LshdgNMg50pDh7In+uSCIsKUpnqZJWfQ8EB4gOlLRnXBzVVFvpIA5yJTBV241aeLSLI622aRmiho8k3uYRcM6LlLksEWzMA/DjqP4+oNLW5G9Wfz/J/88wEuAAQA9yExzBAEQqwAAAABJRU5ErkJggg==) no-repeat right;
    background-size: 25px 25px;
    background-position: 96% 62%;
    padding: 3px 32px 3px 3px;
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

.text2:hover,
.text2:focus {
    width:100%;
    transition: 500ms all ease;
  
    background-size: 25px 25px;
    background-position: 96% 62%;
    padding: 3px 32px 3px 3px;
}
      .style1
      {
          width: 100%;
      }
      </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">

    </asp:ToolkitScriptManager>

    
    <br />
    <center>
    <asp:Panel ID="panel1" ALIGN="CENTER" width="50%" runat="server">
    <table align="center" width="100%" style="border: thin ridge #008080">
        <tr>
            <td align="right" width="30%">
                <asp:Label ID="Label4" runat="server" Font-Size="Medium" Text="Plant Name"></asp:Label>
            </td>
            <td width="25%" align="left" valign="top">
                <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                     Font-Bold="True" Font-Size="Large" Height="30px" 
                    onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="230px" 
                    CssClass="tb10">
                </asp:DropDownList>
                
                </td>
            <td width="25%">
                                    &nbsp;</td>
        </tr>
        <tr>
            <td align="right" width="30%">
                <asp:Label ID="Label2" runat="server" Font-Size="Medium" Text="Sub Header"></asp:Label>
            </td>
            <td width="25%" align="left" valign="top">
                
                <div class="dropdown">


      <asp:DropDownList ID="dtpsubhead" runat="server" 
                        onselectedindexchanged="dtpsubhead_SelectedIndexChanged" AutoPostBack="True" 
                        Height="30px" CssClass="tb10">
          <asp:ListItem Value="0">----------Select----------</asp:ListItem>
          <asp:ListItem Value="1">Operating Expenses</asp:ListItem>
          <asp:ListItem Value="2">Procurement Expenses</asp:ListItem>
          <asp:ListItem Value="3">Administrative Expenses</asp:ListItem>
          <asp:ListItem Value="4">Employee Benefit Expenses</asp:ListItem>
          <asp:ListItem Value="5">Rates &amp; Taxes</asp:ListItem>
          <asp:ListItem Value="6">Financial Expences</asp:ListItem>
      </asp:DropDownList>



  
</div>
                
                </td>
            <td width="25%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                    ControlToValidate="dtpsubhead" InitialValue="0" runat="server" 
                    ErrorMessage=" select Group"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Date</td>
            <td colspan="2" align="left">

    
                                <asp:TextBox ID="txt_FromDate" runat="server" class="text1"
                                    Width="200px" ></asp:TextBox>
                         <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" PopupPosition="TopRight">
                        </asp:CalendarExtender>                   	
            </td>
        </tr>
        <tr>
            <td align="right">
                Amount</td>
            <td colspan="2" align="left">

    
                                <asp:TextBox ID="txt_itemAmount" runat="server" class="text1" Width="200px"   onkeypress="javascript:return isNumber(event)" 
                                    AutoPostBack="True" CssClass="text1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="txt_itemAmount" ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                Description</td>
            <td colspan="2" align="left">

    
                                <asp:TextBox ID="txt_description" runat="server" class="text1" Width="200px"   
                                    AutoPostBack="True" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td align="right">

    
                            <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                                Height="16px" Visible="false" Width="29px">
                            </asp:DropDownList>

    
                                <asp:Button ID="Button1" runat="server" CssClass="button" onclick="Button1_Click" 
                                    Text="Save" Font-Size="10px" Font-Bold="True" />
                            
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </asp:Panel>
        <br />
        <br />
        <asp:Panel ID="Panel22"  Width="50%" runat="server">
            <table class="text2">
                <tr WIDTH=50%>
                    <td align=center style="text-align: right">
                        Amount</td>
                    <td WIDTH="50%">
                        <asp:TextBox ID="txt_itemAmountupdate" runat="server" AutoPostBack="True" 
                            class="text1" CssClass="text1" onkeypress="javascript:return isNumber(event)" 
                            Width="200px" ontextchanged="txt_itemAmountupdate_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr WIDTH="50%">
                    <td align="center" style="text-align: right">
                        &nbsp;</td>
                    <td WIDTH="50%">
                        <asp:Button ID="Button2" runat="server" CssClass="button" Font-Bold="True" 
                            Font-Size="10px" onclick="Button2_Click" Text="Update" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </center>
            <br />
            <table class="text2" align=center>
                <tr align="center">
                    <td colspan="2">

                    <asp:GridView ID="GridView1"
CssClass="table table-striped table-bordered table-hover"
   runat="server" PageSize="20" Font-Size="Medium" onpageindexchanging="GridView1_PageIndexChanging" 
                            onrowcancelingedit="GridView1_RowCancelingEdit" 
                            onselectedindexchanging="GridView1_SelectedIndexChanging" 
                            onselectedindexchanged="GridView1_SelectedIndexChanged">
  
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
  
                        <HeaderStyle ForeColor="#660066" />
                    </asp:GridView>
                            </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
    </table>
            <br />                


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

