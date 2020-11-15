<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Silostock.aspx.cs" Inherits="Silostock" %>

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
          color: #660066;
          font-weight: 700;
      }
      .style2
      {
          height: 53px;
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





    <script src="jquery-2.1.1.min.js" type="text/javascript"></script>
<script src="amcharts/amcharts.js" type="text/javascript"></script>
<script src="amcharts/serial.js" type="text/javascript"></script>
<%--<script type="text/javascript">

    AmCharts.ready(function () {
        var chart = new AmCharts.AmSerialChart();
        var j = '<%=jsondata%>';
        chart.dataProvider = j;
        chart.categoryField = "subject";

        var graph = new AmCharts.AmGraph();
        graph.valueField = "plant_name";
        graph.type = "Milkkg";
        chart.addGraph(graph);
        var categoryAxis = chart.categoryAxis;
        categoryAxis.autoGridCount = false;
        categoryAxis.gridCount = chart.dataProvider.length;
        categoryAxis.gridPosition = "start";
        //categoryAxis.labelRotation = 90;
        graph.fillAlphas = 0.2;
        chart.write('chartdiv');
    });
</script>--%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" >
    <title></title>
    <script src="jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="amcharts/amcharts.js" type="text/javascript"></script>
    <script src="amcharts/serial.js" type="text/javascript"></script>
    <%--<script type="text/javascript">
        $('Button1').click(function () {
            $.ajax({
                type: "POST",
                url: "/JSONData.aspx.cs/GetJsonData",
                data: "[]",
                contentType: "application/json;chartset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
            });
            return false;
        });
    </script>--%>
    <script type="text/javascript">
        // code to draw the chart in javascript as given in the website "http://www.amcharts.com/tutorials/your-first-chart-with-amcharts/"
        $(AmCharts).ready(function () {
            var chart = new AmCharts.AmSerialChart();
            var j = '<%=GetJsonData()%>'; //this statement should get the json data from code behind page to the variable "j"
            chart.dataProvider = j; //the json data in the var j is given to the data provider.
            chart.categoryField = "PLANT_CODE";

            var graph = new AmCharts.AmGraph();
            graph.valueField = "Milkkg";
            graph.type = "column";
            chart.addGraph(graph);
            //var categoryAxis = chart.categoryAxis;
            //categoryAxis.autoGridCount = false;
            //categoryAxis.gridCount = chart.dataProvider.length;
            //categoryAxis.gridPosition = "start";
            //categoryAxis.labelRotation = 90;
            //graph.fillAlphas = 0.2;
            chart.write('chartdiv');
        });
    </script>
    <%--This code is to check whether the json data from the code behind file is getting generated or not. --%>
    <%--<script>
        var js = '<%=jsondata%>';
        $(document).ready(function () {
            alert(js);
        });
    </script>--%>
</head>
<body>
    <form>
    <div>
    
        <div id="Div2" style="width:1300px;height:500px"></div>
    </div>
    </form>
</body>
</html>





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">

    </asp:ToolkitScriptManager>

    
    <br />

    
    <table  align="center" width="50%" style="border: thin ridge #008080">
        <tr>
            <td class="style2" style="width: 55%; text-align: right;">
                From<asp:TextBox ID="txt_FromDate" runat="server" class="text1"
                                    Width="75px" ></asp:TextBox>
                         <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" 
                    runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" 
                    PopupPosition="TopRight">
                        </asp:CalendarExtender>                   	
            </td>
            <td class="style1" style="width: 55%">
                To<asp:TextBox ID="txt_ToDate" runat="server" Width="75px"  class="text1" ></asp:TextBox>                    	
                                 <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txt_ToDate"
                                PopupButtonID="txt_ToDate" Format="dd/MM/yyyy" PopupPosition="TopRight"  >
                                   </asp:CalendarExtender>
            </td>
        </tr>
        <tr align="center">
            <td align="CENTER" colspan="2" style="width: 55%; text-align: center">

    
                <asp:Label ID="Label4" runat="server" Font-Size="Medium" Text="Plant Name"></asp:Label>
                <asp:DropDownList ID="ddl_Plantname" runat="server" AutoPostBack="True" 
                     Font-Bold="True" Font-Size="Medium" Height="30px" 
                    onselectedindexchanged="ddl_Plantname_SelectedIndexChanged" Width="230px" 
                    CssClass="tb10">
                </asp:DropDownList>
                
    
                            <asp:DropDownList ID="ddl_Plantcode" runat="server" AutoPostBack="true" 
                                Height="16px" Visible="false" Width="29px">
                            </asp:DropDownList>

    
            </td>
        </tr>
        <tr align="center">
            <td align="CENTER" colspan="2" style="width: 55%; text-align: center">

    
                                <asp:Button ID="Button5" runat="server" CssClass="button" onclick="Button5_Click" 
                                    Text="Show" Font-Size="10px" Font-Bold="True" />
                            
                                <asp:Button ID="Button6" runat="server" CssClass="button" Font-Bold="True" 
                                           OnClientClick="return PrintPanel();" 
                          Font-Size="10px"  Text="Print" onclick="Button6_Click" />
                            
            </td>
        </tr>
        </table>
            <br />

    
    <br />

    
    <center>
            <br />
            <table width="100%"  class=text2>
            <tr  WIDTH="100%" align="center">
          <td align="center">
          <center>
           <asp:Panel id="pnlContents" align="center" runat = "server">
            <asp:GridView ID="GridView1" runat="server" 
                onrowdatabound="GridView1_RowDataBound" 
                onrowcreated="GridView1_RowCreated" 
                  CssClass="table table-striped table-bordered table-hover" >

                 <Columns>
									   <asp:TemplateField HeaderText="SNo.">
										   <ItemTemplate>
											   <%# Container.DataItemIndex + 1 %>
										   </ItemTemplate>
									   </asp:TemplateField>
								   </Columns>
                                    <HeaderStyle ForeColor="#660066" />
    </asp:GridView>
    </asp:Panel>
    </center>

                                <asp:Button ID="Button2" runat="server" CssClass="button" onclick="Button2_Click" 
                                    Text="Export" Font-Size="Smaller" />
    </td>
      </tr>
            <tr  WIDTH="100%" align="center">
          <td align="center">


          <!-- Styles -->
<style>
#chartdiv {
	width		: 75%;
	height		: 500px;
	font-size	: 11px;
}															
</style>

