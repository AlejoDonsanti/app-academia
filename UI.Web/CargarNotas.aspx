<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CargarNotas.aspx.cs" Inherits="UI.Web.CargarNotas" %>

<asp:Content ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <h1>Cargar Notas</h1>
    <br />
    <asp:DropDownList ID="ddCursos" runat="server" DataTextField="Descripcion" DataValueField="ID"></asp:DropDownList>
    <asp:Button ID="Button1" runat="server" Text="Actualizar" OnClick="Button1_Click" /><br />

    <asp:GridView ID="gvAlumnosInscriptos" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnSelectedIndexChanged="gridView_SelectedIndexChanged" SelectedRowStyle-BackColor="Black" SelectedRowStyle-ForeColor="White">

        <Columns>

            <asp:BoundField DataField="ID" HeaderText="ID" />
            <asp:BoundField DataField="Alumno" HeaderText="Alumno" />
            <asp:BoundField DataField="Condicion" HeaderText="Condición" />
            <asp:BoundField DataField="Nota" HeaderText="Nota" />
            <asp:CommandField ShowSelectButton="True" />

        </Columns>

<SelectedRowStyle BackColor="Black" ForeColor="White"></SelectedRowStyle>
    </asp:GridView><br />
    <asp:Button ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click" />
    <asp:Panel ID="editPanel" runat="server">
        <br />
        <asp:Label ID="lbNota" runat="server" Text="Nota: "></asp:Label>
        <asp:TextBox ID="tbNota" runat="server"></asp:TextBox><br />
        <asp:Label ID="lbCondicion" runat="server" Text="Condición: "></asp:Label>
        <asp:TextBox ID="tbCondicion" runat="server"></asp:TextBox><br />
        
       <asp:LinkButton ID="aceptarLinkButton" runat="server" Width="70" OnClick="aceptarLinkButton_Click" >Aceptar</asp:LinkButton>
       <asp:LinkButton ID="cancelarLinkButton" runat="server" Width="70" CausesValidation="false" OnClick="cancelarLinkButton_Click" >Cancelar</asp:LinkButton>
    
    </asp:Panel>
</asp:Content>
