const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

document.getElementById("sendBtn").addEventListener("click", function () {
    let message = document.getElementById("message").value;

    fetch("/api/message", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${localStorage.getItem("token")}` 
        },
        body: JSON.stringify({ content: message }),
    })
        .then(response => {
            if (!response.ok) throw new Error("Failed to send message");
        })
        .catch(err => console.error(err));
});

document.getElementById("logoutBtn").addEventListener("click", function () {
    localStorage.clear();
    window.location.href = "/Login";
});


hubConnection.on("ReceiveMessage", function (message) {
    const content = message.content;
    const userName = message.assignedUserName;
    const createdDate = new Date(message.createdDate); 
    const formattedTime = createdDate.toLocaleTimeString(); 

    let messageElement = document.createElement("p");

    let userNameElement = document.createElement("strong");
    userNameElement.textContent = userName + ": ";

    let timeElement = document.createElement("small");
    timeElement.style.color = "gray";
    timeElement.style.marginLeft = "10px";
    timeElement.textContent = `(${formattedTime})`;

    messageElement.appendChild(userNameElement);
    messageElement.appendChild(document.createTextNode(content + " "));
    messageElement.appendChild(timeElement);

    document.getElementById("chatroom").appendChild(messageElement);
});



hubConnection.start()
    .then(() => document.getElementById("sendBtn").disabled = false)
    .catch(err => console.error(err.toString()));

