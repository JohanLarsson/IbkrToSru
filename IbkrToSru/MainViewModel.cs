namespace IbkrToSru;

using System;
using System.Collections.Immutable;
using System.IO;

public sealed class MainViewModel : System.ComponentModel.INotifyPropertyChanged
{
    private string personNumber = "19790305-4524";
    private ExchangeRate exchangeRate = new("USD", 8.5815);
    private string csvFile;
    private string sruFile;
    private int year = DateTime.Now.Year - 1;
    private ImmutableArray<Execution> executions;

    public event System.ComponentModel.PropertyChangedEventHandler? PropertyChanged;

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
        }
    }

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
        }
    }

    public string SruFile
    {
        get => this.sruFile;
        set
        {
            if (value == this.sruFile)
            {
                return;
            }

            this.sruFile = value;
            this.OnPropertyChanged();
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
        }
    }

    public ExchangeRate ExchangeRate
    {
        get => this.exchangeRate;
        set
        {
            if (ReferenceEquals(value, this.exchangeRate))
            {
                return;
            }

            this.exchangeRate = value;
            this.OnPropertyChanged();
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
        }
    }

    public void Export()
    {
        File.WriteAllText(this.sruFile, Sru.Create(this.executions, this.year, this.exchangeRate, this.personNumber));
    }

    private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
    {
        this.PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
    }
}
