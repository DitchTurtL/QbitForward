using QbitForward.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace QbitForward.Views;

public partial class MainWindow : Window
{
    private MainWindowViewModel dataContext => (MainWindowViewModel)DataContext;


    public MainWindow(MainWindowViewModel mwvm)
    {
        mwvm.MainWindow = this;
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

    private async void DropTarget_DropAsync(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.Text))
        {
            var url = e.Data.GetData(DataFormats.Text) as string;
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                await dataContext.AddUrlAsync(url);
            }
            else
            {
                // invalid url dropped
            }
        }

    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (this.DataContext == null)
            return;

        dataContext.Password = ((PasswordBox)sender).Password;
        dataContext.UpdateClientConfig();
    }

    public void ShowMessage()
    {
        // Play fade out animation
        var fadeOut = (Storyboard)this.Resources["FadeOutAnimation"];
        fadeOut.Begin(MessageGrid);
    }
}