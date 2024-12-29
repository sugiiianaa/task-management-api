
# Task Management Api

---

This project is a self learning project to create simple *CRUD* operation with authentication sytem using .NET.
I try my *best* to use DDD on the development process, but I know that this project isn't completely right on the implementation, but at least I try my best.
I also use Entity Framework and PostgreSQL for this project.

---

## Project Structure

```
|
| - TaskManagement.Application
|
|     --- Interface
|     --- Models
|     --- Services
|
| - TaskManagement.Domain
|
|     --- Dtos
|     --- Entities
|     --- Enums
|
| - TaskManagement.Infrastucture
|
|     --- Interface
|     --- Migrations
|     --- Persistence
|     --- Repositories
|
| - TaskManagement.API
|
|     --- Endpoints
|     --- Enums
|     --- Extentsions
|     --- Helper
|     --- Middleware
|     --- Models
|     --- Program.cs
```

## How to run locally

1. You should install PostgreSQL on your local machine and .Net framework
   You could see the istall them through here :
   
   - [PostgreSQL installation](https://www.postgresql.org/)
   - [.Net installation](https://learn.microsoft.com/en-us/dotnet/core/install/)
   
2. If you already install the prerequisite tools, you could open the project through visual studio.

3. Go to TaskManagementAPI and then create `appsettings.json` you could copy all the value needed from `appsettings.Example.json` and adjust it with your own settings.

4. You could run the project by pressing `F5` or use this button : 
<img width="959" alt="image" src="https://github.com/user-attachments/assets/f6aee2cf-1a1f-4d7d-8556-def95e37e6ae" />

5. The project should be running.

## Message for the readers

If you look through my code and think something should be fixed or could be improved you could open an `issue` or `pull request` *OR* you could email me through : 
sugiiianaa@gmail.com. 

Any suggestion and recommondation for improving the project would be really apreciated!
