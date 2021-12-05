using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Archive.Logic.Interfaces;
using Archive.Logic.Services.Interfaces;

namespace Archive.Logic.Services
{
    public class MapperService : IMapperService
    {
        public List<TEntity> Map<TEntity, TRelatedEntity>(IEnumerable<ITextDocument>? documents)
        {
            if (documents is null)
                return new List<TEntity>();

            List<TEntity> result = new();
            TEntity? entity = (TEntity?)Activator.CreateInstance(typeof(TEntity));

            if (entity is null)
                throw new Exception($"Не удалось создать экземпляр объекта {typeof(TEntity).Name}!");

            foreach (ITextDocument document  in documents)
            {
                var documentProperties = GetProperties(document.GetType());
                var entityProperties = GetProperties(entity.GetType());

                for (int i = 0; i < entityProperties.Length; i++)
                {
                    // Если на входе Document.RefDocuments
                    if (entityProperties[i].PropertyType is List<TRelatedEntity>)
                        entityProperties[i].SetValue(entity, Map<TRelatedEntity, TEntity>((IEnumerable<ITextDocument>?)documentProperties[i].GetValue(document)));

                    // Если на входе ReferenceDocument.Documents
                    if (entityProperties[i].PropertyType is List<TEntity>)
                        continue;// У ReferenceDocument не должно быть элементов в свойстве Documents.

                    PropertyInfo property = GetProperty(documentProperties, entityProperties[i].Name);
                    entityProperties[i].SetValue(entity, property.GetValue(document.GetType()));
                }
                result.Add(entity);
            }

            return result;
        }

        private static PropertyInfo GetProperty(PropertyInfo[] properties, string name)
        {
            return properties.Where(x => x.Name == name).First();
        }

        private static PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties().OrderBy(x => x.Name).ToArray();
        }
    }
}
