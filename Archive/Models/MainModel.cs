using System.Collections.ObjectModel;

using Archive.Core;
using Archive.Data.Entities;

namespace Archive.Models
{
    public class MainModel : ObservableObject
    {
        private ObservableCollection<Document> _storedDocuments;
        private ObservableCollection<Document> _findedDocuments;
        private string _chosenDocumentText;


        public MainModel()
        {
            _storedDocuments = new ObservableCollection<Document>();
            _findedDocuments = new ObservableCollection<Document>();
        }


        /// <summary>
        /// Вернёт список всех документов из БД.
        /// </summary>
        public ObservableCollection<Document> StoredDocument
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
        public ObservableCollection<Document> FindedDocuments
        {
            get => _findedDocuments;
            set
            {
                _findedDocuments = value;
                OnPropertyChanged(nameof(FindedDocuments));
            }
        }

        public string Text
        {
            get => _chosenDocumentText;
            set
            {
                _chosenDocumentText = value;
                OnPropertyChanged(nameof(Text));
            }
        }
    }
}
