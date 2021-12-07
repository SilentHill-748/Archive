using System;

namespace Archive.Logic.Exceptions
{
    public class TypeNotFoundException : Exception
    {
        public TypeNotFoundException() 
            : base()
        {
            Message = "Не удалось найти тип в сборке!";
        }

        public TypeNotFoundException(Type type)
            : base()
        {
            string assemblyName = type.Assembly.FullName ?? string.Empty;
            Message = $"Не удалось найти тип {type} в сборке {assemblyName}";
        }


        public override string Message { get; }
    }
}
