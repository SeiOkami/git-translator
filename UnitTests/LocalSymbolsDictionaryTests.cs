using GitTranslator.Configuration;

namespace UnitTests;

public class LocalSymbolsDictionaryTests
{

    [Theory]
    [MemberData(nameof(TestData_Constructor))]
    public void Constructor(string englishString, string localString, LocalSymbolsDictionary expected)
    {
        var result = new LocalSymbolsDictionary(englishString, localString);
        Assert.Equal(result, expected);
    }

    public static IEnumerable<Object[]> TestData_Constructor()
    {
        
        yield return new Object[] {
            "qwerty",
            "������",
            new LocalSymbolsDictionary
            {
                { '�', 'q' },
                { '�', 'w' },
                { '�', 'e' },
                { '�', 'r' },
                { '�', 't' },
                { '�', 'y' },
            }
        };

        yield return new Object[] {
            "qwe",
            "������",
            new LocalSymbolsDictionary
            {
                { '�', 'q' },
                { '�', 'w' },
                { '�', 'e' },
            }
        };


        yield return new Object[] {
            "qwerty",
            "���",
            new LocalSymbolsDictionary
            {
                { '�', 'q' },
                { '�', 'w' },
                { '�', 'e' },
            }
        };

        yield return new Object[] {
            "qwerty",
            "qwerty",
            new LocalSymbolsDictionary
            {
                { 'q', 'q' },
                { 'w', 'w' },
                { 'e', 'e' },
                { 'r', 'r' },
                { 't', 't' },
                { 'y', 'y' },
            }
        };

        yield return new Object[] {
            "qqq",
            "���",
            new LocalSymbolsDictionary
            {
                { '�', 'q' },
            }
        };

        yield return new Object[] {
            "",
            "",
            new LocalSymbolsDictionary()
        };

    }
}