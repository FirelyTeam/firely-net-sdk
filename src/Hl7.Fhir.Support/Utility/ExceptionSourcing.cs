using Hl7.Fhir.ElementModel;
using System;

namespace Hl7.Fhir.Utility
{
    public interface IExceptionSink
    {
        void Notify(object source, CapturedException args);
    }

    public interface IExceptionSource
    {
        IExceptionSink Sink { get; set; }
    }


    public class CapturedException
    {
        public static CapturedException Info(string message) => new CapturedException(message, null, ExceptionSeverity.Info);

        public static CapturedException Warning(Exception exception) => new CapturedException(exception.Message, exception, ExceptionSeverity.Warning);

        public static CapturedException Error(Exception exception) => new CapturedException(exception.Message, exception, ExceptionSeverity.Error);

        private CapturedException(string message, Exception exception, ExceptionSeverity severity)
        {
            Message = message;
            Exception = exception;
            Severity = severity;
        }

        public readonly string Message;
        public readonly Exception Exception;
        public readonly ExceptionSeverity Severity;
    }

    public enum ExceptionSeverity
    {
        Error,
        Warning,
        Info
    }
}
