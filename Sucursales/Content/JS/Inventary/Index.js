﻿$(document).ready(function () {
    Utils.appliFilter()

    $(document).on("click", ".btn-delete",function (e) {
        e.preventDefault()
        var url = $(this).attr("href")
        Swal.fire({
            title: 'Estas seguro de querer quitar el inventario',
            showCancelButton: true,
            confirmButtonText: `Eliminar`,
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            /* Read more about isConfirmed, isDenied below */
            if (result.isConfirmed) {
                window.location.replace(url)
            }
        });
    });
});