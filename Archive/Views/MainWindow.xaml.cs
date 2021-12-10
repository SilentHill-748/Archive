using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Archive.ViewModels;

namespace Archive.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel viewModel;


        public MainWindow()
        {
            InitializeComponent();
            
            viewModel = new();

            var documentCollectionVM = new DocumentCollectionViewModel(viewModel.MainModel);

            DataContext = viewModel;
            DocumentCollectionToolBar.DataContext = documentCollectionVM;
        }

        private void OpenLoadWindowItem_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow loadWindow = new();
            loadWindow.Owner = this;

            if (loadWindow.ShowDialog() == true)
                viewModel.LoadDocumentsCommand.Execute(null);
        }

        private void AboutItem_Click(object sender, RoutedEventArgs e)
        {
            string text = "Данная программа разработа под заказ для [ClentName]." +
                "\n\nИсполнитель: Студент ЛФ ПНИПУ Палин Никита\n\n\t\t© Silent Hill";
            MessageBox.Show(text, "О программе", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.LoadDocumentsCommand.Execute(null);
        }
    }
}