<!-- Resources -->
<script src="https://www.amcharts.com/lib/3/amcharts.js"></script>
<script src="https://www.amcharts.com/lib/3/serial.js"></script>
<script src="https://www.amcharts.com/lib/3/plugins/export/export.min.js"></script>
<link rel="stylesheet" href="https://www.amcharts.com/lib/3/plugins/export/export.css" type="text/css" media="all" />
<script src="https://www.amcharts.com/lib/3/themes/light.js"></script>

<!-- Chart code -->
<script>
    var chartData = [{
        "category": "Wine left in the barrel",
        "value1": 30,
        "value2": 70
    }];


    var chart = AmCharts.makeChart("chartdiv", {
        "theme": "light",
        "type": "serial",
        "depth3D": 100,
        "angle": 30,
        "autoMargins": false,
        "marginBottom": 100,
        "marginLeft": 350,
        "marginRight": 300,
        "dataProvider": chartData,
        "valueAxes": [{
            "stackType": "100%",
            "gridAlpha": 0
        }],
        "graphs": [{
            "type": "column",
            "topRadius": 1,
            "columnWidth": 1,
            "showOnAxis": true,
            "lineThickness": 2,
            "lineAlpha": 0.5,
            "lineColor": "#FFFFFF",
            "fillColors": "#8d003b",
            "fillAlphas": 0.8,
            "valueField": "value1"
        }, {
            "type": "column",
            "topRadius": 1,
            "columnWidth": 1,
            "showOnAxis": true,
            "lineThickness": 2,
            "lineAlpha": 0.5,
            "lineColor": "#cdcdcd",
            "fillColors": "#cdcdcd",
            "fillAlphas": 0.5,
            "valueField": "value2"
        }],

        "categoryField": "category",
        "categoryAxis": {
            "axisAlpha": 0,
            "labelOffset": 40,
            "gridAlpha": 0
        },
        "export": {
            "enabled": true
        }
    });
</script>

<!-- HTML -->
<div id="chartdiv"></div>

              </td>
      </tr>
    </table>
            <br />
    </center>
            <br />
            <br />          
            
            <body>
<form id="form1">
<div>
<div id="Div1" style="width:500px;height:400px"></div>
</div>
</form>
</body>

                  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
