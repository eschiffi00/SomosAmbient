<%@ Page Title="" Language="C#" MasterPageFile="~/app/app.Master" AutoEventWireup="true" CodeBehind="RecetaEdit.aspx.cs" Inherits="WebApplication.app.StockNS.RecetaEdit"%>
<asp:Content id="Content1" ContentPlaceHolderID="head" runat="server">
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

        //function EnforceFieldLengthMax(txt,event)
        //{
        //    const array = txt.value.split("\r\n");

        //    if (array[array.length - 1].length > 99 && event.keyCode !== 13)
        //    {
        //        txt.value = txt.value + "\r\n";
        //    }
        //}

    </script>
</asp:Content>
<asp:Content id="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

  <!--#region Alterta -->
    <div runat="server" id="divAlerta" class="row alert alert-danger invisible">
        <div class="col-md-11">
            <asp:Label id="lblAlerta" runat="server" Text=""></asp:Label>
        </div>
        <div class="col-md-1 text-right mb-5">
            <asp:LinkButton id="btnCloseAlerta" CssClass="close" runat="server" OnClick="btnCloseAlerta_Click">x</asp:LinkButton>
        </div>
    </div>
    <!--#endregion Alerta -->


    <!--#region divPpal -->
    <div runat="server" id="divPpal">
        <div class="row">
            <h4 runat="server" id="h4Titulo" class="ml-3 mb-4">Modificación de Receta</h4>
        </div>
            <%--onrowcancelingedit="GridView1_RowCancelingEdit" 
             onrowupdating="GridView1_RowUpdating">--%>
        <%--onrowediting="GridView1_RowEditing"--%>
        <div id="divDescripcion" class="form-group row">
            <label for="txtDescripcion" class="col-sm-2 col-form-label text-sm-left text-md-right">Descripcion</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control inputStock" placeholder="Descripcion de la Receta" required="required" />
                <div class="invalid-feedback">Debe ingresar una descripcion para la Receta</div>
            </div>
        </div>
        <div id="divPeso" class="form-group row">
            <label for="txtPeso" class="col-sm-2 col-form-label text-sm-left text-md-right">Peso</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="txtPeso" CssClass="form-control inputStock" placeholder="Ingrese el Stock" />        
            </div>
        </div>
        <div id="divCantidad" class="form-group row">
            <label for="txtCantidad" class="col-sm-2 col-form-label text-sm-left text-md-right">Cantidad</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" ID="txtCantidad" CssClass="form-control inputStock" placeholder="Ingrese el Stock" />        
            </div>
         </div>
        <div class="col-sm-2 col-form-label text-sm-left text-md-right">
            <asp:Button id="ButtonAdd" runat="server" class="btnblack btn col-form-label text-sm-left text-md-right" Text="Nuevo Item" OnClick="ButtonAdd_Click" />
        </div>
        <div id="divGrid" class="form-group row col-sm-3 col-form-label text-sm-left text-md-right">
        <asp:GridView id="GridView1" runat="server" AutoGenerateColumns="False" 
                 AutoGenerateEditButton="True" CellPadding="4" ForeColor="#000000" GridLines="None"
                onrowdatabound="GridView1_RowDataBound">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:TemplateField HeaderText="Item">
                    <ItemTemplate>
                        <asp:DropDownList  id="ddlItems" runat="server" datatextfield="Descripcion" datavaluefield="Descripcion" style="height:30px" ></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cantidad">
                    <ItemTemplate>
                        <asp:TextBox id="Cantidad" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Peso">
                    <ItemTemplate>
                        <asp:TextBox id="Peso" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle BackColor="Black" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="Black" Font-Bold="True" ForeColor="#D1DDF1" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
            <EditRowStyle BackColor="#0076ff" />
            <AlternatingRowStyle BackColor="White" />
         </asp:GridView>
        </div>
        
        <div id="divPasos" class="form-group row">
            <label for="TextBoxReceta" class="col-sm-2 col-form-label text-sm-left text-md-right">Pasos</label>
            <div class="col-sm-6">
                <asp:TextBox runat="server" TextMode="MultiLine" ID="TextBoxReceta" CssClass="form-control" 
                    AcceptsReturn ="true" VerticalScrollBarVisibility="Visible" TextWrapping="Wrap" 
                    placeholder="Ingrese aqui los pasos de su receta" 
                    Width="600px" Height="200px" />        
            </div>
        </div>
        <div class="form-group row">
            <label for="btnSubmit" class="col-sm-2 col-form-label text-sm-left text-md-right"></label>
            <div class="col-sm-4">
                <a href="<%$RouteUrl:routename=ListaRecetas%>" class="btn btncancel mt-1" runat="server">Cancelar</a>
                <asp:Button Text="Crear Receta" runat="server" ID="btnSubmit" ClientIDMode="Static" CssClass="btn btnsubmit mt-1" OnClick="btnSubmit_Click" />
            </div>
        </div>

    </div>
    <!--#endregion divPpal -->
</asp:Content>
<asp:Content id="Content3" ContentPlaceHolderID="foot" runat="server">
</asp:Content>
