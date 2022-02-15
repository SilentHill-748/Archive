using System;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;

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


        public MainViewModel()
        {
            _model = new MainModel();

            _startCommand = new RelayCommand(StartSearch, CanStartSearch);
            _selectDocumentCommand = new RelayCommand(SelectCommandToResponceTreeView);
            _showDocumentTextCommand = new RelayCommand(ShowDocumentText);
            _loadDocuments = new RelayCommand(LoadAllDocuments);
            _setSearchMode = new RelayCommand(SetSearchMode);
            _addDocumentToCollectionCommand = new RelayCommand(AddDocumentToСollection, CanAddDocumentToCollection);
            _exitCommand = new RelayCommand(Exit);
            _clearTreeCommand = new RelayCommand(ClearTree);
            _cleanDbCommand = new RelayCommand(CleanDatabase, CanCleanDatabase);
        }

        #region Properties
        public SearchMode SearchMode { get; set; }

        public MainModel MainModel => _model;
        #endregion


        #region Commands
        private readonly RelayCommand _startCommand;
        public RelayCommand StartSearchCommand
        {
            get => _startCommand;
        }

        private readonly RelayCommand _selectDocumentCommand;
        public RelayCommand SelectDocumentCommand
        {
            get => _selectDocumentCommand;
        }

        private readonly RelayCommand _showDocumentTextCommand;
        public RelayCommand ShowDocumentTextCommand
        {
            get => _showDocumentTextCommand;
        }

        private readonly RelayCommand _loadDocuments;
        public RelayCommand LoadDocumentsCommand
        {
            get => _loadDocuments;
        }

        private readonly RelayCommand _setSearchMode;
        public RelayCommand SetSearchModeCommand
        {
            get => _setSearchMode;
        }

        private readonly RelayCommand _addDocumentToCollectionCommand;
        public RelayCommand AddDocumentToCollectionCommand
        {
            get => _addDocumentToCollectionCommand;
        }

        private readonly RelayCommand _exitCommand;
        public RelayCommand ExitCommand
        {
            get => _exitCommand;
        }

        private readonly RelayCommand _clearTreeCommand;
        public RelayCommand ClearTreeCommand
        {
            get => _clearTreeCommand;
        }

        private readonly RelayCommand _cleanDbCommand;
        public RelayCommand CleanDatabaseCommand
        {
            get => _cleanDbCommand;
        }
        #endregion


        // Запускает алгоритм поиска документов по указанному поисковому запросу.
        private void StartSearch(object? commandParameter)
        {
            // Тут commandParameter точно не будет null.
            string searchRequest = commandParameter?.ToString()!;

            MainModel.SelectedDocuments.Clear();

            try
            {
                ISearchService searchService = ServiceFactory.GetService<ISearchService>(searchRequest);
                List<Document> findedDocuments = searchService.Search(SearchMode);

                foreach (Document document in findedDocuments)
                    MainModel.SelectedDocuments.Add(document);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
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
                MainModel.SelectedDocuments.Add(document);
            }
        }

        // Показывает весь текст документа в окно 'Содержание документа'.
        private void ShowDocumentText(object? commandParameter)
        {
            if (commandParameter is not null)
            {
                if (commandParameter is Document document)
                    MainModel.Text = document.Text;
                else if (commandParameter is ReferenceDocument refDocument)
                    MainModel.Text = refDocument.Text;
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
            try
            {
                List<Document> documents = App.DbService.GetAll();

                MainModel.StoredDocument.Clear();

                foreach (Document document in documents)
                    MainModel.StoredDocument.Add(document);

                MainModel.KeyWords = documents.Select(x => x.KeyWords).ToArray();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
        }

        private void AddDocumentToСollection(object? commandParameter)
        {
            //параметр не null! Это проверяется в методе CanAddDocumentToCollection.
            Document document = (Document)commandParameter!;
            MainModel.DocumentCollection.Add(document);
        }

        private bool CanAddDocumentToCollection(object? commandParameter)
        {
            return commandParameter is not null && commandParameter is Document;
        }

        private void ClearTree(object? commandParameter)
        {
            MainModel.SelectedDocuments.Clear();
            MainModel.Text = string.Empty;
        }

        private void CleanDatabase(object? commandParameter)
        {
            try
            {
                _ = App.DbService.DropDatabase();

                bool isCreated = App.DbService.CreateDatabase();

                if (isCreated)
                {
                    MainModel.StoredDocument.Clear();
                    MainModel.KeyWords = Array.Empty<string>();
                }
                else
                    throw new InvalidOperationException("Не удалось очистить базу данных! " +
                        "Вероятно файл БД отсутствует или занят другим процессом!");
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
        }

        private bool CanCleanDatabase(object? commandParameter)
        {
            return MainModel.StoredDocument.Count > 0;
        }

        // Метод для высвобождения загруженных ресурсов.
        private void Exit(object? commandParameter)
        {
            MainModel.DocumentCollection.Dispose();
            MainModel.SelectedDocuments.Clear();
            MainModel.StoredDocument.Clear();
        }

        private void HandleException(Exception exception)
        {
            MessageBox.Show(
                "Возникла ошибка с текстом:\n" + exception.Message,
                "Ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
