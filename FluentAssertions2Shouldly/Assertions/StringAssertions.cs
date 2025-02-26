using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class StringAssertions : ObjectAssertions<string>
    {
        public StringAssertions(string value) : base(value)
        {
        }

        public new StringAssertions And => this;

        public StringAssertions BeEmpty()
        {
            Subject.ShouldBeEmpty();
            return this;
        }

        public StringAssertions NotBeEmpty()
        {
            Subject.ShouldNotBeEmpty();
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

        public StringAssertions BeNullOrWhiteSpace()
        {
            Subject.ShouldBeNullOrWhiteSpace();
            return this;
        }

        public StringAssertions NotBeNullOrWhiteSpace()
        {
            if (Subject == null)
            {
                throw new ShouldAssertException("Subject (null)\n    should not be null or white space");
            }
            if (Subject.Length == 0)
            {
                throw new ShouldAssertException($"Subject ({Subject})\n    should not be null or white space");
            }
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
            var regex = new Regex(pattern);
            regex.IsMatch(Subject).ShouldBeTrue($"Expected string to match pattern '{pattern}' but it was '{Subject}'");
            return this;
        }

        public StringAssertions Match(string pattern, RegexOptions options = RegexOptions.None)
        {
            var regex = new Regex(pattern, options);
            regex.IsMatch(Subject).ShouldBeTrue($"Expected string to match pattern '{pattern}' but it was '{Subject}'");
            return this;
        }

        public StringAssertions NotMatch(string pattern, RegexOptions options = RegexOptions.None)
        {
            Subject.ShouldNotMatch(pattern);
            return this;
        }

        public new StringAssertions BeEquivalentTo(string expected)
        {
            if (Subject == null && expected == null) return this;
            if (Subject == null || expected == null)
            {
                throw new ShouldAssertException($"Expected string to be equivalent to {expected} but was {Subject}");
            }
            Subject.ToLowerInvariant().ShouldBe(expected.ToLowerInvariant());
            return this;
        }

        public new StringAssertions NotBeEquivalentTo(string expected)
        {
            if (Subject == null && expected == null)
            {
                throw new ShouldAssertException("Expected strings to not be equivalent but both were null");
            }
            if (Subject == null || expected == null) return this;
            Subject.ToLowerInvariant().ShouldNotBe(expected.ToLowerInvariant());
            return this;
        }

        public StringAssertions Contain(string expected)
        {
            Subject.ShouldContain(expected);
            return this;
        }

        public StringAssertions NotContain(string expected)
        {
            Subject.ShouldNotContain(expected);
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

        public StringAssertions ContainAll(params string[] expected)
        {
            foreach (var item in expected)
            {
                Subject.ShouldContain(item);
            }
            return this;
        }

        public StringAssertions ContainAny(params string[] expected)
        {
            bool containsAny = expected.Any(item => Subject.Contains(item));
            containsAny.ShouldBeTrue($"Expected string to contain any of [{string.Join(", ", expected)}] but found none");
            return this;
        }

        public StringAssertions NotContainAny(params string[] unexpected)
        {
            foreach (var item in unexpected)
            {
                Subject.ShouldNotContain(item);
            }
            return this;
        }

        public StringAssertions HaveLength(int expected)
        {
            Subject.Length.ShouldBe(expected);
            return this;
        }

        public new StringAssertions BeNull()
        {
            Subject.ShouldBeNull();
            return this;
        }

        public new StringAssertions NotBeNull()
        {
            Subject.ShouldNotBeNull();
            return this;
        }

        public StringAssertions NotBe(string expected)
        {
            Subject.ShouldNotBe(expected);
            return this;
        }

        public new StringAssertions BeOfType(Type expected)
        {
            Subject.ShouldBeOfType(expected);
            return this;
        }

        public new StringAssertions BeOfType<TExpected>()
        {
            Subject.ShouldBeOfType<TExpected>();
            return this;
        }

        public AndConstraint<StringAssertions> Exist()
        {
            var path = Subject;
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), path);
            }
            var fileInfo = new FileInfo(path);
            fileInfo.Refresh();
            fileInfo.Exists.ShouldBeTrue($"Expected file {fileInfo.FullName} to exist");
            return new AndConstraint<StringAssertions>(this);
        }

        public StringAssertions BeEquivalentTo(string expected, StringCompareShould comparison)
        {
            if (comparison == StringCompareShould.IgnoreCase)
            {
                Subject.ToLowerInvariant().ShouldBe(expected.ToLowerInvariant());
            }
            else
            {
                Subject.ShouldBe(expected);
            }
            return this;
        }
    }

    public enum StringCompareShould
    {
        IgnoreCase,
        RespectCase
    }
} 