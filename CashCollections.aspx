<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CashCollections.aspx.cs" Inherits="CashCollections" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <link href="autocomplete/jquery-ui.css" rel="stylesheet" type="text/css" />
 <link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
 <script src="Js/JTemplate.js?v=3000" type="text/javascript"></script>
    <script src="Js/jquery.blockUI.js?v=3000" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Css/VyshnaviStyles.css" />
    <script src="js/jquery.json-2.4.js" type="text/javascript"></script>
    <script src="src/jquery-ui-1.8.13.custom.min.js" type="text/javascript"></script>
    <link href="js/DateStyles.css?v=3003" rel="stylesheet" type="text/css" />
    <script src="js/1.8.6.jquery.ui.min.js" type="text/javascript"></script>
    <style type="text/css">
        .ddlsize
        {
            width: 196px;
            height: 30px;
            font-size: 16px;
            border: 1px solid gray;
            border-radius: 7px 7px 7px 7px;
        }
        .datepicker
        {
            border: 1px solid gray;
            background: url("Images/CalBig.png") no-repeat scroll 99%;
            width: 70%;
            top: 0;
            left: 0;
            height: 20px;
            font-weight: 700;
            font-size: 12px;
            cursor: pointer;
            border: 1px solid gray;
            margin: .5em 0;
            padding: .6em 20px;
            border-radius: 10px 10px 10px 10px;
            filter: Alpha(Opacity=0);
            box-shadow: 3px 3px 3px #ccc;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            get_autocomplete_details();
            $("#datepicker").datepicker({ dateFormat: 'yy-mm-dd', numberOfMonths: 1, showButtonPanel: false, maxDate: '+13M +0D',
                onSelect: function (selectedDate) {
                }
            });
            $("#dtchequedate").datepicker({ dateFormat: 'yy-mm-dd', numberOfMonths: 1, showButtonPanel: false, maxDate: '+13M +0D',
                onSelect: function (selectedDate) {
                }
            });
            var date = new Date();
            var day = date.getDate();
            var month = date.getMonth() + 1;
            var year = date.getFullYear();
            if (month < 10) month = "0" + month;
            if (day < 10) day = "0" + day;
            today = year + "-" + month + "-" + day;
            $('#datepicker').val(today);
            FillSalesOffice();
        });
        function FillSalesOffice() {
            var data = { 'operation': 'GetPlantSalesOffice' };
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
        function BindSalesOffice(msg) {
            var ddlsalesOffice = document.getElementById('ddlSalesOffice');
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
        function ddlSalesOfficeChange(ID) {
            var BranchID = ID.value;
            var data = { 'operation': 'GetSalesRoutes', 'BranchID': BranchID };
            var s = function (msg) {
                if (msg) {
                    if (msg == "Session Expired") {
                        alert(msg);
                        window.location = "LoginDefault.aspx";
                    }
                    BindRouteName(msg);
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        function BindRouteName(msg) {
            document.getElementById('ddlRouteName').options.length = "";
            var veh = document.getElementById('ddlRouteName');
            var length = veh.options.length;
            for (i = length - 1; i >= 0; i--) {
                veh.options[i] = null;
            }
            var opt = document.createElement('option');
            opt.innerHTML = "Select Route Name";
            opt.value = "";
            veh.appendChild(opt);
            for (var i = 0; i < msg.length; i++) {
                if (msg[i] != null) {
                    var opt = document.createElement('option');
                    opt.innerHTML = msg[i].RouteName;
                    opt.value = msg[i].rid;
                    veh.appendChild(opt);
                }
            }
        }
        function ddlRouteNameChange(id) {
            FillAgentName(id.value);
        }
        function FillAgentName(RouteID) {
            var data = { 'operation': 'GetAgents', 'RouteID': RouteID };
            var s = function (msg) {
                if (msg) {
                    BindAgentName(msg);
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        function BindAgentName(msg) {
            document.getElementById('ddlAgentName').options.length = "";
            var ddlAgentName = document.getElementById('ddlAgentName');
            var length = ddlAgentName.options.length;
            for (i = length - 1; i >= 0; i--) {
                ddlAgentName.options[i] = null;
            }
            var opt = document.createElement('option');
            opt.innerHTML = "Select Agent Name";
            opt.value = "";
            ddlAgentName.appendChild(opt);
            for (var i = 0; i < msg.length; i++) {
                if (msg[i] != null) {
                    var opt = document.createElement('option');
                    opt.innerHTML = msg[i].BranchName;
                    opt.value = msg[i].Sno;
                    ddlAgentName.appendChild(opt);
                }
            }
        }
        var ledgernames = [];
        function get_autocomplete_details() {
            var data = { 'operation': 'get_ledger_details1' };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        ledgernames = msg;
                        var availableTags = [];
                        for (var i = 0; i < msg.length; i++) {
                            var ledgername = msg[i].ledgername;
                            availableTags.push(ledgername);
                        }
                        $('#txtDesc').autocomplete({
                            source: availableTags,
                            change: ledgerchange,
                            autoFocus: true
                        });
                    }
                }
                else {
                }
            };
            var e = function (x, h, e) {
                alert(e.toString());
            };
            callHandler(data, s, e);
        }

        function ledgerchange() {
            var name = document.getElementById('txtDesc').value;
            for (var i = 0; i < ledgernames.length; i++) {
                if (name == ledgernames[i].ledgername) {
                    document.getElementById('txt_code').value = ledgernames[i].ledgercode;
                    document.getElementById('txtHiddenName').value = ledgernames[i].ledgername;
                }
            }
        }

        function ledgercodefill() {
            var data = { 'operation': 'get_ledger_details' };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillledgercode(msg);
                    }
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            callHandler(data, s, e);
        }
        function fillledgercode(msg) {
            var data = document.getElementById('slct_role');
            var length = data.options.length;
            document.getElementById('slct_role').options.length = null;
            var opt = document.createElement('option');
            opt.innerHTML = "Select Role";
            opt.value = "Select Role";
            opt.setAttribute("selected", "selected");
            opt.setAttribute("disabled", "disabled");
            opt.setAttribute("class", "dispalynone");
            data.appendChild(opt);
            for (var i = 0; i < msg.length; i++) {
                if (msg[i].roles != null) {
                    var option = document.createElement('option');
                    option.innerHTML = msg[i].ledgercode;
                    option.value = msg[i].ledgername;
                    data.appendChild(option);
                }
            }
        }



        function BtnCashAmountClick() {
            var Name = document.getElementById('txtDesc').value;
            var MasterName = document.getElementById('txtHiddenName').value;
            if (MasterName == Name) {
            }
            else {
                alert("This Name Does Not Exist");
                return false;
            }



            var ledgercode = document.getElementById('txt_code').value;
            var ddlAmountType = document.getElementById('ddlAmountType').value;
            var chequeDate = "";
            var txtChequeNo = "";
            var txtBankName = "";
            if (ddlAmountType == "Select") {
                alert("Select Amount Type");
                return false;
            }
            if (ddlAmountType == "Cheque") {
                txtChequeNo = document.getElementById('txtChequeNo').value;
                chequeDate = document.getElementById('dtchequedate').value;
                txtBankName = document.getElementById('txtBankName').value;
                if (txtChequeNo == "") {
                    alert("Enter Cheque No");
                    return false;
                }
                if (chequeDate == "") {
                    alert("Enter chequeDate");
                    return false;
                }
                if (txtBankName == "") {
                    alert("Enter Bank Name");
                    return false;
                }
            }
            if (ddlAmountType == "DD") {
                txtChequeNo = document.getElementById('txtChequeNo').value;
                if (txtChequeNo == "") {
                    alert("Enter DD No");
                    return false;
                }
            }
            var txtAmount = document.getElementById('txtAmount').value;
            if (txtAmount == "") {
                alert("Enter Amount");
                return false;
            }
            var txtCashierRemarks = document.getElementById('txtCashierRemarks').value;
            if (txtCashierRemarks == "") {
                alert("Enter Remarks");
                return false;
            }
            if (!confirm("Do you want to save this transaction")) {
                return false;
            }

            var data = { 'operation': 'BtnCashAmountClick', 'Name': Name, 'ledgercode': ledgercode, 'ddlAmountType': ddlAmountType, 'ChequeNo': txtChequeNo, 'chequeDate': chequeDate, 'BankName': txtBankName, 'Amount': txtAmount, 'Remarks': txtCashierRemarks };
            var s = function (msg) {
                if (msg) {
                    alert(msg);
                    document.getElementById('txtDesc').value = "";
                    document.getElementById('txtAmount').value = "";
                    document.getElementById('txt_code').value = "";
                    document.getElementById('txtCashierRemarks').value = "";
                }
                else {
                }
            };
            var e = function (x, h, e) {
            };
            $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        function ddlCollectionTypeChange() {
            var collectiontype = document.getElementById('ddlcollectiontype').value;
            if (collectiontype == "Select") {
                alert("Select collection Type");
                return false;
            }
            if (collectiontype == "Agent") {

                $('.divsalesoffclass').css('display', 'table-row ');
                $('.divRouteclass').css('display', 'table-row ');
                $('.divAgentclass').css('display', 'table-row ');
                $('.divnameclass').css('display', 'none');

            }
            if (collectiontype == "Other") {

                $('.divsalesoffclass').css('display', 'none ');
                $('.divRouteclass').css('display', 'none ');
                $('.divAgentclass').css('display', 'none ');
                $('.divnameclass').css('display', 'table-row');

            }


        }
        var PaymentType = "";
        function ddlPaymentTypeChange(Payment) {
            PaymentType = Payment.options[Payment.selectedIndex].text;
            if (PaymentType == "Cash") {
                $('.divChequeclass').css('display', 'none');
                $('.divChequeDateclass').css('display', 'none');
                $('.divBankclass').css('display', 'none');


            }
            if (PaymentType == "Bank Transfer") {
                $('.divChequeclass').css('display', 'none');
                $('.divChequeDateclass').css('display', 'none');
                $('.divBankclass').css('display', 'none');
            }
            if (PaymentType == "Cheque") {
                $('.divChequeclass').css('display', 'table-row');
                $('.divChequeDateclass').css('display', 'table-row');
                $('.divBankclass').css('display', 'table-row');

                document.getElementById("spnchequeno").innerHTML = "Cheque No";

                var input = document.getElementById("txtChequeNo");
                input.placeholder = "Enter Cheque No";
            }
            if (PaymentType == "DD") {
                $('.divChequeclass').css('display', 'table-row');
                $('.divChequeDateclass').css('display', 'table-row');
                $('.divBankclass').css('display', 'table-row');

                document.getElementById("spnchequeno").innerHTML = "DD No";

                var input = document.getElementById("txtChequeNo");
                input.placeholder = "Enter DD No";
            }
            if (PaymentType == "Journal Voucher") {
                $('.divChequeclass').css('display', 'table-row');
                document.getElementById("spnchequeno").innerHTML = "Voucher No";
                var input = document.getElementById("txtChequeNo");
                input.placeholder = "Enter Journal Voucher";
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
<section class="content-header">
        <h1>
            Cash Collections<small>Preview</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Reports</a></li>
            <li><a href="#">Cash Collections</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <i style="padding-right: 5px;" class="fa fa-cog"></i>Cash Collections Details
                </h3>
            </div>
            <div class="box-body">
                <table align="center">
                    <tr>
                    <tr class="divnameclass">
                        <td id="txtname">
                         <label>    Name<span style="color: red;">*</span></label> 
                        </td>
                        <td style="height:40px;">
                            <input type="text" id="txtDesc" class="form-control" placeholder="Enter Name" />
                        </td>
                    </tr>
                     <tr>
                            <td>
                                <label> Ledger Code</label>
                            </td>
                            <td style="height: 40px;">
                                <input id="txt_code" type="text" name="Last Name" class="form-control"  placeholder="Enter Ledger Code"  />
                            </td>
                            <td style="height: 40px;">
                            <input id="txtHiddenName" type="hidden" class="form-control" name="HiddenName" />
                        </td>
                            </tr>
                            
                    <tr>
                        <td>
                        <label>     <span>Amount Type<span style="color: red;">*</span></span></label> 
                        </td>
                        <td style="height:40px;">
                            <select id="ddlAmountType" class="form-control" onchange="ddlPaymentTypeChange(this);">
                                <option>Select</option>
                                <option>Cash</option>
                                <option>Cheque</option>
                                <option>DD</option>
                                <option>Bank Transfer</option>
                                <option>Journal Voucher</option>
                            </select>
                        </td>
                    </tr>
                    <tr class="divChequeclass" style="display: none;">
                        <td>
                         <label>    <span id="spnchequeno">Cheque No</span></label> 
                        </td>
                        <td id="divCheque" style="height:40px;">
                            <input type="text" id="txtChequeNo" class="Spancontrol" placeholder="Enter Cheque No" />
                        </td>
                    </tr>
                    <tr class="divBankclass" style="display: none;">
                        <td>
                          <label>   <span>Bank Name</span></label> 
                        </td>
                        <td style="height:40px;">
                            <input type="text" id="txtBankName" class="Spancontrol" placeholder="Enter Bank Name" />
                        </td>
                    </tr>
                    <tr class="divChequeDateclass" style="display: none;">
                        <td>
                        <label>     <span>Cheque Date</span></label> 
                        </td>
                        <td id="divchequedate" style="height:40px;">
                            <input type="text" name="journey_date" class="datepicker" tabindex="3" readonly="readonly"
                                id="dtchequedate" placeholder="DD-MM-YYYY" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <label>   <span>Amount<span style="color: red;">*</span></span></label> 
                        </td>
                        <td style="height:40px;">
                            <input type="text" id="txtAmount" class="form-control" placeholder="Enter Amount" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                      <label>       Remarks<span style="color: red;">*</span></label> 
                        </td>
                        <td style="height:40px;">
                            <textarea rows="5" cols="45" id="txtCashierRemarks" class="form-control" maxlength="2000"
                                placeholder="Enter Remarks"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="height:40px;">
                            <input type="button" id="btnSave" value="Save" class="btn btn-success" onclick="BtnCashAmountClick();"/>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </section>
</asp:Content>

