
# Dynamic Mapping System

The Dynamic Mapping System is a web application designed to facilitate the mapping of reservation data between different models, specifically converting between a standard reservation model and a Google reservation model. The application provides a RESTful API for easy integration and data manipulation.

## Technologies Used

- **.NET 6**: The framework used to build the web application.
- **ASP.NET Core MVC**: For building the API.
- **Entity Framework Core**: For data access (if applicable).
- **Moq**: For mocking dependencies in unit tests.
- **Newtonsoft.Json**: For JSON serialization and deserialization.
- **MS Test**: For unit testing the application.

## Features

- **Dynamic Mapping**: Maps data between `Reservation` and `GoogleReservationModel` using a dynamic mapper service.
- **Error Handling**: Implements global error handling to manage exceptions and return structured error responses.
- **Unit Testing**: Comprehensive unit tests for controllers and services to ensure reliability and maintainability.
