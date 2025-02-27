using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using System.IO;
using System.ComponentModel;
using System.Threading;

namespace FluentAssertions2Shouldly.Tests
{
    public class AssertionTests
    {
        [Fact]
        public void StringAssertions_AllMethods()
        {
            // Basic assertions
            "test".Should().Be("test");
            "test".Should().NotBe("other");
            "test".Should().BeEquivalentTo("TEST");
            
            // Content assertions
            "Hello World".Should().StartWith("Hello");
            "Hello World".Should().EndWith("World");
            "Hello World".Should().Contain("lo Wo");
            "Hello World".Should().HaveLength(11);
            
            // Empty/null assertions
            string.Empty.Should().BeEmpty();
            "".Should().BeEmpty();
            "test".Should().NotBeNullOrWhiteSpace();
            " ".Should().NotBeEmpty();
            ((string)null).Should().BeNull();
            "test".Should().NotBeNull();
            
            // Case assertions
            "TEST".Should().BeUpperCase();
            "test".Should().BeLowerCase();
            
            // Pattern assertions
            "test123".Should().MatchRegex(@"^test\d+$");
            
            // Type assertions
            "test".Should().BeOfType(typeof(string));
            "test".Should().BeOfType<string>();
        }

        [Fact]
        public void NumericAssertions_AllMethods()
        {
            // Basic assertions
            42.Should().Be(42);
            42.Should().NotBe(43);
            
            // Comparison assertions
            42.Should().BeGreaterThan(41);
            42.Should().BeLessThan(43);
            42.Should().BeGreaterThanOrEqualTo(42);
            42.Should().BeGreaterThanOrEqualTo(41);
            42.Should().BeLessThanOrEqualTo(42);
            42.Should().BeLessThanOrEqualTo(43);
            42.Should().BeInRange(40, 45);
            
            // Sign assertions
            42.Should().BePositive();
            (-42).Should().BeNegative();
            
            // Set assertions
            42.Should().BeOneOf(41, 42, 43);
            42.Should().NotBeOneOf(43, 44, 45);
            
            // Approximate assertions
            3.14159.Should().BeApproximately(3.14, 0.01);
            3.14159.Should().BeCloseTo(3.14, 0.01);
            3.14159.Should().NotBeCloseTo(3.0, 0.1);
            
            // Type assertions
            42.Should().BeOfType(typeof(int));
            42.Should().BeOfType<int>();
            
            // Special value assertions
            double.PositiveInfinity.Should().BeInfinity();
            double.NaN.Should().BeNaN();
            42.0.Should().NotBeNaN();
            
            // Zero assertions
            0.Should().BeZero();
            42.Should().NotBeZero();
            
            // Nullable assertions
            int? nullValue = null;
            nullValue.Should().BeNull();
            int? nonNullValue = 42;
            nonNullValue.Should().NotBeNull();
        }

        [Fact]
        public void CollectionAssertions_AllMethods()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3 };
            var descendingList = new List<int> { 5, 4, 3, 2, 1 };
            var singleItemList = new List<int> { 42 };
            var nullableList = new List<string?> { "test", null, "value" };
            var positiveNumbers = new List<int> { 1, 2, 3, 4, 5 };

            // Act & Assert
            list.Should().HaveCount(3);
            list.Should().NotBeEmpty();
            list.Should().Contain(2);
            list.Should().BeInAscendingOrder();
            
            // Test BeInDescendingOrder
            descendingList.Should().BeInDescendingOrder();
            
            // Test ContainInConsecutiveOrder
            list.Should().ContainInConsecutiveOrder(1, 2, 3);
            
            // Test OnlyContain
            positiveNumbers.Should().OnlyContain(1, 2, 3, 4, 5);
            
            // Test NotBeEquivalentTo
            list.Should().NotBeEquivalentTo(new List<int> { 4, 5, 6 });
            
            // Test ContainSingle
            singleItemList.Should().ContainSingle();
            singleItemList.Should().ContainSingle(x => x == 42);
            
            // Test SatisfyRespectively
            list.Should().SatisfyRespectively(
                first => first.Should().Be(1),
                second => second.Should().Be(2),
                third => third.Should().Be(3)
            );
            
            // Test NotContainNulls
            var nonNullStrings = new List<string> { "test", "value" };
            nonNullStrings.Should().NotContainNulls();
            
