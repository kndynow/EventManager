# Event Manager - A Group Assignment

## Project Overview

Event Manager is a web application designed to simplify event management and ticket purchasing. This project aims to create a user-friendly platform for both event attendees and administrators.

### Key Features

- **For Users:**
  - Browse and search for public events
  - Purchase tickets for desired events
  - Manage and track purchased tickets

- **For Administrators:**
  - Publish, update, and remove events
  - Manage registered users
  - Monitor ticket sales and event attendance

## Getting Started

[Instructions for setting up the project locally]

## Technologies Used

- Browse and search for public events
- Purchase tickets for desired events
- Manage and track purchased tickets

- **For Administrators:**
  - Publish, update, and remove events
  - Manage registered users
  - Monitor ticket sales and event attendance

## Getting Started

1. Clone project
2. In your terminal, navigate to EventManager.Api
3. Run the following commands:

    ```bash
    dotnet user-secrets init
    dotnet user-secrets set "JwtSettings:SecretKey" "din-hemliga-nyckel-här-minst-32-tecken"
    ```

5. Test if the changes worked by first start the API-project
6. Then start the UI-project

## Technologies Used

- ASP.NET Core 9.0
- Blazor WebAssembly
- Entity Framework Core
