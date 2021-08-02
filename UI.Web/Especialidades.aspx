<%@ Page Title="Especialidades" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Especialidades.aspx.cs" Inherits="UI.Web.Especialidades" %>
<asp:Content ID="ContentEsp" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
     <asp:Panel ID="gridPanelEsp" runat="server" >
        <asp:GridView ID="gridViewEsp" runat="server" AutoGenerateColumns="False" SelectedRowStyle-BackColor="Black" SelectedRowStyle-ForeColor="White" DataKeyNames="ID" OnLoad="Page_Load" OnSelectedIndexChanged="gridView_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID"/>
            <asp:BoundField HeaderText="Descripcion" DataField="Descripcion"/>
            <asp:CommandField SelectText="Seleccionar" ShowSelectButton="true" />
        </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="formPanelEsp" Visible= "false" runat="server" Width="436px">
        <asp:Label ID="descripcionLabel" runat="server" Text="Descripcion: "></asp:Label>
        <asp:TextBox ID="descripcionTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorEsp1" runat="server" ControlToValidate="descripcionTextBox" ErrorMessage="Descripcion requerida">*</asp:RequiredFieldValidator>
        <br />
    </asp:Panel>
     <asp:Panel ID="gridActionsPanelEsp" runat="server">
       <asp:LinkButton ID="editarLinkButtonEsp" runat="server" Width="50" OnClick="editarLinkButtonEsp_Click">Editar</asp:LinkButton>
       <asp:LinkButton ID="eliminarLinkButtonEsp" runat="server" Width="70" OnClick="eliminarLinkButtonEsp_Click">Eliminar</asp:LinkButton>
       <asp:LinkButton ID="nuevoLinkButtonEsp" runat="server" Width="50" OnClick="nuevoLinkButtonEsp_Click">Nuevo</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="formActionsPanelEsp" runat="server">
       <asp:LinkButton ID="aceptarLinkButtonEsp" runat="server" Width="70" OnClick="aceptarLinkButtonEsp_Click">Aceptar</asp:LinkButton>
       <asp:LinkButton ID="cancelarLinkButtonEsp" runat="server" Width="70" CausesValidation="false" OnClick="cancelarLinkButtonEsp_Click">Cancelar</asp:LinkButton>
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummaryEsp1" runat="server" />
</asp:Content>
