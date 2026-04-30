# 📌 Task Board Application (VARStreet Assignment)

## 🚀 Overview

This is a full-stack Task Management application built using:

* Backend: ASP.NET Core Web API (.NET 8)
* Frontend: React (Functional Components + Hooks)
* Database: SQLite with Entity Framework Core

The application allows users to:

* Create and manage projects
* Add tasks under projects
* Track task status and priority
* Add comments to tasks
* View dashboard insights

---

## 🛠️ Tech Stack

### Backend

* ASP.NET Core Web API
* Entity Framework Core (SQLite)
* Clean Architecture (Controllers → Services → DbContext)
* Global Exception Handling Middleware

### Frontend

* React (Hooks + Functional Components)
* React Router
* Axios
* Custom Hook (`useApi`)

---

## 📁 Project Structure

### Backend (`TaskBoard.Api`)

* Controllers → API endpoints
* Services → Business logic
* Models → Entities
* DTOs → Request/Response models
* Data → DbContext + Seed Data
* Middleware → Global exception handler
* Migrations → EF Core migrations

### Frontend (`task-board-ui`)

* pages → Dashboard, Project Board, Task Detail
* components → Reusable UI
* hooks → useApi
* services → API calls
* context → Global state

---

## ⚙️ Setup Instructions

### 🔹 1. Clone Repository

git clone https://github.com/YOUR-USERNAME/YOUR-REPO.git
cd YOUR-REPO

---

### 🔹 2. Backend Setup

cd TaskBoard.Api

Restore packages:
dotnet restore

Apply migrations:
dotnet ef migrations add InitialCreate
dotnet ef database update

Run backend:
dotnet run

👉 API runs at: https://localhost:5001 (or similar)

---

### 🔹 3. Frontend Setup

cd task-board-ui

Install dependencies:
npm install

Run frontend:
npm start

👉 App runs at: http://localhost:3000

---

## 🔹 Database Seeding

On first run, the database is automatically seeded with sample Projects, Tasks, and Comments.

---

## 🔹 API Configuration

Update API base URL in:

src/services/api.js

Example:
https://localhost:5001/api

---

## 🔗 API Endpoints

### Projects

* GET /api/projects
* POST /api/projects
* GET /api/projects/{id}
* PUT /api/projects/{id}
* DELETE /api/projects/{id}

### Tasks

* GET /api/projects/{projectId}/tasks
* POST /api/projects/{projectId}/tasks
* GET /api/tasks/{id}
* PUT /api/tasks/{id}
* DELETE /api/tasks/{id}

### Comments

* GET /api/tasks/{taskId}/comments
* POST /api/tasks/{taskId}/comments
* DELETE /api/comments/{id}

### Dashboard

* GET /api/dashboard

---

## ✨ Features Implemented

* CRUD operations for Projects, Tasks, Comments
* Task filtering (status, priority)
* Sorting (due date, priority, created date)
* Pagination support
* Dashboard summary (tasks by status, overdue, upcoming)
* Global error handling middleware
* DTO-based API design
* SQLite persistence with EF Core
* Clean layered architecture

---

## ⚠️ Assumptions

* Basic UI is implemented focusing on functionality
* Validation handled via Data Annotations
* API base URL is configurable

---

## 📌 Notes

* Controllers are thin — logic handled in Services
* Auto timestamps handled in DbContext
* Cascade delete configured

---

## 🙌 Author

Tejas Pathre
.NET Developer
