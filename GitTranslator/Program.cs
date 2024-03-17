using System.Diagnostics;
using GitTranslator.Configuration;

namespace GitTranslator;

static class Program
{
    private const string GIT_NAME = "git";

    static void Main(string[] args)
    {
        var config = Configurarion.DefaultConfiguration;
        var arguments = GitTranslatorArguments.Translate(args, config);
        ShowTextBeforeRedirection(config, arguments);
        StartGit(arguments);
    }
    
    private static void ShowTextBeforeRedirection(Configurarion config, string arguments)
    {
        if (!String.IsNullOrEmpty(config.DisplayTextBeforeRedirection))
            Console.WriteLine(config.DisplayTextBeforeRedirection);

        if (config.DisplayGitCommandArguments)
            Console.WriteLine($"{GIT_NAME} {arguments}");
    }

    private static void StartGit(string arguments)
    {
        Process.Start(GIT_NAME, arguments);
    }

}
