using System.Reflection;
using System.Text.Json;

namespace GitTranslator.Configuration;

public class Configurarion
{
    private const string DEFAULT_CONFIGURATION_FILE = "configuration.json";
    private static Configurarion? _defaultConfiguration;

    public string DisplayTextBeforeRedirection { get; set; } = String.Empty;
    public bool DisplayGitCommandArguments { get; set; }
    public bool UseLayoutSubstitution { get; set; } = false;
    public LocalSymbolsDictionary[] LocalLayoutSymbols { get; set; } = [];
    public string[] ReplaceableLayoutWords { get; set; } = [];
    public bool UseReplacementDictionary { get; set; } = false;
    public Dictionary<string, string> ReplacementDictionary { get; set; } = [];

    public static Configurarion DefaultConfiguration
    {
        get
        {
            if (_defaultConfiguration == null)
            {
                var exePath = Assembly.GetExecutingAssembly().Location;
                var exeDirectory = Path.GetDirectoryName(exePath)!;
                var fullPath = Path.Combine(exeDirectory, DEFAULT_CONFIGURATION_FILE);
                _defaultConfiguration = new(fullPath);
            }
                
            return _defaultConfiguration;
        }
    }

    public Configurarion() { }

    public Configurarion(FileConfigurationModel model)
    {
        DisplayTextBeforeRedirection = model.DisplayTextBeforeRedirection ?? "";
        DisplayGitCommandArguments = model.DisplayGitCommandArguments;

        FillLayoutSubstitution(model);
        FillReplacementDictionary(model);
    }

    public Configurarion(string filePath) : this(ModelFromFile(filePath)) { }

    private static FileConfigurationModel ModelFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            var result = JsonSerializer.Deserialize<FileConfigurationModel>(json);
            if (result == null)
                throw new FileLoadException("The file does not contain a configuration", filePath);
            else
                return result;
        }
        else
        {
            throw new FileNotFoundException(filePath);
        }
    }

    private void FillLayoutSubstitution(FileConfigurationModel model)
    {
        if (!model.UseLayoutSubstitution
            || model.EnglishLayoutSymbols == null
            || model.ReplaceableLayoutWords == null
            || model.LocalLayoutSymbols == null)
            return;

        UseLayoutSubstitution = true;

        ReplaceableLayoutWords = model.ReplaceableLayoutWords;

        LocalLayoutSymbols = new LocalSymbolsDictionary[model.LocalLayoutSymbols.Length];
        for (int i = 0; i < LocalLayoutSymbols.Length; i++)
        {
            LocalLayoutSymbols[i] = new LocalSymbolsDictionary(
                model.EnglishLayoutSymbols, model.LocalLayoutSymbols[i]);
        }
    }

    private void FillReplacementDictionary(FileConfigurationModel model)
    {
        if (!model.UseReplacementDictionary
            || model.ReplacementDictionary == null)
            return;

        UseReplacementDictionary = true;

        foreach (var keyValue in model.ReplacementDictionary)
        {
            ReplacementDictionary.Add(keyValue.Key.ToLower(), keyValue.Value.ToLower());
        }
    }

}
