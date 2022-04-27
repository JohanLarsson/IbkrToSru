namespace IbkrToSru;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public static class Csv
{
    public static ImmutableArray<Execution> ReadExecutions(string csv)
    {
        if (csv.Contains("Trades,Data,Order,Stocks,", StringComparison.Ordinal))
        {
            return ReadExecutions(csv, ',').ToImmutableArray();
        }

        if (csv.Contains("Trades\tData\tOrder\tStocks\t", StringComparison.Ordinal))
        {
            return ReadExecutions(csv, '\t').ToImmutableArray();
        }

        throw new FormatException("Expected comma or tab separated IBKR csv.");

        static IEnumerable<Execution> ReadExecutions(string csv, char terminator)
        {
            var position = 0;
            while (ReadLine(csv, out var line, ref position))
            {
                if (TryRead(line, terminator) is { } execution)
                {
                    yield return execution;
                }
            }

            static Execution? TryRead(ReadOnlySpan<char> line, char terminator)
            {
                // Trades,Header,DataDiscriminator,Asset Category,Currency,Symbol,Date/Time,Quantity,T. Price,C. Price,Proceeds,Comm/Fee,Basis,Realized P/L,MTM P/L,Code
                // Trades,Data,Order,Stocks,USD,APPS,\"2021-10-05, 10:46:40\",13,74.06,73.56,-962.78,-1,963.78,0,-6.5,O;P
                var position = 0;
                if (SkipKnown(line, "Trades", terminator, ref position) &&
                    SkipKnown(line, "Data", terminator, ref position) &&
                    SkipKnown(line, "Order", terminator, ref position) &&
                    SkipKnown(line, "Stocks", terminator, ref position))
                {
                    if (ReadString(line, terminator, out var currency, ref position) &&
                        ReadString(line, terminator, out var symbol, ref position) &&
                        ReadTime(line, terminator, out var time, ref position) &&
                        ReadDouble(line, terminator, out var quantity, ref position) &&
                        ReadDouble(line, terminator, out var price, ref position) &&
                        ReadDouble(line, terminator, out _, ref position) &&
                        ReadDouble(line, terminator, out var proceeds, ref position) &&
                        ReadDouble(line, terminator, out var fee, ref position) &&
                        ReadDouble(line, terminator, out _, ref position) &&
                        ReadDouble(line, terminator, out var pnl, ref position) &&
                        ReadString(line, terminator, out _, ref position))
                    {
                        return new Execution(
                            Currency: currency,
                            Symbol: symbol,
                            Time: time,
                            Quantity: (int)quantity,
                            Price: price,
                            Proceeds: proceeds,
                            Fee: fee,
                            Pnl: pnl);
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
                case '\r':
                    {
                        line = csv[position..i];
                        if (i < csv.Length - 1 &&
                            csv[i + 1] == '\n')
                        {
                            position = i + 2;
                        }
                        else
                        {
                            position = i + 1;
                        }

                        return true;
                    }
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

    private static bool ReadDouble(ReadOnlySpan<char> csv, char terminator, out double result, ref int position) =>
        double.TryParse(ValueSpan(csv, terminator, ref position), NumberStyles.Number, CultureInfo.InvariantCulture, out result);

    private static bool ReadString(ReadOnlySpan<char> csv, char terminator, [NotNullWhen(true)] out string? result, ref int position)
    {
        result = ValueSpan(csv, terminator, ref position).ToString();
        return true;
    }

    private static bool ReadTime(ReadOnlySpan<char> csv, char terminator, out DateTime result, ref int position) =>
        DateTime.TryParse(ValueSpan(csv, terminator, ref position), out result);

    private static bool SkipKnown(ReadOnlySpan<char> csv, string text, char terminator, ref int position) =>
        ValueSpan(csv, terminator, ref position).Equals(text, StringComparison.Ordinal);

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

    private static ReadOnlySpan<char> ValueSpan(ReadOnlySpan<char> csv, char terminator, ref int position)
    {
        var temp = position;
        if (ReadQuoted(csv, terminator, out var inner, ref temp))
        {
            position = temp;
            return inner;
        }

        for (var i = position; i < csv.Length; i++)
        {
            if (csv[i] == terminator)
            {
                var result = csv[position..i];
                position = i + 1;
                if (terminator == ' ')
                {
                    Skip(csv, ' ', ref position);
                }

                return result;
            }

            if (i == csv.Length - 1)
            {
                var result = csv[position..];
                position = csv.Length;
                return result;
            }
        }

        throw new FormatException("Could not read value at position");

        static bool ReadQuoted(ReadOnlySpan<char> csv, char terminator, out ReadOnlySpan<char> inner, ref int position)
        {
            if (position < csv.Length &&
                csv[position] == '"')
            {
                for (var i = position + 1; i < csv.Length; i++)
                {
                    if (csv[i] == '"' &&
                        i < csv.Length - 1 &&
                        csv[i + 1] == terminator)
                    {
                        inner = csv[(position + 1)..i];
                        position = i + 2;
                        if (terminator == ' ')
                        {
                            Skip(csv, ' ', ref position);
                        }

                        return true;
                    }
                }
            }

            inner = default;
            return false;
        }
    }
}
