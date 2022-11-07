var tabladata;
var tablarestaurant;
var tablaproducto;


$(document).ready(function () {
    


    ////validamos el formulario
    $("#form").validate({
        rules: {
            Nombre: "required",
            Descripcion: "required"
        },
        messages: {
            Nombre: "(*)",
            Descripcion: "(*)"

        },
        errorElement: 'span'
    });


    //OBTENER RESTAURANTES
    tablarestaurant = $('#tbRestaurant').DataTable({
        "ajax": {
            "url": $.MisUrls.url._ObtenerRestaurantes,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "IdRestaurant", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-sm btn-primary ml-2' type='button' onclick='restaurantSelect(" + JSON.stringify(row) + ")'><i class='fas fa-check'></i></button>"
                },
                "orderable": false,
                "searchable": false,
                "width": "90px"
            },
            { "data": "RUC" },
            { "data": "Nombre" },
            { "data": "Direccion" }

        ],
        "language": {
            "url": $.MisUrls.url.Url_datatable_spanish
        },
        responsive: true
    });

    tablaproducto = $('#tbProducto').DataTable({
        "ajax": {
            "url": $.MisUrls.url._ObtenerProductos,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            {
                "data": "IdProducto", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-sm btn-primary ml-2' type='button' onclick='productoSelect(" + JSON.stringify(row) + ")'><i class='fas fa-check'></i></button>"
                },
                "orderable": false,
                "searchable": false,
                "width": "90px"
            },
            { "data": "Codigo" },
            { "data": "Nombre" },
            { "data": "Descripcion" },
            {
                "data": "oCategoria", render: function (data) {
                    return data.Descripcion
                }
            }

        ],
        "language": {
            "url": $.MisUrls.url.Url_datatable_spanish
        },
        responsive: true
    });


    tabladata = $('#tbdata').DataTable({
        "ajax": {
            "url": $.MisUrls.url._ObtenerAsignaciones,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "oRestaurant", render: function (data) { return data.Nombre } },
            { "data": "oRestaurant", render: function (data) { return data.RUC } },
            { "data": "oProducto", render: function (data) { return data.Codigo } },
            { "data": "oProducto", render: function (data) { return data.Nombre } },
            { "data": "Stock" },
            {
                "data": "IdProductoTienda", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminar(" + data + ")'><i class='fa fa-trash'></i></button>"
                },
                "orderable": false,
                "searchable": false,
                "width": "80px"
            }

        ],
        "language": {
            "url": $.MisUrls.url.Url_datatable_spanish
        },
        responsive: true
    });

})


function buscarRestaurant() {
    tablarestaurant.ajax.reload();
    $('#modalRestaurant').modal('show');
}

function buscarProducto() {
    tablaproducto.ajax.reload();
    $('#modalProducto').modal('show');
}

function tiendaSelect(json) {
    $("#txtIdRestaurant").val(json.IdRestaurant);
    $("#txtRuc").val(json.RUC);
    $("#txtRazonSocial").val(json.Nombre);
    $("#txtDireccion").val(json.Direccion);

    $('#modalRestaurant').modal('hide');
}

function productoSelect(json) {
    $("#txtIdProducto").val(json.IdProducto);
    $("#txtCodigo").val(json.Codigo);
    $("#txtNombre").val(json.Nombre);
    $("#txtDescripcion").val(json.Descripcion);

    $('#modalProducto').modal('hide');
}

$("#txtCodigo").on('keypress', function (e) {

    if (e.which == 13) {

        //OBTENER PRODUCTOS
        jQuery.ajax({
            url: $.MisUrls.url._ObtenerProductos,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#txtCodigo").LoadingOverlay("hide");
                var encontrado = false;
                if (data.data != null) {
                    $.each(data.data, function (i, item) {
                        if (item.Activo == true && item.Codigo == $("#txtCodigo").val()) {

                            $("#txtIdProducto").val(item.IdProducto);
                            $("#txtCodigo").val(item.Codigo);
                            $("#txtNombre").val(item.Nombre);
                            $("#txtDescripcion").val(item.Descripcion);

                            encontrado = true;
                            return false;
                        }
                    })

                    if (!encontrado) {
                        $("#txtIdProducto").val("0");
                        $("#txtNombre").val("");
                        $("#txtDescripcion").val("");
                    }
                }

            },
            error: function (error) {
                console.log(error)
            },
            beforeSend: function () {
                $("#txtCodigo").LoadingOverlay("show");
            },
        });


    }
});


function asignarProducto() {

    var camposvacios = false;

    if ($("#txtIdRestaurant").val() == "0" || $("#txtIdProducto").val() == "0")
        camposvacios = true;

    if (!camposvacios) {

        var request = {
            objeto: {
                oProducto: { IdProducto: parseInt($("#txtIdProducto").val()) },
                oRestaurant: { IdRestaurant: parseInt($("#txtIdRestaurant").val()) },
            }
        }

        jQuery.ajax({
            url: $.MisUrls.url._RegistrarProductoTienda,
            type: "POST",
            data: JSON.stringify(request),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data.resultado) {
                    tabladata.ajax.reload();
                    $("#txtIdProducto").val("0");
                    $("#txtCodigo").val("");
                    $("#txtNombre").val("");
                    $("#txtDescripcion").val("");
                } else {

                    swal("Mensaje", "No se pudo registrar la asignación", "warning")
                }
            },
            error: function (error) {
                console.log(error)
            },
            beforeSend: function () {

            },
        });

    } else {
        swal("Mensaje!", "Es necesario completar todos los campos", "warning")
    }


}


function eliminar($id) {

    swal({
        title: "Mensaje",
        text: "¿Desea eliminar la asignación?",
        type: "warning",
        showCancelButton: true,

        confirmButtonText: "Si",
        confirmButtonColor: "#DD6B55",

        cancelButtonText: "No",

        closeOnConfirm: true
    },

        function () {
            jQuery.ajax({
                url: $.MisUrls.url._EliminarProductoTienda + "?id=" + $id,
                type: "GET",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    if (data.resultado) {
                        tabladata.ajax.reload();
                    } else {
                        swal("Mensaje", "No se pudo eliminar la asignación?", "warning")
                    }
                },
                error: function (error) {
                    console.log(error)
                },
                beforeSend: function () {

                },
            });
        });

}