using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Archive.Core;
using Archive.Models;
using Archive.Logic;
using Archive.Logic.Services.Interfaces;
using Archive.Logic.Services;
using Archive.Data.Entities;

namespace Archive.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly MainModel _model;
        private readonly IDbService _databaseService;
        //private readonly IDocumentBuilderService _documentBuilder;
        //private readonly IPrintService _printService;


        public MainViewModel()
        {
            _model = new MainModel();

            _databaseService = ServiceFactory.GetService<IDbService>();
            //_printService = ServiceFactory.GetService<IPrintService>();
        }

        #region Properties
        public SearchMode SearchMode { get; set; }

        public MainModel MainModel => _model;
        #endregion

        #region Commands
        public RelayCommand StartSearchCommand
        {
            get => new(StartSearch, CanStartSearch);
        }

        public RelayCommand SelectDocumentCommand
        {
            get => new(SelectCommandToResponceTreeView);
        }

        public RelayCommand ShowAllTextOfChosenDocumentCommand
        {
            get => new(SelectCommandToResponceTreeView);
        }

        public RelayCommand ShowDocumentTextCommand
        {
            get => new(ShowDocumentText);
        }

        private readonly RelayCommand _loadDocuments;
        public RelayCommand LoadDocumentsCommand
        {
            get => _loadDocuments ?? new RelayCommand(LoadAllDocuments);
        }

        private readonly RelayCommand _setSearchMode;
        public RelayCommand SetSearchModeCommand
        {
            get => _setSearchMode ?? new RelayCommand(SetSearchMode, o => true);
        }
        #endregion


        // Запускает алгоритм поиска документов по указанному поисковому запросу.
        private void StartSearch(object commandParameter)
        {
            // Тут commandParameter точно не будет null.
            string searchRequest = commandParameter.ToString()!;

            MainModel.FindedDocuments.Clear();

            ISearchService searchService = ServiceFactory.GetService<ISearchService>(searchRequest);
            List<Document> findedDocuments = searchService.Search(SearchMode);

            foreach (Document document in findedDocuments)
                MainModel.FindedDocuments.Add(document);
        }

        private bool CanStartSearch(object? commandParameter)
        {
            return  commandParameter is not null && 
                    !string.IsNullOrWhiteSpace(commandParameter.ToString());
        }

        private void SelectCommandToResponceTreeView(object? commandParameter)
        {
            if ((commandParameter is not null) && 
                (commandParameter is Document document))
            {
                MainModel.FindedDocuments.Add(document);
            }
        }

        // Показывает весь текст документа в окно 'Содержание документа'.
        private void ShowDocumentText(object? commandParameter)
        {
            if (commandParameter is not null)
            {
                if (commandParameter is Document document)
                    MainModel.Text = document.Text;
            }
        }

        private void SetSearchMode(object? commandParameter)
        {
            if ((commandParameter is not null) && 
                (commandParameter is RadioButton radioButton))
            {
                SearchMode = radioButton.Content switch
                {
                    "По заголовку" => SearchMode.DocumentTitle,
                    "По ключевым словам" => SearchMode.KeyWords,
                    _ => SearchMode.DocumentText
                };
            }
        }

        private void LoadAllDocuments(object? commandParameter)
        {
            List<Document> documents = _databaseService.UnitOfWork
                .GetRepository<Document>()
                .GetAll()
                .ToList();

            MainModel.FindedDocuments = new ObservableCollection<Document>(documents);
        }
    }
}
