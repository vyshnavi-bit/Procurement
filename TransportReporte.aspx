<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TransportReporte.aspx.cs" Inherits="TransportReporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
      <meta http-equiv="content-type" content="text/plain; charset=UTF-8"/>
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
            $('#txt_date').val(mm + '-' + dd + '-' + yyyy);
            $('#txt_todate').val(mm + '-' + dd + '-' + yyyy);

        });
        function get_plant_details() {
            var data = { 'operation': 'get_plant_details1' };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillplanttype(msg);
                    }
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            callHandler(data, s, e);
        }
        function fillplanttype(msg) {
            var data = document.getElementById('slct_plantname');
            var length = data.options.length;
            document.getElementById('slct_plantname').options.length = null;
            var opt = document.createElement('option');
            opt.innerHTML = "Select Plant name";
            opt.value = "";
            opt.setAttribute("selected", "selected");
            opt.setAttribute("disabled", "disabled");
            opt.setAttribute("class", "dispalynone");
            data.appendChild(opt);
            for (var i = 0; i < msg.length; i++) {
                if (msg[i].plantname != null) {
                    var option = document.createElement('option');
                    option.innerHTML = msg[i].plantname;
                    option.value = msg[i].plantcode;
                    data.appendChild(option);
                }
            }
        }
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
        function vechiledetails() {
            var plantcode = document.getElementById('slct_plantname').value;
            var fromdate = document.getElementById('txt_date').value;
            var todate = document.getElementById('txt_todate').value;
            var data = { 'operation': 'get_gpsvechile_detailsreporte', 'plant_code': plantcode, 'fromdate': fromdate, 'todate': todate };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillgrid(msg);
                    }
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            callHandler(data, s, e);
        }
        function fillgrid(msg) {
            var results = '<div style="overflow:auto" ><table class="table table-bordered table-hover dataTable no-footer" border="2" role="grid" aria-describedby="example2_info" id="tbl_gps">';
            results += '<thead><tr><th scope="col"></th><th scope="col">Route Name</th><th scope="col">vech type</th><th scope="col">vech No</th><th scope="col">Tot ltr</th><th scope="col">ltr cost</th><th scope="col">Fix cost</th><th scope="col">Days</th><th scope="col">Km Rate</th><th scope="col">PerDay Km</th><th scope="col">Total Km</th><th scope="col">Gps Kms</th><th scope="col">Final Kms</th><th scope="col">Ded Amt</th><th scope="col">Tot Amt</th><th scope="col">Admin Amt</th><th scope="col">Descr</th><th scope="col">Bank Name</th><th scope="col">branch Name</th><th scope="col">A/c No</th><th scope="col">Ifsc</th><th scope="col">Accnter Name</th><th scope="col">Pan No</th></tr></thead></tbody>';
            var k = 1;
            var l = 0;
            var COLOR = ["#b3ffe6", "AntiqueWhite", "#daffff", "MistyRose", "Bisque"];
            for (var i = 0; i < msg.length; i++) {
                results += '<tr style="background-color:' + COLOR[l] + '"><td><input id="btn_poplate" type="button"  onclick="getme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
                //                results += '<td data-title="brandstatus"  class="2">' + msg[i].routeid + '</td>';
                results += '<td data-title="brandstatus" class="22">' + msg[i].Route_Name + '</td>';
                results += '<td data-title="brandstatus"  class="3">' + msg[i].vechiletype + '</td>';
                results += '<td data-title="brandstatus"  class="4">' + msg[i].vechileno + '</td>';
                results += '<td data-title="brandstatus" class="5">' + msg[i].totaltr + '</td>';
                results += '<td data-title="brandstatus"  class="6">' + msg[i].ltrcost + '</td>';
                results += '<td data-title="brandstatus" class="7">' + msg[i].fixedcost + '</td>';
                results += '<td data-title="brandstatus" class="8">' + msg[i].noofdays + '</td>';
                results += '<td data-title="brandstatus" class="11">' + msg[i].kmrate + '</td>';
                results += '<td data-title="brandstatus" class="12">' + msg[i].perdaykms + '</td>';
                results += '<td data-title="brandstatus" class="25">' + msg[i].Totalkms + '</td>';
                results += '<td data-title="brandstatus" class="10">' + msg[i].adminkms + '</td>';
                results += '<td data-title="brandstatus" class="19">' + msg[i].finalkms + '</td>';

                results += '<td data-title="brandstatus" class="13">' + msg[i].deductionamount + '</td>';
                results += '<td data-title="brandstatus" class="14">' + msg[i].TotalAmount + '</td>';
                results += '<td data-title="brandstatus" class="16">' + msg[i].AdminAmount + '</td>';

                results += '<td data-title="brandstatus" class="15">' + msg[i].DeductionDesc + '</td>';
                results += '<td data-title="brandstatus" class="23">' + msg[i].bmbankname + '</td>';
                results += '<td data-title="brandstatus" class="24">' + msg[i].branchname + '</td>';
                results += '<td data-title="brandstatus" class="17">' + msg[i].Accno + '</td>';
                results += '<td data-title="brandstatus" class="18">' + msg[i].Ifsc + '</td>';
                results += '<td data-title="brandstatus" class="20">' + msg[i].AccounterName + '</td>';
                results += '<td data-title="brandstatus" style="display:none" class="26">' + msg[i].Tid + '</td>';
                results += '<td data-title="brandstatus" style="display:none" class="27">' + msg[i].fromdate + '</td>';
                results += '<td data-title="brandstatus" style="display:none" class="28">' + msg[i].todate + '</td>';
                results += '<td data-title="brandstatus" class="21">' + msg[i].PancardNo + '</td></tr>';
                l = l + 1;
                if (l == 4) {
                    l = 0;
                }
            }
            results += '</table></div>';
            $("#div_Company").css("display", "block");
            $("#div_Company").html(results);
        }
        function getme(thisid) {
            var Tid = $(thisid).parent().parent().children('.26').html();
            var gpskms = $(thisid).parent().parent().children('.10').html();
            var perdaykms = $(thisid).parent().parent().children('.12').html();
            var kmrate = $(thisid).parent().parent().children('.11').html();
            var Totalkms = $(thisid).parent().parent().children('.25').html();
            var finalkms = $(thisid).parent().parent().children('.19').html();
            var AdminAmount = $(thisid).parent().parent().children('.16').html();
            var fromdate = $(thisid).parent().parent().children('.27').html();
            var todate = $(thisid).parent().parent().children('.28').html();


            $("#lbl_kmrate").css("display", "block"); 
            $("#km_rate").css("display", "block");
            $("#lbl_diffrence").css("display", "block");

            $("#lbl_adminkms").css("display", "block");
            $("#txt_adminkms").css("display", "block");

            $("#lbl_perday").css("display", "block");
            $("#txt_perkm").css("display", "block");

            $("#lbl_total").css("display", "block");
            $("#txt_total").css("display", "block");
            $("#lbl_totaldays").css("display", "block");

            $("#lbl_gps").css("display", "block");
            $("#txt_gps").css("display", "block");

            $("#lbl_admin").css("display", "block"); 
            $("#txt_adminamt").css("display", "block");
            $("#btn_update").css("display", "block"); 
            document.getElementById('spn_diffrence').innerHTML = parseFloat(Totalkms) - parseFloat(gpskms);
            var fromdate = new Date(fromdate);
            var todate = new Date(todate);
            var timeDiff = Math.abs(todate.getTime() - fromdate.getTime() + 1);
            var age1 = Math.ceil(timeDiff / (1000 * 3600 * 24));
            var totaldays = parseFloat(age1).toFixed(0);
            document.getElementById('txt_totaldays').innerHTML = totaldays;
            document.getElementById('txt_tid').value = Tid;
            document.getElementById('km_rate').value = kmrate;
            document.getElementById('txt_perkm').value = perdaykms;
            document.getElementById('txt_total').value = Totalkms;
            document.getElementById('txt_gps').value = gpskms;
            document.getElementById('txt_adminkms').value = finalkms;
            document.getElementById('txt_adminamt').value = AdminAmount;

        }
        function updatecalc() {
            var Tid = document.getElementById('txt_tid').value;
            var kmrate = document.getElementById('km_rate').value;
            var perdaykms = document.getElementById('txt_perkm').value;
            var Totalkms = document.getElementById('txt_total').value;
            var gpskms = document.getElementById('txt_gps').value;
            var finalkms = document.getElementById('txt_adminkms').value;
            var AdminAmount = document.getElementById('txt_adminamt').value;
            var data = { 'operation': 'updateapproval', 'AdminAmount': AdminAmount, 'finalkms': finalkms, 'gpskms': gpskms, 'Totalkms': Totalkms, 'perdaykms': perdaykms, 'kmrate': kmrate, 'Tid': Tid };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        alert(msg);
                        $("#lbl_kmrate").css("display", "none"); 
                        $("#km_rate").css("display", "none");
                        $("#lbl_adminkms").css("display", "none");
                        $("#txt_adminkms").css("display", "none");

                        $("#lbl_perday").css("display", "none");
                        $("#txt_perkm").css("display", "none");
                        $("#lbl_diffrence").css("display", "none");

                        $("#lbl_total").css("display", "none");
                        $("#txt_total").css("display", "none");
                        document.getElementById('spn_diffrence').innerHTML = "";
                        document.getElementById('txt_totaldays').innerHTML = "";

                        $("#lbl_gps").css("display", "none");
                        $("#txt_gps").css("display", "none");
                        $("#lbl_totaldays").css("display", "none");
                        $("#lbl_admin").css("display", "none");
                        $("#txt_adminamt").css("display", "none");
                        $("#btn_update").css("display", "none");
                        $("#div_Company").css("display", "none");
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
        function admincal() {
            var kms = document.getElementById('txt_adminkms').value;
            var rate = document.getElementById('km_rate').value;
            var totalamount = parseFloat(kms) * parseFloat(rate);
            document.getElementById('txt_adminamt').value = totalamount;
        }
        
        var tableToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
    , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
    , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
    , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                window.location.href = uri + base64(format(template, ctx))
            }
        })()
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <section class="content">
        <!-- Small boxes (Stat box) -->
        <div class="row">
            <section class="content-header">
                <h1>
                  Transport Reporte
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Operation</a></li>
                    <li><a href="#">Masters</a></li>
                </ol>
            </section>
    <section class="content">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <i style="padding-right: 5px;" class="fa fa-cog"></i> Transport Distance reporte
                </h3>
            </div>
            <div class="box-body" >
                <div id='Company_fillform' width=50%>
                    <table align="center" width=40%>
                        <tr id="txt_company">
                            <td>
                                <label>
                                    <b>Plant Name:</b></label>
                            </td>
                            <td  >
                                <select id="slct_plantname"  name=" Plant Code" class="form-control"></select>
                            </td>

                               <td >
                               <label id="lbl_kmrate"  style="display:none">
                                    <b>Km Rate:</b></label>
                                <input style="display:none" id="km_rate"   type="text" class="form-control"  placeholder="Enter Km rtae"/>
                               <label id="lbl_perday"  style="display:none" >
                                    <b>perDay Km:</b></label>
                                <input  style="display:none" id="txt_perkm"   type="text" class="form-control"  placeholder="Enter perDay  Km"/>
                               <label id="lbl_totaldays"   style="display:none" >
                                    <b> Total Days:</b></label>
                            <span id="txt_totaldays"></span>
                            </td>
                            </tr>

                             <tr >
                                   <td >
                               <label  >
                                    <b>from date:</b></label>
                            </td>
                            <td style="height: 40px;">
                                <input id="txt_date"   type="date"  class="form-control"  />
                            </td>

                                   <td >
                               <label id="lbl_total"  style="display:none">
                                    <b>Total Km:</b></label>
                                <input  style="display:none" id="txt_total"   type="text" class="form-control"  placeholder="Enter Total Km"/>
                               <label id="lbl_gps"   style="display:none" >
                                    <b> Gps Km:</b></label>
                                <input style="display:none" id="txt_gps"   type="text" class="form-control"  placeholder="Enter Gps Km" />
                               <label id="lbl_diffrence"   style="display:none" >
                                    <b> Diffrence Km:</b></label>
                            <span id="spn_diffrence"></span>
                            </td>

                        </tr>
                          <tr >
                                   <td >
                               <label  >
                                    <b>To date:</b></label>
                            </td>
                            <td style="height: 40px;">
                                <input id="txt_todate"   type="date" class="form-control"  />
                            </td>
                              <td >
                               <label id="lbl_adminkms"  style="display:none" >
                                    <b>Admin Kms:</b></label>
                                <input style="display:none" id="txt_adminkms"   type="text" class="form-control"  placeholder="Enter Admin Km" onchange="admincal();"/>
                               <label id="lbl_admin"  style="display:none" >
                                    <b>Admin amt:</b></label>
                                <input style="display:none" id="txt_adminamt"   type="text" class="form-control"  placeholder="Enter Admin Km"/>
                               <label id="lbl_tid"  style="display:none">
                                    <b>Tid:</b></label>
                                <input style="display:none" id="txt_tid"   type="text" class="form-control"  placeholder="Enter Tid"/>
                            </td>
                        </tr>
                        </table>
                        <table align="center">
                        <tr>
                            <td colspan="2" align="center" style="height: 40px;">
                                <input id="btn_save" type="button" class="btn btn-primary" name="submit" value='Show' onclick="vechiledetails()" />&nbsp&nbsp
                            </td>

                            <td colspan="2" align="center" style="height: 40px;">
                                <input style="display:none" id="btn_update" type="button" class="btn btn-primary" name="submit" value='Update' onclick="updatecalc()" />&nbsp
                            </td>

                             <td>
                                         <button type="button" id="Button2" class="btn btn-success"   onclick ="javascript:CallPrint('divPrint');"><i class="fa fa-print"></i> Print</button>&nbsp
                                        </td>
                            <td>
                                       <%-- <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/exporttoxl.aspx">Export to XL</asp:HyperLink>--%>
                                         <input type="button" class="btntogglecls" style="height: 28px; opacity: 1.0; width: 100%;
                                        background-color: #d5d5d5; color: Blue;" onclick="tableToExcel('tbl_gps', 'W3C Example Table')"
                                        value="Export to Excel">
                                    </td>

                        </tr>
                     </table>
            </div>
            <div id="divPrint">
             <div id="div_Company" style="overflow-x:scroll">
                </div>
            <div  style="display:none">
                             <div id ="Company_fillform"></div>
                                   <br />
                                    <label id="lblTitle"  Font-Bold="true" Font-Size="20px" ForeColor="#0252aa"
                                                    ></label>
                                                <br />
                                                <label id="lblAddress"  Font-Bold="true" Font-Size="12px" ForeColor="#0252aa"
                                                    ></label>
                                                <br />
                                                 <label id="lblHeading"  Font-Bold="true" Font-Size="15px" ForeColor="#0252aa"
                                                    ></label>
                                                    <br />
                                        <table style="width: 100%;">
                                        <tr>
                                        </tr>
                                            <tr>
                                                <td style="width: 25%;">
                                                    <span style="font-weight: bold; font-size: 15px;">Prepared By</span>
                                                </td>
                                                <td style="width: 25%;">
                                                    <span style="font-weight: bold; font-size: 15px;">Audit By</span>
                                                </td>
                                                <td style="width: 25%;">
                                                    <span style="font-weight: bold; font-size: 15px;">A.O</span>
                                                </td>
                                                <td style="width: 25%;">
                                                    <span style="font-weight: bold; font-size: 15px;">GM</span>
                                                </td>
                                                <td style="width: 25%;">
                                                    <span style="font-weight: bold; font-size: 15px;">Director</span>
                                                </td>
                                            </tr>
                                        </table>
                                        </div>
             </div>
                                        <br/>
               <div align="center" id="printbtn" style="display:none">
           </div>
       </div>
         </section>
</asp:Content>


