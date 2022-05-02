namespace IbkrToSru;

using System;
using System.Collections.Immutable;
using System.IO;
using System.Windows;

public sealed class MainViewModel : System.ComponentModel.INotifyPropertyChanged
{
    private string? csvFile;
    private string personNumber = "personnummer";
    private double exchangeRate = 8.5815;
    private int year = 2021;
    private ImmutableArray<Execution> executions;

    public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

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
            this.OnPropertyChanged(nameof(this.SruText));
        }
    }

    public string SruText
    {
        get
        {
            if (this.executions is { IsDefaultOrEmpty: true })
            {
                return "missing data";
            }
            else
            {
                try
                {
                    return Sru.Create(this.executions, this.year, new ExchangeRate("USD", this.exchangeRate), this.personNumber);
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
        }
    }

    private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
    }
}
