using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Archive.Logic.Exceptions;
using Archive.Logic.Interfaces;
using Archive.Logic.Services.Interfaces;

namespace Archive.Logic.Services
{
    public class MapperService : IMapperService
    {
        public List<TEntity> Map<TEntity, TRelatedEntity>(IEnumerable<ITextDocument>? documents)
        {
            if (documents is null || !documents.Any())
                return new List<TEntity>();

            List<TEntity> result = new();

            foreach (ITextDocument document in documents)
            {
                TEntity entity = Map<TEntity, TRelatedEntity>(document);
                result.Add(entity);
            }

            return result;
        }

        public TEntity Map<TEntity, TRelatedEntity>(ITextDocument document)
        {
            TEntity entity = (TEntity?)Activator.CreateInstance(typeof(TEntity)) ??
                throw new CannotCreateInstanceException($"Не получается создать экзепляр типа {typeof(TEntity)}");

            if (document is null)
                return entity;

            var entityProperties = GetProperties(entity.GetType());
            var documentProperties = GetProperties(document.GetType());

            for (int i = 0; i < entityProperties.Length; i++)
            {
                if (entityProperties[i].PropertyType == typeof(List<TRelatedEntity>))
                {
                    var realtedValues = documentProperties[i].GetValue(document);
                    entityProperties[i].SetValue(entity, Map<TRelatedEntity, TEntity>((IEnumerable<ITextDocument>?)realtedValues));
                    continue;
                }

                PropertyInfo documentProperty = GetProperty(documentProperties, entityProperties[i]);
                entityProperties[i].SetValue(entity, documentProperty.GetValue(document));
            }

            return entity;
        }

        private static PropertyInfo GetProperty(PropertyInfo[] properties, PropertyInfo property)
        {
            return properties.Where(x => x.Name.Equals(property.Name)).First();
        }

        private static PropertyInfo[] GetProperties(Type type)
        {
            return type.GetProperties().OrderBy(x => x.PropertyType.Name).ThenBy(x => x.Name).ToArray();
        }
    }
}
