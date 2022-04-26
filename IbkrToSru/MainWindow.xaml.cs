namespace IbkrToSru;

using System.IO;
using System.Windows;
using Microsoft.Win32;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
    }

    private void OnOpenFileClick(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Filter = "Csv files|*.csv",
        };

        if (dialog.ShowDialog() == true)
        {
            ((MainViewModel)this.DataContext).CsvFile = dialog.FileName;
        }
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var dialog = new SaveFileDialog()
        {
            DefaultExt = ".sru",
            Filter = "SRU files|*.sru",
        };

        if (dialog.ShowDialog() == true)
        {
            File.WriteAllText(dialog.FileName, ((MainViewModel)this.DataContext).SruText);
        }
    }
}
