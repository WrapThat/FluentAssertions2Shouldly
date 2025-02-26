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

        public AndConstraint<NumericAssertions<T>> And => new AndConstraint<NumericAssertions<T>>(this);

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

        public NumericAssertions<T> BeGreaterThanOrEqualTo(T expected)
        {
            Subject.CompareTo(expected).ShouldBeGreaterThanOrEqualTo(0);
            return this;
        }

        public NumericAssertions<T> BeLessThan(T expected)
        {
            Subject.CompareTo(expected).ShouldBeLessThan(0);
            return this;
        }

        public NumericAssertions<T> BeLessThanOrEqualTo(T expected)
        {
            Subject.CompareTo(expected).ShouldBeLessThanOrEqualTo(0);
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

        public NumericAssertions<T> NotBeOneOf(params T[] unexpected)
        {
            unexpected.ShouldNotContain(Subject);
            return this;
        }

        public NumericAssertions<T> BeApproximately(T expected, T precision)
        {
            var difference = Convert.ToDouble(Subject) - Convert.ToDouble(expected);
            Math.Abs(difference).ShouldBeLessThanOrEqualTo(Convert.ToDouble(precision));
            return this;
        }

        public NumericAssertions<T> NotBeApproximately(T expected, T precision)
        {
            var difference = Convert.ToDouble(Subject) - Convert.ToDouble(expected);
            Math.Abs(difference).ShouldBeGreaterThan(Convert.ToDouble(precision));
            return this;
        }

        public NumericAssertions<T> BeCloseTo(T expected, T precision)
        {
            return BeApproximately(expected, precision);
        }

        public NumericAssertions<T> NotBeCloseTo(T expected, T precision)
        {
            return NotBeApproximately(expected, precision);
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

        public NumericAssertions<T> BeZero()
        {
            Subject.CompareTo(default(T)).ShouldBe(0);
            return this;
        }

        public NumericAssertions<T> NotBeZero()
        {
            Subject.CompareTo(default(T)).ShouldNotBe(0);
            return this;
        }

        public NumericAssertions<T> BeFinite()
        {
            var value = Convert.ToDouble(Subject);
            (!double.IsInfinity(value) && !double.IsNaN(value)).ShouldBeTrue();
            return this;
        }

        public NumericAssertions<T> BeInfinity()
        {
            var value = Convert.ToDouble(Subject);
            double.IsInfinity(value).ShouldBeTrue();
            return this;
        }

        public NumericAssertions<T> BePositiveInfinity()
        {
            var value = Convert.ToDouble(Subject);
            double.IsPositiveInfinity(value).ShouldBeTrue();
            return this;
        }

        public NumericAssertions<T> BeNegativeInfinity()
        {
            var value = Convert.ToDouble(Subject);
            double.IsNegativeInfinity(value).ShouldBeTrue();
            return this;
        }

        public NumericAssertions<T> BeNaN()
        {
            var value = Convert.ToDouble(Subject);
            double.IsNaN(value).ShouldBeTrue();
            return this;
        }

        public NumericAssertions<T> NotBeNaN()
        {
            var value = Convert.ToDouble(Subject);
            double.IsNaN(value).ShouldBeFalse();
            return this;
        }
    }

    public class NullableNumericAssertions<T> where T : struct, IComparable<T>
    {
        private readonly T? Subject;

        public NullableNumericAssertions(T? subject)
        {
            Subject = subject;
        }

        public AndConstraint<NullableNumericAssertions<T>> And => new AndConstraint<NullableNumericAssertions<T>>(this);

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