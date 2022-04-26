namespace IbkrToSruTests;

using System;

using IbkrToSru;

using NUnit.Framework;

public static class CsvTests
{
    private static readonly TestCaseData[] Cases =
    {
        new(
            "Trades,Data,Order,Stocks,USD,AMC,\"2021-11-23, 12:46:38\",50,39.83,39.16,-1991.5,-1,1992.5,0,-33.5,O\r\n" +
            "Trades,Data,Order,Stocks,USD,AMC,\"2021-11-30, 11:55:26\",-50,33.8,33.94,1690,-1.014569,-1992.5,-303.514569,-7,C",
            new[]
            {
                new Execution("USD", "AMC", new DateTime(2021, 11, 23, 12, 46, 38), 50, 39.83, -1991.5d, -1, 0),
                new Execution("USD", "AMC", new DateTime(2021, 11, 30, 11, 55, 26), -50, 33.8, 1690, -1.014569, -303.514569),
            }),
    };

    [TestCaseSource(nameof(Cases))]
    public static void ReadExecutions(string csv, Execution[] expected)
    {
        var actual = Csv.ReadExecutions(csv);
        Assert.AreEqual(expected.Length, actual.Length);
        for (var i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i].Currency, actual[i].Currency);
            Assert.AreEqual(expected[i].Symbol, actual[i].Symbol);
            Assert.AreEqual(expected[i].Time, actual[i].Time);
            Assert.AreEqual(expected[i].Quantity, actual[i].Quantity);
            Assert.AreEqual(expected[i].Price, actual[i].Price);
            Assert.AreEqual(expected[i].Proceeds, actual[i].Proceeds);
            Assert.AreEqual(expected[i].Fee, actual[i].Fee);
            Assert.AreEqual(expected[i].Pnl, actual[i].Pnl);
        }
    }
}