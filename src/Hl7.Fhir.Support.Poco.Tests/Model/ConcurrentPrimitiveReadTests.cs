/*
 * Copyright (c) 2026, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 *
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

#nullable enable

using FluentAssertions;
using Hl7.Fhir.Model;
using Hl7.Fhir.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace Hl7.Fhir.Tests.Model;

/// <summary>
/// A primitive that was given a JsonValue (as a deserializer without validation does) parses it when the value is
/// first read. These tests let several threads do that first read on the same instances at the same time: every
/// thread must get the parsed value.
/// </summary>
[TestClass]
public class ConcurrentPrimitiveReadTests
{
    private const int THREADS = 4;
    private const int INSTANCES = 50_000;
    private const int ROUNDS = 4;

    [TestMethod]
    public void CodeOfTValue() =>
        readOnAllThreads(
            () => new Code<AdministrativeGender> { JsonValue = "female" },
            code => code.Value == AdministrativeGender.Female);

    [TestMethod]
    public void CodeOfTValueOfInvalidLiteral() =>
        readOnAllThreads(
            () => new Code<AdministrativeGender> { JsonValue = "nonsense" },
            code =>
            {
                try
                {
                    _ = code.Value;
                    return false;
                }
                catch (CodedValidationException)
                {
                    return !code.HasValidValue() && (string?)code.JsonValue == "nonsense";
                }
            },
            instanceCount: 2_000);  // every read throws, which is slow

    [TestMethod]
    public void CodeOfTJsonValueAfterSettingValue() =>
        readOnAllThreads(
            () => new Code<AdministrativeGender>(AdministrativeGender.Female),
            code => (string?)code.JsonValue == "female" && code.Value == AdministrativeGender.Female);

    [TestMethod]
    public void Integer64Value() =>
        readOnAllThreads(
            () => new Integer64 { JsonValue = "9007199254740993" },
            integer => integer.Value == 9007199254740993L && integer.TryToSystemLong(out var parsed) && parsed.Value == 9007199254740993L);

    [TestMethod]
    public void FhirDateTimeToDateTimeOffset()
    {
        var expected = new DateTimeOffset(2026, 1, 15, 10, 30, 0, TimeSpan.FromHours(1));

        readOnAllThreads(
            () => new FhirDateTime { JsonValue = "2026-01-15T10:30:00+01:00" },
            dateTime => dateTime.TryToDateTimeOffset(out var parsed) && parsed == expected && dateTime.ToSystemDateTime() is not null);
    }

    [TestMethod]
    public void DateToSystemDate() =>
        readOnAllThreads(
            () => new Date { JsonValue = "2026-01-15" },
            date => date.TryToSystemDate(out var parsed) && parsed.ToString() == "2026-01-15" && date.ToSystemDate() is not null);

    [TestMethod]
    public void TimeToSystemTime() =>
        readOnAllThreads(
            () => new Time { JsonValue = "10:30:00" },
            time => time.TryToSystemTime(out var parsed) && parsed.ToString() == "10:30:00" && time.ToSystemTime() is not null);

    [TestMethod]
    public void InstantValue()
    {
        var expected = new DateTimeOffset(2026, 1, 15, 10, 30, 0, TimeSpan.FromHours(1));

        readOnAllThreads(
            () => new Instant { JsonValue = "2026-01-15T10:30:00+01:00" },
            instant => instant.Value == expected && instant.TryToSystemDateTime(out var parsed) && parsed is not null);
    }

    [TestMethod]
    public void Base64BinaryValueAndJsonValue()
    {
        byte[] expected = [.. "Hi there!"u8];

        // Reading Value moves the data from the literal to the bytes, and reading JsonValue moves it back.
        readOnAllThreads(
            () => new Base64Binary { JsonValue = "SGkgdGhlcmUh" },
            binary => binary.Value is { } bytes && bytes.AsSpan().SequenceEqual(expected) && (string?)binary.JsonValue == "SGkgdGhlcmUh");
    }

    [TestMethod]
    public void Base64BinaryValueAndJsonValueOfNonCanonicalLiteral()
    {
        byte[] expected = [.. "Hi there!"u8];

        // A literal that cannot be regenerated from the bytes is kept next to them.
        readOnAllThreads(
            () => new Base64Binary { JsonValue = "SGkg dGhlcmUh" },
            binary => binary.Value is { } bytes && bytes.AsSpan().SequenceEqual(expected) && (string?)binary.JsonValue == "SGkg dGhlcmUh");
    }

    [TestMethod]
    public void Base64BinaryValueOnSomeThreadsAndJsonValueOnOthers()
    {
        byte[] expected = [.. "Hi there!"u8];

        // Threads that only read Value and threads that only read JsonValue keep moving the data in opposite
        // directions, and each of them must find it.
        readOnAllThreads(
            () => new Base64Binary { JsonValue = "SGkgdGhlcmUh" },
            (binary, thread) => thread % 2 == 0
                ? binary.Value is { } bytes && bytes.AsSpan().SequenceEqual(expected)
                : (string?)binary.JsonValue == "SGkgdGhlcmUh" && binary.HasValidValue());
    }

    [TestMethod]
    public void Base64BinaryWithoutValue() =>
        readOnAllThreads(
            () => new Base64Binary(),
            binary => binary.Value is null && binary.JsonValue is null && binary.HasValidValue());

    // Lets every thread read each of the instances once, and expects every read to be correct.
    private static void readOnAllThreads<T>(Func<T> create, Func<T, bool> readsCorrectly, int instanceCount = INSTANCES) =>
        readOnAllThreads(create, (instance, _) => readsCorrectly(instance), instanceCount);

    // The same, for a read that depends on the number of the thread (0 to THREADS - 1).
    private static void readOnAllThreads<T>(Func<T> create, Func<T, int, bool> readsCorrectly, int instanceCount = INSTANCES)
    {
        long incorrect = 0;
        var exceptions = new ConcurrentDictionary<string, int>();

        for (var round = 0; round < ROUNDS; round++)
        {
            var instances = Enumerable.Range(0, instanceCount).Select(_ => create()).ToArray();

            using var start = new Barrier(THREADS);
            var threads = Enumerable.Range(0, THREADS).Select(thread => new Thread(() =>
            {
                start.SignalAndWait();

                foreach (var instance in instances)
                {
                    try
                    {
                        if (!readsCorrectly(instance, thread)) Interlocked.Increment(ref incorrect);
                    }
                    catch (Exception exception)
                    {
                        exceptions.AddOrUpdate($"{exception.GetType().Name}: {exception.Message}", 1, (_, count) => count + 1);
                    }
                }
            })).ToList();

            threads.ForEach(thread => thread.Start());
            threads.ForEach(thread => thread.Join());
        }

        exceptions.Should().BeEmpty();
        Interlocked.Read(ref incorrect).Should().Be(0, "every thread reads the same valid value");
    }
}
