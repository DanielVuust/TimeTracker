# Template

## Migrations

Create tables for database go to directory "src/TimeTracker/TimeTracker.Infrastructure" to run EF core migraiton commands

```Powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef database update --verbose --context TimeTrackerContext --project . --startup-project ../TimeTracker.Api.Service
```


```shell
ASPNETCORE_ENVIRONMENT=Development dotnet ef database update --verbose --context TimeTrackerContext --project . --startup-project ../TimeTracker.Api.Service
```


To add a new migration:

```shell
ASPNETCORE_ENVIRONMENT=Development dotnet ef migrations add AddUser --context TimeTrackerContext --project . --startup-project ../TimeTracker.Api.Service
```

```Powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef migrations add AddTimeTracker --context TimeTrackerContext --project . --startup-project ../TimeTracker.Api.Service
```

To remove latest added:
```shell
ASPNETCORE_ENVIRONMENT=Development dotnet ef migrations remove --context TimeTrackerContext --project . --startup-project ../TimeTracker.Api.Service
```

```Powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef migrations remove --context TimeTrackerContext --project . --startup-project ../TimeTracker.Api.Service
```
