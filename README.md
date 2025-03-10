# Clean Architecture API

This is just a project to get a better understanding of clean architecture and domain driven design.

Helper notes:
    - When adding a new migration, run it in the infrastructure directory and append the startup project like this
```bash
    dotnet ef migrations add Initial --startup-project ../CleanArchitectureApi.Api
```