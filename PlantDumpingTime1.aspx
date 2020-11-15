<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PlantDumpingTime1.aspx.cs" Inherits="PlantDumpingTime1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
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


      <style type="text/css">
        .form-style-9{
    max-width: 450px;
    background: #FAFAFA;
    padding: 20px;
    margin:20px auto;
    box-shadow: 1px 1px 25px rgba(0, 0, 0, 0.35);
    border-radius: 10px;
 
         height:197px;
         width: 541px;
     }
.form-style-9 ul{
    padding:0;
    margin:0;
    list-style:none;
}
.form-style-9 ul li{
    display: block;
    margin-bottom: 10px;
    min-height: 35px;
         height: 37px;
         width: 81px;
         margin-right: 0px;
            text-align: left;
        }
.form-style-9 ul li  .field-style{
    box-sizing: border-box;
    -webkit-box-sizing: border-box;
    -moz-box-sizing: border-box;
    padding: 8px;
    outline: none;
    border: 1px solid #B0CFE0;
    -webkit-transition: all 0.30s ease-in-out;
    -moz-transition: all 0.30s ease-in-out;
    -ms-transition: all 0.30s ease-in-out;
    -o-transition: all 0.30s ease-in-out;

}.form-style-9 ul li  .field-style:focus{
    box-shadow: 0 0 5px #B0CFE0;
    border:1px solid #B0CFE0;
}
.form-style-9 ul li .field-split{
    width: 49%;
}
.form-style-9 ul li .field-full{
    width:70%;
}
.form-style-9 ul li input.align-left{
    float:left;
}
.form-style-9 ul li input.align-right{
    float:right;
}
.form-style-9 ul li textarea{
    width: 80%;
    height: 100px;
}
.form-style-9 ul li input[type="button"],
.form-style-9 ul li input[type="submit"] {
    -moz-box-shadow: inset 0px 1px 0px 0px #3985B1;
    -webkit-box-shadow: inset 0px 1px 0px 0px #3985B1;
    box-shadow: inset 0px 1px 0px 0px #3985B1;
    background-color: #216288;
    border: 1px solid #17445E;
    display: inline-block;
    cursor: pointer;
    color: #FFFFFF;
    padding: 8px 18px;
    text-decoration: none;
    font: 12px Arial, Helvetica, sans-serif;
            text-align: right;
        }
