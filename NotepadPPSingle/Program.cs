using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

/// <summary>
/// Author: Gregor Santner <https://de-live-gdev.github.io/>
/// License: GPLv3
/// Icon from https://github.com/notepad-plus-plus/notepad-plus-plus/blob/master/PowerEditor/src/icons/nppNewIcon.ico
/// </summary>
namespace NotepadPPSingle
{
    class Program
    {
        static void Main(string[] args)
        {
            new NotepadPPSingle(args).startNotepadPlusPlusSingleWindow();
        }
    }

    class NotepadPPSingle
    {
        string[] args;
        string notepadExecutable = null;
        public NotepadPPSingle(string[] args)
        {
            this.args = args;
        }

        private bool locateNotepadPlusPlus()
        {
            string[] paths = {
                @"C:\Program Files (x86)\Notepad++\notepad++.exe",
                @"D:\Program Files (x86)\Notepad++\notepad++.exe",
                @"C:\Programme\Notepad++\notepad++.exe",
                @"C:\Program Files\Notepad++\notepad++.exe",
                @"notepad++.exe",
            };

            foreach (string path in paths)
            {
                if (File.Exists(path))
                {
                    notepadExecutable = path;
                    return true;
                }
            }
            return false;
        }

        public void startNotepadPlusPlusSingleWindow()
        {
            if (locateNotepadPlusPlus())
            {
                // Apply single window options
                List<string> argList = new List<string>(args);
                foreach (string arg in new string[] { "-notabbar", "-multiInst", "-nosession" })
                {
                    if (!argList.Contains(arg))
                    {
                        argList.Insert(0, arg);
                    }
                }

                // Create arguments string
                String arguments = "";
                argList.ForEach(arg => arguments += "\"" + arg + "\" ");

                // Start process
                Process p = new Process();
                p.StartInfo.FileName = notepadExecutable;
                p.StartInfo.Arguments = arguments;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                p.Start();
            }
        }
    }
}
