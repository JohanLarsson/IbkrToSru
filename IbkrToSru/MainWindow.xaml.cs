namespace IbkrToSru;

using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Navigation;

using Microsoft.Win32;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
    }

    private void OnOpenClick(object sender, RoutedEventArgs e)
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

    private void OnSaveClick(object sender, RoutedEventArgs e)
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

    private void OnRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
        e.Handled = true;
    }
}
