<%@ Page Title="Cursos" Language="C#"  MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="Cursos.aspx.cs" Inherits="UI.Web.Cursos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="gridPanel" runat="server" >
        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="False" SelectedRowStyle-BackColor="Black" SelectedRowStyle-ForeColor="White" DataKeyNames="ID" OnLoad="Page_LoadCurso" OnSelectedIndexChanged="gridView_SelectedIndexChanged" >
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="ID"/>
                <asp:BoundField HeaderText="Curso" DataField="Descripcion"/>
                <asp:BoundField HeaderText="Año Calendario" DataField="AnioCalendario"/>
                <asp:BoundField HeaderText="Cupo" DataField="Cupo"/>
                <asp:BoundField HeaderText="Materia" DataField="Materia"/>
                <asp:BoundField HeaderText="Comision" DataField="Comision"/>
                 <asp:CommandField SelectText="Seleccionar" ShowSelectButton="true" />
            </Columns>

        </asp:GridView>
        <asp:Button ID="btnExportar" runat="server" Text="Exportar" OnClick="btnExportar_Click" />
    </asp:Panel>
    <asp:Panel ID="formPanel" Visible= "false" runat="server" Width="436px">
        <asp:Label ID="descLabel" runat="server" Text="Descripción"></asp:Label>
        <asp:TextBox ID="descTB" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="validadorDesc" runat="server" ControlToValidate="descTB" ErrorMessage="Descripción requerida">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="anioLabel" runat="server" Text="Año Calendario"></asp:Label>
        <asp:TextBox ID="anioTB" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="validadorAnio" runat="server" ControlToValidate="anioTB" ErrorMessage="Año requerido">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="cupoLabel" runat="server" Text="Cupo"></asp:Label>
        <asp:TextBox ID="cupoTB" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cupoTB" ErrorMessage="cupo requerido">*</asp:RequiredFieldValidator>
        <br />
        <asp:DropDownList ID="materiaDD" runat="server" DataTextField="Descripcion" DataValueField="ID" ></asp:DropDownList>
        <asp:DropDownList ID="comisionDD" runat="server" DataTextField="Descripcion" DataValueField="ID" ></asp:DropDownList>
    </asp:Panel>
    <asp:Panel ID="gridActionsPanel" runat="server" CssClass="botonesAccion">
       <asp:LinkButton ID="editarLinkButton" runat="server" Width="50" OnClick="editarLinkButton_Click">Editar</asp:LinkButton>
       <asp:LinkButton ID="eliminarLinkButton" runat="server" Width="70" OnClick="eliminarLinkButton_Click" >Eliminar</asp:LinkButton>
       <asp:LinkButton ID="nuevoLinkButton" runat="server" Width="50" OnClick="nuevoLinkButton_Click">Nuevo</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="formActionsPanel" runat="server" CssClass="botonesAccion">
       <asp:LinkButton ID="aceptarLinkButton" runat="server" Width="70" OnClick="aceptarLinkButton_Click">Aceptar</asp:LinkButton>
       <asp:LinkButton ID="cancelarLinkButton" runat="server" Width="70" CausesValidation="false" OnClick="cancelarLinkButton_Click">Cancelar</asp:LinkButton>
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
</asp:Content>
