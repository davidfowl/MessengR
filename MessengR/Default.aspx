<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MessengR.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.signalR-0.5.2.js" type="text/javascript"></script>
    <script src="signalR/hubs" type="text/javascript"></script>     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<% if (!Request.IsAuthenticated)
   {
       %>
       <h2>Sign up</h2> 
       <ol>
           <li><a href="~/Account/Register.aspx" runat="server">Sign up!</a></li>
           <li>Get the client from <a href="https://github.com/davidfowl/MessengR">github</a></li>
       </ol>
       <%
   }
   else
   {
       %> 
           Get the client from <a href="https://github.com/davidfowl/MessengR">github</a>
       <%
   }
%>
</asp:Content>
