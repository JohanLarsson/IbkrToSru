namespace IbkrToSru;

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
}
