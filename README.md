# ClinicSystem

## Overview
**ClinicSystem** is a comprehensive healthcare management application designed to streamline clinic operations, including scheduling appointments, managing patient records, and coordinating staff roles. This project leverages modern technologies and architectural patterns to ensure a robust, scalable, and maintainable codebase.

## Features
- **Appointment Management**
  - Schedule and manage patient appointments with doctors
- **Patient and Staff Management**
  - Maintain detailed records of patients, doctors, and secretaries
- **Authentication and Authorization**
  - Implemented using Microsoft Identity
- **Data Mapping and Validation**
  - FluentAPI to manage the relations between Entities
- **Design Patterns**
  - Repository Pattern for data access
  - Unit of Work for managing transactions
  - MVC (Model-View-Controller) pattern for organizing the application
- **Front-end Integration**
  - jQuery for DOM manipulation and AJAX calls
  - DataTables jQuery plugin for enhanced table functionality (searching, pagination)
- **Dashboard**
  - Display counts, charts, and other metrics for a quick overview
- **Role Management**
  - Different roles for Secretary and Doctor with specific permissions

## Technologies Used
- **Backend**: .NET Core, MS Identity.
- **Frontend**: HTML, Bootstrap, jQuery, DataTables jQuery
- **Database**: SQL Server
- **Architecture**: N-Tier Architecture, Code First Approach

## Project Structure

```plaintext
ClinicSystem/
├── src/
│   ├── Core/
│   │   └── ClinicSystem.Core/
│   │       ├── DTOs
│   │       ├── Entities
│   │       ├── Interfaces
│   │       ├── Utilities
│   │       └── ...
│   ├── Infrastructure/
│   │   └── ClinicSystem.Infrastructure/
│   │       ├── Data
│   │       ├── Migrations
│   │       ├── Repositories
│   │       └── UnitOfWork
|   |       └── Services
│   ├── Web/
│   │   └── ClinicSystem.Web/
│   │       ├── wwwroot
│   │       ├── Controllers
│   │       ├── Views
│   │       └── ViewModels
│   │       └── Utilities

## Screen of Project

![WhatsApp Image 2024-05-21 at 13 59 49_e9a298e2](https://github.com/Islam-Ismail-Aly/ClinicSystem/assets/23121933/ff0fe78f-7f1d-446f-8d0f-31fa7ed0aa2e)
