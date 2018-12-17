///////////////////////////////////////////////////////////////////////////////
// Directives
///////////////////////////////////////////////////////////////////////////////

#tool nuget:?package=NUnit.ConsoleRunner&version=3.5.0

#l "versionUtils.cake"
#l "settingsUtils.cake"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");
var settingsFile = Argument<string>("settingsFile", ".\\settings.json");
var skipBuild = Argument<string>("skipBuild", "false").ToLower() == "true" || Argument<string>("skipBuild", "false") == "1";

var buildSettings = SettingsUtils.LoadSettings(Context, settingsFile);
var versionInfo = VersionUtils.LoadVersion(Context, buildSettings);

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

var solutions = GetFiles(buildSettings.Build.SolutionFilePath);
var solutionPaths = solutions.Select(solution => solution.GetDirectory());

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup((c) =>
{
	c.Information("Command Line:");
	c.Information("\tConfiguration: {0}", configuration);
	c.Information("\tSettings Files: {0}", settingsFile);
	c.Information("\tSkip Build: {0}", skipBuild);
	c.Information("\tSolutions Found: {0}", solutions.Count);

    // Executed BEFORE the first task.
	buildSettings.Display(c);
	versionInfo.Display(c);
});

Teardown((c) =>
{
    // Executed AFTER the last task.
    Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Description("Cleans all directories that are used during the build process.")
	.WithCriteria(!skipBuild)
    .Does(() =>
{
    // Clean solution directories.
    foreach(var path in solutionPaths)
    {
        Information("Cleaning {0}", path);
        CleanDirectories(path + "/**/bin/" + configuration);
        CleanDirectories(path + "/**/obj/" + configuration);
    }
});

Task("CleanAll")
    .Description("Cleans all directories that are used during the build process.")
    .Does(() =>
{
    // Clean solution directories.
    foreach(var path in solutionPaths)
    {
			Information("Cleaning {0}", path);
			CleanDirectories(path + "/**/bin");
			CleanDirectories(path + "/**/obj");
			CleanDirectories(path + "/packages/**/*");
			CleanDirectories(path + "/artifacts/**/*");
			CleanDirectories(path + "/packages");
			CleanDirectories(path + "/artifacts");
    }
});

Task("CleanPackages")
    .Description("Cleans all packages that are used during the build process.")
    .Does(() =>
{
    // Clean solution directories.
    foreach(var path in solutionPaths)
    {
        Information("Cleaning {0}", path);
		CleanDirectories(path + "/packages/**/*");
		CleanDirectories(path + "/packages");
    }
});

Task("Restore")
    .Description("Restores all the NuGet packages that are used by the specified solution.")
	.WithCriteria(!skipBuild)
    .Does(() =>
{
    // Restore all NuGet packages.
    foreach(var solution in solutions)
    {
        Information("Restoring {0}...", solution);
		var restoreSettings = new NuGetRestoreSettings {
			 ConfigFile = buildSettings.Build.NugetConfigPath,
			 ToolPath = new FilePath("./.nuget/nuget.exe")
		};
		Information("Nuget path: {0}", restoreSettings.ToolPath);
        NuGetRestore(solution, restoreSettings);
    }
});

Task("Build")
    .Description("Builds all the different parts of the project.")
	.WithCriteria(!skipBuild)
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
	.IsDependentOn("UpdateVersion")
    .Does(() =>
{
	if (buildSettings.Version.AutoIncrementVersion)
	{
		RunTarget("IncrementVersion");
	}

    // Build all solutions.
    foreach(var solution in solutions)
    {
        Information("Building {0}", solution);
		var msBuildSettings = new MSBuildSettings {
			MaxCpuCount = 1,
			Configuration = configuration,
			PlatformTarget = PlatformTarget.MSIL,
			ToolVersion = MSBuildToolVersion.VS2017,
			MSBuildPlatform = (Cake.Common.Tools.MSBuild.MSBuildPlatform)1,
//			Verbosity = Verbosity.Diagnostic
		}.WithProperty("TreatWarningsAsErrors",buildSettings.Build.TreatWarningsAsErrors.ToString())
		 .WithTarget("Build");

		msBuildSettings.WithProperty("AppxPackage", "false");

		if (buildSettings.Build.EnableXamarinIOS)
		{
			// Mac build host connection properties
			msBuildSettings.WithProperty("ServerAddress", buildSettings.Build.MacAgentIPAddress);
			msBuildSettings.WithProperty("ServerUser", buildSettings.Build.MacAgentUserName);
			msBuildSettings.WithProperty("ServerPassword", buildSettings.Build.MacAgentUserPassword);
		}

        MSBuild(solution, msBuildSettings);
    }
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
	var nUnitSettings = new NUnit3Settings {
        NoResults = false
	};
	var testFileSpec = buildSettings.Test.SourcePath + "/**/bin/" + configuration + "/" + buildSettings.Test.AssemblyFileSpec;
	Information("Testing {0}", testFileSpec);
    NUnit3(testFileSpec, nUnitSettings);
});

Task("Package")
    .Description("Packages all nuspec files into nupkg packages.")
    .IsDependentOn("Build")
    //.IsDependentOn("Run-Unit-Tests")
    .Does(() =>
{
	var artifactsPath = Directory(buildSettings.NuGet.ArtifactsPath);
	var nugetProps = new Dictionary<string, string>() { {"Configuration", configuration} };

	CreateDirectory(artifactsPath);

	var nuspecFiles = GetFiles(buildSettings.NuGet.NuSpecFileSpec).OrderBy(filePath => filePath.FullPath.Length);
	foreach(var nsf in nuspecFiles)
	{
		Information("Packaging {0}", nsf);

		if (buildSettings.NuGet.UpdateVersion) {
			VersionUtils.UpdateNuSpecVersion(Context, buildSettings, versionInfo, nsf.FullPath);
		}

		VersionUtils.UpdateNuSpecVersionDependency(Context, buildSettings, versionInfo, nsf.FullPath);

		NuGetPack(nsf, new NuGetPackSettings {
			Version = versionInfo.ToString(),
			ReleaseNotes = versionInfo.ReleaseNotes,
			Symbols = true,
			Properties = nugetProps,
			OutputDirectory = artifactsPath
		});
	}
});

Task("Publish")
    .Description("Publishes all of the nupkg packages to the nuget server. ")
    .IsDependentOn("Package")
    .Does(() =>
{
	var nupkgFiles = GetFiles(buildSettings.NuGet.NuGetPackagesSpec);
	foreach(var pkg in nupkgFiles)
	{
		// Lets skip everything except the current version and we can skip the symbols pkg for now
		if (!pkg.ToString().Contains(versionInfo.ToString()) || pkg.ToString().Contains("symbols")) {
			Information("Skipping {0}", pkg);
			continue;
		}

		Information("Publishing {0}", pkg);

		var nugetSettings = new NuGetPushSettings {
			Source = buildSettings.NuGet.FeedUrl,
			ConfigFile = buildSettings.NuGet.NuGetConfig,
			Verbosity = NuGetVerbosity.Detailed
		};

		if (!string.IsNullOrEmpty(buildSettings.NuGet.FeedApiKey))
		{
			nugetSettings.ApiKey = buildSettings.NuGet.FeedApiKey;
		}

		NuGetPush(pkg, nugetSettings);
	}
});


Task("UnPublish")
    .Description("UnPublishes all of the current nupkg packages from the nuget server. Issue: versionToDelete must use : instead of . due to bug in cake")
    .Does(() =>
{
	var v = Argument<string>("versionToDelete", versionInfo.ToString()).Replace(":",".");

	var nuspecFiles = GetFiles(buildSettings.NuGet.NuSpecFileSpec);
	foreach(var f in nuspecFiles)
	{
		Information("UnPublishing {0}", f.GetFilenameWithoutExtension());

		var args = string.Format("delete {0} {1} -Source {2} -NonInteractive",
								f.GetFilenameWithoutExtension(),
								v,
								buildSettings.NuGet.FeedUrl
								);

		if (buildSettings.NuGet.FeedApiKey != "VSTS" ) {
			args = args + string.Format(" -ApiKey {0}", buildSettings.NuGet.FeedApiKey);
		}

		if (!string.IsNullOrEmpty(buildSettings.NuGet.NuGetConfig)) {
			args = args + string.Format(" -Config {0}", buildSettings.NuGet.NuGetConfig);
		}

		Information("NuGet Command Line: {0}", args);
		using (var process = StartAndReturnProcess("tools\\nuget.exe", new ProcessSettings {
			Arguments = args
		}))
		{
			process.WaitForExit();
			Information("nuget delete exit code: {0}", process.GetExitCode());
		}
	}
});

Task("UpdateVersion")
	.Description("Updates the version number in the necessary files")
	.Does(() =>
{
	Information("Updating Version to {0}", versionInfo.ToString());

	VersionUtils.UpdateVersion(Context, buildSettings, versionInfo);
});

Task("IncrementVersion")
	.Description("Increments the version number and then updates it in the necessary files")
	.IsDependentOn("UpdateVersion")
	.Does(() =>
{
	var oldVer = versionInfo.ToString();
	if (versionInfo.IsPreRelease) versionInfo.PreRelease++; else versionInfo.Build++;

	Information("Incrementing Version {0} to {1}", oldVer, versionInfo.ToString());
	VersionUtils.UpdateVersion(Context, buildSettings, versionInfo);
});

Task("BuildNewVersion")
	.Description("Increments and Builds a new version")
	.IsDependentOn("IncrementVersion")
	.IsDependentOn("Build")
	.Does(() =>
{
});

Task("PublishNewVersion")
	.Description("Increments, Builds, and publishes a new version")
	.IsDependentOn("BuildNewVersion")
	.IsDependentOn("Publish")
	.Does(() =>
{
});

Task("DisplaySettings")
    .Description("Displays All Settings.")
    .Does(() =>
{
	// Settings will be displayed as they are part of the Setup task
});


///////////////////////////////////////////////////////////////////////////////
// TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
    .Description("This is the default task which will be ran if no specific target is passed in.")
    .IsDependentOn("Build");

///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);
