Product Alert Service API

A minimal ASP.NET Core Web API that monitors product stock levels and restock dates to provide alerts about low stock or overdue restocking.
Features

    Reads product data from a JSON file.

    Detects products with stock below a configurable threshold.

    Detects products overdue for restocking based on configurable days.

    Returns alerts with severity levels.

    Minimal API design with dependency injection and configuration support.

    Swagger/OpenAPI integration for easy API exploration.

Getting Started
Prerequisites

    .NET 7 SDK

    A JSON file with product data (see sample below)

Configuration

Configure the file path and alert thresholds in appsettings.json:

{
  "ProductAlertService": {
    "FilePath": "C:\\path\\to\\your\\products.json"
  }
}

Sample products.json Structure

[
  {
    "id": 1,
    "name": "Product A",
    "quantity": 3,
    "lastRestocked": "2024-01-01T00:00:00"
  },
  {
    "id": 2,
    "name": "Product B",
    "quantity": 10,
    "lastRestocked": "2024-05-01T00:00:00"
  }
]

Running the API

    Clone the repository.

    Update appsettings.json with your JSON file path.

    Run the API:

dotnet run

    Open https://localhost:{port}/swagger to explore the endpoints.

API Endpoints

    GET /alerts
    Retrieves a list of stock alerts. Optional query parameters:

        quantity (int, default 5): Stock quantity threshold.

        days (int, default -180): Number of days to check restock overdue.

Example:

GET /alerts?quantity=3&days=-90

Testing

Basic NUnit tests included to verify product restock date logic.

Run tests with:

dotnet test

Project Structure

    Infra/Models – Data models

    Infra/Services – Business logic for alerts

    Program.cs – Minimal API setup

    appsettings.json – Configuration

    tests – Unit tests
