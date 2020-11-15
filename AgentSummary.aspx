<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AgentSummary.aspx.cs" Inherits="AgentSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script src="Js/JTemplate.js?v=3004" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="Css/VyshnaviStyles.css" />
    
    <style  type="text/css">

    #gridtable {
    font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
    border-collapse: collapse;
    width: 100%;
}

#gridtable td, #gridtable th {
    border: 1px solid #ddd;
    padding: 8px;
}

#gridtable tr:nth-child(even){background-color: #f2f2f2;}

#gridtable tr:hover {background-color: #ddd;}

#gridtable th {
    padding-top: 12px;
    padding-bottom: 12px;
    text-align: left;
    background-color: #4CAF50;
    color: white;
}


.textbox { 
    border: 1px solid #848484; 
    -webkit-border-radius: 30px; 
    -moz-border-radius: 30px; 
    border-radius: 30px; 
    outline:0; 
    height:25px; 
    width: 275px; 
    padding-left:10px; 
    padding-right:10px; 
  } 
  .button{
  background-color: #4CAF50;
  }


</style>








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

           $('#txtagentid').autocomplete({
               source: compiledList1,
               change: Agentchangeclick,
               autoFocus: true
           });
       }



       //        function fillagentdetails(msg) {
       //            var data = document.getElementById('Select_AgentId');
       //            var length = data.options.length;
       //            document.getElementById('Select_AgentId').options.length = null;
       //            var opt = document.createElement('option');
       //            opt.innerHTML = "Select Agent";
       //            opt.value = "Select Agent";
       //            opt.setAttribute("selected", "selected");
       //            opt.setAttribute("disabled", "disabled");
       //            opt.setAttribute("class", "dispalynone");
       //            data.appendChild(opt);
       //            for (var i = 0; i < msg.length; i++) {
       //                if (msg[i].Agent_Name != null) {
       //                    var option = document.createElement('option');
       //                    option.innerHTML = msg[i].Agent_Name;
       //                    option.value = msg[i].Agent_Id;
       //                    data.appendChild(option);

       //                }

       //            }
       //        }


       function Agentchangeclick() {
           var agentid = document.getElementById('txtagentid').value;
           var plantcode = document.getElementById('Slct_Plantcode').value;
           var data = { 'operation': 'get_Agent_Information_details', 'plantcode': plantcode, 'agentid': agentid };
           var s = function (msg) {
               if (msg) {
                   if (msg.length > 0) {
                       fillagentinformationdetails(msg);
                       fillagentSplCartagedetails(msg);
                       //fillloandetails(msg);
                       fillMilkdetails(msg);
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


       var agentdetails = [];
       function fillagentinformationdetails(msg) {
           //
           agentdetails = msg[0].Plant_Agent_Details;
           if (agentdetails.length != "0") {
               for (var i = 0; i < agentdetails.length; i++) {
                   var results = '<div  style="overflow:auto;"><table style="width: 40%;"   class="table table-bordered table-hover dataTable no-footer" id="gridtable" role="grid" aria- describedby="example2_info">';
                   results += '<thead><tr><th scope="col">Agent_Id</th>';
                   results += '<td th scope="row" class="1" >' + agentdetails[i].Agent_Id + '</td></tr>';
                   results += '<thead><tr><th scope="col">Agent_Name</th>';
                   results += '<td data-title="code"  class="2">' + agentdetails[i].Agent_Name + '</td></tr>';
                   results += '<thead><tr><th scope="col">RouteName</th>';
                   results += '<td data-title="status"  class="3">' + agentdetails[i].RouteName + '</td></tr>';
                   results += '<thead><tr><th scope="col">JoiningDate</th>';
                   results += '<td data-title="status"  class="4">' + agentdetails[i].JoiningDate + '</td></tr>';
                   results += '<thead><tr><th scope="col">MobileNo</th>';
                   results += '<td data-title="status"  class="4">' + agentdetails[i].MobileNo + '</td></tr>';
                   results += '<thead><tr><th scope="col">Address</th>';
                   results += '<td data-title="status"  class="4">' + agentdetails[i].Address + '</td></tr></thead></tbody>';
               }
           }
           else {

               results = '<div  style="overflow:auto;"><table style="width: 40%;" class="table table-bordered table-hover dataTable no-footer" id="gridtable"  role="grid" aria- describedby="example2_info">';
               results += '<thead><tr><th scope="col">Agent_Id</th></tr><tr><th scope="col">Agent_Name</th></tr><tr><th scope="col">RouteName</th></tr><tr><th scope="col">JoiningDate</th></tr><tr><th scope="col">MobileNo</th></tr><tr><th scope="col">Address</th></tr><tr></thead></tbody>';
           }
           results += '</table></div>';
           $("#div_Agetdetails").html(results);
       }

       var agentSplCartagedetails = [];
       function fillagentSplCartagedetails(msg) {
           agentSplCartagedetails = msg[0].agetCartage_Amt;
           var results = '<div  style="overflow:auto;"><table style="width: 40%;" class="table table-bordered table-hover dataTable no-footer" id="gridtable"  role="grid" aria- describedby="example2_info">';
           results += '<thead><tr><th scope="col">Year</th><th scope="col">Month</th><th scope="col">SpecialBonus</th><th scope="col">CartageAmount</th></tr></thead></tbody>';
           if (agentSplCartagedetails.length != "0") {
               for (var i = 0; i < agentSplCartagedetails.length; i++) {
                   // results += '<tr><td><input id="btn_poplate" type="button"  onclick="Gengetme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
                   results += '<tr><th scope="row" class="1" >' + agentSplCartagedetails[i].YEAR + '</td>';
                   results += '<td data-title="code" class="2">' + agentSplCartagedetails[i].Month + '</td>';
                   results += '<td data-title="status" class="3">' + agentSplCartagedetails[i].SplBonus_Amt + '</td>';
                   results += '<td data-title="status" class="4">' + agentSplCartagedetails[i].Cartage_Amt + '</td></tr>';
               }
           }
           results += '</table></div>';
           $("#div_AgentCartageSplBonus").html(results);
       }
       //        var agentloandetails = [];
       //        function fillloandetails(msg) {
       //            agentloandetails = msg[0].Agetloandetails;
       //            var results = '<div  style="overflow:auto;"><table style="width: 40%;" class="table table-bordered table-hover dataTable no-footer" role="grid" aria- describedby="example2_info">';
       //            results += '<thead><tr><th scope="col">LoanAmount</th><th scope="col">Recipt</th><th scope="col">Balance</th></tr></thead></tbody>';
       //            if (agentloandetails.length != "0") {
       //                    for (var i = 0; i < agentloandetails.length; i++) {
       //                    // results += '<tr><td><input id="btn_poplate" type="button"  onclick="Gengetme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
       //                    results += '<tr><th scope="row" class="1" >' + agentloandetails[i].LoanAmount + '</td>'; 
       //                    results += '<td data-title="code" class="2">' + agentloandetails[i].Recipt + '</td>';
       //                    results += '<td data-title="status" class="3">' + agentloandetails[i].Balance + '</td></tr>';
       //                }
       //            }
       //            results += '</table></div>';
       //            $("#div_AgentLoandetails").html(results);
       //        }

       //        var agentMilkdetails = [];
       //        function fillMilkdetails(msg) {
       //            agentMilkdetails = msg[0].AgetMilkdetails;
       //            if (agentMilkdetails.length != "0") {
       //                for (var i = 0; i < agentMilkdetails.length; i++) {
       //                    var results = '<div  style="overflow:auto;"><table style="width: 40%;" class="table table-bordered table-hover dataTable no-footer" role="grid" aria- describedby="example2_info">';
       //                    results += '<thead><tr><th scope="col">YEAR</th>';
       //                    results += '<td th scope="row" class="1" >' + agentMilkdetails[i].YEAR + '</td></tr>';
       //                    results += '<thead><tr><th scope="col">Month</th>';
       //                    results += '<td data-title="code"  class="2">' + agentMilkdetails[i].Month + '</td></tr>';
       //                    results += '<thead><tr><th scope="col">NoOfDays</th>';
       //                    results += '<td data-title="status"  class="3">' + agentMilkdetails[i].OrderCount + '</td></tr>';
       //                    results += '<thead><tr><th scope="col">FatAvg</th>';
       //                    results += '<td data-title="status"  class="4">' + agentMilkdetails[i].FatAvg + '</td></tr>';
       //                    results += '<thead><tr><th scope="col">SnfAvg</th>';
       //                    results += '<td data-title="status"  class="4">' + agentMilkdetails[i].SnfAvg + '</td></tr>';
       //                    results += '<thead><tr><th scope="col">MilkKgs</th>';
       //                    results += '<td data-title="status"  class="4">' + agentMilkdetails[i].MilkKgs + '</td></tr></thead></tbody>';
       //                }
       //            }
       //            else {
       //                results = '<div  style="overflow:auto;"><table style="width: 40%;" class="table table-bordered table-hover dataTable no-footer" role="grid" aria- describedby="example2_info">';
       //                results += '<thead><tr><th scope="col">YEAR</th></tr><tr><th scope="col">Month</th></tr><tr><th scope="col">OrderCount</th></tr><tr><th scope="col">FatAvg</th></tr><tr><th scope="col">SnfAvg</th></tr><tr><th scope="col">MilkKgs</th></tr><tr></thead></tbody>';
       //            }
       //            results += '</table></div>';
       //            $("#div_AgentMilkdetails").html(results);
       //        }
       var agentMilkdetails = [];
       function fillMilkdetails(msg) {
           agentMilkdetails = msg[0].AgetMilkdetails;
           if (agentMilkdetails.length != "0") {
               results = '<div  style="overflow:auto;"><table style="width: 40%;" class="table table-bordered table-hover dataTable no-footer" id="gridtable"  aria- describedby="example2_info">';
               results += '<thead><tr><th scope="col">YEAR</th><th scope="col">Month</th><th scope="col">OrderCount</th><th scope="col">FatAvg</th><th scope="col">SnfAvg</th><th scope="col">MilkKgs</th></tr></thead></tbody>';
               for (var i = 0; i < agentMilkdetails.length; i++) {
                   results += '<tr><td th scope="row" class="1" >' + agentMilkdetails[i].YEAR + '</td>';
                   results += '<td data-title="code"  class="2">' + agentMilkdetails[i].Month + '</td>';
                   results += '<td data-title="status"  class="3">' + agentMilkdetails[i].OrderCount + '</td>';
                   results += '<td data-title="status"  class="4">' + agentMilkdetails[i].FatAvg + '</td>';
                   results += '<td data-title="status"  class="4">' + agentMilkdetails[i].SnfAvg + '</td>';
                   results += '<td data-title="status"  class="4">' + agentMilkdetails[i].MilkKgs + '</td></tr>';
               }
           }
           else {

           }
           results += '</table></div>';
           $("#div_AgentMilkdetails").html(results);
       }
       function forclearall() {
           document.getElementById('txtagentid').value = ""
           document.getElementById('Slct_Plantcode').selectedIndex = 0;
       }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>
            Agent Summary Details<small>Preview</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Basic Information</a></li>
            <li><a href="#">Agent Summary Details</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <i style="padding-right: 5px;" class="fa fa-cog"></i>Agent Summary Details
                </h3>
            </div>
            <div>
                <table id="tbl_leavemanagement" align="center">
                    <tr>
                        <td style="height: 40px;">
                            PlantName
                        </td>
                        <td style="height: 40px;">
                            <select id="Slct_Plantcode" class="form-control" onchange="Plantchangeclick(this);">
                                <option selected disabled value="Select Plantcode">Select Plantcode</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 40px;">
                            AgentId
                        </td>
                        <td>
                            <input id="txtagentid" type="text" style="height: 28px; opacity: 1.0; width: 150px;"
                                class="textbox" name="vendorcode" placeholder="Search Code" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 40px;">
                            <input id="btn_save" type="button" class="btn btn-primary" name="submit" value="GET"
                                onclick="Get_Aget_Summary_Details();">
                            <input id="btn_close" type="button" class="btn btn-danger" name="submit" value="Clear"
                                onclick="forclearall();">
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <div>
                    <div id="div_Agetdetails" style="width: 50%; float: left;">
                    </div>
                    <div id="div_AgentMilkdetails" style="width: 50%; float: right;">
                    </div>
                </div>
                <table>
                </table>
                <div>
                    <div id="div_AgentCartageSplBonus" style="width: 50%; float: left;">
                    </div>
                    <div id="div_AgentLoandetails" style="width: 50%; float: right;">
                    </div>
                </div>
                <table>
                </table>
                <div>
                    <div id="div_AgentRemarks" style="width: 50%; float: right;">
                    </div>
                    <div id="div_AgentdeductionDetails" style="width: 33%; float: right;">
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
