<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="silobalrpt.aspx.cs" Inherits="silobalrpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
    <link href="Css/VyshnaviStyles.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            
            var date = new Date();
            var day = date.getDate();
            var month = date.getMonth() + 1;
            var year = date.getFullYear();
            if (month < 10) month = "0" + month;
            if (day < 10) day = "0" + day;
            today = year + "-" + month + "-" + day;
            $('#txtfromdate').val(today);
            $('#txttodate').val(today);
            //          UpdateHeads();
        });
        function ddlSalesOfficeChange(ID) {
            UpdateHeads();
        }
        function get_plant_details() {
            var data = { 'operation': 'get_plant_details' };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillplantcode(msg);
                    }
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            callHandler(data, s, e);
        }
        function fillplantcode(msg) {
            var data = document.getElementById('slct_plantcode');
            var length = data.options.length;
            document.getElementById('slct_plantcode').options.length = null;
            var opt = document.createElement('option');
            opt.innerHTML = "Select plant Name";
            opt.value = "Select plant Name";
            opt.setAttribute("selected", "selected");
            opt.setAttribute("disabled", "disabled");
            opt.setAttribute("class", "dispalynone");
            data.appendChild(opt);
            for (var i = 0; i < msg.length; i++) {
                if (msg[i].name != null) {
                    var option = document.createElement('option');
                    option.innerHTML = msg[i].name;
                    option.value = msg[i].code;
                    data.appendChild(option);
                }
            }
        }
        function UpdateHeads() {
            var plantcode = document.getElementById('slct_plantcode').value;
            var data = { 'operation': 'GetHeadOfAccpunts', 'plantcode': plantcode };
            var s = function (msg) {
                if (msg) {
                    BindHeads(msg);
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        var serial = 0;
        function BindHeads(msg) {
            var results = '<div class="divcontainer" style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="example2_info">';
            results += '<thead><tr><th scope="col"></th><th scope="col">HeadName</th><th scope="col">Ledger Code</th><th scope="col">Limit</th></tr></thead></tbody>';
            for (var i = 0; i < msg.length; i++) {
                results += '<tr><td><input id="btn_poplate" type="button"  onclick="getme(this)" name="submit" class="btn btn-success" value="Choose" /></td>';
                results += '<td  data-title="Code" class="1">' + msg[i].HeadName + '</td>';
                results += '<td  data-title="Code"  class="4">' + msg[i].ledger_code + '</td>';
                results += '<td data-title="Code" class="2">' + msg[i].Limit + '</td>';
                results += '<td  data-title="Code"  style="display:none"  class="5">' + msg[i].plantcode + '</td>';

                results += '<td style="display:none" class="3">' + msg[i].Sno + '</td></tr>';
            }
            results += '</table></div>';
            $("#divHeadAcount").html(results);
        }
        function getme(thisid) {
            var HeadName = $(thisid).parent().parent().children('.1').html();
            var ledger_code = $(thisid).parent().parent().children('.4').html();
            var plantcode = $(thisid).parent().parent().children('.5').html();
            var Limit = $(thisid).parent().parent().children('.2').html();
            var Sno = $(thisid).parent().parent().children('.3').html();
            document.getElementById('txtDecription').value = HeadName;
            document.getElementById('txtLedgerCode').value = ledger_code;
            document.getElementById('slct_plantcode').value = plantcode;
            document.getElementById('txtLimit').value = Limit;
            document.getElementById('btnSave').value = "Modify";
            document.getElementById('lbl_sno').value = Sno;
        }
        function btngenrate() {
            var plantcode = document.getElementById('slct_plantcode').value;
            if (plantcode == "Select") {
                alert("Select plantcode");
                return false;
            }
            var frmdate = document.getElementById('txtfromdate').value;
            var todate = document.getElementById('txttodate').value;
            var type = document.getElementById('slcttype').value;
            var btnSave = document.getElementById('btnSave').value;
            var sno = document.getElementById('lbl_sno').value;
            var data = { 'operation': 'get_silobalancerpt_details', 'plantcode': plantcode, 'frmdate': frmdate, 'todate': todate, 'type': type, 'btnSave': btnSave };
            var s = function (msg) {
                if (msg) {
                    alert(msg);
                    document.getElementById('txtLedgerCode').value = "";
                    UpdateHeads();
                    document.getElementById('txtDecription').value = "";
                    document.getElementById('slct_plantcode').value = "";
                    document.getElementById('txtLimit').value = "";
                    document.getElementById('btnSave').value = "Save";
                    document.getElementById('lbl_sno').value = "";
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
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
    </script>
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
</asp:content>
<asp:content id="Content3" contentplaceholderid="ContentPlaceHolder2" runat="Server">
    <section class="content-header">
        <h1>
             SILO BALANCE<small>Preview</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Operations</a></li>
            <li><a href="#">  SILO BALANCE</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <i style="padding-right: 5px;" class="fa fa-cog"></i>  SILO BALANCE Details
                </h3>
            </div>
            <div class="box-body">
                <table align="center">
                 <tr>
                        <td>
                            <label> Plant Code<span style="color: red;">*</span></label>
                        </td>
                        <td style="height: 40px;">
                            <select  id="slct_plantcode" class="form-control">
                            <option value="Select" selected>Select</option>
                            <option value="Cow">Cow</option>
                            <option value="Buffalo">Buffalo</option>
                            </select>
                        </td>
                        <td style="width:10px;">
                        </td>
                         <td>
                          <label> From Date<span style="color: red;">*</span></label>
                        </td>
                        <td style="height: 40px;">
                            <input type="date" id="txtfromdate" class="form-control" />
                        </td>
                        <td style="width:10px;">
                        </td>
                        <td>
                            <label> To Date<span style="color: red;">*</span></label>
                        </td>
                        <td style="height: 40px;">
                           <input type="date" id="txttodate" class="form-control" />
                        </td>
                        <td style="width:10px;">
                        </td>
                         <td>
                            <label>  Type <span style="color: red;">*</span></label>
                        </td>
                        <td style="height: 40px;">
                            <select  id="slcttype" class="form-control">
                            <option value="Acknowledgement" selected>Acknowledgement</option>
                            <option value="Without Rate">Without Rate</option>
                            <option value="Dispatch">Dispatch</option>
                            
                            </select>
                        </td>
                        <td style="width:10px;">
                        </td>
                        <td style="height: 40px;">
                            <input type="button" id="btnSave" value="Genarate" onclick="btngenrate();" class="btn btn-success"/>
                        </td>
                    </tr>
                   
                      <tr style="display:none;"><td>
                            <label id="lbl_sno"></label>
                            </td>
                            </tr>
                   
                </table>
                <div id="divHeadAcount">
                </div>
            </div>
        </div>
    </section>
</asp:content>
