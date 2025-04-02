# TogglTrack Project
A time tracking application built with .NET that allows users to track their activities, manage projects, and monitor productivity. This project implements a clean 3-layer architecture with a modern Blazor UI.
Features
* **User Management**
   * Create and manage multiple user profiles
   * Edit personal user information
   * Select active user at application startup
* **Activity Tracking**
   * Create, read, update, and delete activities
   * Automatic validation preventing overlapping activities
   * One activity at a time per user constraint
* **Project Management**
   * Browse available projects
   * Join existing projects
   * Associate activities with projects
* **Advanced Filtering**
   * Filter activities by custom date range
   * User-friendly preset filters (last week, month, previous month, year)
Architecture
The application follows a 3-layer architecture:
* **App Layer** - Blazor frontend for user interaction
* **Business Logic Layer** - Core application logic and services
* **Data Access Layer** - Database operations with Entity Framework
Technology Stack
* **Backend:**
   * .NET Core
   * Entity Framework Core
   * SQL Server
   * 3-Layer Architecture
* **Frontend:**
   * Blazor
   * Responsive web design
   * Client-side validation
