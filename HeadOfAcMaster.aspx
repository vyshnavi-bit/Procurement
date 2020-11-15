<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HeadOfAcMaster.aspx.cs" Inherits="HeadOfAcMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
      <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
  <script type="text/javascript">
      $(function () {
          get_plant_details();
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
          document.getElementById('slct_plantcode').value = plantcode  ;
          document.getElementById('txtLimit').value = Limit;
          document.getElementById('btnSave').value = "Modify";
          document.getElementById('lbl_sno').value = Sno;
      }
      function Headvalidation() {
          var Decription = "";
          Decription = document.getElementById("txtDecription").value;
          if (Decription == "") {
              alert("Enter Head Description");
              return false;
          }
          var ledger_code = document.getElementById('txtLedgerCode').value;
          var plantcode = document.getElementById('slct_plantcode').value;
          if (plantcode == "") {
              alert("Select plantcode");
              return false;
          }
          var Limit = document.getElementById('txtLimit').value;
          var btnSave = document.getElementById('btnSave').value;
          var sno = document.getElementById('lbl_sno').value;
          var data = { 'operation': 'SaveHeadMasterClick', 'plantcode': plantcode, 'ledger_code': ledger_code, 'Decription': Decription, 'serial': sno, 'btnSave': btnSave, 'Limit': Limit };
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <section class="content-header">
        <h1>
            Head Accounts Master<small>Preview</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Operations</a></li>
            <li><a href="#">Head Accounts Master</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <i style="padding-right: 5px;" class="fa fa-cog"></i>Head Accounts Details
                </h3>
            </div>
            <div class="box-body">
                <table align="center">
                 <tr>
                        <td>
                            <label> Plant Code<span style="color: red;">*</span></label>
                        </td>
                        <td style="height: 40px;">
                            <select  id="slct_plantcode" class="form-control" onchange="UpdateHeads();"></select>
                        </td>
                    </tr>
                    <tr class="divPayTodesc">
                        <td>
                          <label>  Head Of Account<span style="color: red;">*</span></label>
                        </td>
                        <td style="height: 40px;">
                            <input type="text" id="txtDecription" class="form-control" placeholder="Enter Head Decription" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label> Ledger Code<span style="color: red;">*</span></label>
                        </td>
                        <td style="height: 40px;">
                            <input type="text" id="txtLedgerCode" class="form-control" placeholder="Enter LedgerCode" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>   Limit<span style="color: red;">*</span></label>
                        </td>
                        <td style="height: 40px;">
                            <input type="text" id="txtLimit" class="form-control" placeholder="Enter Limit" />
                        </td>
                    </tr>
                      <tr style="display:none;"><td>
                            <label id="lbl_sno"></label>
                            </td>
                            </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="height: 40px;">
                            <input type="button" id="btnSave" value="SAVE" onclick="Headvalidation();" class="btn btn-success"/>
                        </td>
                    </tr>
                </table>
                <div id="divHeadAcount">
                </div>
            </div>
        </div>
    </section>
</asp:Content>

