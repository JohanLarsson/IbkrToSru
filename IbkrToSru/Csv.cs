namespace IbkrToSru;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public static class Csv
{
    public static ImmutableArray<Execution> ReadIbkr(string csv)
    {
        if (csv.Contains("Trades,Data,Order,Stocks,", StringComparison.Ordinal) ||
            csv.Contains("Trades,Data,Order,Equity and Index Options,", StringComparison.Ordinal))
        {
            return ReadExecutions(csv, ',').ToImmutableArray();
        }

        if (csv.Contains("Trades\tData\tOrder\tStocks\t", StringComparison.Ordinal) ||
            csv.Contains("Trades\tData\tOrder\tEquity and Index Options\t", StringComparison.Ordinal))
        {
            return ReadExecutions(csv, '\t').ToImmutableArray();
        }

        throw new FormatException("Expected comma or tab separated IBKR csv.");

        static IEnumerable<Execution> ReadExecutions(string csv, char separator)
        {
            var position = 0;
            while (ReadLine(csv, out var line, ref position))
            {
                if (TryRead(line, separator) is { } execution)
                {
                    yield return execution;
                }
            }

            static Execution? TryRead(ReadOnlySpan<char> line, char separator)
            {
                // Trades,Header,DataDiscriminator,Asset Category,Currency,Symbol,Date/Time,Quantity,T. Price,C. Price,Proceeds,Comm/Fee,Basis,Realized P/L,MTM P/L,Code
                // Trades,Data,Order,Stocks,USD,APPS,\"2021-10-05, 10:46:40\",13,74.06,73.56,-962.78,-1,963.78,0,-6.5,O;P
                var position = 0;
                if (SkipKnown(line, "Trades", separator, ref position) &&
                    SkipKnown(line, "Data", separator, ref position) &&
                    SkipKnown(line, "Order", separator, ref position) &&
                    (TrySkipKnown(line, "Stocks", separator, ref position) || TrySkipKnown(line, "Equity and Index Options", separator, ref position)))
                {
                    if (ReadString(line, separator, out var currency, ref position) &&
                        ReadString(line, separator, out var symbol, ref position) &&
                        ReadTime(line, separator, out var time, ref position) &&
                        ReadDouble(line, separator, out var quantity, ref position) &&
                        ReadDouble(line, separator, out var price, ref position) &&
                        ReadDouble(line, separator, out _, ref position) &&
                        ReadDouble(line, separator, out var proceeds, ref position) &&
                        ReadDouble(line, separator, out var fee, ref position) &&
                        ReadDouble(line, separator, out var basis, ref position) &&
                        ReadDouble(line, separator, out var pnl, ref position) &&
                        ReadString(line, separator, out _, ref position))
                    {
                        return new Execution(
                            Currency: currency,
                            Symbol: symbol,
                            Time: time,
                            Quantity: (int)quantity,
                            Price: price,
                            Proceeds: proceeds,
                            Fee: fee,
                            Basis: basis,
                            Pnl: pnl);
                    }

                    throw new FormatException("Error reading execution");
                }

                return null;
            }
        }
    }

    public static ImmutableArray<BuyOrSell> ReadTradorvate(string csv)
    {
        if (csv.Contains("Timestamp,B/S,Quantity,Price,Contract,Product,Product Description", StringComparison.Ordinal))
        {
            return ReadExecutions(csv, ',').ToImmutableArray();
        }

        throw new FormatException("Expected comma or tab separated Tradorvate csv.");

        static IEnumerable<BuyOrSell> ReadExecutions(string csv, char separator)
        {
            var position = 0;
            while (ReadLine(csv, out var line, ref position))
            {
                if (TryRead(line, separator) is { } execution)
                {
                    yield return execution;
                }
            }

            static BuyOrSell? TryRead(ReadOnlySpan<char> line, char separator)
            {
                // Timestamp,B/S,Quantity,Price,Contract,Product,Product Description
                // 2021-09-16 19:05, Sell,1,4449.00,ESZ1,ES,E-Mini S&P 500
                var position = 0;
                if (ReadTime(line, separator, out var time, ref position))
                {
                    if (ReadString(line, separator, out var action, ref position) &&
                        ReadInt(line, separator, out var quantity, ref position) &&
                        ReadDouble(line, separator, out var price, ref position) &&
                        ReadString(line, separator, out var symbol, ref position) &&
                        ReadString(line, separator, out _, ref position) &&
                        ReadString(line, separator, out _, ref position))
                    {
                        return action switch
                        {
                            " Buy" => new BuyOrSell("USD", symbol, time, quantity, price),
                            " Sell" => new BuyOrSell("USD", symbol, time, -quantity, price),
                            _ => throw new NotSupportedException("Unknown action expected buy or sell."),
                        };
                    }

                    throw new FormatException("Error reading execution");
                }

                return null;
            }
        }
    }

    private static bool ReadLine(ReadOnlySpan<char> csv, out ReadOnlySpan<char> line, ref int position)
    {
        if (csv.IsEmpty ||
            position >= csv.Length - 1)
        {
            line = default;
            return false;
        }

        for (var i = position; i < csv.Length; i++)
        {
            switch (csv[i])
            {
                case '\r'
                    when i < csv.Length - 1 &&
                         csv[i + 1] == '\n':
                    line = csv[position..i];
                    position = i + 2;
                    return true;
                case '\r':
                    line = csv[position..i];
                    position = i + 1;
                    return true;
                case '\n':
                    line = csv[position..i];
                    position = i + 1;
                    return true;
            }
        }

        line = csv[position..];
        position = csv.Length;
        return true;
    }

    private static bool ReadInt(ReadOnlySpan<char> csv, char separator, out int result, ref int position) =>
        int.TryParse(ValueSpan(csv, separator, ref position), NumberStyles.Number, CultureInfo.InvariantCulture, out result);

    private static bool ReadDouble(ReadOnlySpan<char> csv, char separator, out double result, ref int position) =>
        double.TryParse(ValueSpan(csv, separator, ref position), NumberStyles.Number, CultureInfo.InvariantCulture, out result);

    private static bool ReadString(ReadOnlySpan<char> csv, char separator, [NotNullWhen(true)] out string? result, ref int position)
    {
        result = ValueSpan(csv, separator, ref position).ToString();
        return true;
    }

    private static bool ReadTime(ReadOnlySpan<char> csv, char separator, out DateTime result, ref int position) =>
        DateTime.TryParse(ValueSpan(csv, separator, ref position), out result);

    private static bool TrySkipKnown(ReadOnlySpan<char> csv, string text, char separator, ref int position)
    {
        var before = position;
        if (SkipKnown(csv, text, separator, ref position))
        {
            return true;
        }

        position = before;
        return false;
    }

    private static bool SkipKnown(ReadOnlySpan<char> csv, string text, char separator, ref int position) =>
        ValueSpan(csv, separator, ref position).Equals(text, StringComparison.Ordinal);

    private static void Skip(ReadOnlySpan<char> csv, char c, ref int position)
    {
        while (position < csv.Length)
        {
            if (csv[position] != c)
            {
                return;
            }

            position++;
        }
    }

    private static ReadOnlySpan<char> ValueSpan(ReadOnlySpan<char> csv, char separator, ref int position)
    {
        var start = position;
        if (position < csv.Length &&
            csv[position] == '"' &&
            IndexOfEndQuote(csv, separator, start + 1, ref position) is var index)
        {
            return csv[(start + 1)..index];
        }

        for (var i = position; i < csv.Length; i++)
        {
            if (csv[i] == separator)
            {
                position = i + 1;
                return csv[start..i];
            }
        }

        position = csv.Length;
        return csv[start..];

        static int IndexOfEndQuote(ReadOnlySpan<char> csv, char separator, int startAt, ref int position)
        {
            for (var i = startAt; i < csv.Length; i++)
            {
                if (csv[i] == '"')
                {
                    position = i + 1;
                    if (separator != ' ')
                    {
                        Skip(csv, ' ', ref position);
                    }

                    if (position == csv.Length)
                    {
                        return i;
                    }

                    if (csv[position] == separator)
                    {
                        position++;
                        return i;
                    }
                }
            }

            throw new FormatException("did not find end quote");
        }
    }
}
