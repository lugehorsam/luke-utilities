using System.Diagnostics;
using System;
using System.Security;
using NUnit.Framework.Constraints;

public class BashScript
{
    public string StdOut => stdOut;

    string stdOut;

    public string StdError => stdError;

    private string stdError;

    private readonly Process process;

    public BashScript(string filePath, string[] parameters, string workingDirectory)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = filePath + " " + String.Join(" ", parameters),
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            WorkingDirectory = workingDirectory,
        };
        
        process = new Process
        {
            StartInfo = startInfo,
        };
    }

    public void Run()
    {
        process.Start();
        stdOut = process.StandardOutput.ReadToEnd();
        stdError = process.StandardError.ReadToEnd();
        process.WaitForExit();
        if (!String.IsNullOrEmpty(stdError))
        {
            throw new BashScriptException(this);
        }        
    }
}
