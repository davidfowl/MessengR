<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MessengR.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Scripts/jquery-1.6.4.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.signalR-0.5.2.js" type="text/javascript"></script>
    <script src="signalR/hubs" type="text/javascript"></script>     
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<% if (!Request.IsAuthenticated) return; %>

    <asp:ScriptManager runat="server" ID="sm" />
    <asp:UpdatePanel runat="server" ID="myPanel">
        <ContentTemplate>
            <ul>
            <asp:Repeater runat="server" ID="users">
                <ItemTemplate>
                    <li>
                        <%# (bool)Eval("Online") ? "<span class=\"icon icon-online\" />" : "<span class=\"icon icon-offline\" />"%> <%# Eval("Name") %>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
            </ul>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <script type="text/javascript">
        var chat = $.connection.chat,
            conversations = {};

        function updateRepeater() {
            __doPostBack('<%= myPanel.UniqueID %>', '');
        }

        chat.addMessage = function (message) {
            
        };

        // Since we're just rebinding the repeader, force a refresh
        chat.markOnline = updateRepeater;

        chat.markOffline = updateRepeater;

        $.connection.hub.start().done(updateRepeater);

    </script>
</asp:Content>
