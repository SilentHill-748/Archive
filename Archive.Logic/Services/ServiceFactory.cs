using System;
using System.Linq;

using Archive.Logic.Exceptions;
using Archive.Logic.Services.Interfaces;

namespace Archive.Logic.Services
{
    public class ServiceFactory
    {
        /// <summary>
        /// Возвращает интерфейс сервиса, который хранит тип этого интерфейса.
        /// </summary>
        /// <typeparam name="T">Тип интерфейса сервиса.</typeparam>
        /// <returns>Сервис, реализующий указанный интерфейс <typeparamref name="T"/>.</returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="TypeNotFoundException"></exception>
        /// <exception cref="CannotCreateInstanceException"></exception>
        public static T GetService<T>()
            where T: IService
        {
            object[] parameters = Array.Empty<object>();
            return GetService<T>(parameters);
        }

        /// <summary>
        /// Создает и возвращает сервис с параметрами, реализующий указанный тип интерфейса.
        /// </summary>
        /// <typeparam name="T">Тип интерфейса.</typeparam>
        /// <param name="parameters">Параметры конструктора сервиса.</param>
        /// <returns>Сервис, реализующий указанный интерфейс <typeparamref name="T"/>.</returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="TypeNotFoundException"></exception>
        /// <exception cref="CannotCreateInstanceException"></exception>
        public static T GetService<T>(params object[] parameters)
            where T: IService
        {
            if (!typeof(T).IsInterface)
                throw new Exception("Передан не интерфейсный тип!");

            //Получаю тип класса, который реализует указанный интерфейс среди типов сборки.
            Type? serviceType = typeof(ServiceFactory).Assembly
                .GetTypes()
                .Where(t => t.GetInterface(typeof(T).Name) is not null)
                .FirstOrDefault();

            if (serviceType is null)
                throw new TypeNotFoundException(typeof(T));

            // Настраиваю массив дженериков конкретному типу от заданных в интерфейсе дженериков.
            if (serviceType.ContainsGenericParameters)
                serviceType = serviceType.MakeGenericType(typeof(T).GetGenericArguments());

            T service = (T?)Activator.CreateInstance(serviceType, parameters) ??
                throw new CannotCreateInstanceException($"Не найден экземпляр для интерфейса {typeof(T)}!");

            return service;
        }
    }
}