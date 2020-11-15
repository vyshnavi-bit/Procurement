<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LedgerMaster.aspx.cs" Inherits="LedgerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
      <meta http-equiv="content-type" content="text/plain; charset=UTF-8"/>
      <script type="text/javascript">
        $(function () {
            get_ledger_details();
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
        function cancelUserdetails() {
            $("#div_user").show();
            $("#user_fillform").hide();
            $('#showlogs').show();
            forclearall();
        }
       
        function saveledgermaster() {
            var ledgername = document.getElementById('txt_name').value;
            var ledgercode = document.getElementById('txt_code').value;
            var userid = document.getElementById('lbl_sno').innerHTML;
            var btnval = document.getElementById('btn_save').value;
            if (ledgername == "") {
                alert("Enter ledgername");
                return false;
            }
            if (ledgercode == "") {
                alert("Enter ledgercode");
                return false;
            }

            var data = { 'operation': 'saveledgermaster', 'ledgercode': ledgercode, 'ledgername': ledgername,'userid':userid, 'btnVal': btnval };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        alert(msg);
                        forclearall();
                        get_ledger_details();
                        $('#div_user').show();
                        $('#user_fillform').css('display', 'none');
                        $('#showlogs').css('display', 'block');
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
            document.getElementById('lbl_sno').innerHTML = "";
            document.getElementById('txt_name').value = "";
            document.getElementById('txt_code').value = "";
            document.getElementById('btn_save').value = "save";
            get_ledger_details();
        }
        function showUserdesign() {
            $("#div_user").hide();
            $("#user_fillform").show();
            $('#showlogs').hide();
            get_role_details();
            get_plant_details();
        }

        function get_ledger_details() {
            var data = { 'operation': 'get_ledger_details' };
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
            var results = '<div  style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="example2_info">';
            results += '<thead><tr><th scope="col"></th><th scope="col">Ledger Name</th><th scope="col">Ledger Code</th></tr></thead></tbody>';
            var k = 1;
            var l = 0;
            var COLOR = ["#b3ffe6", "AntiqueWhite", "#daffff", "MistyRose", "Bisque"];
            for (var i = 0; i < msg.length; i++) {
                results += '<tr style="background-color:' + COLOR[l] + '"><td><input id="btn_poplate" type="button"  onclick="getme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
                results += '<td data-title="brandstatus"  class="2">' + msg[i].ledgername + '</td>';
                results += '<td data-title="brandstatus"  class="3">' + msg[i].ledgercode + '</td>';
                results += '<td style="display:none" class="15">' + msg[i].userid + '</td></tr>';
                l = l + 1;
                if (l == 4) {
                    l = 0;
                }
            }
            results += '</table></div>';
            $("#div_user").html(results);
        }
        function getme(thisid) {
            var userid = $(thisid).parent().parent().children('.15').html();
            var ledgername = $(thisid).parent().parent().children('.2').html();
            var ledgercode = $(thisid).parent().parent().children('.3').html();

            document.getElementById('lbl_sno').innerHTML = userid;
            document.getElementById('txt_name').value = ledgername;
            document.getElementById('txt_code').value = ledgercode;
            document.getElementById('btn_save').value = "Modify";
            $("#div_user").hide();
            $("#user_fillform").show();
            $('#showlogs').hide();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>
          Ledger Master
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Masters</a></li>
            <li><a href="#"> Ledger Master</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <i style="padding-right: 5px;" class="fa fa-cog"></i>Ledger Details
                </h3>
            </div>
            <div class="box-body">
                <div id="showlogs" align="center">
                    <input id="btn_addAddress" type="button" name="submit" value='AddLedgerDetails' class="btn btn-primary"
                        onclick="showUserdesign()" />
                </div>
                <div id="div_user">
                </div>
                <div id='user_fillform' style="display: none;">
                    <table align="center">
                            <tr>
                            <td>
                               <label> Ledger Name</label>
                            </td>
                            <td style="height: 40px;">
                                <input id="txt_name" type="text" name="First Name" class="form-control" placeholder="Enter  Ledger Name" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label> Ledger Code</label>
                            </td>
                            <td style="height: 40px;">
                                <input id="txt_code" type="text" name="Last Name" class="form-control"  placeholder="Enter Ledger Code"  />
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
                        <tr align>
                            <td colspan="2" align="center" style="height: 40px;">
                                <input id="btn_save" type="button" class="btn btn-primary" name="submit" value='save'  onclick="saveledgermaster()" />
                                <input id='btn_close' type="button" class="btn btn-danger" name="Close" value='Close' onclick="cancelUserdetails()" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </section>
</asp:Content>


