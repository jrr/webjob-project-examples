# WebJob Project Examples

Working examples of different [WebJobs](https://github.com/Azure/azure-webjobs-sdk/) project types, including SDK-style project files and F#, as discussed [here](https://spin.atomicobject.com/2018/06/28/azure-webjobs-f-sharp/). Note that these target WebJobs 2.x on .NET Framework, not the new .NET Core 3.x betas.

 - **WebApplication1** - An empty ASP app that contains references to each of the webjobs, and can be deployed from VS2017 with _right-click -> Publish_. (the webjobs will be deployed along with it)
 - **WebJob-Traditional** - Created with VS2017, this uses the legacy project format (beginning with `<?xml version="1.0" encoding="utf-8"?>`), but has been updated to use `<PackageReference>` instead of packages.config. In Visual Studio it will have a _right-click -> Publish as Azure WebJob_.
 - **WebJob-dotnet-fs** - Based on a console app created with `dotnet new`, this F# WebJob uses the new project format (beginning with `<Project Sdk="Microsoft.NET.Sdk">`). It doesn't get a _Publish as Azure WebJob_ option in Visual Studio, but it will happily ride along with WebApplication1's deployment.
 - **WebJob-dotnet-cs** - Same as above, but in C#. This project also includes a [FAKE](https://github.com/fsharp/FAKE) script for individual deployment, to make up for the loss of _Publish as Azure WebJob_.

