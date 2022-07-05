<%@ Page Title="Editar Rol" Culture="es-AR" UICulture="es-AR"  Language="C#" MasterPageFile="~/app/app.Master" AutoEventWireup="true" CodeBehind="UsuarioRolEdit.aspx.cs" Inherits="WebApplication.app.Seguridad.UsuarioRolEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--#region divPpal -->
    <div runat="server" id="divPpal">
        <div class="row">
            <h4 runat="server" id="h4Titulo" class="ml-3 mb-4">Permisos de Usuarios</h4>
        </div>
        <div class="row">
            <div class="form-group row mx-auto col-12 col-sm-8 text-left">
                <label for="ddlUsuarios" class="col12 col-sm-2 mt-2">Usuario</label>
                <div class="col-12 col-sm-8 text-left">
                    <asp:DropDownList runat="server" ID="ddlUsuarios" CssClass="form-control mt-1" OnSelectedIndexChanged="ddlUsuarios_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        
        <div class="form-group row text-center">
            <div class="col-12 col-sm-8 mx-auto">
                <asp:GridView runat="server" ID="grdRoles" CssClass="table table-striped table-bordered table-hover table-sm" AutoGenerateColumns="false" OnRowDataBound="grdRoles_RowDataBound" >
                
                    <Columns>
                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" >
                            <ItemStyle HorizontalAlign="Center" CssClass="verticalMiddle" />
                            <ItemTemplate>
                                <asp:CheckBox ID="cbPermiso" runat="server" CssClass="form-check" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RolId" HeaderText="RolId" HeaderStyle-CssClass="invisible" ItemStyle-CssClass="invisible" />
                        <asp:BoundField DataField="Nombre" HeaderText="Rol" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="row">
            <div class="form-group row mx-auto col-12 col-sm-8 text-left">
                <div class="col-12 col-sm-8 text-left">
                    <%--<a href="javascript:history.back()" class="btn btn-primary">Cancelar</a>--%>
                    <a href="/main" class="btn btn-primary">Cancelar</a>
                    <asp:Button Text="Guardar" runat="server" ID="btnSubmit" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                </div>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
</asp:Content>
