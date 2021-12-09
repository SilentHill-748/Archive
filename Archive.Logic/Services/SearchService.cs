using System;
using System.Collections.Generic;
using System.Linq;

using Archive.Data.Entities;
using Archive.Logic.Services.Interfaces;

namespace Archive.Logic.Services
{
    public class SearchService : ISearchService
    {
        private readonly IDbService _database;
        private readonly string _searchRequest;


        public SearchService()
        {
            _searchRequest = string.Empty;
            _database = ServiceFactory.GetService<IDbService>();
        }

        public SearchService(string searchRequest) : this()
        {
            ArgumentNullException.ThrowIfNull(searchRequest, nameof(searchRequest));

            _searchRequest = searchRequest;
        }


        public List<Document> Search(SearchMode mode)
        {
            List<Document> allDocuments = _database.UnitOfWork
                .GetRepository<Document>()
                .GetAll()
                .ToList();

            if ((allDocuments.Count == 0) || 
                (_searchRequest == string.Empty))
                return allDocuments;

            return mode switch
            {
                SearchMode.DocumentText => SearchAtText(allDocuments),
                SearchMode.DocumentTitle => SearchAtTitle(allDocuments),
                SearchMode.KeyWords => SearchByKeyWords(allDocuments),
                _ => new List<Document>()
            };
        }

        private List<Document> SearchAtText(List<Document> documents)
        {
            List<Document> result = new();

            for (int i = 0; i < documents.Count; i++)
            {
                // Замена пробелов с кодом 32 на пробелы с кодом 160. Иначе коды пробелов будут и 32 и 160, а надо общий код.
                string replaceSpaces = _searchRequest.Replace((char)32, (char)160);

                if (documents[i].Text.Contains(replaceSpaces))
                    result.Add(documents[i]);
            }
            return result;
        }

        private List<Document> SearchAtTitle(List<Document> documents)
        {
            List<Document> result = new();

            // Преобразую поисковый запрос к низкому регистру и убераю пробелы.
            string[] searchWords = ConvertToLowerWithoutSpaces(_searchRequest);

            result = (from document in documents
                      from searchWord in searchWords
                      where document.Title.ToLower().Equals(searchWord)
                      select document)
                      .ToList();

            return result;
        }

        private List<Document> SearchByKeyWords(List<Document> documents)
        {
            // Преобразую поисковый запрос к низкому регистру и убераю пробелы.
            string[] keyWords = ConvertToLowerWithoutSpaces(_searchRequest);

            List<Document> result = new();

            result = (from document in documents
                      from searchWord in keyWords
                      where document.KeyWords.ToLower().Contains(searchWord)
                      select document)
                      .ToList();

            return result;
        }

        private static string[] ConvertToLowerWithoutSpaces(string value)
        {
            return value
                .Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(word => word.ToLower())
                .ToArray();
        }
    }
}
