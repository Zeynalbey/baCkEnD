
var connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7026/hubs/alert-hub", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
    })
    .build();


connection.on("Notify", function (message) {
    console.log(message);
    InsertNotificaitonMessageToBox(message.title, message.content);
})

connection.start();



function InsertNotificaitonMessageToBox(title, content) {
    let menu = $("#notification-menu");
    menu.prepend(GetNotificationMessage(title, content))
}

function GetNotificationMessage(title, content) {
    return `<a href="#" class="dropdown-item py-3">
                <small class="float-end text-muted ps-2">2 min ago</small>
                <div class="media">
                    <div class="avatar-md bg-soft-primary">
                        <i class="ti ti-chart-arcs"></i>
                    </div>
                    <div class="media-body align-self-center ms-2 text-truncate">
                        <h6 class="my-0 fw-normal text-dark">${title}</h6>
                        <small class="text-muted mb-0">${content}</small>
                    </div><!--end media-body-->
                </div><!--end media-->
            </a><!--end-item-->`
}