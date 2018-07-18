using Hl7.Fhir.ElementModel;
using System;

namespace Hl7.Fhir.Utility
{
    public delegate bool ExceptionNotificationHandler(object source, ExceptionNotification args);

    public static class ExceptionSourceExtensions
    {
        public static void NotifyOrThrow(this IExceptionSink sink, object source, ExceptionNotification args)
        {
            if (sink != null)
                sink.Notify(source, args);
            else if (args.Severity == ExceptionSeverity.Error)
                throw args.Exception;
        }

        public static IDisposable Catch(this IElementNavigator source, ExceptionNotificationHandler handler) =>
            source is IExceptionSource s ? s.Catch(handler) : throw new NotImplementedException("source does not implement IExceptionSource");

        public static IDisposable Catch(this IExceptionSource source, ExceptionNotificationHandler handler) => new ExceptionInterceptor(source, handler);

        private class ExceptionInterceptor : IDisposable, IExceptionSink
        {
            private readonly IExceptionSource _interceptee;
            private readonly IExceptionSink _originalSink;
            private readonly ExceptionNotificationHandler _handler;
            public ExceptionInterceptor(IExceptionSource interceptee, ExceptionNotificationHandler handler)
            {
                _interceptee = interceptee;
                _originalSink = interceptee.Sink;
                _interceptee.Sink = this;
                _handler = handler;
            }

            public void Notify(object source, ExceptionNotification args)
            {
                var handled = _handler(source, args);

                if(!handled && _originalSink != null)
                    _originalSink.Notify(source, args);
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
}
