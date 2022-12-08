/* 
 * Copyright (c) 2020, Firely (info@fire.ly) and contributors
 * See the file CONTRIBUTORS for details.
 * 
 * This file is licensed under the BSD 3-Clause license
 * available at https://raw.githubusercontent.com/FirelyTeam/firely-net-sdk/master/LICENSE
 */

using System;
using static Hl7.Fhir.Utility.Result;

namespace Hl7.Fhir.Utility
{

    public abstract class Result<T>
    {
        public static implicit operator Result<T>(T value) => Ok(value);

        public bool Success => this is Ok<T>;

        public T ValueOrDefault(T d) => this is Ok<T> ok ? ok.Value : d;

        public T ValueOrDefault() => this is Ok<T> ok ? ok.Value : default;

        public T ValueOrElse(Func<Exception, T> f) =>
            this switch
            {
                Ok<T> ok => ok.Value,
                Fail<T> err => f(err.Error),
                _ => default
            };

        public T ValueOrThrow() => this switch
        {
            Ok<T> ok => ok.Value,
            Fail<T> err => throw err.Error,
            _ => default
        };


        public S Handle<S>(Func<T, S> ok, Func<Exception, S> fail) =>
            this switch
            {
                Ok<T> o => ok(o.Value),
                Fail<T> f => fail(f.Error),
                _ => default
            };


        public Result<S> Chain<S>(Func<T, Result<S>> f) =>
            this switch
            {
                Ok<T> ok => f(ok.Value),
                Fail<T> err => Fail<S>(err.Error),
                _ => default
            };

        public Result<S> Combine<S>(Result<S> second) => Chain(_ => second);
        public override bool Equals(object obj) => throw new NotImplementedException("Should be overridden in subclasses of Result");
        public override int GetHashCode() => throw new NotImplementedException("Should be overridden in subclass of Result");

        public static Result<T> operator &(Result<T> l, Result<T> r) => l.Combine(r);
        public static bool operator ==(Result<T> l, Result<T> r) => Equals(l, r);
        public static bool operator !=(Result<T> l, Result<T> r) => !Equals(l, r);
        //public static bool operator true(Result<T> l) => l is Ok<bool> ok && ok == true;
        //public static bool operator false(Result<T> l) => !(l is Ok<bool> ok && ok == true);
    }

    public class Ok : Ok<Unit>
    {
        public Ok() : base(default) { }
    }


    public class Ok<T> : Result<T>
    {
        public T Value { get; private set; }

        public Ok(T value) => Value = value ?? throw new ArgumentNullException(nameof(value));

        public static explicit operator Ok<T>(T value) => new Ok<T>(value);
        public static implicit operator T(Ok<T> value) => value.Value;

        public override string ToString() => Value?.ToString();
        public override bool Equals(object obj) => obj is Ok<T> ok && Value.Equals(ok.Value);
        public override int GetHashCode() => Value.GetHashCode();
    }


    public interface IFailed
    {
        Exception Error { get; }
    }

    public class Fail<T> : Result<T>, IFailed
    {
        public Exception Error { get; private set; }

        public Fail(Exception error) => Error = error ?? throw new ArgumentNullException(nameof(error));

        public override string ToString() => Error.Message;
        public override bool Equals(object obj) => obj is Fail<T> fail && Error.Equals(fail.Error);
        public override int GetHashCode() => Error.GetHashCode();
    }

    public sealed class Fail : Fail<Unit>
    {
        public Fail(Exception error) : base(error)
        {
        }
    }

    public static class Result
    {
        public static void Throw(this IFailed f) => throw f.Error;

        public static Ok<R> Ok<R>(R value) => new Ok<R>(value);
        public static Ok Ok() => new Ok();

        public static Fail<T> Fail<T>(Exception error) => new Fail<T>(error);
        public static Fail Fail(Exception error) => new Fail(error);

        // This allows us to use LINQ with the Result<T> type
        public static Result<TB> Select<TA, TB>(this Result<TA> a, Func<TA, TB> select) => a.Chain(aVal => Ok(select(aVal)));

        // This allows us to use LINQ with the Result<T> type
        public static Result<TC> SelectMany<TA, TB, TC>(this Result<TA> a, Func<TA, Result<TB>> func, Func<TA, TB, TC> select) =>
            a.Chain(aVal => func(aVal).Chain(bVal => Ok(select(aVal, bVal))));
    }

}
