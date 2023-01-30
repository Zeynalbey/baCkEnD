
    let btns = document.querySelectorAll(".add-product-to-basket-btn")

    btns.forEach(x => x.addEventListener("click", function (e) {
        e.preventDefault()
        fetch(e.target.parentElement.href)
            .then(response => response.text())
            .then(data => {
                $('.cart-block').html(data);
            })
    }))





    $(document).on("click", ".remove-product-to-basket-btn", function (e) {
        e.preventDefault();

        fetch(e.target.parentElement.href)
            .then(response => response.text())
            .then(data => {
                $('.cart-block').html(data);
            })
    })


    $(document).on("click", ".plus-btn", function (e) {
        e.preventDefault();

        fetch(e.target.href)
            .then(response => response.text())
            .then(data => {
                $('.cartPageJs').html(data);

                fetch(e.target.nextElementSibling.href)
                    .then(response => response.text())
                    .then(data => {
                        $('.cart-block').html(data);
                    })
            })
    })


function addtobasket() {
    var colorid = $("#color-data").val()
    var sizeid = $("#size-data").val()
    var quantity = $("#quantity-data").val()
    var productid = $("#productid-data").val()
    console.log(colorid)
    console.log(sizeid)
    console.log(quantity)
    console.log(productid)
  console.log("ilk")

    var basketitem = {
        Id: productid,
        ColorId: colorid,
        SizeId: sizeid,
        Quantity: quantity
    };
     console.log("sonra")

    $.ajax({
        type: "POST",
        url: "/basket/add",
        data: {
            Id: productid,
            ColorId: colorid,
            SizeId: sizeid,
            Quantity: quantity
        },
            success: function (response) {
                console.log("salam")
                $('.cart-block').html(response);


            },
        error: function (err) {
                console.log("error var")
                $(".product-details-modal").html(err.responseText);

            }
    })


}


//$(document).on("click", '.add-product-to-basket-btn-modal', function (e) {
//    $.ajax(
//        {
//            type: "GET",
//            url: "https://localhost:7026/shop/list",

//            data: {
                
//            },

//            success: function (response) {
//                console.log(response)
//                cardBlock.html(response);




//            },
//            error: function (err) {
//                $(".product-details-modal").html(err.responseText);

//            }

//        });

//})