using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Utility
{
    public interface IExceptionSource
    {
        event EventHandler<ExceptionRaisedEventArgs> OnExceptionRaised;
    }

    public class ExceptionRaisedEventArgs : EventArgs
    {
        public static ExceptionRaisedEventArgs Info(string message) => new ExceptionRaisedEventArgs(message, null, ExceptionSeverity.Info);

        public static ExceptionRaisedEventArgs Warning(Exception exception) => new ExceptionRaisedEventArgs(exception.Message, exception, ExceptionSeverity.Warning);

        public static ExceptionRaisedEventArgs Error(Exception exception) => new ExceptionRaisedEventArgs(exception.Message, exception, ExceptionSeverity.Error);

        public ExceptionRaisedEventArgs(string message, Exception exception, ExceptionSeverity severity)
        {
            Exception = exception;
            Message = message;
            Severity = severity;
        }

        public readonly Exception Exception;
        public readonly string Message;
        public readonly ExceptionSeverity Severity;
    }

    public enum ExceptionSeverity
    {
        Error,
        Warning,
        Info
    }
}
