<%@ Page Title="Cambio de Clave" Culture="es-AR" UICulture="es-AR"  Language="C#" AutoEventWireup="true" CodeBehind="UsuarioClave.aspx.cs" Inherits="WebApplication.app.Seguridad.UsuarioClave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

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

        //Ejemplo validacion en js
        $(document).ready(function () {
            $("#btnSubmit").click(function (event) {
                var form = document.getElementById('form1');
                if (form.checkValidity() == true) {
                    if ($('#txtPassword1').val() != $('#txtPassword2').val()) {
                        $('#txtPassword2').removeClass('is-valid');
                        $('#txtPassword2').addClass('is-invalid');
                        event.preventDefault();
                        event.stopPropagation();
                    }
                }
            });

        });
        
    </script>  
</head>
<body>
    <form id="form1" runat="server" novalidate>
        <div class="container-flex text-center">
            <div class="mt-5 mb-3">
                <h4 runat="server" id="h4Titulo">Cambio de clave</h4>
            </div>
            <div class="text-left">
                <div style="background-color: lightgray; width: 50%" class="p-2 pt-4 mt-4; m-auto">
                    <div class="form-group row">
                        <label for="txtLogin" class="col-sm-2 col-form-label text-sm-left text-md-right">Login</label>
                        <div class="col-sm-8">
                            <asp:TextBox runat="server" ID="txtLogin" CssClass="form-control" Enabled="false" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txtPassword1" class="col-sm-2 col-form-label text-sm-left text-md-right">Clave</label>
                        <div class="col-sm-8">
                            <%--<asp:TextBox runat="server" TextMode="Password" ID="txtPassword1" ClientIDMode="Static" CssClass="form-control" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}" placeholder="Clave" required="required"/>--%>
                            <asp:TextBox runat="server" TextMode="Password" ID="txtPassword1" ClientIDMode="Static" CssClass="form-control" placeholder="Clave" required="required"/>
                            <div class="invalid-feedback">La Clave debe ser de 8 caracteres o más y al menos una mayúscula y un número</div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="txtPassword2" class="col-sm-2 col-form-label text-sm-left text-md-right">Confirmación</label>
                        <div class="col-sm-8">
                            <asp:TextBox runat="server" TextMode="Password" ID="txtPassword2" ClientIDMode="Static" CssClass="form-control" placeholder="Confirmación de Clave" required="required" />
                            <div class="invalid-feedback">Las Claves no son iguales</div>
                        </div>
                    </div>

                    <div class="form-group row">
                        <label for="btnSubmit" class="col-sm-2 col-form-label text-sm-left text-md-right"></label>
                        <div class="col-sm-8">
                            <a href="javascript:history.back()" class="btn btn-primary mt-1">Volver</a>
                            <asp:Button Text="Cambiar Clave" runat="server" ID="btnSubmit" ClientIDMode="Static" CssClass="btn btn-primary mt-1" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
                <div style="width: 50%" class="p-2 pt-4 mt-4; m-auto">
                    <asp:Label Text="text" runat="server" ID="lblMensaje" style="color:blue" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
