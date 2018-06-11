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
        public ExceptionRaisedEventArgs(string message, ExceptionSeverity severity = ExceptionSeverity.Error) : this(message, null, severity)
        {
        }

        public ExceptionRaisedEventArgs(Exception exception, ExceptionSeverity severity = ExceptionSeverity.Error) : this(exception.Message, exception, severity)
        {
        }
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
