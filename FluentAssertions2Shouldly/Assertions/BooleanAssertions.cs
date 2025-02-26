using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class BooleanAssertions
    {
        private readonly bool _subject;

        public BooleanAssertions(bool value)
        {
            _subject = value;
        }

        public AndConstraint<BooleanAssertions> And => new AndConstraint<BooleanAssertions>(this);

        public BooleanAssertions BeTrue(string? because = null)
        {
            _subject.ShouldBeTrue(because);
            return this;
        }

        public BooleanAssertions BeFalse(string? because = null)
        {
            _subject.ShouldBeFalse(because);
            return this;
        }

        public BooleanAssertions NotBeFalse()
        {
            _subject.ShouldBeTrue(); // Equivalent to NotBeFalse
            return this;
        }

        public BooleanAssertions NotBeTrue()
        {
            _subject.ShouldBeFalse(); // Equivalent to NotBeTrue
            return this;
        }

        public BooleanAssertions Be(bool expected)
        {
            _subject.ShouldBe(expected);
            return this;
        }

        public BooleanAssertions NotBe(bool expected)
        {
            _subject.ShouldNotBe(expected);
            return this;
        }
    }
} 