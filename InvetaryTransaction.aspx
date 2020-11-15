<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="InvetaryTransaction.aspx.cs" Inherits="InvetaryTransaction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
 <link href="autocomplete/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="Js/JTemplate.js?v=3004" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            get_Plant_Route_details();
            get_InvetaryMaster_details();
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

        function Plantchangeclick(Slct_Plantcode) {
            var plantcode = document.getElementById('Slct_Plantcode').value;
            var data = { 'operation': 'get_Agent_details', 'plantcode': plantcode };
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
        var compiledList1 = [];
        function fillagentdetails(msg) {
            //var compiledList = [];
            for (var i = 0; i < msg.length; i++) {
                var Agent_Id = msg[i].Agent_Id;
                compiledList1.push(Agent_Id);
            }

            $('#txtAgent').autocomplete({
                source: compiledList1,
                change: Routechangeclick1,
                autoFocus: true
            });
        }

        function get_radio_value() {
            var inputs = document.getElementsByName("selected");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].checked) {
                    return inputs[i].value;
                }
            }
        }



        var fillsubcatstock1 = []; var emptydata = [];
        function get_InvetaryMaster_details() {
           // var category = document.getElementById('ddlcantype').value;
            var data = { 'operation': 'get_InvetaryMaster_details' };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillinventary(msg);
                        fillinventary1 = msg;
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

        function fillinventary(msg) {
            fillinventary1 = msg;
            var data = document.getElementById('ddlinventaryname');
            var length = data.options.length;
            document.getElementById('ddlinventaryname').options.length = null;
            for (var i = 0; i < fillinventary1.length; i++) {
                if (fillinventary1[i].inventaryname != null) {
                    var option = document.createElement('option');
                    option.innerHTML = fillinventary1[i].inventaryname;
                    option.value = fillinventary1[i].inventorysno;
                    data.appendChild(option);
                  //document.getElementById('txtavilable').innerHTML = fillsubcatstock1[i].moniterqty;
                }
                changeqty(ddlinventaryname);
            }
        }
        function changeqty(ddlinventaryname) {
            var sqty = document.getElementById('ddlinventaryname').value;
            for (var i = 0; i < fillinventary1.length; i++) {
                if (sqty == fillinventary1[i].inventorysno) {
                    document.getElementById('txtavilable').innerHTML = fillinventary1[i].monitorqty;
                }
            }
        }
        function changeqtyvalidation() {
            var availqty = document.getElementById('txtavilable').innerHTML;
            var stockquantity = document.getElementById('txtNocans').value;
            if (stockquantity <= availqty) {
                alert("Sufficient Quantity Is Not There Please Give Sufficient Quantity ");
            }
        }

        function Save_Inventary_Transaction() {
            var plant_code = document.getElementById('Slct_Plantcode').value;
            if (plant_code == "" || plant_code == "Select plantName") {
                alert("Enter PlantName");
                return false;
            }
            var agentid = document.getElementById('txtAgent').value;
            if (agentid == "") {
                alert("Enter Agent Id");
                return false;
            }
            var date = document.getElementById('txtDate').value;
            //            if (date == "") {
            //                alert("Select Date");
            //                return false;
            //            }
          //  var canid = document.getElementById('ddlcantype').value;
            //            if (can == "" || can == "Select CanType") {
            //                alert("Select CanType");
            //                return false;
            //            }
            var reciverorissuername = document.getElementById('txtIssueerName').value;
            var qty = document.getElementById('txtNocans').value;
            //  var mbrt = document.getElementById('txtMbrt').value;

            var isssueorreceive = get_radio_value();
            //            if (isssueorreceive == "") {

            //                alert("Please Issue Or  Recieve");
            //            }

            var inventaryname = document.getElementById('ddlinventaryname').value;
            if (inventaryname == "") {
                alert("Select InventaryName");
                return false; ddlinventaryname
            }
            var sno = document.getElementById('lbl_sno').value;
            var btnval = document.getElementById('btn_save').value;
            var data = { 'operation': 'Save_Inventary_Transaction', 'plant_code': plant_code, 'agentid': agentid, 'date': date, 'reciverorissuername': reciverorissuername, 'qty': qty, 'isssueorreceive': isssueorreceive, 'inventaryname': inventaryname, 'btnval': btnval, 'sno': sno };
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
        function Routechangeclick1() {
            var plant_code = document.getElementById('Slct_Plantcode').value;
            var agentid = document.getElementById('txtAgent').value;
            var data = { 'operation': 'get_Inventary_Transaction_details', 'plant_code': plant_code, 'agentid': agentid };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillrotetimemaintainenace(msg);

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
        function fillrotetimemaintainenace(msg) {
            var results = '<div  style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid">';
            results += '<thead><tr><th scope="col"></th><th scope="col">Date</th><th scope="col">VehicleStartTime</th><th scope="col">VehicleEndTime</th><th scope="col">MBRT</th></tr></thead></tbody>';
            //results += '<tr><th scope="col"></th><th scope="col">PlantName</th><th scope="col">RouteName</th><th scope="col">VehicleStartTime</th><th scope="col">VehicleEndTime</th><th scope="col">MobNo</th></tr></thead></tbody>';
            for (var i = 0; i < msg.length; i++) {
                results += '<tr><td><input id="btn_poplate" type="button"  onclick="getme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
                // results += '<th scope="row" class="1" >' + msg[i].PlantName + '</th>';
                //results += '<td data-title="code" class="2">' + msg[i].RouteName + '</td>';
                results += '<td   data-title="status" class="3">' + msg[i].Plant_Code + '</td>';
                results += '<td data-title="status" class="4">' + msg[i].Agentid + '</td>';
                results += '<td  data-title="status" class="5">' + msg[i].date + '</td>';
                results += '<td data-title="status" style="display:none" class="6">' + msg[i].canid + '</td>';
                results += '<td style="display:none" style="display:none" data-title="status" class="7">' + msg[i].reciverorissuername + '</td>';
                results += '<td data-title="status"  class="8">' + msg[i].qty + '</td>';
                results += '<td style="display:none" class="9">' + msg[i].sno + '</td></tr>';
            }
            results += '</table></div>';
            $("#div_Invetarytransaction").html(results);
        }



        function getme(thisid) {

            var PlantName = $(thisid).parent().parent().children('.1').html();
            var AgentName = $(thisid).parent().parent().children('.2').html();
            var Plant_Code = $(thisid).parent().parent().children('.3').html();
            var Agentid = $(thisid).parent().parent().children('.4').html();
            var date = $(thisid).parent().parent().children('.5').html();
            var canid = $(thisid).parent().parent().children('.6').html();
            var reciverorissuername = $(thisid).parent().parent().children('.7').html();
            var qty = $(thisid).parent().parent().children('.8').html();
            var sno = $(thisid).parent().parent().children('.9').html();

            document.getElementById('Slct_Plantcode').value = Plant_Code;
            Routechangeclick();
            document.getElementById('txtAgent').value = Route_id;
            document.getElementById('txtDate').value = Date;
            //document.getElementById('ddlcantype').value = Session;
            document.getElementById('txtIssueerName').value = VehicleSettime;
            document.getElementById('txtNocans').value = VehicleInttime;
            document.getElementById('lbl_sno').value = sno;
            document.getElementById('btn_save').value = "Modify";
        }

        function forclearall() {
            document.getElementById('Slct_Plantcode').selectedIndex = 0;
            document.getElementById('txtAgent').value = "";
            document.getElementById('txtDate').value = "";
           // document.getElementById('ddlcantype').selectedIndex = 0;
            document.getElementById('txtIssueerName').value = 0;
            document.getElementById('txtNocans').value = 0;
            document.getElementById('btn_save').value = "Save";

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <section class="content-header">
        <h1>
            InventaryTransaction Details<small>Preview</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Basic Information</a></li>
            <li><a href="#">InventaryTransaction Details</a></li>
        </ol>
    </section>--%>
    <div class="box box-info">
        <div class="box-header with-border">
            <h3 class="box-title">
                <i style="padding-right: 5px;" class="fa fa-cog"></i>InventaryTransaction Details
            </h3>
        </div>
        <div>
            <table id="rdolst" align="center">
                <tr>
                    <td>
                        <input id="Rdoissue" type="radio" name="selected" value="1" checked="checked" /><label
                            for="Rdoissue">Issue
                        </label>
                    </td>
                    <td>
                        <input id="rdoReceive" type="radio" name="selected" value="2" /><label for="rdoReceive">Recieve</label>
                    </td>
                </tr>
            </table>
            <%-- <td>
                        <label>
                            Issueing
                        </label>
                    </td>
                    <td>
                        <input id="Rdoissue" type="checkbox" onchange="Rdoissue_CheckedChanged" text="Issuing"
                            class="form-control" />
                    </td>
                      <td>
                        <label>
                            Receiving
                        </label>
                    </td>
                    <td>
                        <input id="rdoReceive" type="checkbox" onchange="rdoReceive_CheckedChanged" class="form-control" />
                    </td>--%>
            <%--            </tr>--%>
            <%--  <tr>
                    <td>
                        <label>
                            Cans
                        </label>
                    </td>
                    <td>
                        <input id="chk_can" type="checkbox" onchange="chk_can_CheckedChanged" class="form-control" />
                    </td>
                    <td>
                        <label>
                            Dpu
                        </label>
                    </td>
                    <td>
                        <input id="chk_dpu" type="checkbox" onchange="chk_dpu_CheckedChanged" class="form-control" />
                    </td>
                </tr>--%>
            <table id="tbl_leavemanagement" align="center">
                <tr>
                    <td>
                        <label>
                            Plant Name
                        </label>
                    </td>
                    <td style="height: 40px;">
                        <select id="Slct_Plantcode" class="form-control" onchange="Plantchangeclick(this);">
                            <option selected disabled value="Select plantName">Select plantName</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            AgentId/Canno
                        </label>
                    </td>
                    <td>
                        <input type="text" class="form-control" id="txtAgent" placeholder="Enter Agent id"
                            />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Date
                        </label>
                    </td>
                    <td style="height: 40px;">
                        <input type="date" class="form-control" id="txtDate" placeholder="Enter Cortege" />
                    </td>
                </tr>
                <%--<tr>
                    <td style="height: 40px;">
                        <label>
                            Can Type
                        </label>
                    </td>
                    <td style="height: 40px;">
                        <select id="ddlcantype" class="form-control" onchange="getmaterialwise(this);">
                            <option value="Select Type" disabled selected>Select Type</option>
                            <option value="1">Feed</option>
                            <option value="2">Dpu</option>
                             <option value="3">Meterials</option>
                             <option value="4">Can</option>
                        </select>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <label>
                            Inventary Name
                        </label>
                    </td>
                    <td>
                    <select id="ddlinventaryname" class="form-control" onchange="changeqty(this);">
                        <option selected disabled value="Select CanType">Select InventaryName</option>
                    </select>
                    </td>
                </tr>
                <%--    <tr>
                    <td>
                        <label>
                            DpuType
                        </label>
                    </td>
                    <td>
                        <select id="ddldputype" class="form-control">
                            <option selected disabled value="Select Plantcode">Select DpuType</option>
                        </select>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <label>
                            Issuer/Receiver Name
                        </label>
                    </td>
                    <td style="height: 40px;">
                        <input type="text" class="form-control" id="txtIssueerName" placeholder="Enter Cortege" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            Number of cans
                        </label>
                    </td>
                    <td style="height: 40px;">
                        <input type="text" class="form-control" id="txtNocans" placeholder="Enter Number of Cans"
                            onchange="changeqtyvalidation();" />
                    </td>
                    <td style="width: 100px;">
                    </td>
                    <td style="width: 100px;">
                        <span class="form-control" id="txtavilable" />
                    </td>
                </tr>
                <%--   <tr>
                    <td>
                        Number of Dpus
                    </td>
                    <td style="height: 40px;">
                        <input type="text" class="form-control" id="txtNoDpu" placeholder="Enter Cortege" />
                    </td>
                </tr>--%>
                <tr>
                    <td>
                    </td>
                    <td style="height: 40px;">
                        <input id="btn_save" type="button" class="btn btn-primary" name="submit" value="Save"
                            onclick="Save_Inventary_Transaction();">
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
            <div id="div_Invetarytransaction">
            </div>
        </div>
    </div>
    <%--  </section>--%>
</asp:Content>
