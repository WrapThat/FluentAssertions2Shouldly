using System;
using System.IO;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class StringAssertions
    {
        private readonly string Subject;

        public StringAssertions(string subject)
        {
            Subject = subject;
        }

        public StringAssertions Be(string expected)
        {
            Subject.ShouldBe(expected);
            return this;
        }

        public StringAssertions StartWith(string expected)
        {
            Subject.ShouldStartWith(expected);
            return this;
        }

        public StringAssertions EndWith(string expected)
        {
            Subject.ShouldEndWith(expected);
            return this;
        }

        public StringAssertions Contain(string expected)
        {
            Subject.ShouldContain(expected);
            return this;
        }

        public StringAssertions HaveLength(int expected)
        {
            Subject.Length.ShouldBe(expected);
            return this;
        }

        public StringAssertions BeEmpty()
        {
            Subject.ShouldBeEmpty();
            return this;
        }

        public StringAssertions BeEquivalentTo(string expected)
        {
            Subject.ShouldBe(expected, StringCompareShould.IgnoreCase);
            return this;
        }

        public StringAssertions NotBeNullOrWhiteSpace()
        {
            if (Subject == null)
            {
                throw new ShouldAssertException("Subject (null)\n    should not be null or white space");
            }
            Subject.ShouldNotBeNullOrWhiteSpace();
            return this;
        }

        public StringAssertions BeNullOrWhiteSpace()
        {
            Subject.ShouldBeNullOrWhiteSpace();
            return this;
        }

        public StringAssertions BeUpperCase()
        {
            Subject.ShouldBe(Subject.ToUpperInvariant());
            return this;
        }

        public StringAssertions BeLowerCase()
        {
            Subject.ShouldBe(Subject.ToLowerInvariant());
            return this;
        }

        public StringAssertions MatchRegex(string pattern)
        {
            Subject.ShouldMatch(pattern);
            return this;
        }

        public StringAssertions BeNullOrEmpty()
        {
            Subject.ShouldBeNullOrEmpty();
            return this;
        }

        public StringAssertions NotBeNullOrEmpty()
        {
            Subject.ShouldNotBeNullOrEmpty();
            return this;
        }

        public StringAssertions BeNull()
        {
            Subject.ShouldBeNull();
            return this;
        }

        public StringAssertions NotBeNull()
        {
            Subject.ShouldNotBeNull();
            return this;
        }

        public StringAssertions NotBe(string expected)
        {
            Subject.ShouldNotBe(expected);
            return this;
        }

        public StringAssertions BeOfType(Type expected)
        {
            Subject.ShouldBeOfType(expected);
            return this;
        }

        public StringAssertions BeOfType<TExpected>()
        {
            Subject.ShouldBeOfType<TExpected>();
            return this;
        }

        public AndConstraint<StringAssertions> Exist()
        {
            var fileInfo = new FileInfo(Subject);
            fileInfo.Should().Exist();
            return new AndConstraint<StringAssertions>(this);
        }

        public StringAssertions NotBeEmpty()
        {
            Subject.ShouldNotBeEmpty();
            return this;
        }
    }
} 