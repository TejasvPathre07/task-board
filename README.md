# Task Board App

This is a simple task management project where you can create projects and add tasks inside them. You can also add comments to tasks and see some basic dashboard info.

## Tech Used

* ASP.NET Core Web API
* React
* SQLite
* Entity Framework Core

## How to Run

### Backend

Go to backend folder:

cd TaskBoard.Api

Run:

dotnet restore
dotnet ef database update
dotnet run

It will start on something like:
https://github.com/TejasvPathre07/task-board

---

### Frontend

Open another terminal:

cd task-board-ui

Run:

npm install
npm start

App will open at:
http://localhost:3000

---

## Note

* Start backend first, then frontend
* If API is not working, check the port in frontend

---

## Author

Tejas Pathre
