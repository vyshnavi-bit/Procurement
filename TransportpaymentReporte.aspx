<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TransportpaymentReporte.aspx.cs" Inherits="TransportpaymentReporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  <link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
      

    
    <script type="text/javascript">
        $(function () {
            var role_id1 = '<%= Session["Role"] %>';
            var role_id = parseInt(role_id1.toString());
            if (role_id <= 2) {
                document.getElementById('txt_vts').readOnly = true;
                document.getElementById('txt_admin').readOnly = true;
            }
            else {
                document.getElementById('txt_vts').readOnly = false;
                document.getElementById('txt_admin').readOnly = false;
            }
            get_bank_details();
            get_plant_details();
            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1;
            var yyyy = today.getFullYear();
            if (dd < 10) {
                dd = '0' + dd
            }
            if (mm < 10) {
                mm = '0' + mm
            }
            var hrs = today.getHours();
            var mnts = today.getMinutes();
            $('#txt_fromdate').val(mm + '/' + dd + '/' + yyyy);
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
            var data = document.getElementById('slct_plant');
            var length = data.options.length;
            document.getElementById('slct_plant').options.length = null;
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
        function get_bank_details() {
            var data = { 'operation': 'get_bank_details' };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillbanktype(msg);
                    }
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            callHandler(data, s, e);
        }
        function fillbanktype(msg) {
            var data = document.getElementById('slct_bank');
            var length = data.options.length;
            document.getElementById('slct_bank').options.length = null;
            var opt = document.createElement('option');
            opt.innerHTML = "Select Bank name";
            opt.value = "";
            opt.setAttribute("selected", "selected");
            opt.setAttribute("disabled", "disabled");
            opt.setAttribute("class", "dispalynone");
            data.appendChild(opt);
            for (var i = 0; i < msg.length; i++) {
                    var option = document.createElement('option');
                    option.innerHTML = msg[i].name;
                    option.value = msg[i].code;
                    data.appendChild(option);
                }
            }
        var route_underplant = [];
        function get_route_details() {
            var plantcode = document.getElementById('slct_plant').value;
            var data = { 'operation': 'get_transportroute_details_plant', 'plantcode': plantcode };
            var s = function (msg) {
                if (msg) {
                   
                        fillroutetype(msg);
                        route_underplant = msg;
                        get_transport_payment_details();
                  

                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            callHandler(data, s, e);
        }
        function fillroutetype(msg) {
            var plantcode = document.getElementById('slct_plant').value;
            var data = document.getElementById('slct_route');
            var length = data.options.length;
            document.getElementById('slct_route').options.length = null;
            var opt = document.createElement('option');
            opt.innerHTML = "Select Route name";
            opt.value = "";
            opt.setAttribute("selected", "selected");
            opt.setAttribute("disabled", "disabled");
            opt.setAttribute("class", "dispalynone");
            data.appendChild(opt);
            for (var i = 0; i < msg.length; i++) {
              if (plantcode == msg[i].plantcode)
                    {
                    var option = document.createElement('option');
                    option.innerHTML = msg[i].routename;
                    option.value = msg[i].routeid;
                    data.appendChild(option);
                }
              }
            }

        function savetransportpaymentdetails() {
            var fromdate = document.getElementById('txt_fromdate').value;
            var todate = document.getElementById('txt_todate').value;
            var plantname = document.getElementById('slct_plant').value;
            var routename = document.getElementById('slct_route').value;
            var bankname = document.getElementById('slct_bank').value;
            var vechiletype = document.getElementById('txt_vechtype').value;
            var vechioleno = document.getElementById('txt_vechile').value;
            var mode = document.getElementById('txt_mode').value;
            var totalliters = document.getElementById('txt_tltr').value;
            var totalcost = document.getElementById('txt_lcost').value;
            var fixedrate = document.getElementById('txt_fixed').value;
            var noofdays = document.getElementById('txt_days').value;
            var perdaykms = document.getElementById('txt_kms').value;
            var vtskms = document.getElementById('txt_vts').value;
            var kmrate = document.getElementById('txt_rate').value;
            var adminkms = document.getElementById('txt_admin').value;
            var totalamount = document.getElementById('txt_amount').value;
            var adminamount = document.getElementById('txt_amtadmin').value;
            var accountno = document.getElementById('txt_account').value;
            var accountholdername = document.getElementById('txt_name').value;
            var ifcscode = document.getElementById('txt_ifcs').value;
            var pancardno = document.getElementById('txt_pan').value;
            var cashdeposite = document.getElementById('txt_deposite').value;
            var deductionamount = document.getElementById('txt_dedamt').value;
            var descrption = document.getElementById('txt_desc').value; 
            var branchname = document.getElementById('txt_branch').value;
            var Totalkms = document.getElementById('txt_totalkms').value;


            var Tid = document.getElementById('lbl_sno').value;
            var btnval = document.getElementById('btn_save').value;

            if (fromdate == "") {
                alert("Enter fromdate");
                return false;
            }
            if (todate == "") {
                alert("Enter todate");
                return false;
            }
            if (plantname == "") {
                alert("Enter plantname");
                return false;
            }
            if (routename == "") {
                alert("Enter routename");
                return false;
            }
            if (bankname == "") {
                alert("Enter bankname");
                return false;
            }
            if (accountno == "") {
                alert("Enter accountno");
                return false;
            }
            if (accountholdername == "") {
                alert("Enter accountholdername");
                return false;
            }
            if (branchname == "") {
                alert("Enter branchname");
                return false;
            }
            if (ifcscode == "") {
                alert("Enter ifcscode");
                return false;
            }
             if (pancardno == "") {
                alert("Enter pancardno");
                return false;
            }
           
            if (vechiletype == "") {
                alert("Enter vechiletype");
                return false;
            }
            if (vechioleno == "") {
                alert("Select vechioleno");
                return false;
            }

            if (mode == "" || mode == "Select") {
                alert("Enter mode");
                return false;
            }

            if (mode == "0") {
                fixedrate = 0;
                adminkms = 0;
                vtskms = 0;
                perdaykms = 0;
                noofdays = 0;
                kmrate = 0;
            }
            else if (mode == "1") {
                totalliters = 0;
                adminkms = 0;
                vtskms = 0;
                perdaykms = 0;
                totalcost = 0;
                kmrate = 0;
            }
            else if (mode == "2") {
                totalliters = 0;
                fixedrate = 0;
                noofdays = 0;
                totalcost = 0;
            }

            var data = { 'operation': 'savetransportpaymentdetails', 'Totalkms': Totalkms, 'branchname': branchname, 'descrption': descrption, 'deductionamount': deductionamount, 'cashdeposite': cashdeposite, 'pancardno': pancardno, 'ifcscode': ifcscode, 'accountholdername': accountholdername, 'accountno': accountno, 'adminamount': adminamount, 'totalamount': totalamount, 'adminkms': adminkms, 'kmrate': kmrate, 'vtskms': vtskms, 'perdaykms': perdaykms, 'vtskms': vtskms, 'perdaykms': perdaykms, 'noofdays': noofdays, 'fixedrate': fixedrate, 'totalcost': totalcost, 'totalliters': totalliters, 'mode': mode, 'vechioleno': vechioleno, 'vechiletype': vechiletype, 'bankname': bankname, 'routename': routename, 'plantname': plantname, 'todate': todate, 'fromdate': fromdate, 'Tid': Tid, 'btnVal': btnval };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        alert(msg);
                        forclearall();
                        get_transport_payment_details();
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
        function forclearall() {
            document.getElementById('lbl_sno').value = "";
            document.getElementById('txt_fromdate').value = "";
            document.getElementById('txt_todate').value = "";
            document.getElementById('slct_plant').value = "";
            document.getElementById('slct_route').value = "";
            document.getElementById('slct_bank').value = "";
            document.getElementById('txt_vechtype').value = "";
            document.getElementById('txt_vechile').value = "";
            document.getElementById('txt_mode').selectedIndex = "0";
            document.getElementById('txt_tltr').value = "";
            document.getElementById('txt_lcost').value = "";
            document.getElementById('txt_fixed').value = "";
            document.getElementById('txt_days').value = "";
            document.getElementById('txt_kms').value = "";
            document.getElementById('txt_vts').value = "";
            document.getElementById('txt_rate').value = "";
            document.getElementById('txt_admin').value = "";
            document.getElementById('txt_amount').value = "";
            document.getElementById('txt_amtadmin').value = "";
            document.getElementById('txt_branch').value = "";
            document.getElementById('txt_totalkms').value = "";

            document.getElementById('txt_account').value = "";
            document.getElementById('txt_ifcs').value = "";
            document.getElementById('txt_name').value = "";
            document.getElementById('txt_pan').value = "";
            document.getElementById('txt_deposite').value = "";
            document.getElementById('txt_desc').value = "";
            document.getElementById('txt_dedamt').value = "";


            document.getElementById('btn_save').value = "save";
            get_transport_payment_details();
        }

        function get_transport_payment_details() {
            var plantcode = document.getElementById('slct_plant').value;
            var data = { 'operation': 'get_transport_payment_details', 'plantcode': plantcode };
            var s = function (msg) {
                if (msg) {
                   
                        fillAddress(msg);
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        function fillAddress(msg) {
            var results = '<div  style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="example2_info">';
            results += '<thead><tr><th scope="col"></th><th scope="col">fdate</th><th scope="col">tdate</th><th scope="col">plantcode</th><th scope="col">routename</th><th scope="col">bankname</th><th scope="col">Branchname</th><th scope="col">acnt no</th><th scope="col">A/c holdername</th><th scope="col">Ifsc</th><th scope="col">panno</th><th scope="col">cashdeposite</th><th scope="col">deduction amt</th><th scope="col">descrption</th><th scope="col">vechiletype</th><th scope="col">vechileno</th><th scope="col">mode</th><th scope="col">totliters</th><th scope="col">totcost</th><th scope="col">fixedrate</th><th scope="col">noofdays</th><th scope="col">perdaykms</th><th scope="col">total Kms</th><th scope="col">vtskms</th><th scope="col">adminkms</th><th scope="col">Km rate</th><th scope="col">totamount</th><th scope="col">adminamount</th></tr></thead></tbody>';
            var k = 1;
            var l = 0;
            var COLOR = ["#b3ffe6", "AntiqueWhite", "#daffff", "MistyRose", "Bisque"];
            for (var i = 0; i < msg.length; i++) {
                results += '<tr style="background-color:' + COLOR[l] + '"><td><input id="btn_poplate" type="button"  onclick="getme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
                results += '<td data-title="brandstatus"  class="1">' + msg[i].fromdate + '</td>';
                results += '<td data-title="brandstatus"  class="2">' + msg[i].todate + '</td>';
                results += '<td data-title="brandstatus"  class="3">' + msg[i].plantname + '</td>';
                results += '<td data-title="brandstatus" class="4">' + msg[i].routename + '</td>';
                results += '<td data-title="brandstatus" class="5">' + msg[i].bankname + '</td>';
                results += '<td data-title="brandstatus" class="29">' + msg[i].branchname + '</td>';
                results += '<td data-title="brandstatus" class="27">' + msg[i].accountno + '</td>';
                results += '<td data-title="brandstatus" class="21">' + msg[i].accountholdername + '</td>';
                results += '<td data-title="brandstatus" class="28">' + msg[i].ifcscode + '</td>';
                results += '<td data-title="brandstatus" class="22">' + msg[i].pancardno + '</td>';
                results += '<td data-title="brandstatus" class="23">' + msg[i].cashdeposite + '</td>';
                results += '<td data-title="brandstatus" class="26">' + msg[i].deductionamount + '</td>';
                results += '<td data-title="brandstatus" class="25">' + msg[i].descrption + '</td>';
                results += '<td data-title="brandstatus" class="6">' + msg[i].vechiletype + '</td>';
                results += '<td data-title="brandstatus" class="7">' + msg[i].vechioleno + '</td>';
                results += '<td data-title="brandstatus"  class="8">' + msg[i].mode + '</td>';
                results += '<td data-title="brandstatus" class="9">' + msg[i].totalliters + '</td>';
                results += '<td data-title="brandstatus"  class="10">' + msg[i].totalcost + '</td>';
                results += '<td  data-title="brandstatus"  class="11">' + msg[i].fixedrate + '</td>';
                results += '<td data-title="brandstatus" class="12">' + msg[i].noofdays + '</td>';
                results += '<td data-title="brandstatus" class="13">' + msg[i].perdaykms + '</td>';
                results += '<td data-title="brandstatus" class="30">' + msg[i].Totalkms + '</td>';
                results += '<td data-title="brandstatus" class="14">' + msg[i].vtskms + '</td>';
                results += '<td data-title="brandstatus" class="15">' + msg[i].adminkms + '</td>';

                results += '<td data-title="brandstatus" class="19">' + msg[i].kmrate + '</td>';
                results += '<td data-title="brandstatus" class="16">' + msg[i].totalamount + '</td>';
                results += '<td data-title="brandstatus" class="17">' + msg[i].adminamount + '</td>';

               



                results += '<td style="display:none" class="20">' + msg[i].status + '</td>';
                results += '<td style="display:none" class="18">' + msg[i].Tid + '</td></tr>';
                l = l + 1;
                if (l == 4) {
                    l = 0;
                }
            }
            results += '</table></div>';
            $("#div_user").html(results);
        }
        function getme(thisid) {
            var Tid = $(thisid).parent().parent().children('.18').html();
            var fromdate = $(thisid).parent().parent().children('.1').html();
            var todate = $(thisid).parent().parent().children('.2').html();
            var plantname = $(thisid).parent().parent().children('.3').html();
            fillroutetype(route_underplant);
            var routename = $(thisid).parent().parent().children('.4').html();
            var bankname = $(thisid).parent().parent().children('.5').html();
            var vechiletype = $(thisid).parent().parent().children('.6').html();
            var vechileno = $(thisid).parent().parent().children('.7').html();
            var mode = $(thisid).parent().parent().children('.8').html();
            var totalliters = $(thisid).parent().parent().children('.9').html();
            var totalcost = $(thisid).parent().parent().children('.10').html();
            var fixedrate = $(thisid).parent().parent().children('.11').html();
            var noofdays = $(thisid).parent().parent().children('.12').html();
            var perdaykms = $(thisid).parent().parent().children('.13').html();
            var vtskms = $(thisid).parent().parent().children('.14').html();
            var adminkms = $(thisid).parent().parent().children('.15').html();
            var kmrate = $(thisid).parent().parent().children('.19').html();
            var totalamount = $(thisid).parent().parent().children('.16').html();


            var accountholdername = $(thisid).parent().parent().children('.21').html();
            var pancardno = $(thisid).parent().parent().children('.22').html();
            var cashdeposite = $(thisid).parent().parent().children('.23').html();
            var descrption = $(thisid).parent().parent().children('.25').html();
            var deductionamount = $(thisid).parent().parent().children('.26').html();
            var accountno = $(thisid).parent().parent().children('.27').html();
            var ifcscode = $(thisid).parent().parent().children('.28').html();
            var branchname = $(thisid).parent().parent().children('.29').html();
            var Totalkms = $(thisid).parent().parent().children('.30').html();




            var adminamount = $(thisid).parent().parent().children('.17').html();
            var status = $(thisid).parent().parent().children('.20').html();
            var status1 = parseInt(status);

            var role_id1 = '<%= Session["Role"] %>';
            var role_id = parseInt(role_id1.toString());
            if (role_id > 2) {
                document.getElementById('lbl_sno').value = Tid;
                document.getElementById('txt_fromdate').value = fromdate;
                document.getElementById('txt_todate').value = todate;
                document.getElementById('slct_plant').value = plantname;
                document.getElementById('slct_route').value = routename;
                document.getElementById('slct_bank').value = bankname;
                document.getElementById('txt_vechtype').value = vechiletype;
                document.getElementById('txt_vechile').value = vechileno;
                document.getElementById('txt_mode').value = mode;
                document.getElementById('txt_tltr').value = totalliters;
                document.getElementById('txt_lcost').value = totalcost;
                document.getElementById('txt_fixed').value = fixedrate;
                document.getElementById('txt_days').value = noofdays;
                document.getElementById('txt_kms').value = perdaykms;

                document.getElementById('txt_name').value = accountholdername;
                document.getElementById('txt_pan').value = pancardno;
                document.getElementById('txt_deposite').value = cashdeposite;
                document.getElementById('txt_desc').value = descrption;
                document.getElementById('txt_dedamt').value = deductionamount;
                document.getElementById('txt_account').value = accountno;
                document.getElementById('txt_ifcs').value = ifcscode;
                document.getElementById('txt_branch').value = branchname;
                document.getElementById('txt_totalkms').value = Totalkms;


                document.getElementById('txt_vts').value = vtskms;
                document.getElementById('txt_admin').value = adminkms;
                document.getElementById('txt_vts').readOnly = false;
                document.getElementById('txt_admin').readOnly = false;

                document.getElementById('txt_rate').value = kmrate;
                document.getElementById('txt_amount').value = totalamount;
                document.getElementById('txt_amtadmin').value = adminamount;
                document.getElementById('btn_save').value = "Modify";
            }
            else {
                if (status1 != 1) {
                    document.getElementById('lbl_sno').value = Tid;
                    document.getElementById('txt_fromdate').value = fromdate;
                    document.getElementById('txt_todate').value = todate;
                    document.getElementById('slct_plant').value = plantname;
                    document.getElementById('slct_route').value = routename;
                    document.getElementById('slct_bank').value = bankname;
                    document.getElementById('txt_vechtype').value = vechiletype;
                    document.getElementById('txt_vechile').value = vechileno;
                    document.getElementById('txt_mode').value = mode;
                    document.getElementById('txt_tltr').value = totalliters;
                    document.getElementById('txt_lcost').value = totalcost;
                    document.getElementById('txt_fixed').value = fixedrate;
                    document.getElementById('txt_days').value = noofdays;
                    document.getElementById('txt_kms').value = perdaykms;


                    document.getElementById('txt_name').value = accountholdername;
                    document.getElementById('txt_pan').value = pancardno;
                    document.getElementById('txt_deposite').value = cashdeposite ;
                    document.getElementById('txt_desc').value = descrption;
                    document.getElementById('txt_dedamt').value = deductionamount;
                    document.getElementById('txt_account').value = accountno; 
                    document.getElementById('txt_ifcs').value = ifcscode;
                    document.getElementById('txt_branch').value = branchname;
                    document.getElementById('txt_totalkms').value = Totalkms;


                    document.getElementById('txt_vts').value = vtskms;
                    document.getElementById('txt_admin').value = adminkms;
                    document.getElementById('txt_vts').readOnly = true;
                    document.getElementById('txt_admin').readOnly = true;
                    document.getElementById('txt_rate').value = kmrate;
                    document.getElementById('txt_amount').value = totalamount;
                    document.getElementById('txt_amtadmin').value = adminamount;
                    document.getElementById('btn_save').value = "Modify";
                }
                else {
                    alert("you can't edit this record");
                    return false;
                }
            }

        }
        function displaymodes() {
            var mode = document.getElementById('txt_mode').value;
            if (mode == "0") {
                document.getElementById('txt_fixed').readOnly = true;
                document.getElementById('txt_days').readOnly = true;
                document.getElementById('txt_kms').readOnly = true;
                document.getElementById('txt_vts').readOnly = true;
                document.getElementById('txt_admin').readOnly = true;
                document.getElementById('txt_tltr').readOnly = false;
                document.getElementById('txt_lcost').readOnly = false;
                document.getElementById('txt_rate').readOnly = true;

            }
            else if (mode == "1") {
                document.getElementById('txt_tltr').readOnly = true;
                document.getElementById('txt_lcost').readOnly = true;
                document.getElementById('txt_kms').readOnly = true;
                document.getElementById('txt_vts').readOnly = true;
                document.getElementById('txt_admin').readOnly = true;
                document.getElementById('txt_fixed').readOnly = false;
                document.getElementById('txt_days').readOnly = false;
                document.getElementById('txt_rate').readOnly = true;

            }
            else if (mode == "2") {
                document.getElementById('txt_tltr').readOnly = true;
                document.getElementById('txt_lcost').readOnly = true; 
                document.getElementById('txt_fixed').readOnly = true;
                document.getElementById('txt_days').readOnly = true;
                document.getElementById('txt_kms').readOnly = false;

                var role_id1 = '<%= Session["Role"] %>';
                var role_id = parseInt(role_id1.toString());
                if (role_id > 2) {
                    document.getElementById('txt_vts').readOnly = false;
                    document.getElementById('txt_admin').readOnly = false;
                }
                else {
                    document.getElementById('txt_vts').readOnly = true;
                    document.getElementById('txt_admin').readOnly = true;
                }

//                document.getElementById('txt_vts').readOnly = false;
//                document.getElementById('txt_admin').readOnly = false;
                document.getElementById('txt_rate').readOnly = false;
            
            }
        }
        function calculateAge() {
            var date = new Date(document.getElementById("txt_fromdate").value);
            var today = new Date(document.getElementById("txt_todate").value);
            var timeDiff = Math.abs(today.getTime() - date.getTime()+1);
            var age1 = Math.ceil(timeDiff / (1000 * 3600 * 24));
            document.getElementById('txt_tota').value = parseFloat(age1).toFixed(0);
        }
        function kmcalc() {
            var totdays = document.getElementById('txt_tota').value;
            var perdaykms = document.getElementById('txt_kms').value;
            var totalamounts = (parseFloat(totdays) * parseFloat(perdaykms));
            document.getElementById('txt_totalkms').value = totalamounts;
        }
        function admincalc() {
            var kmrate = document.getElementById('txt_rate').value;
            var adminkms = document.getElementById('txt_admin').value;
            var totalamount = (parseFloat(kmrate) * parseFloat(adminkms));
            document.getElementById('txt_amtadmin').value = totalamount;
        } 

        function ltramountcalculation() {
            var mode = document.getElementById('txt_mode').value;
            if (mode == "0") {
                var totalliters = document.getElementById('txt_tltr').value;
                if (totalliters == "" || totalliters == null || totalliters == undefined) {
                    totalliters = "0";
                }
                var totalcost = document.getElementById('txt_lcost').value;
                if (totalcost == "" || totalcost == null || totalcost == undefined) {
                    totalcost = "0";
                }
                var deductionamount = document.getElementById('txt_dedamt').value;
                if (deductionamount == "" || deductionamount == null || deductionamount == undefined) {
                    deductionamount = "0";
                }
                var totalamount = (parseFloat(totalliters) * parseFloat(totalcost) - parseFloat(deductionamount));
                var totdays = document.getElementById('txt_tota').value;
                var totalamounts = (parseFloat(totalamount) * parseFloat(totdays));
                document.getElementById('txt_amount').value = totalamounts;

            }
            else if (mode == "1") {
                var fixedrate = document.getElementById('txt_fixed').value;
                if (fixedrate == "" || fixedrate == null || fixedrate == undefined) {
                    fixedrate = "0";
                }
                var noofdays = document.getElementById('txt_days').value;
                if (noofdays == "" || noofdays == null || noofdays == undefined) {
                    noofdays = "0";
                }
                var deductionamount = document.getElementById('txt_dedamt').value;
                if (deductionamount == "" || deductionamount == null || deductionamount == undefined) {
                    deductionamount = "0";
                }
                var totalamount = (parseFloat(fixedrate) * parseFloat(noofdays) - parseFloat(deductionamount));
                var totdays = document.getElementById('txt_tota').value;
                var totalamounts = (parseFloat(totalamount) * parseFloat(totdays));
                document.getElementById('txt_amount').value = totalamounts;
            }
            else if (mode == "2") {
                var perdaykms = document.getElementById('txt_kms').value;
                if (perdaykms == "" || perdaykms == null || perdaykms == undefined) {
                    perdaykms = "0";
                }
                var kmrate = document.getElementById('txt_rate').value;
                if (kmrate == "" || kmrate == null || kmrate == undefined) {
                    kmrate = "0";
                }
                var deductionamount = document.getElementById('txt_dedamt').value;
                if (deductionamount == "" || deductionamount == null || deductionamount == undefined) {
                    deductionamount = "0";
                }
                var totalamount = (parseFloat(perdaykms) * parseFloat(kmrate) - parseFloat(deductionamount));
                var totdays = document.getElementById('txt_tota').value;
                var totalamounts = (parseFloat(totalamount) * parseFloat(totdays));
                document.getElementById('txt_amount').value = totalamounts;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="content-header">
        <h1>
           Transport payment Reporte
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li><a href="#">Transport payment Reporte</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <i style="padding-right: 5px;" class="fa fa-cog"></i>Transport payment Reporte Details
                </h3>
            </div>
            <div class="box-body">
               
               
                <div id='user_fillform'>
                    <table align="center">
                            <tr>
                            <td>
                               <label>From Date</label>
                            </td>
                            <td style="height: 40px;">
                                <input id="txt_fromdate" type="date"  class="form-control"  />
                            </td>
                        
                            <td>
                                <label>To Date</label>
                            </td>
                            <td style="height: 40px;">
                                <input id="txt_todate" type="date" class="form-control" onblur="calculateAge();"/>
                                </td>
                                <td style="display:none">
                                <input type="text" id="txt_tota" class="form-control"  >
                                </td>
                            </tr>
                            <tr>
                            <td>
                                <label>Plant Name</label>
                            </td>
                            <td style="height: 40px; ">
                                <select id="slct_plant"  name="Plant Name" class="form-control" onchange="get_route_details();"></select>
                            </td>
                           
                            <td >
                                <label>Route name</label>
                            </td>
                            <td style="height: 40px; ">
                                <select id="slct_route"  name=" Route" class="form-control" ></select>
                            </td>
                            </tr>

                            <tr>
                            <td>
                                <label>Bank Name</label>
                            </td>
                            <td style="height: 40px; ">
                                <select id="slct_bank" name="Bank" class="form-control"></select>
                            </td>
                             <td>
                                <label>Account No</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_account"  type="text"  name="Bank" class="form-control" placeholder="Enter Account No"/>
                            </td>
                           
                        </tr>

                          <tr>
                        <td>
                                <label>Branch Name</label>
                                  
                            </td>
                             <td style="height: 40px; ">
                                <input id="txt_branch" type="text" name="Branch" class="form-control" placeholder="Enter Branch Name"/>
                            </td>

                            <td>
                                <label>Ifcs Code</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_ifcs" type="text" name="Vechile" class="form-control" placeholder="Enter Ifcs Code"/>
                            </td>
                           
                       
                        </tr>

                         <tr>
                             <td>
                                <label>A/C Holder Name</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_name"  type="text"  name="Bank" class="form-control" placeholder="Enter Account Holder Name"/>
                            </td>
                            <td>
                                <label>Pan Card</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_pan" type="text" name="Vechile" class="form-control" placeholder="Enter Pan Card"/>
                            </td>
                          
                        </tr>
                         <tr>
                            <td>
                                <label>Cash Deposite</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_deposite"  type="text"  name="Bank" class="form-control" placeholder="Enter Cash Deposite"/>
                            </td>
                             <td>

                                <label>Deduction amt</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_dedamt"  type="text"  name="Bank" class="form-control" placeholder="Enter Deduction"/>
                            </td>
                            
                        </tr>
                         <tr>
                      
                         <td>
                                <label>descrption</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_desc"  type="text"  name="Bank" class="form-control" placeholder="Enter descrption"/>
                            </td>
                              <td>
                                <label>Vechile Type</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_vechtype" type="text" name="Vechile" class="form-control" placeholder="Enter Vechile Type"/>
                            </td>
                             
                        </tr>

                        <tr>
                         <td>
                                <label> Vechile No</label>
                                  
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_vechile" type="text" name="Vechile" class="form-control" placeholder="Enter Vechile No"/>
                            </td>
                            <td>
                                <label> Mode</label>
                            </td>
                            <td style="height: 40px; ">
                                 <select id="txt_mode" class="form-control" onchange="displaymodes();">
                                    <option>Select</option>
                                    <option value="0">Ltr</option>
                                    <option value="1">fixed</option>
                                     <option value="2">km</option>
                                </select>
                            </td>
                            
                        </tr>

                        <tr>
                            <td>
                                <label>Toatl Ltr</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_tltr" type="text" name="Ltr" class="form-control" placeholder="Enter Total Liters"/>
                            </td>
                         <td>
                                <label>Ltr Cost</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_lcost" type="text" name="ltr" class="form-control" placeholder="Enter ltrcost"  onchange="ltramountcalculation();"/>
                            </td>
                            
                        </tr>

                       <tr>
                            <td>
                                <label>Fixed rate</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_fixed" type="text" name="rate" class="form-control" placeholder="Enter Fixed rate"/>
                            </td>
                         <td>
                                <label>No.Of Days Cost</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_days" type="text" name="days" class="form-control" placeholder="Enter no.of days"  onchange="ltramountcalculation();"/>
                            </td>
                            </tr>
                             <tr>
                            <td>
                                <label>Per day kms</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_kms" type="text" name="day kms" class="form-control" placeholder="Enter Perday Kms"  onchange="kmcalc();"/>
                            </td>
                            <td>
                                <label>Total Kms</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_totalkms" type="text" name="day kms" class="form-control" placeholder="Enter Total Kms" readonly/>
                            </td>
                              <td style="display:none">
                                <label>Vts kms</label>
                            </td>
                            <td  style="display:none">
                                <input id="txt_vts" type="text" name="Vts" class="form-control" placeholder="Enter vts kms"/>
                            </td>
                           
                        
                            </tr>
                              <tr>
                            <td>
                                <label>Admin kms </label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_admin" type="text" name="Admin" class="form-control" placeholder="Enter admin Kms" />
                            </td>
                            <td>
                                <label> km rate </label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_rate" type="text" name="Admin" class="form-control" placeholder="Enter km rate"  onchange="ltramountcalculation();"/>
                            </td>
                            </tr>
                            <tr>
                         <td>
                                <label>Total  Amt</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_amount" type="text" name="days" class="form-control" placeholder="Enter Total Amt" readonly/>
                            </td>
                            <td>
                                <label>Admin  Amt</label>
                            </td>
                            <td style="height: 40px; ">
                                <input id="txt_amtadmin" type="text" name="days" class="form-control" placeholder="Enter Admin Amt"/>
                            </td>
                            </tr>

                        <tr style="display:none;">
                            <td>
                                <label id="lbl_sno"  >
                                </label>
                            </td>
                        </tr>
                        </table>
                        <table align="center">
                        <tr>
                            <td colspan="2" align="center" style="height: 40px;">
                                <input id="btn_save" type="button" class="btn btn-primary" name="submit" value='save'  onclick="savetransportpaymentdetails()" />
                                <input id='btn_close' type="button" class="btn btn-danger" name="Close" value='Close' onclick="forclearall()" />
                            </td>
                        </tr>
                    </table>
                </div>
                 <div id="div_user">
                </div>

            </div>
        </div>
    </section>


</asp:Content>


