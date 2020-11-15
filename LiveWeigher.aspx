<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LiveWeigher.aspx.cs" Inherits="LiveWeigher" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> 

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
 
       label {
 
           display:block;
 
           padding:10px;
 
           margin:10px 0px;
 
       }
 
       .Items {
 
       background:black;
 
       color:White;
 
       border: 1px #ff006e solid;
 
       }
 
      
       
 
        </style>

</head>
<body>
    <form id="form1" runat="server" style="background-color: #0000A0" height="1750px">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>

      <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>
 <div style="position: fixed; text-align: center; height:1%; width: 1%; top: 0; right: 0; left: 0; z-index: 9999999; background-color:Gray ; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="waiting.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 1px;position:fixed;top:45%;left:45%;" />
        </div>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
            <ContentTemplate>
    <asp:Panel ID="Panel1" runat="server" BackColor="#333399" BorderColor="#FF9933" 
        BorderStyle="Double" BorderWidth="5px">   
  <center>  
      <br />
      <asp:Label ID="comname" runat="server" Text="Sri Vyshnavi Dairy Specialities Pvt Limited" 
          style="color: #FFFFFF; font-size: xx-large; font-family: Andalus"></asp:Label>
<br />
      <asp:Label ID="Label4" runat="server" Text="Procurement Live DashBoard" 
          style="color: #FFFFFF; font-size: x-large; font-family: Andalus"></asp:Label>  
      <br />
      <asp:Label ID="Label3" runat="server" 
          style="color: #FFFFFF; font-size: x-large; font-family: Andalus"></asp:Label>  
      <asp:Label ID="Label6" runat="server" 
          style="color: #FFFFFF; font-size: x-large; font-family: Andalus"></asp:Label>
          <br />
          <asp:Label ID="TMilk" runat="server"  text="TotalMilk:"
          style="color: #FFFFFF; font-size: xx-large; font-family: Andalus"></asp:Label>
           <asp:Label ID="totalMilk" runat="server" 
          style="color: #FFFFFF; font-size: xx-large; font-family: Andalus"></asp:Label>
          <asp:Label ID="Fat" runat="server"  text="Afat:"
          style="color: #FFFFFF; font-size: xx-large; font-family: Andalus"></asp:Label>
           <asp:Label ID="Afat" runat="server" 
          style="color: #FFFFFF; font-size: xx-large; font-family: Andalus"></asp:Label>
          <asp:Label ID="Snf" runat="server"  text="Asnf:"
          style="color: #FFFFFF; font-size: xx-large; font-family: Andalus"></asp:Label>
           <asp:Label ID="Asnf" runat="server" 
          style="color: #FFFFFF; font-size: xx-large; font-family: Andalus"></asp:Label>
           </center>
   
<div align="right">
    <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="White" Font-Bold="true"  NavigateUrl="~/home.aspx">Home</asp:HyperLink></div>
