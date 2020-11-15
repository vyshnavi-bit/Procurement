<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashBoard1.aspx.cs" Inherits="DashBoard1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>Procurement | Dashboard</title>
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <!-- Bootstrap 3.3.6 -->
  <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css">
  <!-- Font Awesome -->
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css">
  <!-- Ionicons -->
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css">
  <!-- jvectormap -->
  <link rel="stylesheet" href="plugins/jvectormap/jquery-jvectormap-1.2.2.css">
  <!-- Theme style -->
  <link rel="stylesheet" href="dist/css/AdminLTE.min.css">
  <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->
  <link rel="stylesheet" href="dist/css/skins/_all-skins.min.css">

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->

 


</head>
<style>


  ul li { display:inline; font-size:3em; text-align:center; font-family:'BebasNeueRegular', Arial, Helvetica, sans-serif; text-shadow:0 0 5px #00c6ff; }

    .style3
    {
        width: 65px;
        height: 52px;
        float: left;
    }
    
     .style31
    {
        width: 65px;
        height: 52px;
        float: right;
    }
    
  

    .style32
    {
        height: 40px;
    }
    
  

    </style>

  <script type="text/javascript" src="https://www.google.com/jsapi"></script>

        

          <script type="text/javascript" src="https://www.google.com/jsapi"></script> 
        
        <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
   <script type="text/javascript">
       google.charts.load('current', { packages: ['corechart'] });     
   </script>
  <div style="width: 110%">
  <CENTER>
<body bgcolor="#6699ff">
<center>
    <form id="form1" runat="server" 
    style="background-color: #AEE3E3; height: 999px;" width=100%>
   
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <asp:Timer ID="Timer1" runat="server" Interval="60000" OnTick="Timer1_Tick">
    </asp:Timer>
  
    <asp:panel ID="Panel1" runat="server" Width="100%">
    <table width=100%>
    <tr>
    <td align="center">



        <asp:Label ID="Label21" runat="server" style="font-size: xx-large; font-family: Aparajita; text-decoration: underline; font-weight: 700; color: #000066;" 
            Text="Procurement Daily Collection Data DashBoard:"></asp:Label>



        <asp:Label ID="Label22" runat="server" style="font-size: xx-large; font-family: Aparajita; text-decoration: underline; font-weight: 700; color: #000066;" ></asp:Label>



   </td>
        <td align="center">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/home.aspx">Home</asp:HyperLink>
            <br />
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/LiveDumping.aspx">Live Procurement</asp:HyperLink>
        </td>
    </tr>
    </table>
    <table width=100%>
     
     
        <tr valign="top">
            <td align="center" valign=top>
                      <table width="90%">
                    <tr>
                       <td width=10%>
                           <img class="style3" 
                    src="cow.png" />
                        </td>
                        <td width=20%>
                            <div class="small-box bg-aqua">
                                <div class="inner">
                                    <asp:Label ID="Label6" runat="server"   font-size="18pt"    font-family="Batang"    Text="Milkkg"></asp:Label>
                                    <br />
                                    <asp:Label ID="ckg" runat="server"  font-size="18pt"    Font-Bold="true"   font-family="Batang"  ></asp:Label>
                                       
                                </div>
                                <div class="icon">
                                    <i class="ion ion-bag"></i>
                                </div>
                                <%--   <a class="small-box-footer" href="#">More info
                                <i class="fa fa-arrow-circle-right"></i></a>--%>
                            </div>
                        </td>
                         <td width=20%>
                            <div class="small-box bg-yellow">
                                <div class="inner">
                                    <asp:Label ID="Label8" runat="server" 
                                       font-size="18pt"    font-family="Batang" 
                                        Text="Avg Fat"></asp:Label>
                                    <br />
                                    <asp:Label ID="cfat" runat="server" 
                                       font-size="18pt"    Font-Bold="true"   font-family="Batang" ></asp:Label>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-person-add"></i>
                                </div>
                                <%--  <a class="small-box-footer" href="#">More info
                                <i class="fa fa-arrow-circle-right"></i></a>--%>
                            </div>
                        </td>
                         <td width=20%>
                            <div class="small-box bg-red">
                                <div class="inner">
                                    <asp:Label ID="Label10" runat="server" 
                                   font-size="18pt"    font-family="Batang" 
                                        Text="Avg Snf"></asp:Label>
                                    <br />
                                    <asp:Label ID="csnf" runat="server" 
                                        font-size="18pt"    Font-Bold="true"   font-family="Batang"></asp:Label>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-pie-graph"></i>
                                </div>
                                <%-- <a class="small-box-footer" href="#">More info
                                <i class="fa fa-arrow-circle-right"></i></a>--%>
                            </div>
                        </td>
                        <td width=20%>
                            <div class="small-box bg-green">
                                <div class="inner">
                                    <asp:Label ID="Label12" runat="server" 
                                    font-size="18pt"    font-family="Batang" 
                                        Text="Milk ltr"></asp:Label>
                                    <br />
                                    <asp:Label ID="cltr" runat="server" 
                                       font-size="18pt"    Font-Bold="true"   font-family="Batang" ></asp:Label>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-pie-graph"></i>
                                </div>
                                <%-- <a class="small-box-footer" href="#">More info
                                <i class="fa fa-arrow-circle-right"></i></a>--%>
                            </div>
                        </td>
                        <td width=10%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
     
     
    </table>
    <table width=100%>
     
     
        <tr>
            <td align="center">
               
                <table width="90%">
                    <tr>
                        <td width=10%>
                            <img class="style3" 
                    src="buff1.jpg" />
                        </td>
                        <td width=20%>
                            <div class="small-box bg-aqua">
                                <div class="inner">
                                    <asp:Label ID="Label14" runat="server" 
                                       font-size="18pt"    font-family="Batang" 
                                        Text="Milkkg"></asp:Label>
                                    <br />
                                    <asp:Label ID="bkg" runat="server" 
                                       font-size="18pt"    Font-Bold="true"   font-family="Batang"> </asp:Label>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-bag"></i>
                                </div>
                                <%--   <a class="small-box-footer" href="#">More info
                                <i class="fa fa-arrow-circle-right"></i></a>--%>
                            </div>
                        </td>
                         <td width=20%>
                            <div class="small-box bg-yellow">
                                <div class="inner">
                                    <asp:Label ID="Label16" runat="server" 
                                      font-size="18pt"    font-family="Batang" 
                                        Text="Avg Fat"></asp:Label>
                                    <br />
                                    <asp:Label ID="bfat" runat="server" 
                                        font-size="18pt"    Font-Bold="true"   font-family="Batang"></asp:Label>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-person-add"></i>
                                </div>
                                <%--   <a class="small-box-footer" href="#">More info
                                <i class="fa fa-arrow-circle-right"></i></a>--%>
                            </div>
                        </td>
                         <td width=20%>
                            <div class="small-box bg-red">
                                <div class="inner">
                                    <asp:Label ID="Label18" runat="server" 
                                        font-size="18pt"    font-family="Batang"  
                                        Text="Avg Snf"></asp:Label>
                                    <br />
                                    <asp:Label ID="bsnf" runat="server" 
                                        font-size="18pt"    Font-Bold="true"   font-family="Batang"></asp:Label>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-pie-graph"></i>
                                </div>
                                <%--    <a class="small-box-footer" href="#">More info
                                <i class="fa fa-arrow-circle-right"></i></a>--%>
                            </div>
                        </td>
                       <td width=20%>
                            <div class="small-box bg-green">
                                <div class="inner">
                                    <asp:Label ID="Label20" runat="server" 
                                       font-size="18pt"    font-family="Batang" 
                                        Text="Milk ltr"></asp:Label>
                                    <br />
                                    <asp:Label ID="bltr" runat="server" 
                                        font-size="18pt"    Font-Bold="true"   font-family="Batang"></asp:Label>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-pie-graph"></i>
                                </div>
                                <%--   <a class="small-box-footer" href="#">More info
                                <i class="fa fa-arrow-circle-right"></i></a>--%>
                            </div>
                        </td>
                        <td width=10%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
     
     
    </table>

        <table width=100%>
     
     
        <tr>
            <td align="center">
               
                <table width="90%">
                    <tr>
                        <td width=10%>
                            &nbsp;<img class="style3" 
                    src="cow.png" /></td>
                        <td width=20%>
                            <div class="small-box bg-aqua">
                                <div class="inner">
                                    <asp:Label ID="Label1" runat="server" 
                                        font-size="18pt"    font-family="Batang" 
                                        Text="Milkkg"></asp:Label>
                                    <br />
                                    <asp:Label ID="mkg" runat="server" 
                                        font-size="18pt"    Font-Bold="true"   font-family="Batang"></asp:Label>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-bag"></i>
                                </div>
                                <%--   <a class="small-box-footer" href="#">More info
                                <i class="fa fa-arrow-circle-right"></i></a>--%>
                            </div>
                        </td>
                         <td width=20%>
                            <div class="small-box bg-yellow">
                                <div class="inner">
                                    <asp:Label ID="Label3" runat="server" 
                                      font-size="18pt"    font-family="Batang" 
                                        Text="Avg Fat"></asp:Label>
                                    <br />
                                    <asp:Label ID="fat" runat="server" 
                                        font-size="18pt"    Font-Bold="true"   font-family="Batang" ></asp:Label>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-person-add"></i>
                                </div>
                                <%--   <a class="small-box-footer" href="#">More info
                                <i class="fa fa-arrow-circle-right"></i></a>--%>
                            </div>
                        </td>
                         <td width=20%>
                            <div class="small-box bg-red">
                                <div class="inner">
                                    <asp:Label ID="Label5" runat="server" 
                                       font-size="18pt"    font-family="Batang" 
                                        Text="Avg Snf"></asp:Label>
                                    <br />
                                    <asp:Label ID="snf" runat="server" 
                                       font-size="18pt"    Font-Bold="true"   font-family="Batang" ></asp:Label>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-pie-graph"></i>
                                </div>
                                <%--    <a class="small-box-footer" href="#">More info
                                <i class="fa fa-arrow-circle-right"></i></a>--%>
                            </div>
                        </td>
                       <td width=20%>
                            <div class="small-box bg-green">
                                <div class="inner">
                                    <asp:Label ID="Label9" runat="server" 
                                        font-size="18pt"    font-family="Batang" 
                                        Text="Milk ltr"></asp:Label>
                                    <br />
                                    <asp:Label ID="ltr" runat="server" 
                                        font-size="18pt"    Font-Bold="true"   font-family="Batang" ></asp:Label>
                                </div>
                                <div class="icon">
                                    <i class="ion ion-pie-graph"></i>
                                </div>
                                <%--   <a class="small-box-footer" href="#">More info
                                <i class="fa fa-arrow-circle-right"></i></a>--%>
                            </div>
                        </td>
                        <td width=10%>
                            <img class="style31" 
                    src="buff1.jpg" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
     
     
    </table>





        <tr align="center">
                       <td colspan="2">
                           <asp:UpdatePanel ID="Updatepanel1" runat="server">
                               <ContentTemplate>
                               <table  width=90%>
                                   <caption>
                                       <tr>
                                           <td align=center class="style32" colspan="2">
                                               <div ID="example" class="k-content absConf">
                                                   <div class="chart-wrapper" style="margin: auto;">
                                                       <div ID="chart" style="padding-left:0px;">
                                                       </div>
                                                   </div>
                                               </div>
                                                <div ID="Div1" class="k-content absConf">
                                                    <div class="chart-wrapper" style="margin: auto;">
                                                        <div ID="chart1" style="padding-left:px;">
                                                        </div>
                                                    </div>
                                               </div>
                                               <%--  <div ID="Div1" class="k-content absConf">
                                                   <div class="chart-wrapper" style="margin: auto;">
                                                       <div ID="chart1" style="padding-left:px;">
                                                           <div ID="visualization1" style="padding-left:px; text-align: left;" 
                                                               style="width: 40%; height: 92px;">
                                                           </div>
                                                       </div>
                                                   </div>
                                               </div>--%>
                                                </td>
                                       </tr>
                                   </caption>
                                   <tr>
                                       <td align="left">
                                           <div ID="visualization" 
                                               style="padding-left:px; text-align: left; font-size: small;" 
                                               style="width: 40%; height: 200px;">
                                           </div>
                                       </td>
                                       <td align="left">
                                           <div ID="visualization1" 
                                               style="padding-left:px; text-align: left; font-size: small;" 
                                               style="width: 40%; height:200px;">
                                           </div>
                                       </td>
                                   </tr>
                               </table>
                                  
                               </ContentTemplate>
                           </asp:UpdatePanel>
                       </td>
                   </tr>
                   <tr align="center">
                       <td colspan="2">
                     
                            <table>
                            <tr align="center">
                            <td>
                           <div>
                           <center style="font-size: small">
                               <asp:Literal ID="lt" runat="server"></asp:Literal>
                               <asp:Literal ID="lt1" runat="server"></asp:Literal>
                               </center>
                           </div>
                           <div ID="divLineChart">
                           </div>
                           </td>
                           </tr>
                           </table> 
                           <script type="text/javascript">

                               // Global variable to hold data
                               google.load('visualization', '1', { packages: ['corechart'] });
                               google.load('visualization1', '1', { packages: ['corechart'] });
</script>

                           &nbsp;</td>
                   </tr>

        <table class="nav-justified">
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>

      </asp:Panel>
   
<%--
       </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>--%>
     </form>
     </center>
</body>
  <CENTER>
  </div>
</html>
