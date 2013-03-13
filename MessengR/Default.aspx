<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MessengR.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/jquery.signalR-1.0.1.min.js"></script>
    <script src="signalR/hubs" type="text/javascript"></script>     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<% 
    
var url = Regex.Replace(Request.Url.AbsoluteUri, @"Default\.aspx", "", RegexOptions.IgnoreCase);
var config = String.Format(@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <appSettings>
    <add key=""HostUrl"" value=""{0}""/>
  </appSettings>
</configuration>""", url);
%>

<% if (!Request.IsAuthenticated)
   {
       %>
       <h2>Sign up</h2> 
       <ol>
           <li><a href="~/Account/Register.aspx" runat="server">Sign up!</a></li>
           <li>Get the client from <a href="https://github.com/davidfowl/MessengR">github</a></li>
           <li>
Change the settings in MessengR.Client's app.config
<pre>
<%: config%>
</pre>
           </li>
       </ol>
       <%
   }
   else
   {
       %> 
Get the client from <a href="https://github.com/davidfowl/MessengR">github</a>
Change the settings in MessengR.Client's app.config
<pre>
<%: config%>
</pre>
       <%
   }
%>
</asp:Content>
