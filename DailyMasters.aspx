<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DailyMasters.aspx.cs" Inherits="DailyMasters" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">

    <script type="text/javascript">
        $(function () {
            showChillingMachinery();
            get_Plant_Route_details();
        });
        
        function showCompressorMachinery() {
            $("#div_Compressor").show();
            $("#div_Rechilling").hide();
            $("#div_silo").hide();
        }
        function showChillingMachinery() {
            $("#div_Rechilling").show();
            $("#div_Compressor").hide();
            $("#div_silo").hide();
        }
        function showSiloDetails() {
            $("#div_silo").show();
            $("#div_Rechilling").hide();
            $("#div_Compressor").hide();
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
            document.getElementById('Slct_Plantcode1').options.length1 = null;

            var data2 = document.getElementById('Slct_Plantcode2');
            var length2 = data2.options.length;
            document.getElementById('Slct_Plantcode2').options.length = null;


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

            for (var i = 0; i < msg.length; i++) {
                if (msg[i].Plant_Code != null) {
                    if (routedata.indexOf(msg[i].Plant_Code) == -1) {
                        var option = document.createElement('option');
                        var option1 = document.createElement('option');
                        var option2 = document.createElement('option');

                        option.innerHTML = msg[i].PlantName;
                        option.value = msg[i].Plant_Code;

                        option1.innerHTML = msg[i].PlantName;
                        option1.value = msg[i].Plant_Code;

                        option2.innerHTML = msg[i].PlantName;
                        option2.value = msg[i].Plant_Code;

                        data.appendChild(option);
                        data1.appendChild(option1);
                        data2.appendChild(option2);
                        routedata.push(msg[i].Plant_Code);
                    }
                }

            }
        }

        function saveCompressiormachinaryDetails() {

            var plant_code = document.getElementById('Slct_Plantcode1').value;
            if (plant_code == "" || plant_code == "Select plantName") {
                alert("Enter PlantName");
                return false;
            }

            var CompressiorName = document.getElementById('txtCompressiorName').value;
            if (CompressiorName == "") {
                alert("Enter CompressiorName ");
                return false;

            }
            var Compressiorcapacity = document.getElementById('txtCompressiorcapacity').value;
            if (Compressiorcapacity == "") {
                alert("Enter Compressiorcapacity ");
                return false;
            }
            var CompressiorPerhours = document.getElementById('txtCompressiorPerhours').value;
            var btnval = document.getElementById('btn_comprssior').value;
            var sno = document.getElementById('lbl_sno').value;
            var data = { 'operation': 'saveCompressiormachinaryDetails', 'plant_code': plant_code, 'CompressiorName': CompressiorName, 'Compressiorcapacity': Compressiorcapacity, 'CompressiorPerhours': CompressiorPerhours, 'btnVal': btnval, 'sno': sno };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        alert(msg);
                         Compressiorclearall()();
                      $('#div_CompressiorGrid').show();
                       $('#div_Compressor').show();

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
        function Compressiorclearall() {
            document.getElementById('Slct_Plantcode').selectedIndex = 0;
            document.getElementById('txtCompressiorName').value = "";
            document.getElementById('txtCompressiorcapacity').value = "";
            document.getElementById('txtCompressiorPerhours').value = "";
            document.getElementById('lbl_sno').value = "";
            document.getElementById('btn_comprssior').value = "Save";
        }

        function Routechangeclick1(Slct_Plantcode1) {
            var plant_code = document.getElementById('Slct_Plantcode1').value;
            var data = { 'operation': 'get_CompressiorMachinaryDetails', 'plant_code': plant_code };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillCompressiorDetails(msg);
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

        function fillCompressiorDetails(msg) {
            var results = '<div  style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid" id="gridtable">';
            results += '<thead><tr><th scope="col"></th><th scope="col">CompressiorName</th><th scope="col">Compressiorcapacity</th><th scope="col">GenTotReading</th></tr></thead></tbody>';
            for (var i = 0; i < msg.length; i++) {
                results += '<tr><td><input id="btn_poplate" type="button"  onclick="CompressiorGengetme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
                results += '<th scope="row" class="1" >' + msg[i].CompressiorName + '</th>';
                results += '<td data-title="code" class="2">' + msg[i].Compressiorcapacity + '</td>';
                results += '<td data-title="status" class="3">' + msg[i].CompressiorPerhours + '</td>';
                results += '<td style="display:none" class="4">' + msg[i].sno + '</td></tr>';
            }
            results += '</table></div>';
            $("#div_CompressiorGrid").html(results);
        }
        function CompressiorGengetme(thisid) {
          
            var CompressiorName = $(thisid).parent().parent().children('.1').html();
            var Compressiorcapacity = $(thisid).parent().parent().children('.2').html();
            var CompressiorPerhours = $(thisid).parent().parent().children('.3').html();
            var sno = $(thisid).parent().parent().children('.4').html();


            document.getElementById('txtCompressiorName').value = CompressiorName;
            document.getElementById('txtCompressiorcapacity').value = Compressiorcapacity;
            document.getElementById('txtCompressiorPerhours').value = CompressiorPerhours;
            document.getElementById('btn_comprssior').value = "Modify";
            document.getElementById('lbl_sno').value = sno;
            $('#div_CompressiorGrid').show();
            $('#div_Compressor').show();
        }
        function saveChillingMachinaryDetails() {
            var plant_code = document.getElementById('Slct_Plantcode').value;
            if (plant_code == "" || plant_code == "Select plantName") {
                alert("Enter PlantName");
                return false;
            }
            var Genaratorname = document.getElementById('txtGenaratorname').value;
            if (Genaratorname == "") {

                alert("Enter Genaratorname");
                return false;
            }
            var GenaratorCapacity = document.getElementById('txtGenaratorCapacity').value;
            if (GenaratorCapacity == "") {

                alert("Enter GenaratorCapacity");
                return false;
            }
            var GenaratorPerhours = document.getElementById('txtGenaratorPerhours').value;
            if (GenaratorPerhours == "") {

                alert("Enter GenaratorPerhours");
                return false;
            }
            var sno = document.getElementById('lbl_sno').value;
            var btnval = document.getElementById('btn_save').value;
            var data = { 'operation': 'saveChillingMachinaryDetails', 'plant_code': plant_code, 'Genaratorname': Genaratorname, 'GenaratorCapacity': GenaratorCapacity, 'GenaratorPerhours': GenaratorPerhours,'btnVal': btnval, 'sno': sno };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        alert(msg);
                        // get_PowerDetails();
                        ChillingMachinaryclearall();
                       $('#div_Rechilling').show();
                       $('#grid_ChillingData').show();
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
        function Routechangeclick(Slct_Plantcode) {
            var plant_code = document.getElementById('Slct_Plantcode').value;
            var data = { 'operation': 'get_ChillingMachinaryDetails', 'plant_code': plant_code };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillChillingMachinaryDetails(msg);
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
        function fillChillingMachinaryDetails(msg) {
            var results = '<div  style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid"  id="gridtable">';
            results += '<thead><tr><th scope="col"></th><th scope="col">Genaratorname</th><th scope="col">GenaratorCapacity</th><th scope="col">GenaratorPerhours</th></tr></thead></tbody>';
            for (var i = 0; i < msg.length; i++) {
                results += '<tr><td><input id="btn_poplate" type="button"  onclick="chemicalgetme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
                results += '<th scope="row" class="1" >' + msg[i].Genaratorname + '</th>';
                results += '<td data-title="uimstatus" class="2">' + msg[i].GenaratorCapacity + '</td>';
                results += '<td data-title="uimstatus" class="3">' + msg[i].GenaratorPerhours + '</td>';
                results += '<td style="display:none" class="7">' + msg[i].sno + '</td></tr>';
            }
            results += '</table></div>';
            $("#grid_ChillingData").html(results);
        }
        function chemicalgetme(thisid) {
            //  powerchangeclosing();
            var Genaratorname = $(thisid).parent().parent().children('.1').html();
            var GenaratorCapacity = $(thisid).parent().parent().children('.2').html();
            var GenaratorPerhours = $(thisid).parent().parent().children('.3').html();
            var sno = $(thisid).parent().parent().children('.4').html();

            document.getElementById('txtGenaratorname').value = Genaratorname;
            document.getElementById('txtGenaratorCapacity').value = GenaratorCapacity;
            document.getElementById('txtGenaratorPerhours').value = GenaratorPerhours;
            document.getElementById('txtCausticSoda').value = causticsoda;
            document.getElementById('lbl_sno').value = sno;
            document.getElementById('btn_save').value = "Modify";
            $('#div_Rechilling').show();
            $('#grid_ChillingData').show();
        }
        function ChillingMachinaryclearall() {
            document.getElementById('Slct_Plantcode').selectedIndex = 0;
            document.getElementById('txtGenaratorname').value = "";
            document.getElementById('txtGenaratorCapacity').value = "";
            document.getElementById('txtGenaratorPerhours').value = "";
            document.getElementById('lbl_sno').value = "";
            document.getElementById('btn_save').value = "Save";
            $('#div_Rechilling').show();
            $('#grid_ChillingData').show();
        }


        function saveSiloDetails() {
            var plant_code = document.getElementById('Slct_Plantcode2').value;
            if (plant_code == "" || plant_code == "Select plantName") {
                alert("Enter PlantName");
                return false;
            }
            var Silo1model = document.getElementById('txtSilo1').value;
            if (Silo1model == "") {

                alert("Enter Silo1 model");
                return false;
            }
            var Silo1capacity = document.getElementById('txtSilo1capacity').value;
            var Silo2model = document.getElementById('txtSilo2').value;
            if (Silo2model == "") {

                alert("Enter Silo2 model");
                return false;
            }
            var Silo2capacity = document.getElementById('txtSilo2capacity').value;

            var Silo3model = document.getElementById('txtSilo3').value;
            if (Silo3model == "") {

                alert("Enter Silo3 model");
                return false;
            }
            var Silo3capacity = document.getElementById('txtSilo3capacity').value;


            var sno = document.getElementById('lbl_sno').value;
            var btnval = document.getElementById('btn_silo').value;
            var data = { 'operation': 'saveSiloDetails', 'plant_code': plant_code, 'Silo1model': Silo1model, 'Silo1capacity': Silo1capacity, 'Silo2model': Silo2model, 'Silo2capacity': Silo2capacity, 'Silo3model': Silo3model, 'Silo3capacity': Silo3capacity, 'btnVal': btnval, 'sno': sno };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        alert(msg);
                        // get_PowerDetails();
                        Siloclearall();
                        $('#div_silo').show();
                        $('#grid_Silo').show();
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
            var data = { 'operation': 'get_SiloDetails', 'plant_code': plant_code };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillSiloDetails(msg);
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
        function fillSiloDetails(msg) {
            var results = '<div  style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid"  id="gridtable">';
            results += '<thead><tr><th scope="col"></th><th scope="col">Silo1model</th><th scope="col">Silo1capacity</th><th scope="col">Silo2model</th><th scope="col">Silo2capacity</th><th scope="col">Silo3model</th><th scope="col">Silo3capacity</th></tr></thead></tbody>';
            for (var i = 0; i < msg.length; i++) {
                results += '<tr><td><input id="btn_poplate" type="button"  onclick="silogetme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
                results += '<th scope="row" class="1" >' + msg[i].Silo1model + '</th>';
                results += '<td data-title="uimstatus" class="2">' + msg[i].Silo1capacity + '</td>';
                results += '<td data-title="uimstatus" class="3">' + msg[i].Silo2model + '</td>';
                results += '<td data-title="uimstatus" class="4">' + msg[i].Silo2capacity + '</td>';
                results += '<td data-title="uimstatus" class="5">' + msg[i].Silo3model + '</td>';
                results += '<td data-title="uimstatus" class="6">' + msg[i].Silo3capacity + '</td>';
                results += '<td style="display:none" class="7">' + msg[i].sno + '</td></tr>';
            }
            results += '</table></div>';
            $("#grid_Silo").html(results);
        }
        function silogetme(thisid) {
            //  powerchangeclosing();
            var Silo1model = $(thisid).parent().parent().children('.1').html();
            var Silo1capacity = $(thisid).parent().parent().children('.2').html();
            var Silo2model = $(thisid).parent().parent().children('.3').html();
            var Silo2capacity = $(thisid).parent().parent().children('.4').html();
            var Silo3model = $(thisid).parent().parent().children('.5').html();
            var Silo3capacity = $(thisid).parent().parent().children('.6').html();
            var sno = $(thisid).parent().parent().children('.7').html();

            document.getElementById('txtSilo1').value = Silo1model;
            document.getElementById('txtSilo1capacity').value = Silo1capacity;
            document.getElementById('txtSilo2').value = Silo2model;
            document.getElementById('txtSilo2capacity').value = Silo2capacity;
            document.getElementById('txtSilo3').value = Silo3model;
            document.getElementById('txtSilo3capacity').value = Silo3capacity;
            document.getElementById('lbl_sno').value = sno;
            document.getElementById('btn_silo').value = "Modify";
            $('#div_silo').show();
            $('#grid_Silo').show();
        }
        function Siloclearall() {
            document.getElementById('Slct_Plantcode').selectedIndex = 0;
            document.getElementById('txtSilo1').value = "";
            document.getElementById('txtSilo1capacity').value = "";
            document.getElementById('txtSilo2').value = "";
            document.getElementById('txtSilo2capacity').value = "";
            document.getElementById('txtSilo3').value = "";
            document.getElementById('txtSilo3capacity').value = "";
            document.getElementById('lbl_sno').value = "";
            document.getElementById('btn_silo').value = "Save";
            $('#div_silo').show();
            $('#grid_Silo').show();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <section class="content">
        <!-- Small boxes (Stat box) -->
        <div class="row">
            <section class="content-header">
                <h1>
                    Machinery Masters
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
                                <li id="id_tab_Personal" class="active"><a data-toggle="tab" href="#" onclick="showChillingMachinery()">
                                    <i class="fa fa-university" aria-hidden="true"></i>&nbsp;&nbsp;ChillingMachinery</a></li>
                                <li id="id_tab_documents" class=""><a data-toggle="tab" href="#" onclick="showCompressorMachinery()">
                                    <i class="fa fa-truck" aria-hidden="true"></i>&nbsp;&nbsp;CompressorMachinery</a></li>
                                     <li id="Li1" class=""><a data-toggle="tab" href="#" onclick="showSiloDetails()">
                                    <i class="fa fa-truck" aria-hidden="true"></i>&nbsp;&nbsp;SiloDetails</a></li>
                            </ul>
                        </div>
                        <div id="div_Rechilling" style="display: none;">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <i style="padding-right: 5px;" class="fa fa-cog"></i>Genarator Details
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
                                            <td>
                                                Genarator Name
                                            </td>
                                            <td>
                                                <input id="txtGenaratorname" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter Genaratorname" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Genarator Capacity
                                            </td>
                                            <td>
                                                <input id="txtGenaratorCapacity" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter GenaratorCapacity" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                 Genarator Perhours
                                            </td>
                                            <td>
                                                <input id="txtGenaratorPerhours" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter GenaratorPerhours"  />
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
                                                    onclick="saveChillingMachinaryDetails()" />
                                                <input id='btn_close' type="button" class="btn btn-danger" name="Close" value='Close'
                                                    onclick="ChillingMachinaryclearall()" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="grid_ChillingData">
                                </div>
                            </div>
                        </div>
                        <div id="div_Compressor" style="display: none;">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <i style="padding-right: 5px;" class="fa fa-cog"></i>Compressior Machinery Details
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
                                            <td>
                                                Compressior Name
                                            </td>
                                            <td>
                                                <input id="txtCompressiorName" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter CompressiorName" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Compressior Capacity
                                            </td>
                                            <td>
                                                 <input id="txtCompressiorcapacity" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter Compressiorcapacity" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                 Compressior Perhours
                                            </td>
                                            <td>
                                                <input id="txtCompressiorPerhours" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter CompressiorPerhours"  />
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
                                                <input id="btn_comprssior" type="button" class="btn btn-primary" name="submit" value='save'
                                                    onclick="saveCompressiormachinaryDetails()" />
                                                <input id='btn_closecompressior' type="button" class="btn btn-danger" name="Close" value='Close'
                                                    onclick="Compressiorclearall()" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="div_CompressiorGrid">
                                </div>
                            </div>
                        </div>
                  <div id="div_silo" style="display: none;">
                            <div class="box-header with-border">
                                <h3 class="box-title">
                                    <i style="padding-right: 5px;" class="fa fa-cog"></i>Silo Details
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
                                                <select id="Slct_Plantcode2" class="form-control" onchange="Routechangeclick2(this);">
                                                    <option selected disabled value="Select Plantcode">Select Plantcode</option>
                                                </select>
                                            </td>
                                        </tr>
                                       <tr>
                                            <td>
                                                Silo1 Name With Model
                                            </td>
                                            <td>
                                                 <input id="txtSilo1" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter Silo1 Name With Model" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                 Silo1 Capacity
                                            </td>
                                            <td>
                                                <input id="txtSilo1capacity" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter Silo1 Capacity" />
                                            </td>
                                        </tr>
                                     
                                       <tr>
                                            <td>
                                                Silo2 Name With Model
                                            </td>
                                            <td>
                                                 <input id="txtSilo2" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter Silo2 Name With Model" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                 Silo3 Capacity
                                            </td>
                                            <td>
                                                <input id="txtSilo2capacity" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter Silo3 Capacity"  />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Silo3 Name With Model
                                            </td>
                                            <td>
                                                 <input id="txtSilo3" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter  Silo3 Name With Model" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                 Silo3 Capacity
                                            </td>
                                            <td>
                                                <input id="txtSilo3capacity" type="text" maxlength="45" class="form-control" name="vendorcode"
                                                    placeholder="Enter Silo3capacity"  />
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
                                                <input id="btn_silo" type="button" class="btn btn-primary" name="submit" value='save'
                                                    onclick="saveSiloDetails()" />
                                                <input id='btn_closesilo' type="button" class="btn btn-danger" name="Close" value='Close'
                                                    onclick="Siloclearall()" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="grid_Silo">
                                </div>
                            </div>
                        </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
