using System;
using Topshelf;

namespace FileOrganizer
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<FileOrganizer>(s =>
                {
                    s.ConstructUsing(fileOrganizer => new FileOrganizer());
                    s.WhenStarted(fileOrganizer => fileOrganizer.Start());
                    s.WhenStopped(fileOrganizer => fileOrganizer.Stop());
                });

                x.RunAsLocalSystem();

                x.SetServiceName("FileOrganizer");
                x.SetDisplayName("File Organizer Service");
                x.SetDescription("A service for file organization.");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}