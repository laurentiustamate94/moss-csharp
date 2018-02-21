using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace Moss.Core.Models
{
    internal class CmdOptions
    {
        [Option(
            'u',
            "user-id",
            Required = true,
            HelpText = "The user id used for authentication")]
        public string UserId { get; set; }

        [Option(
            'l',
            "language",
            Required = true,
            HelpText = "The language used in the source files")]
        public string Language { get; set; }

        [Option(
            "display-languages",
            Required = false,
            HelpText = "Display all languages accepted")]
        public bool DisplayAllLanguages { get; set; }

        [Option(
            "display-usage",
            Required = false,
            HelpText = "Display example usage")]
        public bool DisplayUsage { get; set; }

        [Option(
            'd',
            "directory",
            Required = false,
            HelpText = "Whether or not submissions are by directory and not by file")]
        public bool IsDirectoryMode { get; set; }

        [Option(
            'b',
            "base-file",
            Required = false,
            HelpText = "Names a source file as a base file")]
        public IEnumerable<string> BaseFileNames { get; set; }

        [Option(
            'm',
            "maxmatches",
            Required = false,
            DefaultValue = 10,
            HelpText = "The maximum number of times a given passage may appear")]
        public int MaximumMatches { get; set; }

        [Option(
            'c',
            "comment",
            Required = false,
            HelpText = "A string that is attached to the report")]
        public string Comments { get; set; }

        [Option(
            'n',
            "show",
            Required = false,
            DefaultValue = 250,
            HelpText = "The number of matching files to show in the results")]
        public int MaximumFilesToShow { get; set; }

        [Option(
            'x',
            "experimental",
            Required = false,
            HelpText = "Sends requests to the current experimental version")]
        public bool IsExperimental { get; set; }

        [ValueList(typeof(List<string>))]
        public IEnumerable<string> FileNames { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
