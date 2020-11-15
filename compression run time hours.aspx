<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="compression run time hours.aspx.cs" Inherits="compression_run_time_hours" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
  <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="Js/JTemplate.js?v=3004" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Css/VyshnaviStyles.css" />
<script type="text/javascript">
    $(function () {
        get_comruntimehours_details();
        get_milkrechilling_details();
        showdivcompressiondesign();
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
    
    function save_comruntimehours_click() {
        var Session = document.getElementById('selct_sessionCompression').value;
        var StartTime = document.getElementById('txt_starttime').value;
        var IBTTemp = document.getElementById('txt_ibt').value;
        var EndTime = document.getElementById('txt_endtime').value;
        var IBTTemp1 = document.getElementById('txtibt').value;


        if (Session == "") {
            alert("Enter Session ");
            return false;

        }
        if (StartTime == "") {
            alert("Enter StartTime ");
            return false;

        }
        if (IBTTemp == "") {
            alert("Enter IBTTemp ");
            return false;

        }
        if (EndTime == "") {
            alert("Enter EndTime ");
            return false;

        }
        if (IBTTemp1 == "") {
            alert("Enter IBTTemp ");
            return false;

        }
        var sno = document.getElementById('lbl_sno').value;
        //var status = document.getElementById('ddlstatus').value;
        var btnval = document.getElementById('btn_save').value;
        var data = { 'operation': 'save_comruntimehours_click', 'Session': Session, 'StartTime': StartTime, 'IBTTemp': IBTTemp, 'EndTime': EndTime, 'IBTTemp1': IBTTemp1, 'btnVal': btnval, 'sno': sno };
        var s = function (msg) {
            if (msg) {
                if (msg.length > 0) {
                    alert(msg);
                    get_comruntimehours_details();
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
 
    function get_comruntimehours_details() {
        var data = { 'operation': 'get_comruntimehours_details' };
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
        var results = '<div class="divcontainer" style="overflow:auto;"><table class="responsive-table">';
        results += '<thead><tr><th scope="col"></th><th scope="col">Session</th><th scope="col">StartTime</th><th scope="col">IBTTemp</th><th scope="col">EndTime</th><th scope="col">IBTTemp</th></tr></thead></tbody>';
        for (var i = 0; i < msg.length; i++) {
            results += '<tr><td><input id="btn_poplate" type="button"  onclick="getme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
            results += '<th scope="row" class="1" style="text-align:center;">' + msg[i].Session + '</th>';
            results += '<td  class="2">' + msg[i].StartTime + '</td>';
            results += '<td  class="3">' + msg[i].IBTTemp + '</td>';
            results += '<td  class="4">' + msg[i].EndTime + '</td>';
            results += '<td  class="6">' + msg[i].IBTTemp1 + '</td>';
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
        var sno = $(thisid).parent().parent().children('.5').html();

        document.getElementById('selct_sessionCompression').value = Session;
        document.getElementById('txt_starttime').value = StartTime;
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
        var Session = document.getElementById('SessionMilkRechillin').value;
        var ONTime = document.getElementById('txtontime').value;
        var OFFTime = document.getElementById('txtofftime').value;
        var TotalLiters = document.getElementById('Txtttl').value;
             if (Session == "") {
            alert("Enter Session ");
            return false;

        }
        if (ONTime == "") {
            alert("Enter ONTime ");
            return false;

        }
        if (OFFTime == "") {
            alert("Enter OFFTime ");
            return false;

        }
        if (TotalLiters == "") {
            alert("Enter TotalLiters ");
            return false;

        }
        var sno = document.getElementById('lbl_sno').value;
        var btnval = document.getElementById('btn_save1').value;
        var data = { 'operation': 'savemilkrechilling', 'Session': Session, 'ONTime': ONTime, 'OFFTime': OFFTime, 'TotalLiters': TotalLiters, 'btnVal': btnval, 'sno': sno };
        var s = function (msg) {
            if (msg) {
                if (msg.length > 0) {
                    alert(msg);
                    get_milkrechilling_details();
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
    function get_milkrechilling_details() {
        var data = { 'operation': 'get_milkrechilling_details' };
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
        var results = '<div class="divcontainer" style="overflow:auto;"><table class="responsive-table">';
        results += '<thead><tr><th scope="col"></th><th scope="col">Session</th><th scope="col">ONTime</th><th scope="col">OFFTime</th><th scope="col">TotalLiters</th></tr></thead></tbody>';
        for (var i = 0; i < msg.length; i++) {
            results += '<tr><td><input id="btn_poplate" type="button"  onclick="getmilk(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
            results += '<th scope="row" class="1" style="text-align:center;">' + msg[i].Session + '</th>';
            results += '<td class="2">' + msg[i].ONTime + '</td>';
            results += '<td  class="3">' + msg[i].OFFTime + '</td>';
            results += '<td  class="4">' + msg[i].TotalLiters + '</td>';
            results += '<td style="display:none" class="5">' + msg[i].sno + '</td></tr>';

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
        var sno = $(thisid).parent().parent().children('.5').html();

        document.getElementById('SessionMilkRechillin').value = Session;
        document.getElementById('txtontime').value = ONTime;
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
        document.getElementById('txtofftime').value = "";
        document.getElementById('Txtttl').value = "";
        document.getElementById('lbl_sno').selectedIndex = 0;
        document.getElementById('btn_save1').value = "save";
        $("#lbl_code_error_msg").hide();
        $("#lbl_name_error_msg").hide();
    }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                    <th>
                                    </th>
                                </tr>
                                <tr>
            <td>
                Session
            </td>
            <td style="height: 40px;">
                <select id="selct_sessionCompression" class="form-control">
                                <option selected disabled value="Select session">Select session</option>
                                                   <option value="AM">AM</option>

                                                    <option  value="PM">PM</option>

                </select>
            </td>
        </tr>
           <tr>
            <td>
               Start Time
            </td>
            <td>
         <input type=time class="form-control" id="txt_starttime" placeholder="Enter StartTime"
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
                
          <input type="time" class="form-control" id="txt_endtime" placeholder="Enter Endtime"
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
                                    <th>
                                    </th>
                                </tr>
                                <tr>
            <td>
                Session
            </td>
           <td >
                            <select id="SessionMilkRechillin" class="form-control" >
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
         <input type=time class="form-control" id="txtontime" placeholder="Enter ONtime"
                    style="margin: 0px; height: 35px; width: 312px;" />
            </td>
        </tr>
        <tr>
        <td>
             OFF Time
        </td>
        <td>
                
          <input type="time" class="form-control" id="txtofftime" placeholder="Enter OFFtime"
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

