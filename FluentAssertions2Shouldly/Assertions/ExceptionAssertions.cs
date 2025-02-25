using System;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class ExceptionAssertions<T> where T : Exception
    {
        public T Subject { get; }

        public ExceptionAssertions(T value)
        {
            Subject = value;
        }

        public ExceptionAssertions<T> WithMessage(string expected)
        {
            Subject.Message.ShouldBe(expected);
            return this;
        }
    }
} 