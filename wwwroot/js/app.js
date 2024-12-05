// app.js
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connection.on("ReceiveNotification", function (message) {
    alert(message);  // You can replace this with your own UI update logic
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
