<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DataDashBoard.aspx.cs" Inherits="DataDashBoard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>--%>
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

 

    <script type="text/javascript">
        window.onload = showdata1;
        function showdata1() {
            var gridViewCtlId = '<%=GridView1. ClientID%>';
            var grid = document.getElementById(gridViewCtlId);
            var gridLength = grid.rows.length;
            for (var i = 1; i < gridLength; i++) {

                for (var j = 3; j < 11; j++) {


                    elementid = grid.rows[i].cells[j];
                    if (elementid.innerText == "Received") {
                        elementid.style.color = 'green';
                    }
                    else if (elementid.innerText = "Pending") {
                        elementid.style.color = 'red';
                        if (elementid.style.visibility == 'hidden')
                            elementid.style.visibility = 'visible';
                        else
                            elementid.style.visibility = 'hidden';

                    }
                }
            }
            setTimeout('showdata1()', 100);
        }
    </script>

    <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
<script src="scripts/jquery.blockUI.js" type="text/javascript"></script>
<script type = "text/javascript">
    function BlockUI(elementID) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function () {
            $("#" + elementID).block({ message: '<table align = "center"><tr><td>' +
     '<img src="images/loadingAnim.gif"/></td></tr></table>',
                css: {},
                overlayCSS: { backgroundColor: '#000000', opacity: 0.6
                }
            });
        });
        prm.add_endRequest(function () {
            $("#" + elementID).unblock();
        });
    }
    $(document).ready(function () {

        BlockUI("<%=pnlAddEdit.ClientID %>");
        $.blockUI.defaults.css = {};
    });
    function Hidepopup() {


      
        //in case u want to  the gridview to be not visible use this
         $find("popup").hide();
        
    
        return false;
    }
</script>



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
      .table-hover
      {
          text-align: left;
      }
      </style>

        <script type="text/javascript" src="https://www.google.com/jsapi"></script>

        

          <script type="text/javascript" src="https://www.google.com/jsapi"></script> 
        
        <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
   <script type="text/javascript">
       google.charts.load('current', { packages: ['corechart'] });     
   </script>
  

   <%--<script type = "text/javascript">

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
    </script>--%>

   <%--  <style type="text/css">
 
       label {
 
           display:block;
 
           padding:10px;
 
           margin:10px 0px;
 
       }
 
       .Items {
 
       background:#ffd800;
 
       color:#333;
 
       border: 1px #ff006e solid;
 
       }
 
       .Selected {
 
       background:#0094ff;
 
       color:#ffffff;
 
       border: 1px #ff006e solid;
 
       }
 
   </style>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: x-large;
            color: #CC99FF;
        }
    </style>--%>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
    <form>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
  
    <div class=Items>
      <center>
          <br />
          <span class="style1">Plant Regular Proceesing Data   
          <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
          <br />
          </span>
          <br class="style1" />
            <br />

       <asp:GridView ID="GridView1" runat="server" Font-Size="20px" 
              onrowdatabound="GridView1_RowDataBound" 
              onselectedindexchanged="GridView1_SelectedIndexChanged"  

              AutoGenerateColumns="False" >
              <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="Brown" />
                  <HeaderStyle BackColor="#f4f4f4" Font-Bold="False" Font-Italic="False"     Font-Names="Raavi" Font-Size="Small" />
                   <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
        <Columns>
                            <asp:TemplateField HeaderText="SNo">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Plantcode" HeaderText="Plantcode" 
                                SortExpression="Plantcode" />
                            <asp:BoundField DataField="PlantName" HeaderText="PlantName" 
                                SortExpression="PlantName" />
                            <asp:BoundField DataField="procureData" HeaderText="procureData" 
                                SortExpression="procureData" />
                            <asp:BoundField DataField="ClosingStock" HeaderText="ClosingStock" 
                                SortExpression="ClosingStock" />
                            <asp:BoundField DataField="RouteTime" HeaderText="RouteTime" 
                                SortExpression="RouteTime" />
                            <asp:BoundField DataField="CashCollection" HeaderText="CashCollection" 
                                SortExpression="CashCollection" />
                            <asp:BoundField DataField="GarberTesting" HeaderText="GarberTesting" 
                                SortExpression="GarberTesting" />
                          <%--  <asp:BoundField DataField="LiveData" HeaderText="LiveData" 
                                SortExpression="LiveData" />--%>
                                <asp:BoundField DataField="compressor" HeaderText="compressor" 
                                SortExpression="compressor" />
                                <asp:BoundField DataField="Rechilling" HeaderText="Rechilling" 
                                SortExpression=" Rechilling" />
                                <asp:BoundField DataField="GensetPower" HeaderText="GensetPower" 
                                SortExpression="GensetPower" />
                            <asp:CommandField ShowSelectButton="True" />
                            </Columns>
      </asp:GridView>

      
          <br />
      </center>  

<asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" style = "display:none">
<asp:Label Font-Bold = "true" ID = "Label4" runat = "server" Text = "Customer Details" ></asp:Label>
<br />
<table align = "center">
<tr>
<td>
<asp:Label ID = "Plcode" runat = "server" Text = "Plantcode" ></asp:Label>
</td>
<td>
<asp:TextBox ID="Plantcode" Width = "40px"   runat="server" Enabled="false"></asp:TextBox>
</td>
</tr>
<tr>
<td>
<asp:Label ID = "plname" runat = "server" Text = "PlantName" ></asp:Label>
</td>
<td>
<asp:TextBox ID="plant1name" runat="server" Enabled="false"></asp:TextBox>   
</td>
</tr>
<tr>
<td>
<asp:Label ID = "Locks" runat = "server" Text = "Unlock"  ></asp:Label>
</td>
<td>
 <asp:DropDownList ID="DropDownList1" runat="server">
           <asp:ListItem Value="1">Unlock</asp:ListItem>
           <asp:ListItem Value="0">Lock</asp:ListItem>
    </asp:DropDownList>
   
</td>
</tr>
<tr>
<td>
<asp:Button ID="btnSave" runat="server" Text="Save" onclick="Save_Click" />
</td>
<td>
<asp:Button ID="btnCancel" runat="server" Text="Cancel"      OnClientClick = "return Hidepopup()"/>
</td>
</tr>
</table>
</asp:Panel>


<asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
<asp:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
PopupControlID="pnlAddEdit" TargetControlID = "lnkFake"
BackgroundCssClass="modalBackground">
</asp:ModalPopupExtender>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID = "GridView1" />
<asp:AsyncPostBackTrigger ControlID = "btnSave" />
</Triggers>
</asp:UpdatePanel>
</form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

