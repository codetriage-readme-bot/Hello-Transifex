

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
            .AppendQuoted("--minimum-perc=80") // Only download a translation if 80% of it is translated
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

Task("Build")
  .IsDependentOn("Clean")
  .IsDependentOn("Download-Translations")
  .Does(() =>
{
    DotNetBuild("./Hello-Transifex.sln", (conf) => conf.SetConfiguration(configuration));
});

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);