            // Test existing assertions
            list.Should().BeEquivalentTo(new[] { 1, 2, 3 });
            list.Should().NotContain(4);
            list.Should().ContainInOrder(1, 2, 3);
        }

        [Fact]
        public void DictionaryAssertions_AllMethods()
        {
            // Basic dictionary setup
            var dict = new Dictionary<string, int> { { "one", 1 }, { "two", 2 } };
            var emptyDict = new Dictionary<string, int>();
            var singleDict = new Dictionary<string, int> { { "single", 1 } };
            var supersetDict = new Dictionary<string, int> { { "one", 1 }, { "two", 2 }, { "three", 3 } };

            // Basic assertions
            dict.Should().ContainKey("one");
            dict.Should().ContainValue(2);
            dict.Should().ContainPair("one", 1);
            dict.Should().NotContainKey("three");
            dict.Should().HaveCount(2);

            // Multiple key/value assertions
            dict.Should().ContainKeys("one", "two");
            dict.Should().ContainValues(1, 2);

            // Value retrieval
            dict.Should().HaveValueForKey("one", 1);
            dict.Should().HaveValue(2);

            // Negative assertions
            dict.Should().NotContainPair("one", 2);
            dict.Should().NotBeEmpty();

            // Empty dictionary assertions
            emptyDict.Should().BeEmpty();
            emptyDict.Should().HaveCount(0);

            // Single item assertions
            singleDict.Should().ContainSingle();
            singleDict.Should().HaveCount(1);

            // Subset/superset assertions
            dict.Should().BeSubsetOf(supersetDict);
            dict.Should().NotBeSubsetOf(singleDict);

            // Chained assertions
            dict.Should()
                .ContainKey("one")
                .And.ContainValue(2)
                .And.HaveCount(2);
        }

        [Fact]
        public void BooleanAssertions_AllMethods()
        {
            // Arrange
            bool trueValue = true;
            bool falseValue = false;

            // Act & Assert
            trueValue.Should().BeTrue();
            falseValue.Should().BeFalse();
            trueValue.Should().NotBeFalse();
            falseValue.Should().NotBeTrue();
            
            // Test Be and NotBe
            trueValue.Should().Be(true);
            falseValue.Should().Be(false);
            trueValue.Should().NotBe(false);
            falseValue.Should().NotBe(true);
        }

        [Fact]
        public void DateTimeAssertions_AllMethods()
        {
            var now = DateTime.Now;
            var past = now.AddDays(-1);
            var future = now.AddDays(1);
            var nearNow = now.AddSeconds(2);
            var farFromNow = now.AddHours(1);
            var specificTime = new DateTime(2024, 3, 14, 15, 30, 45, 500);
            
            // Basic assertions
            now.Should().Be(now);
            now.Should().NotBe(past);
            
            // Comparison assertions
            now.Should().BeAfter(past);
            now.Should().BeBefore(future);
            now.Should().BeOnOrAfter(past);
            now.Should().BeOnOrAfter(now);
            now.Should().BeOnOrBefore(future);
            now.Should().BeOnOrBefore(now);
            
            // Closeness assertions
            now.Should().BeCloseTo(nearNow, TimeSpan.FromSeconds(5));
            now.Should().NotBeCloseTo(farFromNow, TimeSpan.FromMinutes(30));
            
            // Component assertions
            specificTime.Should().HaveYear(2024);
            specificTime.Should().HaveMonth(3);
            specificTime.Should().HaveDay(14);
            specificTime.Should().HaveHour(15);
            specificTime.Should().HaveMinute(30);
            specificTime.Should().HaveSecond(45);
            specificTime.Should().HaveMillisecond(500);
            
            // Date assertions
            now.Should().BeSameDateAs(now.Date);
            
            // Kind assertions
            DateTime.UtcNow.Should().BeUtc();
            DateTime.Now.Should().BeLocal();
            
            // Set membership assertions
            var dates = new[] { past, now, future };
            now.Should().BeOneOf(dates);
            farFromNow.Should().NotBeOneOf(dates);
        }

        [Fact]
        public async Task TaskAssertions_AllMethods()
        {
            // Completion assertions
            await Task.CompletedTask.Should().CompleteWithinAsync(TimeSpan.FromSeconds(1));
            
            // Exception assertions
            var failingTask = Task.FromException(new InvalidOperationException());
            await failingTask.Should().ThrowAsync<InvalidOperationException>();
            
            // No exception assertions
            await Task.CompletedTask.Should().NotThrowAsync();
        }

        [Fact]
        public void TypeAssertions_AllMethods()
        {
            // Basic assertions
            typeof(string).Should().Be(typeof(string));
            typeof(string).Should().NotBe(typeof(int));
            
            // Inheritance assertions
            typeof(string).Should().BeDerivedFrom<object>();
            typeof(string).Should().BeDerivedFrom(typeof(object));
            
            // Member assertions
            typeof(string).Should().HaveProperty("Length");
            typeof(string).Should().HaveMethod("Substring");
            typeof(string).Should().HaveConstructor();
        }

        [Fact]
        public void FileAssertions_AllMethods()
        {
            var file = new FileInfo("test.txt");
            File.WriteAllText(file.FullName, "test");
            
            try
            {
                // Basic assertions
                file.Should().Exist();
                file.Should().HaveExtension(".txt");
                file.Should().HaveLength(4); // "test" is 4 bytes
                
                File.Delete(file.FullName);
                file.Should().NotExist();
            }
            finally
            {
                if (File.Exists(file.FullName))
                {
                    File.Delete(file.FullName);
                }
            }
        }

        [Fact]
        public void PropertyChangeAssertions_AllMethods()
        {
            var person = new TestPerson();
            var monitor = person.MonitorPropertyChanges();
            person.Name = "John";
            monitor.RaisePropertyChangeFor("Name");
        }

        [Flags]
        private enum TestFlags
        {
            None = 0,
            One = 1,
            Two = 2,
            Four = 4,
            All = One | Two | Four
        }

        [Fact]
        public void EnumAssertions_AllMethods()
        {
            // Basic assertions
            TestFlags value = TestFlags.One;
            value.Should().Be(TestFlags.One);
            value.Should().NotBe(TestFlags.Two);
            value.Should().BeDefinedEnum();
            value.Should().HaveValue(1);
            value.Should().NotHaveValue(2);

            // Flag assertions
            value.Should().HaveFlag(TestFlags.One);
            value.Should().NotHaveFlag(TestFlags.Two);

            // Multiple flags
            TestFlags combined = TestFlags.One | TestFlags.Two;
            combined.Should().HaveFlags(TestFlags.One, TestFlags.Two);
            combined.Should().NotHaveFlag(TestFlags.Four);
            combined.Should().HaveValue(3);
            combined.Should().HaveExactFlags(TestFlags.One, TestFlags.Two);

            // Set membership
            value.Should().BeOneOf(TestFlags.None, TestFlags.One, TestFlags.Two);

            // Chaining assertions
            value.Should()
                .Be(TestFlags.One);

            value.Should().HaveFlags(TestFlags.One);
            value.Should().NotHaveFlags(TestFlags.Two);
            value.Should().BeDefinedEnum();

            // Invalid value
            TestFlags invalid = (TestFlags)99;
            invalid.Should().NotBeDefinedEnum();
        }

        [Fact]
        public void ObjectAssertions_AllMethods()
        {
            // Test Which property
            var obj = new { Value = 42 };
            obj.Should().NotBeNull().Which.Should().BeAssignableTo<object>();

            // Test As method
            var baseClass = new DerivedClass();
            baseClass.Should().As<BaseClass>().NotBeNull();

            // Test delegate assertions
            Func<int> throwingFunc = () => throw new InvalidOperationException();
            Func<int> nonThrowingFunc = () => 42;

            var throwingAssertion = new ObjectAssertions<Func<int>>(throwingFunc);
            throwingAssertion.ThrowExactly<InvalidOperationException>();

            var nonThrowingAssertion = new ObjectAssertions<Func<int>>(nonThrowingFunc);
            nonThrowingAssertion.NotThrow();
        }

        [Fact]
        public void ExceptionAssertions_AllMethods()
        {
            // Arrange
            var innerException = new ArgumentException("Inner exception message");
            var exception = new InvalidOperationException("Test exception message", innerException);
            
            // Act & Assert
            // Message assertions
            ExceptionExtensions.Should(exception).WithMessage("Test exception message");
            ExceptionExtensions.Should(exception).WithMessage("TEST EXCEPTION MESSAGE", ignoreCase: true);
            ExceptionExtensions.Should(exception).WithMessageContaining("exception message");
            ExceptionExtensions.Should(exception).WithMessageContaining("EXCEPTION", ignoreCase: true);
            
            // Inner exception assertions
            ExceptionExtensions.Should(exception).WithInnerException<ArgumentException>();
            ExceptionExtensions.Should(exception).WithInnerExceptionExactly<ArgumentException>();
            
            // Multiple assertions (without chaining)
            var exceptionAssertions = ExceptionExtensions.Should(exception);
            exceptionAssertions.WithMessage("Test exception message");
            exceptionAssertions.WithInnerException<ArgumentException>();
            
            // Test with different exception types
            var customException = new CustomTestException("Custom message", new ArgumentNullException("null arg"));
            var customExceptionAssertions = ExceptionExtensions.Should(customException);
            customExceptionAssertions.WithMessage("Custom message");
            customExceptionAssertions.WithInnerExceptionExactly<ArgumentNullException>();
        }

        private class TestPerson : INotifyPropertyChanged
        {
            private string _name = string.Empty;
            public event PropertyChangedEventHandler? PropertyChanged;

            public string Name
            {
                get => _name;
                set
                {
                    if (_name != value)
                    {
                        _name = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                    }
                }
            }
        }

        private class BaseClass { }
        private class DerivedClass : BaseClass { }

        private class CustomTestException : Exception
        {
            public CustomTestException(string message, Exception innerException) 
                : base(message, innerException)
            {
            }
        }
    }
} 