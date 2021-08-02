<%@ Page Title="Comisiones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Comisiones.aspx.cs" Inherits="UI.Web.Comisiones" %>
<asp:Content ID="ContentCom" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="gridPanelCom" runat="server" >
        <asp:GridView ID="gridViewCom" runat="server" AutoGenerateColumns="False" SelectedRowStyle-BackColor="Black" SelectedRowStyle-ForeColor="White" DataKeyNames="ID" OnLoad="Page_Load" OnSelectedIndexChanged="gridViewCom_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion"/>
            <asp:BoundField HeaderText="Año Especialidad" DataField="AnioEspecialidad"/>
            <asp:BoundField HeaderText="Plan" DataField="Plan"/>
            <asp:CommandField SelectText="Seleccionar" ShowSelectButton="true" />
        </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="formPanelCom" Visible= "false" runat="server" Width="436px">
        <asp:Label ID="descripcionLabel" runat="server" Text="Descripcion: "></asp:Label>
        <asp:TextBox ID="descripcionTextBox" runat="server">
        </asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCom1" runat="server" ControlToValidate="descripcionTextBox" ErrorMessage="Descripcion requerida">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="anioLabel" runat="server" Text="Año Especialidad: "></asp:Label>
        <asp:TextBox ID="anioTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCom2" runat="server" ControlToValidate="anioTextBox" ErrorMessage="Año requerido">*</asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="planLabel" runat="server" Text="Plan: "></asp:Label>
        <asp:TextBox ID="planTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorCom4" runat="server" ControlToValidate="planTextBox" ErrorMessage="Plan requerido">*</asp:RequiredFieldValidator>
        <br />
    </asp:Panel>
    <asp:Panel ID="gridActionsPanelCom" runat="server">
       <asp:LinkButton ID="editarLinkButtonCom" runat="server" Width="50" OnClick="editarLinkButtonCom_Click">Editar</asp:LinkButton>
       <asp:LinkButton ID="eliminarLinkButtonCom" runat="server" Width="70" OnClick="eliminarLinkButtonCom_Click">Eliminar</asp:LinkButton>
       <asp:LinkButton ID="nuevoLinkButtonCom" runat="server" Width="50" OnClick="nuevoLinkButtonCom_Click">Nuevo</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="formActionsPanelCom" runat="server">
       <asp:LinkButton ID="aceptarLinkButtonCom" runat="server" Width="70" OnClick="aceptarLinkButtonCom_Click">Aceptar</asp:LinkButton>
       <asp:LinkButton ID="cancelarLinkButtonCom" runat="server" Width="70" CausesValidation="false" OnClick="cancelarLinkButtonCom_Click">Cancelar</asp:LinkButton>
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummaryCom1" runat="server" />
</asp:Content>
