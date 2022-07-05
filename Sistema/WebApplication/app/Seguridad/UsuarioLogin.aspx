<%@ Page Title="Ingreso al sistema" Culture="es-AR" UICulture="es-AR"  Language="C#" AutoEventWireup="true" CodeBehind="UsuarioLogin.aspx.cs" Inherits="WebApplication.app.Seguridad.UsuarioLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bondi - Ingreso al Sistema</title>

    <meta name="viewport" content="width=device-width, initial-scale=1">

    <link   href="/Content/bootstrap.min.css" rel="stylesheet" />

    <script src="<%=ResolveUrl("~")%>Scripts/jquery-3.0.0.min.js"></script>
    <script src="<%=ResolveUrl("~")%>Scripts/umd/popper.min.js"></script>
    <script src="<%=ResolveUrl("~")%>Scripts/bootstrap.min.js"></script>
    <link   href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"  rel="stylesheet" />
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
        <div class="container-flex text-center">
             <!--#region Alterta -->
            <div runat="server" id="divAlerta" class="row alert alert-danger w-75 invisible" style="margin-top: 20px; margin-left: auto; margin-right: auto">
                <div class="col-md-11">
                    <asp:Label ID="lblAlerta" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-md-1 text-right mb-1">
                    <asp:LinkButton ID="btnCloseAlerta" CssClass="close" runat="server" OnClick="btnCloseAlerta_Click">x</asp:LinkButton>
                </div>
            </div>
            <!--#endregion Alerta -->  

            <div runat="server" id="divPpal">
                <div>
                    <img src='<%=ResolveUrl("~") %>img/logo.png' />
                </div>
                <div class="mt-3 mb-3">
                    <h4 runat="server" id="h4Titulo">Iniciar sesión</h4>
                </div>
<%--                <div class="m-auto w-75 text-center" >
                    <asp:DropDownList ID="ddlServidores" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0" Text="<--Seleccione Edificio-->"></asp:ListItem>
                    </asp:DropDownList>
                </div>--%>
                <div class="text-left mt-2">
                    <div style="background-color: lightgray; width: 75%" class="p-2 pt-4 mt-4 m-auto">
                        <div class="form-group row">
                            <label for="lblLogin" class="col-sm-2 col-form-label text-sm-left text-md-right">Login</label>
                            <div class="col-sm-8">
                                <asp:TextBox runat="server" ID="txtLogin" CssClass="form-control" required="required" />
                                <div class="invalid-feedback">Usuario requerido</div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="txtPassword" class="col-sm-2 col-form-label text-sm-left text-md-right">Clave</label>
                            <div class="col-sm-8">
                                <asp:TextBox runat="server" TextMode="SingleLine" ID="txtPassword" ClientIDMode="Static" CssClass="form-control" required="required" />
                                <div class="invalid-feedback">Clave requerida</div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <label for="btnSubmit" class="col-sm-2 col-form-label text-sm-left text-md-right"></label>
                            <div class="col-sm-4">
                                <asp:Button Text="Ingresar" runat="server" ID="btnSubmit" ClientIDMode="Static" CssClass="btn btn-primary mt-1" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="m-auto w-50">
                        <a target="_blank" href="<asp:Literal runat="server" Text="<%$RouteUrl:routename=OlvideClave %>" />">Olvidé la contraseña</a>
                        
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
