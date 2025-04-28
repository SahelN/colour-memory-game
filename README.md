# 🎮 Colour Memory Game

A fullstack memory game built with React (frontend) and ASP.NET Core (backend), using MongoDB as the database.

---

## 📋 Table of Contents

- [🧠 Features]
- [🛠️ Technologies]
- [🚀 Getting Started]
  - [📦 Prerequisites]
  - [🔧 Installation]
  
- [📋 To Do]
    - [🧪 Testing]
    - [📑 Code Documentation]
    - [🐳 Dockerize the Application]

---

## 🧠 Features

- User-friendly 4x4 memory game with colored cards.
- Score system: +1 point for a match, -1 point for a mismatch.
- Automatic board updates after 2 seconds.
- Shows current score during the game.
- Displays a message when the game is over.
- Allows starting a new game.

---

## 🛠️ Technologies

- **Frontend**: React, TypeScript
- **Backend**: ASP.NET Core 9 Web API, C#
- **Database**: MongoDB (local via Docker or MongoDB Atlas)
- **Other tools**: Docker, Serilog, ESLint, Prettier

---

## 🚀 Getting Started

### 📦 Prerequisites

Make sure you have installed:

-   Node.js and npm installed
-	.NET Core SDK installed
-	MongoDB instance available
-	Docker Desktop installed

### 🔧 Installation

#### Clone the repository:

```bash
git clone https://github.com/SahelN/colour-memory-game.git
cd colour-memory-game
```

#### Start MongoDB with Docker (if not using Atlas):
```bash
docker run -d --name mongodb -p 27017:27017 mongo --bind_ip_all
```

#### Install backend dependencies:
```bash
cd ../backend/ColourMemory.Api
dotnet restore
dotnet clean
dotnet build
```

#### Install frontend dependencies
```bash
cd frontend
npm install
```

### Access the app 

> 💡 **Important:**  
> Before running the backend, make sure that MongoDB is running.
> 
> If you are using Docker, you can verify that MongoDB is running by executing:
> 
> ```bash
> docker ps
> ```
> 
> You should see a container named `mongodb` listed and its status should be `Up`.  
> 
> If MongoDB is not running, you can start it with:
> 
> ```bash
> docker start mongodb
> ```

#### 1. Start the backend
```bash
cd backend/ColourMemory.Api
dotnet run
```
##### *** Backend API will be available at https://localhost:7042

#### 2. Start the frontend
##### Open a second terminal:
```bash
cd frontend
npm start
```
##### *** Frontend application will be available at http://localhost:3000

## 📋 To Do
### 🧪 Testing
	Frontend: (Planned for Jest/React Testing Library)
	Backend: (Planned for xUnit or NUnit)
### 📑 Code Documentation
### 🐳 Dockerize the Application