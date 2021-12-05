using System;

namespace Archive.Logic.Exceptions
{
    public class EmptyConfigurationFileException : Exception
    {
        public EmptyConfigurationFileException()
            : base()
        {
            Message = "Файл конфигурации не может быть пустым!";
        }

        public EmptyConfigurationFileException(string message)
            : base(message)
        {
            Message = message;
        }


        public override string Message { get; }
    }
}
