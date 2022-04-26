namespace IbkrToSru;

using System;
using System.Globalization;

public record ExchangeRate(
    string Currency,
    double Rate)
{
    public double ToSek(double amount) => amount * this.Rate;

    public string ToSekText(double amount) => (amount * this.Rate).ToString("0.##", CultureInfo.GetCultureInfo("sv-SE"));
}

public record Execution(
    string Currency,
    string Symbol,
    DateTime Time,
    double Quantity,
    double Price,
    double Proceeds,
    double Fee,
    double Pnl);
