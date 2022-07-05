<%@ Page Culture="es-AR" UICulture="es-AR" Language="C#" AutoEventWireup="true" CodeBehind="UsuarioOlvideClave.aspx.cs" Inherits="WebApplication.app.Seguridad.UsuarioOlvideClave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contraseña</title>

    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link   href="/Content/bootstrap.min.css" rel="stylesheet" />

    <script src="<%=ResolveUrl("~")%>Scripts/jquery-3.0.0.min.js"></script>
    <script src="<%=ResolveUrl("~")%>Scripts/umd/popper.min.js"></script>
    <script src="<%=ResolveUrl("~")%>Scripts/bootstrap.min.js"></script>
    <link   href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"  rel="stylesheet">
    <link   href="<%=ResolveUrl("~")%>Content/CasaC.css" rel="stylesheet" />
    <script type="text/javascript">  

        (function () {
            'use strict';
            window.addEventListener('load', function () {
                var form = document.getElementById('form1');
                form.addEventListener('submit', function (event) {
                    if (form.checkValidity() === false) {
                        event.preventDefault();
                        event.stopPropagation();
                    }
                    form.classList.add('was-validated');
                }, false);
            }, false);
        })();
    </script>
</head>
<body>
    <form id="form1" runat="server" novalidate>
        <div class="container text-center mt-4">
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
            <div id="divPpal" runat="server">
                <div>
                    <img src='<%=ResolveUrl("~") %>img/logo.png' />
                </div>
                <div class="mt-3 mb-3">
                    <h4 runat="server" id="h1">Olvido de contraseña</h4>
                </div>
                <div class="m-auto w-75 text-center" >
                    <asp:DropDownList ID="ddlServidores" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>

                <div class="text-left mt-2">
                    <div style="background-color: lightgray; width: 75%" class="p-2 pt-4 mt-4; m-auto">
                        <div class="form-group row">
                            <label for="txtEmail" class="col-sm-4 col-form-label text-sm-left text-md-right text-nowrap">Ingrese su Email</label>
                            <div class="col-sm-6">
                                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" required="required" />
                                <div class="invalid-feedback">Email obligatorio</div>
                            </div>
                        </div>

                        <div class="form-group row">
                            <label for="btnSubmit" class="col-sm-2 col-form-label text-sm-left text-md-right"></label>
                            <div class="col-sm-8">
                                <asp:Button Text="Recuperar contraseña" runat="server" ID="btnSubmit" ClientIDMode="Static" CssClass="btn btn-primary mt-1" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
