using System;
using System.Collections.ObjectModel;

using Archive.Core;
using Archive.Data.Entities;
using Archive.Logic.Documents;

namespace Archive.Models
{
    public class MainModel : ObservableObject
    {
        private ObservableCollection<Document> _storedDocuments;
        private ObservableCollection<Document> _findedDocuments;
        private DocumentCollection _documentCollection;
        private string _chosenDocumentText;
        private string[] _keyWords;


        public MainModel()
        {
            _storedDocuments = new ObservableCollection<Document>();
            _findedDocuments = new ObservableCollection<Document>();
            _documentCollection = new DocumentCollection();

            _chosenDocumentText = string.Empty;
            _keyWords = Array.Empty<string>();
        }


        /// <summary>
        /// Вернёт список всех документов из БД.
        /// </summary>
        public ObservableCollection<Document> StoredDocument
        {
            get => _storedDocuments;
        }

        /// <summary>
        /// Вернёт список документов, найденных по паттерну поиска.
        /// </summary>
        public ObservableCollection<Document> SelectedDocuments
        {
            get => _findedDocuments;
        }

        public DocumentCollection DocumentCollection
        {
            get => _documentCollection;
            set
            {
                _documentCollection = value;
                OnPropertyChanged(nameof(DocumentCollection));
            }
        }

        /// <summary>
        /// Вернёт весь текст выбранного документа.
        /// </summary>
        public string Text
        {
            get => _chosenDocumentText;
            set
            {
                _chosenDocumentText = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        /// <summary>
        /// Вернёт ключевые слова всех докумнетов.
        /// </summary>
        public string[] KeyWords
        {
            get => _keyWords;
            set
            {
                _keyWords = value;
                OnPropertyChanged(nameof(KeyWords));
            }
        }
    }
}
