using System;
using Shouldly;
using Shouldly.ShouldlyExtensionMethods;

namespace FluentAssertions2Shouldly
{
    public class ExceptionAssertions<T> where T : Exception
    {
        public T Subject { get; }

        public ExceptionAssertions(T value)
        {
            Subject = value;
        }

        public AndConstraint<ExceptionAssertions<T>> And => new AndConstraint<ExceptionAssertions<T>>(this);

        public ExceptionAssertions<T> WithMessage(string expected, bool ignoreCase = false)
        {
            if (ignoreCase)
            {
                Subject.Message.ToLowerInvariant().ShouldBeEquivalentTo(expected.ToLowerInvariant());
            }
            else
            {
                ((object)Subject.Message).ShouldBe(expected);
            }
            return this;
        }

        public ExceptionAssertions<T> WithMessageContaining(string expected, bool ignoreCase = false)
        {
            if (ignoreCase)
            {
                Subject.Message.ToLowerInvariant().ShouldContain(expected.ToLowerInvariant());
            }
            else
            {
                Subject.Message.ShouldContain(expected);
            }
            return this;
        }

        public ExceptionAssertions<TInner> WithInnerException<TInner>() where TInner : Exception
        {
            Subject.InnerException.ShouldBeOfType<TInner>();
            return new ExceptionAssertions<TInner>((TInner)Subject.InnerException);
        }

        public ExceptionAssertions<TInner> WithInnerExceptionExactly<TInner>() where TInner : Exception
        {
            Subject.InnerException.ShouldNotBeNull();
            Subject.InnerException.GetType().ShouldBe(typeof(TInner));
            return new ExceptionAssertions<TInner>((TInner)Subject.InnerException);
        }
    }
} 