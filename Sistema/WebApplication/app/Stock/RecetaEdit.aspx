<%@ Page Title="" Language="C#" MasterPageFile="~/app/app.Master" AutoEventWireup="true" CodeBehind="RecetaEdit.aspx.cs" Inherits="WebApplication.app.StockNS.RecetaEdit"%>
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
            <h4 runat="server" id="h4Titulo" class="ml-3 mb-4">Modificación de Receta</h4>
        </div>

        <div class="form-group row">
            <label for="txtDescripcion" class="col-sm-2 col-form-label text-sm-left text-md-right">Descripcion</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" placeholder="Descripcion de la Receta" required="required" />
                <div class="invalid-feedback">Debe ingresar una descripcion para la Receta</div>
            </div>
        </div>
        <asp:Panel class="form-group row" ID="PanelReceta" runat="server"  Height="100%" Width="100%">
   
           
           <br />
           <br />

           <asp:Button ID="btnpanel" runat="server" Text="Button" style="width:82px" />
        </asp:Panel>

        <div class="form-group row" id="contStock"></div>
        <div id="divPeso" class="form-group row">
            <label for="txtPeso" class="col-sm-2 col-form-label text-sm-left text-md-right">Peso</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="txtPeso" CssClass="form-control" placeholder="Ingrese el Stock" />        
            </div>
        </div>
        <div id="divCantidad" class="form-group row">
            <label for="txtCantidad" class="col-sm-2 col-form-label text-sm-left text-md-right">Cantidad</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="txtCantidad" CssClass="form-control" placeholder="Ingrese el Stock" />        
            </div>
        </div>
        <div id="divPasos" class="form-group row">
            <label for="txtPasos" class="col-sm-2 col-form-label text-sm-left text-md-right">Pasos</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" TextMode="MultiLine" ID="TextBoxReceta" CssClass="form-control" placeholder="Ingrese aqui los pasos de su receta" Width="400px" Height="200px" />        
            </div>
        </div>

        <div class="form-group row">
            <label for="btnSubmit" class="col-sm-2 col-form-label text-sm-left text-md-right"></label>
            <div class="col-sm-4">
                <a href="<%$RouteUrl:routename=ListaRecetas%>" class="btn btn-primary mt-1" runat="server">Cancelar</a>
                <asp:Button Text="Crear Receta" runat="server" ID="btnSubmit" ClientIDMode="Static" CssClass="btn btn-primary mt-1" OnClick="btnSubmit_Click" />
            </div>
        </div>

    </div>
    <!--#endregion divPpal -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
</asp:Content>
