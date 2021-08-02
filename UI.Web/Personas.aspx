<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Personas.aspx.cs" Inherits="UI.Web.Personas" %>

<asp:Content ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:GridView ID="gvPersona" runat="server" AutoGenerateColumns="false" SelectedRowStyle-BackColor="Black" SelectedRowStyle-ForeColor="White" DataKeyNames="ID" OnSelectedIndexChanged="gvPersona_SelectedIndexChanged">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID"/>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre"/>
            <asp:BoundField HeaderText="Apellido" DataField="Apellido"/>
            <asp:BoundField HeaderText="Email" DataField="Email"/>
            <asp:BoundField HeaderText="Direccion" DataField="Direccion"/>
            <asp:BoundField HeaderText="Telefono" DataField="Telefono"/>
            <asp:BoundField HeaderText="Legajo" DataField="Legajo"/>
            <asp:BoundField HeaderText="Fecha de Nacimiento" DataField="FechaNacimiento"/>
            <asp:BoundField HeaderText="Tipo Persona" DataField="TipoPersona"/>
            <asp:BoundField HeaderText="ID PLAN" DataField="IDPlan"/>
            <asp:CommandField SelectText="Seleccionar" ShowSelectButton="true" />
        </Columns>
        
    </asp:GridView>
    <asp:Panel ID="formPanel" Visible= "false" runat="server" Width="436px" HorizontalAlign="Left">
        <asp:Label ID="labelNombre" runat="server" Text="Nombre"></asp:Label>
        <asp:TextBox ID="tbNombre" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="tbNombre" ErrorMessage="Nombre Requerido" runat="server">*</asp:RequiredFieldValidator><br />
        <asp:Label ID="label1" runat="server" Text="Apellido"></asp:Label>
        <asp:TextBox ID="tbApellido" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="tbApellido" ErrorMessage="Apellido Requerido" runat="server">*</asp:RequiredFieldValidator><br />
        <asp:Label ID="label2" runat="server" Text="Legajo"></asp:Label>
        <asp:TextBox ID="tbLegajo" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="tbLegajo" ErrorMessage="Legajo Requerido" runat="server">*</asp:RequiredFieldValidator><br />
        <asp:Label ID="label3" runat="server" Text="Email"></asp:Label>
        <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="tbEmail" ErrorMessage="Email Requerido" runat="server">*</asp:RequiredFieldValidator><br />
        <asp:Label ID="label6" runat="server" Text="Direccion"></asp:Label>
        <asp:TextBox ID="tbDir" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="tbDir" ErrorMessage="Direccion Requerida" runat="server">*</asp:RequiredFieldValidator><br />
        <asp:Label ID="label7" runat="server" Text="Telefono"></asp:Label>
        <asp:TextBox ID="tbTel" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="tbTel" ErrorMessage="Telefono Requerido" runat="server">*</asp:RequiredFieldValidator><br />
        
        <asp:Label ID="label5" runat="server" Text="Tipo de persona"></asp:Label>
        <asp:DropDownList ID="DDTipoPersona" runat="server" DataTextField="Descripcion" DataValueField="ID" >
            <asp:ListItem Value="1">Alumno</asp:ListItem>
            <asp:ListItem Value="2">Profesor</asp:ListItem>
            <asp:ListItem Value="3">Adminiestrador</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Label ID="planLabel" runat="server" Text="Plan: "></asp:Label>
        <asp:DropDownList ID="planesDD" runat="server" DataTextField="Descripcion" DataValueField="ID"></asp:DropDownList>
        <br />
        <asp:Label ID="label4" runat="server" Text="Fecha de Nacimiento"></asp:Label><br />
        <asp:Calendar ID="calendarFechaNac" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px" CaptionAlign="NotSet">
            <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
            <NextPrevStyle VerticalAlign="Bottom" />
            <OtherMonthDayStyle ForeColor="#808080" />
            <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
            <SelectorStyle BackColor="#CCCCCC" />
            <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
            <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
            <WeekendDayStyle BackColor="#FFFFCC" />
        </asp:Calendar>

    </asp:Panel>
     <section class="botones">
        <asp:Panel ID="gridActionsPanel" runat="server">
       <asp:LinkButton ID="editarLinkButton" runat="server" Width="50" OnClick="editarLinkButtonPersona_Click" >Editar</asp:LinkButton>
       <asp:LinkButton ID="eliminarLinkButton" runat="server" Width="70" OnClick="eliminarLinkButtonMat_Click" >Eliminar</asp:LinkButton>
       <asp:LinkButton ID="nuevoLinkButton" runat="server" Width="50" OnClick="nuevoLinkButtonMat_Click" >Nuevo</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="formActionsPanel" runat="server">
       <asp:LinkButton ID="aceptarLinkButton" runat="server" Width="70" OnClick="aceptarLinkButtonMat_Click" >Aceptar</asp:LinkButton>
       <asp:LinkButton ID="cancelarLinkButton" runat="server" Width="70" CausesValidation="false" OnClick="cancelarLinkButtonMat_Click" >Cancelar</asp:LinkButton>
    </asp:Panel>
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
   </section>
</asp:Content>