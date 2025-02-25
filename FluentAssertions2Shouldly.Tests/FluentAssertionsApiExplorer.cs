using System;
using System.Collections.Generic;
using Xunit;

namespace FluentAssertions2Shouldly.Tests
{
    public class FluentAssertionsApiExplorer
    {
        [Fact]
        public void DemonstrateMainAssertions()
        {
            // String assertions
            string actual = "Hello World";
            actual.Should().Be("Hello World");
            actual.Should().StartWith("Hello");
            actual.Should().EndWith("World");
            actual.Should().Contain("lo Wo");
            actual.Should().HaveLength(11);
            actual.Should().NotBeNullOrWhiteSpace();
            
            // Numeric assertions
            int number = 5;
            number.Should().Be(5);
            number.Should().BeGreaterThan(4);
            number.Should().BeLessThan(6);
            number.Should().BeInRange(1, 10);
            
            // Boolean assertions
            bool flag = true;
            flag.Should().BeTrue();
            flag.Should().NotBeFalse();
            
            // Collection assertions
            var list = new List<int> { 1, 2, 3 };
            list.Should().HaveCount(3);
            list.Should().Contain(2);
            list.Should().BeInAscendingOrder();
            list.Should().NotBeEmpty();
            
            // Object assertions
            var obj = new { Name = "Test", Age = 30 };
            obj.Should().NotBeNull();
            obj.Should().BeOfType(obj.GetType());
            
            // Exception assertions
            Action action = () => throw new InvalidOperationException("test");
            action.Should().Throw<InvalidOperationException>()
                 .WithMessage("test");
            
            // DateTime assertions
            var date = DateTime.Now;
            date.Should().BeAfter(DateTime.MinValue);
            date.Should().BeBefore(DateTime.MaxValue);
            
            // Nullable assertions
            int? nullableNumber = null;
            nullableNumber.Should().BeNull();
            
            // Dictionary assertions
            var dict = new Dictionary<string, int> { { "one", 1 } };
            dict.Should().ContainKey("one");
            dict.Should().ContainValue(1);
            
            // Type assertions
            typeof(string).Should().Implement<IEnumerable<char>>();
            
            // File assertions
            "path/to/file.txt".Should().Exist();  // For file paths
            
            // Execution time assertions
            action = () => System.Threading.Thread.Sleep(100);
            action.Should().ExecuteWithin(TimeSpan.FromSeconds(1));
        }
    }
} 