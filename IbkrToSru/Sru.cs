namespace IbkrToSru;

using System;
using System.Collections.Immutable;
using System.Text;

public static class Sru
{
    public static string Create(ImmutableArray<Execution> executions, int year, ExchangeRate exchangeRate, string personNumber, DateTime now = default)
    {
        var builder = new StringBuilder();
        if (now == default)
        {
            now = DateTime.Now;
        }

        foreach (var execution in executions)
        {
            if (execution.Time.Year == year &&
                execution.Pnl != 0)
            {
                if (execution.Currency != exchangeRate.Currency)
                {
                    throw new NotSupportedException("Only supporting USD for now.");
                }

                builder.AppendLine($"#BLANKETT K4-{year}P4");
                builder.AppendLine($"#IDENTITET {personNumber.Replace("-", string.Empty)} {now.ToString("yyyyMMdd HHmmss")}");
                builder.AppendLine($"#UPPGIFT 3100 {Math.Abs(execution.Quantity)}");
                builder.AppendLine($"#UPPGIFT 3101 {execution.Symbol}");
                builder.AppendLine($"#UPPGIFT 3102 {exchangeRate.ToSekText(execution.Proceeds)}");
                builder.AppendLine($"#UPPGIFT 3103 {exchangeRate.ToSekText(execution.Proceeds - execution.Pnl)}");
                if (execution.Pnl > 0)
                {
                    builder.AppendLine($"#UPPGIFT 3104 {exchangeRate.ToSekText(execution.Pnl)}");
                    builder.AppendLine($"#UPPGIFT 3105 0");
                }
                else
                {
                    builder.AppendLine($"#UPPGIFT 3104 0");
                    builder.AppendLine($"#UPPGIFT 3105 {exchangeRate.ToSekText(-execution.Pnl)}");
                }

                builder.AppendLine("#BLANKETTSLUT");
            }
        }

        builder.AppendLine("#FIL_SLUT");
        return builder.ToString();
    }
}
