
var tabladata;
$(document).ready(function () {
   


    ////validamos el formulario
    $("#form").validate({
        rules: {
            RUC: "required",
            RazonSocial: "required",
            Telefono: "required",
            Direccion: "required"
        },
        messages: {
            RUC: "(*)",
            RazonSocial: "(*)",
            Telefono: "(*)",
            Direccion: "(*)"

        },
        errorElement: 'span'
    });


    tabladata = $('#tbRestaurant').DataTable({
        "ajax": {
            "url": $.MisUrls.url._ObtenerRestaurantes,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "Nombre" },
            { "data": "RUC" },
            { "data": "Direccion" },
            { "data": "Telefono" },
            {
                "data": "Activo", "render": function (data) {
                    if (data) {
                        return '<span>Activo</span>'
                    } else {
                        return '<span>No Activo</span>'
                    }
                }
            },
            {
                "data": "IdRestaurant", "render": function (data, type, row, meta) {
                    return "<button class='btn btn-primary btn-sm' type='button' onclick='abrirPopUpForm(" + JSON.stringify(row) + ")'><i class='fas fa-pen'></i></button>" +
                        "<button class='btn btn-danger btn-sm ml-2' type='button' onclick='eliminar(" + data + ")'><i class='fa fa-trash'></i></button>"
                },
                "orderable": false,
                "searchable": false,
                "width": "90px"
            }

        ],
        "language": {
            "url": $.MisUrls.url.Url_datatable_spanish
        },
        responsive: true
    });


})


function abrirPopUpForm(json) {

    $("#txtid").val(0);

    if (json != null) {

        $("#txtid").val(json.IdRestaurant);

        $("#txtNombre").val(json.Nombre);
        $("#txtRuc").val(json.RUC);
        $("#txtDireccion").val(json.Direccion);
        $("#txtTelefono").val(json.Telefono);
        $("#cboEstado").val(json.Activo == true ? 1 : 0);

    } else {
        $("#txtNombre").val("");
        $("#txtRuc").val("");
        $("#txtTelefono").val("");
        $("#txtDireccion").val("");
        $("#cboEstado").val(1);
    }

    $('#FormModal').modal('show');

}


function Guardar() {

    if ($("#form").valid()) {

        var request = {
            objeto: {
                IdRestaurant: parseInt($("#txtid").val()),
                Nombre: $("#txtNombre").val(),
                RUC: $("#txtRuc").val(),
                Direccion: $("#txtDireccion").val(),
                Telefono: $("#txtTelefono").val(),
                Activo: ($("#cboEstado").val() == "1" ? true : false)
            }
        }

        jQuery.ajax({
            url: $.MisUrls.url._GuardarRestaurant,
            type: "POST",
            data: JSON.stringify(request),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {

                if (data.resultado) {
                    tabladata.ajax.reload();
                    $('#FormModal').modal('hide');
                } else {

                    swal("Mensaje", "No se pudo guardar los cambios", "warning")
                }
            },
            error: function (error) {
                console.log(error)
            },
            beforeSend: function () {

            },
        });

    }

}


function eliminar($id) {


    swal({
        title: "Mensaje",
        text: "¿Desea eliminar el restaurant seleccionado?",
        type: "warning",
        showCancelButton: true,

        confirmButtonText: "Si",
        confirmButtonColor: "#DD6B55",

        cancelButtonText: "No",

        closeOnConfirm: true
    },

        function () {
            jQuery.ajax({
                url: $.MisUrls.url._EliminarRestaurant + "?id=" + $id,
                type: "GET",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    if (data.resultado) {
                        tabladata.ajax.reload();
                    } else {
                        swal("Mensaje", "No se pudo eliminar el restaurant", "warning")
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