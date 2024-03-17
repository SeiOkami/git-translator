namespace GitTranslator.Configuration;

public class FileConfigurationModel
{
    public string? DisplayTextBeforeRedirection { get; set; }
    public bool DisplayGitCommandArguments { get; set; }
    public bool UseLayoutSubstitution { get; set; }
    public string? EnglishLayoutSymbols { get; set; }
    public string[]? LocalLayoutSymbols { get; set; }
    public string[]? ReplaceableLayoutWords { get; set; }
    public bool UseReplacementDictionary { get; set; }
    public Dictionary<string, string>? ReplacementDictionary { get; set; }
}
