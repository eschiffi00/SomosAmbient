<%@ Page Title="Editar Usuario" Culture="es-AR" UICulture="es-AR" Language="C#" MasterPageFile="~/app/app.Master" AutoEventWireup="true" CodeBehind="UsuarioEdit.aspx.cs" Inherits="WebApplication.app.Seguridad.UsuarioEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            <h4 runat="server" id="h4Titulo" class="ml-3 mb-4">Modificación de usuario</h4>
        </div>

        <div class="form-group row">
            <label for="txtLogin" class="col-sm-2 col-form-label text-sm-left text-md-right">Login</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtLogin" CssClass="form-control" placeholder="Ingrese el Login name de 8 caracteres o más" required="required" />
                <div class="invalid-feedback">Por favor ingrese el login</div>
            </div>
        </div>
        <div class="form-group row">
            <label for="txtNombre" class="col-sm-2 col-form-label text-sm-left text-md-right">Nombre</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" placeholder="Ingrese el Nombre completo" required="required" />
                <div class="invalid-feedback">Por favor ingrese el Nombre y Apellido del usuario</div>
            </div>
        </div>
        <div class="form-group row">
            <label for="txtEmail" class="col-sm-2 col-form-label text-sm-left text-md-right">Email</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtEmail" TextMode="Email" CssClass="form-control" placeholder="Ingrese el Email" AutoCompleteType="Email" required="required" />
                <div class="invalid-feedback">Por favor ingrese el mail correcto del usuario</div>
            </div>
        </div>
        <div runat="server" id="divPasswords">
            <div class="form-group row">
                <label for="txtPassword1" class="col-sm-2 col-form-label text-sm-left text-md-right">Clave</label>
                <div class="col-sm-8">
                    <asp:TextBox runat="server" TextMode="Password" ID="txtPassword1" ClientIDMode="Static" CssClass="form-control" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}" placeholder="Clave" required="required"/>
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
        </div>
        <div runat="server" id="divNroFallos">
            <div class="form-group row">
                <label for="txtNroFallos" class="col-sm-2 col-form-label text-sm-left text-md-right">Nro de fallos</label>
                <div class="col-4">
                    <asp:Label runat="server" ID="lblNroFallos" CssClass="form-control" ReadOnly="true"/>
                </div>
                <div class="col-4">
                    <asp:Button Text="Reiniciar Fallos" runat="server" ID="btnVuelveACero" CssClass="btn btn-sm btn-outline-dark mt-1 mr-2" UseSubmitBehavior="false" OnClick="btnVuelveACero_Click" />
                    <asp:Button Text="Cambiar Clave" runat="server" ID="btnCambiaClave" CssClass="btn btn-sm btn-outline-dark mt-1" UseSubmitBehavior="false" OnClick="btnCambiaClave_Click" />
                </div>
            </div>
        </div>

        <div class="form-group row">
            <label for="ddlEsAdmin" class="col-sm-2 col-form-label text-sm-left text-md-right">Es admin</label>
            <div class="col-sm-4">
                <asp:DropDownList runat="server" ID="ddlEsAdmin" ClientIDMode="Static" CssClass="form-control mt-1">
                    <asp:ListItem Text="No" Value="0" Selected="True" />
                    <asp:ListItem Text="Si" Value="1" />
                </asp:DropDownList>
            </div>
        </div>

        <div class="form-group row" id="divFeCreacion" runat="server">
            <label for="txtFeCreacion" class="col-sm-2 col-form-label text-sm-left text-md-right">FeCreacion</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtFeCreacion" CssClass="form-control" Enabled="false" />
            </div>
        </div>
        <div class="form-group row" id="divCreadoPor" runat="server">
            <label for="txtCreadoPor" class="col-sm-2 col-form-label text-sm-left text-md-right">Creado por</label>
            <div class="col-sm-8">
                <asp:TextBox runat="server" ID="txtCreadoPor" CssClass="form-control" Enabled="false" />
            </div>
        </div>

        <div class="form-group row">
            <label for="btnSubmit" class="col-sm-2 col-form-label text-sm-left text-md-right"></label>
            <div class="col-sm-4">
                <a href="<%$RouteUrl:routename=ListaUsuarios %>" class="btn btn-primary mt-1" runat="server">Cancelar</a>
                <asp:Button Text="Crear Usuario" runat="server" ID="btnSubmit" ClientIDMode="Static" CssClass="btn btn-primary mt-1" OnClick="btnSubmit_Click" />
            </div>
        </div>

    </div>
    <!--#endregion divPpal -->




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="server">
</asp:Content>
