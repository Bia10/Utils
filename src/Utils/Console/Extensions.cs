using Spectre.Console;
using Spectre.Console.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utils.String;

namespace Utils.Console
{
    public class Extensions
    {
        public static void Log(string message, string severity, bool escapeMessage = false)
        {
            if (string.IsNullOrEmpty(message)) return;
            if (message.EndsWith("..."))
                message = message[..^4];

            var escapedMsg = message;
            if (escapeMessage) escapedMsg = Markup.Escape(message);

            var year = DateTime.Now.Year.ToString("0000");
            var month = DateTime.Now.Month.ToString("00");
            var day = DateTime.Now.Day.ToString("00");
            var hour = DateTime.Now.Hour.ToString("00");
            var minute = DateTime.Now.Minute.ToString("00");
            var second = DateTime.Now.Second.ToString("00");
            var milliseconds = DateTime.Now.Millisecond.ToString("000");
            var timeStampFull = year + "/" + month + "/" + day + "-" + hour + ":" + minute + ":" + second + "." + milliseconds;

            try
            {
                switch (severity)
                {
                    case "success":
                        AnsiConsole.MarkupLine($"[green]{timeStampFull}-{severity.ToUpper()}:[/] {escapedMsg} [green]...[/]");
                        break;
                    case "debug":
                        AnsiConsole.MarkupLine($"[grey]{timeStampFull}-{severity.ToUpper()}:[/] {escapedMsg} [grey]...[/]");
                        break;
                    case "info":
                        AnsiConsole.MarkupLine($"[white]{timeStampFull}-{severity.ToUpper()}:[/] {escapedMsg} [white]...[/]");
                        break;
                    case "warn":
                        AnsiConsole.MarkupLine($"[darkorange]{timeStampFull}-{severity.ToUpper()}:[/] {escapedMsg} [darkorange]...[/]");
                        break;
                    case "error":
                        AnsiConsole.MarkupLine($"[red1]{timeStampFull}-{severity.ToUpper()}:[/] {escapedMsg} [red1]...[/]");
                        break;
                    case "critical":
                        AnsiConsole.MarkupLine($"[red3_1]{timeStampFull}-{severity.ToUpper()}:[/] {escapedMsg} [red3_1]...[/]");
                        break;

                    default:
                        AnsiConsole.MarkupLine($"[red3_1]{timeStampFull}-{severity.ToUpper()}:[/] Unsupported log severity! [red3_1]...[/]");
                        break;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
                throw;
            }
        }

        public static IRenderable DirectorySummary(string dirPath)
        {
            if (!dirPath.Valid())
                throw new InvalidOperationException("Path to directory is empty/null or whitespace!");

            FileSystemInfo[] fileSystemInfos;

            try
            {
                fileSystemInfos = new DirectoryInfo(dirPath).GetFileSystemInfos();
                if (fileSystemInfos.Length == 0)
                    throw new InvalidOperationException("Directory is empty!");
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex);
                throw;
            }

            var table = new Table().SafeBorder();
            table.AddColumn("Summary for dir:");
            table.AddColumn($"{dirPath}");
            table.AddColumn($"FileSystemInfo length: {fileSystemInfos.Length}");

            var fileType = string.Empty;
            foreach (var file in fileSystemInfos)
            {
                fileType = file switch
                {
                    FileInfo => "File",
                    DirectoryInfo => "Folder",
                    _ => fileType
                };
                table.AddRow($"{fileType}", $"{file.Name}", $"{file.FullName}");
            }
            return table;
        }

        public static string YesNo(bool value)
        {
            return value ? "[green]Yes[/]" : "[red]No[/]";
        }

        public static IPrompt<string> GenerateChoiceMenu(IEnumerable<string> choicesText)
        {
            var choicesList = choicesText.ToList();
            if (!choicesList.Any())
                 throw new InvalidOperationException("Input empty!");

            var prompt = new SelectionPrompt<string>()
                .Title("[green]What would you like to do[/]?")
                .PageSize(choicesList.Count)
                .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]");

            foreach (var choice in choicesList)
                prompt.AddChoice(choice);

            return prompt;
        }
    }
}