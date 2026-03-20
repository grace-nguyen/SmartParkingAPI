# 🚗 Smart Parking API

A modern, lightweight Web API built with **ASP.NET Core 8** to manage a smart parking system. This project demonstrates core backend development concepts, including Clean Architecture, Inheritance, and Dependency Injection.

## 🌟 Key Features

- **Multi-Vehicle Support**: Handles Cars, Vans, and Motorbikes using Object-Oriented Inheritance.
- **Dynamic Fee Calculation**: Automatically calculates parking fees based on real-time duration and vehicle-specific rates.
- **Robust Validation**: Uses Data Annotations and Enums to ensure high-quality input data.
- **RESTful Design**: Provides clean endpoints for vehicle Entry, Listing, and Checkout.
- **Developer-Friendly**: Fully documented with Swagger UI for easy testing.

## 🛠 Tech Stack

- **Framework**: .NET 8 (ASP.NET Core Web API)
- **Architecture**: Layered Architecture (Controllers, Services, Models, DTOs, Interfaces)
- **Dependency Injection**: Singleton Lifetime for in-memory data persistence.
- **Serialization**: System.Text.Json with Enum-to-String conversion.

## 📂 Project Structure

- `Controllers/`: Handles incoming HTTP requests and routes them to services.
- `Services/`: Contains business logic and manages the vehicle collection.
- `Models/`: Core entities (Car, Van, Motorbike) and the base `Vehicle` class.
- `DTOs/`: Data Transfer Objects for decoupled communication between client and server.
- `Interfaces/`: Defines service contracts for better testability and flexibility.

## 🚀 Getting Started

### 1. Prerequisites

- Install [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).

### 2. Installation & Execution

```bash
# Clone the repository
git clone <your-repository-url>

# Navigate to the project folder
cd SmartParkingApi

# Restore dependencies
dotnet restore

# Run the application
dotnet run
```
