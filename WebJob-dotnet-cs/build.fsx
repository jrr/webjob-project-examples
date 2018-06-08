#r "paket: nuget Fake.Core.Target prerelease //"
#r "paket: nuget Fake.Azure.Kudu //"
#r "paket: nuget Fake.IO.FileSystem //"
#r "paket: nuget Fake.IO.Zip //"
#r "paket: nuget Fake.DotNet.Cli //"

#load "./.fake/build.fsx/intellisense.fsx"

open Fake.Core
open Fake.Azure
open Fake.DotNet
open Fake.IO
open Fake.IO.Globbing.Operators

(*

This script is meant for a developer to locally deploy just one webjob, in order to rapidly iterate on it.

To set up FAKE:

 - install .net core >= 2.1
 - dotnet tool install fake-cli -g --version=5.0.0-*

To deploy the webjob:

 - Fill in the url, user, and password below. You can obtain them from Azure Portal, or with Azure-CLI:
   az webapp deployment list-publishing-profiles -g myresourcegroup --name my-app-service
 - fake build -t deploy

*)

let appServiceUrl = "https://<FILL-ME-IN>.scm.azurewebsites.net"
let publishProfileUser = "<FILL-ME-IN>"
let publishProfilePassword = "<FILL-ME-IN>"

(*
    Future todo:
    - fetch publishing profile programmatically
    - handle both debug and release (currently hardcoded debug)
    - avoid hardcoding other things: project file, path to published files, webjob name
*)

Target.create "Clean" (fun _ ->
  ["bin";"obj"] |> Shell.deleteDirs
  "out.zip" |> File.delete
  Shell.Exec ("dotnet", "clean") |> ignore
)

Target.create "Build" (fun _ ->
  Trace.log " --- Building the app --- "
  Fake.DotNet.DotNet.publish (fun a -> {a with Configuration = DotNet.BuildConfiguration.Debug}) "WebJob-dotnet-cs.csproj"
)

Target.create "Deploy" (fun _ ->
  Trace.log " --- Deploying app --- "

  let url = new System.Uri(appServiceUrl)
  
  let files = !! "bin/Debug/net471/publish/**/*"
  Trace.log (sprintf "zipping %d files" (files |> Seq.toList |> List.length))

  // https://fake.build/apidocs/v5/fake-io-zip.html

  [ @"app_data\jobs\continuous\WebJob-dotnet-cs", files ] |> Fake.IO.Zip.zipOfIncludes "out.zip"

  // todo: get these programmatically?
  let deployParams :Kudu.ZipDeployParams = {
                                              PackageLocation="out.zip"
                                              UserName = publishProfileUser
                                              Password = publishProfilePassword
                                              Url=url
                                           }
  Fake.Azure.Kudu.zipDeploy deployParams
)

open Fake.Core.TargetOperators

// *** Define Dependencies ***
"Clean"
  ==> "Build"
  ==> "Deploy"

// *** Start Build ***
Target.runOrDefault "Build"
