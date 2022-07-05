<%@ Page Title="Listar Usuarios" Culture="es-AR" UICulture="es-AR"  Language="C#" MasterPageFile="~/app/app.Master" AutoEventWireup="true" CodeBehind="UsuarioBrowse.aspx.cs" Inherits="WebApplication.app.Seguridad.UsuarioBrowse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script>
function ConfirmaBorrado(e)
{
    //o = document.getElementById(e.id);
    anchor = "#" + e.id;
    if ($(anchor).attr("disabled") == "disabled") return false;
    resp = confirm('¿Está seguro de borrar este usuario?');
    if (resp == true)
    {
        //$(anchor).text("Borrando...");
        $(anchor).attr("disabled", "disabled");
        $(anchor).prenventDefault();
        return true;
    }
    else return false;
}
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-8">
            <h4 class="ml-3 mb-4">Listado de usuarios</h4>
        </div>
        <div class="col-4 text-right">
            <asp:Button ID="btnNuevoUsuario" Text="Nuevo Usuario" runat="server" CssClass="btn btn-primary" OnClick="btnNuevoUsuario_Click" />
        </div>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="grdUsuarios" CssClass="table table-striped table-bordered table-hover table-sm" runat="server" AutoGenerateColumns="false" OnRowCommand="grdUsuarios_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center columna-iconos-th" >
                    <ItemStyle HorizontalAlign="Center" CssClass="verticalMiddle columna-iconos-td" />
                    <ItemTemplate>
                        <div class="columna-iconos-gridview">
                            <div>
                                <asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="CommandNameDelete" Text="" ToolTip="Borrar" CssClass=" ml-2 mr-2" OnClientClick="return ConfirmaBorrado(this);"><i class="fa fa-trash" ></i></asp:LinkButton>
                            </div>
                            <div>
                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CommandName="CommandNameEdit" Text="" ToolTip="Modificar" CssClass=""><i class="fa fa-pencil" ></i></asp:LinkButton>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UsuarioId" HeaderText="UsuarioId" HeaderStyle-CssClass="invisible" ItemStyle-CssClass="invisible" />
                <asp:BoundField DataField="LoginName" HeaderText="Usuario" Visible="true" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" Visible="true" />
                <asp:BoundField DataField="Email" HeaderText="Email" Visible="true" />
                <asp:TemplateField HeaderText="Habilitado">
                    <ItemTemplate>
                        <asp:CheckBox ID="Habilitado" runat="server" Enabled="false" Checked='<%# (int)Eval("EstadoId") == 1 %>'></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <div class="nohaydatos">No hay datos.</div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
</asp:Content>
