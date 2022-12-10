$(function () {
    $(document).on('click', '.questionCategory', function () {
        var id = $(this).data('id');
        $.ajax({
            method: 'GET',
            url: "/pages/LoadQuestions",
            data: {
                id: id
            },
            //success: function (data) {
            //    console.log(data);
            //},
            success: function (result) {
                $('#questionContainer').html("");
                $('#questionContainer').append(result);

            }
        })
    })
})

$(function () {
    $(document).on('click', '.productCategory', function () {
        var id = $(this).data('id');
        $.ajax({
            method: 'GET',
            url: "/shop/LoadProducts",
            data: {
                id: id
            },
            success: function (data) {
                console.log(data);
            },
            success: function (result) {
                $('#productContainer').empty();
                $('#productContainer').append(result);
            }
        })
    })
})

jQuery(function ($) {
    $(document).on('click', '#addToCart', function () {
        var id = $(this).data('id');

        $.ajax({
            method: "POST",
            url: "/basket/add",
            data: {
                id: id
            },
            success: function () {
                console.log("ok");
            }
        })
    })

    $(document).on('click', '#deleteBtn', function () {
        var id = $(this).data('id');

        $.ajax({
            method: "POST",
            url: "/basket/delete",
            data: {
                id: id
            },
            success: function () {
                $(`.basket-product[id=${id}]`).remove();
            }
        })
    })
})
