using System;

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
                throw new ObjectNotImplementedInterfaceException(typeof(TService), typeof(TServiceInterface));

            return serviceInterface;
        }
    }
}
//TODO: Создать свой эксепшн или указать нужный из встроенных.