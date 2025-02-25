using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Xunit;

namespace FluentAssertions2Shouldly.Tests
{
    public class ShouldlyApiExplorer
    {
        [Fact]
        public void DemonstrateMainAssertions()
        {
            // Create test file
            var filePath = "test.txt";
            File.WriteAllText(filePath, "test");

            try
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
                list.Should().HaveCount(3);                           // Equivalent to Should().HaveCount()
                list.ShouldContain(2);                            // Equivalent to Should().Contain()
                list.ShouldBeInOrder();                           // Equivalent to Should().BeInAscendingOrder()
                list.ShouldNotBeEmpty();                          // Equivalent to Should().NotBeEmpty()
                
                // Object assertions
                var obj = new { Name = "Test" };
                obj.Should().BeAssignableTo<object>();            // Fixed type syntax
                
                // Exception assertions
                Action action = () => throw new InvalidOperationException("test");
                action.Should().Throw<InvalidOperationException>().WithMessage("test");
                
                // DateTime assertions
                var date = DateTime.Now;
                date.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
                
                // Nullable assertions
                int? nullableInt = null;
                nullableInt.Should().BeNull();                    // Equivalent to Should().BeNull()
                
                // Dictionary assertions
                var dict = new Dictionary<string, int> { { "key", 1 } };
                dict.Should().ContainKey("key");                     // Equivalent to Should().ContainKey()
                dict.Values.ShouldContain(1);                     // Alternative to ContainValue()
                
                // Type assertions
                typeof(string).Should().Implement<IEnumerable<char>>();
                
                // File assertions
                new FileInfo(filePath).Should().Exist();  // Alternative to Exist()
                
                // Execution time assertions
                action = () => Thread.Sleep(100);
                action.Should().ExecuteWithin(TimeSpan.FromSeconds(1));
            }
            finally
            {
                // Cleanup
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
} 