using System;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class ObjectAssertions<T> where T : class
    {
        public T Subject { get; }

        public ObjectAssertions(T value)
        {
            Subject = value;
        }

        public ObjectAssertions<T> BeNull()
        {
            Subject.ShouldBeNull();
            return this;
        }

        public ObjectAssertions<T> NotBeNull()
        {
            Subject.ShouldNotBeNull();
            return this;
        }

        public ObjectAssertions<T> Be(T expected)
        {
            Subject.ShouldBe(expected);
            return this;
        }

        public ObjectAssertions<T> BeOfType(Type expected)
        {
            Subject.ShouldBeOfType(expected);
            return this;
        }

        public ObjectAssertions<T> BeOfType<TExpected>()
        {
            Subject.ShouldBeOfType<TExpected>();
            return this;
        }

        public ObjectAssertions<T> BeAssignableTo<TExpected>()
        {
            Subject.ShouldBeAssignableTo<TExpected>();
            return this;
        }

        public ObjectAssertions<T> Match<TValue>(Func<TValue, bool> predicate) where TValue : class
        {
            var value = Subject as TValue;
            value.ShouldNotBeNull();
            predicate(value).ShouldBeTrue();
            return this;
        }

        public ObjectAssertions<T> BeAssignableTo(Type expected)
        {
            Subject.ShouldBeAssignableTo(expected);
            return this;
        }

        public ObjectAssertions<T> NotBeAssignableTo<TExpected>()
        {
            Subject.GetType().IsAssignableTo(typeof(TExpected))
                .ShouldBeFalse();
            return this;
        }

        public ObjectAssertions<T> NotBeAssignableTo(Type expected)
        {
            Subject.GetType().IsAssignableTo(expected)
                .ShouldBeFalse();
            return this;
        }
    }
} 