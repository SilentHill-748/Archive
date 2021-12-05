using System;

namespace Archive.Logic.Exceptions
{
    public class FileFormatNotSupportedException : Exception
    {
        public FileFormatNotSupportedException()
            : base()
        {
            Message = "Формат файла не поддерживается!";
        }

        public FileFormatNotSupportedException(string message)
            : base(message)
        {
            Message = message;
        }


        public override string Message { get; }
    }
}
