using GitTranslator;

namespace UnitTests;

public class GitTranslatorArgumentsTests
{

    [Theory]
    [MemberData(nameof(TestData_Translate))]
    public void Translate(string inputString, string? expectedString)
    {
        var expected = expectedString ?? inputString;
        var result = GitTranslatorArguments.Translate(inputString);
        Assert.Equal(result, expected);
    }

    public static IEnumerable<Object[]> TestData_Translate()
    {
        
        yield return new Object[] {
            "status rrrr",
            null!,
        };

        yield return new Object[] {
            "сруслщге ветка",
            "checkout ветка",
        };

        yield return new Object[] {
            "сруслout ветка",
            "checkout ветка",
        };

        yield return new Object[] {
            "ыефегы",
            "status",
        };
    }
}