using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hl7.Fhir.Utility.Tests
{
    [TestClass]
    public class TaskHelperTests
    {
        [TestMethod]
        public void AwaitInlineRunsCompletedTaskOnCallingThread()
        {
            var callerThread = Environment.CurrentManagedThreadId;

            int? genericThread = null;
            var result = TaskHelper.AwaitInline(() =>
            {
                genericThread = Environment.CurrentManagedThreadId;
                return Task.FromResult(42);
            });

            result.Should().Be(42);
            genericThread.Should().Be(callerThread);

            int? plainThread = null;
            TaskHelper.AwaitInline(() =>
            {
                plainThread = Environment.CurrentManagedThreadId;
                return Task.CompletedTask;
            });

            plainThread.Should().Be(callerThread);
        }

        [TestMethod]
        public void AwaitInlineThrowsOnIncompleteTask()
        {
            var tcs = new TaskCompletionSource<int>();

            Assert.Throws<PlatformNotSupportedException>(() => TaskHelper.AwaitInline(() => tcs.Task));
            Assert.Throws<PlatformNotSupportedException>(() => TaskHelper.AwaitInline((Func<Task>)(() => tcs.Task)));
        }

        [TestMethod]
        public void AwaitInlineUnwrapsExceptions()
        {
            // Faulted task: the original exception, not an AggregateException.
            Assert.Throws<InvalidOperationException>(() => TaskHelper.AwaitInline(() => Task.FromException<int>(new InvalidOperationException())));
            Assert.Throws<InvalidOperationException>(() => TaskHelper.AwaitInline(() => Task.FromException(new InvalidOperationException())));

            // Delegate that throws before returning a task.
            Assert.Throws<InvalidOperationException>(() => TaskHelper.AwaitInline((Func<Task<int>>)(() => throw new InvalidOperationException())));
            Assert.Throws<InvalidOperationException>(() => TaskHelper.AwaitInline((Func<Task>)(() => throw new InvalidOperationException())));
        }

        [TestMethod]
        public void AwaitWorksOnThisPlatform()
        {
            TaskHelper.Await(() => Task.FromResult(42)).Should().Be(42);

            // A task that really yields can only be awaited via Task.Run, so this covers the non-inline path.
            TaskHelper.Await(async () =>
            {
                await Task.Delay(10);
                return 42;
            }).Should().Be(42);

            var done = false;
            TaskHelper.Await(async () =>
            {
                await Task.Delay(10);
                done = true;
            });
            Volatile.Read(ref done).Should().BeTrue();

            Assert.Throws<InvalidOperationException>(() => TaskHelper.Await(async () =>
            {
                await Task.Delay(10);
                throw new InvalidOperationException();
            }));
        }
    }
}
