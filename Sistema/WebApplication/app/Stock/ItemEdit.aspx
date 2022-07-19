﻿<%@ Page Title="" Language="C#" MasterPageFile="~/app/app.Master" AutoEventWireup="true" CodeBehind="ItemEdit.aspx.cs" Inherits="WebApplication.app.StockNS.ItemEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        window.onload = function () {
            document.getElementById("divPeso").style.display = 'none';
            document.getElementById("divCantidad").style.display = 'none';
            CreaRadios();
            if (document.querySelector('#divPeso input').value != "") {
                document.getElementById("divPeso").style.display = 'flex';
                document.getElementById("Peso").checked = true;
            } else if (document.querySelector('#divCantidad input').value != "") {
                document.getElementById("divCantidad").style.display = 'flex';
                document.getElementById("Cantidad").checked = true;
            }
        };

        function CreaRadios() {
            let stockdescripcion = ["Peso", "Cantidad"];
            for (var i = 0; i <= 1; i++) {
                var label = document.createElement('label');
                label.htmlFor = 'tipoStock';
                if (stockdescripcion[i] == "Peso") {
                    label.className = 'col-sm-3 text-sm-left text-md-right radio';
                } else {
                    label.className = 'text-sm-left text-md-right radio';
                }
                    

                var radiobox = document.createElement('input');
                radiobox.type = 'radio';
                radiobox.name = 'radio';
                radiobox.id = stockdescripcion[i];

                label.appendChild(radiobox);


                var span = document.createElement('span');
                span.className = 'checkmark';
                label.appendChild(span);

                var p = document.createElement('span');
                p.className = 'checkmark';
                p.textContent = stockdescripcion[i];
                label.appendChild(p);

                var container = document.getElementById('contStock');

                container.appendChild(label);
            }
            var radioSelect = document.getElementById("Peso");
            radioSelect.addEventListener('click', (event) => {
                document.getElementById("divPeso").style.display = 'flex';
                document.getElementById("divCantidad").style.display = 'none';
                document.querySelector('#divCantidad input').value = ""
            });
            radioSelect = document.getElementById("Cantidad");
            radioSelect.onclick = function () {
                document.getElementById("divPeso").style.display = 'none';
                document.getElementById("divCantidad").style.display = 'flex';
                document.querySelector('#divPeso input').value = ""
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
            <h4 runat="server" id="h4Titulo" class="ml-3 mb-4">Modificación de Item</h4>
        </div>

        <div class="form-group row">
            <label for="txtDescripcion" class="col-sm-2 col-form-label text-sm-left text-md-right">Descripcion</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" placeholder="Descripcion del Item" required="required" />
                <div class="invalid-feedback">Debe ingresar una descripcion para el item</div>
            </div>
        </div>
         <div class="form-group row">
            <label for="ddlCuentaID" class="col-sm-2 col-form-label text-sm-left text-md-right">Cuenta Contable</label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlCuentaID" ClientIDMode="Static" CssClass="form-control mt-1"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group row" id="contStock"></div>
        <div id="divCantidad" class="form-group row">
            <label for="txtCantidad" class="col-sm-2 col-form-label text-sm-left text-md-right">Unidades</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="txtCantidad" CssClass="form-control" placeholder="Ingrese el Stock por Unidad" required="required" />        
            </div>
        </div>
        <div id="divPeso" class="form-group row">
            <label for="txtPeso" class="col-sm-2 col-form-label text-sm-left text-md-right">Peso</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="txtPeso" CssClass="form-control" placeholder="Ingrese el Stock por Peso" required="required" />        
            </div>
        </div>

        <div class="form-group row">
            <label for="btnSubmit" class="col-sm-2 col-form-label text-sm-left text-md-right"></label>
            <div class="col-sm-4">
                <a href="<%$RouteUrl:routename=ListaUsuarios %>" class="btn btn-primary mt-1" runat="server">Cancelar</a>
                <asp:Button Text="Crear Item" runat="server" ID="btnSubmit" ClientIDMode="Static" CssClass="btn btn-primary mt-1" OnClick="btnSubmit_Click" />
            </div>
        </div>

    </div>
    <!--#endregion divPpal -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
</asp:Content>