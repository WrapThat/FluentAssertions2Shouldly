using System;
using System.Linq;
using Shouldly;
using Shouldly.ShouldlyExtensionMethods;

namespace FluentAssertions2Shouldly
{
    public class NumericAssertions<T> where T : struct, IComparable<T>
    {
        protected T Subject { get; }

        public NumericAssertions(T subject)
        {
            Subject = subject;
        }

        public NumericAssertions<T> Be(T expected)
        {
            ((object)Subject).ShouldBe(expected);
            return this;
        }

        public NumericAssertions<T> NotBe(T expected)
        {
            ((object)Subject).ShouldNotBe(expected);
            return this;
        }

        public NumericAssertions<T> BeGreaterThan(T expected)
        {
            Subject.CompareTo(expected).ShouldBeGreaterThan(0);
            return this;
        }

        public NumericAssertions<T> BeLessThan(T expected)
        {
            Subject.CompareTo(expected).ShouldBeLessThan(0);
            return this;
        }

        public NumericAssertions<T> BeInRange(T min, T max)
        {
            Subject.CompareTo(min).ShouldBeGreaterThanOrEqualTo(0);
            Subject.CompareTo(max).ShouldBeLessThanOrEqualTo(0);
            return this;
        }

        public NumericAssertions<T> BePositive()
        {
            Subject.CompareTo(default).ShouldBeGreaterThan(0);
            return this;
        }

        public NumericAssertions<T> BeNegative()
        {
            Subject.CompareTo(default).ShouldBeLessThan(0);
            return this;
        }

        public NumericAssertions<T> BeOneOf(params T[] expected)
        {
            expected.ShouldContain(Subject);
            return this;
        }

        public NumericAssertions<T> BeApproximately(T expected, T precision)
        {
            var difference = Convert.ToDouble(Subject) - Convert.ToDouble(expected);
            Math.Abs(difference).ShouldBeLessThanOrEqualTo(Convert.ToDouble(precision));
            return this;
        }

        public NumericAssertions<T> BeOfType(Type expected)
        {
            ((object)Subject).ShouldBeOfType(expected);
            return this;
        }

        public NumericAssertions<T> BeOfType<TExpected>()
        {
            ((object)Subject).ShouldBeOfType<TExpected>();
            return this;
        }

        public AndConstraint<NumericAssertions<T>> BeNull()
        {
            throw new ShouldAssertException($"Cannot verify if {typeof(T).Name} is null because value types cannot be null");
        }
    }

    public class NullableNumericAssertions<T> where T : struct, IComparable<T>
    {
        private readonly T? Subject;

        public NullableNumericAssertions(T? subject)
        {
            Subject = subject;
        }

        public NullableNumericAssertions<T> BeNull()
        {
            Subject.ShouldBeNull();
            return this;
        }

        public NullableNumericAssertions<T> NotBeNull()
        {
            Subject.ShouldNotBeNull();
            return this;
        }

        public NumericAssertions<T> Value
        {
            get
            {
                Subject.ShouldNotBeNull();
                return new NumericAssertions<T>(Subject.Value);
            }
        }
    }
} 