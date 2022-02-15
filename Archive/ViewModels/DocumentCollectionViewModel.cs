using Archive.Core;
using Archive.Models;
using Archive.Data.Entities;

using Microsoft.Win32;
using System.Windows;
using Archive.Logic.Documents;

namespace Archive.ViewModels
{
    public class DocumentCollectionViewModel
    {
        public DocumentCollectionViewModel(MainModel mainModel)
        {
            _moveUpCommand = new RelayCommand(MoveUpDocument, CanDoActionWithDocument);
            _moveDownCommand = new RelayCommand(MoveDownDocument, CanDoActionWithDocument);
            _removeDocumentCommand = new RelayCommand(RemoveDocument, CanDoActionWithDocument);
            _removeAllCommand = new RelayCommand(RemoveAll);
            _saveCollectionCommand = new RelayCommand(SaveCollection);
            _loadCollectionCommand = new RelayCommand(LoadCollection);
            _exportCollectionCommand = new RelayCommand(ExportCollection);
            _printAllCommand = new RelayCommand(PrintAllCollection);

            MainModel = mainModel;
        }


        public MainModel MainModel { get; }


        #region Commands
        private readonly RelayCommand _moveUpCommand;
        public RelayCommand MoveUpCommand
        {
            get => _moveUpCommand;
        }
        
        private readonly RelayCommand _moveDownCommand;
        public RelayCommand MoveDownCommand
        {
            get => _moveDownCommand;
        }
        
        private readonly RelayCommand _removeDocumentCommand;
        public RelayCommand RemoveDocumentCommand
        {
            get => _removeDocumentCommand;
        }
        
        private readonly RelayCommand _removeAllCommand;
        public RelayCommand RemoveAllDocumentsCommand
        {
            get => _removeAllCommand;
        }

        private readonly RelayCommand _saveCollectionCommand;
        public RelayCommand SaveCommand
        {
            get => _saveCollectionCommand;
        }

        private readonly RelayCommand _loadCollectionCommand;
        public RelayCommand LoadCommand
        {
            get => _loadCollectionCommand;
        }

        private readonly RelayCommand _exportCollectionCommand;
        public RelayCommand ExportCommand
        {
            get => _exportCollectionCommand;
        }

        private readonly RelayCommand _printAllCommand;
        public RelayCommand PrintAllCommand
        {
            get => _printAllCommand;
        }
        #endregion


        private void MoveUpDocument(object? commandParameter)
        {
            // параметер проверяется методом CanDoActionWithDocument. Аналогично для некоторых других методов.
            Document document = (Document)commandParameter!;
            MainModel.DocumentCollection.MoveUp(document);
        }

        private bool CanDoActionWithDocument(object? commandParameter)
        {
            return commandParameter is not null && commandParameter is Document;
        }

        private void MoveDownDocument(object? commandParameter)
        {
            Document document = (Document)commandParameter!;
            MainModel.DocumentCollection.MoveDown(document);
        }

        private void RemoveDocument(object? commandParameter)
        {
            Document document = (Document)commandParameter!;
            MainModel.DocumentCollection.Remove(document);
        }

        private void RemoveAll(object? commandParameter)
        {
            MainModel.DocumentCollection.Clear();
        }

        private void SaveCollection(object? commandParameter)
        {
            SaveFileDialog sfd = new();
            sfd.Filter = "XML Files | *.xml";

            try
            {
                if (sfd.ShowDialog() == true)
                    MainModel.DocumentCollection.SaveCollection(sfd.FileName);
            }
            catch (System.Exception ex)
            {
                HandleException(ex);
                throw;
            }
        }

        private void LoadCollection(object? commandParameter)
        {
            OpenFileDialog ofd = new();
            ofd.Filter = "XML Files | *.xml";

            try
            {
                if (ofd.ShowDialog() == true)
                {
                    DocumentCollection dc = new();
                    dc.LoadCollection(ofd.FileName);
                    MainModel.DocumentCollection = dc;
                }
            }
            catch (System.Exception ex)
            {
                HandleException(ex);
                throw;
            }
        }

        private void ExportCollection(object? commandParameter)
        {
            SaveFileDialog sfd = new();
            sfd.Filter = "Text Files | *.txt";

            try
            {
                if (sfd.ShowDialog() == true)
                    MainModel.DocumentCollection.ExportCollection(sfd.FileName);
            }
            catch (System.Exception ex)
            {
                HandleException(ex);
                throw;
            }
        }

        private void PrintAllCollection(object? commandParameter)
        {
            try
            {
                MainModel.DocumentCollection.PrintDocuments();
            }
            catch (System.Exception ex)
            {
                HandleException(ex);
                throw;
            }
        }

        private static void HandleException(System.Exception exception)
        {
            MessageBox.Show(
                "Возникла ошибка с тестом:\n" + exception.Message, 
                "Ошибка", 
                MessageBoxButton.OK, 
                MessageBoxImage.Error);
        }
    }
}
