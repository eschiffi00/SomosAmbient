<%@ Page Title="" Language="C#" MasterPageFile="~/app/app.Master" AutoEventWireup="true" CodeBehind="RecetaBrowse.aspx.cs" Inherits="WebApplication.app.StockNS.RecetaBrowse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function ConfirmaBorrado(e) {
            //o = document.getElementById(e.id);
            anchor = "#" + e.id;
            if ($(anchor).attr("disabled") == "disabled") return false;
            resp = confirm('¿Está seguro de borrar esta Receta?');
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
            <h4 class="ml-3 mb-4">Listado de Recetas &nbsp;
                <i class="fa fa-search searchicon faborde"></i>
                <asp:LinkButton CssClass="LnkBtnExportar" runat="server" ID="btnExportar" OnClick="btnExportar_Click"><i class="fa fa-download exporticon faborde" title="Exportar Recetas"></i></asp:LinkButton>
            </h4>
        </div>
        <div class="col-4 text-right">
            <asp:Button ID="btnNuevoReceta" Text="Nueva Receta" runat="server" CssClass="btn btn-primary" OnClick="btnNuevoReceta_Click" />
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
        <asp:GridView ID="grdRecetas" CssClass="table table-striped table-bordered table-hover table-sm" runat="server" AutoGenerateColumns="false" >
            <Columns>
                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center columna-iconos-th">
                    <ItemStyle HorizontalAlign="Center" CssClass="verticalMiddle columna-iconos-td" />
                    <ItemTemplate>
                        <%--<div class="d-flex justify-content-between w-75 text-center mr-1">--%>
                        <div class="columna-iconos-gridview">
                            <div>
                                <asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="False" CommandName="CommandNameDelete" Text="" ToolTip="Borrar Receta" CssClass="ml-2 mr-2" OnClientClick="return ConfirmaBorrado(this);"><i class="fa fa-trash" ></i></asp:LinkButton>
                            </div>
                            <div>
                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CausesValidation="False" CommandName="CommandNameEdit" Text="" ToolTip="Modificar" CssClass="ml-2 mr-2" ><i class="fa fa-pencil" ></i></asp:LinkButton>
                            </div>
                            <%--<div>
                                <asp:LinkButton ID="LinkButtonTarifa" runat="server" CausesValidation="False" CommandName="CommandNameTarifa" Text="" ToolTip="Tarifas" CssClass="mr-2"><i class="fa fa-usd" ></i></asp:LinkButton>
                            </div>--%>
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="invisible" ItemStyle-CssClass="invisible" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" Visible="true" />
                <asp:BoundField DataField="StockID" HeaderText="Stock" Visible="false" />
                <asp:BoundField DataField="Peso" HeaderText="Stock Peso" Visible="true" />
                <asp:BoundField DataField="Cantidad" HeaderText="Stock Unidad" Visible="true" />

               
              
            </Columns>
            <EmptyDataTemplate>
                <div class="nohaydatos">No hay datos.</div>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
</asp:Content>
