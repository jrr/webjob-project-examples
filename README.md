# WebJob Project Examples

Working examples of different WebJob project types:
 - **WebJob-Traditional** - created with VS2017, this uses the legacy project format (beginning with `<?xml version="1.0" encoding="utf-8"?>`), but has been updated to use `<PackageReference>` instead of packages.config.
 - **WebJob-dotnet-cs** - based on a console app created with `dotnet new`, this uses the new project format (beginning with `<Project Sdk="Microsoft.NET.Sdk">`)
 - **WebJob-dotnet-fs** - same as above, but in F#

**WebApplication1** is an empty ASP app that contains references to each of the webjobs, and can be deployed from VS2017 with right-click -> Publish.
