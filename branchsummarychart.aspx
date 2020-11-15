<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"    CodeFile="branchsummarychart.aspx.cs" Inherits="branchsummarychart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="Kendo/jquery.min.js" type="text/javascript"></script>
    <script src="Kendo/kendo.all.min.js" type="text/javascript"></script>
    <link href="Kendo/kendo.common.min.css" rel="stylesheet" type="text/css" />
    <link href="Kendo/kendo.default.min.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.blockUI.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {

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

        function generate_branchwiselinechart() {
            var status = document.getElementById('slct_status').value;
            var date = document.getElementById('txt_fromdate').value;
            var data = { 'operation': 'get_branchsummarywise_milkdetails', 'status': status, 'date': date };
            var s = function (msg) {
                if (msg) {
                    if (msg.length > 0) {
                        createChart(msg);
                    }
                }
                else {
                }
            };
            var e = function (x, h, e) {
            }; $(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);
            callHandler(data, s, e);
        }
        var datainXSeries = 0;
        var datainYSeries = 0;
        var newXarray = [];
        var newYarray = [];
        function createChart(databind) {
            var newYarray = [];
            var newXarray = [];

            for (var k = 0; k < databind.length; k++) {
                var BranchName = [];
                var IndentDate = databind[k].Date;
                var DeliveryQty = databind[k].quantity;
                var Status = databind[k].status;
                newXarray = IndentDate.split(',');
                for (var i = 0; i < DeliveryQty.length; i++) {
                    newYarray.push({ 'data': DeliveryQty[i].split(','), 'name': Status[i] });
                }
            }
            //                var BranchName = [];
            //                var InwardDate = databind[0].Date;
            //                var inwardQty = databind[0].quantity;
            //                var Status = databind[0].Status;
            //                                newXarray = InwardDate.split(',');
            //                                for (var i = 0; i < inwardQty.length; i++) {
            //                                    newYarray.push({ 'data': inwardQty[i].split(','), 'name': Status[i] });
            //                                }
            $("#chart").kendoChart({
                title: {
                    text: "Plant Wise Milk summary details",
                    color: "#006600"
                },
                legend: {
                    position: "bottom"
                },
                chartArea: {
                    background: ""
                },
                seriesDefaults: {
                    type: "line",
                    style: "smooth"
                },
                series: newYarray,
                valueAxis: {
                    labels: {
                        format: "{0}%"
                    },
                    line: {
                        visible: false
                    },
                    axisCrossingValue: -10
                },
                categoryAxis: {
                    categories: newXarray,
                    //                        categories: [2002, 2003, 2004, 2005, 2006, 2007, 2008, 2009, 2010, 2011],
                    majorGridLines: {
                        visible: false
                    },
                    labels: {
                        rotation: 65
                    }
                },
                tooltip: {
                    visible: true,
                    format: "{0}%",
                    template: "#= series.name #: #= value #"
                }
            });
        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>

     <script type = "text/javascript">
         function PrintPanel() {
             var panel = document.getElementById("<%=pnlContents.ClientID %>");
             var printWindow = window.open('', '', 'height=400,width=800');
             //       printWindow.document.write('<html><head><title>DIV Contents</title>');
             printWindow.document.write('</head><body >');
             printWindow.document.write(panel.innerHTML);
             printWindow.document.write('</body></html>');
             printWindow.document.close();
             setTimeout(function () {
                 printWindow.print();
             }, 100);
             return false;
         }
    </script>

     <script type="text/javascript" src="https://www.google.com/jsapi"></script>

        

          <script type="text/javascript" src="https://www.google.com/jsapi"></script> 
        
        <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
   <script type="text/javascript">
       google.charts.load('current', { packages: ['corechart'] });     
   </script>
  
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
        
       <div class="form-style-9" style="height: auto">
<div class="form-style-3">
<table width=100% style="height: 93px">

<tr  ALIGN=LEFT>
<td WIDTH="98%" style="top: auto">

<fieldset style="height: auto" ><legend>Branch&nbsp; Summary Report </legend>






                            
    <center>
                
<asp:Panel ID="ser"  runat=server >

        <table class="style1">
            <tr width=100%>
                <td align="center">


                   
                    <asp:CheckBox ID="RadioButtonList1" runat="server" 
                        oncheckedchanged="RadioButtonList1_CheckedChanged" Text="Single Plant" 
                        AutoPostBack="True" />
                
                    
                    
                    </td>
            </tr>
            <tr width="100%">
                <td align="center" style="width: 100%" width="50%">
                    <span>From Date </span>
                    <asp:TextBox ID="txt_FromDate" runat="server"   Width="75px"></asp:TextBox>
                    <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                        Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                        TargetControlID="txt_FromDate">
                    </asp:CalendarExtender>
                    To<span> Date </span>
                    <asp:TextBox ID="txt_ToDate" runat="server" Width="75px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                        PopupButtonID="txt_ToDate" PopupPosition="TopRight" 
                        TargetControlID="txt_ToDate">
                    </asp:CalendarExtender>
                </td>
            </tr>
            <tr width="100%">
                <td align="center" style="width: 100%" width="50%">
                    <asp:Label ID="Label7" runat="server" Font-Size="Medium" Text="Plant Name"></asp:Label>
                    <asp:DropDownList ID="ddl_Plantname" runat="server" CssClass="tb10" 
                        Height="30px" Width="200px" 
                        onselectedindexchanged="ddl_charttype_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr width="100%">
                <td align="center" style="width: 100%" width="50%">
                    <asp:Label ID="Label6" runat="server" Font-Size="Medium" Text="Chart Type"></asp:Label>
                    <asp:DropDownList ID="ddl_charttype" runat="server" CssClass="tb10" 
                        Height="30px" onselectedindexchanged="ddl_charttype_SelectedIndexChanged" 
                        Width="200px">
                        <asp:ListItem Value="0">---SELECT----</asp:ListItem>
                        <asp:ListItem>PieChart</asp:ListItem>
                        <asp:ListItem>BarChart</asp:ListItem>
                        <asp:ListItem>LineChart</asp:ListItem>
                        <asp:ListItem>ColumnChart</asp:ListItem>
                        <asp:ListItem>AreaChart</asp:ListItem>
                        <asp:ListItem>SteppedAreaChart</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr width="100%">
                <td align="center" style="width: 100%" width="50%">
                    <asp:Button ID="Button6" runat="server" CssClass="form93" Font-Bold="False" 
                        Font-Size="X-Small" onclick="Button5_Click" 
                        OnClientClick="return confirmationSave();" TabIndex="6" Text="Get Report" 
                        xmlns:asp="#unknown" />
                    <asp:Button ID="Button7" runat="server" CssClass="form93" Font-Bold="False" 
                        Font-Size="X-Small" onclick="Button7_Click" 
                        OnClientClick="return PrintPanel();" TabIndex="6" Text="Print" 
                        xmlns:asp="#unknown" />
                </td>
            </tr>
        </table>

    </asp:Panel>

                
    
            
</label>
 </fieldset>


</td>
 
</tr>

</table>
</div>

<table style="width: 555px" >
        <tr width="100%" align=center>
            <td align="CENTER" >
           
                               <asp:Literal ID="lt1" runat="server"></asp:Literal>
           
 <asp:Literal ID="lt" runat="server"></asp:Literal>
           </td>
            <td align="CENTER" >
           
                &nbsp;</td>
        </tr>
        </table>
 
</div>

<center>
                           <br />
              <asp:Panel id="pnlContents" runat = "server">
              <center>
                               <asp:Literal ID="lt2" runat="server"></asp:Literal>
           </center>
                           <div ID="visualization" style="width: 1000px; height: 500px;"  >
                           </div>
                           
                           </center>
                           </asp:Panel>
  <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">

    </asp:ToolkitScriptManager>


</asp:Content>
