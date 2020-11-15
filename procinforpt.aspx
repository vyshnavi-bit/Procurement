<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="procinforpt.aspx.cs" Inherits="procinforpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
<script type="text/javascript">
    function CallPrint(strid) {
        var divToPrint = document.getElementById(strid);
        var newWin = window.open('', 'Print-Window', 'width=400,height=400,top=100,left=100');
        newWin.document.open();
        newWin.document.write('<html><body   onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
        newWin.document.close();
    }
    </script>
    <script type="text/javascript">
        $(function () {
            get_procinfo_details();
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
        function get_procinfo_details() {
            var data = { 'operation': 'get_procinfo_details' };
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
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        function filldetails(msg) {
            var results = '<div  style="overflow:auto;border: 2px solid gray;"><table class="table table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="example2_info">';
            results += '<thead><tr><th scope="col" style="text-align: center !important; border: 1px solid gray;;">Plant Name</th><th scope="col" style="text-align: center !important; border: 1px solid gray;;">Present</th><th scope="col" style="text-align: center !important; border: 1px solid gray;;">last Week Same Day</th><th scope="col" style="text-align: center !important; border: 1px solid gray;;">last Month Same Day</th><th scope="col" style="text-align: center !important; border: 1px solid gray;;">last Year Same Day</th></tr></thead></tbody>';
            var k = 1;
            var l = 0;
            var presentcowmilktotal = 0;
            var weekcowmilktotal = 0;
            var monthcowmilktotal = 0;
            var yearcowmilktotal = 0;

            var presentbuffmilktotal = 0;
            var weekbuffmilktotal = 0;
            var monthbuffmilktotal = 0;
            var yearbuffmilktotal = 0;

            var prevmilktype = "";
            for (var i = 0; i < msg.length; i++) {
                var doe = msg[i].doe;
                document.getElementById('spndate').innerHTML = doe;
                var milktype = msg[i].Milktype;
                if (milktype == prevmilktype) {

                    var presentmilkqty = parseFloat(msg[i].present) || 0; 
                    var weekmilkqty = parseFloat(msg[i].Lastweek) || 0; 
                    var monthmilkqty = parseFloat(msg[i].lastmonth) || 0; 
                    var yearmilkqty = parseFloat(msg[i].lastyear) || 0; 

                    presentcowmilktotal += presentmilkqty;
                    weekbuffmilktotal += weekmilkqty;
                    monthcowmilktotal += monthmilkqty;
                    yearcowmilktotal += yearmilkqty;
                    results += '<tr>';
                    results += '<td data-title="brandstatus"  class="2" style="text-align: center !important; border: 1px solid gray;;"><span id="btn_poplate"  onclick="viewroute_details(this)"  name="submit"">' + msg[i].plantname + '</span></td>';
                    results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].present + '</td>';
                    results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].Lastweek + '</td>';
                    results += '<td data-title="brandstatus"  class="4" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].lastmonth + '</td>';
                    results += '<td data-title="brandstatus"  class="5" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].lastyear + '</td>';
                    results += '<td style="display:none" class="6">' + msg[i].plantcode + '</td></tr>';
                }
                else {
                    if (presentcowmilktotal > 0) {
                        results += '<tr style="background-color: cadetblue;color: white;">';
                        results += '<td data-title="brandstatus"  class="2" style="text-align: center !important; border: 1px solid gray;;"><span id="btn_poplate"  onclick="viewroute_details(this)"  name="submit"">Total</span></td>';
                        results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + presentcowmilktotal.toFixed(2) + '</td>';
                        results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + weekbuffmilktotal.toFixed(2) + '</td>';
                        results += '<td data-title="brandstatus"  class="4" style="text-align: center !important; border: 1px solid gray;;">' + monthcowmilktotal.toFixed(2) + '</td>';
                        results += '<td data-title="brandstatus"  class="5" style="text-align: center !important; border: 1px solid gray;;">' + yearcowmilktotal.toFixed(2) + '</td>';
                        results += '<td style="display:none" class="6"></td></tr>';
                        presentcowmilktotal = 0;
                        weekbuffmilktotal = 0;
                        monthcowmilktotal = 0;
                        yearcowmilktotal = 0;
                    }
                    prevmilktype = milktype
                    results += '<tr>';
                    results += '<td data-title="brandstatus"  class="2" style="text-align: center !important; border: 1px solid gray;;"><span id="btn_poplate"  onclick="viewroute_details(this)"  name="submit"">' + msg[i].plantname + '</span></td>';
                    results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].present + '</td>';
                    results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].Lastweek + '</td>';
                    results += '<td data-title="brandstatus"  class="4" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].lastmonth + '</td>';
                    results += '<td data-title="brandstatus"  class="5" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].lastyear + '</td>';
                    results += '<td style="display:none" class="6">' + msg[i].plantcode + '</td></tr>';
                    var presentmilkqty = parseFloat(msg[i].present) || 0;
                    var weekmilkqty = parseFloat(msg[i].Lastweek) || 0;
                    var monthmilkqty = parseFloat(msg[i].lastmonth) || 0;
                    var yearmilkqty = parseFloat(msg[i].lastyear) || 0; 
                    presentcowmilktotal += presentmilkqty;
                    weekbuffmilktotal += weekmilkqty;
                    monthcowmilktotal += monthmilkqty;
                    yearcowmilktotal += yearmilkqty;
                }
            }
            results += '<tr style="background-color: cadetblue;color: white;">';
            results += '<td data-title="brandstatus"  class="2" style="text-align: center !important; border: 1px solid gray;;"><span id="btn_poplate"  onclick="viewroute_details(this)"  name="submit"">"Total"</span></td>';
            results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + presentcowmilktotal.toFixed(2) + '</td>';
            results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + weekbuffmilktotal.toFixed(2) + '</td>';
            results += '<td data-title="brandstatus"  class="4" style="text-align: center !important; border: 1px solid gray;;">' + monthcowmilktotal.toFixed(2) + '</td>';
            results += '<td data-title="brandstatus"  class="5" style="text-align: center !important; border: 1px solid gray;;">' + yearcowmilktotal.toFixed(2) + '</td>';
            results += '<td style="display:none" class="6"></td></tr>';
            results += '</table></div>';
            $("#divprocinfo").html(results);
        }
        function viewroute_details(thisid) {
            var plantcode = $(thisid).parent().parent().children('.6').html();
            var data = { 'operation': 'get_procinforoute_details', 'plantcode': plantcode };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillroutewisedetails(msg);
                    }
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }

        function fillroutewisedetails(msg) {
            $("#divroutewiseprocinfo").css("display", "block");
            $("#divprocinfo").css("display", "none");
            $("#btnclose").css("display", "block");
            var results = '<div  style="overflow:auto; border: 2px solid gray;"><table id="tblroutewise" class="table table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="example2_info">';
            results += '<thead><tr style="border: 1px solid gray;"><th scope="col" style="text-align: center !important; border: 1px solid gray;; border: 1px solid gray;">Plant Name</th><th scope="col" style="text-align: center !important; border: 1px solid gray;;border: 1px solid gray;">Route Name</th><th scope="col" style="text-align: center !important; border: 1px solid gray;;border: 1px solid gray;">Present</th><th scope="col" style="text-align: center !important; border: 1px solid gray;;border: 1px solid gray;">last Week Same Day</th><th scope="col" style="text-align: center !important; border: 1px solid gray;;">last Month Same Day</th><th scope="col" style="text-align: center !important; border: 1px solid gray;;">last Year Same Day</th></tr></thead></tbody>';
            var k = 1;
            var l = 0;
            var Rpresentcowmilktotal = 0;
            var Rweekbuffmilktotal = 0;
            var Rmonthcowmilktotal = 0;
            var Ryearcowmilktotal = 0;

            for (var i = 0; i < msg.length; i++) {

                var presentmilkqty = parseFloat(msg[i].present) || 0;
                var weekmilkqty = parseFloat(msg[i].Lastweek) || 0;
                var monthmilkqty = parseFloat(msg[i].lastmonth) || 0;
                var yearmilkqty = parseFloat(msg[i].lastyear) || 0;
                Rpresentcowmilktotal += presentmilkqty;
                Rweekbuffmilktotal += weekmilkqty;
                Rmonthcowmilktotal += monthmilkqty;
                Ryearcowmilktotal += yearmilkqty;

                results += '<tr>';
                results += '<td data-title="brandstatus"  class="2" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].plantname + '</td>';
                results += '<td data-title="brandstatus"  class="6" style="text-align: center !important; border: 1px solid gray;;"><span id="btn_poplate"  onclick="viewroutewiseagent_details(this)"  name="submit"">' + msg[i].routename + '</span></td>';
                results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].present + '</td>';
                results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].Lastweek + '</td>';
                results += '<td data-title="brandstatus"  class="4" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].lastmonth + '</td>';
                results += '<td data-title="brandstatus"  class="5" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].lastyear + '</td>';
                results += '<td style="display:none"  class="7">' + msg[i].routeid + '</td>';
                results += '<td style="display:none" class="8">' + msg[i].plantcode + '</td></tr>';
            }
            results += '<tr style="background-color: cadetblue;color: white;">';
            results += '<td data-title="brandstatus"  class="2" style="text-align: center !important; border: 1px solid gray;;"></td>';
            results += '<td data-title="brandstatus"  class="6" style="text-align: center !important; border: 1px solid gray;;">Total</td>';
            results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + Rpresentcowmilktotal.toFixed(2) + '</td>';
            results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + Rweekbuffmilktotal.toFixed(2) + '</td>';
            results += '<td data-title="brandstatus"  class="4" style="text-align: center !important; border: 1px solid gray;;">' + Rmonthcowmilktotal.toFixed(2) + '</td>';
            results += '<td data-title="brandstatus"  class="5" style="text-align: center !important; border: 1px solid gray;;">' + Ryearcowmilktotal.toFixed(2) + '</td>';
            results += '</tr>';
            results += '</table></div>';
            $("#divroutewiseprocinfo").html(results);
        }

        function viewroutewiseagent_details(thisid) {
            var routeid = $(thisid).parent().parent().children('.7').html();
            var plantcode = $(thisid).parent().parent().children('.8').html();
            var data = { 'operation': 'viewroutewiseagent_details', 'plantcode': plantcode, 'routeid': routeid };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillroutewiseagentdetails(msg);
                    }
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }

        function fillroutewiseagentdetails(msg) {
            $("#divroutewiseprocinfo").css("display", "none");
            $("#divprocinfo").css("display", "none");
            $("#btnclose").css("display", "none");
            $("#divagentinfo").css("display", "block");
            $("#btnagentclose").css("display", "block");
            var results = '<div  style="overflow:auto; border: 2px solid gray;"><table id="tblroutewise" class="table table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="example2_info">';
            results += '<thead><tr style="border: 1px solid gray;"><th scope="col" style="text-align: center !important; border: 1px solid gray;; border: 1px solid gray;">Route Name</th><th scope="col" style="text-align: center !important; border: 1px solid gray;;border: 1px solid gray;">Agent Name</th><th scope="col" style="text-align: center !important; border: 1px solid gray;;border: 1px solid gray;">Present</th><th scope="col" style="text-align: center !important; border: 1px solid gray;;border: 1px solid gray;">last Week Same Day</th><th scope="col" style="text-align: center !important; border: 1px solid gray;;">last Month Same Day</th><th scope="col" style="text-align: center !important; border: 1px solid gray;;">last Year Same Day</th><th scope="col" style="text-align: center !important; border: 1px solid gray;;">Total Kms To Plant</th></tr></thead></tbody>';
            var k = 1;
            var l = 0;
            var RApresentcowmilktotal = 0;
            var RAweekbuffmilktotal = 0;
            var RAmonthcowmilktotal = 0;
            var RAyearcowmilktotal = 0;

            for (var i = 0; i < msg.length; i++) {

                var presentmilkqty = parseFloat(msg[i].present) || 0;
                var weekmilkqty = parseFloat(msg[i].Lastweek) || 0;
                var monthmilkqty = parseFloat(msg[i].lastmonth) || 0;
                var yearmilkqty = parseFloat(msg[i].lastyear) || 0;
                RApresentcowmilktotal += presentmilkqty;
                RAweekbuffmilktotal += weekmilkqty;
                RAmonthcowmilktotal += monthmilkqty;
                RAyearcowmilktotal += yearmilkqty;

                results += '<tr>';
                results += '<td data-title="brandstatus"  class="2" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].routename + '</td>';
                results += '<td data-title="brandstatus"  class="6" style="text-align: center !important; border: 1px solid gray;;"><span id="btn_poplate"  onclick="viewroutewiseagent_details(this)"  name="submit"">' + msg[i].agentname + '</span></td>';
                results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].present + '</td>';
                results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].Lastweek + '</td>';
                results += '<td data-title="brandstatus"  class="4" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].lastmonth + '</td>';
                results += '<td data-title="brandstatus"  class="5" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].lastyear + '</td>';
                results += '<td data-title="brandstatus"  class="5" style="text-align: center !important; border: 1px solid gray;;">' + msg[i].totalkms + '</td>';
                results += '</tr>';
            }
            results += '<tr style="background-color: cadetblue;color: white;">';
            results += '<td data-title="brandstatus"  class="2" style="text-align: center !important; border: 1px solid gray;;"></td>';
            results += '<td data-title="brandstatus"  class="6" style="text-align: center !important; border: 1px solid gray;;">Total</td>';
            results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + RApresentcowmilktotal.toFixed(2) + '</td>';
            results += '<td data-title="brandstatus"  class="3" style="text-align: center !important; border: 1px solid gray;;">' + RAweekbuffmilktotal.toFixed(2) + '</td>';
            results += '<td data-title="brandstatus"  class="4" style="text-align: center !important; border: 1px solid gray;;">' + RAmonthcowmilktotal.toFixed(2) + '</td>';
            results += '<td data-title="brandstatus"  class="5" style="text-align: center !important; border: 1px solid gray;;">' + RAyearcowmilktotal.toFixed(2) + '</td>';
            results += '</tr>';
            results += '</table></div>';
            $("#divagentinfo").html(results);
        }

        function btncloseclick() {
            $("#divroutewiseprocinfo").css("display", "none");
            $("#divprocinfo").css("display", "block");
            $("#btnclose").css("display", "none");
        }
        function btnagentcloseclick() {
            $("#divroutewiseprocinfo").css("display", "block");
            $("#divprocinfo").css("display", "none");
            $("#btnclose").css("display", "block");
            $("#divagentinfo").css("display", "none");
            $("#btnagentclose").css("display", "none");
        }
    </script>

</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
<div style="text-align:center;width:100%;">
<div id="divPrint" style="padding-top: 2%;">
    <span id="spndate"></span>
    <div id="divprocinfo"></div>
    <div id="divroutewiseprocinfo" style="display:none;"></div>
    <div id="divagentinfo" style="display:none;"></div>
   
</div>
<br />
 <input type="button" id="btnclose" class="btn btn-primary" style="display:none;" onclick="btncloseclick();" value="Close" />
 <input type="button" id="btnagentclose" class="btn btn-primary" style="display:none;" onclick="btnagentcloseclick();" value="Close" />
 <asp:Button ID="btnPrint" runat="Server" CssClass="btn btn-primary" OnClientClick="javascript:CallPrint('divPrint');" Text="Print" />
</div>
</asp:content>
<asp:content id="Content3" contentplaceholderid="ContentPlaceHolder2" runat="Server">
</asp:content>
