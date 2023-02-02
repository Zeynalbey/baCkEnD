
$(document).ready(function () {

    let btns = document.querySelectorAll(".add-basket-btn")

    btns.forEach(x => x.addEventListener("click", function (e) {
        e.preventDefault()
        fetch(e.target.parentElement.href)
            .then(response => response.text())
            .then(data => {
                $('.cart-block').html(data);
            })
    }))

    $(document).on("click", ".remove-basket-btn", function (e) {
        e.preventDefault();
        fetch(e.target.parentElement.href)
            .then(response => response.text())
            .then(data => {
                $('.cart-block').html(data);
            })


        //e.preventDefault();

        //let aHref = e.target.parentElement.href;
        //$.ajax({

        //    url: aHref,
        //    success: function (response) {
        //        $('.cart-block').html(response);
        //    }
        //}
        //)
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

    $(document).on("click", ".minus-btn", function (e) {
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

    $(document).on("click", '.select-catagory', function (e) {
        e.preventDefault();
        let aHref = e.target.href;
        let category = e.target.previousElementSibling
        let CategoryId = category.value;


        console.log(CategoryId)

        console.log(aHref)



        $.ajax(
            {
                type: "GET",
                url: aHref,

                data: {
                    CategoryId: CategoryId
                },

                success: function (response) {
                    console.log(response)
                    $('.filtered-area').html(response);

                },
                error: function (err) {
                    $(".modalProduct").html(err.responseText);

                }

            });

    })

    $(document).on("click", '.select-color', function (e) {
        e.preventDefault();
        let aHref = e.target.href;
        let category = e.target.previousElementSibling
        let CategoryId = category.value;


        console.log(CategoryId)

        console.log(aHref)



        $.ajax(
            {
                type: "GET",
                url: aHref,

                data: {
                    CategoryId: CategoryId
                },

                success: function (response) {
                    console.log(response)
                    $('.filtered-area').html(response);

                },
                error: function (err) {
                    $(".modalProduct").html(err.responseText);

                }

            });

    })

    $(document).on("click", '.select-tag', function (e) {
        e.preventDefault();
        let aHref = e.target.href;
        let category = e.target.previousElementSibling
        let CategoryId = category.value;


        console.log(CategoryId)

        console.log(aHref)



        $.ajax(
            {
                type: "GET",
                url: aHref,

                data: {
                    CategoryId: CategoryId
                },

                success: function (response) {
                    console.log(response)
                    $('.filtered-area').html(response);

                },
                error: function (err) {
                    $(".modalProduct").html(err.responseText);

                }

            });

    })


    $(document).on("change", ".searchproductPrice", function (e) {
        e.preventDefault();

        let minPrice = e.target.previousElementSibling.children[0].children[3].innerText.slice(1);
        let MinPrice = parseInt(minPrice);

        let maxPrice = e.target.previousElementSibling.children[0].children[4].innerText.slice(1);
        let MaxPrice = parseInt(maxPrice);
        let aHref = document.querySelector(".shoppage-url").href;

        console.log(MinPrice);
        console.log(MaxPrice);
        console.log(aHref)

        $.ajax(
            {
                url: aHref,

                data: {
                    MinPrice: MinPrice,
                    MaxPrice: MaxPrice

                },

                success: function (response) {
                    $('.filtered-area').html(response);


                },
                error: function (err) {
                    $(".modalProduct").html(err.responseText);

                }

            });


    })
})