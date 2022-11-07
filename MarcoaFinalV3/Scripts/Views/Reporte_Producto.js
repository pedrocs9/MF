$(document).ready(function () {
   

    //OBTENER TIENDAS
    jQuery.ajax({
        url: $.MisUrls.url._ObtenerRestaurantes,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            $("#cboTienda").LoadingOverlay("hide");
            $("#cboTienda").html("");

            $("<option>").attr({ "value": 0 }).text("-- Seleccionar todas--").appendTo("#cboTienda");
            if (data.data != null)
                $.each(data.data, function (i, item) {

                    if (item.Activo == true) {
                        $("<option>").attr({ "value": item.IdRestaurant }).text(item.Nombre).appendTo("#cboTienda");
                    }
                })
        },
        error: function (error) {
            console.log(error)
        },
        beforeSend: function () {
            $("#cboTienda").LoadingOverlay("show");
        },
    });

})


$('#btnBuscar').on('click', function () {

    jQuery.ajax({
        url: $.MisUrls.url._ObtenerReporteProducto + "?idrestaurant=" + $("#cboTienda").val() + "&codigoproducto=" + $("#txtCodigoProducto").val(),
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            if (data != undefined && data != null) {

                $("#tbReporte tbody").html("");


                $.each(data, function (i, row) {

                    $("<tr>").append(
                        $("<td>").text(row["RucRestaurant"]),
                        $("<td>").text(row["NombreRestaurant"]),
                        $("<td>").text(row["DireccionRestaurant"]),
                        $("<td>").text(row["CodigoProducto"]),
                        $("<td>").text(row["NombreProducto"]),
                        $("<td>").text(row["DescripcionProducto"]),
                        $("<td>").text(row["StockenRestaurant"]),
                        $("<td>").text(row["PrecioCompra"]),
                        $("<td>").text(row["PrecioVenta"])

                    ).appendTo("#tbReporte tbody");

                })

            }

        },
        error: function (error) {
            console.log(error)
        },
        beforeSend: function () {
        },
    });

})

function printData() {

    if ($('#tbReporte tbody tr').length == 0) {
        swal("Mensaje", "No existen datos para imprimir", "warning")
        return;
    }

    var divToPrint = document.getElementById("tbReporte");

    var style = "<style>";
    style = style + "table {width: 100%;font: 17px Calibri;}";
    style = style + "table, th, td {border: solid 1px #DDD; border-collapse: collapse;";
    style = style + "padding: 2px 3px;text-align: center;}";
    style = style + "</style>";

    newWin = window.open("");


    newWin.document.write(style);
    newWin.document.write("<h3>Reporte de productos por restaurant</h3>");
    newWin.document.write(divToPrint.outerHTML);
    newWin.print();
    newWin.close();
}