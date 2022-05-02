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
                new Execution("USD", "AMC", new DateTime(2021, 11, 23, 12, 46, 38), 50, 39.83, -1991.5d, -1, 1992.5, 0),
                new Execution("USD", "AMC", new DateTime(2021, 11, 30, 11, 55, 26), -50, 33.8, 1690, -1.014569, -1992.5, -303.514569),
            },
            "#BLANKETT K4-2021P4\r\n" +
            "#IDENTITET 197903054524 20220426 133009\r\n" +
            "#UPPGIFT 3100 50\r\n" +
            "#UPPGIFT 3101 AMC\r\n" +
            "#UPPGIFT 3102 14494\r\n" +
            "#UPPGIFT 3103 17099\r\n" +
            "#UPPGIFT 3104 0\r\n" +
            "#UPPGIFT 3105 2605\r\n" +
            "#BLANKETTSLUT\r\n" +
            "#FIL_SLUT\r\n"),
        new(
            new[]
            {
                new Execution("USD", "AEHR", new DateTime(2021, 10, 06, 09, 45, 56), 80, 14.24, -1139.2, -1, 1140.2, 0),
                new Execution("USD", "AEHR", new DateTime(2021, 10, 07, 13, 09, 43), -80, 16.13, 1290.4, -1.01610104, -1140.2, 149.183899),
            },
            "#BLANKETT K4-2021P4\r\n" +
            "#IDENTITET 197903054524 20220426 133009\r\n" +
            "#UPPGIFT 3100 80\r\n" +
            "#UPPGIFT 3101 AEHR\r\n" +
            "#UPPGIFT 3102 11065\r\n" +
            "#UPPGIFT 3103 9785\r\n" +
            "#UPPGIFT 3104 1280\r\n" +
            "#UPPGIFT 3105 0\r\n" +
            "#BLANKETTSLUT\r\n" +
            "#FIL_SLUT\r\n"),
        new(
            new[]
            {
                new Execution("USD", "AVGO", new DateTime(2021, 12, 10, 09, 36, 08), -10, 624.83, 6248.30, -1.03305633, -6247.26694367, 0),
                new Execution("USD", "AVGO", new DateTime(2021, 12, 10, 09, 39, 30), 10, 632.37, -6323.7, -1, 6247.266944, -77.433055999999993d),
            },
            "#BLANKETT K4-2021P4\r\n" +
            "#IDENTITET 197903054524 20220426 133009\r\n" +
            "#UPPGIFT 3100 10\r\n" +
            "#UPPGIFT 3101 AVGO\r\n" +
            "#UPPGIFT 3102 53611\r\n" +
            "#UPPGIFT 3103 54275\r\n" +
            "#UPPGIFT 3104 0\r\n" +
            "#UPPGIFT 3105 664\r\n" +
            "#BLANKETTSLUT\r\n" +
            "#FIL_SLUT\r\n"),
        new(
            new[]
            {
                new Execution("USD", "NAKD", new DateTime(2021, 10, 14, 09, 41, 55), 4091, 0.69375407, -2838.1479, -30.6825, 2868.8304, 0),
                new Execution("USD", "NAKD", new DateTime(2021, 10, 18, 09, 30, 01), -4091, 0.6616, 2706.6056, -28.377888689, -2867.096556904, -188.868845),
            },
            "#BLANKETT K4-2021P4\r\n" +
            "#IDENTITET 197903054524 20220426 133009\r\n" +
            "#UPPGIFT 3100 4091\r\n" +
            "#UPPGIFT 3101 NAKD\r\n" +
            "#UPPGIFT 3102 22983\r\n" +
            "#UPPGIFT 3103 24604\r\n" +
            "#UPPGIFT 3104 0\r\n" +
            "#UPPGIFT 3105 1621\r\n" +
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
