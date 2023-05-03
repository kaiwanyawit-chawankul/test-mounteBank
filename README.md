# How this repo works
## C# project to call to the external service
```
    dotnet new sln -n DemoMounteBank
    dotnet new xunit -n DemoMounteBank.Test -o test/DemoMounteBank.Test
    dotnet add test/DemoMounteBank.Test package FluentAssertions
    dotnet sln add test/DemoMounteBank.Test
    dotnet test
```


## Testing service
## Getting mock request from mounteBank
## Testing service with mounteBank