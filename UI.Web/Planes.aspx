<%@ Page Title="Planes" Language="C#" MasterPageFile="~/Site.Master"  EnableEventValidation = "false" AutoEventWireup="true" CodeBehind="Planes.aspx.cs" Inherits="UI.Web.Planes" %>
<asp:Content ID="ContentPla" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="gridPanelPla" runat="server" >
        <asp:GridView ID="gridViewPla" runat="server" AutoGenerateColumns="False" SelectedRowStyle-BackColor="Black" SelectedRowStyle-ForeColor="White" DataKeyNames="ID" OnLoad="Page_Load" OnSelectedIndexChanged="gridViewPla_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion"/>
            <asp:BoundField HeaderText="Especialidad" DataField="Especialidad"/>
            <asp:CommandField SelectText="Seleccionar" ShowSelectButton="true" />
        </Columns>
        </asp:GridView>
        
    </asp:Panel>
    <asp:Button runat="server" ID="btnExportar" Text="Exportar" OnClick="btnExportar_Click" />
    <asp:Panel ID="formPanelPla" Visible= "false" runat="server" Width="436px">
        <asp:Label ID="descripcionLabel" runat="server" Text="Descripcion: "></asp:Label>
        <asp:TextBox ID="descripcionTextBox" runat="server">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPla1" runat="server" ControlToValidate="descripcionTextBox" ErrorMessage="Descripcion requerida">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="especialidadLabel" runat="server" Text="Especialidad: "></asp:Label>
        <asp:TextBox ID="especialidadTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPla2" runat="server" ControlToValidate="especialidadTextBox" ErrorMessage="Especialidad requerida">*</asp:RequiredFieldValidator>
        <br />
    </asp:Panel>
    <asp:Panel ID="gridActionsPanelPla" runat="server">
       <asp:LinkButton ID="editarLinkButtonPla" runat="server" Width="50" OnClick="editarLinkButtonPla_Click">Editar</asp:LinkButton>
       <asp:LinkButton ID="eliminarLinkButtonPla" runat="server" Width="70" OnClick="eliminarLinkButtonPla_Click">Eliminar</asp:LinkButton>
       <asp:LinkButton ID="nuevoLinkButtonPla" runat="server" Width="50" OnClick="nuevoLinkButtonPla_Click">Nuevo</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="formActionsPanelMat" runat="server">
       <asp:LinkButton ID="aceptarLinkButtonPla" runat="server" Width="70" OnClick="aceptarLinkButtonPla_Click">Aceptar</asp:LinkButton>
       <asp:LinkButton ID="cancelarLinkButtonPla" runat="server" Width="70" CausesValidation="false" OnClick="cancelarLinkButtonPla_Click">Cancelar</asp:LinkButton>
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummaryPla1" runat="server" />
</asp:Content>
