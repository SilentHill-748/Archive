using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using Archive.Logic.Documents;
using Archive.Logic.Interfaces;
using Archive.Logic.Services.Interfaces;

namespace Archive.Logic.Services
{
    public class ParsingService : IParsingService
    {
        private readonly string _filename;


        public ParsingService(string filename)
        {
            ArgumentNullException.ThrowIfNull(filename, nameof(filename));

            if (filename == "")
                throw new ArgumentException("Пустой путь к файлу!");

            _filename = filename; 
        }


        public List<IDocumentInfo> Parse()
        {
            string[] lines = File.ReadAllLines(_filename);

            if (lines.Length == 0)
                return new List<IDocumentInfo>();

            return GetFromReadedLines(lines);
        }

        // преобразует все прочитанные строки из файла в набор IDocumentInfo.
        private List<IDocumentInfo> GetFromReadedLines(string[] linesFromFile)
        {
            List<IDocumentInfo> result = new();

            for (int i = 0; i < linesFromFile.Length; i++)
            {
                string[] documentArgs = linesFromFile[i]
                    .Split(':')
                    .Select(argument => Regex.Replace(argument, @"(\[)|(\])", ""))
                    .ToArray();

                documentArgs = ConfigureDocumentAttributes(documentArgs);

                result.Add(new DocumentInfo(documentArgs));
            }

            return result;
        }

        // Конфигурирует информационные аргументы документа
        private string[] ConfigureDocumentAttributes(string[] arguments)
        {
            string rootDir = GetDirectory();

            // Настройка корректных путей к файлам, удаление из ключевых слов пробелов и перевод их в нижний регистр.
            for (int i = 0; i < arguments.Length; i++)
            {
                var references = arguments[i]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Where(x => File.Exists(rootDir + "\\" + x));

                if (references.Any()) // Иначе, если строка содержит множество файлов, то это ссылочные файлы.
                {
                    arguments[i] = string.Join(",", references.Select(filename => rootDir + "\\" + filename));
                }
                else // Иначе, если это точно не файл/файлы, то это ключевые слова.
                    arguments[i] = arguments[i].ToLower();
            }

            return arguments;
        }

        private string GetDirectory()
        {
            DirectoryInfo? directoryInfo = Directory.GetParent(_filename);

            if (directoryInfo is null)
                throw new DirectoryNotFoundException("Не получилось определить директорию!");

            return directoryInfo.FullName;
        }
    }
}
