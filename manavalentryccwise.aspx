<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="manavalentryccwise.aspx.cs" Inherits="manavalentryccwise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
<script type="text/javascript">

    $(function () {
        //get_RouteTimeMaintenance_details();
        get_Plant_Route_details();
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
        var opt = document.createElement('option');
        opt.innerHTML = "Select plantName";
        opt.value = "Select plantName";
        opt.setAttribute("selected", "selected");
        opt.setAttribute("disabled", "disabled");
        opt.setAttribute("class", "dispalynone");
        data.appendChild(opt);
        for (var i = 0; i < msg.length; i++) {
            if (msg[i].Plant_Code != null) {
                if (routedata.indexOf(msg[i].Plant_Code) == -1) {
                    var option = document.createElement('option');
                    option.innerHTML = msg[i].PlantName;
                    option.value = msg[i].Plant_Code;
                    data.appendChild(option);
                    routedata.push(msg[i].Plant_Code);
                }
            }

        }
    }
    function Routechangeclick() {
        var plantcode = document.getElementById('Slct_Plantcode').value;
        var data = document.getElementById('Select_Routeid');
        var length = data.options.length;
        document.getElementById('Select_Routeid').options.length = null;
        var opt = document.createElement('option');
        opt.innerHTML = "Select RouteName";
        opt.value = "Select RouteName";
        opt.setAttribute("selected", "selected");
        opt.setAttribute("disabled", "disabled");
        opt.setAttribute("class", "dispalynone");
        data.appendChild(opt);
        for (var i = 0; i < routedetailes.length; i++) {
            if (plantcode == routedetailes[i].Plant_Code) {
                var option = document.createElement('option');
                option.innerHTML = routedetailes[i].RouteName;
                option.value = routedetailes[i].Route_id;
                data.appendChild(option);
            }
        }
    }
    function Save_Agent_samples_entry() {
        var plant_code = document.getElementById('Slct_Plantcode').value;
        if (plant_code == "" || plant_code == "Select plantName") {
            alert("Enter PlantName");
            return false;
        }
        var Route_id = document.getElementById('Select_Routeid').value;
        if (Route_id == "" || Route_id == "Select RouteName") {
            alert("Enter RouteName");
            return false;
        }
        var agentid = document.getElementById('txt_agent').value;
        if (agentid == "") {
            alert("Enter AgentId");
            return false;
        }
        var date = document.getElementById('txtDate').value;
        if (date == "") {
            alert("Select Date");
            return false;
        }
        var session = document.getElementById('ddlsession').value;
        if (session == "" || session == "Select Session") {
            alert("Select Session");
            return false;
        }
        var fat = document.getElementById('txt_fat').value;
        if (fat == "") {
            alert("Enter Fat");
            return false;
        }
        var snf = document.getElementById('txt_snf').value;
        if (snf == "") {
            alert("Enter Snf");
            return false;
        }
        var clr = document.getElementById('txt_clr').value;
        if (clr == "") {
            alert("Enter Clr");
            return false;
        }

        var milkqty = document.getElementById('txt_ltr').value;
        if (milkqty == "") {
            alert("Enter milkqty");
            return false;
        }


        var SAMPLENO = document.getElementById('txt_sampleno').value;
        if (SAMPLENO == "") {
            alert("Enter SAMPLENO");
            return false;
        }

        var NOOFCANS = document.getElementById('txt_cans').value;
        if (NOOFCANS == "") {
            alert("Enter NOOFCANS");
            return false;
        }
       var milknature = document.getElementById('slct_mn').value;
       if (milknature == "" || milknature == "Select Milktype") {
           alert("Select milk nature");
           return false;
       }
        var starttime = document.getElementById('txtDairyTime').value;
        var intime = document.getElementById('txtInTime').value;
        var sno = document.getElementById('lbl_sno').value;
        var btnval = document.getElementById('btn_save').value;
        var data = { 'operation': 'Save_Agent_samples_procurement', 'plant_code': plant_code, 'Route_id': Route_id, 'date': date, 'session': session, 'agentid': agentid, 'fat': fat, 'snf': snf, 'clr': clr, 'milkqty': milkqty, 'NOOFCANS': NOOFCANS, 'SAMPLENO': SAMPLENO, 'starttime': starttime, 'intime': intime, 'milknature': milknature, 'btnval': btnval, 'sno': sno };
        var s = function (msg) {
            if (msg) {
                if (msg.length > 0) {
                    alert(msg);
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


    function Routechangeclick1(Select_Routeid) {
        var plant_code = document.getElementById('Slct_Plantcode').value;
        var Route_id = document.getElementById('Select_Routeid').value;
        var data = { 'operation': 'get_Agent_details', 'plantcode': plant_code, 'Route_id': Route_id };
        var s = function (msg) {
            if (msg) {
                if (msg.length > 0) {
                    fillagentdetails(msg);

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

    function fillagentdetails(msg) {
        var routeid = document.getElementById('Select_Routeid').value;
        var data = document.getElementById('slct_agent');
        var length = data.options.length;
        document.getElementById('slct_agent').options.length = null;
        var opt = document.createElement('option');
        opt.innerHTML = "Select Agent";
        opt.value = "Select Agent";
        opt.setAttribute("selected", "selected");
        opt.setAttribute("disabled", "disabled");
        opt.setAttribute("class", "dispalynone");
        data.appendChild(opt);
        for (var i = 0; i < msg.length; i++) {
            if (routeid == msg[i].routeid) {
                var option = document.createElement('option');
                option.innerHTML = routedetailes[i].Agent_Name;
                option.value = routedetailes[i].Agent_Id;
                data.appendChild(option);
            }
        }
    }


    //    function fillrotetimemaintainenace(msg) {
    //        var results = '<div class="divcontainer" style="overflow:auto;"><table class="responsive-table">';
    //        results += '<thead><tr><th scope="col"></th><th scope="col">PlantName</th><th scope="col">RouteName</th><th scope="col">VehicleStartTime</th><th scope="col">VehicleEndTime</th><th scope="col">MobNo</th></tr></thead></tbody>';
    //        for (var i = 0; i < msg.length; i++) {
    //            results += '<tr><td><input id="btn_poplate" type="button"  onclick="getme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
    //            // results += '<th scope="row" class="1" style="text-align:center;">' + msg[i].name + '</th>';
    //            results += '<td data-title="PlantName"  class="1">' + msg[i].PlantName + '</td>';
    //            results += '<td data-title="RouteName" class="2">' + msg[i].RouteName + '</td>';
    //            results += '<td data-title="Plant_Code" style="display:none" class="3">' + msg[i].Plant_Code + '</td>';
    //            results += '<td data-title="Route_id"  style="display:none"  class="4">' + msg[i].Route_id + '</td>';
    //            results += '<td data-title="VehicleSettime"  class="5">' + msg[i].VehicleSettime + '</td>';
    //            results += '<td data-title="VehicleInttime" class="6">' + msg[i].VehicleInttime + '</td>';
    //            results += '<td data-title="MBRT" style="display:none" class="7">' + msg[i].MBRT + '</td>';
    //            results += '<td data-title="Date"  style="display:none" class="8">' + msg[i].Date + '</td>';
    //            results += '<td data-title="Session"  style="display:none" class="9">' + msg[i].Session + '</td>';
    //            results += '<td data-title="agentid" style="display:none" class="10">' + msg[i].sno + '</td></tr>';
    //        }
    //        results += '</table></div>';
    //        $("#div_RouteTimeMaintain").html(results);
    //    }


    function fillrotetimemaintainenace(msg) {
        var results = '<div  style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid">';
        results += '<thead><tr><th scope="col"></th><th scope="col">Date</th><th scope="col">VehicleStartTime</th><th scope="col">VehicleEndTime</th><th scope="col">MBRT</th></tr></thead></tbody>';
        //results += '<tr><th scope="col"></th><th scope="col">PlantName</th><th scope="col">RouteName</th><th scope="col">VehicleStartTime</th><th scope="col">VehicleEndTime</th><th scope="col">MobNo</th></tr></thead></tbody>';
        for (var i = 0; i < msg.length; i++) {
            results += '<tr><td><input id="btn_poplate" type="button"  onclick="getme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
            // results += '<th scope="row" class="1" >' + msg[i].PlantName + '</th>';
            //results += '<td data-title="code" class="2">' + msg[i].RouteName + '</td>';
            results += '<td   data-title="status" class="8">' + msg[i].Date + '</td>';
            results += '<td data-title="status" class="5">' + msg[i].VehicleSettime + '</td>';
            results += '<td  data-title="status" class="6">' + msg[i].VehicleInttime + '</td>';
            results += '<td data-title="status" style="display:none" class="3">' + msg[i].Plant_Code + '</td>';
            results += '<td style="display:none" style="display:none" data-title="status" class="4">' + msg[i].Route_id + '</td>';
            results += '<td data-title="status"  class="7">' + msg[i].MBRT + '</td>';
            results += '<td data-title="status" style="display:none" class="9">' + msg[i].Session + '</td>';
            results += '<td style="display:none" class="10">' + msg[i].sno + '</td></tr>';
        }
        results += '</table></div>';
        $("#div_RouteTimeMaintain").html(results);
    }

    function getme(thisid) {

        var PlantName = $(thisid).parent().parent().children('.1').html();
        var RouteName = $(thisid).parent().parent().children('.2').html();
        var Plant_Code = $(thisid).parent().parent().children('.3').html();
        var Route_id = $(thisid).parent().parent().children('.4').html();
        var VehicleSettime = $(thisid).parent().parent().children('.5').html();
        var VehicleInttime = $(thisid).parent().parent().children('.6').html();
        var MBRT = $(thisid).parent().parent().children('.7').html();
        var Date = $(thisid).parent().parent().children('.8').html();
        var Session = $(thisid).parent().parent().children('.9').html();
        var sno = $(thisid).parent().parent().children('.10').html();
        document.getElementById('Slct_Plantcode').value = Plant_Code;
        Routechangeclick();
        document.getElementById('Select_Routeid').value = Route_id;
        // document.getElementById('txtDate').value = Date;
        document.getElementById('ddlsession').value = Session;
        document.getElementById('txtDairyTime').value = VehicleSettime;
        document.getElementById('txtInTime').value = VehicleInttime;
        document.getElementById('txtMbrt').value = MBRT;
        document.getElementById('lbl_sno').value = sno;
        document.getElementById('btn_save').value = "Modify";

    }

    function forclearall() {
        document.getElementById('Slct_Plantcode').selectedIndex = 0;
        document.getElementById('Select_Routeid').selectedIndex = "";
        document.getElementById('txtDate').value = "";
        document.getElementById('ddlsession').value = "";
        document.getElementById('txtDairyTime').value = 0;
        document.getElementById('txtInTime').value = 0;
        document.getElementById('txtMbrt').value = 0;
        document.getElementById('btn_save').value = "Save";

    }


 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<section class="content-header">
        <h1>
            RouteTimeMaintenance Details<small>Preview</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Basic Information</a></li>
            <li><a href="#">RouteTimeMaintenance</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <i style="padding-right: 5px;" class="fa fa-cog"></i>RouteTimeMaintenance Details
                </h3>
            </div>
            <div>
                <table id="tbl_leavemanagement" align="center">
                    <tr>
                        <td style="height: 40px;">
                            PlantName
                        </td>
                        <td style="height: 40px;">
                            <select id="Slct_Plantcode" class="form-control" onchange="Routechangeclick(this);">
                                <option selected disabled value="Select Plantcode">Select Plantcode</option>
                            </select>
                        </td>
                         <td style="width: 8px;">
                        </td>
                        <td style="width: 8px;">
                        </td>
                          <td style="height: 40px;">
                            RouteName
                        </td>
                        <td>
                            <select id="Select_Routeid" class="form-control">
                                <option selected disabled value="Select Type">Select Route ID</option>
                            </select>
                        </td>

                        <td style="width: 8px;">
                        </td>
                        <td style="width: 8px;">
                        </td>
                          <td style="height: 40px;">
                            Agent ID
                        </td>
                        <td>
                        <input type="text" class="form-control" id="txt_agent" placeholder="Enter Aget ID" />
                        </td>
                    </tr>
                    <tr>
                       <td style="height: 40px;">
                            Date
                        </td>
                        <td style="height: 40px;">
                            <input type="date" class="form-control" id="txtDate" placeholder="Enter Cortege" />
                        </td>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 8px;">
                        </td>
                        <td style="height: 40px;">
                            Session
                        </td>
                        <td style="height: 40px;">
                            <select id="ddlsession" class="form-control">
                                <option selected disabled value="Select Session">Select Session</option>
                                <option value="AM">AM</option>
                                <option value="PM">PM</option>
                            </select>
                        </td>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 8px;">
                        </td>
                        <td style="height: 40px;" id="Td3">
                            Milk_qty
                        </td>
                        <td style="height: 40px;">
                            <input type="text" class="form-control" id="txt_ltr" placeholder="Enter qty" />
                        </td>
                    </tr>


                    <tr>
                       <td style="height: 40px;">
                            fat
                        </td>
                        <td style="height: 40px;">
                            <input type="text" class="form-control" id="txt_fat" placeholder="Enter fat" />
                        </td>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 8px;">
                        </td>
                        <td style="height: 40px;">
                            Snf
                        </td>
                        <td style="height: 40px;">
                            <input type="text" class="form-control" id="txt_snf" placeholder="Enter snf" />
                        </td>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 8px;">
                        </td>
                        <td style="height: 40px;" id="Td4">
                            clr
                        </td>
                        <td style="height: 40px;">
                            <input type="text" class="form-control" id="txt_clr" placeholder="Enter Clr" />
                        </td>
                    </tr>


                    <tr>
                       <td style="height: 40px;">
                            Noofcans
                        </td>
                        <td style="height: 40px;">
                            <input type="text" class="form-control" id="txt_cans" placeholder="Enter cans" />
                        </td>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 8px;">
                        </td>
                        <td style="height: 40px;">
                            Milk Nature
                        </td>
                        <td style="height: 40px;">
                            <select id="slct_mn" class="form-control">
                                <option selected disabled value="Select Session">Select Milktype</option>
                                <option value="1">Cow</option>
                                <option value="2">Buffalow</option>
                            </select>
                        </td>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 8px;">
                        </td>
                        <td style="height: 40px;" id="Td5">
                            Sample No
                        </td>
                        <td style="height: 40px;">
                            <input type="text" class="form-control" id="txt_sampleno" placeholder="Enter SAMPLENO" />
                        </td>
                    </tr>

                    <tr>
                    <td style="height: 40px;">
                            Dairy Time
                        </td>
                        <td style="height: 40px;">
                            <input type="text" class="form-control" id="txtDairyTime" placeholder="Enter Dairy Time" />
                        </td>
                        <td style="width: 8px;">
                        </td>
                        <td style="width: 8px;">
                        </td>
                        <td style="height: 40px;" id="lblbranch">
                            End Time
                        </td>
                        <td style="height: 40px;">
                            <input type="text" class="form-control" id="txtInTime" placeholder="Enter In Time" />
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                        </td>
                        <td style="height: 40px;">
                            <input id="btn_save" type="button" class="btn btn-primary" name="submit" value="Save"
                                onclick="Save_Agent_samples_entry();">
                            <input id="btn_close" type="button" class="btn btn-danger" name="submit" value="Cancel"
                                onclick="forclearall();">
                        </td>
                    </tr>
                    <tr hidden>
                        <td>
                            <label id="lbl_sno">
                            </label>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <div id="div_RouteTimeMaintain">
                </div>
            </div>
        </div>
    </section>
</asp:Content>

