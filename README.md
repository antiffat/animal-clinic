
# Animal Clinic Management System

## Project Description

**Animal Clinic Management System** is a web application designed to manage the operations of an animal clinic. The system allows the clinic staff to manage animals, employees, users, and visits efficiently. The application also includes authentication and role-based authorization to secure various endpoints.

## Getting Started

These instructions will help you set up the project on your local machine for development and testing purposes.

### Prerequisites

Before you begin, ensure you have the following installed:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or any other database supported by EF Core)
- [Git](https://git-scm.com/downloads)

### Installation

1. **Clone the Repository**:
   ```sh
   git clone https://github.com/antiffat/animal-clinic.git
   cd animal-clinic
   ```

2. **Navigate to the Project Directory**:
   ```sh
   cd src/AnimalClinic
   ```

3. **Restore Dependencies**:
   ```sh
   dotnet restore
   ```

### Running the Project

1. **Update Database**:
   - Ensure your SQL Server is running.
   - Update the database with migrations.
     ```sh
     dotnet ef database update
     ```

2. **Run the Application**:
   ```sh
   dotnet run
   ```

3. **Access the Application**:
   - Open your browser and navigate to `https://localhost:5237` (or the port specified in the launch settings).

## Project Structure

Here is a brief overview of the project's structure:

```
animal-clinic/
├── src/
│   └── AnimalClinic/
│       ├── Controllers/        # API Controllers
│       ├── DTOs/               # Data Transfer Objects
│       ├── Helpers/            # Helper Classes
│       ├── Migrations/         # Entity Framework Migrations
│       ├── Models/             # Entity Models
│       ├── Properties/         # Project Properties
│       ├── appsettings.json    # Application Settings
│       ├── Program.cs          # Main Program
│       ├── AnimalClinic.csproj # Project File
│       └── ...                 # Other files and folders
├── .gitignore
├── AnimalClinic.sln            # Solution File
└── README.md                   # Project Documentation
```

## Dependencies

The project uses the following dependencies:

- **ASP.NET Core**: For building the web API.
- **Entity Framework Core**: For database interactions.
- **Swashbuckle.AspNetCore**: For integrating Swagger for API documentation.
- **Newtonsoft.Json**: For JSON serialization and deserialization.
- **Microsoft.Identity.Client**: For authentication.
- **Humanizer**: For formatting dates, times, and numbers.
