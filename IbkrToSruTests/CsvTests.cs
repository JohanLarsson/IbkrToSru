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
        new(
            "Trades,Data,Order,Stocks,USD,NAKD,\"2021-10-14, 09:41:55\",\"4,091\",0.69375407,0.7029,-2838.1479,-30.6825,2868.8304,0,37.416,O;P\r\n" +
            "Trades,Data,Order,Stocks,USD,NAKD,\"2021-10-18, 09:30:01\",\"-4,091\",0.6616,0.67,2706.6056,-28.377888689,-2867.096556904,-188.868845,-34.3644,C;O;P",
            new[]
            {
                new Execution("USD", "NAKD", new DateTime(2021, 10, 14, 09, 41, 55), 4091, 0.69375407, -2838.1479, -30.6825, 0),
                new Execution("USD", "NAKD", new DateTime(2021, 10, 18, 09, 30, 01), -4091, 0.6616, 2706.6056, -28.377888689, -188.868845),
            }),
        new(
            "Trades,Data,Order,Stocks,USD,NAKD,\"2021-10-18, 09:30:01\",-836,0.6616,0.67,553.0976,-5.633280798,-24.654,-1.733843,-7.0224,C;O\r\n" +
            "Trades,Data,Order,Stocks,USD,NAKD,\"2021-10-18, 09:40:34\",836,0.659976077,0.67,-551.74,-4.18,547.464321,-8.455679,8.38,C;L;P",
            new[]
            {
                new Execution("USD", "NAKD", new DateTime(2021, 10, 18, 09, 30, 01), -836, 0.6616, 553.0976, -5.633280798, -1.733843d),
                new Execution("USD", "NAKD", new DateTime(2021, 10, 18, 09, 40, 34), 836, 0.659976077, -551.74, -4.1799999999999997d, -8.4556789999999999d),
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
