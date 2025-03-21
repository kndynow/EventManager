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

**To configure your connection string:**
In the EventManager.Api project:

1. Create a new file named 'appsettings.Development.json' if it doesn't exist.
2. Add your MongoDB connection string to this file using the following format:
   {
     "MongoDbSettings": {
       "ConnectionString": "your_mongodb_connection_string_here",
       "DatabaseName": "EventManager"
     }
   }
3. Replace 'your_mongodb_connection_string_here' with your actual MongoDB connection string.
4. Ensure this file is not tracked by Git to keep your connection string private.

## Technologies Used

- ASP.NET Core 9.0
- Blazor WebAssembly
- MongoDB
