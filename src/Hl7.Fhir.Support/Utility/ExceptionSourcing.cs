using Hl7.Fhir.ElementModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hl7.Fhir.Utility
{
    public delegate bool ExceptionRaisedHandler(object source, ExceptionRaisedEventArgs args);

    public interface IExceptionSink
    {
        bool Raise(object source, ExceptionRaisedEventArgs args);
    }

    public interface IExceptionSource
    {
        IExceptionSink Sink { get; set; }
    }

    public static class ExceptionSourceExtensions
    {
        public static bool RaiseOrThrow(this IExceptionSink sink, object source, ExceptionRaisedEventArgs args) =>
            sink?.Raise(source, args) ?? (args.Severity == ExceptionSeverity.Error ? throw args.Exception : false);

        public static IDisposable Catch(this IElementNavigator source, ExceptionRaisedHandler handler) =>
            source is IExceptionSource s ? s.Catch(handler) : throw new NotImplementedException("source does not implement IExceptionSource");

        public static IDisposable Catch(this IElementNavigator source, IExceptionSink interceptor) => source.Catch(interceptor.Raise);


        public static IDisposable Catch(this IExceptionSource source, ExceptionRaisedHandler handler) => new ExceptionInterceptor(source, handler);

        public static IDisposable Catch(this IExceptionSource source, IExceptionSink interceptor) => source.Catch(interceptor.Raise);

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

            public bool Raise(object source, ExceptionRaisedEventArgs args)
            {
                if (_handler(source, args)) return true;
                return _originalSink != null ? _originalSink.Raise(source, args) : false;
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
