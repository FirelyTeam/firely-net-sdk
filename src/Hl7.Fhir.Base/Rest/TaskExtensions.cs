/* 
 * Copyright (c) 2014, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using Hl7.Fhir.Model;
using System;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace Hl7.Fhir.Rest
{
    public static class TaskExtensions
    {
        public static T? WaitResult<T>(this Task<T?> task) where T: class 
        {
            if (task == null) return null;

            try
            {
                task.Wait();
            }
            catch(AggregateException ae)
            {
                //throw ae;
                throw ae.Flatten().InnerException!;
            }

            return task.Result;
        }
        
        /// <summary>
        /// Use the WaitNoResult so that the exception handling throws what you expect,
        /// and not the Aggregate exception
        /// </summary>
        /// <param name="task"></param>
        public static void WaitNoResult(this Task task)
        {
            if (task == null) return;

            try
            {
                task.Wait();
            }
            catch (AggregateException ae)
            {
                //throw ae;
                throw ae.Flatten().InnerException!;
            }
        }

        public static Task<TResult> FromResult<TResult>(TResult resultValue)
        {
            var completionSource = new TaskCompletionSource<TResult>();
            completionSource.SetResult(resultValue);
            return completionSource.Task;
        }
    }
}

#nullable restore