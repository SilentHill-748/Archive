using System.Collections.Generic;

using Archive.Core;
using Archive.Logic.Interfaces;

namespace Archive.Models
{
    public class MainModel : ObservableObject
    {
        private List<ITextDocument> _storedDocuments;
        private List<ITextDocument> _findedDocuments;


        public MainModel()
        {
            _storedDocuments = new List<ITextDocument>();
            _findedDocuments = new List<ITextDocument>();
        }


        public List<ITextDocument> StoredDocument
        {
            get => _storedDocuments;
            set
            {
                _storedDocuments = value;
                OnPropertyChanged(nameof(StoredDocument));
            }
        }

        public List<ITextDocument> FindedDocuments
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
