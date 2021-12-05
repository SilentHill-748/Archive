using System;

namespace Archive.Logic.Exceptions
{
    public class CannotReadManifestResourceStream : Exception
    {
        public CannotReadManifestResourceStream()
            : base()
        {
            Message = "Не получилось прочитать файл ресурса! Возможно неверно задан путь к нему!";
        }

        public CannotReadManifestResourceStream(string message)
            : base(message)
        {
            Message = message;
        }


        public override string Message { get; }
    }
}
