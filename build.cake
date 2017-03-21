#addin "nuget:?package=Cake.Transifex"

var configuration = Argument("configuration", "Release");
var target        = Argument("target", "Default");

Task("Clean")
  .Does(() =>
{
    DotNetBuild("./Hello-Transifex.sln", (conf) => conf.SetConfiguration(configuration).WithTarget("Clean"));
});

// Download all transifex Translations
// Probably should have a condition to only
// run on appveyor, and on certain branches
// or if the .transifexrc file exists in
// The user profile directory
Task("Download-Translations")
  .Does(() =>
{
    TransifexPull(new TransifexPullSettings
    {
        All = true,
        MinimumPercentage = 60,
        Mode = TransifexMode.Reviewed
    });
});

// This task can be used if you need to
// push out the source translation file
// to transifex (since transifex only auto-updates once per day)
Task("Upload-Translations")
  .Does(() =>
{
    TransifexPush(new TransifexPushSettings
    {
        Source = true,
        Translations = true
    });
});

Task("Build")
  .IsDependentOn("Clean")
  .IsDependentOn("Download-Translations")
  .Does(() =>
{
    DotNetBuild("./Hello-Transifex.sln", (conf) => conf.SetConfiguration(configuration));
});

Task("Create-ZipFile")
  .IsDependentOn("Build")
  .Does(() =>
{
    Zip("./src/Hello-Transifex/bin/" + configuration, "./Hello-Transifex-bin.zip");
});

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);