const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

document.getElementById("sendBtn").addEventListener("click", function () {
    let message = document.getElementById("message").value;

    fetch("/api/message", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ content: message, assignedUserId: 1 }),
    })
        .then(response => {
            if (!response.ok) throw new Error("Failed to send message");
        })
        .catch(err => console.error(err));
});

hubConnection.on("ReceiveMessage", function (message) {
    const content = message.content;
    const userName = message.assignedUserName;

    let messageElement = document.createElement("p");

    let userNameElement = document.createElement("strong");
    userNameElement.textContent = userName + ": ";

    messageElement.appendChild(userNameElement);
    messageElement.appendChild(document.createTextNode(content));

    document.getElementById("chatroom").appendChild(messageElement);
});

hubConnection.start()
    .then(() => document.getElementById("sendBtn").disabled = false)
    .catch(err => console.error(err.toString()));
