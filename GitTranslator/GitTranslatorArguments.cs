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
            var translatedString = TranslatedString(inputString, config);
            result[i] = translatedString ?? inputString;
        }

        return string.Join(' ', result);
    }

    private static string? TranslatedString(string inputString, Configurarion config)
    {
        foreach (var symbolDict in config.LocalSymbols)
        {
            var translatedString = new string(
                inputString.Select(
                    c => symbolDict.TryGetValue(c, out char replacement) ? replacement : c).ToArray());

            if (config.ReplaceableWords.Contains(translatedString.ToLower()))
                return translatedString;
        }

        return null;
    }

}
