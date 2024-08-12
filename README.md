# .NET Zeebe Worker for Camunda 8

Source code for the tutorial around Zeebe Workers written in .NET.
This project contains a simple worker that can connect a BPMN service task.

Requirements:

* .NET Core >= 3.1
* IDE (Visual Studio, VSCode, or similar)

## How to run:
Build and run the .NET application via your favorite IDE. If you prefer using a terminal, run ```dotnet build``` and then ```dotnet run```

### Useful information
- This project's target framework is **.Net Core 8.0**
- It uses nuget package that is using [zeebe-client-csharp](https://github.com/camunda-community-hub/zeebe-client-csharp) for the camunda-8-access-layer
- You need a valid C8 client credentials to connect this client with Camunda 8.
- A more complex example can be found here [camunda-8-dotnet-guide](https://github.com/Hafflgav/camunda-8-dotnet-guide), adjust the version of .NET in that project, as it uses an older one.