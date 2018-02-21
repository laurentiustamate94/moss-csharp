using System;
using System.Linq;
using Moss.Core.Models;
using Moss.Standard;
using Moss.Standard.Models;

namespace Moss.Core
{
    internal class Program
    {
        //TODO Find a better way to do this
        public static readonly string _languagesDelimitedByComma = "c,cc,java,ml,pascal,ada,lisp,scheme,haskell,fortran,ascii,vhdl,perl,matlab,python,mips,prolog,spice,vb,csharp,modula2,a8086,javascript,plsql";

        public static void Main(string[] args)
        {
            if (args.Contains("--display-languages"))
            {
                Console.WriteLine($"Available languages:\n\t{_languagesDelimitedByComma.Replace(",", "\n\t")}");

                return;
            }

            if (args.Contains("--display-usage"))
            {
                Console.WriteLine("Usage: moss [-x] [-l language] [-d] [-b basefile1] ... [-b basefilen] [-m #] [-c \"string\"] file1 file2 file3 ...");

                return;
            }

            var options = new CmdOptions();

            if (!CommandLine.Parser.Default.ParseArguments(args, options))
            {
                return;
            }

            var socket = new MossSocket(options.UserId);
            var result = socket.SendRequest(new MossRequest
            {
                BaseFileNames = options.BaseFileNames,
                Comments = options.Comments,
                FileNames = options.FileNames,
                IsDirectoryMode = options.IsDirectoryMode,
                IsExperimental = options.IsExperimental,
                Language = options.Language,
                MaximumFilesToShow = options.MaximumFilesToShow,
                MaximumMatches = options.MaximumMatches
            });

            if (!result.IsValid)
            {
                Console.WriteLine("Something went wrong");

                return;
            }

            Console.WriteLine($"URL: {result.ResultUrl}");

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
