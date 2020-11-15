<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="JSONData.aspx.cs" Inherits="JSONData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1"  >
    <title></title>

</head>
<body>
    <form id="form1" >
    <div>
    <canvas id="Chart" width="600" height="500"></canvas>
    </div>
    </form>
    
       
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
   
     <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/1.0.2/Chart.js"></script>

    <script>
        $(function () {
            var ctx = document.getElementById("Chart").getContext('2d');
            $.ajax({

                url: "Default.aspx/getChartData",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var chartLabel = eval(response.d[0]); //Labels
                    var chartData = eval(response.d[1]); //Data
                    var barData = {
                        labels: chartLabel,
                        datasets: [
                            {
                                label: 'July Sales',
                                fillColor: "rgba(225,225,225,0.2)",
                                strokeColor: "Blue",
                                pointColor: "rgba(220,220,220,1)",
                                pointStrokeColor: "Green",
                                pointHighlightFill: "#fff",
                                pointHighlightStroke: "rgba(220,220,220,1)",
                                data: chartData
                            }
                        ]
                    };
                    var skillsChart = new Chart(ctx).Line(barData);
                }

            });
        }
        );

    </script>
   
</body>
</html>
<
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div>
        <asp:Button ID="Button1" runat="server" Text="Show JSON data" OnClick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Draw Chart" />
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>--%>
        <div id="chartdiv" style="width:1300px;height:500px"></div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

