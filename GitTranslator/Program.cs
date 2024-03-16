using System.Diagnostics;

namespace GitTranslator;

static class Program
{
    static void Main(string[] args)
    {
        var arguments = GitTranslatorArguments.Translate(args);
        StartGit(arguments);
    }
    
    private static void StartGit(string arguments)
    {
       Process process = new Process { 
           StartInfo = new()
            {
                FileName = "git",
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        process.Start();

        while (!process.StandardOutput.EndOfStream)
        {
            Console.WriteLine(process.StandardOutput.ReadLine());
        }
    }

}
