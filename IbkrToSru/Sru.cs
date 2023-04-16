namespace IbkrToSru;

using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

public static class Sru
{
    public static string Create(ImmutableArray<Execution> executions, int year, ExchangeRate exchangeRate, string personNumber, DateTime now = default)
    {
        if (executions.IsDefaultOrEmpty)
        {
            return string.Empty;
        }

        if (now == default)
        {
            now = DateTime.Now;
        }

        var items = ImmutableArray.CreateBuilder<SruItem>();

        foreach (var execution in executions)
        {
            if (execution.Time.Year == year &&
                execution.Pnl != 0)
            {
                if (execution.Currency != exchangeRate.Currency)
                {
                    throw new NotSupportedException("Only supporting USD for now.");
                }

                items.Add(
                    new SruItem(
                        Quantity: (int)Math.Abs(execution.Quantity),
                        Symbol: execution.Symbol,
                        Proceeds: exchangeRate.ToSek(execution.Proceeds < 0 ? execution.Basis : execution.Proceeds + execution.Fee),
                        Basis: exchangeRate.ToSek(execution.Proceeds < 0 ? -execution.Proceeds - execution.Fee : -execution.Basis),
                        Win: execution.Pnl > 0 ? exchangeRate.ToSek(execution.Pnl) : 0,
                        Loss: execution.Pnl < 0 ? exchangeRate.ToSek(-execution.Pnl) : 0));
            }
        }

        return Create(items.ToImmutable(), year, personNumber, now);
    }

    public static string CreateMergedBySymbol(ImmutableArray<Execution> executions, int year, ExchangeRate exchangeRate, string personNumber, DateTime now = default)
    {
        if (executions.IsDefaultOrEmpty)
        {
            return string.Empty;
        }

        if (executions.Any(x => x.Currency != exchangeRate.Currency))
        {
            throw new NotSupportedException("Only supporting USD for now.");
        }

        if (now == default)
        {
            now = DateTime.Now;
        }

        var items = ImmutableArray.CreateBuilder<SruItem>();

        foreach (var group in executions.Where(x => x.Pnl != 0 && x.Time.Year == year).GroupBy(x => x.Symbol))
        {
            var pnl = group.Sum(x => x.Pnl);
            items.Add(
                new SruItem(
                    Quantity: (int)group.Sum(x => Math.Abs(x.Quantity)),
                    Symbol: group.Key,
                    Proceeds: exchangeRate.ToSek(group.Sum(x => x.Proceeds < 0 ? x.Basis : x.Proceeds + x.Fee)),
                    Basis: exchangeRate.ToSek(group.Sum(x => x.Proceeds < 0 ? -x.Proceeds - x.Fee : -x.Basis)),
                    Win: pnl > 0 ? exchangeRate.ToSek(pnl) : 0,
                    Loss: pnl < 0 ? exchangeRate.ToSek(-pnl) : 0));
        }

        return Create(items.ToImmutable(), year, personNumber, now);
    }

    public static string Create(ImmutableArray<SruItem> items, int year, string personNumber, DateTime now = default)
    {
        if (items.IsDefaultOrEmpty)
        {
            return string.Empty;
        }

        var builder = new StringBuilder();
        if (now == default)
        {
            now = DateTime.Now;
        }

        var page = 0;
        foreach (var chunk in items.Chunk(9))
        {
            builder.AppendLine($"#BLANKETT K4-{year}P4");
            builder.AppendLine($"#IDENTITET {personNumber.Replace("-", string.Empty)} {now.ToString("yyyyMMdd HHmmss")}");
            var n = 0;
            foreach (var item in chunk)
            {
                builder.AppendLine($"#UPPGIFT 31{n}0 {item.Quantity}");
                builder.AppendLine($"#UPPGIFT 31{n}1 {item.Symbol}");
                builder.AppendLine($"#UPPGIFT 31{n}2 {item.Proceeds}");
                builder.AppendLine($"#UPPGIFT 31{n}3 {item.Basis}");
                builder.AppendLine($"#UPPGIFT 31{n}4 {item.Win}");
                builder.AppendLine($"#UPPGIFT 31{n}5 {item.Loss}");
                n++;
            }

            builder.AppendLine($"#UPPGIFT 3300 {chunk.Sum(x => x.Proceeds)}");
            builder.AppendLine($"#UPPGIFT 3301 {chunk.Sum(x => x.Basis)}");
            builder.AppendLine($"#UPPGIFT 3304 {chunk.Sum(x => x.Win)}");
            builder.AppendLine($"#UPPGIFT 3305 {chunk.Sum(x => x.Loss)}");
            builder.AppendLine($"#UPPGIFT 7014 {page}");
            builder.AppendLine("#BLANKETTSLUT");
            page++;
        }

        builder.AppendLine("#FIL_SLUT");
        return builder.ToString();
    }
}
