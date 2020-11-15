<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">

style11
{
    width:100%;
}

.blink
{
text-decoration:blink
}


</style>


<%--<script type="text/javascript">
    function blink(id, delay) {
        var s = document.getElementById(id).style;
        s.visibility = (s.visibility == "" ? "hidden" : "");
        setTimeout(blink, delay);
    }
    attachEvent("onload", "blink('lblCountry', 300)");
</script>
--%>

  
 <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
<script type="text/javascript">
    $(function () {
        blinkeffect('#txtblnk');
    })
    function blinkeffect(selector) {
        $(selector).fadeOut('fast', function () {
            $(this).fadeIn('fast', function () {
                blinkeffect(this);
            });
        });
    }
</script>

<script type="text/javascript">
    $(function () {
        blinkeffect('.DIVCLASS');
    })
    function blinkeffect(selector) {
        $(selector).fadeOut('slow', function () {
            $(this).fadeIn('slow', function () {
                blinkeffect(this);
            });
        });
    }

    $(function () {
        blinkeffect('.DIV1CLASS');
    })
    function blinkeffect(selector) {
        $(selector).fadeOut('slow', function () {
            $(this).fadeIn('slow', function () {
                blinkeffect(this);
            });
        });
    }

  

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="style11">

          <asp:DataList ID="DataList1" runat="server" BackColor="Gray" BorderColor="#666666"
            BorderStyle="None" BorderWidth="2px" CellPadding="3" CellSpacing="2"
            Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="6" RepeatDirection="Horizontal"
            Width=100%>
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" Font-Size="Large" ForeColor="White"
                HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderTemplate>
                Employee Details</HeaderTemplate>
            <ItemStyle BackColor="White" ForeColor="Black" BorderWidth="2px" />
            <ItemTemplate>
               <%-- <asp:Image ID="imgEmp" runat="server" Width="100px" Height="120px" ImageUrl='<%# Bind("PhotoPath", "~/photo/{0}") %>' style="padding-left:40px"/><br /> --%>
                <div id="Div2" class="DIV1CLASS">
                <b>plantcode:</b>
                <asp:Label ID="lblCName" runat="server" style="color: #0000FF; font-weight: bold" Text='<%# Bind("plantcode") %>'></asp:Label>
                <br />
                <b>plantname:</b>
                <asp:Label ID="lblName" runat="server"  style="color: #510019; font-weight: bold"   Text='<%# Bind("plantname") %>'></asp:Label>
                <br />
               <b> milkkg:</b>
             
                <asp:Label ID="lblCity" runat="server"  style="color:#A12069; font-weight: bold" Text=' <%# Bind("milkkg") %>'></asp:Label>
                
                <br />
                <b>milkltr:</b>
                
                <asp:Label ID="lblCountry" runat="server"  style="color:#FF6347; font-weight: bold"  Text=' <%# Bind("milkltr") %>'></asp:Label>
               
                <br />
                <b>fat:</b>
                 
                <asp:Label ID="Label1" runat="server"  style="color:#800000; font-weight: bold"  Text='<%# Bind("fat") %>'></asp:Label>
                 
                <br />
                <b>snf:</b>
                <asp:Label ID="Label2" runat="server"  style="color:#0000A0; font-weight: bold" Text='<%# Bind("snf") %>'></asp:Label>
                <br />
                <b>noofsample:</b>
                <asp:Label ID="Label3" runat="server"  style="color:#D580FF; font-weight: bold"  CssClass="blink" Text='<%# Bind("noofsample") %>'></asp:Label>
              </div>
            </ItemTemplate>
        </asp:DataList>
    </div>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>

