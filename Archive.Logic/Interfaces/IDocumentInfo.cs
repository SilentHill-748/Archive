using System.Collections.Generic;
using System.IO;

namespace Archive.Logic.Interfaces
{
    /// <summary>
    /// Интерфейс с информацией для построения документов.
    /// </summary>
    public interface IDocumentInfo
    {
        /// <summary>
        /// Путь к главному документу.
        /// </summary>
        FileInfo RootDocument { get; }
        /// <summary>
        /// Набор ключевых слов для главного документа.
        /// </summary>
        string? KeyWords { get; }
        /// <summary>
        /// Список путей к ссылочным документам.
        /// </summary>
        List<IDocumentInfo>? References { get; }
    }
}
