/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Hl7.Fhir.Utility
{
    public static class TaskHelper
    {
        // Single-threaded WebAssembly (Blazor WASM) cannot block on a queued Task.Run, so run inline there.
        private static readonly bool s_runInline =
            RuntimeInformation.IsOSPlatform(OSPlatform.Create("BROWSER")) || RuntimeInformation.IsOSPlatform(OSPlatform.Create("WASI"));

        public static T Await<T>(Func<Task<T>> asyncFunc)
        {
            if (s_runInline) return AwaitInline(asyncFunc);

            // Call GetAwaiter() rather then .Result to unwrap the AggregateException
            return Task.Run(asyncFunc).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public static void Await(Func<Task> asyncFunc)
        {
            if (s_runInline)
            {
                AwaitInline(asyncFunc);
                return;
            }

            // Call GetAwaiter() rather then .Result to unwrap the AggregateException
            Task.Run(asyncFunc).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Invokes <paramref name="asyncFunc"/> on the calling thread and returns its result, which must already be available.
        /// Throws <see cref="PlatformNotSupportedException"/> if the task is not complete. This happens after the work has started,
        /// so that work keeps running un-awaited.
        /// </summary>
        internal static T AwaitInline<T>(Func<Task<T>> asyncFunc)
        {
            var task = asyncFunc();
            ensureCompleted(task);
            return task.GetAwaiter().GetResult();
        }

        /// <inheritdoc cref="AwaitInline{T}(Func{Task{T}})"/>
        internal static void AwaitInline(Func<Task> asyncFunc)
        {
            var task = asyncFunc();
            ensureCompleted(task);
            task.GetAwaiter().GetResult();
        }

        private static void ensureCompleted(Task task)
        {
            if (!task.IsCompleted)
                throw new PlatformNotSupportedException(
                    "This SDK call blocks on an async operation, which single-threaded WebAssembly cannot do. " +
                    "Use resolvers (and terminology services) that complete synchronously, e.g. in-memory sources.");
        }

        public static async Task<bool> AnyAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Task<bool>> predicate)
        {
            foreach (var elem in source)
                if (await predicate(elem).ConfigureAwait(false)) return true;

            return false;
        }
    }
}
