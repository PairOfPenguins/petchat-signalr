# ASP.NET Core SignalR Chat Application

A real-time chat application built with ASP.NET Core and SignalR. This app features user registration, login with JWT authentication, and real-time messaging. The frontend is crafted with HTML, JavaScript, Bootstrap, and Tailwind CSS.

---

## Features

- **User Registration & Login**  
  Secure user authentication with JWT tokens. Passwords are stored securely using hashing and salting techniques.

- **Real-Time Chat**  
  Leverages SignalR for instant messaging between users.

- **Responsive Frontend**  
  Built with Bootstrap and Tailwind CSS for a responsive and modern design.

---

## UI Screenshot

Here's a screenshot of the app's user interface:

![App UI](https://github.com/user-attachments/assets/1603f18d-5f59-4b91-bd12-60a3a33976d4)

---

## Technologies Used

- **Backend:** ASP.NET Core, SignalR  
- **Frontend:** HTML, JavaScript, CSS (Bootstrap, Tailwind CSS)  
- **Authentication:** JWT (JSON Web Tokens)  
- **Password Storage:** Hashing and Salting  

---

## Getting Started

### Prerequisites

- .NET SDK
- Node.js and npm
- SQL Server or any supported database

### Setup Instructions

1. **Clone the Repository**

    ```bash
    git clone https://github.com/PairOfPenguins/petchat-signalr.git
    cd petchat-signalr
    ```

2. **Backend Setup**

    - Navigate to the ASP.NET Core project directory:

      ```bash
      cd Backend
      ```

    - Restore dependencies and build the project:

      ```bash
      dotnet restore
      dotnet build
      ```

    - Run the application:

      ```bash
      dotnet run
      ```

4. **Database Setup**

    - Open the `appsettings.json` file in the project root and add your connection string. It should look like this:

      ```json
      {
        "ConnectionStrings": {
          "DefaultConnection": "Your connection string here"
        }
      }
      ```

    - Replace `"Your connection string here"` with your actual database connection string.

    - Update the database:

      ```bash
      dotnet ef database update
      ```

---

## Usage

- **Register:** Create a new user account.
- **Login:** Authenticate using JWT.
- **Chat:** Start real-time conversations with other users.

---

## To-Do

- **Frontend for PUT and DELETE Requests**  
  Implement UI components for updating and deleting messages and user accounts.

- **Admin Role Management**  
  Introduce role-based authorization to restrict access to certain endpoints. Currently, any user can access all endpoints.
