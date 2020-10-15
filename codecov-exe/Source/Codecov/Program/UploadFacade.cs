using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Codecov.Coverage.EnviornmentVariables;
using Codecov.Coverage.Report;
using Codecov.Coverage.SourceCode;
using Codecov.Coverage.Tool;
using Codecov.Factories;
using Codecov.Services;
using Codecov.Services.ContinuousIntegrationServers;
using Codecov.Services.VersionControlSystems;
using Codecov.Terminal;
using Codecov.Upload;
using Codecov.Url;
using Codecov.Utilities;
using Codecov.Yaml;
using Serilog;

namespace Codecov.Program
{
    internal class UploadFacade
    {
        public UploadFacade(CommandLineOptions commandLineOptions)
        {
            CommandLineCommandLineOptions = commandLineOptions;
            var envVars = new EnviornmentVariables(commandLineOptions);
            ContinuousIntegrationServer = ContinuousIntegrationServerFactory.Create(envVars);
            envVars.LoadEnviornmentVariables(ContinuousIntegrationServer);
            EnviornmentVariables = envVars;
        }

        private static IDictionary<TerminalName, ITerminal> Terminals => TerminalFactory.Create();

        private ICoverage Coverage => new Coverage.Tool.Coverage(CommandLineCommandLineOptions);

        private IUrl Url => new Url.Url(new Host(CommandLineCommandLineOptions, EnviornmentVariables), new Route(), new Query(CommandLineCommandLineOptions, Repositories, ContinuousIntegrationServer, Yaml, EnviornmentVariables));

        private IYaml Yaml => new Yaml.Yaml(SourceCode);

        private CommandLineOptions CommandLineCommandLineOptions { get; }

        private IContinuousIntegrationServer ContinuousIntegrationServer { get; }

        private string DisplayUrl
        {
            get
            {
                var url = Url.GetUrl.ToString();
                var regex = new Regex(@"token=\w{8}-\w{4}-\w{4}-\w{4}-\w{12}&");
                return regex.Replace(url, string.Empty);
            }
        }

        private IEnviornmentVariables EnviornmentVariables { get; }

        private IReport Report => new Report(CommandLineCommandLineOptions, EnviornmentVariables, SourceCode, Coverage);

        private IEnumerable<IRepository> Repositories => RepositoryFactory.Create(VersionControlSystem, ContinuousIntegrationServer);

        private ISourceCode SourceCode => new SourceCode(VersionControlSystem);

        private IUpload Upload => new Uploads(Url, Report, CommandLineCommandLineOptions.Features);

        private IVersionControlSystem VersionControlSystem => VersionControlSystemFactory.Create(CommandLineCommandLineOptions, Terminals[TerminalName.Generic]);

        public void Uploader()
        {
            var ci = ContinuousIntegrationServer.GetType().Name;
            if (ci.Equals("ContinuousIntegrationServer"))
            {
                Log.Warning("No CI detected.");
            }
            else if (ci.Equals("TeamCity"))
            {
                Log.Information("{ci} detected.", ci);
                if (string.IsNullOrWhiteSpace(ContinuousIntegrationServer.Branch))
                {
                    Log.Warning("Teamcity does not automatically make build parameters available as environment variables.\nAdd the following environment parameters to the build configuration.\nenv.TEAMCITY_BUILD_BRANCH = %teamcity.build.branch%.\nenv.TEAMCITY_BUILD_ID = %teamcity.build.id%.\nenv.TEAMCITY_BUILD_URL = %teamcity.serverUrl%/viewLog.html?buildId=%teamcity.build.id%.\nenv.TEAMCITY_BUILD_COMMIT = %system.build.vcs.number%.\nenv.TEAMCITY_BUILD_REPOSITORY = %vcsroot.<YOUR TEAMCITY VCS NAME>.url%.");
                }
            }
            else
            {
                Log.Information("{ci} detected.", ci);
            }

            var vcs = VersionControlSystem.GetType().Name;
            if (vcs.Equals("VersionControlSystem"))
            {
                Log.Warning("No VCS detected.");
            }
            else
            {
                Log.Information("{vcs} detected.", vcs);
            }

            Log.Information("Project root: {RepoRoot}", VersionControlSystem.RepoRoot);
            if (string.IsNullOrWhiteSpace(Yaml.FileName))
            {
                Log.Information("Yaml not found, that's ok! Learn more at {CodecovUrl}", "https://docs.codecov.io/docs/codecov-yaml");
            }

            Log.Information("Reading reports.");
            Log.Information(string.Join("\n", Coverage.CoverageReports.Select(x => x.File)));

            if (EnviornmentVariables.UserEnvironmentVariables.Any())
            {
                Log.Information("Appending build variables");
                Log.Information(string.Join("\n", EnviornmentVariables.UserEnvironmentVariables.Select(x => x.Key.Trim()).ToArray()));
            }

            if (CommandLineCommandLineOptions.Dump)
            {
                Log.Warning("Skipping upload and dumping contents.");
                Log.Information("url: {GetUrl}", Url.GetUrl);
                Log.Information(Report.Reporter);
                return;
            }

            // We warn if the total file size is above 20 MB
            var fileSizes = Coverage.CoverageReports.Sum(x => FileSystem.GetFileSize(x.File));
            if (fileSizes > 20_971_520)
            {
                Log.Warning("Total file size of reports is above 20MB, this may prevent report being shown on {Host}", Url.GetUrl.Host);
                Log.Warning("Reduce the total upload size if this occurs");
            }

            Log.Information("Uploading Reports.");
            Log.Information("url: {Scheme}://{Authority}", Url.GetUrl.Scheme, Url.GetUrl.Authority);
            Log.Information("query: {DisplayUrl}", DisplayUrl);

            var response = Upload.Uploader();
            Log.Verbose("response: {response}", response);
            var splitResponse = response.Split('\n');
            if (splitResponse.Length > 1)
            {
                var s3 = new Uri(splitResponse[1]);
                var reportUrl = splitResponse[0];
                Log.Information("Uploading to S3 {Scheme}://{Authority}", s3.Scheme, s3.Authority);
                Log.Information("View reports at: {reportUrl}", reportUrl);
            }
        }
    }
}
