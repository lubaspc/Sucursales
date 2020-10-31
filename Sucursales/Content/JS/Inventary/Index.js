$(document).ready(function () {
    const inputs = $("#filters").find("input");
    $(document).on("click", "#btn-filter", function (e) {
        e.preventDefault()
        var url = $(this).attr("href") + "?"
        values = inputs.map(function (i, input) {
            return $(input).attr("name") + "=" + $(input).val()
        })
        url = url + Object.values(values).join("&")
        window.location.replace(url)
    });

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