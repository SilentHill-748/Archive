using System;
using System.Collections.Generic;

namespace Archive.Logic.Interfaces
{
    /// <summary>
    /// Интерфейс текстового документа.
    /// </summary>
    public interface ITextDocument : IDisposable
    {
        /// <summary>
        /// Номер документа.
        /// </summary>
        int Number { get; }
        /// <summary>
        /// Название документа в файловой системе.
        /// </summary>
        string Title { get; }
        /// <summary>
        /// Полный текст документа.
        /// </summary>
        string Text { get; }
        /// <summary>
        /// Полный путь к документу.
        /// </summary>
        string Path { get; }
        /// <summary>
        /// Набор ключевых слов для данного документа.
        /// </summary>
        string? KeyWords { get; }
        /// <summary>
        /// Коллекция связных документов с данным.
        /// </summary>
        List<ITextDocument>? Documents { get; set; }
    }
}
