$(document).on("click", ".show-plant-modal", function (e) {
    e.preventDefault();

    var url = e.target.parentElement.href;
    console.log(url)

    fetch(url)
        .then(response => response.text())
        .then(data => {
            $('.plant-details-modal').html(data);
            console.log(data)
        })

    $("#quickModal").modal("show");
})