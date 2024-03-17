using GitTranslator.Configuration;

namespace GitTranslator;

public static class GitTranslatorArguments
{

    public static string Translate(string inputString, Configurarion? inputConfig = null)
    {
        return Translate(inputString.Split(' '), inputConfig);
    }

    public static string Translate(string[] inputStrings, Configurarion? inputConfig = null)
    {
        var config = inputConfig ?? Configurarion.DefaultConfiguration;

        var result = new string[inputStrings.Length];

        for (int i = 0; i < inputStrings.Length; i++)
        {
            var inputString = inputStrings[i];
            string? translatedString = null;

            FillTranslatedString_UseLayoutSubstitution(inputString, config, ref translatedString);

            FillTranslatedString_UseReplacementDictionary(inputString, config, ref translatedString);

            result[i] = translatedString ?? inputString;
        }

        return string.Join(' ', result);
    }

    private static void FillTranslatedString_UseLayoutSubstitution(string inputString, Configurarion config, ref string? translatedString)
    {
        if (translatedString != null
            || !config.UseLayoutSubstitution)
            return;

        foreach (var symbolDict in config.LocalLayoutSymbols)
        {
            var thisString = new string(
                inputString.Select(
                    c => symbolDict.TryGetValue(c, out char replacement) ? replacement : c).ToArray());

            if (config.ReplaceableLayoutWords.Contains(thisString.ToLower()))
            {
                translatedString = thisString;
                break;
            }
        }
    }

    private static void FillTranslatedString_UseReplacementDictionary(string inputString, Configurarion config, ref string? translatedString)
    {
        if (translatedString != null
            || !config.UseReplacementDictionary)
            return;

        var key = inputString.ToLower();
        if (config.ReplacementDictionary.ContainsKey(key))
            translatedString = config.ReplacementDictionary[key];
    }

}
