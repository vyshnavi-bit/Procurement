<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="GensetPowerDiesel.aspx.cs" Inherits="GensetPowerDiesel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
    <script type="text/javascript">
        $(function () {
            showGensets();
            get_Plant_Route_details();
        });
        function showGensets() {
            $("#div_Gendetails").show();
            $("#div_Disel").hide();
            $("#div_Power").hide();
            $("#div_Chemicals").hide();
        }
        function showDisel() {
            $("#div_Disel").show();
            $("#div_Power").hide();
            $("#div_Gendetails").hide();
            $("#div_Chemicals").hide();
        }
        function showPowerCosumption() {
            $("#div_Power").show();
            $("#div_Disel").hide();
            $("#div_Gendetails").hide();
            $("#div_Chemicals").hide();
        }
        
        function showChemicalsDetails() {
            $("#div_Chemicals").show();
            $("#div_Power").hide();
            $("#div_Disel").hide();
            $("#div_Gendetails").hide();
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
                Error: e
            });
        }

        var routedetailes = [];
        var bankdetailes = [];
        function get_Plant_Route_details() {
            var data = { 'operation': 'get_Plant_Route_details' };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillplantdetails(msg);
                        routedetailes = msg;
                        //bankdetailes = msg;
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
        var routedata = [];
        function fillplantdetails(msg) {
            var data = document.getElementById('Slct_Plantcode');
            var length = data.options.length;
            document.getElementById('Slct_Plantcode').options.length = null;

            var data1 = document.getElementById('Slct_Plantcode1');
            var length1 = data1.options.length;
            document.getElementById('Slct_Plantcode1').options.length = null;

            var data2 = document.getElementById('Slct_Plantcode2');
            var length2 = data2.options.length;
            document.getElementById('Slct_Plantcode2').options.length = null;

            var data3 = document.getElementById('Slct_Plantcode3');
            var length3 = data3.options.length;
            document.getElementById('Slct_Plantcode3').options.length = null;

            var opt = document.createElement('option');
            opt.innerHTML = "Select plantName";
            opt.value = "Select plantName";
            opt.setAttribute("selected", "selected");
            opt.setAttribute("disabled", "disabled");
            opt.setAttribute("class", "dispalynone");
            data.appendChild(opt);

            var opt1 = document.createElement('option');
            opt1.innerHTML = "Select plantName";
            opt1.value = "Select plantName";
            opt1.setAttribute("selected", "selected");
            opt1.setAttribute("disabled", "disabled");
            opt1.setAttribute("class", "dispalynone");
            data1.appendChild(opt1);

            var opt2 = document.createElement('option');
            opt2.innerHTML = "Select plantName";
            opt2.value = "Select plantName";
            opt2.setAttribute("selected", "selected");
            opt2.setAttribute("disabled", "disabled");
            opt2.setAttribute("class", "dispalynone");
            data2.appendChild(opt2);


            var opt3 = document.createElement('option');
            opt3.innerHTML = "Select plantName";
            opt3.value = "Select plantName";
            opt3.setAttribute("selected", "selected");
            opt3.setAttribute("disabled", "disabled");
            opt3.setAttribute("class", "dispalynone");
            data3.appendChild(opt3);



            for (var i = 0; i < msg.length; i++) {
                if (msg[i].Plant_Code != null) {
                    if (routedata.indexOf(msg[i].Plant_Code) == -1) {
                        var option = document.createElement('option');
                        var option1 = document.createElement('option');
                        var option2 = document.createElement('option');
                        var option3 = document.createElement('option');
                        option.innerHTML = msg[i].PlantName;
                        option.value = msg[i].Plant_Code;

                        option1.innerHTML = msg[i].PlantName;
                        option1.value = msg[i].Plant_Code;
                        option2.innerHTML = msg[i].PlantName;
                        option2.value = msg[i].Plant_Code;
                        option3.innerHTML = msg[i].PlantName;
                        option3.value = msg[i].Plant_Code;

                        data.appendChild(option);
                        data1.appendChild(option1);
                        data2.appendChild(option2);
                        data3.appendChild(option3);
                        routedata.push(msg[i].Plant_Code);
                    }
                }

            }
        }





        function Genchangeclosing() {
            var openinggen = document.getElementById('txtGOpeningReading').value;
            var closinginggen = document.getElementById('txtGEndingReading').value;

            document.getElementById('txtGTotalReading').value = closinginggen - openinggen;

        }

        function Diselchangeclosing() {

            var diselopening = document.getElementById('txtDOpeningLiters').value;
            var dieselconsumption = document.getElementById('txtDConsumption').value;

            document.getElementById('txtDClosingLiters').value = diselopening - dieselconsumption;
        }

        function powerchangeclosing() {


            var poweropenunit = document.getElementById('txtPOpeningUnits').value;
            var powerconsuptiounit = document.getElementById('txtPConsumptionUnits').value;
            document.getElementById('txtPClosingUnits').value = powerconsuptiounit - poweropenunit;

        }

        function saveGenReadingDetails() {

            var plant_code = document.getElementById('Slct_Plantcode').value;
            if (plant_code == "" || plant_code == "Select plantName") {
                alert("Enter PlantName");
                return false;
            }


            var Date = document.getElementById('txtDate').value;
            if (Date == "") {
                alert("Enter Date ");
                return false;

            }
            var GenOpReading = document.getElementById('txtGOpeningReading').value;
            if (GenOpReading == "") {
                alert("Enter GenOpReading ");
                return false;
            }
            var GenClReading = document.getElementById('txtGEndingReading').value;
            var GenTotReading = document.getElementById('txtGTotalReading').value;
            var btnval = document.getElementById('btn_save').value;
            var sno = document.getElementById('lbl_sno').value;
            var data = { 'operation': 'saveGenReadingDetails', 'plant_code': plant_code, 'Date': Date, 'GenOpReading': GenOpReading, 'GenClReading': GenClReading, 'GenTotReading': GenTotReading, 'btnVal': btnval, 'sno': sno };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        alert(msg);
                        // get_GenReadingDetails();
                        GenReadingclearall();
                        $("#div_GenData").show();
                        $("#div_Gendetails").show();
                       
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
        function GenReadingclearall() {
            document.getElementById('Slct_Plantcode').selectedIndex = 0;
            document.getElementById('txtDate').value = "";
            document.getElementById('txtGOpeningReading').value = "";
            document.getElementById('txtGEndingReading').value = "";
            document.getElementById('txtGTotalReading').value = "";
            document.getElementById('lbl_sno').value = "";
            document.getElementById('btn_save').value = "Save";
        }

        function Routechangeclick(Slct_Plantcode) {
            var plant_code = document.getElementById('Slct_Plantcode').value;
            var data = { 'operation': 'get_GenReadingDetails', 'plant_code': plant_code };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillGenReadingDetails(msg);
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

        function fillGenReadingDetails(msg) {
            var results = '<div  style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid" id="gridtable">';
            results += '<thead><tr><th scope="col"></th><th scope="col">GenOpReading</th><th scope="col">GenClReading</th><th scope="col">GenTotReading</th></tr></thead></tbody>';
            for (var i = 0; i < msg.length; i++) {
                results += '<tr><td><input id="btn_poplate" type="button"  onclick="Gengetme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
                results += '<th scope="row" class="1" >' + msg[i].Date + '</th>';
                results += '<td data-title="code" class="2">' + msg[i].GenOpReading + '</td>';
                results += '<td data-title="status" class="3">' + msg[i].GenClReading + '</td>';
                results += '<td style="display:none" data-title="status" class="4">' + msg[i].GenTotReading + '</td>';
                results += '<td style="display:none" class="5">' + msg[i].sno + '</td></tr>';
            }
            results += '</table></div>';
            $("#div_GenData").html(results);
        }
        function Gengetme(thisid) {
           
            var Date = $(thisid).parent().parent().children('.1').html();
            var GenOpReading = $(thisid).parent().parent().children('.2').html();
            var GenClReading = $(thisid).parent().parent().children('.3').html();
            var GenTotReading = $(thisid).parent().parent().children('.4').html();
            var sno = $(thisid).parent().parent().children('.5').html();


            document.getElementById('txtDate').value = Date;
            document.getElementById('txtGOpeningReading').value = GenOpReading;
            document.getElementById('txtGEndingReading').value = GenClReading;
            document.getElementById('txtGTotalReading').value = GenTotReading;
            document.getElementById('btn_save').value = "Modify";
            document.getElementById('lbl_sno').value = sno;
            $("#div_GenData").show();
            $("#div_Gendetails").show();
        }


        function saveDeiselDetails() {

            var plant_code = document.getElementById('Slct_Plantcode1').value;
            if (plant_code == "" || plant_code == "Select plantName") {
                alert("Enter PlantName");
                return false;
            }
            var DiselOpLtrs = document.getElementById('txtDOpeningLiters').value;
            if (DiselOpLtrs == "") {

                alert("Enter OpeningLiters");
                return false;
            }
            var sno = document.getElementById('lbl_sno').value;
            var DispReceipt = document.getElementById('txtDReceipt').value;
            var DispHours = document.getElementById('txtDHours').value;
            var DispConsumption = document.getElementById('txtDConsumption').value;
            var DispCloLtrs = document.getElementById('txtDClosingLiters').value;
            var DiselDate = document.getElementById('txtDiselDate').value;
            var btnval = document.getElementById('btn_savedisel').value;
            var data = { 'operation': 'saveDeiselDetails', 'plant_code': plant_code, 'DiselOpLtrs': DiselOpLtrs, 'DispReceipt': DispReceipt, 'DispHours': DispHours, 'DispConsumption': DispConsumption, 'DispCloLtrs': DispCloLtrs, 'DiselDate': DiselDate, 'btnVal': btnval, 'sno': sno };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        alert(msg);
                       // get_DeiselDetails();
                        Diseldetailsclearall();
                        $('#div_Disel').show();
                        $('#div_DiselGrid').show();

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

        function Routechangeclick1(Slct_Plantcode1) {
            var plant_code = document.getElementById('Slct_Plantcode1').value;
            var data = { 'operation': 'get_DeiselDetails', 'plant_code': plant_code };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillDeiselDetails(msg);
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
        function fillDeiselDetails(msg) {
            var results = '<div  style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid"  id="gridtable">';
            results += '<thead><tr><th scope="col"></th><th scope="col">PDiselOpLtrsF</th><th scope="col">DispConsumption</th><th scope="col">DispCloLtrs</th></tr></thead></tbody>';
            for (var i = 0; i < msg.length; i++) {
                results += '<tr><td><input id="btn_poplate" type="button"  onclick="Diselgetme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
                results += '<th scope="row" class="1" >' + msg[i].DiselOpLtrs + '</th>';
                results += '<td style="display:none" data-title="uimstatus" class="2">' + msg[i].DispReceipt + '</td>';
                results += '<th style="display:none" scope="row" class="3" >' + msg[i].DispHours + '</th>';
                results += '<td data-title="uimstatus" class="4">' + msg[i].DispConsumption + '</td>';
                results += '<td data-title="uimstatus" class="5">' + msg[i].DispCloLtrs + '</td>';
                results += '<td style="display:none" class="6">' + msg[i].DiselDate + '</td>';
                results += '<td style="display:none" class="7">' + msg[i].sno + '</td></tr>';
            }
            results += '</table></div>';
            $("#div_DiselGrid").html(results);
        }
        function Diselgetme(thisid) {
            Diselchangeclosing();
            var DiselOpLtrs = $(thisid).parent().parent().children('.1').html();
            var DispReceipt = $(thisid).parent().parent().children('.2').html();
            var DispHours = $(thisid).parent().parent().children('.3').html();
            var DispConsumption = $(thisid).parent().parent().children('.4').html();
            var DispCloLtrs = $(thisid).parent().parent().children('.5').html();
            var DiselDate = $(thisid).parent().parent().children('.6').html();
            var sno = $(thisid).parent().parent().children('.7').html();



            document.getElementById('txtDOpeningLiters').value = DiselOpLtrs;
            document.getElementById('txtDConsumption').value = DispConsumption;

            document.getElementById('txtDHours').value = DispHours;
            document.getElementById('txtDReceipt').value = DispReceipt;
            document.getElementById('txtDiselDate').value = DiselDate;
            document.getElementById('txtDClosingLiters').value = DispCloLtrs;
            document.getElementById('lbl_sno').value = sno;
            document.getElementById('btn_savedisel').value = "Modify";
            $('#div_Disel').show();
            $('#div_DiselGrid').show();
        }
        function Diseldetailsclearall() {
            
            document.getElementById('Slct_Plantcode1').selectedIndex = 0;
            document.getElementById('txtDOpeningLiters').value = "";
            document.getElementById('txtDiselDate').value = "";
            document.getElementById('txtDConsumption').value = "";
            document.getElementById('txtDHours').value = "";
            document.getElementById('txtDReceipt').value = "";
            document.getElementById('txtDClosingLiters').value = "";
            document.getElementById('lbl_sno').value = "";
            document.getElementById('btn_savedisel').value = "Save";
            $("#lbl_code_error_msg").hide();
            $("#lbl_name_error_msg").hide();
            $('#div_Disel').show();
            $('#div_DiselGrid').show();
        }
        function savePowerDetails() {
            var plant_code = document.getElementById('Slct_Plantcode2').value;
            if (plant_code == "" || plant_code == "Select plantName") {
                alert("Enter PlantName");
                return false;
            }

            var PowOpUnit = document.getElementById('txtPOpeningUnits').value;
            if (PowOpUnit == "") {

                alert("Enter PowOpUnit"); 
                return false;
            }
            var PowCloUnit = document.getElementById('txtPClosingUnits').value;
            var PowConsumpUnit = document.getElementById('txtPConsumptionUnits').value;
            var PowerDate = document.getElementById('txtPowerDate').value;
            var sno = document.getElementById('lbl_sno').value;
            var btnval = document.getElementById('btn_savepower').value;
            var data = { 'operation': 'savePowerDetails', 'plant_code': plant_code, 'PowOpUnit': PowOpUnit, 'PowCloUnit': PowCloUnit, 'PowConsumpUnit': PowConsumpUnit, 'PowerDate': PowerDate, 'btnVal': btnval, 'sno': sno };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        alert(msg);
                       // get_PowerDetails();
                        powerdetailsclearall();
                        $('#grid_power').show();
                        $('#div_Power').show();
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

        function Routechangeclick2(Slct_Plantcode2) {

            var plant_code = document.getElementById('Slct_Plantcode2').value;
            var data = { 'operation': 'get_PowerDetails', 'plant_code': plant_code };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillPowerDetails(msg);
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
        function fillPowerDetails(msg) {
            var results = '<div  style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid"  id="gridtable">';
            results += '<thead><tr><th scope="col"></th><th scope="col">PowOpUnit</th><th scope="col">PowConsumpUnit</th><th scope="col">PowCloUnit</th></tr></thead></tbody>';
            for (var i = 0; i < msg.length; i++) {
                results += '<tr><td><input id="btn_poplate" type="button"  onclick="powergetme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
                results += '<th scope="row" class="1" >' + msg[i].PowOpUnit + '</th>';
                results += '<td data-title="uimstatus" class="3">' + msg[i].PowConsumpUnit + '</td>'; 
                results += '<td data-title="uimstatus" class="2">' + msg[i].PowCloUnit + '</td>';
                results += '<td data-title="uimstatus" class="4">' + msg[i].PowerDate + '</td>';
                results += '<td style="display:none" class="5">' + msg[i].sno + '</td></tr>';
            }
            results += '</table></div>';
            $("#grid_power").html(results);
        }
        function powergetme(thisid) {
            powerchangeclosing();
            var PowOpUnit = $(thisid).parent().parent().children('.1').html();
            var PowCloUnit = $(thisid).parent().parent().children('.2').html();
            var PowConsumpUnit = $(thisid).parent().parent().children('.3').html();
            var PowerDate = $(thisid).parent().parent().children('.4').html();
            var sno = $(thisid).parent().parent().children('.5').html();

            document.getElementById('txtPowerDate').value = PowerDate;
            document.getElementById('txtPOpeningUnits').value = PowOpUnit;
            document.getElementById('txtPClosingUnits').value = PowCloUnit;
            document.getElementById('txtPConsumptionUnits').value = PowConsumpUnit;
            document.getElementById('lbl_sno').value = sno;
            document.getElementById('btn_savepower').value = "Modify";
            $('#grid_power').show();
            $('#div_Power').show();
        }
        function powerdetailsclearall() {
            
            document.getElementById('Slct_Plantcode2').selectedIndex = 0;
            document.getElementById('txtPOpeningUnits').value = "";
            document.getElementById('txtPowerDate').value = "";
            document.getElementById('txtPClosingUnits').value = "";
            document.getElementById('txtPConsumptionUnits').value = "";
            document.getElementById('lbl_sno').value = "";
            document.getElementById('btn_savepower').value = "Save";
            $('#grid_power').show();
            $('#div_Power').show();
        }



        function saveChemicalDetails() {
            var plant_code = document.getElementById('Slct_Plantcode3').value;
            if (plant_code == "" || plant_code == "Select plantName") {
                alert("Enter PlantName");
                return false;
            }

            var sulphuric = document.getElementById('txtsulphuric').value;
            if (sulphuric == "") {

                alert("Enter sulphuric");
                return false;
            }
            var Idophore = document.getElementById('txtIdophore').value;
            if (Idophore == "") {

                alert("Enter Idophore");
                return false;
            }
            var washingsoda = document.getElementById('txtwashingsoda').value;
            if (washingsoda == "") {

                alert("Enter washingsoda");
                return false;
            }
            var causticsoda = document.getElementById('txtCausticSoda').value;
            if (causticsoda == "") {

                alert("Enter CausticSoda"); 
                return false;
            }
            var slurrey = document.getElementById('txtSlurrey').value;
            var nitricacid = document.getElementById('txtNitricAcid').value;
            var ChemicalDate = document.getElementById('txtChemicalDate').value;
            var sno = document.getElementById('lbl_sno').value;
            var btnval = document.getElementById('btnChemical').value;
            var data = { 'operation': 'saveChemicalDetails', 'plant_code': plant_code, 'sulphuric': sulphuric, 'Idophore': Idophore, 'washingsoda': washingsoda, 'causticsoda': causticsoda, 'slurrey': slurrey, 'nitricacid': nitricacid, 'ChemicalDate': ChemicalDate, 'btnVal': btnval, 'sno': sno };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        alert(msg);
                        // get_PowerDetails();
                        chemicaldetailsclearall();
                        $('#grid_chemicals').show();
                        $('#div_Chemicals').show();
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

        function Routechangeclick3(Slct_Plantcode3) {

            var plant_code = document.getElementById('Slct_Plantcode3').value;
            var data = { 'operation': 'get_ChemicalDetails', 'plant_code': plant_code };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillChemicalDetails(msg);
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
        function fillChemicalDetails(msg) {
            var results = '<div  style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid"  id="gridtable">';
            results += '<thead><tr><th scope="col"></th><th scope="col">sulphuric</th><th scope="col">Idophore</th><th scope="col">Date</th></tr></thead></tbody>';
            for (var i = 0; i < msg.length; i++) {
                results += '<tr><td><input id="btn_poplate" type="button"  onclick="chemicalgetme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
                results += '<th scope="row" class="1" >' + msg[i].sulphuric + '</th>';
                results += '<td data-title="uimstatus" class="2">' + msg[i].Idophore + '</td>';
                results += '<td data-title="uimstatus" class="7">' + msg[i].ChemicalDate + '</td>';
                results += '<td style="display:none" class="3">' + msg[i].washingsoda + '</td>';
                results += '<td style="display:none" class="4">' + msg[i].causticsoda + '</td>';
                results += '<td style="display:none" class="5">' + msg[i].slurrey + '</td>';
                results += '<td style="display:none" class="6">' + msg[i].nitricacid + '</td>';
              
                results += '<td style="display:none" class="8">' + msg[i].sno + '</td></tr>';
            }
            results += '</table></div>';
            $("#grid_chemicals").html(results);
        }
        function chemicalgetme(thisid) {
          //  powerchangeclosing();
            var sulphuric = $(thisid).parent().parent().children('.1').html();
            var Idophore = $(thisid).parent().parent().children('.2').html();
            var washingsoda = $(thisid).parent().parent().children('.3').html();
            var causticsoda = $(thisid).parent().parent().children('.4').html();
            var slurrey = $(thisid).parent().parent().children('.5').html(); 
            var nitricacid = $(thisid).parent().parent().children('.6').html();
            var ChemicalDate = $(thisid).parent().parent().children('.7').html();
            var sno = $(thisid).parent().parent().children('.8').html();


            document.getElementById('txtChemicalDate').value = ChemicalDate;
            document.getElementById('txtsulphuric').value = sulphuric;
            document.getElementById('txtIdophore').value = Idophore;
            document.getElementById('txtwashingsoda').value = washingsoda;
            document.getElementById('txtCausticSoda').value = causticsoda;
            document.getElementById('txtSlurrey').value = slurrey;
            document.getElementById('txtNitricAcid').value = nitricacid;
            document.getElementById('lbl_sno').value = sno;
            document.getElementById('btnChemical').value = "Modify";
            $('#grid_chemicals').show();
            $('#div_Chemicals').show();
        }
        function chemicaldetailsclearall() {
            document.getElementById('Slct_Plantcode2').selectedIndex = 0;
            document.getElementById('txtsulphuric').value = "";
            document.getElementById('txtIdophore').value = "";
            document.getElementById('txtwashingsoda').value = "";
            document.getElementById('txtCausticSoda').value = "";
            document.getElementById('txtSlurrey').value = "";
            document.getElementById('txtNitricAcid').value = "";
            document.getElementById('lbl_sno').value = "";
            document.getElementById('btnChemical').value = "Save";
            $('#grid_chemicals').show();
            $('#div_Chemicals').show();
        }




    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <section class="content">
        <!-- Small boxes (Stat box) -->
        <div class="row">
            <section class="content-header">
                <h1>
                    GeneratorPowerDiesel
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Operation</a></li>
                    <li><a href="#">Masters</a></li>
                </ol>
            </section>
            <section class="content">
                <div class="box box-info">
                    <div class="box-header with-border">
                    </div>
                    <div class="box-body">
                        <div>
                            <ul class="nav nav-tabs">
                                <li id="id_tab_Personal" class="active"><a data-toggle="tab" href="#" onclick="showGensets()">
                                    <i class="fa fa-university" aria-hidden="true"></i>&nbsp;&nbsp;GeneratorReding</a></li>
                                <li id="id_tab_documents" class=""><a data-toggle="tab" href="#" onclick="showDisel()">
                                    <i class="fa fa-truck" aria-hidden="true"></i>&nbsp;&nbsp;DiselDetails</a></li>
                                <li id="Li2" class=""><a data-toggle="tab" href="#" onclick="showPowerCosumption()">
                                    <i class="fa fa-file-text"></i>&nbsp;&nbsp;PowerConsumpition</a></li>
                                <li id="Li1" class=""><a data-toggle="tab" href="#" onclick="showChemicalsDetails()">
                                    <i class="fa fa-file-text"></i>&nbsp;&nbsp;ChemicalsDetails</a></li>
                            </ul>
                        </div>
                        <div id="div_Gendetails" style="display: none;">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <i style="padding-right: 5px;" class="fa fa-cog"></i>GensetReding
                                </h3>
                            </div>
                            <div class="box-body">
                                <div id="babkfillform">
                                    <table align="center" style="width: 60%;">
                                        <tr>
                                            <td style="height: 40px;">
                                                PlantName
                                            </td>
                                            <td style="height: 40px;">
                                                <select id="Slct_Plantcode" class="form-control" onchange="Routechangeclick(this);">
                                                    <option selected disabled value="Select Plantcode">Select Plantcode</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 40px;">
                                                Date<span style="color: red;">*</span>
                                            </td>
                                            <td>
                                                <input type="date" class="form-control" id="txtDate" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                OpeningReading
                                            </td>
                                            <td>
                                                <input id="txtGOpeningReading" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter OpeningReading" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                EndingReading
                                            </td>
                                            <td>
                                                <input id="txtGEndingReading" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter EndingReading" onchange="Genchangeclosing();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                TotalReading
                                            </td>
                                            <td>
                                                <input id="txtGTotalReading" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter EndingReading" readonly />
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td>
                                                <label id="lbl_sno">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center" style="height: 40px;">
                                                <input id="btn_save" type="button" class="btn btn-primary" name="submit" value='save'
                                                    onclick="saveGenReadingDetails()" />
                                                <input id='btn_close' type="button" class="btn btn-danger" name="Close" value='Close'
                                                    onclick="GenReadingclearall()" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="div_GenData">
                                </div>
                            </div>
                        </div>
                        <div id="div_Disel" style="display: none;">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <i style="padding-right: 5px;" class="fa fa-cog"></i>DiselDetails
                                </h3>
                            </div>
                            <div class="box-body">
                                <div id="pffillform">
                                    <table align="center" style="width: 60%;">
                                        <tr>
                                            <td style="height: 40px;">
                                                PlantName
                                            </td>
                                            <td style="height: 40px;">
                                                <select id="Slct_Plantcode1" class="form-control" onchange="Routechangeclick1(this);">
                                                    <option selected disabled value="Select Plantcode">Select Plantcode</option>
                                                </select>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="height: 40px;">
                                                Date<span style="color: red;">*</span>
                                            </td>
                                            <td>
                                                <input type="date" class="form-control" id="txtDiselDate" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                OpeningLiters
                                            </td>
                                            <td>
                                                <input id="txtDOpeningLiters" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter OpeningLiters" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Receipt
                                            </td>
                                            <td>
                                                <input id="txtDReceipt" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter Receipt" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Hours
                                            </td>
                                            <td>
                                                <input id="txtDHours" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter Hours" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Consumption
                                            </td>
                                            <td>
                                                <input id="txtDConsumption" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter Consumption" onchange="Diselchangeclosing();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                ClosingLiters
                                            </td>
                                            <td>
                                                <input id="txtDClosingLiters" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter ClosingLiters" readonly />
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td>
                                                <label id="Label2">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center" style="height: 40px;">
                                                <input id="btn_savedisel" type="button" class="btn btn-primary" name="submit" value='save'
                                                    onclick="saveDeiselDetails()" />
                                                <input id='btn_closedeisel' type="button" class="btn btn-danger" name="Close" value='Close'
                                                    onclick="Diseldetailsclearall()" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="div_DiselGrid">
                                </div>
                            </div>
                        </div>
                        <div id="div_Power" style="display: none;">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <i style="padding-right: 5px;" class="fa fa-cog"></i>PowerConsumpition
                                </h3>
                            </div>
                            <div class="box-body">
                                <div id="uomfillform">
                                    <table align="center" style="width: 60%;">
                                        <tr>
                                            <td style="height: 40px;">
                                                PlantName
                                            </td>
                                            <td style="height: 40px;">
                                                <select id="Slct_Plantcode2" class="form-control" onchange="Routechangeclick2(this);">
                                                    <option selected disabled value="Select Plantcode">Select Plantcode</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 40px;">
                                                Date<span style="color: red;">*</span>
                                            </td>
                                            <td>
                                                <input type="date" class="form-control" id="txtPowerDate" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                OpeningUnits
                                            </td>
                                            <td>
                                                <input id="txtPOpeningUnits" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter OpeningUnits" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                ClosingUnits
                                            </td>
                                            <td>
                                                <input id="txtPConsumptionUnits" type="text" maxlength="45" class="form-control"
                                                    name="vendorcode" placeholder="Enter ConsumptionUnits" onchange="powerchangeclosing();" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                ConsumptionUnits
                                            </td>
                                            <td>
                                                <input id="txtPClosingUnits" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter ClosingUnits" readonly />
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td>
                                                <label id="lbl_sno">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center" style="height: 40px;">
                                                <input id="btn_savepower" type="button" class="btn btn-primary" name="submit" value='save'
                                                    onclick="savePowerDetails()" />
                                                <input id='btn_closepower' type="button" class="btn btn-danger" name="Close" value='Close'
                                                    onclick="powerdetailsclearall()" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="grid_power">
                                </div>
                            </div>
                        </div>
                          <div id="div_Chemicals" style="display: none;">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <i style="padding-right: 5px;" class="fa fa-cog"></i>Chemicals Details
                                </h3>
                            </div>
                            <div class="box-body">
                                <div id="Div2">
                                    <table align="center" style="width: 60%;">
                                        <tr>
                                            <td style="height: 40px;">
                                                PlantName
                                            </td>
                                            <td style="height: 40px;">
                                                <select id="Slct_Plantcode3" class="form-control" onchange="Routechangeclick3(this);">
                                                    <option selected disabled value="Select Plantcode">Select Plantcode</option>
                                                </select>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="height: 40px;">
                                                Date<span style="color: red;">*</span>
                                            </td>
                                            <td>
                                                <input type="date" class="form-control" id="txtChemicalDate" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Sulphuric Acid  
                                            </td>
                                            <td>
                                                <input id="txtsulphuric" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter Sulphuric Acid " />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Idophore
                                            </td>
                                            <td>
                                                <input id="txtIdophore" type="text" maxlength="45" class="form-control"
                                                    name="vendorcode" placeholder="Enter Idophore"  />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                washing soda
                                            </td>
                                            <td>
                                                <input id="txtwashingsoda" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter Washing soda"  />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                                Caustic Soda
                                            </td>
                                            <td>
                                                <input id="txtCausticSoda" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter Caustic Soda"  />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                               Slurrey 
                                            </td>
                                            <td>
                                                <input id="txtSlurrey" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter Slurrey"  />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                                Nitric Acid
                                            </td>
                                            <td>
                                                <input id="txtNitricAcid" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter Nitric Acid" />
                                            </td>
                                        </tr>
                                        <tr style="display: none;">
                                            <td>
                                                <label id="Label1">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center" style="height: 40px;">
                                                <input id="btnChemical" type="button" class="btn btn-primary" name="submit" value='save'
                                                    onclick="saveChemicalDetails()" />
                                                <input id='btnCloseChemical' type="button" class="btn btn-danger" name="Close" value='Close'
                                                    onclick="chemicaldetailsclearall()" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="grid_chemicals">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>
        
    </section>
</asp:Content>
