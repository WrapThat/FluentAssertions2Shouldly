using System;
using Shouldly;

namespace FluentAssertions2Shouldly.Assertions
{
    public class RecordStructAssertions<T> where T : struct
    {
        private readonly T _subject;

        public RecordStructAssertions(T value)
        {
            _subject = value;
        }

        public AndConstraint<RecordStructAssertions<T>> And => new AndConstraint<RecordStructAssertions<T>>(this);

        public RecordStructAssertions<T> Be(T expected)
        {
            _subject.ShouldBe(expected);
            return this;
        }

        public RecordStructAssertions<T> NotBe(T expected)
        {
            _subject.ShouldNotBe(expected);
            return this;
        }

        public RecordStructAssertions<T> BeOneOf(params T[] expected)
        {
            expected.ShouldContain(_subject);
            return this;
        }

        public RecordStructAssertions<T> NotBeOneOf(params T[] unexpected)
        {
            unexpected.ShouldNotContain(_subject);
            return this;
        }

        public RecordStructAssertions<T> BeOfType(Type expected)
        {
            _subject.GetType().ShouldBe(expected);
            return this;
        }

        public RecordStructAssertions<T> BeOfType<TExpected>() where TExpected : struct
        {
            _subject.ShouldBeOfType<TExpected>();
            return this;
        }

        public RecordStructAssertions<T> BeAssignableTo<TExpected>()
        {
            _subject.ShouldBeAssignableTo<TExpected>();
            return this;
        }

        public RecordStructAssertions<T> BeAssignableTo(Type expected)
        {
            _subject.ShouldBeAssignableTo(expected);
            return this;
        }

        public RecordStructAssertions<T> Match<TValue>(Func<TValue, bool> predicate) where TValue : struct
        {
            if (_subject is TValue value)
            {
                predicate(value).ShouldBeTrue();
            }
            else
            {
                throw new ShouldAssertException($"Expected {typeof(T).Name} to be of type {typeof(TValue).Name}");
            }
            return this;
        }
    }
} 