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
            42.Should().BeInRange(40, 45);
            
            // Sign assertions
            42.Should().BePositive();
            (-42).Should().BeNegative();
            
            // Set assertions
            42.Should().BeOneOf(41, 42, 43);
            
            // Approximate assertions
            3.14159.Should().BeApproximately(3.14, 0.01);
            
            // Type assertions
            42.Should().BeOfType(typeof(int));
            42.Should().BeOfType<int>();
        }

        [Fact]
        public void CollectionAssertions_AllMethods()
        {
            var list = new List<int> { 1, 2, 3 };
            
            // Basic assertions
            list.Should().HaveCount(3);
            list.Should().NotBeEmpty();
            list.Should().Contain(2);
            list.Should().NotContain(4);
            
            // Order assertions
            list.Should().BeInAscendingOrder();
            list.Should().ContainInOrder(1, 2, 3);
            
            // Content assertions
            list.Should().OnlyContainElementsThat(x => x > 0);
            list.Should().BeEquivalentTo(new[] { 3, 2, 1 });
            list.Should().BeSubsetOf(new[] { 1, 2, 3, 4 });
            
            // Single element assertions
            list.Should().ContainSingle(x => x == 2);
            
            // Position assertions
            list.Should().HaveElementAt(0, 1);
            list.Should().StartWith(1);
            list.Should().EndWith(3);
            
            // Intersection assertions
            list.Should().IntersectWith(new[] { 2, 3, 4 });
            list.Should().NotIntersectWith(new[] { 4, 5, 6 });
        }

        [Fact]
        public void DictionaryAssertions_AllMethods()
        {
            var dict = new Dictionary<string, int> { { "one", 1 }, { "two", 2 } };
            
            // Basic assertions
            dict.Should().HaveCount(2);
            dict.Should().NotBeEmpty();
            
            // Key assertions
            dict.Should().ContainKey("one");
            dict.Should().ContainKeys("one", "two");
            
            // Value assertions
            dict.Should().ContainValue(1);
            dict.Should().ContainValues(1, 2);
            dict.Should().HaveValue(1);
            dict.Should().HaveValueForKey("one", 1);
            
            // Empty dictionary assertions
            new Dictionary<string, int>().Should().BeEmpty();
        }

        [Fact]
        public void BooleanAssertions_AllMethods()
        {
            // Basic assertions
            true.Should().BeTrue();
            false.Should().BeFalse();
            true.Should().NotBeFalse();
            
            // Expression assertions
            (1 == 1).Should().BeTrue();
            (1 != 1).Should().BeFalse();
        }

        [Fact]
        public void DateTimeAssertions_AllMethods()
        {
            var now = DateTime.Now;
            var past = now.AddDays(-1);
            var future = now.AddDays(1);
            
            // Basic assertions
            now.Should().Be(now);
            now.Should().NotBe(past);
            
            // Comparison assertions
            now.Should().BeAfter(past);
            now.Should().BeBefore(future);
            now.Should().BeCloseTo(now.AddMilliseconds(100), TimeSpan.FromSeconds(1));
            
            // Component assertions
            now.Should().HaveYear(now.Year);
            now.Should().HaveMonth(now.Month);
            now.Should().HaveDay(now.Day);
            
            // Date assertions
            now.Should().BeSameDateAs(now.Date);
            
            // Kind assertions
            DateTime.UtcNow.Should().BeUtc();
            DateTime.Now.Should().BeLocal();
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

        private class TestPerson : INotifyPropertyChanged
        {
            private string _name = string.Empty;
            public event PropertyChangedEventHandler? PropertyChanged;

            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }
    }
} 