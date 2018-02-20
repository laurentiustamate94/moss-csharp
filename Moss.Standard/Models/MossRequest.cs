using System.Collections.Generic;

namespace Moss.Standard.Models
{
    /// <summary>
    /// Models the MOSS (Measure Of Software Similarity) request
    /// (http://moss.stanford.edu/general/scripts/mossnet)
    /// 
    /// All comments (except for the FileNames property) are copied as-is
    /// from the MOSS documentation (http://moss.stanford.edu/general/scripts/mossnet)
    /// </summary>
    public sealed class MossRequest
    {
        /// <summary>
        /// The -l option specifies the source language of the tested programs.
        /// Moss supports many different languages;
        /// see the variable "languages" below for the full list.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// The -d option specifies that submissions are by directory, not by file.
        /// That is, files in a directory are taken to be part of the same program,
        /// and reported matches are organized accordingly by directory.
        /// </summary>
        public bool IsDirectoryMode { get; set; }

        /// <summary>
        /// The -b option names a "base file".  Moss normally reports all code
        /// that matches in pairs of files.  When a base file is supplied,
        /// program code that also appears in the base file is not counted in matches.
        /// A typical base file will include, for example, the instructor-supplied 
        /// code for an assignment.  Multiple -b options are allowed.  You should 
        /// use a base file if it is convenient; base files improve results, but 
        /// are not usually necessary for obtaining useful information. 
        /// 
        /// IMPORTANT: Unlike previous versions of moss, the -b option *always*
        /// takes a single filename, even if the -d option is also used.
        /// </summary>
        public IEnumerable<string> BaseFileNames { get; set; }

        /// <summary>
        /// The -m option sets the maximum number of times a given passage may appear
        /// before it is ignored.  A passage of code that appears in many programs
        /// is probably legitimate sharing and not the result of plagiarism.  With -m N,
        /// any passage appearing in more than N programs is treated as if it appeared in 
        /// a base file (i.e., it is never reported).  Option -m can be used to control
        /// moss' sensitivity.  With -m 2, moss reports only passages that appear
        /// in exactly two programs.  If one expects many very similar solutions
        /// (e.g., the short first assignments typical of introductory programming
        /// courses) then using -m 3 or -m 4 is a good way to eliminate all but
        /// truly unusual matches between programs while still being able to detect
        /// 3-way or 4-way plagiarism.  With -m 1000000 (or any very 
        /// large number), moss reports all matches, no matter how often they appear.  
        /// 
        /// The -m setting is most useful for large assignments where one also a base file 
        /// expected to hold all legitimately shared code.  The default for -m is 10.
        /// </summary>
        public int MaximumMatches { get; set; }

        /// <summary>
        /// The -c option supplies a comment string that is attached to the generated
        /// report.  This option facilitates matching queries submitted with replies
        /// received, especially when several queries are submitted at once.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// The -n option determines the number of matching files to show in the results.
        /// The default is 250.
        /// </summary>
        public int MaximumFilesToShow { get; set; }

        /// <summary>
        /// The -x option sends queries to the current experimental version of the server.
        /// The experimental server has the most recent Moss features and is also usually
        /// less stable (read: may have more bugs).
        /// </summary>
        public bool IsExperimental { get; set; }

        /// <summary>
        /// The files that will be checked for software similarities
        /// </summary>
        public IEnumerable<string> FileNames { get; set; }
    }
}
