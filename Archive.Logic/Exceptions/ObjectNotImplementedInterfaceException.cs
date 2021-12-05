using System;

namespace Archive.Logic.Exceptions
{
    public class ObjectNotImplementedInterfaceException : Exception
    {
        public ObjectNotImplementedInterfaceException()
            : base()
        {
            Message = "Нельзя создать объект, т.к. он не имплементирует нужный интерфейс!";
        }

        public ObjectNotImplementedInterfaceException(string message)
            : base(message)
        {
            Message = message;
        }

        public ObjectNotImplementedInterfaceException(Type objectType, Type interfaceType)
            : base()
        {
            Message = $"Нельзя создать экземпляр объекта {objectType.Name}, т.к. он не имплементирует интерфейс {interfaceType.Name}!";
        }


        public override string Message { get; }
    }
}
