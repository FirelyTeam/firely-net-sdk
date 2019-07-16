using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hl7.Fhir.Tests
{
    public class ExceptionAssert
    {
        public static T Throws<T>(Action action, string message = null) where T : Exception
        {
#if NET40
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            try
            {
                action();
            }

            catch (Exception ex)
            {
                if (!typeof(T).Equals(ex.GetType()))
                {
                    Assert.Fail(String.Format("Test method threw exception {0}, but exception {1} was expected. Exception message: {2}", ex.GetType().Name, typeof(T).Name, message));
                }

                return (T)ex;
            }

            Assert.Fail(String.Format("Test method did not throw expected exception {0}. {1}", typeof(T).Name, message));
            return default(T);

#else
            return Assert.ThrowsException<T>(action);
#endif
        }

        public static T Throws<T>(Func<object> action, string message = null) where T : Exception
        {
#if NET40
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            try
            {
                action();
            }

            catch (Exception ex)
            {
                if (!typeof(T).Equals(ex.GetType()))
                {
                    Assert.Fail(String.Format("Test method threw exception {0}, but exception {1} was expected. Exception message: {2}", ex.GetType().Name, typeof(T).Name, message));
                }

                return (T) ex;
            }

            Assert.Fail(String.Format("Test method did not throw expected exception {0}. {1}", typeof(T).Name, message));
            return default (T);
            
#else
            return Assert.ThrowsException<T>(action);
#endif
        }
    }
}
