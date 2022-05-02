namespace IbkrToSruTests;

using System;

using IbkrToSru;

using NUnit.Framework;

public static class CsvTests
{
    private static readonly TestCaseData[] Cases =
    {
        new(
            "Trades,Header,DataDiscriminator,Asset Category,Currency,Symbol,Date/Time,Quantity,T. Price,C. Price,Proceeds,Comm/Fee,Basis,Realized P/L,MTM P/L,Code\r\n" +
            "Trades,Data,Order,Stocks,USD,AMC,\"2021-11-23, 12:46:38\",50,39.83,39.16,-1991.5,-1,1992.5,0,-33.5,O\r\n" +
            "Trades,Data,Order,Stocks,USD,AMC,\"2021-11-30, 11:55:26\",-50,33.8,33.94,1690,-1.014569,-1992.5,-303.514569,-7,C",
            new[]
            {
                new Execution("USD", "AMC", new DateTime(2021, 11, 23, 12, 46, 38), 50, 39.83, -1991.5d, -1, 1992.5, 0),
                new Execution("USD", "AMC", new DateTime(2021, 11, 30, 11, 55, 26), -50, 33.8, 1690, -1.014569, -1992.5, -303.514569),
            }),
        new(
            "Trades,Data,Order,Stocks,USD,AEHR,\"2021-10-06, 09:45:56\",80,14.24,13.61,-1139.2,-1,1140.2,0,-50.4,O\r\n" +
            "Trades,Data,Order,Stocks,USD,AEHR,\"2021-10-07, 13:09:43\",-80,16.13,16.66,1290.4,-1.01610104,-1140.2,149.183899,-42.4,C\r\n" +
            "Trades,SubTotal,,Stocks,USD,AEHR,,0,,,151.2,-2.01610104,0,149.183899,-92.8,\r\n",
            new[]
            {
                new Execution("USD", "AEHR", new DateTime(2021, 10, 06, 09, 45, 56), 80, 14.24, -1139.2, -1, 1140.2, 0),
                new Execution("USD", "AEHR", new DateTime(2021, 10, 07, 13, 09, 43), -80, 16.13, 1290.4, -1.01610104, -1140.2, 149.183899),
            }),
        new(
            "Trades,Data,Order,Stocks,USD,AMC,\"2021-11-23, 12:46:38\",50,39.83,39.16,-1991.5,-1,1992.5,0,-33.5,O\n" +
            "Trades,Data,Order,Stocks,USD,AMC,\"2021-11-30, 11:55:26\",-50,33.8,33.94,1690,-1.014569,-1992.5,-303.514569,-7,C\n",
            new[]
            {
                new Execution("USD", "AMC", new DateTime(2021, 11, 23, 12, 46, 38), 50, 39.83, -1991.5d, -1, 1992.5, 0),
                new Execution("USD", "AMC", new DateTime(2021, 11, 30, 11, 55, 26), -50, 33.8, 1690, -1.014569, -1992.5, -303.514569),
            }),
        new(
            "Trades,Data,Order,Stocks,USD,NAKD,\"2021-10-14, 09:41:55\",\"4,091\",0.69375407,0.7029,-2838.1479,-30.6825,2868.8304,0,37.416,O;P\r\n" +
            "Trades,Data,Order,Stocks,USD,NAKD,\"2021-10-18, 09:30:01\",\"-4,091\",0.6616,0.67,2706.6056,-28.377888689,-2867.096556904,-188.868845,-34.3644,C;O;P",
            new[]
            {
                new Execution("USD", "NAKD", new DateTime(2021, 10, 14, 09, 41, 55), 4091, 0.69375407, -2838.1479, -30.6825, 2868.8304, 0),
                new Execution("USD", "NAKD", new DateTime(2021, 10, 18, 09, 30, 01), -4091, 0.6616, 2706.6056, -28.377888689, -2867.096556904, -188.868845),
            }),
        new(
            "Trades,Data,Order,Stocks,USD,NAKD,\"2021-10-18, 09:30:01\",-836,0.6616,0.67,553.0976,-5.633280798,-24.654,-1.733843,-7.0224,C;O\r\n" +
            "Trades,Data,Order,Stocks,USD,NAKD,\"2021-10-18, 09:40:34\",836,0.659976077,0.67,-551.74,-4.18,547.464321,-8.455679,8.38,C;L;P",
            new[]
            {
                new Execution("USD", "NAKD", new DateTime(2021, 10, 18, 09, 30, 01), -836, 0.6616, 553.0976, -5.633280798, -24.654, -1.733843d),
                new Execution("USD", "NAKD", new DateTime(2021, 10, 18, 09, 40, 34), 836, 0.659976077, -551.74, -4.1799999999999997d, 547.464321, -8.4556789999999999d),
            }),
        new(
            "Trades,Data,Order,Stocks,USD,AVGO,\"2021-12-10, 09:36:08\",-10,624.83,631.68,6248.3,-1.03305633,-6247.26694367,0,-68.5,O\r\n" +
            "Trades,Data,Order,Stocks,USD,AVGO,\"2021-12-10, 09:39:30\",10,632.37,631.68,-6323.7,-1,6247.266944,-77.433056,-6.9,C\r\n" +
            "Trades,SubTotal,,Stocks,USD,AVGO,,0,,,-75.4,-2.03305633,0.00000033,-77.433056,-75.4,",
            new[]
            {
                new Execution("USD", "AVGO", new DateTime(2021, 12, 10, 09, 36, 08), -10, 624.83, 6248.30, -1.03305633, -6247.26694367, 0),
                new Execution("USD", "AVGO", new DateTime(2021, 12, 10, 09, 39, 30), 10, 632.37, -6323.7, -1, 6247.266944, -77.433055999999993d),
            }),
    };

    private static readonly TestCaseData[] TradorvateCases =
    {
        new(
            "Timestamp,B/S,Quantity,Price,Contract,Product,Product Description\r\n" +
            "2021-09-16 19:05, Sell,1,4449.00,ESZ1,ES,E-Mini S&P 500\r\n" +
            "2021-09-16 19:20, Buy,1,4454.00,ESZ1,ES,E-Mini S&P 500\r\n",
            new[]
            {
                new BuyOrSell("USD", "ESZ1", new DateTime(2021, 09, 16, 19, 05, 00), -1, 4449),
                new BuyOrSell("USD", "ESZ1", new DateTime(2021, 09, 16, 19, 20, 00), 1, 4454),
            }),
    };

    [TestCaseSource(nameof(Cases))]
    public static void ReadIbkr(string csv, Execution[] expected)
    {
        var actual = Csv.ReadIbkr(csv);
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
            Assert.AreEqual(expected[i].Basis, actual[i].Basis);
            Assert.AreEqual(expected[i].Pnl, actual[i].Pnl);
        }
    }

    [TestCaseSource(nameof(TradorvateCases))]
    public static void ReadTradorvate(string csv, BuyOrSell[] expected)
    {
        var actual = Csv.ReadTradorvate(csv);
        Assert.AreEqual(expected.Length, actual.Length);
        for (var i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i].Currency, actual[i].Currency);
            Assert.AreEqual(expected[i].Symbol, actual[i].Symbol);
            Assert.AreEqual(expected[i].Time, actual[i].Time);
            Assert.AreEqual(expected[i].Quantity, actual[i].Quantity);
            Assert.AreEqual(expected[i].Price, actual[i].Price);
        }
    }
}
