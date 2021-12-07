using System;

namespace Archive.Logic.Exceptions
{
    public class CannotCreateInstanceException : Exception
    {
        public CannotCreateInstanceException() : base()
        {
            Message = "Не удалось создать экземпляр объекта!";
        }

        public CannotCreateInstanceException(string message)
            : base(message)
        {
            Message = message;
        }

        public CannotCreateInstanceException(Type type)
            : base()
        {
            Message = $"Не удалось создать экземпляр объекта {type}";
        }


        public override string Message { get; }
    }
}
