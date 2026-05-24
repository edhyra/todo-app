# Todo App (TodoManagementApp)

Simple Windows Forms Todo management application using MongoDB for storage and access-code-based employee login.

**Prerequisites:**

- Windows 10 or 11 (recommended)
- .NET 8 SDK installed (https://dotnet.microsoft.com/en-us/download)
- MongoDB 6.x running locally or Docker installed to run MongoDB in a container
- (Optional) Visual Studio 2022/2023 or Visual Studio Code for development

**Repository layout (important files):**

- `TodoManagementApp/` - main WinForms application project
- `mongo/data/` - local MongoDB data directory (do NOT commit this)
- `TodoManagementApp/Config/appsettings.json` - application configuration (Mongo connection, DB name, admin seed)

## Run locally (development)

1. Ensure MongoDB is running on `mongodb://127.0.0.1:27017` (or update `Config/appsettings.json`).

   - Start MongoDB with Docker (recommended for testing):

```powershell
docker run --name todo-mongo -p 27017:27017 -v ${PWD}/mongo/data:/data/db -d mongo:6.0
```

2. Build and run the app from the `TodoManagementApp` folder:

```powershell
cd TodoManagementApp
dotnet restore
dotnet build -c Release
dotnet run --configuration Release
```

The app reads configuration from `TodoManagementApp/Config/appsettings.json` (defaults present).

## Publish (Windows self-contained executable)

To produce a self-contained Windows x64 build:

```powershell
cd TodoManagementApp
dotnet publish -c Release -r win-x64 --self-contained true -o publish
# Run the produced executable in the `publish` folder (e.g. TodoManagementApp.exe)
```

## Git / GitHub

The repository remote for pushing is:

```
https://github.com/edhyra/todo-app.git
```

Example push workflow:

```bash
git add .
git commit -m "Add terminate UI, .gitignore, README"
git remote add origin https://github.com/edhyra/todo-app.git
git push -u origin main
```

Important: Do NOT commit the `mongo/data/` directory or any database files. The repository includes a `.gitignore` that excludes `/mongo/data/`, `bin/`, and `obj/` folders.

## Notes & Troubleshooting

- If the app cannot connect to MongoDB, verify the connection string in `TodoManagementApp/Config/appsettings.json` and ensure MongoDB is running.
- The manager UI now includes a "Terminate Employee" action which sets the employee's `Active` flag to `false` (displayed as `Inactive`).
