<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralError.aspx.cs" Inherits="WebApplication.GeneralError" %>

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
        $(document).ready(function () {
            $("#divAlerta").click(function () {
                $("#divDetalle").css('display', 'inline');
            })
        });
    </script>
</head>
<body class="text-center">
    <form id="form1" runat="server">
        <div class="container text-center">
            <!--#region Alterta -->
            <div runat="server" id="divAlerta" class="alert alert-danger" style="margin-top: 20px !important;">
                <div>
                    <asp:Label ID="lblAlerta" runat="server" Text=""></asp:Label>
                </div>
                <div id="divDetalle" style="display:none">
                    <asp:TextBox ID="txtDetalle" runat="server" style="color:#f8d7da; background-color:#f8d7da; width: 100%" Text="" Enabled="false"  TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <!--#endregion Alerta -->

            <div>
                <a href="javascript:history.back()" class="" >Volver a la página anterior</a>
            </div>
            <div>
                <a href="~/" class="" >Reiniciar el sistema</a>
            </div>
        </div>
    </form>
</body>
</html>
