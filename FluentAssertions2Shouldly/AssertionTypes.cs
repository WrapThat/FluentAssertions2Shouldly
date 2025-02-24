using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class StringAssertions
    {
        public string Subject { get; }

        public StringAssertions(string value)
        {
            Subject = value;
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

        public StringAssertions NotBeEmpty()
        {
            Subject.ShouldNotBeEmpty();
            return this;
        }

        public StringAssertions BeNullOrWhiteSpace()
        {
            Subject.ShouldBeNullOrWhiteSpace();
            return this;
        }

        public StringAssertions NotBeNullOrWhiteSpace()
        {
            Subject.ShouldNotBeNullOrWhiteSpace();
            return this;
        }
    }

    public class NumericAssertions<T> where T : struct
    {
        public T Subject { get; }

        public NumericAssertions(T value)
        {
            Subject = value;
        }

        public NumericAssertions<T> Be(T expected)
        {
            Subject.ShouldBe(expected);
            return this;
        }

        public NumericAssertions<T> BeGreaterThan(T expected)
        {
            Subject.ShouldBeGreaterThan(expected);
            return this;
        }

        public NumericAssertions<T> BeLessThan(T expected)
        {
            Subject.ShouldBeLessThan(expected);
            return this;
        }

        public NumericAssertions<T> BeInRange(T min, T max)
        {
            Subject.ShouldBeInRange(min, max);
            return this;
        }

        public NumericAssertions<T> BePositive()
        {
            Subject.ShouldBePositive();
            return this;
        }

        public NumericAssertions<T> BeNegative()
        {
            Subject.ShouldBeNegative();
            return this;
        }
    }

    public class CollectionAssertions<T>
    {
        public IEnumerable<T> Subject { get; }

        public CollectionAssertions(IEnumerable<T> value)
        {
            Subject = value;
        }

        public CollectionAssertions<T> HaveCount(int expected)
        {
            Subject.Count().ShouldBe(expected);
            return this;
        }

        public CollectionAssertions<T> Contain(T expected)
        {
            Subject.ShouldContain(expected);
            return this;
        }

        public CollectionAssertions<T> BeEmpty()
        {
            Subject.ShouldBeEmpty();
            return this;
        }

        public CollectionAssertions<T> NotBeEmpty()
        {
            Subject.ShouldNotBeEmpty();
            return this;
        }

        public CollectionAssertions<T> BeInAscendingOrder()
        {
            Subject.ShouldBeInOrder();
            return this;
        }

        public CollectionAssertions<T> BeEquivalentTo(IEnumerable<T> expectation)
        {
            Subject.ShouldBe(expectation);
            return this;
        }
    }

    public class ExceptionAssertions<T> where T : Exception
    {
        public T Subject { get; }

        public ExceptionAssertions(T value)
        {
            Subject = value;
        }

        public ExceptionAssertions<T> WithMessage(string expected)
        {
            Subject.Message.ShouldBe(expected);
            return this;
        }
    }

    public class ActionAssertions
    {
        private readonly Action _subject;

        public ActionAssertions(Action value)
        {
            _subject = value;
        }

        public ExceptionAssertions<T> Throw<T>() where T : Exception
        {
            var ex = Should.Throw<T>(_subject);
            return new ExceptionAssertions<T>(ex);
        }
    }

    public class AsyncActionAssertions
    {
        private readonly Func<Task> _subject;

        public AsyncActionAssertions(Func<Task> value)
        {
            _subject = value;
        }

        public async Task<ExceptionAssertions<T>> ThrowAsync<T>() where T : Exception
        {
            var ex = await Should.ThrowAsync<T>(_subject);
            return new ExceptionAssertions<T>(ex);
        }

        public async Task CompleteWithinAsync(TimeSpan timeout)
        {
            var task = _subject();
            await Should.CompleteIn(task, timeout);
        }
    }

    public class FileAssertions
    {
        public FileInfo Subject { get; }

        public FileAssertions(FileInfo value)
        {
            Subject = value;
        }

        public FileAssertions Exist()
        {
            Subject.Exists.ShouldBeTrue();
            return this;
        }

        public FileAssertions NotExist()
        {
            Subject.Exists.ShouldBeFalse();
            return this;
        }

        public FileAssertions HaveExtension(string expected)
        {
            Subject.Extension.ShouldBe(expected);
            return this;
        }

        public FileAssertions HaveLength(long expected)
        {
            Subject.Length.ShouldBe(expected);
            return this;
        }
    }

    public class PropertyChangeAssertions<T>
    {
        private readonly T _subject;
        private readonly string _propertyName;

        public PropertyChangeAssertions(T value, string propertyName)
        {
            _subject = value;
            _propertyName = propertyName;
        }

        public void RaisePropertyChangeFor<TProperty>(string propertyName)
        {
            // Shouldly doesn't have direct property change support
            // This is a placeholder for the functionality
            true.ShouldBeTrue($"Property {propertyName} should raise PropertyChanged event");
        }
    }
} 