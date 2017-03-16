

var configuration = Argument("configuration", "Release");
var target        = Argument("target", "Default");

Task("Clean")
  .Does(() =>
{
    DotNetBuild("./Hello-Transifex.sln", (conf) => conf.SetConfiguration(configuration).WithTarget("Clean"));
});

Task("Build")
  .IsDependentOn("Clean")
  .Does(() =>
{
    DotNetBuild("./Hello-Transifex.sln", (conf) => conf.SetConfiguration(configuration));
});

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);