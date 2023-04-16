namespace IbkrToSru;

using System;
using System.Collections.Immutable;
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

        foreach (var item in items)
        {
            builder.AppendLine($"#BLANKETT K4-{year}P4");
            builder.AppendLine($"#IDENTITET {personNumber.Replace("-", string.Empty)} {now.ToString("yyyyMMdd HHmmss")}");
            builder.AppendLine($"#UPPGIFT 3100 {item.Quantity}");
            builder.AppendLine($"#UPPGIFT 3101 {item.Symbol}");
            builder.AppendLine($"#UPPGIFT 3102 {item.Proceeds}");
            builder.AppendLine($"#UPPGIFT 3103 {item.Basis}");
            builder.AppendLine($"#UPPGIFT 3104 {item.Win}");
            builder.AppendLine($"#UPPGIFT 3105 {item.Loss}");
            builder.AppendLine("#BLANKETTSLUT");
        }

        builder.AppendLine("#FIL_SLUT");
        return builder.ToString();
    }
}
