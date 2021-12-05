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
using Archive.Core;
using Archive.Logic.Services;
using Archive.Logic.Services.Interfaces;

namespace Archive.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel viewModel;


        public MainWindow()
        {
            InitializeComponent();
            
            viewModel = new();
            DataContext = viewModel;
        }

        private void OpenLoadWindowItem_Click(object sender, RoutedEventArgs e)
        {
            LoadWindow loadWindow = new();

            loadWindow.ShowDialog();
        }

        private void StackPanel_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                viewModel.SearchMode = radioButton.Content switch
                {
                    "В доументе" => SearchMode.OnDocumentText,
                    "По заголовку" => SearchMode.OnDocumentTitle,
                    "По ключевым словам" => SearchMode.ByKeyWords,
                    _ => throw new Exception("Такого режима поиска не существует!")
                };
            }
        }

        private void AboutItem_Click(object sender, RoutedEventArgs e)
        {
            string text = "Данная программа разработа под заказ для [ClentName]." +
                "\n\nИсполнитель: Студент ЛФ ПНИПУ Палин Никита\n\n\t\t© Silent Hill";
            MessageBox.Show(text, "О программе", 
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
