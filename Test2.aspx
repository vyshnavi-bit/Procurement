<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Test2.aspx.cs" Inherits="Test2" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">
        
       .menuItem
        {
            border:Solid 1px black;
            width:100px;
            padding:2px;
            background-color:Orange;
        }
        .menuItem a
        {
            color:blue;
        }
                
        .IE8Fix
        {
            z-index: 1000;

        }
    
    </style>


      <style type="text/css">
        .menuItem
        {
            border: Solid 1px black;
            width: 100px;
            padding: 4px;
            background-color: Silver;
            font-size: medium;
            font-style: normal;
        }
        .menuItem a
        {
            color: Blue;
            font-size: medium;
        }
        
        .IE8Fix
        {
            z-index: 1000;
            background-color: Silver;
        }
    </style>
    <style type="text/css">
        .parent_menu
        {
            width: 108px;
            background-color: #8AE0F2;
            color: #000;
            text-align: center;
            height: 30px;
            margin-right: 5px;
            font-size: medium;
        }
        .child_menu
        {
            width: 110px;
            background-color: #000;
            color: #fff;
            text-align: center;
            height: 30px;
            line-height: 30px;
            font-size: medium;
        }
        .sub_menu
        {
            width: 150px;
            background-color: #000;
            color: #fff;
            text-align: center;
            height: 30px;
            line-height: 30px;
            margin-top: 5px;
            font-size: medium;
        }
        .selected_menu
        {
            background-color: #FF6600;
        }
        .hover_menu
        {
            background-color: #990000;
            color: #fff;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>

       <asp:ToolkitScriptManager ID="ToolkitScriptManager1" EnablePageMethods="true" runat="server">
    </asp:ToolkitScriptManager>   



 






 <center>
      <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" Font-Size="Small" BackColor="#FF9933"                                >
                                <LevelMenuItemStyles>
                                    <asp:MenuItemStyle CssClass="parent_menu" />
                                </LevelMenuItemStyles>
                                <LevelSelectedStyles>
                                    <asp:MenuItemStyle CssClass="child_menu" />
                                </LevelSelectedStyles>
                                <DynamicMenuItemStyle CssClass="sub_menu" />
                                <DynamicHoverStyle CssClass="hover_menu" />
                                <StaticSelectedStyle CssClass="selected_menu" />
                                <StaticHoverStyle CssClass="hover_menu" />
                             
                                <DynamicMenuStyle CssClass="IE8Fix" />
                                 
                            </asp:Menu>
      
      </center>



      <div ALIGN="center">
    <asp:TreeView ID="tvMenu" runat="server" Width="357px"></asp:TreeView>
</div>



    
    </div>
                    <asp:Button ID="btn_Generate" runat="server" BackColor="#00CCFF" 
                  BorderStyle="Double" Font-Bold="True" ForeColor="White" 
        Height="26px"  Visible="true"  
                  Text="Generate" onclick="btn_Generate_Click"  /> 


<br />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    &nbsp;
</ContentTemplate>
 
</asp:UpdatePanel>
</div>
</asp:Content>
