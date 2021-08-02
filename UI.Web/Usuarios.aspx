<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="UI.Web.Usuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <link href="TablasEstilos.css" rel="stylesheet" />
    <section class="fa-table">
    <asp:Panel ID="gridPanel" runat="server" >
        <asp:GridView class="tablas" ID="gridView" runat="server" AutoGenerateColumns="False" SelectedRowStyle-BackColor="Black" SelectedRowStyle-ForeColor="White" DataKeyNames="ID" OnLoad="Page_Load" OnSelectedIndexChanged="gridView_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre"/>
            <asp:BoundField HeaderText="Apellido" DataField="Apellido"/>
            <asp:BoundField HeaderText="Email" DataField="Email"/>
            <asp:BoundField HeaderText="Usuario" DataField="NombreUsuario"/>
            <asp:BoundField HeaderText="Habilitado" DataField="Habilitado"/>
            <asp:CommandField SelectText="Seleccionar" ShowSelectButton="true" />
        </Columns>
        </asp:GridView>
    </asp:Panel>
    </section>

   <asp:Panel ID="formPanel" Visible= "false" runat="server" Width="436px">
        <asp:Label ID="nombreLabel" runat="server" Text="Nombre: "></asp:Label>
        <asp:TextBox ID="nombreTextBox" runat="server">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="nombreTextBox" ErrorMessage="Nombre requerido">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="apellidoLabel" runat="server" Text="Apellido: "></asp:Label>
        <asp:TextBox ID="apellidoTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="apellidoTextBox" ErrorMessage="Apellido requerido">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="emailLabel" runat="server" Text="Email: "></asp:Label>
        <asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="emailTextBox" ErrorMessage="Email requerido">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="habilitadoLabel" runat="server" Text="Habilitado: "></asp:Label>
        <asp:CheckBox ID="habilitadoCheckBox" runat="server" />
        <br />
        <asp:Label ID="nombreUsuarioLabel" runat="server" Text="Usuario: "></asp:Label>
        <asp:TextBox ID="nombreUsuarioTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="nombreUsuarioTextBox" ErrorMessage="Usuario requerido">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="claveLabel" runat="server" Text="clave: "></asp:Label>
        <asp:TextBox ID="claveTextBox" TextMode="Password" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="claveTextBox" ErrorMessage="Clave requerida">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="repetirClaveLabel" runat="server" Text="Repetir Clave:"></asp:Label>
        <asp:TextBox ID="repetirClaveTextBox" TextMode="Password" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="repetirClaveTextBox" ForeColor="red" ErrorMessage="Clave requerida">*</asp:RequiredFieldValidator>
        <br />
   
    </asp:Panel>
    <section class="botones">
        <asp:Panel ID="gridActionsPanel" runat="server">
       <asp:LinkButton ID="editarLinkButton" runat="server" Width="50" OnClick="editarLinkButton_Click">Editar</asp:LinkButton>
       <asp:LinkButton ID="eliminarLinkButton" runat="server" Width="70" OnClick="eliminarLinkButton_Click">Eliminar</asp:LinkButton>
       <asp:LinkButton ID="nuevoLinkButton" runat="server" Width="50" OnClick="nuevoLinkButton_Click">Nuevo</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="formActionsPanel" runat="server">
       <asp:LinkButton ID="aceptarLinkButton" runat="server" Width="70" OnClick="aceptarLinkButton_Click">Aceptar</asp:LinkButton>
       <asp:LinkButton ID="cancelarLinkButton" runat="server" Width="70" CausesValidation="false" OnClick="cancelarLinkButton_Click">Cancelar</asp:LinkButton>
    </asp:Panel>
   </section>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
</asp:Content>
