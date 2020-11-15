<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LiveDumping1.aspx.cs" Inherits="LiveDumping1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <style type="text/css">
        label
        {
            display: block;
            padding: 10px;
            margin: 10px 0px;
        }
        
        .Items
        {
            background: black;
            color: White;
            border: 1px #ff006e solid;
        }
         .bcolor
        {
            background: #e0e0e0;
           
        }
         .dheight
        {
            height:590px;
        }
        .linkNoUnderline
        {
         text-decoration: none;
        }
    </style>
</head>
<body class="bcolor">
    <form id="form1" runat="server">
    <div class="dheight" >
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
        </asp:ToolkitScriptManager>
        <asp:UpdateProgress ID="UpdateProgress" runat="server">
            <ProgressTemplate>
                <div style="position: fixed; text-align: center; height: 1px; width: 1px; top: 0; right: 0;
                    left: 0; z-index: 9999999; background-color: Gray; opacity: 0.7;">
                    <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..."
                        ToolTip="Loading ..." Style="padding: 1px; position: fixed; top: 45%; left: 45%;" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div align="center">
                 <div style="width: 13%; float: left;">
                                    <img src="Image/VLogo.png" alt="Vyshnavi" width="100px" height="52px" />
                                </div>
 <div style="width: 70%; float: left;">
               <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Font-Size="22px" ForeColor="#ff1493" Text="SRI VYSHNAVI DAIRY SPECIALITIES PVT LIMITED"></asp:Label>
               <br />
                <span style="font-size: 20px; font-weight: bold; color: #0252aa;">Live Procurement DashBoard</span>
                                <asp:TextBox ID="txt_fromdate" runat="server" Visible="false" 
                   Height="16px" Width="16px"   
                    ></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txt_fromdate"
                    PopupButtonID="txt_fromdate" Format="dd/MM/yyyy" 
        PopupPosition="TopRight">
                </asp:CalendarExtender>   
                <br />
               <span style="font-size: 20px; font-weight: bold; color: Red;"> <asp:Label ID="Label3" runat="server"></asp:Label> </span> 
               <span style="font-size: 20px; font-weight: bold; color: Red;"> <asp:Label ID="Label6" runat="server"></asp:Label></span>
                <br />
               
                <table border="1" width="90%" align="center" >
                <tr>
                 <th> <span style="font-size: 18px; font-weight: bold; "> MilkType</span></th>
                <th> <span style="font-size: 18px; font-weight: bold; ">TotalMilk</span></th>
                <th> <span style="font-size: 18px; font-weight: bold; ">AFat</span></th>
                <th> <span style="font-size: 18px; font-weight: bold; ">ASnf</span></th>
                </tr>
                <tr>
                <td width="30%" align="center">
                 <span style="font-size: 18px; font-weight: bold; color: Red;">BUFF</span>
                </td>
                <td width="30%" align="center">
                <span style="font-size: 18px; font-weight: bold; color: #0252aa;"><asp:Label ID="totalBMilk" runat="server" ></asp:Label></span>
                </td>
               <td width="30%" align="center">
               <span style="font-size: 18px; font-weight: bold; color: #0252aa;"><asp:Label ID="ABfat" runat="server" ></asp:Label></span>
                </td>
               <td width="30%" align="center">
               <span style="font-size: 18px; font-weight: bold; color: #0252aa;"><asp:Label ID="ABsnf" runat="server" ></asp:Label></span>
                </td>
                </tr>
                </table>           
           
           
          
         
                 </div>
                  <div style="width: 17%; float: left;">
                  <table width="width: 100%">
            <tr "width: 100%" valign="top">
            <td >             
        <asp:Literal ID="lt" runat="server"></asp:Literal>
   
    <div id="chart_div">        
    </div> 
    <span style="font-size: 20px; font-weight: bold; color: Red;">
        <asp:Label ID="Lbl_YMilk" runat="server" Visible="false"></asp:Label>
        <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="Blue" Font-Bold="true"  NavigateUrl="~/home.aspx">Home</asp:HyperLink>
        </span>
            </td>
            </tr>
            </table>

                  </div>
                <div><span style="font-size: 18px; font-weight: bold; color: #0252aa;">
                    <asp:Label ID="totalcMilk" runat="server"></asp:Label>
                    <asp:Label ID="Acfat" runat="server"></asp:Label>
                    <asp:Label ID="Acsnf" runat="server"></asp:Label>
                    </span></div>
                  <table width="100%">
                  <tr class="bcolor">
                  <td align="center" valign="top" width="100%">
                  <asp:GridView ID="grdLivepro" runat="server" ForeColor="White" Width="100%" CssClass="gridcls"
                                        GridLines="Both" Font-Bold="true" 
                          onrowdatabound="grdLivepro_RowDataBound" AutoGenerateColumns="false">
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="true" ForeColor="White" Font-Italic="False"
                                             Font-Size="Large" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
                                        <AlternatingRowStyle HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                         <Columns>
       <%-- <asp:TemplateField HeaderText = "Sno" ItemStyle-Width="30">
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("sno", "~/LivePlantRoutewisedetails.aspx?Id={0}") %>'
                    Text='<%# Eval("sno") %>' />
            </ItemTemplate>
        </asp:TemplateField>--%>       
        <asp:BoundField DataField="sno" HeaderText="Sno" ItemStyle-Width="20" />
        <asp:BoundField DataField="TRRid" HeaderText="Rrid" ItemStyle-Width="20" />
         <%--<asp:TemplateField HeaderText = "PlantName" ItemStyle-Width="150">     
        <ItemTemplate>
                <asp:HyperLink CssClass="linkNoUnderline" ForeColor="Brown" ID="HyperLink3" runat="server" NavigateUrl='<%# string.Format("~/DashBoard.aspx?Id={0}&Name={1}&Country={2}", 
                    HttpUtility.UrlEncode(Eval("sno").ToString()), HttpUtility.UrlEncode(Eval("TRRid").ToString()), HttpUtility.UrlEncode(Eval("Pname").ToString())) %>'
                    Text='<%# Eval("Pname") %>' />
            </ItemTemplate>       
        </asp:TemplateField>--%>
        <%--  <asp:TemplateField HeaderText = "PlantName" ItemStyle-Width="30">
            <ItemTemplate>
                <asp:HyperLink ID="HyperLink2" runat="server"  CssClass="linkNoUnderline" ForeColor="Brown" NavigateUrl='<%# Eval("plant_code", "~/LivePlantRoutewisedetails.aspx?Id={0}") %>'
                    Text='<%# Eval("Pname") %>' />
            </ItemTemplate>
        </asp:TemplateField> --%>
        <%--<asp:TemplateField HeaderText="PlantName" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lbtnBOLNo" runat="server"  ForeColor="Brown" Text='<%# Eval("sno") %>' OnClick="lbtnBOLNo_Click"></asp:HyperLink>
                                    </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:TemplateField HeaderText="PlantName">
        <ItemTemplate>
             <asp:LinkButton runat="server" ID="lnkView"  CssClass="linkNoUnderline" ForeColor="Brown" Text='<%# Eval("Pname") %>'  OnClick="lnkView_Click"></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>
       <%-- <asp:BoundField DataField="Pname" HeaderText="PlantName" ItemStyle-Width="150" />--%>
        <asp:BoundField DataField="TMkg" HeaderText="Milkkg" />
        <asp:BoundField DataField="TwSampleNo" HeaderText="WSampNo" />
        <asp:BoundField DataField="TwStime" HeaderText="WStime" />
        <asp:BoundField DataField="TwEtime" HeaderText="WEtime" />
        <asp:BoundField DataField="TASampleNo" HeaderText="ASampNo" />
        <asp:BoundField DataField="TAStime" HeaderText="AStime" />
        <asp:BoundField DataField="TAEtime" HeaderText="AEtime" />
        <asp:BoundField DataField="Tfat" HeaderText="Fat" />
        <asp:BoundField DataField="Tsnf" HeaderText="Snf" />
        <asp:BoundField DataField="Tconn" HeaderText="Status" />
        <asp:BoundField DataField="plant_code" HeaderText="" />
        
    </Columns>

                                    </asp:GridView>
                  </td>

                  <td align="center" valign="top" width="1%"  >
                   <asp:Label ID="lblmsg" runat="server" Text="K" Visible="false" ForeColor="Fuchsia"></asp:Label> </center> 
                  </td>                
                     
                          <td align="right" valign="top" width="1%">
                              <asp:GridView ID="GridView1" runat="server" CssClass="gridcls" Font-Bold="true" Visible="false" 
                                  ForeColor="White" GridLines="Both" Width="100%" 
                                  onrowdatabound="GridView1_RowDataBound">
                                  <EditRowStyle BackColor="#999999" />
                                  <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
                                  <HeaderStyle BackColor="#e0e0e0" Font-Bold="true" Font-Italic="False" 
                                      Font-Size="Large" ForeColor="Black"/>
                                  <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                  <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
                                  <AlternatingRowStyle HorizontalAlign="Center" />
                                  <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                              </asp:GridView>
                          </td>
                           <td align="center" valign="top" width="9%"  >
                   <asp:GridView ID="GridView2" runat="server" CssClass="gridcls" Font-Bold="true"  Visible="false"
                                  ForeColor="White" GridLines="Both" Width="100%" 
                                   onrowdatabound="GridView2_RowDataBound">
                                  <EditRowStyle BackColor="#999999" />
                                  <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
                                  <HeaderStyle BackColor="#e0e0e0" Font-Bold="true" Font-Italic="False" 
                                       Font-Size="Large" ForeColor="Black" />
                                  <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                  <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
                                  <AlternatingRowStyle HorizontalAlign="Center" />
                                  <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                              </asp:GridView> 
                  </td>
                      </tr>
                  
                  </table>                          

                            <asp:Timer ID="Timer1" runat="server" Interval="7200" ontick="Timer1_Tick">
                            </asp:Timer>
                </div>
                <table align="center" width="100px">
                    <tr>
                        <td valign="top">
                            <asp:GridView ID="Gv_PlantRouteData" runat="server" ForeColor="White" Width="100%"
                                CssClass="gridcls" GridLines="Both" Font-Bold="true" 
                                AutoGenerateColumns="false" onrowdatabound="Gv_PlantRouteData_RowDataBound">
                                <EditRowStyle BackColor="#999999" />
                                <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" Font-Size="Large" />   
                                 <HeaderStyle BackColor="#006699" Font-Bold="true" ForeColor="White" Font-Italic="False" />                   
                                 <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
                                <Columns> 
                                  <%--<asp:TemplateField HeaderText="RouteName">
       <ItemTemplate>
              <asp:LinkButton runat="server" ID="lnkView1"  CssClass="linkNoUnderline" ForeColor="Brown" Text='<%# Eval("RouteName") %>'  OnClick="lnkView1_Click"></asp:LinkButton>
         </ItemTemplate>
       </asp:TemplateField>          --%>    

       <asp:TemplateField HeaderText = "RouteName" ItemStyle-Width="150">     
        <ItemTemplate>
                <asp:HyperLink CssClass="linkNoUnderline" ForeColor="Brown" runat="server"   Text='<%# Eval("RouteName") %>' /> 
            </ItemTemplate>       
        </asp:TemplateField>                     
                                   
                                    <asp:BoundField DataField="Milkkg" HeaderText="Milkkg" ItemStyle-Width="20" />                                  
                                    <asp:BoundField DataField="Fat" HeaderText="Fat" />
                                    <asp:BoundField DataField="Snf" HeaderText="Snf" /> 
                                                                     
                                                                      
                                </Columns>
                            </asp:GridView>                          
                        </td>
                        
                    </tr>
                    <tr>
                    <td valign="top" align="right">
              <%--       <asp:GridView ID="grdweight" runat="server" ForeColor="White"  CssClass="gridcls"
        GridLines="Both" Font-Bold="true"  AutoGenerateColumns="false">
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
        <HeaderStyle BackColor="#FF6600" Font-Bold="true" ForeColor="White" Font-Italic="False"
            Font-Size="Large" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
        <AlternatingRowStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="false" ForeColor="#333333" />
        <Columns>
            <asp:BoundField DataField="Wtime" HeaderText="WTime" ItemStyle-Width="20" />
            <asp:BoundField DataField="WSampleNo" HeaderText="WsampleNo" ItemStyle-Width="40" />
           
        </Columns>
    </asp:GridView>--%>
                         
                    </td>
                    <td valign="top" align="left">
               <%--     <asp:GridView ID="grdAnalyzer" runat="server" ForeColor="White"  CssClass="gridcls"
        GridLines="Both" Font-Bold="true"  AutoGenerateColumns="false">
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
        <HeaderStyle BackColor="#6A8347" Font-Bold="false" ForeColor="White" Font-Italic="False"
            Font-Size="Large" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
        <AlternatingRowStyle HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <Columns>
            <asp:BoundField DataField="Atime" HeaderText="ATime" ItemStyle-Width="20" />
            <asp:BoundField DataField="ASampleNo" HeaderText="AsampleNo" ItemStyle-Width="40" />
           
        </Columns>
    </asp:GridView>--%>
                    </td>
                    </tr>
                </table>
                
                
     
            </ContentTemplate>
           
             <Triggers>   
    
 <%--<asp:PostBackTrigger ControlID ="chart_div" />   --%> 
    </Triggers>
    
        </asp:UpdatePanel>
    </div>
     
  
  
    </form>
    
</body>
</html>


<%--   <div align="center">
        <span style="font-size: 15px; font-weight: bold; color: #0252aa;"><strong>Copyright &copy; 2014-2015 <a href="http://vyshnavifoods.com">
    Sri Vyshnavi Dairy Spl Pvt Ltd</a>.</strong> All rights reserved.</span>
     
      </div>--%>
