<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="compressionruntimehours.aspx.cs" Inherits="compression_run_time_hours" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
    <%--    <style>
        .form-control
        {
            border: 2px solid rgb(173, 204, 204);
            height: 21px;
            width: 200px;
            box-shadow: 0 0 27px rgb(204, 204, 204) inset;
            padding: 3px 3px 3px 3px;
            text-align: center;
        }
        
        .button
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            background-color: #4CAF50; /* Green */
            color: white;
            padding: 10px 26px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: medium;
            margin: 4px 2px;
            cursor: pointer;
            font-weight: 700;
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
  </style>--%>
    <script type="text/javascript">
        $(function () {
            //            get_comruntimehours_details();
            //            get_milkrechilling_details();
            showdivcompressiondesign();
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




            for (var i = 0; i < msg.length; i++) {
                if (msg[i].Plant_Code != null) {
                    if (routedata.indexOf(msg[i].Plant_Code) == -1) {
                        var option = document.createElement('option');
                        var option1 = document.createElement('option');

                        option.innerHTML = msg[i].PlantName;
                        option.value = msg[i].Plant_Code;

                        option1.innerHTML = msg[i].PlantName;
                        option1.value = msg[i].Plant_Code;
                        data.appendChild(option);
                        data1.appendChild(option1);
                        routedata.push(msg[i].Plant_Code);
                    }
                }

            }
        }
        function save_comruntimehours_click() {
            var plant_code = document.getElementById('Slct_Plantcode').value;
            if (plant_code == "" || plant_code == "Select plantName") {
                alert("Enter PlantName");
                return false;
            }
            var Session = document.getElementById('selct_sessionCompression').value;
            var StartTime = document.getElementById('txt_starttime').value;
            var IBTTemp = document.getElementById('txt_ibt').value;
            var EndTime = document.getElementById('txt_endtime').value;
            var IBTTemp1 = document.getElementById('txtibt').value;
            if (Session == "") {
                alert("Enter Session ");
                return false;

            }
            //            if (StartTime == "") {
            //                alert("Enter StartTime ");
            //                return false;

            //            }
            if (IBTTemp == "") {
                alert("Enter IBTTemp ");
                return false;

            }
            //            if (EndTime == "") {
            //                alert("Enter EndTime ");
            //                return false;

            //            }
            if (IBTTemp1 == "") {
                alert("Enter IBTTemp ");
                return false;

            }
            var sno = document.getElementById('lbl_sno').value;
            var CompressionDate = document.getElementById('txtCompressionDate').value;
            var btnval = document.getElementById('btn_save').value;
            var data = { 'operation': 'save_comruntimehours_click', 'plant_code': plant_code, 'Session': Session, 'StartTime': StartTime, 'IBTTemp': IBTTemp, 'EndTime': EndTime, 'IBTTemp1': IBTTemp1, 'CompressionDate': CompressionDate, 'btnVal': btnval, 'sno': sno };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        alert(msg);
                        //get_comruntimehours_details();
                        $('#div_CompressionData').show();
                        $('#divcompression').show();

                        cancelcompresiondetails();
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
            var data = { 'operation': 'get_comruntimehours_details', 'plant_code': plant_code };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillcompression(msg);
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
        function fillcompression(msg) {
            var results = '<div class="divcontainer" style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer">';
            results += '<thead><tr><th scope="col"></th><th scope="col">Session</th><th scope="col">StartTime</th><th scope="col">IBTTemp</th><th scope="col">EndTime</th><th scope="col">IBTTemp</th></tr></thead></tbody>';
            for (var i = 0; i < msg.length; i++) {
                results += '<tr><td><input id="btn_poplate" type="button"  onclick="getme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
                results += '<th scope="row" class="1" style="text-align:center;">' + msg[i].Session + '</th>';
                results += '<td  class="2">' + msg[i].StartTime + '</td>';
                results += '<td  class="3">' + msg[i].IBTTemp + '</td>';
                results += '<td  class="4">' + msg[i].EndTime + '</td>';
                results += '<td  class="6">' + msg[i].IBTTemp1 + '</td>';
                results += '<td style="display:none" class="7">' + msg[i].CompressionDate + '</td>';
                results += '<td style="display:none" class="5">' + msg[i].sno + '</td></tr>';

            }
            results += '</table></div>';
            $("#div_CompressionData").html(results);
        }
        function getme(thisid) {
            var Session = $(thisid).parent().parent().children('.1').html();
            var StartTime = $(thisid).parent().parent().children('.2').html();
            var IBTTemp = $(thisid).parent().parent().children('.3').html();
            var EndTime = $(thisid).parent().parent().children('.4').html();
            var IBTTemp1 = $(thisid).parent().parent().children('.6').html();
            var CompressionDate = $(thisid).parent().parent().children('.7').html();
            var sno = $(thisid).parent().parent().children('.5').html();

            document.getElementById('selct_sessionCompression').value = Session;
            document.getElementById('txt_starttime').value = StartTime;
            document.getElementById('txtCompressionDate').value = CompressionDate;
            document.getElementById('txt_ibt').value = IBTTemp;
            document.getElementById('txt_endtime').value = EndTime;
            document.getElementById('txtibt').value = IBTTemp1;
            document.getElementById('lbl_sno').value = sno;
            document.getElementById('btn_save').value = "Modify";
            $('#div_CompressionData').show();
            $('#divcompression').show();
        }

        function showdivcompressiondesign() {
            $("#divcompression").css("display", "block");
            $("#divmilk").css("display", "none");
        }
        function showdivmilkrechillingdesign() {
            $("#divmilk").css("display", "block");
            $("#divcompression").css("display", "none");
        }


        function savemilkrechilling() {
            var plant_code = document.getElementById('Slct_Plantcode1').value;
            if (plant_code == "" || plant_code == "Select plantName") {
                alert("Enter PlantName");
                return false;
            }
            var Session = document.getElementById('SessionMilkRechillin').value;
            var ONTime = document.getElementById('txtontime').value;
            var OFFTime = document.getElementById('txtofftime').value;
            var TotalLiters = document.getElementById('Txtttl').value;
            if (Session == "") {
                alert("Enter Session ");
                return false;

            }
            //            if (ONTime == "") {
            //                alert("Enter ONTime ");
            //                return false;

            //            }
            //            if (OFFTime == "") {
            //                alert("Enter OFFTime ");
            //                return false;

            //            }
            if (TotalLiters == "") {
                alert("Enter TotalLiters ");
                return false;

            }
            var RechillingDate = document.getElementById('txtRechillingDate').value;
            var sno = document.getElementById('lbl_sno').value;
            var btnval = document.getElementById('btn_save1').value;
            var data = { 'operation': 'savemilkrechilling', 'plant_code': plant_code, 'Session': Session, 'ONTime': ONTime, 'OFFTime': OFFTime, 'TotalLiters': TotalLiters, 'RechillingDate': RechillingDate, 'btnVal': btnval, 'sno': sno };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        alert(msg);
                        // get_milkrechilling_details();
                        $('#div_milkdata').show();
                        $('#divmilk').show();
                        cancelmilkdetails();
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
            var data = { 'operation': 'get_milkrechilling_details', 'plant_code': plant_code };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillmilkrechillingdetails(msg);
                        //                    fillcompression(msg);
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
        function fillmilkrechillingdetails(msg) {
            var results = '<div class="divcontainer" style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer">';
            results += '<thead><tr><th scope="col"></th><th scope="col">Session</th><th scope="col">ONTime</th><th scope="col">OFFTime</th><th scope="col">TotalLiters</th></tr></thead></tbody>';
            for (var i = 0; i < msg.length; i++) {
                results += '<tr><td><input id="btn_poplate" type="button"  onclick="getmilk(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
                results += '<th scope="row" class="1" style="text-align:center;">' + msg[i].Session + '</th>';
                results += '<td class="2">' + msg[i].ONTime + '</td>';
                results += '<td  class="3">' + msg[i].OFFTime + '</td>';
                results += '<td  class="4">' + msg[i].TotalLiters + '</td>';
                results += '<td style="display:none"  class="5">' + msg[i].RechillingDate + '</td>';
                results += '<td style="display:none" class="6">' + msg[i].sno + '</td></tr>';

            }
            results += '</table></div>';
            $("#div_milkdata").html(results);
            $("#Div_fillmilk").show();
        }
        function getmilk(thisid) {
            var Session = $(thisid).parent().parent().children('.1').html();
            var ONTime = $(thisid).parent().parent().children('.2').html();
            var OFFTime = $(thisid).parent().parent().children('.3').html();
            var TotalLiters = $(thisid).parent().parent().children('.4').html();
            var RechillingDate = $(thisid).parent().parent().children('.5').html();
            var sno = $(thisid).parent().parent().children('.6').html();

            document.getElementById('SessionMilkRechillin').value = Session;
            document.getElementById('txtontime').value = ONTime;
            document.getElementById('txtRechillingDate').value = RechillingDate;
            document.getElementById('txtofftime').value = OFFTime;
            document.getElementById('Txtttl').value = TotalLiters;
            document.getElementById('lbl_sno').value = sno;
            document.getElementById('btn_save1').value = "Modify";
            //document.getElementById('lbl_Bankid).value = Bankid;
            //document.getElementById('ddlstatus').value = status;
            // $("#div_BankData").hide();
            $("#div_milkdata").show();
            $("#divmilk").show();



        }
        function cancelcompresiondetails() {
            document.getElementById('selct_sessionCompression').value = "";
            document.getElementById('txt_starttime').value = "";
            document.getElementById('txt_ibt').value = "";
            document.getElementById('txtCompressionDate').value = "";
            document.getElementById('txt_endtime').value = "";
            document.getElementById('txtibt').value = "";
            document.getElementById('lbl_sno').selectedIndex = 0;
            document.getElementById('btn_save').value = "save";
            $("#lbl_code_error_msg").hide();
            $("#lbl_name_error_msg").hide();
        }
        function cancelmilkdetails() {

            document.getElementById('SessionMilkRechillin').value = "";
            document.getElementById('txtontime').value = "";
            document.getElementById('txtRechillingDate').value = "";
            document.getElementById('txtofftime').value = "";
            document.getElementById('Txtttl').value = "";
            document.getElementById('lbl_sno').selectedIndex = 0;
            document.getElementById('btn_save1').value = "save";
            $("#lbl_code_error_msg").hide();
            $("#lbl_name_error_msg").hide();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>
            CompressionRunTimeHours
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Operation</a></li>
            <li><a href="#">CompressionRunTimeHours</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-info">
            <div>
                <ul class="nav nav-tabs">
                    <li id="id_tab_Personal" class="active"><a data-toggle="tab" href="#" onclick="showdivcompressiondesign()">
                        <i class="fa fa-street-view"></i>&nbsp;&nbsp;CompressionRunTimeHours</a></li>
                    <li id="id_tab_documents" class=""><a data-toggle="tab" href="#" onclick="showdivmilkrechillingdesign()">
                        <i class="fa fa-file-text"></i>&nbsp;&nbsp;MilkRechilling</a></li>
                </ul>
            </div>
            <div id="divcompression" style="display: none;">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <i style="padding-right: 5px;" class="fa fa-cog"></i>CompressionRunTimeHours
                    </h3>
                </div>
                <div class="box-body">
                    <div id='fillform'>
                        <table align="center" style="width: 60%;">
                            <tr>
                                <td>
                                    PlantName
                                </td>
                                <td>
                                    <select id="Slct_Plantcode" class="form-control" onchange="Routechangeclick(this);" style="margin: 0px; height: 35px; width: 312px;">
                                        <option selected disabled value="Select Plantcode">Select Plantcode</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Date
                                </td>
                                <td>
                                    <input type="date"  class="form-control" id="txtCompressionDate" style="margin: 0px; height: 35px; width: 312px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Session
                                </td>
                                <td style="height: 40px;">
                                    <select id="selct_sessionCompression"  class="form-control" style="margin: 0px; height: 35px; width: 312px;">
                                        <option selected disabled value="Select session">Select session</option>
                                        <option value="AM">AM</option>
                                        <option value="PM">PM</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Start Time
                                </td>
                                <td>
                                    <input type="text" class="form-control" id="txt_starttime" placeholder="Enter StartTime"
                                        style="margin: 0px; height: 35px; width: 312px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    IBT Temp
                                </td>
                                <td>
                                    <input type="text" class="form-control" id="txt_ibt" placeholder="Enter IBT Temp"
                                        style="margin: 0px; height: 35px; width: 312px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    End Time
                                </td>
                                <td>
                                    <input type="text" class="form-control" id="txt_endtime" placeholder="Enter Endtime"
                                        style="margin: 0px; height: 35px; width: 312px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    IBT Temp1
                                </td>
                                <td>
                                    <input type="text" class="form-control" id="txtibt" placeholder="Enter IBT Temp"
                                        style="margin: 0px; height: 35px; width: 312px;" />
                                </td>
                            </tr>
                            <tr>
                            <tr>
                                <td colspan="6" align="center" style="height: 40px;">
                                    <input id="btn_save" type="button" class="btn btn-primary" name="submit" value='save'
                                        onclick="save_comruntimehours_click()" />
                                    <input id='btn_close' type="button" class="btn btn-danger" name="Close" value='Close'
                                        onclick="cancelcompresiondetails()" />
                                </td>
                            </tr>
                        </table>
                        <div id="div_CompressionData">
                        </div>
                    </div>
                </div>
            </div>
            <div id="divmilk" style="display: none;">
                <div class="box-header with-border">
                    <h3 class="box-title">
                        <i style="padding-right: 5px;" class="fa fa-cog"></i>MilkRechilling
                    </h3>
                </div>
                <div class="box-body">
                    <div id='Div_fillmilk'>
                        <table align="center" style="width: 60%;">
                            <tr>
                                <td>
                                    PlantName
                                </td>
                                <td>
                                    <select id="Slct_Plantcode1" class="text2" onchange="Routechangeclick1(this);" class="form-control" style="margin: 0px; height: 35px; width: 312px;">
                                        <option selected disabled value="Select Plantcode">Select Plantcode</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Date<span style="color: red;">*</span>
                                </td>
                                <td>
                                    <input type="date"  id="txtRechillingDate" class="form-control" style="margin: 0px; height: 35px; width: 312px;"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Session
                                </td>
                                <td>
                                    <select id="SessionMilkRechillin" class="form-control" style="margin: 0px; height: 35px; width: 312px;">
                                        <option selected disabled value="Select session">Select session</option>
                                        <option>AM</option>
                                        <option>PM</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ONTime
                                </td>
                                <td>
                                    <input type="text" class="form-control" id="txtontime" placeholder="Enter ONtime"
                                        style="margin: 0px; height: 35px; width: 312px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    OFF Time
                                </td>
                                <td>
                                    <input type="text" class="form-control" id="txtofftime" placeholder="Enter OFFtime"
                                        style="margin: 0px; height: 35px; width: 312px;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    TotalLiters
                                </td>
                                <td>
                                    <input type="text" class="form-control" id="Txtttl" placeholder="Enter TotalLiters"
                                        style="margin: 0px; height: 35px; width: 312px;" />
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
                                    <input id="btn_save1" type="button" class="btn btn-primary" name="submit" value='save'
                                        onclick="savemilkrechilling()" />
                                    <input id='btn_cancel' type="button" class="btn btn-danger" name="Close" value='Close'
                                        onclick="cancelmilkdetails()" />
                                </td>
                            </tr>
                        </table>
                        <div id="div_milkdata">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
