using Archive.Core;

namespace Archive.Models
{
    public class LoadWindowModel : ObservableObject
    {
        private int _loadingDocumentsCount;
        private int _loadedDocumentsCount;
        private string _configurationFilePath = string.Empty;


        /// <summary>
        /// Вернёт общее число загружаемых документов.
        /// </summary>
        public int LoadingDocumentsCount
        {
            get => _loadingDocumentsCount;
            set
            {
                _loadingDocumentsCount = value;
                OnPropertyChanged(nameof(LoadingDocumentsCount));
            }
        }

        /// <summary>
        /// Вернёт число загруженных документов в текущий момент времени.
        /// </summary>
        public int LoadedDocumentsCount
        {
            get => _loadedDocumentsCount;
            set
            {
                _loadedDocumentsCount = value;
                OnPropertyChanged(nameof(LoadedDocumentsCount));
            }
        }

        /// <summary>
        /// Вернёт путь к файлу конфигурации.
        /// </summary>
        public string ConfigurationFilePath
        {
            get => _configurationFilePath;
            set
            {
                _configurationFilePath = value;
                OnPropertyChanged(nameof(ConfigurationFilePath));
            }
        }
    }
}
