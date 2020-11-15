<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="RouteWisesmsSend.aspx.cs" Inherits="RouteWisesmsSend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
    <script type="text/javascript">

        $(function () {
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
        function get_Sms_Data_RouteWise() {
            var plant_code = document.getElementById('Slct_Plantcode').value;
            if (plant_code == "" || plant_code == "Select plantName") {
                alert("Enter PlantName");
                return false;
            }
            var Date = document.getElementById('txtfromdate').value;
            if (Date == "") {
                alert("Enter Date ");
                return false;
            }
            var data = { 'operation': 'get_Sms_Data_RouteWise', 'plant_code': plant_code, 'Date': Date };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        alert(msg);
                        fillroutewisesmsdata(msg);
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



        function fillroutewisesmsdata(msg) {
            var results = '<div  style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid">';
            results += '<thead><tr><th scope="col">PlantCode</th><th scope="col">PlantName</th><th scope="col">RouteName</th><th scope="col">MilkKg</th><th scope="col">DifferenceMilkg</th><th scope="col">Fat</th><th scope="col">Snf</th><th scope="col">Diffavgsnf</th><th scope="col">Diffavgfat</th></tr></thead></tbody>';
            for (var i = 0; i < msg.length; i++) {
               // results += '<tr><td><input id="btn_poplate" type="button"  onclick="getme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
                results += '<tr><td   data-title="status" class="1">' + msg[i].PlantCode + '</td>';
                results += '<td data-title="status" class="2">' + msg[i].PlantName + '</td>'; 
                results += '<td  data-title="status" class="3">' + msg[i].RouteName + '</td>';
                results += '<td data-title="status"  class="4">' + msg[i].MilkKg + '</td>';
                results += '<td  class="8">' + msg[i].DifferenceMilkg + '</td>';
                results += '<td data-title="status"  class="4">' + msg[i].Fat + '</td>';
                results += '<td  data-title="status" class="5">' + msg[i].Snf + '</td>';
                results += '<td data-title="status"  class="6">' + msg[i].Diffavgsnf + '</td>';
                results += '<td data-title="status"  class="7">' + msg[i].Diffavgfat + '</td></tr>';
                //results += '<td  class="8">' + msg[i].DifferenceMilkg + '</td></tr>';
            }
            results += '</table></div>';
            $("#div_Routewisesms").html(results); 
            $("#sendsms").show();
            $("#SMSREPORT").show();
        }


        function Send_Sms_Data_RouteWise() {
            var data = { 'operation': 'Send_Sms_Data_RouteWise' };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        alert(msg);
//                        fillroutewisesmsdata(msg);
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



        function CallPrint(strid) {
            var divToPrint = document.getElementById(strid);
            var newWin = window.open('', 'Print-Window', 'width=400,height=400,top=100,left=100');
            newWin.document.open();
            newWin.document.write('<html><body   onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
            newWin.document.close();
        }





    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>
            Route Wise SMS Send<small>Preview</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Reports</a></li>
            <li><a href="#">Route Wise SMS Send</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <i style="padding-right: 5px;" class="fa fa-cog"></i>Route Wise SMS Send
                </h3>
            </div>
            <div class="box-body">
                <div runat="server" id="d">
                    <table align="center">
                       
                        <tr>
                            <td>
                                <label>
                                    Date</label>
                            </td>
                            <td>
                                <input type="date" id="txtfromdate" class="form-control" />
                            </td>
                             </tr>
                              <tr>
                            <td>
                                <label>
                                    PlantName</label>
                            </td>
                            <td style="height: 40px;">
                                <select id="Slct_Plantcode" class="form-control" >
                                    <option selected disabled value="Select Plantcode">Select Plantcode</option>
                                </select>
                            </td>
                        </tr>
                             <tr>
                            <td>
                                <input id="btn_save" type="button" class="btn btn-primary" name="submit" value='SHOW'
                                    onclick="get_Sms_Data_RouteWise()" />
                            </td>
                        </tr>
                    </table>
                    <div id="div_Routewisesms">
                    </div>
                    <table align="center">
                       <tr id="sendsms" style="display:none">
                            <td>
                                <input id="Button1" type="button" class="btn btn-primary" name="submit" value='SendSMS'
                                    onclick="Send_Sms_Data_RouteWise()" />
                            </td>
                             <td>
                              </td>
                            <td>
                             <asp:Button ID="Button2" runat="Server" CssClass="btn btn-primary" OnClientClick="javascript:CallPrint1('divMainAddNewRow');"
                                    Text="Print" />
                                      </td>
                        </tr>
                    </table>
                </div>
                  
            </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
