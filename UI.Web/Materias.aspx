<%@ Page Title="Materias" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Materias.aspx.cs" Inherits="UI.Web.Materias" %>
<asp:Content ID="Content3" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="gridPanelMat" runat="server" >
        <asp:GridView ID="gridViewMat" runat="server" AutoGenerateColumns="False" SelectedRowStyle-BackColor="Black" SelectedRowStyle-ForeColor="White" DataKeyNames="ID" OnLoad="Page_Load" OnSelectedIndexChanged="gridViewMat_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion"/>
            <asp:BoundField HeaderText="Horas Semanales" DataField="HsSemanales"/>
            <asp:BoundField HeaderText="Horas Totales" DataField="HsTotales"/>
            <asp:BoundField HeaderText="Plan" DataField="Plan"/>
            <asp:CommandField SelectText="Seleccionar" ShowSelectButton="true" />
        </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="formPanelMat" Visible= "false" runat="server" Width="436px">
        <asp:Label ID="descripcionLabel" runat="server" Text="Descripcion: "></asp:Label>
        <asp:TextBox ID="descripcionTextBox" runat="server">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMat1" runat="server" ControlToValidate="descripcionTextBox" ErrorMessage="Descripcion requerida">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="hsemanalesLabel" runat="server" Text="Horas Semanales: "></asp:Label>
        <asp:TextBox ID="hsemanalesTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMat2" runat="server" ControlToValidate="hsemanalesTextBox" ErrorMessage="Horas semanales requeridas">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="htotalesLabel" runat="server" Text="Horas Totales: "></asp:Label>
        <asp:TextBox ID="htotalesTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorMat3" runat="server" ControlToValidate="htotalesTextBox" ErrorMessage="Horas totales requeridas">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="planLabel" runat="server" Text="Plan: "></asp:Label>
        <asp:DropDownList ID="planesDD" runat="server" DataTextField="Descripcion" DataValueField="ID"></asp:DropDownList>
        <br />
    </asp:Panel>
    <asp:Panel ID="gridActionsPanelMat" runat="server">
       <asp:LinkButton ID="editarLinkButtonMat" runat="server" Width="50" OnClick="editarLinkButtonMat_Click">Editar</asp:LinkButton>
       <asp:LinkButton ID="eliminarLinkButtonMat" runat="server" Width="70" OnClick="eliminarLinkButtonMat_Click">Eliminar</asp:LinkButton>
       <asp:LinkButton ID="nuevoLinkButtonMat" runat="server" Width="50" OnClick="nuevoLinkButtonMat_Click">Nuevo</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="formActionsPanelMat" runat="server">
       <asp:LinkButton ID="aceptarLinkButtonMat" runat="server" Width="70" OnClick="aceptarLinkButtonMat_Click">Aceptar</asp:LinkButton>
       <asp:LinkButton ID="cancelarLinkButtonMat" runat="server" Width="70" CausesValidation="false" OnClick="cancelarLinkButtonMat_Click">Cancelar</asp:LinkButton>
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummaryMat1" runat="server" />
</asp:Content>
