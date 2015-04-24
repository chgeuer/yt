<%@ Page Async="true" Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" 
    AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-4">
            <asp:TextBox ID="txtYouTubeURL" runat="server" Text=""></asp:TextBox>
            <asp:Button ID="btnProcess" runat="server" Text="Process" OnClick="btnProcess_Click" />
        </div>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlVideoFormats" runat="server" />
            <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="btnDownload_Click" />
        </div>
        <div class="col-md-4">
            <asp:Label ID="lblTitle" runat="server" Text="lblTitle" />
            <asp:Label ID="lblMessage" runat="server" Text="lblMessage" />
        </div>
    </div>
</asp:Content>
