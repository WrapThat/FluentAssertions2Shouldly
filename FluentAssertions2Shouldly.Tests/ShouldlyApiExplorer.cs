using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace FluentAssertions2Shouldly.Tests
{
    public class ShouldlyApiExplorer
    {
        [Fact]
        public void DemonstrateMainAssertions()
        {
            // String assertions
            string actual = "Hello World";
            actual.ShouldBe("Hello World");                     // Equivalent to Should().Be()
            actual.ShouldStartWith("Hello");                    // Equivalent to Should().StartWith()
            actual.ShouldEndWith("World");                      // Equivalent to Should().EndWith()
            actual.ShouldContain("lo Wo");                      // Equivalent to Should().Contain()
            actual.Length.ShouldBe(11);                         // Equivalent to Should().HaveLength()
            actual.ShouldNotBeNullOrWhiteSpace();              // Equivalent to Should().NotBeNullOrWhiteSpace()
            
            // Numeric assertions
            int number = 5;
            number.ShouldBe(5);                                // Equivalent to Should().Be()
            number.ShouldBeGreaterThan(4);                     // Equivalent to Should().BeGreaterThan()
            number.ShouldBeLessThan(6);                        // Equivalent to Should().BeLessThan()
            number.ShouldBeInRange(1, 10);                     // Equivalent to Should().BeInRange()
            
            // Boolean assertions
            bool flag = true;
            flag.ShouldBeTrue();                              // Equivalent to Should().BeTrue()
            flag.ShouldBe(true);                              // Alternative to NotBeFalse()
            
            // Collection assertions
            var list = new List<int> { 1, 2, 3 };
            list.Count.ShouldBe(3);                           // Equivalent to Should().HaveCount()
            list.ShouldContain(2);                            // Equivalent to Should().Contain()
            list.ShouldBeInOrder();                           // Equivalent to Should().BeInAscendingOrder()
            list.ShouldNotBeEmpty();                          // Equivalent to Should().NotBeEmpty()
            
            // Object assertions
            var obj = new { Name = "Test", Age = 30 };
            obj.ShouldNotBeNull();                            // Equivalent to Should().NotBeNull()
            obj.ShouldBeOfType(typeof(object));               // Fixed type syntax
            
            // Exception assertions
            Should.Throw<InvalidOperationException>(() =>      // Equivalent to Should().Throw()
                throw new InvalidOperationException("test"))
                .Message.ShouldBe("test");                    // Equivalent to WithMessage()
            
            // DateTime assertions
            var date = DateTime.Now;
            date.ShouldBeGreaterThan(DateTime.MinValue);      // Equivalent to Should().BeAfter()
            date.ShouldBeLessThan(DateTime.MaxValue);         // Equivalent to Should().BeBefore()
            
            // Nullable assertions
            int? nullableNumber = null;
            nullableNumber.ShouldBeNull();                    // Equivalent to Should().BeNull()
            
            // Dictionary assertions
            var dict = new Dictionary<string, int> { { "one", 1 } };
            dict.ShouldContainKey("one");                     // Equivalent to Should().ContainKey()
            dict.Values.ShouldContain(1);                     // Alternative to ContainValue()
            
            // Type assertions
            typeof(string).IsAssignableTo(typeof(IEnumerable<char>))  // Alternative to Implement
                         .ShouldBeTrue();
            
            // File assertions
            System.IO.File.Exists("path/to/file.txt").ShouldBeTrue();  // Alternative to Exist()
            
            // Execution time assertions
            Should.CompleteIn(() =>                           // Equivalent to Should().ExecuteWithin()
                System.Threading.Thread.Sleep(100), 
                TimeSpan.FromSeconds(1));
        }
    }
} 