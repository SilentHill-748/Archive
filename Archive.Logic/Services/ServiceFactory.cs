using System;
using System.Linq;

using Archive.Logic.Exceptions;

namespace Archive.Logic.Services
{
    public class ServiceFactory
    {
        public static TServiceInterface GetService<TService, TServiceInterface>()
            where TService : class
            where TServiceInterface : class
        {
            object[] parameters = Array.Empty<object>();

            return GetService<TService, TServiceInterface>(parameters);
        }

        public static TServiceInterface GetService<TService, TServiceInterface>(params object[]? parameters)
            where TService : class
            where TServiceInterface : class
        {
            TServiceInterface? serviceInterface = 
                (TServiceInterface?)Activator.CreateInstance(typeof(TService), parameters);

            if (serviceInterface is null)
                throw new CannotCreateInstanceException(typeof(TServiceInterface));

            return serviceInterface;
        }

        /// <summary>
        /// Возвращает интерфейс сервиса, который хранит тип этого интерфейса.
        /// </summary>
        /// <typeparam name="T">Тип интерфейса сервиса.</typeparam>
        /// <returns>Сервис, реализующий указанный интерфейс <typeparamref name="T"/>.</returns>
        /// <exception cref="TypeNotFoundException"></exception>
        /// <exception cref="CannotCreateInstanceException"></exception>
        public static T GetService<T>() 
            where T: class
        {
            Type? serviceType = typeof(ServiceFactory).Assembly
                .GetTypes()
                .Where(t => t.GetInterface(typeof(T).Name) is not null)
                .FirstOrDefault();

            if (serviceType is null)
                throw new TypeNotFoundException(typeof(T));

            T service = (T?)Activator.CreateInstance(serviceType) ?? 
                throw new CannotCreateInstanceException($"Не удалось создать экземпляр интерфейса {typeof(T)}!");

            return service;
        }
    }
}
//TODO: Создать свой эксепшн или указать нужный из встроенных.