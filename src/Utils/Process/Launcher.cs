using Spectre.Console;
using System;
using System.Diagnostics;
using System.IO;
using Utils.Console;
using Utils.Types.String;

namespace Utils.Process
{
    public class Launcher
    {
        private static string _exePath;
        private readonly string _cmdArgs;
        public bool Finished { get; private set; }

        public Launcher(string exePath, string cmdArgs)
        {
            _exePath = exePath;
            _cmdArgs = cmdArgs;
        }

        public void Launch()
        {
            if (!_exePath.Valid())
                throw new InvalidOperationException("Path to exe null or empty!");
            var workDirPath = new FileInfo(_exePath).DirectoryName;
            if (workDirPath == null)
                throw new InvalidOperationException("Failed to obtain path to working directory!");

            try
            {
                var process = new System.Diagnostics.Process
                {
                    StartInfo =
                    {
                        FileName = _exePath,
                        Arguments = _cmdArgs,
                        WorkingDirectory = workDirPath,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    }
                };

                process.OutputDataReceived += OutputHandler;
                process.ErrorDataReceived += ErrorHandler;
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();

                if (process.HasExited) Finished = true;
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
            }
        }

        private static void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            ConsoleExtensions.Log(outLine.Data, "info", true);
        }

        private static void ErrorHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            ConsoleExtensions.Log(outLine.Data, "error", true);
        }
    }
}