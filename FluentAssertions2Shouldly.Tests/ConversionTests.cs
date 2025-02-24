using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;
using Shouldly;

namespace FluentAssertions2Shouldly.Tests
{
    public class ConversionTests
    {
        [Fact]
        public void StringAssertions_ShouldWork()
        {
            // Basic equality
            "test".Should().Be("test");
            
            // String operations
            "Hello World".Should().StartWith("Hello");
            "Hello World".Should().EndWith("World");
            "Hello World".Should().Contain("lo Wo");
            "Hello World".Should().HaveLength(11);
            "Hello World".Should().NotBeNullOrWhiteSpace();
            
            // Edge cases
            string.Empty.Should().HaveLength(0);
            "".Should().Be(string.Empty);
            " ".Should().NotBeNullOrWhiteSpace();
            
            // Case sensitivity
            "TEST".Should().Be("TEST");
            "test".Should().NotBe("TEST");
        }

        [Fact]
        public void NumericAssertions_ShouldWork()
        {
            // Integer assertions
            42.Should().Be(42);
            42.Should().BeGreaterThan(41);
            42.Should().BeLessThan(43);
            42.Should().BeInRange(40, 45);
            
            // Double assertions
            3.14.Should().Be(3.14);
            3.14.Should().BeGreaterThan(3.13);
            3.14.Should().BeLessThan(3.15);
            3.14.Should().BeInRange(3.13, 3.15);
            
            // Edge cases
            0.Should().Be(0);
            int.MaxValue.Should().BeGreaterThan(0);
            int.MinValue.Should().BeLessThan(0);
        }

        [Fact]
        public void BooleanAssertions_ShouldWork()
        {
            // Basic assertions
            true.Should().BeTrue();
            false.Should().BeFalse();
            
            // Derived values
            (1 == 1).Should().BeTrue();
            (1 != 1).Should().BeFalse();
        }

        [Fact]
        public void CollectionAssertions_ShouldWork()
        {
            // Basic list operations
            var list = new List<int> { 1, 2, 3 };
            list.Should().HaveCount(3);
            list.Should().Contain(2);
            list.Should().BeInAscendingOrder();
            list.Should().NotBeEmpty();
            
            // Empty collections
            var empty = new List<int>();
            empty.Should().HaveCount(0);
            empty.Should().BeEmpty();
            
            // Complex collections
            var strings = new[] { "a", "b", "c" };
            strings.Should().HaveCount(3);
            strings.Should().Contain("b");
            
            // Edge cases
            var singleItem = new[] { 1 };
            singleItem.Should().HaveCount(1);
            singleItem.Should().NotBeEmpty();
            singleItem.Should().Contain(1);
        }

        [Fact]
        public void ObjectAssertions_ShouldWork()
        {
            // Null checks
            object obj = new object();
            obj.Should().NotBeNull();
            
            // Type checks
            "string".Should().BeOfType(typeof(string));
            123.Should().BeOfType(typeof(int));
            
            // Complex objects
            var person = new { Name = "John", Age = 30 };
            person.Should().NotBeNull();
            person.Should().BeOfType(person.GetType());
        }

        [Fact]
        public void ExceptionAssertions_ShouldWork()
        {
            // Basic exception
            Action action = () => throw new InvalidOperationException("test");
            action.Should().Throw<InvalidOperationException>()
                 .WithMessage("test");
            
            // Derived exceptions
            Action derived = () => throw new ArgumentNullException("param", "message");
            derived.Should().Throw<ArgumentException>()
                   .WithMessage("message (Parameter 'param')");
        }

        [Fact]
        public void DateTimeAssertions_ShouldWork()
        {
            var now = DateTime.Now;
            var past = now.AddDays(-1);
            var future = now.AddDays(1);
            
            now.Should().BeAfter(past);
            now.Should().BeBefore(future);
            
            // Edge cases
            now.Should().NotBe(DateTime.MinValue);
            now.Should().NotBe(DateTime.MaxValue);
        }

        [Fact]
        public void DictionaryAssertions_ShouldWork()
        {
            var dict = new Dictionary<string, int>
            {
                { "one", 1 },
                { "two", 2 }
            };
            
            dict.Should().ContainKey("one");
            dict.Should().ContainValue(1);
            
            // Edge cases
            dict.Should().HaveCount(2);
            dict.Should().NotBeEmpty();
            
            var empty = new Dictionary<string, int>();
            empty.Should().BeEmpty();
            empty.Should().HaveCount(0);
        }

        [Fact]
        public void EdgeCases_ShouldWork()
        {
            // Null string
            string nullString = null;
            nullString.Should().BeNull();
            
            // Empty collections
            IEnumerable<int> nullCollection = null;
            nullCollection.Should().BeNull();
            
            // Extreme numbers
            int.MaxValue.Should().Be(int.MaxValue);
            int.MinValue.Should().Be(int.MinValue);
            
            // DateTime extremes
            DateTime.MaxValue.Should().Be(DateTime.MaxValue);
            DateTime.MinValue.Should().Be(DateTime.MinValue);
        }

        [Fact]
        public void ComplexScenarios_ShouldWork()
        {
            // Nested collections
            var nested = new List<List<int>>
            {
                new List<int> { 1, 2 },
                new List<int> { 3, 4 }
            };
            
            nested.Should().HaveCount(2);
            nested.Should().NotBeEmpty();
            nested[0].Should().HaveCount(2);
            nested[1].Should().Contain(4);
            
            // Complex object graphs
            var complex = new Dictionary<string, List<int>>
            {
                { "odds", new List<int> { 1, 3, 5 } },
                { "evens", new List<int> { 2, 4, 6 } }
            };
            
            complex.Should().ContainKey("odds");
            complex["odds"].Should().HaveCount(3);
            complex["odds"].Should().Contain(3);
            complex["evens"].Should().BeInAscendingOrder();
        }
    }
} 