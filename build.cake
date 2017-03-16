

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
    var settings = new ProcessSettings().WithArguments((builder) =>
        builder.Append("pull")
            //.AppendQuoted("--language=<lang>") // Select what languages should be pulled
            //.AppendQuoted("--resource=Resources.resx") // Only download translations for resource
            .Append("--all") // Download all translations, both new and old
            //.Append("--source") // Download the source file
            //.Append("--force") // Force download of translations files
            //.Append("--skip") // Don't stop on errors.
            //.Append("--disable-overwrite") // Don't overwrite existing translations
            .AppendQuoted("--minimum-perc=60") // Only download a translation if 60% of it is translated
            //.Append("--pseudo") // Download the pseudo file (Used when translating outside of transifex)
            .AppendQuoted("--mode=reviewed") // Only download reviewed strings, possible values: http://bit.ly/pullmode
            //.Append("--xliff") // Download the file as xliff
        );
    
    var exitCode = StartProcess("tx", settings);
    if (exitCode != 0)
    {
        throw new Exception("Download of translation files failed");
    }
});

// This task can be used if you need to
// push out the source translation file
// to transifex (since transifex only auto-updates once per day)
Task("Upload-Translation-Source")
  .Does(() =>
{
    var settings = new ProcessSettings().WithArguments((builder) =>
      builder.Append("push")
        //.Append("--languages=<langs>") // Specify which translations you want to push (defaults to all)
        //.Append("--resource=Resource.resx") // Specify the resource for which you want to push the translations (defaults to all)
        //.Append("--force") // Push source files without checking modification times.
        //.Append("--skip") // Don't stop on errors.
        .Append("--source") // Push the source file to the server
        .Append("--translations") // Push the translation files to the server
        //.Append("--no-interactive") // Don't require user input when forcing a push
        //.Append("--xliff") // Aply this option to upload file as xliff
    );

    var exitCode = StartProcess("tx", settings);
    if (exitCode != 0)
    {
        throw new Exception("Uploading source translations failed");
    }
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