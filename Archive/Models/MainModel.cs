using System.Collections.Generic;

using Archive.Core;
using Archive.Data.Entities;

namespace Archive.Models
{
    public class MainModel : ObservableObject
    {
        private List<Document> _storedDocuments;
        private List<Document> _findedDocuments;


        public MainModel()
        {
            _storedDocuments = new List<Document>();
            _findedDocuments = new List<Document>();
        }


        /// <summary>
        /// Вернёт список всех документов из БД.
        /// </summary>
        public List<Document> StoredDocument
        {
            get => _storedDocuments;
            set
            {
                _storedDocuments = value;
                OnPropertyChanged(nameof(StoredDocument));
            }
        }

        /// <summary>
        /// Вернёт список документов, найденных по паттерну поиска.
        /// </summary>
        public List<Document> FindedDocuments
        {
            get => _findedDocuments;
            set
            {
                _findedDocuments = value;
                OnPropertyChanged(nameof(FindedDocuments));
            }
        }
    }
}
