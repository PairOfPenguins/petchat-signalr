document.getElementById("registerForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const username = document.getElementById("username").value.trim();
    const password = document.getElementById("password").value.trim();

    const response = await fetch("/api/user", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password })
    });

    const messageElement = document.getElementById("message");

    if (response.ok) {
        messageElement.textContent = "Registration successful! Redirecting...";
        setTimeout(() => window.location.href = "/Login", 1500);
    } else {
        try {
            const errorData = await response.json(); 
            if (errorData.errors) {
                const errors = Object.values(errorData.errors).flat();
                messageElement.textContent = "Error: " + errors.join(", ");
            } else {
                messageElement.textContent = "Error: " + (errorData.title || "Unknown error occurred");
            }
        } catch {
            messageElement.textContent = "Error: Unable to process the error response.";
        }
    }
});
