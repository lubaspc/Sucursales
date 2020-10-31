$(document).ready(function () {
    var index = 0;
    const $table = $('#table-products'),
        $form = $table.closest('form')

    $('#btn-add').click(function () {
        const $option = $("#product-add option:selected")
        const id = $option.val(),
            name = $option.data("name"),
            price = $option.data('price')
            $tr = $('<tr>');

        $tr.append($('<td>', {
            html: $('<input>', {
                name: 'Products[' + index + '].ProductId',
                type: 'hidden',
                value: id
            })
        }).append($('<input>', {
            readonly: true,
            value: name,
        })));

        $tr.append($('<td>', {
            html: $('<input>', {
                name: 'Products[' + index + '].Quantity',
                type: 'number',
                value: 1,
                'data-td': 'td-subtotal-' + index,
                class: 'inp-quanity',
                'data-price': price
            })
        }))

        $tr.append($('<td>', {
            id: 'td-subtotal-'+index,
            html: price.toLocaleString('en-US', { style: 'currency', currency: 'USD' }),
            class: 'td-subtotal'
        }))


        $tr.append($('<td>', {
            html: '<i class="fas fa-trash text-warning fa-2x btn-erase cursor-pointer" style="cursor:pointer"></i>'
        }))

        $table.append($tr)
        index++
        calculateTotal()
    })

    $(document).on('change', '.inp-quanity', function () {
        const $input = $(this),
            $td = $('#'+$input.data('td')),
            subTotal = $input.val() * $input.data('price')
        $td.html(subTotal.toLocaleString('en-US', { style: 'currency', currency: 'USD' })
        )
        calculateTotal()
    })

    function calculateTotal() {
        var total = 0;
        $('.td-subtotal').each(function (i, $td) {
            total += $($td).html().replace('$', '') * 1
        });
        $('#Total').val(total)
        $('#lbl-total').html(total.toLocaleString('en-US', { style: 'currency', currency: 'USD' }))
    }

    $(document).on('click', '.btn-erase', function () {
        $(this).closest("tr").remove()
        calculateTotal()
    })
});