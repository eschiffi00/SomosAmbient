﻿<%@ Page Title="" Language="C#" MasterPageFile="~/app/app.Master" AutoEventWireup="true" CodeBehind="ItemsEdit.aspx.cs" Inherits="WebApplication.app.StockNS.ItemsEdit"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            document.getElementById("Itemcoleccion").style.display = 'none';
            var chkdetalle = document.querySelector('[id$="chkDetalle"]');
            chkdetalle.addEventListener('change', CheckDetalle);
                
            CheckDetalle();
            //if (document.querySelector('#divPeso input').value != "") {
            //    document.getElementById("divPeso").style.display = 'flex';
            //    document.getElementById("Peso").checked = true;
            //} else if (document.querySelector('#divCantidad input').value != "") {
            //    document.getElementById("divCantidad").style.display = 'flex';
            //    document.getElementById("Cantidad").checked = true;
            //}
        };
        function CheckDetalle() {
            var chkboxid = document.querySelector('[id$="chkDetalle"]').id;
            if (document.getElementById(chkboxid).checked) {
                document.getElementById("Itemcoleccion").style.display = 'flex';
                document.getElementById("categorias").style.display = 'none';
                document.getElementById("cuentas").style.display = 'none';

            } else {
                document.getElementById("Itemcoleccion").style.display = 'none';
                document.getElementById("categorias").style.display = 'flex';
                document.getElementById("cuentas").style.display = 'flex';
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

  <!--#region Alterta -->
    <div runat="server" id="divAlerta" class="row alert alert-danger invisible">
        <div class="col-md-11">
            <asp:Label ID="lblAlerta" runat="server" Text=""></asp:Label>
        </div>
        <div class="col-md-1 text-right mb-5">
            <asp:LinkButton ID="btnCloseAlerta" CssClass="close" runat="server" OnClick="btnCloseAlerta_Click">x</asp:LinkButton>
        </div>
    </div>
    <!--#endregion Alerta -->


    <!--#region divPpal -->
    <div runat="server" id="divPpal">
        <div class="row">
            <h4 runat="server" id="h4Titulo" class="ml-3 mb-4">Modificación de Items</h4>
        </div>

        <div class="form-group row">
            <label for="txtDescripcion" class="col-sm-2 col-form-label text-sm-left text-md-right">Descripcion</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" TabIndex="1" placeholder="Descripcion del Producto" required="required" />
                <div class="invalid-feedback">Debe ingresar una descripcion para el Item</div>
            </div>
        </div>
        <div>
            <div class="form-group row">
                <label for="chkDetalle" class="col-sm-2 col-form-label text-sm-left text-md-right">Collección</label>
                <div class="col-sm-6">
                     <asp:CheckBox ID="chkDetalle" CssClass="customCheck" runat="server" Enabled="true" Checked='<%# (int)Eval("ItemDetalleId") > 0 %>'></asp:CheckBox>        
                </div>
            </div>
            <div class="form-group row" id="Itemcoleccion">
                <label for="ddlItems" class="col-sm-2 col-form-label text-sm-left text-md-right">Item Collección</label>
                <div class="col-sm-4">
                    <asp:DropDownList  id="ddlItems" runat="server" datatextfield="Descripcion" datavaluefield="Descripcion" style="height:30px" ></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="form-group row" id="categorias">
            <label for="ddlCategoriaId" class="col-sm-2 col-form-label text-sm-left text-md-right">Categoria</label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlCategoriaId" ClientIDMode="Static" TabIndex="2" CssClass="form-control mt-1"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group row" id="cuentas">
            <label for="ddlCuenta" class="col-sm-2 col-form-label text-sm-left text-md-right">Cuenta Contable</label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlCuenta" ClientIDMode="Static" TabIndex="2" CssClass="form-control mt-1"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group row">
            <label for="txtCosto" class="col-sm-2 col-form-label text-sm-left text-md-right">Costo</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="txtCosto" TabIndex="3" CssClass="form-control" placeholder="Ingrese el Costo" required="required" />        
            </div>
        </div>
        <div class="form-group row">
            <label for="txtMargen" class="col-sm-2 col-form-label text-sm-left text-md-right">Margen</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="txtMargen" TabIndex="4" CssClass="form-control" placeholder="Ingrese el Margen" required="required" />        
            </div>
        </div>
        <div class="form-group row">
            <label for="txtPrecio" class="col-sm-2 col-form-label text-sm-left text-md-right">Precio</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="txtPrecio" TabIndex="5" CssClass="form-control" placeholder="Ingrese el Precio" required="required" />        
            </div>
        </div>
        <div class="form-group row">
            <label for="ddlUnidad" class="col-sm-2 col-form-label text-sm-left text-md-right">Unidad</label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlUnidad" TabIndex="6" ClientIDMode="Static" CssClass="form-control mt-1"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group row">
            <label for="txtCantidad" class="col-sm-2 col-form-label text-sm-left text-md-right">Cantidad</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="txtCantidad" TabIndex="7" CssClass="form-control" placeholder="Ingrese el Stock" required="required" />        
            </div>
        </div>
        <div class="form-group row">
            <label for="ddlEstado" class="col-sm-2 col-form-label text-sm-left text-md-right">Estado</label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlEstado" TabIndex="8" ClientIDMode="Static" CssClass="form-control mt-1">
                    <asp:ListItem Text="Habilitado" Value="36" Selected="True" />
                    <asp:ListItem Text="Deshabilitado" Value="37" />
                </asp:DropDownList>
            </div>
        </div>

        <div class="form-group row">
            <label for="btnSubmit" class="col-sm-2 col-form-label text-sm-left text-md-right"></label>
            <div class="col-sm-4">
                <a href="<%$RouteUrl:routename=ListaItems%>" class="btn btncancel mt-1" TabIndex="9" runat="server">Cancelar</a>
                <asp:Button Text="Crear Item" runat="server" ID="btnSubmit" ClientIDMode="Static" TabIndex="10" CssClass="btn btnsubmit mt-1" OnClick="btnSubmit_Click" />
            </div>
        </div>

    </div>
    <!--#endregion divPpal -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
</asp:Content>
