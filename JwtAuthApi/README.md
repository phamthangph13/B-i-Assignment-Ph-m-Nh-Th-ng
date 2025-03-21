# JWT Authentication API with ASP.NET Core

This is a RESTful API with JWT Authentication built using ASP.NET Core Web API and Swagger UI.

## Features

- JWT Authentication
- Entity Framework Core with InMemory Database
- RESTful API endpoints with authorization
- Swagger UI with JWT Authentication support
- Role-based authorization

## Getting Started

1. Clone this repository
2. Run the application:
   ```
   dotnet run
   ```
3. Navigate to https://localhost:5001/swagger to access the Swagger UI

## API Endpoints

### Authentication

- **POST /api/auth/login** - Authenticate user and get JWT token
  - Request body:
    ```json
    {
      "username": "admin",
      "password": "admin123"
    }
    ```
  - Success response:
    ```json
    {
      "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
      "expiration": "2023-05-20T15:30:45.1234567Z"
    }
    ```

### Users (Protected Routes)

- **GET /api/users** - Get all users (requires authentication)
- **GET /api/users/{id}** - Get user by ID (requires authentication)
- **GET /api/users/profile** - Get current user's profile (requires authentication)
- **GET /api/users/admin** - Admin-only endpoint (requires Admin role)

## Using JWT Token in Swagger

1. Get a token by using the `/api/auth/login` endpoint
2. Click the "Authorize" button at the top of the Swagger UI
3. Enter your token in the format: `Bearer your_token_here`
4. Click "Authorize" and close the dialog
5. Now you can access the protected endpoints

## Demo Users

The API comes with two pre-configured users:

1. Admin User:
   - Username: `admin`
   - Password: `admin123`
   - Role: `Admin`

2. Regular User:
   - Username: `user`
   - Password: `user123`
   - Role: `User`

## Security Notes

- This is a demo application. In a production environment, you should:
  - Store hashed passwords (using BCrypt or similar)
  - Use a more secure JWT key
  - Use a persistent database
  - Add input validation
  - Implement refresh tokens
  - Add HTTPS configuration 