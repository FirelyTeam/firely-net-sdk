using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Utility
{
    public delegate bool ExceptionRaisedHandler(ExceptionRaisedEventArgs args);

    public interface IExceptionSink
    {
        bool Raise(ExceptionRaisedEventArgs args);
    }

    public interface IExceptionSource
    {
        IExceptionSink Sink { get; set; }
    }

    public static class ExceptionSourceExtensions
    {
        public static IDisposable Intercept(this IExceptionSource source, ExceptionRaisedHandler handler) => new ExceptionInterceptor(source, handler);

        public static IDisposable Intercept(this IExceptionSource source, IExceptionSink interceptor) => source.Intercept(interceptor.Raise);

        private class ExceptionInterceptor : IDisposable, IExceptionSink
        {
            private IExceptionSource _interceptee;
            private IExceptionSink _originalSink;
            private ExceptionRaisedHandler _handler;
            public ExceptionInterceptor(IExceptionSource interceptee, ExceptionRaisedHandler handler)
            {
                _interceptee = interceptee;
                _originalSink = interceptee.Sink;
                _interceptee.Sink = this;
                _handler = handler;
            }

            public bool Raise(ExceptionRaisedEventArgs args)
            {
                if (_handler(args)) return true;
                return _originalSink != null ? _originalSink.Raise(args) : false;
            }

            #region IDisposable Support
            private bool disposedValue = false; // To detect redundant calls

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        _interceptee.Sink = _originalSink;                            
                    }

                    disposedValue = true;
                }
            }

            // This code added to correctly implement the disposable pattern.
            void IDisposable.Dispose()
            {
                // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
                Dispose(true);
            }
            #endregion
        }
    }



    public class ExceptionRaisedEventArgs
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
