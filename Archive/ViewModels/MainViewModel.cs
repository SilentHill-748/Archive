using Archive.Core;
using Archive.Models;
using Archive.Logic.Services.Interfaces;
using Archive.Data.Entities;
using Archive.Logic.Services;
using System.Linq;

namespace Archive.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly MainModel _model;
        private readonly IDbService _databaseService;
        //private readonly IDocumentBuilderService _documentBuilder;
        //private readonly ISearchService _searchService;
        //private readonly IPrintService _printService;


        public MainViewModel()
        {
            _model = new MainModel();

            _databaseService = ServiceFactory.GetService<IDbService>();
            //_searchService = ServiceFactory.GetService<ISearchService>();
            //_printService = ServiceFactory.GetService<IPrintService>();
        }

        #region Properties
        public SearchMode SearchMode { get; set; }

        public MainModel MainModel => _model;
        #endregion

        #region Commands
        public RelayCommand StartSearchCommand
        {
            get => new(StartSearch);
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
        private void ShowDocumentText(object? commandParameter)
        {
            // some code
        }

        private void LoadAllDocuments(object? commandParameter)
        {
            MainModel.StoredDocument = _databaseService.UnitOfWork
                .GetRepository<Document>()
                .GetAll()
                .ToList();
        }
    }
}
