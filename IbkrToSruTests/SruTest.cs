namespace IbkrToSruTests;

using System;
using System.Collections.Immutable;

using IbkrToSru;

using NUnit.Framework;

public static class SruTest
{
    private static readonly TestCaseData[] Cases =
    {
        new(
            new[]
            {
                new Execution("USD", "AMC", new DateTime(2021, 11, 23, 12, 46, 38), 50, 39.83, -1991.5d, -1, 0),
                new Execution("USD", "AMC", new DateTime(2021, 11, 30, 11, 55, 26), -50, 33.8, 1690, -1.014569, -303.514569),
            },
            "#BLANKETT K4-2021P4\r\n" +
            "#IDENTITET 197903054524 20220426 133009\r\n" +
            "#UPPGIFT 3100 50\r\n" +
            "#UPPGIFT 3101 AMC\r\n" +
            "#UPPGIFT 3102 14502,74\r\n" +
            "#UPPGIFT 3103 17107,35\r\n" +
            "#UPPGIFT 3104 0\r\n" +
            "#UPPGIFT 3105 2604,61\r\n" +
            "#BLANKETTSLUT\r\n" +
            "#FIL_SLUT\r\n"),
    };

    [TestCaseSource(nameof(Cases))]
    public static void Create(Execution[] executions, string expected)
    {
        var actual = Sru.Create(
            executions.ToImmutableArray(),
            2021,
            new ExchangeRate("USD", 8.5815),
            "19790305-4524",
            new DateTime(2022, 04, 26, 13, 30, 09));
        Assert.AreEqual(expected, actual);
    }
}
