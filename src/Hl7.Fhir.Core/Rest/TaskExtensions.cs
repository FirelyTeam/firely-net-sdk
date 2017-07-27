using Hl7.Fhir.Model;
using System;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.Rest
{
    public static class TaskExtensions
    {
        public static T WaitResult<T>(this Task<T> task) where T: class 
        {
            if (task == null) return null;

            try
            {
                task.Wait();
            }
            catch(AggregateException ae)
            {
                //throw ae;
                throw ae.Flatten().InnerException;
            }

            return task.Result;
        }

        public static Task<TResult> FromResult<TResult>(TResult resultValue)
        {
            var completionSource = new TaskCompletionSource<TResult>();
            completionSource.SetResult(resultValue);
            return completionSource.Task;
        }
    }
}