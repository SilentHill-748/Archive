using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Archive.Logic.Interfaces
{
    /// <summary>
    /// Предоставляет операции для работы над коллекцией документов <typeparamref name="TDocument"/> типа.
    /// </summary>
    /// <typeparam name="TDocument">Тип документов.</typeparam>
    public interface IDocumentCollection<TDocument> : IDisposable, IEnumerable<TDocument>
    {
        ObservableCollection<TDocument> Documents { get; }


        /// <summary>
        /// Добавляет указанный документ в коллекцию.
        /// </summary>
        /// <param name="document">Добавляемый документ.</param>
        void Add(TDocument document);

        /// <summary>
        /// Удаляет выбранный документ из коллекции.
        /// </summary>
        /// <param name="document">Удаляемый документ.</param>
        /// <returns></returns>
        bool Remove(TDocument document);

        /// <summary>
        /// Удаляет документ по его номеру.
        /// </summary>
        /// <param name="number">Номер документа.</param>
        /// <returns></returns>
        bool Remove(int number);

        /// <summary>
        /// Поднимает на 1 позицию вверх выбранный документ.
        /// </summary>
        /// <param name="document">Документ, который надо поднять.</param>
        void MoveUp(TDocument document);

        /// <summary>
        /// Опускает на 1 позицию вниз выбранный документ.
        /// </summary>
        /// <param name="document">Документ, который надо опустить.</param>
        void MoveDown(TDocument document);

        /// <summary>
        /// печатает все документы из коллекции.
        /// </summary>
        void PrintDocuments();

        /// <summary>
        /// Печатает документ, выбранный из коллекции по номеру.
        /// </summary>
        /// <param name="number">Номер документа.</param>
        void PrintDocument(int number);

        /// <summary>
        /// Печатает выбранный документ из коллекции.
        /// </summary>
        /// <param name="document">Документ для печати.</param>
        void PrintDocument(TDocument document);

        /// <summary>
        /// Сохраняет всю коллекцию в файл по указанному пути.
        /// </summary>
        /// <param name="filename">Путь к файлу сохранения.</param>
        void SaveCollection(string filename);

        /// <summary>
        /// Загружает из файла все документы в коллекцию.
        /// </summary>
        /// <param name="filename">Полный путь к файлу.</param>
        void LoadCollection(string filename);

        /// <summary>
        /// Выполняет операцию экспорта документов в файл по указанному пути.
        /// </summary>
        /// <param name="filename">Полный путь к файлу.</param>
        void ExportCollection(string filename);

        /// <summary>
        /// Выполняет полную очистку коллекции.
        /// </summary>
        void Clear();
    }
}
