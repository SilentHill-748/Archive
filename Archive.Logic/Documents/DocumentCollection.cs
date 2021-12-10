using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using System.Threading.Tasks;

using Archive.Logic.Services;
using Archive.Logic.Services.Interfaces;
using Archive.Logic.Interfaces;
using Archive.Data.Entities;

namespace Archive.Logic.Documents
{
    [XmlRoot("DocumentCollection")]
    public class DocumentCollection : IDocumentCollection<Document>
    {
        private ObservableCollection<Document> _documentCollection;


        public DocumentCollection()
        {
            _documentCollection = new ObservableCollection<Document>();
        }


        [XmlElement("Document")]
        public ObservableCollection<Document> Documents
        {
            get => _documentCollection;
        }


        public void Add(Document document)
        {
            ArgumentNullException.ThrowIfNull(document, nameof(document));

            Documents.Add(document);
        }

        public void Clear()
        {
            Documents.Clear();
        }

        public void ExportCollection(string filename)
        {
            ArgumentNullException.ThrowIfNull(filename, nameof(filename));

            //Запись в txt файл.
            using StreamWriter writer = new(filename);

            foreach (Document document in Documents)
                writer.WriteLine($"{document.Number}: {document.Title} - {document.Path}");

            writer.Flush();
        }

        public void LoadCollection(string filename)
        {
            ArgumentNullException.ThrowIfNull(filename, nameof(filename));

            if (!File.Exists(filename))
                throw new ArgumentException($"Не найден указанный файл!\nПроверьте путь к файлу '{filename}'!");

            Stream fileStream = File.OpenRead(filename);

            XmlSerializer serializer = new(typeof(DocumentCollection));

            DocumentCollection? collection = (DocumentCollection?)serializer.Deserialize(fileStream);

            if (collection is not null)
                this._documentCollection = collection.Documents;
        }

        public void SaveCollection(string filename)
        {
            ArgumentNullException.ThrowIfNull(filename, nameof(filename));

            Stream fileStream = File.OpenWrite(filename);

            XmlSerializer xmlSerializer = new(typeof(DocumentCollection));
            xmlSerializer.Serialize(fileStream, this);
        }

        public void MoveDown(Document document)
        {
            ArgumentNullException.ThrowIfNull(document, nameof(document));

            for (int i = 0; i < Documents.Count - 1; i++)
            {
                if (document.Number == Documents[i].Number)
                {
                    DocumentSwap(Documents[i], Documents[i + 1]);
                    break;
                }
            }
        }

        public void MoveUp(Document document)
        {
            ArgumentNullException.ThrowIfNull(document, nameof(document));

            for (int i = 1; i < Documents.Count; i++)
            {
                if (document.Number == Documents[i].Number)
                {
                    DocumentSwap(Documents[i], Documents[i - 1]);
                    break;
                }
            }
        }

        public void PrintDocument(int number)
        {
            if (number < 0)
                throw new ArgumentException("Номер документа не может быть меньше 0!");

            Document? document = Documents.Where(d => d.Number == number).FirstOrDefault();

            if (document is null)
                throw new Exception("Документ не найден в подборке!");

            PrintDocument(document);
        }

        public void PrintDocument(Document document)
        {
            ArgumentNullException.ThrowIfNull(document, nameof(document));

            IPrintService printService = ServiceFactory.GetService<IPrintService>();
            printService.PrintDocument(document);
        }

        public void PrintDocuments()
        {
            IPrintService printService = ServiceFactory.GetService<IPrintService>();
            printService.PrintDocuments(Documents);
        }

        public bool Remove(Document document)
        {
            ArgumentNullException.ThrowIfNull(document, nameof(document));

            return Documents.Remove(document);
        }

        public bool Remove(int number)
        {
            if (number < 0)
                throw new ArgumentException("Номер документа не может быть меньше 0!");

            Document? document = Documents.Where(d => d.Number == number).FirstOrDefault();

            if (document is not null)
                return Documents.Remove(document);

            return false;
        }

        private void DocumentSwap(Document left, Document right)
        {
            Document temp = left;
            left = right;
            right = temp;
        }
    }
}
