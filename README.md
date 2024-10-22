# Chat Application

Welcome to the Chat Application! This project is built using .NET and WebSockets, providing real-time communication features. The application allows users to join rooms, send messages, and manage chat functionality effectively.

---

## Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [Use Cases](#use-cases)
- [Getting Started](#getting-started)
- [Client Usage](#client-usage)
- [Server Usage](#server-usage)
- [Docker Setup](#docker-setup)
- [Kubernetes Setup](#kubernetes-setup)

---

## Features

- **Real-time Messaging**: Users can send and receive messages instantly.
- **Room Management**: Join and leave chat rooms.
- **Default Room**: Users are assigned to a default "general" room upon connection.
- **Broadcast Messaging**: Messages include usernames for better identification.
- **WebSocket Support**: Efficient handling of WebSocket connections.

---

## Architecture

The application is structured into two main components:

1. **Server**: Handles WebSocket connections, message broadcasting, and room management.
2. **Client**: A console application that connects to the server, sends messages, and interacts with rooms.

---

## Use Cases

### Server Use Cases

- **WebSocket Connection**: Accepts incoming WebSocket connections from clients.
- **Room Management**:
  - Join a room with the command `/join {roomName}`.
  - Leave a room with the command `/leave {roomName}`.
- **Message Handling**: Broadcast messages to all clients in the same room.
- **User Association**: Automatically join the "general" room if no room is specified.

### Client Use Cases

- **Connecting to Server**: Initiate a connection to the WebSocket server.
- **Send Messages**: Users can send messages to the room they are currently in.
- **Join and Leave Rooms**: Dynamically join or leave chat rooms based on user commands.

---

## Getting Started

### Prerequisites

- .NET 6 or higher
- Visual Studio or any preferred IDE

### Cloning the Repository

Clone the repository and navigate to the project directory.

### Running the Server

Navigate to the server directory and run the server using the appropriate command for your development environment.

---

## Client Usage

1. **Connect to Server**:
   - Upon running, the client will connect to the WebSocket server.

2. **Send Messages**:
   - Type your message and hit enter to send it to the current room.

3. **Join a Room**:
   - Type `/join {roomName}` to join a specific room.

4. **Leave a Room**:
   - Type `/leave {roomName}` to leave the current room.

---

## Server Usage

1. **WebSocket Endpoint**: The server listens on a specific port.
   - **Docker Compose**: Accessible on port **8010**.
   - **Kubernetes**: Accessible on port **32000**.

2. **Default Room**: Users are automatically placed in the "general" room upon connection.
3. **Broadcast Messages**: All messages sent will include the sender's username.

---

## Docker Setup

To run the application using Docker Compose, navigate to the directory containing the `docker-compose.yml` file. The server will be accessible at `http://localhost:8010`.

docker compose up -d

---

## Kubernetes Setup

To deploy the application on Kubernetes, ensure you have a valid Kubernetes configuration file. The server will be accessible at `http://localhost:32000`.

kubectl apply -f ./k8s -d

---

Thank you for checking out the Chat Application! I hope you find it useful for your learning in websocket usage with real-time communication needs.