<div align="center">
    <asp:DataList ID="datalstProfileCount" runat="server" RepeatColumns="1"  
        RepeatDirection="Horizontal"   RepeatLayout="Table" ShowHeader="true"  
        Width="100%" 
        onselectedindexchanged="datalstProfileCount_SelectedIndexChanged" 
        onitemdatabound="datalstProfileCount_ItemDataBound">
                        <HeaderTemplate>
                        <center>
                            <table border="0" cellpadding="0" cellspacing="0" bordercolor="#e1e1e0" style="border: solid 1px #e1e1e0;
                                font-family: Segoe UI; font-size: 12px; font-weight: bold; width:100%">
                                <tr style="font-size: 13px; background: #ffedc2; border-bottom: 1px solid #eba602;
                                    border-left: 1px solid #d6d6d6; font-weight: 600; font-size: 13px; padding: 10px 8px;
                                    color: #c82124;">
                                     <td style="width: 50px; color: #c84241; text-align: left; font-size:medium; padding: 10px">
                                        SNo:
                                    </td>
                                    <td style="width: 150px; color: #c84241; text-align: left; font-size:medium; padding: 10px">
                                        PlantName:
                                    </td>
                                    <td style="width: 100px; color: #c84241; text-align: left; font-size:medium; padding: 10px">
                                       Tot MilkKg:
                                    </td>
                                    <td style="width: 100px; color: #c84241; text-align: left; font-size:medium; padding: 10px">
                                        Avg Fat:
                                    </td>
                                    <td style="width: 100px; color: #c84241; text-align: left; font-size:medium; padding: 10px">
                                        Avg Snf:
                                    </td>
                                    <td style="width: 100px; color: #c84241; text-align: left; font-size:medium; padding: 10px">
                                       Tot Samples:
                                    </td>
                                     <td style="width: 100px; color: #c84241; text-align: left; font-size:medium; padding: 10px">
                                       Stime:
                                    </td>
                                     <td style="width: 100px; color: #c84241; text-align: left; font-size:medium; padding: 10px">
                                       Etime:
                                    </td>

                                </tr>
                            </table>
                            </center>
                        </HeaderTemplate>
                        <ItemTemplate>
                        <center>
                            <table border="0" cellpadding="0" cellspacing="0" bordercolor="#e1e1e0"  style="border: solid 1px #e1e1e0;
                                font-family: Segoe UI; font-size: 12px; font-weight: bold; width:100%; margin: -1px" height:"250px">
                                <tr style="font-size: 12px; vertical-align: middle; background-color:White; color: #9d9d9c;">
                                <td style="text-align: left; width: 50px; color: #333333; padding: 10px">
                                    <asp:Label ID="label2" runat="server" Text='<%#Eval("sno") %>'></asp:Label>
                                  
                                    </td>
                                    <td style="text-align: left; width: 150px; color: #333333; padding: 10px">
                                    <asp:Label ID="label144" runat="server"  ForeColor="HotPink" Font-Size="Medium" Text='<%#Eval("plantcode") %>'></asp:Label>-
                                    <asp:Label ID="Label5" runat="server" Font-Size="Small" Text='<%#Eval("Plant_Name") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: left; width: 100px; color: #333333; padding: 10px">
                                        <asp:Label ID="lblTotalCount" runat="server" ForeColor="Green"  Font-Size="Medium" Text='<%#Eval("milkkg") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: left; width: 100px; color: #333333; padding: 10px">
                                        <asp:Label ID="lblActiveMembersCount" runat="server" ForeColor="Brown" Font-Size="Medium" Text='<%#Eval("fat") %>'></asp:Label>
                                    </td>

                                    <td style="text-align: left; width: 100px; color: #333333; padding: 10px">
                                        <asp:Label ID="Label1" runat="server"  ForeColor="Red" Font-Size="Medium" Text='<%#Eval("snf") %>'></asp:Label>
                                    </td>

                                    <td style="text-align: left; width: 100px; color: #333333; padding: 10px">
                                        <asp:Label ID="Label3" runat="server"  ForeColor="Orange" Font-Size="Medium" Text='<%#Eval("sampleno") %>'></asp:Label>
                                    </td>

                                    <td style="text-align: left; width: 100px; color: #333333; padding: 10px">
                                        <asp:Label ID="Label7" runat="server"  ForeColor="Orange" Font-Size="Medium" Text='<%#Eval("Stime") %>'></asp:Label>
                                    </td>
                                    <td style="text-align: left; width: 100px; color: #333333; padding: 10px">
                                        <asp:Label ID="Label8" runat="server"  ForeColor="Orange" Font-Size="Medium" Text='<%#Eval("Etime") %>'></asp:Label>
                                    </td>

                                </tr>
                            </table>
                            </center>
                        </ItemTemplate>
<FooterTemplate>
<asp:Label ID="totmilkkg" runat="server" />
</FooterTemplate>

                   
                      
                    </asp:DataList>
</div>
                   
  
   <center>    <asp:Label ID="lblmsg" runat="server" 
           style="text-align: center; font-weight: 700; font-family: Andalus; color: #FF0000;" 
           Text="Label"></asp:Label> </center> 
  


 <asp:Timer ID="Timer1" runat="server" Interval="3600" ontick="Timer1_Tick">
        </asp:Timer>
   
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
   
    <br />
    <br />
    </asp:Panel>
   </ContentTemplate>
   <%-- <Triggers>   
    
 <asp:PostBackTrigger ControlID ="btn_Save1" />    
    </Triggers>--%>
        </asp:UpdatePanel>
</form>
</body>
</html>
    
   
