<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TimeWeigherAndLab.aspx.cs" Inherits="TimeWeigherAndLab" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
    .button {
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

.button2 {background-color: #008CBA;} /* Blue */
.button3 {background-color: #f44336;} /* Red */
.button4 {background-color: #e7e7e7; color: black;} /* Gray */
.button5 {background-color: #555555;} /* Black */
.text1 {
    border: 2px solid rgb(173, 204, 204);
    height: 31px;
    width: 223px;
    box-shadow: 0 0 27px rgb(204, 204, 204) inset;
    transition: 500ms all ease;
    padding: 3px 3px 3px 3px;
        text-align: center;
    }

.text1:hover,
.text1:focus {
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
.dropbtn {
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
.dropdown {
    position: relative;
    display: inline-block;
            top: 0px;
            left: 0px;
        }

/* Dropdown Content (Hidden by Default) */
.dropdown-content {
    display: none;
    position: absolute;
    background-color: #f9f9f9;
    min-width: 160px;
    box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
}

/* Links inside the dropdown */
.dropdown-content a {
    color: black;
    padding: 12px 16px;
    text-decoration: none;
    display: block;
}

/* Change color of dropdown links on hover */
.dropdown-content a:hover {background-color: #f1f1f1}

/* Show the dropdown menu on hover */
.dropdown:hover .dropdown-content {
    display: block;
}

/* Change the background color of the dropdown button when the dropdown content is shown */
.dropdown:hover .dropbtn {
    background-color: #3e8e41;
}
</style>

  <style>
.text1 {
    border: 2px solid rgb(173, 204, 204);
    height: 31px;
    width: 400px;
    box-shadow: 0 0 27px rgb(204, 204, 204) inset;
    transition: 500ms all ease;
    padding: 3px 3px 3px 3px;
        text-align: left;
    }

.text1:hover,
.text1:focus {
    width: 260px;
    transition: 500ms all ease;
    background: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyBpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMC1jMDYwIDYxLjEzNDc3NywgMjAxMC8wMi8xMi0xNzozMjowMCAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNSBXaW5kb3dzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjYxRDEzQTBCMzI0MzExRTFBNDYzRkQ4Qzc3RDdBOTg5IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjYxRDEzQTBDMzI0MzExRTFBNDYzRkQ4Qzc3RDdBOTg5Ij4gPHhtcE1NOkRlcml2ZWRGcm9tIHN0UmVmOmluc3RhbmNlSUQ9InhtcC5paWQ6NjFEMTNBMDkzMjQzMTFFMUE0NjNGRDhDNzdEN0E5ODkiIHN0UmVmOmRvY3VtZW50SUQ9InhtcC5kaWQ6NjFEMTNBMEEzMjQzMTFFMUE0NjNGRDhDNzdEN0E5ODkiLz4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz52VTCHAAABFUlEQVR42uxV0Q2DIBBV4z8bdAM26AZ1Ap3AToAT6AR1Ap2gbuAGzKD/TkCP5NmQRpQaSH96yQsJHO/k7t0ZK6WikJZEgS14gHSeZxX6BQuhwerL3pw6ACMIQkuQHsgluDQnM2ugN3rgrK33ha3INdbqBHn1wWFVUU7ghMKxLgt8Oe46yVQ7lrgoD/JdwDf/tg+ueK4mGDfOR5zV8D3VaBwFy6CM1VrsCfjYG81BclrPHVZp7HcoLN8LshdgNMg50pDh7In+uSCIsKUpnqZJWfQ8EB4gOlLRnXBzVVFvpIA5yJTBV241aeLSLI622aRmiho8k3uYRcM6LlLksEWzMA/DjqP4+oNLW5G9Wfz/J/88wEuAAQA9yExzBAEQqwAAAABJRU5ErkJggg==) no-repeat right;
    background-size: 25px 25px;
    background-position: 96% 62%;
    padding: 3px 32px 3px 3px;
}


.text2 {
    border: 2px solid rgb(173, 204, 204);
    height: 31px;
    width: 100%;
    box-shadow: 0 0 27px rgb(204, 204, 204) inset;
    transition: 500ms all ease;
    padding: 3px 3px 3px 3px;
        text-align: left;
    }

.text2:hover,
.text2:focus {
    width:100%;
    transition: 500ms all ease;
  
    background-size: 25px 25px;
    background-position: 96% 62%;
    padding: 3px 32px 3px 3px;
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





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
     </asp:ToolkitScriptManager>
    <table  align="center" width="100%" style="border: thin ridge #008080">
        <tr align="center">
            <td align="CENTER" style="text-align: center" colspan="2">

    
                                            <asp:DropDownList ID="ddl_Plantname" runat="server" CssClass="tb10" 
                                                Font-Bold="True" Font-Size="Medium" 
                    Height="20px" Width="205px" Visible="False">
                                            </asp:DropDownList>
                
    
            </td>
        </tr>
        <tr align="center">
            <td align="CENTER" style="text-align: right">

    
                From<asp:TextBox ID="txt_FromDate" runat="server" class="text1"
                                    Width="75px" ></asp:TextBox>
                         <asp:CalendarExtender ID="txt_FromDate_CalendarExtender" 
                    runat="server" TargetControlID="txt_FromDate"
                                PopupButtonID="txt_FromDate" Format="dd/MM/yyyy" 
                    PopupPosition="TopRight">
                        </asp:CalendarExtender>                   	
                
    
            </td>
            <td align="left">

    
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">AM</asp:ListItem>
                    <asp:ListItem Value="2">PM</asp:ListItem>
                </asp:RadioButtonList>
                
    
            </td>
        </tr>
        <tr align="center">
            <td align="CENTER" style="width: 55%; text-align: center" colspan="2">

    
                                <asp:Button ID="Button5" runat="server" CssClass="button" onclick="Button5_Click" 
                                    Text="Show" Font-Size="10px" Font-Bold="True" />
                            
                                <asp:Button ID="Button6" runat="server" CssClass="button" Font-Bold="True" 
                                           OnClientClick="return PrintPanel();" 
                          Font-Size="10px"  Text="Print" onclick="Button6_Click" />
                            
            </td>
        </tr>
        <tr align="center">
            <td align="CENTER" style="width: 55%; text-align: center" colspan="2">

    
                                &nbsp;</td>
        </tr>
        </table>

      <table width=100%  class="style2">
        
        
        </table>

           <center>
                      <asp:Panel id="pnlContents" align="center" runat = "server">
          <table class="style2" width=100%>
                                    <tr width=100%>
                                           <td align=center>
                                               <br />
                                               <asp:GridView ID="GridView1" runat="server" CssClass="gridcls" Font-Bold="true" 
                                                   ForeColor="White" GridLines="Both" onrowcreated="GridView1_RowCreated1" 
                                                   onrowdatabound="GridView1_RowDataBound1" Width="100%">
                                                   <EditRowStyle BackColor="#999999" />
                                                   <FooterStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
                                                   <HeaderStyle BackColor="#f4f4f4" Font-Bold="False" Font-Italic="False" 
                                                       Font-Names="Raavi" Font-Size="Small" ForeColor="Black" />
                                                   <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                   <RowStyle BackColor="#ffffff" ForeColor="#333333" HorizontalAlign="Center" />
                                                   <AlternatingRowStyle HorizontalAlign="Center" />
                                                   <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                   <Columns>
                                                   

                                                    
									   <asp:TemplateField HeaderText="SNo.">
										   <ItemTemplate>
											   <%# Container.DataItemIndex + 1 %>
										   </ItemTemplate>
									   </asp:TemplateField>
								 


                                                   
                                                   
                                                   </Columns>
                                               </asp:GridView>
                                               <br />
                                            </td>
                                           
                                        
                                    </tr>
                                    <tr align="center"  class="text2">
                                        <td width=100% align="center">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                </asp:Panel>
    </center>
            <br />
            <br />                
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