.form-style-9 ul li input[type="button"]:hover,
.form-style-9 ul li input[type="submit"]:hover {
    background: linear-gradient(to bottom, #2D77A2 5%, #337DA8 100%);
    background-color: #28739E;
}
</style>
<style type="text/css">
.form-style-3{
    max-width: 450px;
    font-family: "Lucida Sans Unicode", "Lucida Grande", sans-serif;
}
.form-style-3 label{
    display:block;
  <%--  margin-bottom: 10px;--%>;
        height: 34px;
        text-align: center;
    }
.form-style-3 label > span{
    float: left;
    width: 100px;
    color: DARK;
    font-weight: bold;
    font-size: 13px;
    text-shadow: 1px 1px 1px #fff;
        text-align: right;
    }
.form-style-3 fieldset{
    border-radius: 10px;
    -webkit-border-radius: 10px;
    -moz-border-radius: 10px;
    margin: 0px 0px 10px 0px;
    border: 1px solid #FFD2D2;
    padding: 20px;
    background: #FFF4F4;
    box-shadow: inset 0px 0px 15px #FFE5E5;
    -moz-box-shadow: inset 0px 0px 15px #FFE5E5;
    -webkit-box-shadow: inset 0px 0px 15px #FFE5E5;
}
.form-style-3 fieldset legend{
    color: DARK;
    border-top: 1px solid #FFD2D2;
    border-left: 1px solid #FFD2D2;
    border-right: 1px solid #FFD2D2;
    border-radius: 5px 5px 0px 0px;
    -webkit-border-radius: 5px 5px 0px 0px;
    -moz-border-radius: 5px 5px 0px 0px;
    background: #FFF4F4;
    padding: 0px 8px 3px 8px;
    box-shadow: -0px -1px 2px #F1F1F1;
    -moz-box-shadow:-0px -1px 2px #F1F1F1;
    -webkit-box-shadow:-0px -1px 2px #F1F1F1;
    font-weight: normal;
    font-size: 12px;
}
.form-style-3 textarea{
    width:250px;
    height:100px;
}
.form-style-3 input[type=text],
.form-style-3 input[type=date],
.form-style-3 input[type=datetime],
.form-style-3 input[type=number],
.form-style-3 input[type=search],
.form-style-3 input[type=time],
.form-style-3 input[type=url],
.form-style-3 input[type=email],
.form-style-3 select, 
.form-style-3 textarea{
    border-radius: 3px;
    -webkit-border-radius: 3px;
    -moz-border-radius: 3px;
    border: 1px solid #FFC2DC;
    outline: none;
    color: DARK;
    padding: 5px 8px 5px 8px;
    box-shadow: inset 1px 1px 4px #FFD5E7;
    -moz-box-shadow: inset 1px 1px 4px #FFD5E7;
    -webkit-box-shadow: inset 1px 1px 4px #FFD5E7;
    background: #FFEFF6;
    width:50%;
}
.form-style-3  input[type=submit],
.form-style-3  input[type=button]{
    background: #EB3B88;
    border: 1px solid #C94A81;
    padding: 5px 15px 5px 15px;
    color: #FFCBE2;
    box-shadow: inset -1px -1px 3px #FF62A7;
    -moz-box-shadow: inset -1px -1px 3px #FF62A7;
    -webkit-box-shadow: inset -1px -1px 3px #FF62A7;
    border-radius: 3px;
    border-radius: 3px;
    -webkit-border-radius: 3px;
    -moz-border-radius: 3px;    
    font-weight: bold;
}
.required{
    color:red;
    font-weight:normal;
}
    .style3
    {
        font-size: small;
    }
    .style4
    {
        height: 64px;
    }
</style>



<style type="text/css">
.body
{
    margin: 0;
    padding: 0;
    font-family: Arial;
}
.modal
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;
}
.center
{
    z-index: 1000;
    margin: 300px auto;
    padding: 10px;
    width: 130px;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center img
{
    height: 128px;
    width: 128px;
}
</style>


    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



 <form id="form1" >
    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>
<%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
<ProgressTemplate>
    <div class="modal">
        <div class="center">
       
            <asp:Image ID="Image1" ImageUrl="waiting.gif" AlternateText="Processing" runat="server" />
        </div>
    </div>
</ProgressTemplate>
</asp:UpdateProgress>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    <div align="center">
--%>






   <div class="form-style-9" style="height: 324px">
<div class="form-style-3" style="height: auto">
<table width=100% style="height: 93px">

<tr  ALIGN=LEFT>
<td WIDTH="98%" style="height: auto">

<fieldset style="height: auto"  ><legend>Plant  Summary Report </legend>
    <table class="style1">
         <tr width="100%">
                <td align="center"  width: 15%" style="text-align: right" class="style4">
                    <span>Date </span>
                    <asp:TextBox ID="txt_FromDate" runat="server"   Width="75px"></asp:TextBox>
                    <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" runat="server" 
                        Format="dd/MM/yyyy" PopupButtonID="txt_FromDate" PopupPosition="TopRight" 
                        TargetControlID="txt_FromDate">
                    </asp:CalendarExtender>
                </td>
                <td align="center"  width: 25%" class="style4" >

    
                <asp:RadioButtonList ID="rdolist" runat="server" 
                    RepeatDirection="Horizontal" 
                        onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
                        Width="78px" style="text-align: left">
                    <asp:ListItem Value="1">AM</asp:ListItem>
                    <asp:ListItem Value="2">PM</asp:ListItem>
                </asp:RadioButtonList>

    
                    </td>
                <td align="center"  width: 25%" class="style4">

    
                    <asp:CheckBox ID="rtoroute" runat="server" AutoPostBack="True" 
                        oncheckedchanged="rtoroute_CheckedChanged" Text="Route" />

    
                </td>
            </tr>
         <tr width="100%">
                <td align="center" style="width: 100%" width="50%" colspan="3">

    
                    <span>&nbsp;</span><span class="style3">Plant Name</span><asp:DropDownList ID="ddl_Plantname" runat="server" 
                               CssClass="tb10" Height="30px" 
                               onselectedindexchanged="ddl_charttype_SelectedIndexChanged" 
                     Width="200px">
                           </asp:DropDownList>
           
    
                </td>
            </tr>
         <tr width="100%">
                <td align="center" style="width: 100%" width="50%" colspan="3">

    
                    <table class="style1">
                        <tr align="center" >
                            <td class="style2">
           
    
                    &nbsp;<asp:Label ID="Label1" runat="server" Text="Route Name" style="font-size: small"></asp:Label>
           
    
                    <asp:DropDownList ID="ddl_route" runat="server" 
                               CssClass="tb10" Height="30px" 
                               onselectedindexchanged="ddl_charttype_SelectedIndexChanged" 
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
                    </table>
           
    
                </td>
            </tr>
         <tr width="100%">
                <td align="center" style="width: 100%" width="50%" colspan="3">

    
                    <span style="font-size: small">&nbsp;Chart Type </span>
                           <asp:DropDownList ID="ddl_charttype" runat="server" 
                               CssClass="tb10" Height="30px" 
                               onselectedindexchanged="ddl_charttype_SelectedIndexChanged" 
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
         </table>
 </fieldset>


</td>
 
</tr>

    </table>
</div>
    <table width="100%">
    <tr>
    <td style="text-align: center">
    
    
               <asp:Button ID="Button5" runat="server" CssClass="form93" Font-Bold="False"   
                    Font-Size="X-Small"  xmlns:asp="#unknown"   
                    OnClientClick="return confirmationSave();" onclick="Button5_Click" Text="Get Report" 
                  TabIndex="6" />          
     <asp:Button ID="Button7" runat="server" CssClass="form93" Font-Bold="False"   
                    Font-Size="X-Small"  xmlns:asp="#unknown"    OnClientClick="return PrintPanel();" 
                    onclick="Button7_Click" Text="Print" 
                  TabIndex="6" />
           
    
    </td>
    </tr>
    
    </table>
           
</div>   
    <asp:Panel id="pnlContents" runat = "server">
    <center>
   
   
   
  <div align="CENTER" style="left: auto"> 
        <asp:Literal ID="lt" runat="server"  ></asp:Literal>
        <div id="chart_div" ></div>

          <asp:Literal ID="lt1" runat="server"  ></asp:Literal>
        <div id="chart_div1" ></div>
  </div>   
   
   </center>  
   </asp:Panel>


<%--   
        </div>
</ContentTemplate>


<Triggers>
    <asp:PostBackTrigger ControlID="Button5" />
    
       
    

</Triggers>
</asp:UpdatePanel>
  
    </form>
--%>


        
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>


