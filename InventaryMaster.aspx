<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="InventaryMaster.aspx.cs" Inherits="InventaryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
    <script type="text/javascript">


     $(function () {
         get_InvetaryMaster_details();
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

     function saveInvetary() {
//         var type = document.getElementById('ddlcantype').value;
//         if (type == "" || type == "Select type") {

//             alert("Select type ");
//             return false;
//         }
         var inventaryname = document.getElementById('txtInventaryName').value;
         if (inventaryname == "") {
             alert("Enter inventaryname ");
             return false;
         }
         var qty = document.getElementById('txtquantity').value;
         var btnval = document.getElementById('btnInventary').value;
         var inventorysno = document.getElementById('lbl_sno').value;
         var data = { 'operation': 'saveInvetary', 'inventaryname': inventaryname, 'qty': qty, 'btnVal': btnval, 'inventorysno': inventorysno };
         var s = function (msg) {
             if (msg) {
                 if (msg.length > 0) {
                     alert(msg);
                     get_InvetaryMaster_details();
                    
                    // subqtyforclearall();
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
     function inventaryforclearall() {
            document.getElementById('txtInventaryName').value = "";
            document.getElementById('txtquantity').value = "";
//            document.getElementById('ddlsubcateqty').selectedIndex = 0;
//            document.getElementById('txtquantity').value = "";
            document.getElementById('lbl_sno').value = "";
            document.getElementById('btnInventary').value = "Save";
            $("#lbl_code_error_msg").hide();
            $("#lbl_name_error_msg").hide();
            $("#div_SubCategoryQty").show();
            $("#grid_SubCategoryQty").show();
        }

     function get_InvetaryMaster_details() {
         var data = { 'operation': 'get_InvetaryMaster_details' };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        fillInvetaryMaster(msg);
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

        function fillInvetaryMaster(msg) {
            var results = '<div  style="overflow:auto;"><table class="table table-bordered table-hover dataTable no-footer" role="grid">';
            results += '<thead><tr><th scope="col"></th><th scope="col">InventaryName</th><th scope="col">Quantity</th></tr></thead></tbody>';
            for (var i = 0; i < msg.length; i++) {
                results += '<tr><td><input id="btn_poplate" type="button"  onclick="inventarygetme(this)" name="submit" class="btn btn-primary" value="Edit" /></td>';
              //  results += '<th scope="row" class="1" >' + msg[i].category + '</th>';
                results += '<td data-title="code" class="2">' + msg[i].inventaryname + '</td>';
                results += '<td  data-title="code" class="3">' + msg[i].qty + '</td>';
                results += '<td style="display:none" class="4">' + msg[i].inventorysno + '</td></tr>';
            }
            results += '</table></div>';
            $("#grid_InventaryDetails").html(results);
        }
        function inventarygetme(thisid) {
          //  var category = $(thisid).parent().parent().children('.1').html();
            var inventaryname = $(thisid).parent().parent().children('.2').html();
            var qty = $(thisid).parent().parent().children('.3').html();
            var sno = $(thisid).parent().parent().children('.4').html();

          //  document.getElementById('ddlcantype').value = plantid;
            document.getElementById('txtInventaryName').value = inventaryname;
            document.getElementById('txtquantity').value = qty;
            document.getElementById('btnInventary').value = "Modify";
            document.getElementById('lbl_sno').value = sno;
           
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h1>
            Inventary Master Details<small>Preview</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="#"><i class="fa fa-dashboard"></i>Basic Information</a></li>
            <li><a href="#">Inventary Master Details</a></li>
        </ol>
    </section>
    <section class="content">
        <div class="box box-info">
            <div class="box-header with-border">
                <h3 class="box-title">
                    <i style="padding-right: 5px;" class="fa fa-cog"></i>Inventary Master Details
                </h3>
            </div>
            <div>
                <table align="center">
                   <%-- <tr>
                        <td>
                            <label>
                                Select type</label>
                        </td>
                        <td style="height: 40px;">
                            <select id="ddlcantype" class="form-control" onchange="getmaterialwise(this);">
                                <option value="Select Type" disabled selected>Select Type</option>
                                <option value="1">Feed</option>
                                <option value="2">Dpu</option>
                                <option value="3">Meterials</option>
                                <option value="4">Can</option>
                            </select>
                        </td>
                    </tr>--%>
                    <tr>
                        <td>
                            <label>
                                Inventary Name</label>
                        </td>
                        <td>
                            <input id="txtInventaryName" type="text" class="form-control" name="Quantity" placeholder="Enter Quantity" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                Quantity</label>
                        </td>
                        <td>
                            <input id="txtquantity" type="text" maxlength="45" class="form-control" name="Quantity"
                                placeholder="Enter Quantity" />
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
                            <input id="btnInventary" type="button" class="btn btn-primary" name="submit" value='save'
                                onclick="saveInvetary();" />
                            <input id='btn_closeqty' type="button" class="btn btn-danger" name="Close" value='Close'
                                onclick="inventaryforclearall();" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="grid_InventaryDetails">
            </div>
        </div>
    </section>
</asp:Content>
