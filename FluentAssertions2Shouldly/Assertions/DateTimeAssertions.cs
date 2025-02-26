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

        public AndConstraint<DateTimeAssertions> And => new AndConstraint<DateTimeAssertions>(this);

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

        public DateTimeAssertions BeOnOrAfter(DateTime expected)
        {
            _subject.ShouldBeGreaterThanOrEqualTo(expected);
            return this;
        }

        public DateTimeAssertions BeOnOrBefore(DateTime expected)
        {
            _subject.ShouldBeLessThanOrEqualTo(expected);
            return this;
        }

        public DateTimeAssertions BeCloseTo(DateTime expected, TimeSpan precision)
        {
            var difference = (_subject - expected).Duration();
            difference.ShouldBeLessThanOrEqualTo(precision);
            return this;
        }

        public DateTimeAssertions NotBeCloseTo(DateTime expected, TimeSpan precision)
        {
            var difference = (_subject - expected).Duration();
            difference.ShouldBeGreaterThan(precision);
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

        public DateTimeAssertions HaveHour(int expected)
        {
            _subject.Hour.ShouldBe(expected);
            return this;
        }

        public DateTimeAssertions HaveMinute(int expected)
        {
            _subject.Minute.ShouldBe(expected);
            return this;
        }

        public DateTimeAssertions HaveSecond(int expected)
        {
            _subject.Second.ShouldBe(expected);
            return this;
        }

        public DateTimeAssertions HaveMillisecond(int expected)
        {
            _subject.Millisecond.ShouldBe(expected);
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

        public DateTimeAssertions BeOneOf(params DateTime[] expected)
        {
            expected.ShouldContain(_subject);
            return this;
        }

        public DateTimeAssertions NotBeOneOf(params DateTime[] unexpected)
        {
            unexpected.ShouldNotContain(_subject);
            return this;
        }
    }
} 