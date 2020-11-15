<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CashForm.aspx.cs" Inherits="CashForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
  <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="Js/JTemplate.js?v=3004" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Css/VyshnaviStyles.css" />
    <style type="text/css">
        .ui-autocomplete
        {
            max-height: 100px;
            overflow-y: auto; /* prevent horizontal scrollbar */
            overflow-x: hidden; /* add padding to account for vertical scrollbar */
            z-index: 1000 !important;
        }
        .custom-combobox
        {
            position: relative;
            display: inline-block;
        }
        .custom-combobox-toggle
        {
            position: absolute;
            top: 0;
            bottom: 0;
            margin-left: -1px;
            padding: 0;
        }
        .custom-combobox-input
        {
            margin: 0;
            height: 35px;
            width: 250px;
        }
    </style>
    <script type="text/javascript">
        (function ($) {
            $.widget("custom.combobox", {
                _create: function () {
                    this.wrapper = $("<span>")
          .addClass("custom-combobox")
          .insertAfter(this.element);

                    this.element.hide();
                    this._createAutocomplete();
                    this._createShowAllButton();
                },

                _createAutocomplete: function () {
                    var selected = this.element.children(":selected"),
          value = selected.val() ? selected.text() : "";

                    this.input = $("<input>")
          .appendTo(this.wrapper)
          .val(value)
          .attr("title", "")
          .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
          .autocomplete({
              delay: 0,
              minLength: 0,
              source: $.proxy(this, "_source")
          })
          .tooltip({
              tooltipClass: "ui-state-highlight"
          });

                    this._on(this.input, {
                        autocompleteselect: function (event, ui) {
                            ui.item.option.selected = true;
                            this._trigger("select", event, {
                                item: ui.item.option
                            });
                        },

                        autocompletechange: "_removeIfInvalid"
                    });
                },

                _createShowAllButton: function () {
                    var input = this.input,
          wasOpen = false;

                    $("<a>")
          .attr("tabIndex", -1)
          .attr("title", "Show All Items")
          .tooltip()
          .appendTo(this.wrapper)
          .button({
              icons: {
                  primary: "ui-icon-triangle-1-s"
              },
              text: false
          })
          .removeClass("ui-corner-all")
          .addClass("custom-combobox-toggle ui-corner-right")
          .mousedown(function () {
              wasOpen = input.autocomplete("widget").is(":visible");
          })
          .click(function () {
              input.focus();

              // Close if already visible
              if (wasOpen) {
                  return;
              }

              // Pass empty string as value to search for, displaying all results
              input.autocomplete("search", "");
          });
                },

                _source: function (request, response) {
                    var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                    response(this.element.children("option").map(function () {
                        var text = $(this).text();
                        if (this.value && (!request.term || matcher.test(text)))
                            return {
                                label: text,
                                value: text,
                                option: this
                            };
                    }));
                },

                _removeIfInvalid: function (event, ui) {

                    // Selected an item, nothing to do
                    if (ui.item) {
                        return;
                    }

                    // Search for a match (case-insensitive)
                    var value = this.input.val(),
          valueLowerCase = value.toLowerCase(),
          valid = false;
                    this.element.children("option").each(function () {
                        if ($(this).text().toLowerCase() === valueLowerCase) {
                            this.selected = valid = true;
                            return false;
                        }
                    });

                    // Found a match, nothing to do
                    if (valid) {
                        return;
                    }

                    // Remove invalid value
                    this.input
          .val("")
          .attr("title", value + " didn't match any item")
          .tooltip("open");
                    this.element.val("");
                    this._delay(function () {
                        this.input.tooltip("close").attr("title", "");
                    }, 2500);
                    this.input.autocomplete("instance").term = "";
                },

                _destroy: function () {
                    this.wrapper.remove();
                    this.element.show();
                }
            });
        })(jQuery);

        $(function () {
            $("#combobox").combobox();
            $("#toggle").click(function () {
                $("#combobox").toggle();
            });
        });
    </script>
    <script type="text/javascript">
        var LevelType = "";
        $(function () {
            FillApprovalEmploye();
            FillHeads();
            var LevelType = '<%=Session["LevelType"] %>';
            if (LevelType == "AccountsOfficer" || LevelType == "Director") {
                $('.divsalesOffice').css('display', 'table-row');
                $('.divAprovalEmp').css('display', 'none');
                FillSalesOffice();
            }
            else {
                $('.divsalesOffice').css('display', 'none');
                $('.divAprovalEmp').css('display', 'table-row');
            }
            var date = new Date();
            var day = date.getDate();
            var month = date.getMonth() + 1;
            var year = date.getFullYear();
            if (month < 10) month = "0" + month;
            if (day < 10) day = "0" + day;
            today = year + "-" + month + "-" + day;

            $('#datepicker').val(today);

            $('#txtFromDate').val(today);
            $('#txtToDate').val(today);
        });
        function FillHeads() {
            var data = { 'operation': 'GetHeadNames' };
            var s = function (msg) {
                if (msg) {
                    if (msg == "Session Expired") {
                        alert(msg);
                        window.location = "LoginDefault.aspx";
                    }
                    BindHeads(msg);
                    BindBillHeads(msg);

                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        function FillSalesOffice() {
            var data = { 'operation': 'GetSalesOffice' };
            var s = function (msg) {
                if (msg) {
                    if (msg == "Session Expired") {
                        alert(msg);
                        window.location = "LoginDefault.aspx";
                    }
                    BindSalesOffice(msg);
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        function ddlSalesOfficeChange(ID) {
            var BranchID = ID.value;
            var data = { 'operation': 'GetHeadNames', 'BranchID': BranchID };
            var s = function (msg) {
                if (msg) {
                    if (msg == "Session Expired") {
                        alert(msg);
                        window.location = "LoginDefault.aspx";
                    }
                    BindHeads(msg);
                    BindBillHeads(msg);
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        function BindSalesOffice(msg) {
            var ddlsalesOffice = document.getElementById('ddlsalesOffice');
            var length = ddlsalesOffice.options.length;
            ddlsalesOffice.options.length = null;
            var opt = document.createElement('option');
            opt.innerHTML = "select";
            ddlsalesOffice.appendChild(opt);
            for (var i = 0; i < msg.length; i++) {
                if (msg[i].BranchName != null) {
                    var opt = document.createElement('option');
                    opt.innerHTML = msg[i].BranchName;
                    opt.value = msg[i].Sno;
                    ddlsalesOffice.appendChild(opt);
                }
            }
        }
        function BindHeads(msg) {
            var ddlHeads = document.getElementById('combobox');
            var length = ddlHeads.options.length;
            ddlHeads.options.length = null;
            var opt = document.createElement('option');
            opt.innerHTML = "select";
            ddlHeads.appendChild(opt);
            for (var i = 0; i < msg.length; i++) {
                if (msg[i].HeadName != null) {
                    var opt = document.createElement('option');
                    opt.innerHTML = msg[i].HeadName;
                    opt.value = msg[i].Sno;
                    ddlHeads.appendChild(opt);
                }
            }
        }
        function BindBillHeads(msg) {
            var ddlHeads = document.getElementById('ddlBillHead');
            var length = ddlHeads.options.length;
            ddlHeads.options.length = null;
            var opt = document.createElement('option');
            opt.innerHTML = "select";
            ddlHeads.appendChild(opt);
            for (var i = 0; i < msg.length; i++) {
                if (msg[i].HeadName != null) {
                    var opt = document.createElement('option');
                    opt.innerHTML = msg[i].HeadName;
                    opt.value = msg[i].Sno;
                    ddlHeads.appendChild(opt);
                }
            }
        }
        function FillEmploye() {
            var data = { 'operation': 'GetEmployeeNames' };
            var s = function (msg) {
                if (msg) {
                    if (msg == "Session Expired") {
                        alert(msg);
                        window.location = "LoginDefault.aspx";
                    }
                    BindEmployeeName(msg);

                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        function BindEmployeeName(msg) {
            var ddlCashTo = document.getElementById('ddlCashTo');
            var length = ddlCashTo.options.length;
            ddlCashTo.options.length = null;
            for (var i = 0; i < msg.length; i++) {
                if (msg[i].UserName != null) {
                    var opt = document.createElement('option');
                    opt.innerHTML = msg[i].UserName;
                    opt.value = msg[i].Sno;
                    ddlCashTo.appendChild(opt);
                }
            }
        }
        function FillApprovalEmploye() {
            var data = { 'operation': 'GetApproveEmployeeNames' };
            var s = function (msg) {
                if (msg) {
                    if (msg == "Session Expired") {
                        alert(msg);
                        window.location = "LoginDefault.aspx";
                    }
                    BindApprovalEmploye(msg);

                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        function BindApprovalEmploye(msg) {
            var ddlEmpApprove = document.getElementById('ddlEmpApprove');
            var length = ddlEmpApprove.options.length;
            ddlEmpApprove.options.length = null;
            var opt = document.createElement('option');
            opt.innerHTML = "select";
            ddlEmpApprove.appendChild(opt);
            for (var i = 0; i < msg.length; i++) {
                if (msg[i].UserName != null) {
                    var opt = document.createElement('option');
                    opt.innerHTML = msg[i].UserName;
                    opt.value = msg[i].Sno;
                    ddlEmpApprove.appendChild(opt);
                }
            }
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
        function CallHandlerUsingJson(d, s, e) {
            d = JSON.stringify(d);
            d = d.replace(/&/g, '\uFF06');
            d = d.replace(/#/g, '\uFF03');
            d = d.replace(/\+/g, '\uFF0B');
            d = d.replace(/\=/g, '\uFF1D');
            $.ajax({
                type: "GET",
                url: "DairyFleet.axd?json=",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: d,
                async: true,
                cache: true,
                success: s,
                error: e
            });
        }
        var HeadLimit = 0;
        function ddlCashToChange(ID) {
            //var dlCash = ID.value;
            var dlCash = document.getElementById("combobox").value;
            var data = { 'operation': 'GetHeadLimit', 'HeadSno': dlCash };
            var s = function (msg) {
                if (msg) {
                    HeadLimit = msg;
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            callHandler(data, s, e);
        }
        function BtnRaiseVoucherClick() {
            var Description = "";
            var ddlVoucherType = document.getElementById("ddlVoucherType").value;
            var ddlEmployee = "0";
            Description = document.getElementById("txtdesc").value;
            var CashType = "0";
            if (ddlVoucherType == "Credit") {
                CashType = document.getElementById("ddlCashType").value;
            }
            var txtAMount = document.getElementById("txtAMount").innerHTML;
            var txtRemarks = document.getElementById("txtRemarks").value;
            var ddlEmpApprove = document.getElementById("ddlEmpApprove").value;
            var LevelType = '<%=Session["LevelType"] %>';
            if (Description == "") {
                alert("Enter Pay To");
                return false;
            }
            if (txtAMount == "") {
                alert("Enter Amount");
                return false;
            }


            if (txtRemarks == "") {
                alert("Enter Remarks");
                return false;
            }
            var ddlBillHead = "";
            if (ddlVoucherType == "Credit") {
                ddlBillHead = document.getElementById("ddlBillHead").value;
                CashType = document.getElementById("ddlCashType").value;
                if (CashType == "" || CashType == "Select") {
                    alert("Select Cash Type");
                    return false;
                }
                if (CashType == "Cash") {
                }
                else {
                    if (LevelType == "AccountsOfficer" || LevelType == "Director") {
                    }
                    else {
                        if (ddlEmpApprove == "" || ddlEmpApprove == "select") {
                            alert("Select Approval Name");
                            return false;
                        }
                    }
                }
            }
            if (ddlVoucherType == "Debit") {
                if (LevelType == "AccountsOfficer" || LevelType == "Director") {
                }
                else {
                    if (ddlEmpApprove == "" || ddlEmpApprove == "select") {
                        alert("Select Approval Name");
                        return false;
                    }
                }
            }
            var CashType = "";
            if (ddlVoucherType == "Due") {
                CashType = document.getElementById("ddlCashType").value;
                if (CashType == "" || CashType == "Select") {
                    alert("Select Cash Type");
                    return false;
                }
                if (CashType == "Cash") {
                }
                else {
                    if (LevelType == "AccountsOfficer" || LevelType == "Director") {
                    }
                    else {
                        if (ddlEmpApprove == "" || ddlEmpApprove == "select") {
                            alert("Select Approval Name");
                            return false;
                        }
                    }
                }
            }
            if (ddlVoucherType == "") {
                alert("Select Voucher Type");
                return false;
            }
            var Head = document.getElementById("combobox");
            var HeadLimit = Head.options[Head.selectedIndex].value;
            var ddlCashTo = Head.options[Head.selectedIndex].text;
            var count = 0;
            var IName = "";
            $('.AccountClass').each(function (i, obj) {
                if (count == 0) {
                    IName = $(this).text();
                    count++;

                }
            });
            ddlCashTo = IName;

            var salesOffice = document.getElementById("ddlsalesOffice").value;
            var btnSave = document.getElementById("btnSave").value;
            var spnVoucherID = document.getElementById("spnVoucherID").innerHTML;
            var rows = $("#tableCashFormdetails tr:gt(0)");
            var Cashdetails = new Array();
            $(rows).each(function (i, obj) {
                if ($(this).find('#txtProductQty').val() != "") {
                    Cashdetails.push({ SNo: $(this).find('#HeadSno').text(), Account: $(this).find('#txtAccount').text(), amount: $(this).find('#txtamount').text() });
                }
            });
            var data = { 'operation': 'BtnRaiseVoucherClick', 'Cashdetails': Cashdetails, 'Description': Description, 'Amount': txtAMount, 'Remarks': txtRemarks, 'EmpApprove': ddlEmpApprove, 'VoucherType': ddlVoucherType, 'CashTo': ddlCashTo, 'Employee': ddlEmployee, 'btnSave': btnSave, 'spnVoucherID': spnVoucherID, 'CashType': CashType, 'ddlBillHead': ddlBillHead, 'BranchID': salesOffice };
            var s = function (msg) {
                if (msg) {
                    alert(msg);
                    document.getElementById("ddlVoucherType").selectedIndex = 0;
                    document.getElementById("btnSave").value = "Raise";
                    document.getElementById("spnVoucherID").innerHTML = "";
                    document.getElementById("txtdesc").value = "";
                    document.getElementById("txtRemarks").value = "";
                    document.getElementById("ddlEmpApprove").selectedIndex = 0;
                    document.getElementById("txtAMount").innerHTML = "";
                    Cashform = [];
                    var results = '<div class="divcontainer" style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="example2_info" id="tableCashFormdetails">';
                    results += '<thead><tr><th scope="col">Head Of Account</th><th scope="col">Amount</th><th scope="col"></th></tr></thead></tbody>';
                    for (var i = 0; i < Cashform.length; i++) {
                        results += '<tr><th scope="row" class="AccountClass" id="txtAccount">' + Cashform[i].HeadOfAccount + '</th>';
                        results += '<td data-title="Code" class="AmountClass" id="txtamount">' + Cashform[i].Amount + '</td>';
                        results += '<td><img src="Image/Odclose.png" onclick="RowDeletingClick(this);" style="cursor:pointer;" width="30px" height="30px" alt="Edit" title="Edit Qty"/></td>';
                        results += '<td style="display:none" class="8" id="HeadSno">' + Cashform[i].HeadSno + '</td></tr>';
                    }
                    results += '</table></div>';
                    $("#divHeadAcount").html(results);
                    divRaiseVoucherClick();
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            CallHandlerUsingJson(data, s, e);
        }
        function CallHandlerUsingJson(d, s, e) {
            $.ajax({
                type: "GET",
                url: "DairyFleet.axd?json=",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(d),
                async: true,
                cache: true,
                success: s,
                error: e
            });
        }
        function divRaiseVoucherClick() {
            $('#divRaiseVoucher').css('display', 'block');
            $('#divViewVoucher').css('display', 'none');
            $('#divVocherPayable').css('display', 'none');
        }
        function divViewVoucherClick() {
            $('#divRaiseVoucher').css('display', 'none');
            $('#divViewVoucher').css('display', 'block');
            $('#divVocherPayable').css('display', 'none');
            $('#divVarifyVoucher').css('display', 'none');
            var LevelType = '<%=Session["LevelType"] %>';
            if (LevelType == "AccountsOfficer" || LevelType == "Director") {
                $('#divSOffice').css('display', 'block');
                Fillso();
            }
            else {
                $('#divSOffice').css('display', 'none');
            }
        }
        function Fillso() {
            var data = { 'operation': 'GetSalesOffice' };
            var s = function (msg) {
                if (msg) {
                    if (msg == "Session Expired") {
                        alert(msg);
                        window.location = "LoginDefault.aspx";
                    }
                    BindddlSo(msg);
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        function BindddlSo(msg) {
            var ddlsalesOffice = document.getElementById('ddlSo');
            var length = ddlsalesOffice.options.length;
            ddlsalesOffice.options.length = null;
            var opt = document.createElement('option');
            opt.innerHTML = "select";
            ddlsalesOffice.appendChild(opt);
            for (var i = 0; i < msg.length; i++) {
                if (msg[i].BranchName != null) {
                    var opt = document.createElement('option');
                    opt.innerHTML = msg[i].BranchName;
                    opt.value = msg[i].Sno;
                    ddlsalesOffice.appendChild(opt);
                }
            }
        }
        function divPayVoucherClick() {
            $('#divRaiseVoucher').css('display', 'none');
            $('#divViewVoucher').css('display', 'none');
            $('#divVocherPayable').css('display', 'block');
            $('#divVarifyVoucher').css('display', 'none');
        }
        function divVarifyVoucherClick() {
            $('#divRaiseVoucher').css('display', 'none');
            $('#divViewVoucher').css('display', 'none');
            $('#divVocherPayable').css('display', 'none');
            $('#divVarifyVoucher').css('display', 'none');
        }
        function BtnGetVoucherClick() {
            var VoucherID = document.getElementById("txtVoucherID").value;
            if (VoucherID == "") {
                alert("Enter Voucher ID");
                return false;
            }
            var data = { 'operation': 'BtnGetVoucherClick', 'VoucherID': VoucherID };
            var s = function (msg) {
                if (msg) {
                    if (msg == "No voucher found") {
                        alert(msg);
                        return false;
                    }
                    else {
                        var Status = msg[0].Status;
                        if (Status == "R") {
                            Status = "Raised";
                            $('.divpay').css('display', 'table-row');
                            $('.divCashierRemarks').css('display', 'table-row');
                            alert("Voucher Not Approved");
                            return false;
                        }
                        if (Status == "A") {
                            Status = "Approved";
                            $('.divpay').css('display', 'table-row');
                            $('.divCashierRemarks').css('display', 'table-row');
                        }
                        if (Status == "C") {
                            Status = "Rejected";
                            $('.divpay').css('display', 'table-row');
                            $('.divCashierRemarks').css('display', 'table-row');
                            alert("Voucher is cancelled");
                            return false;
                        }
                        if (Status == "P") {
                            Status = "Paid";
                            $('.divCashierRemarks').css('display', 'none');
                            $('.divpay').css('display', 'none');
                            alert("Voucher already paid");
                            return false;
                        }
                        document.getElementById("spnEmpName").innerHTML = msg[0].EmpName;
                        var VoucherType = msg[0].VoucherType;
                        document.getElementById("spnVoucherType").innerHTML = VoucherType;
                        document.getElementById("spnCashTo").innerHTML = msg[0].CashTo;
                        document.getElementById("spnDescription").innerHTML = msg[0].Description;
                        document.getElementById("spnAmount").innerHTML = msg[0].Amount;
                        document.getElementById("spnApprovalAmount").innerHTML = msg[0].ApprovalAmount;
                        document.getElementById("spnApprovalEmp").innerHTML = msg[0].ApproveEmpName;

                        document.getElementById("spnStatus").innerHTML = Status;
                        if (Status == "Raised") {
                            $('.divpay').css('display', 'none');
                            $('.divforce').css('display', 'table-row');
                        }
                        else {
                            $('.divforce').css('display', 'none');

                        }
                        document.getElementById("spnApprovalRemarks").innerHTML = msg[0].ApprovalRemarks;
                        document.getElementById("spnRemarks").innerHTML = msg[0].Remarks;
                        document.getElementById("hdnAprovalEmpid").value = msg[0].ApprovedBy;
                        document.getElementById("hdnEmpID").value = msg[0].Empid;
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
        function BtnGetVarifyVoucherClick() {
            var VoucherID = document.getElementById("txtVarifyVoucherID").value;
            if (VoucherID == "") {
                alert("Enter Voucher ID");
                return false;
            }
            var data = { 'operation': 'BtnGetVoucherClick', 'VoucherID': VoucherID };
            var s = function (msg) {
                if (msg) {
                    if (msg == "No voucher found") {
                        alert(msg);
                        return false;
                    }
                    else {

                        var VoucherType = msg[0].VoucherType;
                        var Status = msg[0].Status;
                        if (Status == "P") {
                        }
                        else {
                            alert("This voucher is not Paid");
                            return false;
                        }
                        if (VoucherType == "Due") {
                            $('#divClearViucher').css('display', 'table-row');
                        }
                        else {
                            $('#divClearViucher').css('display', 'none');
                            alert("This voucher is not varified");
                            return false;
                        }
                        document.getElementById("spnVarifyAmount").innerHTML = msg[0].ApprovalAmount;
                        document.getElementById("sonVarifyRemarks").innerHTML = msg[0].Remarks;
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
        function CountChange(count) {
            var TotalCash = 0;
            var Total = 0;
            if (count.value == "") {
                $(count).closest("tr").find(".TotalClass").text(Total);
                return false;
            }
            var Cash = $(count).closest("tr").find(".CashClass").text();
            Total = parseInt(count.value) * parseInt(Cash);
            $(count).closest("tr").find(".TotalClass").text(Total);
            $('.TotalClass').each(function (i, obj) {
                TotalCash += parseInt($(this).text());
            });
            document.getElementById('txt_Total').innerHTML = TotalCash;


        }
        function BtnPayVoucherClick() {
            var VoucherID = document.getElementById("txtVoucherID").value;
            var ApprovalAmount = document.getElementById("spnApprovalAmount").innerHTML;
            if (ApprovalAmount == "") {
                alert("Approval Amount can not be null");
                return false;
            }
            var DOE = document.getElementById('datepicker').value;
            var Remarks = document.getElementById("txtCashierRemarks").innerHTML;
            var VoucherType = document.getElementById("spnVoucherType").innerHTML;
            var Force = 0;
            var chkforce = document.getElementById("chkforce").checked;
            if (chkforce == true) {
                var Force = 1;
            }
            else {
                var Force = 0;
            }
            var rowsdenominations = $("#tableReportingDetails tr:gt(0)");
            var DenominationString = "";
            $(rowsdenominations).each(function (i, obj) {
                if ($(this).closest("tr").find(".CashClass").text() == "") {
                }
                else {
                    var str = $(this).closest("tr").find(".CashClass").text();

                    DenominationString += str.trim() + 'x' + $(this).closest("tr").find(".qtyclass").val() + "+";
                }
            });
            var data = { 'operation': 'BtnPayVoucherClick', 'VoucherID': VoucherID, 'ApprovalAmount': ApprovalAmount, 'Remarks': Remarks, 'Force': Force, 'VoucherType': VoucherType, 'DOE': DOE, 'DenominationString': DenominationString };
            var s = function (msg) {
                if (msg) {
                    alert(msg);
                    document.getElementById("txtVoucherID").value = "";
                    document.getElementById("spnApprovalAmount").innerHTML = "";
                    document.getElementById("txtCashierRemarks").value = "";
                    document.getElementById("spnVoucherType").innerHTML = "";
                    document.getElementById("spnCashTo").innerHTML = "";
                    document.getElementById("spnDescription").innerHTML = "";
                    document.getElementById("spnEmpName").innerHTML = "";
                    document.getElementById("spnAmount").innerHTML = "";
                    document.getElementById("spnRemarks").innerHTML = "";
                    document.getElementById("spnApprovalRemarks").innerHTML = "";
                    document.getElementById("spnApprovalEmp").innerHTML = "";
                    document.getElementById("spnStatus").innerHTML = "";
                    $('#divClearViucher').css('display', 'none');
                    $('.qtyclass input').val("0");




                    $('#tableReportingDetails tr').each(function () {
                        $(this).find('.qtyclass').val("0");
                    });

                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }

        function BtnGenerateClick() {
            var fromdate = document.getElementById("txtFromDate").value;
            var ToDate = document.getElementById("txtToDate").value;
            var ddlType = document.getElementById("ddlType").value;
            if (fromdate == "") {
                alert("Select From Date");
                return false;
            }
            if (ToDate == "") {
                alert("Select To Date");
                return false;
            }
            var branchID = "0";
            var LevelType = '<%=Session["LevelType"] %>';
            if (LevelType == "AccountsOfficer" || LevelType == "Director") {
                branchID = document.getElementById("ddlSo").value;
            }
            else {
            }
            var data = { 'operation': 'btnViewVoucherGeneretaeClick', 'fromdate': fromdate, 'ToDate': ToDate, 'Type': ddlType, 'BranchID': branchID };
            var s = function (msg) {
                if (msg) {
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
        //        function displayButtons(cellvalue, options, rowObject) {
        //            var edit = "<input  type='button' value='Edit ' onclick=\"calledit('" + options.rowId + "');\"  />",
        //             save = "<input  type='button' value='  Cancel' onclick=\"callCancelVoucher('" + options.rowId + "');\"  />",
        //             Print = "<input  type='button' value='  Print' onclick=\"callPrintVoucher('" + options.rowId + "');\"  />";
        //            return edit + save + Print;
        //        }
        var serial = 0;
        function BindViewVouchers(msg) {
            var results = '<div class="divcontainer" style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="example2_info">';
            results += '<thead><tr><th scope="col"></th><th scope="col"></th><th scope="col"></th><th scope="col">VoucherID</th><th scope="col">VoucherType</th><th scope="col">Amount</th><th scope="col">Approve Emp Name</th><th scope="col">Approve Amount</th><th scope="col">Status</th><th scope="col">Remarks</th><th scope="col">CashTo</th></tr></thead></tbody>';
            for (var i = 0; i < msg.length; i++) {
                results += '<tr><td><input id="btn_poplate" type="button"  onclick="calledit(this)" name="submit" class="btn btn-success" value="Edit" /></td>';
                results += '<td><input id="btn_poplate" type="button"  onclick="callCancelVoucher(this)" name="submit" class="btn btn-danger" value="Cancel" /></td>';
                results += '<td><input id="btn_poplate" type="button"  onclick="callPrintVoucher(this)" name="submit" class="btn btn-default" value="Print" /></td>';
                results += '<th scope="row" class="1">' + msg[i].VoucherID + '</th>';
                results += '<td  data-title="Code" class="4">' + msg[i].VoucherType + '</td>';
                results += '<td class="5">' + msg[i].Amount + '</td>';
                results += '<td class="6">' + msg[i].ApproveEmpName + '</td>';
                results += '<td data-title="Code" class="2">' + msg[i].Status + '</td>';
                results += '<td data-title="Code" class="9">' + msg[i].Remarks + '</td>';
                results += '<td data-title="Code" class="11">' + msg[i].CashTo + '</td>';
                results += '<td style="display:none data-title="Code" class="12">' + msg[i].Description + '</td>';
                results += '<td style="display:none" class="8">' + msg[i].BranchID + '</td></tr>';
            }
            results += '</table></div>';
            $("#div_Deptdata").html(results);
        }
        function calledit(thisid) {
            var VoucherID = $(thisid).parent().parent().children('.1').html();
            var VoucherType = $(thisid).parent().parent().children('.4').html();
            var BranchID = $(thisid).parent().parent().children('.8').html();
            var Amount = $(thisid).parent().parent().children('.5').html();
            var Remarks = $(thisid).parent().parent().children('.9').html();
            var EmpName = $(thisid).parent().parent().children('.6').html();
            var CashTo = $(thisid).parent().parent().children('.11').html();
            var ApproveEmpName = $(thisid).parent().parent().children('.6').html();
            var Status = $(thisid).parent().parent().children('.2').html();
            if (Status == "Raised") {
                if (CashTo == "Employee Expenses") {
                    $('.divEmp').css('display', 'table-row');
                }
                else {
                    $('.divEmp').css('display', 'none');
                }
                var Description = $(thisid).parent().parent().children('.12').html();
                //                var Description = $('#grd_brchtypemangement').getCell(rowid, 'Description');
                document.getElementById('spnVoucherID').innerHTML = VoucherID;
                document.getElementById('ddlVoucherType').value = VoucherType;
                if (VoucherType == "Debit") {
                    $('.divAprovalEmp').css('display', 'table-row');
                }
                if (VoucherType == "Credit") {
                    $('.divAprovalEmp').css('display', 'none');
                }
                if (VoucherType == "Due") {
                    $('.divAprovalEmp').css('display', 'table-row');
                }
                document.getElementById('txtdesc').value = Description;
                document.getElementById('txtAMount').innerHTML = Amount;
                document.getElementById('txtRemarks').value = Remarks;
                document.getElementById('ddlEmpApprove').text = ApproveEmpName;
                document.getElementById('btnSave').value = "Edit Voucher";
                $('#divRaiseVoucher').css('display', 'block');
                $('#divViewVoucher').css('display', 'none');
                $('#divVocherPayable').css('display', 'none');
                var data = { 'operation': 'GetSubPaybleValues', 'VoucherID': VoucherID, 'BranchID': BranchID };
                var s = function (msg) {
                    if (msg) {
                        var results = '<div class="divcontainer" style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="example2_info" id="tableCashFormdetails">';
                        results += '<thead><tr><th scope="col">Head Of Account</th><th scope="col">Amount</th><th scope="col"></th></tr></thead></tbody>';
                        for (var i = 0; i < msg.length; i++) {
                            results += '<tr><th scope="row" class="AccountClass" id="txtAccount">' + msg[i].HeadOfAccount + '</th>';
                            results += '<td data-title="Code" class="AmountClass" id="txtamount">' + msg[i].Amount + '</td>';
                            results += '<td><img src="Image/Odclose.png" onclick="RowDeletingClick(this);" style="cursor:pointer;" width="30px" height="30px" alt="Edit" title="Edit Qty"/></td>';
                            results += '<td style="display:none" class="8" id="HeadSno">' + msg[i].HeadSno + '</td></tr>';
                        }
                        results += '</table></div>';
                        $("#divHeadAcount").html(results);

                        //                        $('#divHeadAcount').setTemplateURL('CashForm.htm');
                        //                        $('#divHeadAcount').processTemplate(msg);
                    }
                    else {
                    }
                };
                var e = function (x, h, e) {
                };
                $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
                callHandler(data, s, e);
            }
            else {
                var altmsg = "Voucher is " + Status;
                alert(altmsg);
                return false;
            }
        }
        function callPrintVoucher(thisid) {
            var VoucherID = $(thisid).parent().parent().children('.1').html();
            var BranchID = $(thisid).parent().parent().children('.8').html();
            var data = { 'operation': 'btnVoucherPrintClick', 'VoucherID': VoucherID, 'BranchID': BranchID };
            var s = function (msg) {
                if (msg) {

                }
                else {
                    alert(msg);
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
            window.location = "PrintVoucher.aspx";
        }
        function callCancelVoucher(thisid) {
            var VoucherID = $(thisid).parent().parent().children('.1').html();
            var Status = $(thisid).parent().parent().children('.2').html();
            if (Status == "Raised") {
                if (!confirm("Do you really want Save")) {
                    return false;
                }
                var data = { 'operation': 'btnVoucherCancelClick', 'VoucherID': VoucherID };
                var s = function (msg) {
                    if (msg) {
                    }
                    else {
                    }
                };
                var e = function (x, h, e) {
                };
                $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
                callHandler(data, s, e);
            } else {
                var altmsg = "Voucher is " + Status;
                alert(altmsg);
                return false;
            }
        }

        function txtExpAmountChange(txtExpAmount) {
            var ApprovalAmount = document.getElementById("spnVarifyAmount").innerHTML;
            var ExpAmount = txtExpAmount.value;
            var BalAmount = ApprovalAmount - ExpAmount;
            document.getElementById("spnBalAmount").innerHTML = BalAmount;
        }
        function txtReceivedAmountChange(txtReceivedAmount) {
            var ApprovalAmount = document.getElementById("spnVarifyAmount").innerHTML;
            var ExpAmount = document.getElementById("txtExpAmount").value;
            var ReceivedAmount = document.getElementById("txtReceivedAmount").value;
            ExpAmount = parseFloat(ExpAmount).toFixed(2);
            ReceivedAmount = parseFloat(ReceivedAmount).toFixed(2);
            var BalAmount = ExpAmount + ReceivedAmount;
            BalAmount = parseFloat(BalAmount).toFixed(2);
            ApprovalAmount = parseFloat(ApprovalAmount).toFixed(2);
            if (ApprovalAmount == BalAmount) {
                $('.divClearRaiseVoucher').css('display', 'none');
                $('.divDue').css('display', 'none');
            }
            else {

                $('.divClearRaiseVoucher').css('display', 'table-row');
                $('.divDue').css('display', 'table-row');
            }
        }
        var Cashform = [];
        function BtnCashToAddClick() {
            var VoucherType = document.getElementById("ddlVoucherType").value;
            if (VoucherType == "Select" || VoucherType == "") {
                alert("Select Voucher Type");
                return false;
            }
            var Head = document.getElementById("combobox");
            var HeadSno = Head.options[Head.selectedIndex].value;
            var HeadOfAccount = Head.options[Head.selectedIndex].text;
            if (HeadOfAccount == "select" || HeadOfAccount == "") {
                alert("Select Account Name");
                return false;
            }
            var Checkexist = false;
            $('.AccountClass').each(function (i, obj) {
                var IName = $(this).text();
                if (IName == HeadOfAccount) {
                    alert("Account Already Added");
                    Checkexist = true;
                }
            });
            if (Checkexist == true) {
                return;
            }
            var Amount = document.getElementById("txtCashAmount").value;
            Amount = parseFloat(Amount);
            if (VoucherType == "Debit" || VoucherType == "Due") {
                var cashtype = document.getElementById("ddlCashType").value;
                if (cashtype == "BranchTransfer") {

                }
                else {
                    if (Amount > 10000) {
                        alert("Please Enter the Amount below 10,000 Only");
                        document.getElementById("txtCashAmount").value = "";
                        return false;
                    }
                }
            }
            if (Amount == "") {
                alert("Enter Amount");
                return false;
            }
            else {
                if (VoucherType == "Credit") {
                    Cashform.push({ HeadSno: HeadSno, HeadOfAccount: HeadOfAccount, Amount: Amount });
                    var results = '<div class="divcontainer" style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="example2_info" id="tableCashFormdetails">';
                    results += '<thead><tr><th scope="col">Head Of Account</th><th scope="col">Amount</th><th scope="col"></th></tr></thead></tbody>';
                    for (var i = 0; i < Cashform.length; i++) {
                        results += '<tr><th scope="row" class="AccountClass" id="txtAccount">' + Cashform[i].HeadOfAccount + '</th>';
                        results += '<td data-title="Code" class="AmountClass" id="txtamount">' + Cashform[i].Amount + '</td>';
                        results += '<td><img src="Image/Odclose.png" onclick="RowDeletingClick(this);" style="cursor:pointer;" width="30px" height="30px" alt="Edit" title="Edit Qty"/></td>';
                        results += '<td style="display:none" class="8" id="HeadSno">' + Cashform[i].HeadSno + '</td></tr>';
                    }
                    results += '</table></div>';
                    $("#divHeadAcount").html(results);
                    //                    $('#divHeadAcount').setTemplateURL('CashForm.htm');
                    //                    $('#divHeadAcount').processTemplate(Cashform);
                    var TotRate = 0.0;
                    $('.AmountClass').each(function (i, obj) {
                        if ($(this).text() == "") {
                        }
                        else {
                            TotRate += parseFloat($(this).text());
                        }
                    });
                    TotRate = parseFloat(TotRate).toFixed(2);
                    document.getElementById("txtAMount").innerHTML = TotRate;
                    document.getElementById("txtCashAmount").value = "";
                }
                else {
                    Amount = parseFloat(Amount);
                    var dlCash = document.getElementById("combobox").value;
                    var data = { 'operation': 'GetHeadLimit', 'HeadSno': dlCash };
                    var s = function (msg) {
                        if (msg) {
                            HeadLimit = msg;
                            HeadLimit = parseFloat(HeadLimit);
                            if (Amount >= HeadLimit) {
                                alert("Please Enter Amount Less Than limit");
                                return false;
                            }
                            Cashform.push({ HeadSno: HeadSno, HeadOfAccount: HeadOfAccount, Amount: Amount });
                            var results = '<div class="divcontainer" style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="example2_info" id="tableCashFormdetails">';
                            results += '<thead><tr><th scope="col">Head Of Account</th><th scope="col">Amount</th><th scope="col"></th></tr></thead></tbody>';
                            for (var i = 0; i < Cashform.length; i++) {
                                results += '<tr><th scope="row" class="AccountClass" id="txtAccount">' + Cashform[i].HeadOfAccount + '</th>';
                                results += '<td data-title="Code" class="AmountClass" id="txtamount">' + Cashform[i].Amount + '</td>';
                                results += '<td><img src="Image/Odclose.png" onclick="RowDeletingClick(this);" style="cursor:pointer;" width="30px" height="30px" alt="Edit" title="Edit Qty"/></td>';
                                results += '<td style="display:none" class="8" id="HeadSno">' + Cashform[i].HeadSno + '</td></tr>';
                            }
                            results += '</table></div>';
                            $("#divHeadAcount").html(results);
                            var TotRate = 0.0;
                            $('.AmountClass').each(function (i, obj) {
                                if ($(this).text() == "") {
                                }
                                else {
                                    TotRate += parseFloat($(this).text());
                                }
                            });
                            TotRate = parseFloat(TotRate).toFixed(2);
                            document.getElementById("txtAMount").innerHTML = TotRate;
                            document.getElementById("txtCashAmount").value = "";
                        }
                        else {
                        }
                    };
                    var e = function (x, h, e) {
                    };
                    callHandler(data, s, e);
                }
            }
        }
        function BtnClearRaiseVoucherClick() {
            var EmpName = document.getElementById("hdnEmpID").value;
            var ApprovalAmount = document.getElementById("spnApprovalAmount").innerHTML;
            var ExpAmount = document.getElementById("txtExpAmount").value;
            var ReceivedAmount = document.getElementById("txtReceivedAmount").innerHTML;
            var BalAmount = ExpAmount + ReceivedAmount;
            var Amount = ApprovalAmount - BalAmount;
            var VoucherType = document.getElementById("spnVoucherType").innerHTML;
            var CashTo = document.getElementById("spnCashTo").innerHTML;
            var Description = document.getElementById("spnDescription").innerHTML;
            var AprovedBy = document.getElementById("hdnAprovalEmpid").value;
            var data = { 'operation': 'BtnClearRaiseVoucherClick', 'Description': Description, 'Amount': Amount, 'VoucherType': VoucherType, 'CashTo': CashTo, 'Employee': EmpName, 'AprovedBy': AprovedBy };
            var s = function (msg) {
                if (msg) {
                    document.getElementById("spnVoucher").innerHTML = msg;
                    document.getElementById("SpnDue").innerHTML = Amount;
                    $('.ClearRaiseVoucher').css('display', 'none');
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        function RowDeletingClick(Account) {
            Cashform = [];
            var HeadOfAccount = "";
            var HeadSno = "";
            var Account = $(Account).closest("tr").find("#txtAccount").text();
            var Amount = "";
            var rows = $("#tableCashFormdetails tr:gt(0)");
            $(rows).each(function (i, obj) {
                if ($(this).find('#txtamount').text() != "") {
                    HeadOfAccount = $(this).find('#txtAccount').text();
                    HeadSno = $(this).find('#hdnHeadSno').val();
                    Amount = $(this).find('#txtamount').text();
                    if (Account == HeadOfAccount) {
                    }
                    else {
                        Cashform.push({ HeadSno: HeadSno, HeadOfAccount: HeadOfAccount, Amount: Amount });
                    }
                }
            });
            var results = '<div class="divcontainer" style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid" aria-describedby="example2_info" id="tableCashFormdetails">';
            results += '<thead><tr><th scope="col">Head Of Account</th><th scope="col">Amount</th><th scope="col"></th></tr></thead></tbody>';
            for (var i = 0; i < Cashform.length; i++) {
                results += '<tr><th scope="row" class="AccountClass" id="txtAccount">' + Cashform[i].HeadOfAccount + '</th>';
                results += '<td data-title="Code" class="AmountClass" id="txtamount">' + Cashform[i].Amount + '</td>';
                results += '<td><img src="Image/Odclose.png" onclick="RowDeletingClick(this);" style="cursor:pointer;" width="30px" height="30px" alt="Edit" title="Edit Qty"/></td>';
                results += '<td style="display:none" class="8" id="HeadSno">' + Cashform[i].HeadSno + '</td></tr>';
            }
            results += '</table></div>';
            $("#divHeadAcount").html(results);
            //            $('#divHeadAcount').setTemplateURL('CashForm.htm');
            //            $('#divHeadAcount').processTemplate(Cashform);
            var TotRate = 0.0;
            $('.AmountClass').each(function (i, obj) {
                if ($(this).text() == "") {
                }
                else {
                    TotRate += parseFloat($(this).text());
                }
            });
            TotRate = parseFloat(TotRate).toFixed(2);
            document.getElementById("txtAMount").innerHTML = TotRate;
        }
        function BtnVarifyVoucherSaveClick() {
            var VoucherID = document.getElementById("txtVarifyVoucherID").value;
            if (VoucherID == "") {
                alert("Enter Voucher ID");
                return false;
            }
            var ReceivedAmount = document.getElementById("txtReceivedAmount").value;
            if (ReceivedAmount == "") {
                alert("Enter Received Amount");
                return false;
            }
            var Due = document.getElementById("SpnDue").innerHTML;
            var data = { 'operation': 'BtnVarifyVoucherSaveClick', 'VoucherID': VoucherID, 'ReceivedAmount': ReceivedAmount, 'Due': Due };
            var s = function (msg) {
                if (msg) {
                    divVarifyVoucherClick();
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        function ddlVoucherTypeChange(Voucher) {
            var VoucherType = Voucher.value;
            var LevelType = '<%=Session["LevelType"] %>';
            if (VoucherType == "Debit") {
                if (LevelType == "AccountsOfficer" || LevelType == "Director") {
                }
                else {
                    $('.divAprovalEmp').css('display', 'table-row');
                }
                $('.divType').css('display', 'table-row');
                
            }
            if (VoucherType == "Credit") {
                $('.divAprovalEmp').css('display', 'none');
                $('.divType').css('display', 'table-row');
            }
            if (VoucherType == "Due") {
                if (LevelType == "AccountsOfficer" || LevelType == "Director") {
                }
                else {
                    $('.divAprovalEmp').css('display', 'table-row');
                }
                $('.divType').css('display', 'table-row');

            }
        }
        function FillAgents() {
            var data = { 'operation': 'GetAgentNames' };
            var s = function (msg) {
                if (msg) {
                    if (msg == "Session Expired") {
                        alert(msg);
                        window.location = "LoginDefault.aspx";
                    }
                    BindAgentNames(msg);
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        function BindAgentNames(msg) {
            var ddlAgent = document.getElementById('ddlCashTo');
            var length = ddlAgent.options.length;
            ddlAgent.options.length = null;
            for (var i = 0; i < msg.length; i++) {
                if (msg[i].BranchName != null) {
                    var opt = document.createElement('option');
                    opt.innerHTML = msg[i].BranchName;
                    opt.value = msg[i].Sno;
                    ddlAgent.appendChild(opt);
                }
            }
        }
        function ddlCashTypeChange(Cash) {
            var CashType = Cash.value;
            var VoucherType = document.getElementById("ddlVoucherType").value;
            if (VoucherType == "Credit") {
                if (CashType == "Cash") {
                    $('.divAprovalEmp').css('display', 'none');
                    $('#divMainAddNewRow').css('display', 'none');
                }
                else {
                    $('.divAprovalEmp').css('display', 'table-row');
                    $('#divMainAddNewRow').css('display', 'block');
                }
            }
            else {
                if (VoucherType == "Due") {
                    if (CashType == "Bills") {
                        alert("Select Cash Or Others");
                        return false;
                    }
                }
                $('.divAprovalEmp').css('display', 'table-row');
            }
        }
        function btnVoucherAddClick() {
            var ddlBillHead = document.getElementById("ddlBillHead").value;
            if (ddlBillHead == "select" || ddlBillHead == "") {
                alert("Select Head Of Account");
                return false;
            }
            $('#divMainAddNewRow').css('display', 'none');
        }
        function OrdersCloseClick() {
            $('#divMainAddNewRow').css('display', 'none');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
  <div style="width: 100%; background-color: #fff">
        <section class="content-header">
            <div style="width: 100%; float: left;">
                <a id="ancRaise" onclick="divRaiseVoucherClick();" style="width: 20%; font-size: 24px;
                    text-decoration: underline;">Raise Voucher</a> <a id="ancView" onclick="divViewVoucherClick();"
                        style="padding-left: 30%; font-size: 24px; text-decoration: underline;">View Vouchers</a>
                <a onclick="divPayVoucherClick();" style="padding-left: 30%; font-size: 24px; text-decoration: underline;">
                    Pay Voucher</a>
            </div>
            <br />
        </section>
        <section class="content">
            <div id="divRaiseVoucher">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            <i style="padding-right: 5px;" class="fa fa-cog"></i>Raise Voucher Details
                        </h3>
                    </div>
                </div>
                <br />
                <table align="center">
                    <tr class="divsalesOffice">
                        <td>
                            Sales Office
                        </td>
                        <td style="height: 40px;">
                            <select id="ddlsalesOffice" class="form-control" onchange="ddlSalesOfficeChange(this);">
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Voucher Type
                        </td>
                        <td style="height: 40px;">
                            <select id="ddlVoucherType" class="form-control" onchange="ddlVoucherTypeChange(this);">
                                <option>Select</option>
                                <option>Debit</option>
                                <option>Credit</option>
                                <option>Due</option>
                            </select>
                        </td>
                        <td class="divType" style="display: none; height: 40px;">
                            <select id="ddlCashType" class="form-control" onchange="ddlCashTypeChange(this);">
                                <option>Select</option>
                                <option>Cash</option>
                                <option>Bills</option>
                                <option>Others</option>
                                <option>BranchTransfer</option>
                            </select>
                        </td>
                    </tr>
                    <%--    <tr>
                    <td>
                        Account Type
                    </td>
                    <td>
                        <select id="ddlAccountType" class="form-control" onchange="ddlAccountTypeChange(this);">
                            <option>Select</option>
                            <option>Agent</option>
                            <option>Employee</option>
                            <option>Others</option>
                        </select>
                    </td>
                </tr> class="divPayTodesc" style="display: none"--%>
                    <tr>
                        <td>
                            Pay To
                        </td>
                        <td style="height: 40px;">
                            <input type="text" id="txtdesc" class="form-control" maxlength="45" placeholder="Enter PayTo" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Head Of Account
                        </td>
                        <td style="height: 40px;">
                            <%--<select id="ddlHeads" class="form-control" onchange="ddlCashToChange(this);">
                        </select>--%>
                            <select id="combobox" class="form-control">
                            </select>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="height: 40px;">
                            <input type="number" id="txtCashAmount" class="Spancontrol" placeholder="Enter Amount"
                                style="height: 30px;" />
                        </td>
                        <td style="height: 40px;">
                            <input type="button" id="Button3" value="Add" onclick="BtnCashToAddClick();" class="btn btn-success" />
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="1" colspan="2">
                            <div id="divHeadAcount">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Amount
                        </td>
                        <td style="height: 40px;">
                            <span id="txtAMount" class="form-control"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Remarks
                        </td>
                        <td style="height: 40px;">
                            <textarea rows="5" cols="45" id="txtRemarks" class="form-control" maxlength="2000"
                                placeholder="Enter Remarks"></textarea>
                        </td>
                    </tr>
                    <tr class="divAprovalEmp" style="display: none">
                        <td>
                            Requested For Aproval
                        </td>
                        <td style="height: 40px;">
                            <select id="ddlEmpApprove" class="form-control">
                            <option value="1">test</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="display: block;">
                        </td>
                        <td style="height: 40px;">
                            <input type="button" id="btnSave" value="Raise" onclick="BtnRaiseVoucherClick();"
                                class="btn btn-success" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            Voucher ID
                        </td>
                        <td style="height: 40px;">
                            <span id="spnVoucherID" class="Spancontrol"></span>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divViewVoucher" style="display: none;">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            <i style="padding-right: 5px;" class="fa fa-cog"></i>View Vocher
                        </h3>
                    </div>
                </div>
                <br />
                <table style="width: 100%;">
                    <tr>
                        <td id="divSOffice" style="display: none;">
                            <select id="ddlSo" class="form-control">
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
                            Type
                        </td>
                        <td>
                            <select id="ddlType" class="form-control">
                                <option>All</option>
                                <option value="R">Raised</option>
                                <option value="A">Approved</option>
                                <option value="C">Rejected</option>
                                <option value="P">Paid</option>
                            </select>
                        </td>
                        <%--</tr>
                <tr>--%>
                        <td style="width:6px;"></td>
                        <td>
                            <input type="button" id="btnGeneretae" value="Generate" onclick="BtnGenerateClick();"
                                class="btn btn-success" />
                        </td>
                    </tr>
                </table>
                <div id="div_Deptdata">
                </div>
            </div>
            <div id="divVocherPayable" style="display: none;">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            <i style="padding-right: 5px;" class="fa fa-cog"></i>Vocher Payable
                        </h3>
                    </div>
                </div>
                <br />
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 50%;">
                            <table align="center" style="border: 5px solid gray; border-radius: 3px 3px 3px 3px;
                                width: 100%;">
                                <tr>
                                    <td>
                                        <label>
                                            Date:</label>
                                    </td>
                                    <td style="height: 35px;">
                                        <input type="date" id="datepicker" class="form-control" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Voucher ID
                                    </td>
                                    <td style="height: 35px;">
                                        <input type="text" id="txtVoucherID" class="form-control" placeholder="Enter Voucher ID" />
                                    </td>
                                    <td style="height: 35px;">
                                        <input type="button" id="btnGet" value="Get" onclick="BtnGetVoucherClick();" class="btn btn-success" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Voucher Type
                                    </td>
                                    <td style="height: 35px;">
                                        <span id="spnVoucherType" class="form-control"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Cash To
                                    </td>
                                    <td style="height: 35px;">
                                        <span id="spnCashTo" class="form-control"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Description
                                    </td>
                                    <td style="height: 35px;">
                                        <span id="spnDescription" class="form-control"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Employee Name
                                    </td>
                                    <td style="height: 35px;">
                                        <span id="spnEmpName" class="form-control"></span>
                                        <input type="hidden" id="hdnEmpID" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Amount
                                    </td>
                                    <td style="height: 35px;">
                                        <span id="spnAmount" class="form-control"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Remarks
                                    </td>
                                    <td style="height: 30px;">
                                        <span id="spnRemarks" class="form-control"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Approval Amount
                                    </td>
                                    <td style="height: 35px;">
                                        <span id="spnApprovalAmount" class="form-control"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Approval Remarks
                                    </td>
                                    <td style="height: 35px;">
                                        <span id="spnApprovalRemarks" class="form-control"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Requested For Aproval
                                    </td>
                                    <td style="height: 35px;">
                                        <span id="spnApprovalEmp" class="form-control"></span>
                                        <input type="hidden" id="hdnAprovalEmpid" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Status
                                    </td>
                                    <td style="height: 35px;">
                                        <span id="spnStatus" class="Spancontrol"></span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 49%;">
                            <table align="center" style="border: 5px solid gray; border-radius: 3px 3px 3px 3px;
                                width: 100%;">
                                <tr class="divCashierRemarks" style="display: none">
                                    <td>
                                        Remarks
                                    </td>
                                    <td>
                                        <textarea rows="5" cols="45" id="txtCashierRemarks" class="form-control" maxlength="2000"
                                            placeholder="Enter Remarks"></textarea>
                                    </td>
                                </tr>
                                <tr class="divforce" style="display: none">
                                    <td>
                                        <input type="checkbox" id="chkforce" onchange="chkRequestedchange();" />
                                        Force Approval
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" style="width: 100%;" id="tableReportingDetails"
                                            class="mainText2" border="1">
                                            <thead>
                                                <tr>
                                                    <td style="width: 25%; height: 20px; color: #2f3293; font-size: 14px; font-weight: bold;
                                                        text-align: center;">
                                                        Cash
                                                        <br />
                                                    </td>
                                                    <td style="width: 25%; text-align: center; height: 20px; font-size: 14px; font-weight: bold;
                                                        color: #2f3293;">
                                                        Count
                                                        <br />
                                                    </td>
                                                    <td style="width: 10%; text-align: center; height: 20px; font-size: 14px; font-weight: bold;
                                                        color: #2f3293; padding: 0px 0px 0px 2%;">
                                                        Total
                                                        <br />
                                                    </td>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr class="tblrowcolor">
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span16" class="CashClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>1000</b></span>
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <b style="font-size: 11px; font-weight: bold;">X</b>
                                                        <input type="number" id="Number8" onkeyup="CountChange(this);" class="qtyclass" onkeypress="return numberOnlyExample();"
                                                            style="width: 80%; height: 24px; border: 1px solid gray; border-radius: 6px 6px 6px 6px;"
                                                            placeholder="Enter Count" />
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span17" class="TotalClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>0</b></span>
                                                    </td>
                                                </tr>
                                                <tr class="tblrowcolor">
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="txtsno" class="CashClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>500</b></span>
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <b style="font-size: 11px; font-weight: bold;">X</b>
                                                        <input type="number" id="txtCount" onkeyup="CountChange(this);" class="qtyclass"
                                                            onkeypress="return numberOnlyExample();" style="width: 80%; height: 24px; border: 1px solid gray;
                                                            border-radius: 6px 6px 6px 6px;" placeholder="Enter Count" />
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span2" class="TotalClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>0</b></span>
                                                    </td>
                                                </tr>
                                                <tr class="tblrowcolor">
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span1" class="CashClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>100</b></span>
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <b style="font-size: 11px; font-weight: bold;">X</b>
                                                        <input type="number" id="Number1" onkeyup="CountChange(this);" class="qtyclass" onkeypress="return numberOnlyExample();"
                                                            style="width: 80%; height: 24px; border: 1px solid gray; border-radius: 6px 6px 6px 6px;"
                                                            placeholder="Enter Count" />
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span3" class="TotalClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>0</b></span>
                                                    </td>
                                                </tr>
                                                <tr class="tblrowcolor">
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span4" class="CashClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>50</b></span>
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <b style="font-size: 11px; font-weight: bold;">X</b>
                                                        <input type="number" id="Number2" onkeyup="CountChange(this);" class="qtyclass" onkeypress="return numberOnlyExample();"
                                                            style="width: 80%; height: 24px; border: 1px solid gray; border-radius: 6px 6px 6px 6px;"
                                                            placeholder="Enter Count" />
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span5" class="TotalClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>0</b></span>
                                                    </td>
                                                </tr>
                                                <tr class="tblrowcolor">
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span6" class="CashClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>20</b></span>
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <b style="font-size: 11px; font-weight: bold;">X</b>
                                                        <input type="number" id="Number3" onkeyup="CountChange(this);" class="qtyclass" onkeypress="return numberOnlyExample();"
                                                            style="width: 80%; height: 24px; border: 1px solid gray; border-radius: 6px 6px 6px 6px;"
                                                            placeholder="Enter Count" />
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span7" class="TotalClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>0</b></span>
                                                    </td>
                                                </tr>
                                                <tr class="tblrowcolor">
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span8" class="CashClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>10</b></span>
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <b style="font-size: 11px; font-weight: bold;">X</b>
                                                        <input type="number" id="Number4" onkeyup="CountChange(this);" class="qtyclass" onkeypress="return numberOnlyExample();"
                                                            style="width: 80%; height: 24px; border: 1px solid gray; border-radius: 6px 6px 6px 6px;"
                                                            placeholder="Enter Count" />
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span9" class="TotalClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>0</b></span>
                                                    </td>
                                                </tr>
                                                <tr class="tblrowcolor">
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span10" class="CashClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>5</b></span>
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <b style="font-size: 11px; font-weight: bold;">X</b>
                                                        <input type="number" id="Number5" onkeyup="CountChange(this);" class="qtyclass" onkeypress="return numberOnlyExample();"
                                                            style="width: 80%; height: 24px; border: 1px solid gray; border-radius: 6px 6px 6px 6px;"
                                                            placeholder="Enter Count" />
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span11" class="TotalClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>0</b></span>
                                                    </td>
                                                </tr>
                                                <tr class="tblrowcolor">
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span12" class="CashClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>2</b></span>
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <b style="font-size: 11px; font-weight: bold;">X</b>
                                                        <input type="number" id="Number6" onkeyup="CountChange(this);" class="qtyclass" onkeypress="return numberOnlyExample();"
                                                            style="width: 80%; height: 24px; border: 1px solid gray; border-radius: 6px 6px 6px 6px;"
                                                            placeholder="Enter Count" />
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span13" class="TotalClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>0</b></span>
                                                    </td>
                                                </tr>
                                                <tr class="tblrowcolor">
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span14" class="CashClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>1</b></span>
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <b style="font-size: 11px; font-weight: bold;">X</b>
                                                        <input type="number" id="Number7" onkeyup="CountChange(this);" class="qtyclass" onkeypress="return numberOnlyExample();"
                                                            style="width: 80%; height: 24px; border: 1px solid gray; border-radius: 6px 6px 6px 6px;"
                                                            placeholder="Enter Count" />
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                        <span id="Span15" class="TotalClass" style="font-size: 14px; color: Red; font-weight: bold;">
                                                            <b>0</b></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 20%; height: 30px; vertical-align: top; font-size: 12px; font-weight: 500;
                                                        text-align: center; padding: 0px 0px 0px 3px">
                                                    </td>
                                                    <td style="width: 20%; height: 30px; vertical-align: middle; text-align: center;
                                                        color: Gray;">
                                                        <span style="font-size: 16px; color: Blue;">Total:</span>
                                                    </td>
                                                    <td style="width: 20%; height: 30px; font-size: 11px; vertical-align: middle; text-align: center;
                                                        color: Gray;" align="center">
                                                        <span style="font-size: 16px; color: Red; font-weight: bold;" id="txt_Total"></span>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="divpay" style="display: none">
                                    <td>
                                    </td>
                                    <td>
                                        <input type="button" id="btnPay" value="Pay" onclick="BtnPayVoucherClick();" class="btn btn-success" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divVarifyVoucher" style="display: none;">
                <table style="padding-left: 450px">
                    <tr>
                        <td>
                            <h2>
                                Varify Vocher</h2>
                        </td>
                    </tr>
                </table>
                <table align="center">
                    <tr>
                        <td>
                            Voucher ID
                        </td>
                        <td>
                            <input type="text" id="txtVarifyVoucherID" class="Spancontrol" placeholder="Enter Voucher ID" />
                        </td>
                        <td>
                            <input type="button" id="Button4" value="Get" onclick="BtnGetVarifyVoucherClick();"
                                class="SaveButton" style="width: 106px; height: 30px; font-size: 20px;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Amount
                        </td>
                        <td>
                            <span id="spnVarifyAmount" class="Spancontrol"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Remarks
                        </td>
                        <td>
                            <span id="sonVarifyRemarks" class="SpanRemarks"></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td rowspan="15" colspan="15">
                            <div class="divclass" id="divClearViucher" style="display: none;">
                                <table>
                                    <tr>
                                        <td>
                                            Expenses Amount
                                        </td>
                                        <td>
                                            <input type="text" id="txtExpAmount" class="Spancontrol" placeholder="Enter Expenses Amount"
                                                onchange="txtExpAmountChange(this);" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Balance Amount
                                        </td>
                                        <td>
                                            <span id="spnBalAmount" class="Spancontrol"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Received Amount
                                        </td>
                                        <td>
                                            <input type="text" id="txtReceivedAmount" class="Spancontrol" placeholder="Enter Received Amount"
                                                onchange="txtReceivedAmountChange(this);" />
                                        </td>
                                    </tr>
                                    <tr class="divClearRaiseVoucher" style="display: none;">
                                        <td>
                                            Voucher ID
                                        </td>
                                        <td>
                                            <span id="spnVoucher" class="Spancontrol"></span>
                                        </td>
                                        <td class="ClearRaiseVoucher">
                                            <input type="button" id="ClearRaiseVoucher" value="Raise Voucher" onclick="BtnClearRaiseVoucherClick();"
                                                class="SaveButton" style="width: 125px; height: 30px; font-size: 18px;" />
                                        </td>
                                    </tr>
                                    <tr class="divDue" style="display: none;">
                                        <td>
                                            if Due
                                        </td>
                                        <td>
                                            <span id="SpnDue" class="Spancontrol"></span>
                                            <%--    <input type="text" id="txtDue" class="Spancontrol" placeholder="Enter Received Amount" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <input type="button" id="Button1" value="Save" onclick="BtnVarifyVoucherSaveClick();"
                                                class="SaveButton" style="width: 156px; height: 30px; font-size: 24px;" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </section>
    </div>
    <div id="divMainAddNewRow" class="pickupclass" style="text-align: center; height: 100%;
        width: 100%; position: absolute; display: none; left: 0%; top: 0%; z-index: 99999;
        background: rgba(192, 192, 192, 0.7);">
        <div id="divAddNewRow" style="border: 5px solid #A0A0A0; position: absolute; top: 30%;
            background-color: White; left: 10%; right: 10%; width: 80%; height: 50%; -webkit-box-shadow: 1px 1px 10px rgba(50, 50, 50, 0.65);
            -moz-box-shadow: 1px 1px 10px rgba(50, 50, 50, 0.65); box-shadow: 1px 1px 10px rgba(50, 50, 50, 0.65);
            border-radius: 10px 10px 10px 10px;">
            <table align="left" cellpadding="0" cellspacing="0" style="float: left; width: 100%;"
                id="tableCollectionDetails" class="mainText2" border="1">
                <tr>
                    <td>
                        <label>
                            Head Of Account</label>
                    </td>
                    <td>
                        <select id="ddlBillHead" class="form-control">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input type="button" value="ADD Voucher" id="btnAdd" onclick="btnVoucherAddClick();"
                            class="ContinueButton" style="width: 100%; height: 40px; font-size: 16px;" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divclose" style="width: 35px; top: 24.5%; right: 9.5%; position: absolute;
            z-index: 99999; cursor: pointer;">
            <img src="Image/Odclose.png" alt="close" onclick="OrdersCloseClick();" />
        </div>
    </div>
</asp:Content>

