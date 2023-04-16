namespace IbkrToSru;

using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Windows;

public sealed class MainViewModel : System.ComponentModel.INotifyPropertyChanged
{
    private const string PlaceHolderPersonNumber = "19790305-4524";
    private string? csvFile;
    private string personNumber = "19790305-4524";
    private double exchangeRate = 10.1245;
    private bool groupBySymbol;
    private int year = 2022;
    private ImmutableArray<Execution> executions;

    public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

    public double? Net => this.executions.IsDefaultOrEmpty
        ? null
        : this.executions.Sum(x => x.Pnl);

    public string Win => this.Net switch
    {
        > 0 and var win => new ExchangeRate("USD", this.exchangeRate).ToSekText(win),
        <= 0 => "0",
        _ => string.Empty,
    };

    public string? Loss => this.Net switch
    {
        < 0 and var win => new ExchangeRate("USD", this.exchangeRate).ToSekText(-win),
        > 0 => "0",
        _ => string.Empty,
    };

    public string SruText
    {
        get
        {
            if (string.IsNullOrWhiteSpace(this.personNumber) ||
                this.personNumber == PlaceHolderPersonNumber)
            {
                return "ange personnummer";
            }

            if (this.executions is { IsDefaultOrEmpty: true })
            {
                return "csv fil saknas";
            }

            try
            {
                if (this.groupBySymbol)
                {
                    return Sru.CreateMergedBySymbol(this.executions, this.year, new ExchangeRate("USD", this.exchangeRate), this.personNumber);
                }

                return Sru.Create(this.executions, this.year, new ExchangeRate("USD", this.exchangeRate), this.personNumber);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }

    public string? CsvFile
    {
        get => this.csvFile;
        set
        {
            if (value == this.csvFile)
            {
                return;
            }

            this.csvFile = value;
            this.OnPropertyChanged();
            try
            {
                this.Executions =
                    File.Exists(value)
                        ? Csv.ReadIbkr(File.ReadAllText(value))
                        : ImmutableArray<Execution>.Empty;
            }
            catch (Exception e)
            {
                this.Executions = ImmutableArray<Execution>.Empty;
                MessageBox.Show("Error parsing csv." + Environment.NewLine + e.Message);
            }
        }
    }

    public string PersonNumber
    {
        get => this.personNumber;
        set
        {
            if (value == this.personNumber)
            {
                return;
            }

            this.personNumber = value;
            this.OnPropertyChanged();
            this.OnPropertyChanged(nameof(this.SruText));
        }
    }

    public bool GroupBySymbol
    {
        get => this.groupBySymbol;
        set
        {
            if (value == this.groupBySymbol)
            {
                return;
            }

            this.groupBySymbol = value;
            this.OnPropertyChanged();
            this.OnPropertyChanged(nameof(this.SruText));
        }
    }

    public int Year
    {
        get => this.year;
        set
        {
            if (value == this.year)
            {
                return;
            }

            this.year = value;
            this.OnPropertyChanged();
            this.OnPropertyChanged(nameof(this.SruText));
        }
    }

    public double ExchangeRate
    {
        get => this.exchangeRate;
        set
        {
            if (value == this.exchangeRate)
            {
                return;
            }

            this.exchangeRate = value;
            this.OnPropertyChanged();
            this.OnPropertyChanged(nameof(this.Win));
            this.OnPropertyChanged(nameof(this.Loss));
            this.OnPropertyChanged(nameof(this.SruText));
        }
    }

    public ImmutableArray<Execution> Executions
    {
        get => this.executions;
        private set
        {
            if (value == this.executions)
            {
                return;
            }

            this.executions = value;
            this.OnPropertyChanged();
            this.OnPropertyChanged(nameof(this.Net));
            this.OnPropertyChanged(nameof(this.Win));
            this.OnPropertyChanged(nameof(this.Loss));
            this.OnPropertyChanged(nameof(this.SruText));
        }
    }

    private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
    }
}
