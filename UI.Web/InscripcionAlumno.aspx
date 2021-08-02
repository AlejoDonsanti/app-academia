<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site.Master" CodeBehind="InscripcionAlumno.aspx.cs" Inherits="UI.Web.InscripcionAlumno" %>

<asp:Content ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="gridPanel" runat="server" >
        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="False" SelectedRowStyle-BackColor="Black" SelectedRowStyle-ForeColor="White" DataKeyNames="ID" OnLoad="Page_LoadInscripcion" OnSelectedIndexChanged="gridView_SelectedIndexChanged" >
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
    </asp:Panel>
    <asp:Panel ID="formActionsPanel" runat="server" CssClass="botonesAccion">
       <asp:LinkButton ID="aceptarLinkButton" runat="server" Width="70" OnClick="aceptarLinkButton_Click">Inscribirse</asp:LinkButton>
       <asp:LinkButton ID="cancelarLinkButton" runat="server" Width="70" CausesValidation="false" OnClick="cancelarLinkButton_Click">Cancelar</asp:LinkButton>
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />

</asp:Content>
