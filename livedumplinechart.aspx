<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="livedumplinechart.aspx.cs" Inherits="livedumplinechart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Kendo/jquery.min.js" type="text/javascript"></script>
    <script src="Kendo/kendo.all.min.js" type="text/javascript"></script>
    <link href="Kendo/kendo.common.min.css" rel="stylesheet" type="text/css" />
    <link href="Kendo/kendo.default.min.css" rel="stylesheet" type="text/css" />
   <script src="js/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            get_plant_details();
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd
            }
            if (mm < 10) {
                mm = '0' + mm
            }
            var hrs = today.getHours();
            var mnts = today.getMinutes();
            $('#txt_fromdate').val(yyyy + '-' + mm + '-' + dd + 'T' + hrs + ':' + mnts);
        });
        function callHandler(d, s, e) {
            $.ajax({
                url: 'DairyFleet.axd',
                data: d,
                type: 'GET',
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: true,
                cache: true,
                success: s,
                error: e
            });
        }
        function CallHandlerUsingJson(d, s, e) {
            d = JSON.stringify(d);
            d = d.replace(/&/g, '\uFF06');
            d = d.replace(/#/g, '\uFF03');
            d = d.replace(/\+/g, '\uFF0B');
            d = d.replace(/\=/g, '\uFF1D');
            $.ajax({
                type: "GET",
                url: "DairyFleet.axd?json=",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: d,
                async: true,
                cache: true,
                success: s,
                error: e
            });
        }
        function get_plant_details() {
            var data = { 'operation': 'get_plant_details' };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        filldetails(msg);
                    }
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            callHandler(data, s, e);
        }
        function filldetails(msg) {
            var data = document.getElementById('slct_plant');
            var length = data.options.length;
            document.getElementById('slct_plant').options.length = null;
            var opt = document.createElement('option');
            opt.innerHTML = "Select plantcode";
            opt.value = "Select plantcode";
            opt.setAttribute("selected", "selected");
            opt.setAttribute("disabled", "disabled");
            opt.setAttribute("class", "dispalynone");
            data.appendChild(opt);
            for (var i = 0; i < msg.length; i++) {
                if (msg[i].code != null) {
                    var option = document.createElement('option');
                    option.innerHTML = msg[i].name;
                    option.value = msg[i].code;
                    data.appendChild(option);
                }
            }
        }
        function generate_linechart() {
            var plantcode = document.getElementById('slct_plant').value;
            var fromdate = document.getElementById('txt_fromdate').value;
            var Sessions = document.getElementById('slct_sess').value;
            var data = { 'operation': 'get_plantwise_milkdetails', 'plantcode': plantcode, 'fromdate': fromdate, 'Sessions': Sessions };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        createChart(msg);
                    }
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            callHandler(data, s, e);
        }
        var datainXSeries = 0;
        var datainYSeries = 0;
        var newXarray = [];
        var newYarray = [];
        function createChart(databind) {
            var newYarray = [];
            var newXarray = [];
            var plant = "";
            for (var k = 0; k < databind.length; k++) {
                var BranchName = [];
                plant = databind[k].plant;
                var IndentDate = databind[k].Date;
                var DeliveryQty = databind[k].quantity;
                var Status = databind[k].status;
                newXarray = IndentDate.split(',');
                for (var i = 0; i < DeliveryQty.length; i++) {
                    newYarray.push({ 'data': DeliveryQty[i].split(','), 'name': Status[i]});
                }
            }
            //                var BranchName = [];
            //                var InwardDate = databind[0].Date;
            //                var inwardQty = databind[0].quantity;
            //                var Status = databind[0].Status;
            //                                newXarray = InwardDate.split(',');
            //                                for (var i = 0; i < inwardQty.length; i++) {
            //                                    newYarray.push({ 'data': inwardQty[i].split(','), 'name': Status[i] });
            //                                }
            $("#chart").kendoChart({
                title: {
                    text: "Live Dumping Milk Details - " + plant,
                    color: "#006600"
                },
                legend: {
                    position: "bottom"
                },
                chartArea: {
                    background: ""
                },
                seriesDefaults: {
                    type: "line",
                    style: "smooth"
                },
                series: newYarray,
                valueAxis: {
                    labels: {
                        format: "{0}%"
                    },
                    line: {
                        visible: false
                    },
                    axisCrossingValue: -10
                },
                categoryAxis: {
                    categories: newXarray,
                    //                        categories: [2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011],
                    majorGridLines: {
                        visible: false
                    },
                    labels: {
                        rotation: 65
                    }
                },
                tooltip: {
                    visible: true,
                    format: "{0}%",
                    template: "#= series.name #: #= value #"
                }
            });
        }
function btn_generate_onclick() {

}

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content">
        <div class="box box-info">
            <div class="box-body">
                <div id='Inwardsilo_fillform'>
                    <table>
                        <tr>
                            <td>
                                <label>
                                    Plant Code<span style="color: red;">*</span></label>
                                <select id="slct_plant" class="form-control">
                                    
                                </select>
                            </td>
                            <td>
                                <label>
                                    From Date<span style="color: red;">*</span></label>
                                <input id="txt_fromdate" class="form-control" type="date" name="vendorcode"
                                    placeholder="Enter fromDate">
                            </td>

                            <td>
                                 <label>
                                   Session<span style="color: red;">*</span></label>
                                <select id="slct_sess" class="form-control">
                                    <option value="AM">AM</option>
                                <option value="PM">PM</option>
                                </select>
                            </td>
                            <td>
                                <input id='btn_generate' type="button" class="btn btn-success" name="submit" value='Genarate'
                                    onclick="generate_linechart()" />
                            </td>

                             
                        </tr>
                    </table>
                    <br />
                </div>
            </div>
            <div id="example" class="k-content absConf">
        <div class="chart-wrapper" style="margin: auto;">
            <div id="chart" >
            </div>
        </div>
    </div>
        </div>
    </section>
</asp:Content>

