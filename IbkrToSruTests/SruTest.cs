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
            "#UPPGIFT 3102 14503\r\n" +
            "#UPPGIFT 3103 17107\r\n" +
            "#UPPGIFT 3104 0\r\n" +
            "#UPPGIFT 3105 2605\r\n" +
            "#BLANKETTSLUT\r\n" +
            "#FIL_SLUT\r\n"),
        new(
            new[]
            {
                new Execution("USD", "AVGO", new DateTime(2021, 12, 10, 09, 36, 08), -10, 624.83, 6248.30, -1.03305633, 0),
                new Execution("USD", "AVGO", new DateTime(2021, 12, 10, 09, 39, 30), 10, 632.37, -6323.7, -1, -77.433055999999993d),            },
            "#BLANKETT K4-2021P4\r\n" +
            "#IDENTITET 197903054524 20220426 133009\r\n" +
            "#UPPGIFT 3100 10\r\n" +
            "#UPPGIFT 3101 AVGO\r\n" +
            "#UPPGIFT 3102 53602\r\n" +
            "#UPPGIFT 3103 54267\r\n" +
            "#UPPGIFT 3104 0\r\n" +
            "#UPPGIFT 3105 664\r\n" +
            "#BLANKETTSLUT\r\n" +
            "#FIL_SLUT\r\n"),
        new(
            new[]
            {
                new Execution("USD", "NAKD", new DateTime(2021, 10, 14, 09, 41, 55), 4091, 0.69375407, -2838.1479, -30.6825, 0),
                new Execution("USD", "NAKD", new DateTime(2021, 10, 18, 09, 30, 01), -4091, 0.6616, 2706.6056, -28.377888689, -188.868845),
            },
            "#BLANKETT K4-2021P4\r\n" +
            "#IDENTITET 197903054524 20220426 133009\r\n" +
            "#UPPGIFT 3100 4091\r\n" +
            "#UPPGIFT 3101 NAKD\r\n" +
            "#UPPGIFT 3102 23227\r\n" +
            "#UPPGIFT 3103 24848\r\n" +
            "#UPPGIFT 3104 0\r\n" +
            "#UPPGIFT 3105 1621\r\n" +
            "#BLANKETTSLUT\r\n" +
            "#FIL_SLUT\r\n"),
        new(
            new[]
            {
                new Execution("USD", "NAKD", new DateTime(2021, 10, 18, 09, 40, 34), 836, 0.659976077, -551.74, -4.1799999999999997d, -8.4556789999999999d),
            },
            "#BLANKETT K4-2021P4\r\n" +
            "#IDENTITET 197903054524 20220426 133009\r\n" +
            "#UPPGIFT 3100 836\r\n" +
            "#UPPGIFT 3101 NAKD\r\n" +
            "#UPPGIFT 3102 4662\r\n" +
            "#UPPGIFT 3103 4735\r\n" +
            "#UPPGIFT 3104 0\r\n" +
            "#UPPGIFT 3105 73\r\n" +
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
