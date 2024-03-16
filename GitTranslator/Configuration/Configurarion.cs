using System.Reflection;
using System.Text.Json;

namespace GitTranslator.Configuration;

public class Configurarion
{
    private const string DEFAULT_CONFIGURATION_FILE = "configuration.json";
    private static Configurarion? _defaultConfiguration;

    public LocalSymbolsDictionary[] LocalSymbols { get; set; } = [];
    public string[] ReplaceableWords { get; set; } = [];

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
        if (model.EnglishSymbols == null
            || model.ReplaceableWords == null
            || model.LocalSymbols == null)
            return;

        ReplaceableWords = model.ReplaceableWords;

        LocalSymbols = new LocalSymbolsDictionary[model.LocalSymbols.Length];
        for (int i = 0; i < LocalSymbols.Length; i++)
        {
            LocalSymbols[i] = new LocalSymbolsDictionary(model.EnglishSymbols, model.LocalSymbols[i]);
        }
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

}
