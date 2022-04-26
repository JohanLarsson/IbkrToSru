﻿namespace IbkrToSru;

using System.Collections.Immutable;
using System.IO;

public sealed class MainViewModel : System.ComponentModel.INotifyPropertyChanged
{
    private string csvFile;
    private string personNumber = "19790305-4524";
    private double exchangeRate = 8.5815;
    private int year = 2021;
    private ImmutableArray<Execution> executions;

    public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

    public string CsvFile
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
            this.Executions =
                File.Exists(value)
                    ? Csv.ReadExecutions(File.ReadAllText(value))
                    : ImmutableArray<Execution>.Empty;
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

    public string SruText => Sru.Create(this.executions, this.year, new ExchangeRate("USD", this.exchangeRate), this.personNumber);

    private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
    }
}