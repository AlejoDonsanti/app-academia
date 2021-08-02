<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="NotasAlumno.aspx.cs" Inherits="UI.Web.NotasAlumno" %>


<asp:Content runat="server" ContentPlaceHolderID="bodyContentPlaceHolder">
    <h1>Notas por Curso</h1>
    <asp:GridView ID="gvNotasAlumno" runat="server" AutoGenerateColumns="false">
        <Columns>

            <asp:BoundField DataField="Curso" HeaderText="Curso" />
            <asp:BoundField DataField="Nota" HeaderText="Nota" />
            <asp:BoundField DataField="Condicion" HeaderText="Condición" />

        </Columns>
    </asp:GridView>


</asp:Content>