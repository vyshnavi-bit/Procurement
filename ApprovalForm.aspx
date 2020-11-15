<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApprovalForm.aspx.cs" Inherits="ApprovalForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
<script type="text/javascript">
    $(function () {
        $('.divsalesOffice').css('display', 'none');
        GetRisedVouchers();
        var date = new Date();
        var day = date.getDate();
        var month = date.getMonth() + 1;
        var year = date.getFullYear();
        if (month < 10) month = "0" + month;
        if (day < 10) day = "0" + day;
        today = year + "-" + month + "-" + day;
        $('#txtFromDate').val(today);
        $('#txtToDate').val(today);
    });
    function GetRisedVouchers() {
        var BranchID = "0";
        var data = { 'operation': 'GetRaisedVouchers', 'BranchID': BranchID };
        var s = function (msg) {
            if (msg) {
                if (msg == "Session Expired") {
                    alert(msg);
                    window.location = "LoginDefault.aspx";
                }
                var results = '<div class="divcontainer" style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="example2_info">';
                results += '<thead><tr><th scope="col"></th><th scope="col"> Voucher ID</th><th scope="col">Name</th><th scope="col">Type</th><th scope="col">Amount</th></tr></thead></tbody>';
                for (var i = 0; i < msg.length; i++) {
                    results += '<tr><td><input id="btn_poplate" type="button"  onclick="btnViewVoucher(this)" name="submit" class="btn btn-success" value="Approve" /></td>';
                    results += '<th scope="row" class="1">' + msg[i].VoucherID + '</th>';
                    results += '<td  data-title="Code" class="4">' + msg[i].EmpName + '</td>';
                    results += '<td class="5">' + msg[i].VoucherType + '</td>';
                    results += '<td data-title="Code" class="2">' + msg[i].Amount + '</td>';
                    results += '<td style="display:none" data-title="Code" class="2">' + msg[i].Sno + '</td>';
                    results += '<td style="display:none" class="8">' + msg[i].Empid + '</td></tr>';
                }
                results += '</table></div>';
                $("#divRaisedVouchers").html(results);
            }
            else {
            }
        };
        var e = function (x, h, e) {
        };
        $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
        callHandler(data, s, e);
    }
    var VoucherID = "0";
    var branchID = "0";
    function btnViewVoucher(thisid) {
        VoucherID = $(thisid).parent().parent().children('.1').html();
        var data = { 'operation': 'GetBtnViewVoucherclick', 'VoucherID': VoucherID };
        var s = function (msg) {
            if (msg) {
                if (msg == "Session Expired") {
                    alert(msg);
                    window.location = "LoginDefault.aspx";
                }
                BindViewVouchers(msg);
            }
            else {
            }
        };
        var e = function (x, h, e) {
        };
        $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
        callHandler(data, s, e);
    }
    function BindViewVouchers(msg) {
        $('#divMainAddNewRow').css('display', 'block');
        var emp = [];
        var results = '<div class="divcontainer" style="overflow:auto;"><table class="responsive-table">';
        results += '<thead><tr><th scope="col"></th><th scope="col"> Head Name</th><th scope="col">Amount</th></tr></thead></tbody>';
        for (var i = 0; i < emp.length; i++) {
            results += '<tr><td><input id="btn_poplate" type="button"  onclick="btnViewVoucher(this)" name="submit" class="btn btn-primary" value="Approve" /></td>';
            results += '<th scope="row" class="1">' + emp[i].Description + '</th>';
            results += '<td data-title="Code" class="2">' + emp[i].Amount + '</td>';
            results += '<td style="display:none" class="8">' + emp[i].Empid + '</td></tr>';
        }
        results += '</table></div>';
        $("#divHead").html(results);
        //            $('#divHead').setTemplateURL('SubPayable.htm');
        //            $('#divHead').processTemplate(emp);
        document.getElementById("spnName").innerHTML = msg[0].Description;
        document.getElementById("spnVoucherType").innerHTML = msg[0].VoucherType;
        document.getElementById("spnAmount").innerHTML = msg[0].Amount;
        document.getElementById("spnApprovalEmp").innerHTML = msg[0].ApproveEmpName;
        document.getElementById("txtCashierRemarks").value = msg[0].Remarks;
        document.getElementById("spnVoucherID").innerHTML = VoucherID;
        document.getElementById("txtApprovalamt").value = msg[0].ApprovalAmount;
        document.getElementById("txtRemarks").value = msg[0].Remarks;
        document.getElementById("txtApprovalamt").value = msg[0].Amount;
        PopupOpen(VoucherID);
    }
    function PopupOpen(VocherID) {
        var data = { 'operation': 'GetAppriveSubPaybleValues', 'VoucherID': VocherID };
        var s = function (msg) {
            if (msg) {
                var results = '<div class="divcontainer" style="overflow:auto;"><table class="responsive-table">';
                results += '<thead><tr><th scope="col"></th><th scope="col"> Head Name</th><th scope="col">Amount</th></tr></thead></tbody>';
                for (var i = 0; i < msg.length; i++) {
                    results += '<tr><td></td>';
                    results += '<th scope="row" class="1">' + msg[i].HeadOfAccount + '</th>';
                    results += '<td data-title="Code" class="AmountClass">' + msg[i].Amount + '</td>';
                    results += '<td style="display:none" class="8">' + msg[i].HeadSno + '</td></tr>';
                }
                results += '</table></div>';
                $("#divHead").html(results);
                var TotRate = 0.0;
                $('.AmountClass').each(function (i, obj) {
                    if ($(this).text() == "") {
                    }
                    else {
                        TotRate += parseFloat($(this).text());
                    }
                });
                TotRate = parseFloat(TotRate).toFixed(2);
                //                    document.getElementById("txt_total").innerHTML = TotRate;
            }
            else {
            }
        };
        var e = function (x, h, e) {
        };
        $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
        callHandler(data, s, e);
    }
    function OrdersCloseClick() {
        $('#divMainAddNewRow').css('display', 'none');
    }
    function btnApproveVoucherclick() {
        var Remarks = document.getElementById("txtCashierRemarks").value;
        var Approvalamt = document.getElementById("txtApprovalamt").value;
        if (Approvalamt == "") {
            alert("Enter Amount");
            return false;
        }
        var branchID = "0";
        var LevelType = '<%=Session["LevelType"] %>';
        if (LevelType == "AccountsOfficer" || LevelType == "Director") {
            $('.divsalesOffice').css('display', 'table-row');
            branchID = document.getElementById("ddlsalesOffice").value;
        }
        else {
        }
        var AppRemarks = document.getElementById("txtRemarks").value;
        var Status = "A";
        var data = { 'operation': 'btnApproveVoucherclick', 'VoucherID': VoucherID, 'Approvalamt': Approvalamt, 'AppRemarks': Remarks, 'Status': Status, 'Remarks': Remarks };
        var s = function (msg) {
            if (msg) {
                alert(msg);
                $('#divMainAddNewRow').css('display', 'none');
                document.getElementById("txtCashierRemarks").value = "";
                document.getElementById("txtApprovalamt").value = "";
                document.getElementById("txtRemarks").value = "";
                var LevelType = '<%=Session["LevelType"] %>';
                if (LevelType == "AccountsOfficer" || LevelType == "Director") {
                    BtnGenerateClick();
                }
                else {
                    GetRisedVouchers();
                }
            }
            else {
            }
        };
        var e = function (x, h, e) {
        };
        callHandler(data, s, e);
    }
    function btnRejectVoucherclick() {
        var Remarks = document.getElementById("txtCashierRemarks").value;
        var Approvalamt = document.getElementById("txtApprovalamt").value;
        if (Approvalamt == "") {
            alert("Enter Amount");
            return false;
        }
        var branchID = "0";
        var LevelType = '<%=Session["LevelType"] %>';
        if (LevelType == "AccountsOfficer" || LevelType == "Director") {
            $('.divsalesOffice').css('display', 'table-row');
            branchID = document.getElementById("ddlsalesOffice").value;
        }
        else {
        }
        var AppRemarks = document.getElementById("txtRemarks").value;
        var Status = "C";
        var data = { 'operation': 'btnRejectVoucherclick', 'VoucherID': VoucherID, 'BranchID': branchID, 'Approvalamt': Approvalamt, 'Remarks': Remarks, 'Status': Status };
        var s = function (msg) {
            if (msg) {
                alert(msg);
                $('#divMainAddNewRow').css('display', 'none');
                document.getElementById("txtCashierRemarks").value = "";
                document.getElementById("txtApprovalamt").value = "";
                document.getElementById("txtRemarks").value = "";
                var LevelType = '<%=Session["LevelType"] %>';
                if (LevelType == "AccountsOfficer" || LevelType == "Director") {
                    BtnGenerateClick();
                }
                else {
                    GetRisedVouchers();
                }
            }
            else {
            }
        };
        var e = function (x, h, e) {
        };
        callHandler(data, s, e);
    }
    function btnVoucherUpdateClick() {
        var Remarks = document.getElementById("txtCashierRemarks").value;
        var branchID = "0";
        var LevelType = '<%=Session["LevelType"] %>';
        if (LevelType == "AccountsOfficer" || LevelType == "Director") {
            $('.divsalesOffice').css('display', 'table-row');
            branchID = document.getElementById("ddlsalesOffice").value;
        }
        else {
        }
        var data = { 'operation': 'btnVoucherUpdateClick', 'VoucherID': VoucherID, 'branchID': branchID, 'Remarks': Remarks };
        var s = function (msg) {
            if (msg) {
                alert(msg);
                $('#divMainAddNewRow').css('display', 'none');
            }
            else {
            }
        };
        var e = function (x, h, e) {
        };
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
 <section class="content-header">
        <h1>
            Voucher Approval<small>Preview</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Operations</a></li>
            <li><a href="#">Voucher Approval</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <i style="padding-right: 5px;" class="fa fa-cog"></i>Voucher Approval Details
                </h3>
            </div>
            <div class="box-body">
                <table>
                    <tr class="divsalesOffice" style="display: none">
                        <td>
                            Sales Office
                        </td>
                        <td>
                            <select id="ddlsalesOffice" class="form-control">
                            </select>
                        </td>
                        <td>
                            Status
                        </td>
                        <td>
                            <select id="ddlType" class="form-control">
                                <option value="R">Raised</option>
                                <option value="A">Approved</option>
                                <option value="C">Rejected</option>
                                <option value="P">Paid</option>
                            </select>
                        </td>
                        <td>
                            From Date
                        </td>
                        <td>
                            <input type="date" id="txtFromDate" class="formDate" />
                        </td>
                        <td>
                            To Date
                        </td>
                        <td>
                            <input type="date" id="txtToDate" class="formDate" />
                        </td>
                        <td>
                        </td>
                        <td>
                            <input type="button" id="btnPay" value="Genarete" onclick="BtnGenerateClick();" class="btn btn-success" />
                        </td>
                    </tr>
                </table>
                <div id="divRaisedVouchers">
                </div>
                <div id="divMainAddNewRow" class="pickupclass" style="text-align: center; height: 100%;
                    width: 100%; position: absolute; display: none; left: 0%; top: 0%; z-index: 99999;
                    background: rgba(192, 192, 192, 0.7);">
                    <div id="divAddNewRow" style="border: 5px solid #A0A0A0; position: absolute; top: 25%;
                        background-color: White; left: 10%; right: 10%; width: 80%; height: 100%; -webkit-box-shadow: 1px 1px 10px rgba(50, 50, 50, 0.65);
                        -moz-box-shadow: 1px 1px 10px rgba(50, 50, 50, 0.65); box-shadow: 1px 1px 10px rgba(50, 50, 50, 0.65);
                        border-radius: 10px 10px 10px 10px;">
                        <table align="left" cellpadding="0" cellspacing="0" style="float: left; width: 100%;"
                            id="tableCollectionDetails" class="mainText2" border="1">
                            <tr>
                                <td>
                                    Name
                                </td>
                                <td>
                                    <span id="spnName" class="Spancontrol"></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Voucher ID
                                </td>
                                <td>
                                    <span id="spnVoucherID" class="Spancontrol"></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    VoucherType
                                </td>
                                <td>
                                    <span id="spnVoucherType" class="Spancontrol"></span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div id="divHead">
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Amount
                                </td>
                                <td>
                                    <span id="spnAmount" class="Spancontrol"></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Approval Employee
                                </td>
                                <td>
                                    <span id="spnApprovalEmp" class="Spancontrol"></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Remarks
                                </td>
                                <td>
                                    <textarea rows="5" cols="45" id="txtCashierRemarks" class="form-control" placeholder="Enter Remarks"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Approval Amount
                                </td>
                                <td>
                                    <input type="number" id="txtApprovalamt" class="Spancontrol" value="" onkeypress="return numberOnlyExample();"
                                        placeholder="Enter Approval Amount" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Approval Remarks
                                </td>
                                <td>
                                    <input type="text" id="txtRemarks" class="form-control" value="" onkeypress="return numberOnlyExample();"
                                        placeholder="Enter Remarks" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="button" value="Update" id="btnAdd" onclick="btnVoucherUpdateClick();"
                                        class="btn btn-success" />
                                </td>
                                <td>
                                    <input type="button" id="Button1" value="Approve" onclick="btnApproveVoucherclick();"
                                         class="btn btn-success" />
                                </td>
                                <td>
                                    <input type="button" id="Button2" value="Reject" onclick="btnRejectVoucherclick();"
                                         class="btn btn-danger" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divclose" style="width: 35px; top: 24.5%; right: 9.5%; position: absolute;
                        z-index: 99999; cursor: pointer;">
                        <img src="Image/Odclose.png" alt="close" onclick="OrdersCloseClick();" />
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

