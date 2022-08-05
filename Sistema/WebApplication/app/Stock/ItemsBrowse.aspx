<%@ Page Title="" Language="C#" MasterPageFile="~/app/app.Master" AutoEventWireup="true" CodeBehind="ItemsBrowse.aspx.cs" Inherits="WebApplication.app.StockNS.ProductoBrowse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function ConfirmaBorrado(e) {
            //o = document.getElementById(e.id);
            anchor = "#" + e.id;
            if ($(anchor).attr("disabled") == "disabled") return false;
            resp = confirm('¿Está seguro de borrar este Producto?');
            if (resp == true) {
                //$(anchor).text("Borrando...");
                $(anchor).attr("disabled", "disabled");
                $(anchor).prenventDefault();
                return true;
            }
            else return false;
        }

        $(document).ready(function () {
            if ($("#searchpanelstate").val() == "invisible") $("#searchpanel").addClass('invisible');
            else $("#searchpanel").removeClass('invisible');

            $(".searchicon").click(function (event) {
                if ($("#searchpanel").hasClass("invisible")) {
                    $("#searchpanel").removeClass('invisible');
                    $("#searchpanelstate").val("visible");
                }
                else {
                    $("#searchpanel").addClass('invisible');
                    $("#searchpanelstate").val("invisible");
                }
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:TextBox runat="server" ID="searchpanelstate" Text="invisible" CssClass="invisible" ClientIDMode="Static"></asp:TextBox>
    <div class="row">
        <div class="col-8">
            <h4 class="ml-3 mb-4">Listado de Productos &nbsp;
                <i class="fa fa-search searchicon faborde"></i>
                <asp:LinkButton CssClass="LnkBtnExportar" runat="server" ID="btnExportar" OnClick="btnExportar_Click"><i class="fa fa-download exporticon faborde" title="Exportar Productos"></i></asp:LinkButton>
            </h4>
        </div>
        <div class="col-4 text-right">
            <asp:Button ID="btnNuevoProducto" Text="Nuevo Producto" runat="server" CssClass="btn btn-primary" OnClick="btnNuevoItems_Click" />
        </div>
    </div>
    <div id="searchpanel" class="row invisible mb-2">
        <div class="col-3">
            <asp:TextBox runat="server" ID="txtBuscar" TextMode="Search" CssClass="form-control" placeholder="Codigo, Nombre, R.Social" OnTextChanged="txtBuscar_TextChanged" AutoPostBack="true" />
        </div>
        <div class="col-3">
        </div>
    </div>
    <div class="table-responsive">
        <asp:GridView ID="grdItems" CssClass="table table-striped table-bordered table-hover table-sm" runat="server" AutoGenerateColumns="false" >
            <Columns>
                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center columna-iconos-th">
                    <ItemStyle HorizontalAlign="Center" CssClass="verticalMiddle columna-iconos-td" />
                    <ItemTemplate>
                        <%--<div class="d-flex justify-content-between w-75 text-center mr-1">--%>
                        <div class="columna-iconos-gridview">
                            <div>
                                <asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="CommandNameDelete" Text="" ToolTip="Borrar cliente" CssClass="ml-2 mr-2" OnClientClick="return ConfirmaBorrado(this);"><i class="fa fa-trash" ></i></asp:LinkButton>
                            </div>
                            <div>
                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CommandName="CommandNameEdit" Text="" ToolTip="Modificar" CssClass="ml-2 mr-2" OnClick="LinkButtonEdit_Click" ><i class="fa fa-pencil" ></i></asp:LinkButton>
                            </div>
                            <%--<div>
                                <asp:LinkButton ID="LinkButtonTarifa" runat="server" CausesValidation="False" CommandName="CommandNameTarifa" Text="" ToolTip="Tarifas" CssClass="mr-2"><i class="fa fa-usd" ></i></asp:LinkButton>
                            </div>--%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Id" HeaderText="ID" HeaderStyle-CssClass="invisible" ItemStyle-CssClass="invisible" />
                <asp:BoundField DataField="Detalle" HeaderText="Detalle" Visible="true" />
                <asp:BoundField DataField="CategoriaItemId" HeaderText="Categoria" Visible="false" />
                <asp:BoundField DataField="CategoriaDescripcion" HeaderText="Categoria" Visible="true" />
                <asp:BoundField DataField="Costo" HeaderText="Costo" Visible="true" />
                <asp:BoundField DataField="Margen" HeaderText="Margen" Visible="true" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" Visible="true" />
                <asp:BoundField DataField="DepositoId" HeaderText="Stock" Visible="false" />
                <asp:BoundField DataField="Unidad" HeaderText="Unidad" Visible="true" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" Visible="true" />
                <asp:BoundField DataField="Estado" HeaderText="Habilitado" Visible="true" />
               
              
            </Columns>
            <EmptyDataTemplate>
                <div class="nohaydatos">No hay datos.</div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
</asp:Content>
