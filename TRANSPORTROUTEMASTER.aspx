<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TRANSPORTROUTEMASTER.aspx.cs" Inherits="TRANSPORTROUTEMASTER" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
    <script type="text/javascript">
        $(function () {
            get_plant_details();
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

        function savetransportroute() {
            var plantcode = document.getElementById('slct_plantname').value;
            var routename = document.getElementById('txt_route').value;
            var routeid = document.getElementById('txt_routeid').value;

            var status = document.getElementById('slct_status').value;


            var Tid = document.getElementById('lbl_Tid').value;
            var btnval = document.getElementById('btn_save').value;
            if (plantcode == "") {
                alert("Enter plantcode");
                return false;
            }
            if (routename == "") {
                alert("Enter routename");
                return false;
            }
            if (routeid == "") {
                alert("Enter routeid");
                return false;
            }
           
            if (status == "") {
                alert("Enter status");
                return false;
            }

            var data = { 'operation': 'savetransportroute', 'status': status, 'routename': routename, 'routeid': routeid, 'plantcode': plantcode, 'Tid': Tid, 'btnVal': btnval };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        alert(msg);
                        forclearall();
                        get_transportroute_details();
                        document.getElementById('slct_plantname').selectedIndex = 0;

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
            document.getElementById('lbl_Tid').value = "";
            document.getElementById('slct_plantname').value = "";
            document.getElementById('txt_route').value = "";
            document.getElementById('txt_routeid').value = "";
            document.getElementById('btn_save').value = "save";
        }
        function get_transportroute_details() {
            var plantcode = document.getElementById('slct_plantname').value;
            var data = { 'operation': 'get_transportroute_details_plant', 'plantcode': plantcode };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillAddress(msg);
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
        function fillAddress(msg) {
            $('#div_plantrequest').show();
            var results = '<div  style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="example2_info">';
            results += '<thead><tr><th scope="col"></th><th scope="col">Plant Name</th><th scope="col">route id </th><th scope="col">routename </th></tr></thead></tbody>';
            var k = 1;
            var l = 0;
            var COLOR = ["#b3ffe6", "AntiqueWhite", "#daffff", "MistyRose", "Bisque"];
            for (var i = 0; i < msg.length; i++) {
                results += '<tr style="background-color:' + COLOR[l] + '"><td></td>';
                results += '<td data-title="brandstatus"   class="1">' + msg[i].plantcode + '</td>';
                results += '<td data-title="brandstatus"  class="5">' + msg[i].routeid + '</td>';
                results += '<td data-title="brandstatus"  class="2">' + msg[i].routename + '</td>';
                results += '<td style="display:none" data-title="brandstatus"  class="3">' + msg[i].status + '</td>';
                results += '<td style="display:none" class="4">' + msg[i].tid + '</td></tr>';
                l = l + 1;
                if (l == 4) {
                    l = 0;
                }
            }
            results += '</table></div>';
            $("#div_plantrequest").html(results);
        }
      
        function getme(thisid) {
            var Tid = $(thisid).parent().parent().children('.4').html();
            var plantcode = $(thisid).parent().parent().children('.1').html();
            var routename = $(thisid).parent().parent().children('.2').html();
            var routeid = $(thisid).parent().parent().children('.5').html();
            var status = $(thisid).parent().parent().children('.3').html();


            document.getElementById('slct_plantname').value = plantcode;
            document.getElementById('txt_route').value = routename;
            document.getElementById('txt_routeid').value = routeid;
            document.getElementById('slct_status').value = status;

            document.getElementById('lbl_Tid').value = Tid;
            document.getElementById('btn_save').value = "Modify";
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <section class="content-header">
        <h1>
           TRANSPORTROUTEMASTER
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li><a href="#">TRANSPORTROUTEMASTER Reporte</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <i style="padding-right: 5px;" class="fa fa-cog"></i>TRANSPORTROUTEMASTER Details
                </h3>
            </div>
            <div class="box-body">
               
               
                <div id='plantrequest_fillform' >
                    <table align="center">

                        <tr>
                            <td>
                                <label> Plant Name:</label>
                            </td>
                            <td style="height: 40px;">
                                <select id="slct_plantname" type="text" name=" Company Code" class="form-control" onchange=" get_transportroute_details();"></select>
                            </td>
                        
                            <td>
                                <label>route name:</label>
                            </td>
                            <td style="height: 40px;">
                                <input id="txt_route" type="text" name="Address" class="form-control"  placeholder="Enter route name"  />
                            </td>
                            </tr>
                            <tr>
                             <td>
                                <label>route id:</label>
                            </td>
                            <td style="height: 40px;">
                                <input id="txt_routeid" type="text" name="Address" class="form-control"  placeholder="Enter route id "  />
                            </td>
                           <td>
                                <label> Status</label>
                            </td>
                            <td style="height: 40px; ">
                    
                                <select id="slct_status" type="text" class="form-control" >
                                <option value="1">Active</option>
                                <option value="0">InActive</option>
                                
                            </td>
                            </tr>

                        <tr style="display:none;">
                            <td>
                                <label id="lbl_Tid"  >
                                </label>
                            </td>
                        </tr>
                        </table>
                        <table align="center">
                        <tr>
                            <td colspan="2" align="center" style="height: 40px;">
                                <input id="btn_save" type="button" class="btn btn-primary" name="submit" value='save'
                                    onclick="savetransportroute()" />
                                <input id='btn_close' type="button" class="btn btn-danger" name="Close" value='Close'
                                    onclick="forclearall()" />
                            </td>
                        </tr>
                    </table>
                </div>
                 <div id="div_plantrequest">
                </div>
            </div>
        </div>
        
    </section>
</asp:Content>



