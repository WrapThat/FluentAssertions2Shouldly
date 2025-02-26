using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FluentAssertions2Shouldly.Tests
{
    public class NewAssertionTests
    {
        [Fact]
        public void NumericTypeAssertions_ShouldWork()
        {
            // Int16
            short s = 42;
            s.Should().BeOfType(typeof(short));
            s.Should().BeOfType<short>();
            s.Should().BeAssignableTo<IComparable>();
            s.Should().BeAssignableTo(typeof(IComparable));

            // Int32
            int i = 42;
            i.Should().BeOfType(typeof(int));
            i.Should().BeOfType<int>();
            i.Should().BeAssignableTo<IComparable>();
            i.Should().BeAssignableTo(typeof(IComparable));

            // Int64
            long l = 42;
            l.Should().BeOfType(typeof(long));
            l.Should().BeOfType<long>();
            l.Should().BeAssignableTo<IComparable>();
            l.Should().BeAssignableTo(typeof(IComparable));

            // Single
            float f = 42.0f;
            f.Should().BeOfType(typeof(float));
            f.Should().BeOfType<float>();
            f.Should().BeAssignableTo<IComparable>();
            f.Should().BeAssignableTo(typeof(IComparable));

            // Double
            double d = 42.0;
            d.Should().BeOfType(typeof(double));
            d.Should().BeOfType<double>();
            d.Should().BeAssignableTo<IComparable>();
            d.Should().BeAssignableTo(typeof(IComparable));

            // Decimal
            decimal m = 42.0m;
            m.Should().BeOfType(typeof(decimal));
            m.Should().BeOfType<decimal>();
            m.Should().BeAssignableTo<IComparable>();
            m.Should().BeAssignableTo(typeof(IComparable));
        }

        [Fact]
        public void CollectionAssertions_ShouldWork()
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };

            // ContainInOrder
            list.Should().ContainInOrder(1, 2, 3);

            // BeSubsetOf
            list.Should().BeSubsetOf(new[] { 1, 2, 3, 4, 5, 6 });

            // HaveElementAt
            list.Should().HaveElementAt(0, 1);
            list.Should().HaveElementAt(4, 5);

            // IntersectWith
            list.Should().IntersectWith(new[] { 3, 4, 5, 6 });

            // NotIntersectWith
            list.Should().NotIntersectWith(new[] { 6, 7, 8 });

            // OnlyContainElementsThat
            list.Should().OnlyContainElementsThat(x => x > 0);
        }

        [Fact]
        public void DateTimeAssertions_ShouldWork()
        {
            var now = DateTime.Now;
            var utcNow = DateTime.UtcNow;

            // BeCloseTo
            now.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));

            // Component checks
            now.Should().HaveYear(DateTime.Now.Year);
            now.Should().HaveMonth(DateTime.Now.Month);
            now.Should().HaveDay(DateTime.Now.Day);

            // Date comparison
            now.Should().BeSameDateAs(DateTime.Today);

            // Kind checks
            now.Should().BeLocal();
            utcNow.Should().BeUtc();
        }

        [Fact]
        public void TypeAssertions_ShouldWork()
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
        public void DictionaryAssertions_ShouldWork()
        {
            var dict = new Dictionary<string, int>
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 }
            };

            // Multiple keys
            dict.Should().ContainKeys("one", "two");

            // Multiple values
            dict.Should().ContainValues(1, 2);

            // Key-value pairs
            dict.Should().HaveValueForKey("one", 1);
            dict.Should().HaveValueForKey("two", 2);
        }

        [Fact]
        public void RecordStructAssertions_ShouldWork()
        {
            var error = new TestError("test");
            error.Should().Be(new TestError("test"));
            error.Should().NotBe(new TestError("different"));
            error.Should().BeOfType<TestError>();
        }

        private readonly record struct TestError(string Message);
    }
} 