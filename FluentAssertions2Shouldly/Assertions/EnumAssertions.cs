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

        public EnumAssertions<T> Be(T expected)
        {
            _subject.ShouldBe(expected);
            return this;
        }

        public EnumAssertions<T> NotBe(T expected)
        {
            _subject.ShouldNotBe(expected);
            return this;
        }

        public EnumAssertions<T> HaveFlag(T flag)
        {
            _subject.HasFlag(flag).ShouldBeTrue();
            return this;
        }

        public EnumAssertions<T> NotHaveFlag(T flag)
        {
            _subject.HasFlag(flag).ShouldBeFalse();
            return this;
        }

        public EnumAssertions<T> BeOneOf(params T[] validValues)
        {
            validValues.ShouldContain(_subject);
            return this;
        }

        public EnumAssertions<T> BeDefinedEnum()
        {
            Enum.IsDefined(typeof(T), _subject).ShouldBeTrue();
            return this;
        }

        public EnumAssertions<T> NotBeDefinedEnum()
        {
            Enum.IsDefined(typeof(T), _subject).ShouldBeFalse();
            return this;
        }

        public EnumAssertions<T> HaveValue(int value)
        {
            Convert.ToInt32(_subject).ShouldBe(value);
            return this;
        }

        public EnumAssertions<T> NotHaveValue(int value)
        {
            Convert.ToInt32(_subject).ShouldNotBe(value);
            return this;
        }

        public EnumAssertions<T> HaveFlags(params T[] flags)
        {
            foreach (var flag in flags)
            {
                _subject.HasFlag(flag).ShouldBeTrue();
            }
            return this;
        }

        public EnumAssertions<T> NotHaveFlags(params T[] flags)
        {
            foreach (var flag in flags)
            {
                _subject.HasFlag(flag).ShouldBeFalse();
            }
            return this;
        }

        public EnumAssertions<T> HaveExactFlags(params T[] flags)
        {
            var combinedFlags = flags.Aggregate((a, b) => (T)(object)((int)(object)a | (int)(object)b));
            _subject.ShouldBe(combinedFlags);
            return this;
        }

        public AndConstraint<EnumAssertions<T>> And => new AndConstraint<EnumAssertions<T>>(this);
    }
} 