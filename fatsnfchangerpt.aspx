<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="fatsnfchangerpt.aspx.cs" Inherits="fatsnfchangerpt" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="dist/css/AdminLTE.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
    <style>
        .button
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            background-color: #4CAF50; /* Green */
            color: white;
            padding: 10px 26px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
            font-weight: 700;
        }
        
        .button2
        {
            background-color: #008CBA;
        }
        /* Blue */
        .button3
        {
            background-color: #f44336;
        }
        /* Red */
        .button4
        {
            background-color: #e7e7e7;
            color: black;
        }
        /* Gray */
        .button5
        {
            background-color: #555555;
        }
        /* Black */
        .text1
        {
            border: 2px solid rgb(173, 204, 204);
            height: 31px;
            width: 223px;
            box-shadow: 0 0 27px rgb(204, 204, 204) inset;
            transition: 500ms all ease;
            padding: 3px 3px 3px 3px;
            text-align: center;
        }
        
        .text1:hover, .text1:focus
        {
            width: 260px;
            transition: 500ms all ease;
            background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyBpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNSBXaW5kb3dzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjYxRDEzQTBCMzI0MzExRTFBNDYzRkQ4Qzc3RDdBOTg5IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjYxRDEzQTBDMzI0MzExRTFBNDYzRkQ4Qzc3RDdBOTg5Ij4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6NjFEMTNBMDkzMjQzMTFFMUE0NjNGRDhDNzdEN0E5ODkiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6NjFEMTNBMEEzMjQzMTFFMUE0NjNGRDhDNzdEN0E5ODkiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz52VTCHAAABFUlEQVR42uxV0Q2DIBBV4z8bdAM26AZ1Ap3AToAT6AR1Ap2gbuAGzKD/TkCP5NmQRpQaSH96yQsJHO/k7t0ZK6WikJZEgS14gHSeZxX6BQuhwerL3pw6ACMIQkuQHsgluDQnM2ugN3rgrK33ha3INdbqBHn1wWFVUU7ghMKxLgt8Oe46yVQ7lrgoD/JdwDf/tg+ueK4mGDfOR5zV8D3VaBwFy6CM1VrsCfjYG81BclrPHVZp7HcoLN8LshdgNMg50pDh7In+uSCIsKUpnqZJWfQ8EB4gOlLRnXBzVVFvpIA5yJTBV241aeLSLI622aRmiho8k3uYRcM6LlLksEWzMA/DjqP4+oNLW5G9Wfz/J/88wEuAAQA9yExzBAEQqwAAAABJRU5ErkJggg==) no-repeat right;
            background-size: 25px 25px;
            background-position: 96% 62%;
            padding: 3px 32px 3px 3px;
        }
    </style>
    <style>
        /* Style The Dropdown Button */
        .dropbtn
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            background-color: #4CAF50;
            color: white;
            padding: 16px;
            font-size: xx-small;
            cursor: pointer;
        }
        
        /* The container <div> - needed to position the dropdown content */
        .dropdown
        {
            position: relative;
            display: inline-block;
            top: 0px;
            left: 0px;
        }
        
        /* Dropdown Content (Hidden by Default) */
        .dropdown-content
        {
            display: none;
            position: absolute;
            background-color: #f9f9f9;
            min-width: 160px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
        }
        
        /* Links inside the dropdown */
        .dropdown-content a
        {
            color: black;
            padding: 12px 16px;
            text-decoration: none;
            display: block;
        }
        
        /* Change color of dropdown links on hover */
        .dropdown-content a:hover
        {
            background-color: #f1f1f1;
        }
        
        /* Show the dropdown menu on hover */
        .dropdown:hover .dropdown-content
        {
            display: block;
        }
        
        /* Change the background color of the dropdown button when the dropdown content is shown */
        .dropdown:hover .dropbtn
        {
            background-color: #3e8e41;
        }
    </style>
    <style>
        .text1
        {
            border: 2px solid rgb(173, 204, 204);
            height: 31px;
            width: 400px;
            box-shadow: 0 0 27px rgb(204, 204, 204) inset;
            transition: 500ms all ease;
            padding: 3px 3px 3px 3px;
            text-align: left;
        }
        
        .text1:hover, .text1:focus
        {
            width: 260px;
            transition: 500ms all ease;
            background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyBpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNSBXaW5kb3dzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjYxRDEzQTBCMzI0MzExRTFBNDYzRkQ4Qzc3RDdBOTg5IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjYxRDEzQTBDMzI0MzExRTFBNDYzRkQ4Qzc3RDdBOTg5Ij4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6NjFEMTNBMDkzMjQzMTFFMUE0NjNGRDhDNzdEN0E5ODkiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6NjFEMTNBMEEzMjQzMTFFMUE0NjNGRDhDNzdEN0E5ODkiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz52VTCHAAABFUlEQVR42uxV0Q2DIBBV4z8bdAM26AZ1Ap3AToAT6AR1Ap2gbuAGzKD/TkCP5NmQRpQaSH96yQsJHO/k7t0ZK6WikJZEgS14gHSeZxX6BQuhwerL3pw6ACMIQkuQHsgluDQnM2ugN3rgrK33ha3INdbqBHn1wWFVUU7ghMKxLgt8Oe46yVQ7lrgoD/JdwDf/tg+ueK4mGDfOR5zV8D3VaBwFy6CM1VrsCfjYG81BclrPHVZp7HcoLN8LshdgNMg50pDh7In+uSCIsKUpnqZJWfQ8EB4gOlLRnXBzVVFvpIA5yJTBV241aeLSLI622aRmiho8k3uYRcM6LlLksEWzMA/DjqP4+oNLW5G9Wfz/J/88wEuAAQA9yExzBAEQqwAAAABJRU5ErkJggg==) no-repeat right;
            background-size: 25px 25px;
            background-position: 96% 62%;
            padding: 3px 32px 3px 3px;
        }
        
        
        .text2
        {
            border: 2px solid rgb(173, 204, 204);
            height: 31px;
            width: 100%;
            box-shadow: 0 0 27px rgb(204, 204, 204) inset;
            transition: 500ms all ease;
            padding: 3px 3px 3px 3px;
            text-align: left;
        }
        
        .text2:hover, .text2:focus
        {
            width: 100%;
            transition: 500ms all ease;
            background-size: 25px 25px;
            background-position: 96% 62%;
            padding: 3px 32px 3px 3px;
        }
    </style>
    <script type="text/javascript">
        function CallPrint(strid) {
            var divToPrint = document.getElementById(strid);
            var newWin = window.open('', 'Print-Window', 'width=400,height=400,top=100,left=100');
            newWin.document.open();
            newWin.document.write('<html><body   onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
            newWin.document.close();
        }
        
    </script>
    <script type="text/javascript">
        function exportfn() {
            window.location = "exporttoxl.aspx";
        }

        //------------>Prevent Backspace<--------------------//
        $(document).unbind('keydown').bind('keydown', function (event) {
            var doPrevent = false;
            if (event.keyCode === 8) {
                var d = event.srcElement || event.target;
                if ((d.tagName.toUpperCase() === 'INPUT' && (d.type.toUpperCase() === 'TEXT' || d.type.toUpperCase() === 'PASSWORD'))
            || d.tagName.toUpperCase() === 'TEXTAREA') {
                    doPrevent = d.readOnly || d.disabled;
                } else {
                    doPrevent = true;
                }
            }

            if (doPrevent) {
                event.preventDefault();
            }
        });
    </script>
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="Server">
    <asp:toolkitscriptmanager id="ToolkitScriptManager1" enablepagemethods="true" runat="server" />
    <asp:updateprogress id="updateProgress1" runat="server">
        <progresstemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: #FFFFFF; opacity: 0.7;">
                <br />
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="thumbnails/loading.gif"
                    AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: absolute;
                    top: 35%; left: 40%;" />
            </div>
        </progresstemplate>
    </asp:updateprogress>
    <asp:updatepanel id="updPanel" runat="server">
        <contenttemplate>
            <section class="content-header">
                <h1>
                    FAT SNF Modification Report<small>Preview</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i>Reports</a></li>
                    <li><a href="#"> FAT SNF Modification Report</a></li>
                </ol>
            </section>
            <section class="content">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">
                            <i style="padding-right: 5px;" class="fa fa-cog"></i> FAT SNF Modification Details Report
                        </h3>
                    </div>
                    <div class="box-body">
                        <div align="center">
                            <table>
                                <tr>
                                 <td style="width: 350PX;">
                                       <label>Type</label>
                                        <asp:DropDownList ID="ddltype" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="select" Text="--Select--"></asp:ListItem>
                                        <asp:ListItem Value="Fat Snf Range Wise Quantity" Text="Fat Snf Range Wise Quantity"></asp:ListItem>
                                        <asp:ListItem Value="Fat Snf Modification" Text="Fat Snf Modification"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                <td style="width: 350PX;">
                                       <label>Plant Name</label>
                                        <asp:DropDownList ID="ddlplant" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <label>From Date</label>&nbsp;
                                        <asp:TextBox ID="dtp_FromDate" runat="server"   CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="enddate_CalendarExtender" runat="server" Enabled="True"
                                            TargetControlID="dtp_FromDate" Format="dd-MM-yyyy HH:mm">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td style="width:6px;"></td>
                                    <td>
                                        <label>Todate</label>&nbsp;
                                        <asp:TextBox ID="dtp_Todate" runat="server"   CssClass="form-control">
                                        </asp:TextBox>
                                        <asp:CalendarExtender ID="enddate_CalendarExtender2" runat="server" Enabled="True"
                                            TargetControlID="dtp_Todate" Format="dd-MM-yyyy HH:mm">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td style="width:6px;"></td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>&nbsp;
                                        <asp:Button ID="Button2" runat="server" Text="GENERATE" CssClass="button" OnClick="btn_Generate_Click" /><br />
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/exporttoxl.aspx">Export to XL</asp:HyperLink>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="hidepanel" runat="server" Visible='false'>
                                <div id="divPrint">
                                    <div style="width: 100%;">
                                        <div align="center">
                                            <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Font-Size="20px" ForeColor="#0252aa"
                                                Text=""></asp:Label>
                                            <br />
                                            <asp:Label ID="lblAddress" runat="server" Font-Bold="true" Font-Size="12px" ForeColor="#0252aa"
                                                Text=""></asp:Label>
                                            <br />
                                            <span style="font-size: 18px; font-weight: bold; color: #0252aa;"></span><br />
                                        </div>
                                        <table style="width: 80%">
                                            <tr>
                                                <td>
                                                    From Date
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFromDate" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                </td>
                                                <td>
                                                    To date:
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbltodate" runat="server" ForeColor="Red" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <div>
                                            <asp:GridView ID="grdReports" runat="server" CssClass="table table-striped table-bordered table-hover" OnDataBinding="grdReports_DataBinding">
                                                <HeaderStyle ForeColor="#660066" />
                                            </asp:GridView>
                                             </div>
                                        <br />
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 25%;">
                                                    <span style="font-weight: bold; font-size: 12px;">INCHARGE SIGNATURE</span>
                                                </td>
                                                <td style="width: 25%;">
                                                    <span style="font-weight: bold; font-size: 12px;">ACCOUNTS DEPARTMENT</span>
                                                </td>
                                                <td style="width: 25%;">
                                                    <span style="font-weight: bold; font-size: 12px;">AUTHORISED SIGNATURE</span>
                                                </td>
                                                <td style="width: 25%;">
                                                    <span style="font-weight: bold; font-size: 12px;">PREPARED BY</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <asp:Button ID="btnPrint" runat="Server" CssClass="btn btn-primary" OnClientClick="javascript:CallPrint('divPrint');"
                                    Text="Print" />
                            </asp:Panel>
                            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red" Font-Size="20px"></asp:Label>
                        </div>
                    </div>
                </div>
            </section>
        </contenttemplate>
    </asp:updatepanel>
</asp:content>
