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
            // Int16 assertions
            short s1 = 42;
            s1.Should().BeAssignableTo<short>();
            s1.Should().BeAssignableTo(typeof(short));
            s1.Should().BeGreaterThan((short)40);
            s1.Should().BeLessThan((short)50);

            // Int32 assertions
            int i1 = 42;
            i1.Should().BeAssignableTo<int>();
            i1.Should().BeAssignableTo(typeof(int));
            i1.Should().BeGreaterThan(40);
            i1.Should().BeLessThan(50);

            // Int64 assertions
            long l1 = 42L;
            l1.Should().BeAssignableTo<long>();
            l1.Should().BeAssignableTo(typeof(long));
            l1.Should().BeGreaterThan(40L);
            l1.Should().BeLessThan(50L);

            // Single assertions
            float f1 = 42.5f;
            f1.Should().BeAssignableTo<float>();
            f1.Should().BeAssignableTo(typeof(float));
            f1.Should().BeGreaterThan(40.0f);
            f1.Should().BeLessThan(50.0f);
            f1.Should().BeApproximately(42.5f, 0.001f);

            // Double assertions
            double d1 = 42.5;
            d1.Should().BeAssignableTo<double>();
            d1.Should().BeAssignableTo(typeof(double));
            d1.Should().BeGreaterThan(40.0);
            d1.Should().BeLessThan(50.0);
            d1.Should().BeApproximately(42.5, 0.001);

            // Decimal assertions
            decimal m1 = 42.5m;
            m1.Should().BeAssignableTo<decimal>();
            m1.Should().BeAssignableTo(typeof(decimal));
            m1.Should().BeGreaterThan(40.0m);
            m1.Should().BeLessThan(50.0m);
            m1.Should().BeApproximately(42.5m, 0.001m);

            // Special value assertions
            float.PositiveInfinity.Should().BePositiveInfinity();
            float.NegativeInfinity.Should().BeNegativeInfinity();
            float.NaN.Should().BeNaN();
            double.PositiveInfinity.Should().BePositiveInfinity();
            double.NegativeInfinity.Should().BeNegativeInfinity();
            double.NaN.Should().BeNaN();

            // Zero assertions
            0.Should().BeZero();
            0.0f.Should().BeZero();
            0.0.Should().BeZero();
            0m.Should().BeZero();

            // Multiple assertions
            var num = 42;
            num.Should().BeGreaterThan(40);
            num.Should().BeLessThan(50);
            num.Should().BeInRange(40, 50);
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
            var error = new TestError("test error");
            var sameError = new TestError("test error");
            var differentError = new TestError("different error");

            // Basic equality assertions
            error.Should().Be(sameError);
            error.Should().NotBe(differentError);

            // Type assertions
            error.Should().BeOfType<TestError>();
            error.Should().BeAssignableTo<TestError>();

            // Collection assertions
            var errors = new[] { error, differentError };
            errors.Should().Contain(error);
            errors.Should().HaveCount(2);

            // Predicate assertions
            var predicate = new Func<TestError, bool>(e => e.Message == "test error");
            error.Should().Match(predicate);

            var containsError = new Func<TestError, bool>(e => e.Message.Contains("error"));
            error.Should().Match(containsError);
        }

        private readonly record struct TestError(string Message);
    }
} 