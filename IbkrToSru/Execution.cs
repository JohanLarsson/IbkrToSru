namespace IbkrToSru;

using System;

public sealed class Execution
{
    public Execution(string currency, string symbol, DateTime time, double quantity, double price, double proceeds, double fee, double pnl)
    {
        this.Currency = currency;
        this.Symbol = symbol;
        this.Time = time;
        this.Quantity = quantity;
        this.Price = price;
        this.Proceeds = proceeds;
        this.Fee = fee;
        this.Pnl = pnl;
    }

    public string Currency { get; }

    public string Symbol { get; }

    public DateTime Time { get; }

    public double Quantity { get; }

    public double Price { get; }

    public double Proceeds { get; }

    public double Fee { get; }

    public double Pnl { get; }
}