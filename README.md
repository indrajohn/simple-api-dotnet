# Messages Web Server



A simple ASP.NET Core web server application that supports CRUD operations on messages, storing them directly in a local JSON file for persistence. This ensures messages remain available even after the server is restarted, without the complexity of a database

## Getting Started

These instructions will get your copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

Ensure you have the [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) or later installed on your machine.

### Installation

Clone the repository to your local machine:

```bash
git clone https://yourrepositoryurl.git
```
Navigate to the project directory:

```bash
cd path/to/your/project
```
Build the project:
```bash
dotnet build
```
Run the server:
```bash
dotnet run
```

The server will start listening on http://localhost:5000 by default.
## Usage
### Viewing Messages
Retrieve all messages in JSON format:
```bash
curl http://localhost:5000/api/messages
```
Retrieve specific messages by id
```bash
curl http://localhost:5000/api/messages/{id}
```
Replace {id} with the ID of the message you wish to view.

### Adding a Message
Add a new message to the list:
```bash
curl -X POST http://localhost:5000/api/messages -H "Content-Type: application/json" -d "{\"text\":\"Your message here\"}"
```
Replace "Your message here" with the message you wish to add.

### Updating a Message

Update a message by ID:

```bash
curl -X PUT http://localhost:5000/api/messages/{id} -H "Content-Type: application/json" -d "{\"text\":\"Updated message text\"}"
```
Replace {id} and "Updated message text" with the message you wish to update.

### Deleting a Message

Delete a message by ID:

```bash
curl -X DELETE http://localhost:5000/api/messages/{id}
```
Replace {id} with the ID of the message you wish to delete.


## Swagger Documentation

This API project includes Swagger documentation, which allows you to explore and test the available endpoints interactively. Follow the steps below to access the Swagger documentation:

1. Build and Run the Application:


Ensure you have the .NET 5.0 SDK or later installed on your machine. Clone the repository and navigate to the project directory. Build and run the server using the following commands:
```bash
git clone https://yourrepositoryurl.git
cd path/to/your/project
dotnet build
dotnet run
```

The server will start listening on http://localhost:5000 by default.

2. Access Swagger UI:
Once the application is running, open your web browser and go to the Swagger UI documentation page:
```bash
http://localhost:5000/swagger/index.html
```
Replace http://localhost:5000 with the URL where your application is hosted.

3. Explore and Test Endpoints:



The Swagger UI provides a user-friendly interface to explore all available API endpoints, including those for CRUD operations on messages. You can interactively test each endpoint, view their details, and even execute requests directly within the Swagger UI.

## License

[MIT](https://choosealicense.com/licenses/mit/)
