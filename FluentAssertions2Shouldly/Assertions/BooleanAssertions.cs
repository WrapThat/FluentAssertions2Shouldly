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

        public BooleanAssertions BeTrue()
        {
            _subject.ShouldBeTrue();
            return this;
        }

        public BooleanAssertions BeFalse()
        {
            _subject.ShouldBeFalse();
            return this;
        }

        public BooleanAssertions NotBeFalse()
        {
            _subject.ShouldBeTrue(); // Equivalent to NotBeFalse
            return this;
        }
    }
} 