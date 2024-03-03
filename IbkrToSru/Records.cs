namespace IbkrToSru;

using System;
using System.Globalization;

public readonly record struct ExchangeRate(
    string Currency,
    double Rate)
{
    public int ToSek(double amount) => (int)Math.Round(amount * this.Rate);

    public string ToSekText(double amount) => Math.Ceiling(amount * this.Rate).ToString(CultureInfo.InvariantCulture);
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

public readonly record struct SruItem(double Quantity, string Symbol, int Proceeds, int Basis, int Win, int Loss);
