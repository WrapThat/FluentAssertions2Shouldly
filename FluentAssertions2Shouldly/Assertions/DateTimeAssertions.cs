using System;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class DateTimeAssertions
    {
        private readonly DateTime _subject;

        public DateTimeAssertions(DateTime value)
        {
            _subject = value;
        }

        public DateTimeAssertions Be(DateTime expected)
        {
            _subject.ShouldBe(expected);
            return this;
        }

        public DateTimeAssertions NotBe(DateTime expected)
        {
            _subject.ShouldNotBe(expected);
            return this;
        }

        public DateTimeAssertions BeAfter(DateTime expected)
        {
            _subject.ShouldBeGreaterThan(expected);
            return this;
        }

        public DateTimeAssertions BeBefore(DateTime expected)
        {
            _subject.ShouldBeLessThan(expected);
            return this;
        }

        public DateTimeAssertions BeCloseTo(DateTime expected, TimeSpan precision)
        {
            var difference = (_subject - expected).Duration();
            difference.ShouldBeLessThanOrEqualTo(precision);
            return this;
        }

        public DateTimeAssertions HaveYear(int expected)
        {
            _subject.Year.ShouldBe(expected);
            return this;
        }

        public DateTimeAssertions HaveMonth(int expected)
        {
            _subject.Month.ShouldBe(expected);
            return this;
        }

        public DateTimeAssertions HaveDay(int expected)
        {
            _subject.Day.ShouldBe(expected);
            return this;
        }

        public DateTimeAssertions BeSameDateAs(DateTime expected)
        {
            _subject.Date.ShouldBe(expected.Date);
            return this;
        }

        public DateTimeAssertions BeUtc()
        {
            _subject.Kind.ShouldBe(DateTimeKind.Utc);
            return this;
        }

        public DateTimeAssertions BeLocal()
        {
            _subject.Kind.ShouldBe(DateTimeKind.Local);
            return this;
        }
    }
} 