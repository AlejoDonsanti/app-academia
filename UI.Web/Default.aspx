<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UI.Web.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <h1>BIENVENIDO</h1><br /><br />
    <asp:Label runat="server" ID="lblUsuario" Text="Usuario"></asp:Label>
    <asp:TextBox runat="server" ID="tbUsuario"></asp:TextBox><br />
    <asp:Label runat="server" ID="Label1" Text="Contraseña"></asp:Label>
    <asp:TextBox runat="server" ID="tbContraseña" TextMode="Password"></asp:TextBox><br />
    <asp:LinkButton ID="ingresarLinkButton" runat="server" Width="50" OnClick="ingresarLinkButton_Click">Ingresar</asp:LinkButton>
      
</asp:Content>
