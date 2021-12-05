using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Archive.Core;
using Archive.Models;

namespace Archive.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly MainModel _model;


        public MainViewModel()
        {
            _model = new MainModel();
        }

        #region Properties
        public SearchMode SearchMode { get; set; }

        public MainModel MainModel => _model;
        #endregion

        #region Commands
        public RelayCommand StartSearchCommand
        {
            get
            {
                return new RelayCommand(StartSearch);
            }
        }

        public RelayCommand SelectDocumentCommand
        {
            get
            {
                return new RelayCommand(SelectCommandToResponceTreeView);
            }
        }

        public RelayCommand ShowAllTextOfChosenDocumentCommand
        {
            get
            {
                return new RelayCommand(SelectCommandToResponceTreeView);
            }
        }
        #endregion


        // Запускает алгоритм поиска документов по указанному поисковому запросу.
        private void StartSearch(object? commandParameter)
        {
            // Some code
        }

        private void SelectCommandToResponceTreeView(object? commandParameter)
        {
            // some code
        }

        // Показывает весь текст документа в окно 'Содержание документа'.
        private void ShowSummary(object? commandParameter)
        {

        }
    }
}
