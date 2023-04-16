namespace IbkrToSru;

using System;
using System.Globalization;

public readonly record struct ExchangeRate(
    string Currency,
    double Rate)
{
    public double ToSek(double amount) => amount * this.Rate;

    public string ToSekText(double amount) => Math.Round(amount * this.Rate).ToString(CultureInfo.InvariantCulture);
}

public readonly record struct Execution(
    string Currency,
    string Symbol,
    DateTime Time,
    double Quantity,
    double Price,
    double Proceeds,
    double Fee,
    double Basis,
    double Pnl);

public readonly record struct BuyOrSell(
    string Currency,
    string Symbol,
    DateTime Time,
    int Quantity,
    double Price);
