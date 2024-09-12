using QbitForward.ViewModels;
using System.Windows;

namespace QbitForward.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel dataContext => (MainWindowViewModel)DataContext;


    public MainWindow(MainWindowViewModel mwvm)
    {
        DataContext = mwvm;
        InitializeComponent();
    }

    private void DropTarget_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.Text) || e.Data.GetDataPresent(DataFormats.Html))
            e.Effects = DragDropEffects.Copy;
        else
            e.Effects = DragDropEffects.None;
    }

    private void DropTarget_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.Text))
        {
            var url = e.Data.GetData(DataFormats.Text) as string;
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                dataContext.AddUrlAsync(url);
            }
            else
            {
                // invalid url dropped
            }
        }

    }
}