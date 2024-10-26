# Music Catalog App

A WPF application for managing a Music Catalog, built with .NET and using Entity Framework Core for data access.

## Prerequisites

- .NET SDK
- Docker
- Visual Studio 2019 or later (or nuget.exe)
- PostgreSQL (in Docker)

## Getting Started

The important thing is to use Package Manager Console if you are using Visual Studio

### Clone the Repository

```sh
git clone https://github.com/RoundRonin/Music-Catalog.git
cd music-catalog
```

### Set Up the Database
Run PostgreSQL in Docker unisng docker compose.

For safety concerns, credentials for the DB are stored in .env
Though for testing purpose it is included in the repo.

To start:
```sh
docker-compose up -d
```

To stop:
```sh
docker-compose down
```

If the migrations are not created, create database:
```sh
Add-Migration InitialCreate
Update-Database
```

### Install Dependencies
Restore NuGet Packages: Open the solution in Visual Studio and restore NuGet packages.

``` sh
dotnet restore
```

Apply Migrations: Apply Entity Framework Core migrations to set up the database schema. #TODO
```sh
dotnet ef database update
```

## Run the Application
Build and Run: Build and run the application from Visual Studio or using the command line.

``` sh
dotnet build
dotnet run
```

## Project Structure

Project follows MVVM model as is common with WPF. Project also uses Entinty Framework to interact with PostgreSQL database.

Data/: Contains the MusicCatalogContext class for Entity Framework.
Entities/: Contains entity classes (Artist.cs, Album.cs, Song.cs, Playlist.cs).
Views/: Contains WPF XAML files for the UI.
ViewModels/: Contains ViewModel classes for MVVM.
Models/: Contains model classes if any additional business logic is needed.

# Contributing

Quite unnecessary, because this is an exercise task.
