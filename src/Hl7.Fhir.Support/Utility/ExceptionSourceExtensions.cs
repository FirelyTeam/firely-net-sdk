using System;

namespace Hl7.Fhir.Utility
{


    public static class ExceptionSourceExtensions
    {
        public static void NotifyOrThrow(this ExceptionNotificationHandler handler, object source, ExceptionNotification args)
        {
            if (handler != null)
                handler(source, args);
            else if (args.Severity == ExceptionSeverity.Error)
                throw args.Exception;
        }

        public static void NotifyOrThrow(this IExceptionSource ies, object source, ExceptionNotification args)
        {
            if (ies?.ExceptionHandler != null)
                ies.ExceptionHandler(source, args);
            else if (args.Severity == ExceptionSeverity.Error)
                throw args.Exception;
        }

        /// <summary>
        /// Registers an <see cref="ExceptionNotificationHandler" /> with an <see cref="IExceptionSource"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="handler"></param>
        /// <param name="forward">If true, also forwards the error to the original handler (if any).</param>
        /// <returns>An object that, when disposed, unregisters the handler from the source.</returns>
        /// <remarks>
        /// <para>This function can be used directly inside a <c>using</c> block, to scope the interception
        /// of exceptions by the given handler to that block.</para>
        /// <para>The <paramref name="handler"/> replaces the handler already in place in the source (of any), but
        /// as soon as executing leaves the block, the handler is unregistered, and the original handler restored.</para>
        /// <para>If the source originally had a handler set, the <paramref name="forward"/> can be used to forward
        /// the exception to the original handler, after invoking the handler passed in with <paramref name="handler"/>.</para>
        /// </remarks>
        /// <example>
        /// <code>
        /// using(source.Catch((o,a) => lastError = a)) 
        /// {
        ///      var children = source.Children();
        /// }
        /// </code></example>
        public static IDisposable Catch(this IExceptionSource source, ExceptionNotificationHandler handler, bool forward = false) => 
            new ExceptionInterceptor(source, handler, forward);

        private class ExceptionInterceptor : IDisposable
        {
            private readonly IExceptionSource _source;
            private readonly ExceptionNotificationHandler _originalHandler;
            private bool _forward;

            public ExceptionInterceptor(IExceptionSource source, ExceptionNotificationHandler handler, bool forward)
            {
                _source = source;
                _originalHandler = source.ExceptionHandler;
                source.ExceptionHandler = nestedHandler;
                _forward = forward;

                void nestedHandler(object s, ExceptionNotification a)
                {
                    handler(s, a);

                    if (forward) _originalHandler.NotifyOrThrow(s, a);
                }
            }

            #region IDisposable Support
            private bool disposedValue = false; // To detect redundant calls

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        _source.ExceptionHandler = _originalHandler;
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
