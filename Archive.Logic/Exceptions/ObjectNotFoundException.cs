using System;

namespace Archive.Logic.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException()
            : base()
        {
            Message = "объект не был найден!";
        }

        public ObjectNotFoundException(string message)
            : base(message)
        {
            Message = message;
        }


        public override string Message { get; }
    }
}
