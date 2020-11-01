var Utils = {
    appliFilter: function () {
        const inputs = $("#filters").find("input")
        $(document).on("click", "#btn-filter", function (e) {
            e.preventDefault()
            var url = $(this).attr("href") + "?"
            values = inputs.map(function (i, input) {
                return $(input).attr("name") + "=" + $(input).val()
            })
            url = url + Object.values(values).join("&")
            window.location.replace(url)
        });
    }
}