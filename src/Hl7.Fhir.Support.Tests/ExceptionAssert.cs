using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Hl7.Fhir.Tests
{
    public class ExceptionAssert
    {
        public static T Throws<T>(Action action, string message = null) where T : Exception
        {

            return Assert.ThrowsException<T>(action);
        }

        public static T Throws<T>(Func<object> action, string message = null) where T : Exception
        {

            return Assert.ThrowsException<T>(action);
        }

        public static System.Threading.Tasks.Task<T> Throws<T>(Func<System.Threading.Tasks.Task> action, string message = null) where T : Exception
        {
            return Assert.ThrowsExceptionAsync<T>(action);
        }
    }
}
