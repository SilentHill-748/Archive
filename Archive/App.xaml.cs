using System.Windows;
using System.Windows.Threading;

namespace Archive
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(
                "Необработанная ошибка:\n" + e.Exception.Message, 
                "Ошибка", 
                MessageBoxButton.OK, 
                MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
