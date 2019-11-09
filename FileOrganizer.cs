using System;
using System.Timers;
using System.IO;
using System.Linq;

namespace FileOrganizer
{
    class FileOrganizer
    {

        private readonly Timer _timer;

        //private readonly string home = System("user.home");
        private readonly string mainPath = KnownFolders.GetPath(KnownFolder.Downloads);

        //private readonly string logPath = @"\FileOrganizerLogs.txt";

        //private readonly string newPath = Path.GetFullPath(Path.Combine(path,@"..","..",".."));
        public FileOrganizer()
        {
            //_timer = new Timer(86400000) { AutoReset = true };
            _timer = new Timer(10000) { AutoReset = true };
            _timer.Elapsed += TimerElapsed;
        }
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {

            string[] allfiles = Directory.GetFiles(mainPath)
                                      .Select(Path.GetFileName)
                                      .ToArray();

            for (int i = 0; i < allfiles.Length; i++)
            {
                string sourcefile = mainPath + '\\' + allfiles[i];

                string creation = File.GetCreationTime(sourcefile).Date.ToString("dd.MM.yyyy");
                string directoryPath = mainPath + "\\" + creation;
                string directoryFile = directoryPath + "\\" + allfiles[i];

                try
                {
                    if (!Directory.Exists(directoryPath))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(directoryPath);
                        //string directoryLog = "The directory " + directoryPath + " was created successfully at " + Directory.GetCreationTime(directoryPath) + "\n";
                        //File.AppendAllText(logPath, directoryLog);
                    }

                    System.IO.File.Move(sourcefile, directoryFile);
                    //string log = "Source File " + sourcefile + " moved to Directory File " + directoryPath + " at " + Directory.GetCreationTime(directoryPath) + "\n";
                    //File.AppendAllText(logPath, log);
                }
                catch (Exception ex)
                {
                    //File.AppendAllText(logPath, ex.ToString());
                }
            }
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
