using System;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class EnumAssertions<T> where T : struct, Enum
    {
        private readonly T _subject;

        public EnumAssertions(T value)
        {
            _subject = value;
        }

        public AndConstraint<EnumAssertions<T>> And => new AndConstraint<EnumAssertions<T>>(this);

        public AndConstraint<EnumAssertions<T>> Be(T expected)
        {
            _subject.ShouldBe(expected);
            return new AndConstraint<EnumAssertions<T>>(this);
        }

        public AndConstraint<EnumAssertions<T>> NotBe(T expected)
        {
            _subject.ShouldNotBe(expected);
            return new AndConstraint<EnumAssertions<T>>(this);
        }

        public AndConstraint<EnumAssertions<T>> HaveFlag(T flag)
        {
            _subject.HasFlag(flag).ShouldBeTrue();
            return new AndConstraint<EnumAssertions<T>>(this);
        }

        public AndConstraint<EnumAssertions<T>> NotHaveFlag(T flag)
        {
            _subject.HasFlag(flag).ShouldBeFalse();
            return new AndConstraint<EnumAssertions<T>>(this);
        }

        public AndConstraint<EnumAssertions<T>> BeOneOf(params T[] validValues)
        {
            validValues.ShouldContain(_subject);
            return new AndConstraint<EnumAssertions<T>>(this);
        }

        public AndConstraint<EnumAssertions<T>> BeDefinedEnum()
        {
            Enum.IsDefined(typeof(T), _subject).ShouldBeTrue();
            return new AndConstraint<EnumAssertions<T>>(this);
        }

        public AndConstraint<EnumAssertions<T>> NotBeDefinedEnum()
        {
            Enum.IsDefined(typeof(T), _subject).ShouldBeFalse();
            return new AndConstraint<EnumAssertions<T>>(this);
        }

        public AndConstraint<EnumAssertions<T>> HaveValue(int value)
        {
            Convert.ToInt32(_subject).ShouldBe(value);
            return new AndConstraint<EnumAssertions<T>>(this);
        }

        public AndConstraint<EnumAssertions<T>> NotHaveValue(int value)
        {
            Convert.ToInt32(_subject).ShouldNotBe(value);
            return new AndConstraint<EnumAssertions<T>>(this);
        }

        public AndConstraint<EnumAssertions<T>> HaveFlags(params T[] flags)
        {
            foreach (var flag in flags)
            {
                _subject.HasFlag(flag).ShouldBeTrue();
            }
            return new AndConstraint<EnumAssertions<T>>(this);
        }

        public AndConstraint<EnumAssertions<T>> NotHaveFlags(params T[] flags)
        {
            foreach (var flag in flags)
            {
                _subject.HasFlag(flag).ShouldBeFalse();
            }
            return new AndConstraint<EnumAssertions<T>>(this);
        }

        public AndConstraint<EnumAssertions<T>> HaveExactFlags(params T[] flags)
        {
            var combinedFlags = flags.Aggregate((a, b) => (T)(object)((int)(object)a | (int)(object)b));
            _subject.ShouldBe(combinedFlags);
            return new AndConstraint<EnumAssertions<T>>(this);
        }

        public AndConstraint<EnumAssertions<T>> HaveAnyFlag(params T[] flags)
        {
            var hasAny = flags.Any(flag => _subject.HasFlag(flag));
            hasAny.ShouldBeTrue();
            return new AndConstraint<EnumAssertions<T>>(this);
        }

        public AndConstraint<EnumAssertions<T>> NotHaveAnyFlag(params T[] flags)
        {
            var hasAny = flags.Any(flag => _subject.HasFlag(flag));
            hasAny.ShouldBeFalse();
            return new AndConstraint<EnumAssertions<T>>(this);
        }
    }
} 