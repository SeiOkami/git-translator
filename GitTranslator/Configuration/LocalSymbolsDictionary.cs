namespace GitTranslator.Configuration;

public class LocalSymbolsDictionary : Dictionary<char, char>
{
    public LocalSymbolsDictionary() : base() { }

    public LocalSymbolsDictionary(string englishSymbols, string localSymbols)
    {
        for (int i = 0; i < localSymbols.Length; i++)
        {
            if (englishSymbols.Length <= i)
                break;

            TryAdd(localSymbols[i], englishSymbols[i]);
        }
    }
}
