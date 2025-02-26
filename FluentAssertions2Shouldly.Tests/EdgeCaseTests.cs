using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions2Shouldly;
using Shouldly;

namespace FluentAssertions2Shouldly.Tests
{
    public class EdgeCaseTests
    {
        [Fact]
        public void NullValues_ShouldWork()
        {
            // String assertions
            string? nullString = null;
            nullString.Should().BeNull();

            // Object assertions
            object? nullObject = null;
            ((object?)nullObject).Should().BeNull();

            // Collection assertions
            List<int>? nullList = null;
            nullList.Should().BeNull();

            // Array assertions
            int[]? nullArray = null;
            nullArray.Should().BeNull();
        }

        [Fact]
        public void EmptyValues_ShouldWork()
        {
            // String
            string.Empty.Should().BeEmpty();
            "".Should().BeEmpty();
            
            // Collections
            new List<int>().Should().BeEmpty();
            Array.Empty<string>().Should().BeEmpty();
            new Dictionary<string, int>().Should().BeEmpty();
        }

        [Fact]
        public void NumericBoundaries_ShouldWork()
        {
            // Integer boundaries
            int.MaxValue.Should().Be(int.MaxValue);
            int.MinValue.Should().Be(int.MinValue);
            
            // Double boundaries
            double.MaxValue.Should().Be(double.MaxValue);
            double.MinValue.Should().Be(double.MinValue);
            double.Epsilon.Should().BeGreaterThan(0);
            double.PositiveInfinity.Should().BePositiveInfinity();
            double.NegativeInfinity.Should().BeNegativeInfinity();
            double.NaN.Should().BeNaN();
            
            // Float boundaries
            float.MaxValue.Should().Be(float.MaxValue);
            float.MinValue.Should().Be(float.MinValue);
            float.Epsilon.Should().BeGreaterThan(0);
            float.PositiveInfinity.Should().BePositiveInfinity();
            float.NegativeInfinity.Should().BeNegativeInfinity();
            float.NaN.Should().BeNaN();

            // Zero values
            0.Should().BeZero();
            0.0.Should().BeZero();
            0f.Should().BeZero();
            0m.Should().BeZero();

            // Finite values
            42.Should().BeFinite();
            3.14159.Should().BeFinite();
            1.23f.Should().BeFinite();
            123.456m.Should().BeFinite();
        }

        [Fact]
        public void WhitespaceStrings_ShouldWork()
        {
            // Empty string
            "".Should().BeEmpty();
            
            // Whitespace
            " ".Should().NotBeEmpty();
            " ".Should().NotBeNull();
            " ".Should().HaveLength(1);
            
            // Tabs and newlines
            "\t".Should().NotBeEmpty();
            "\n".Should().NotBeEmpty();
            "\r\n".Should().NotBeEmpty();
            
            // Mixed whitespace
            "   \t\n\r   ".Should().NotBeEmpty();
        }

        [Fact]
        public void CircularReferences_ShouldWork()
        {
            var node1 = new Node { Value = 1 };
            var node2 = new Node { Value = 2 };
            node1.Next = node2;
            node2.Next = node1;

            node1.Should().NotBeNull();
            node1.Value.Should().Be(1);
            node1.Next.Should().NotBeNull();
            node1.Next.Value.Should().Be(2);
        }

        [Fact]
        public async Task AsyncEdgeCases_ShouldWork()
        {
            // Completed task
            var completed = Task.CompletedTask;
            await completed.Should().CompleteWithinAsync(TimeSpan.FromMilliseconds(1));
            
            // Canceled task
            var canceled = Task.FromCanceled(new System.Threading.CancellationToken(true));
            await canceled.Invoking(t => t.Should().CompleteWithinAsync(TimeSpan.FromSeconds(1)))
                         .Should().ThrowAsync<Exception>();
            
            // Faulted task
            var faulted = Task.FromException(new InvalidOperationException());
            await faulted.Invoking(t => t.Should().CompleteWithinAsync(TimeSpan.FromSeconds(1)))
                        .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public void EnumFlags_ShouldWork()
        {
            // Single flag
            TestFlags.One.Should()
                .Be(TestFlags.One)
                .And.HaveFlag(TestFlags.One)
                .And.NotHaveFlag(TestFlags.Two)
                .And.BeDefinedEnum();

            // Combined flags
            var combined = TestFlags.One | TestFlags.Two;
            combined.Should()
                .HaveFlags(TestFlags.One, TestFlags.Two)
                .And.NotHaveFlag(TestFlags.Four)
                .And.HaveValue((int)(TestFlags.One | TestFlags.Two));

            // Any flag
            combined.Should()
                .HaveAnyFlag(TestFlags.One, TestFlags.Four)
                .And.NotHaveAnyFlag(TestFlags.All);

            // No flags
            TestFlags.None.Should()
                .Be(TestFlags.None)
                .And.NotHaveAnyFlag(TestFlags.One, TestFlags.Two, TestFlags.Four);

            // All flags
            TestFlags.All.Should()
                .Be(TestFlags.All)
                .And.HaveFlags(TestFlags.One, TestFlags.Two, TestFlags.Four)
                .And.HaveExactFlags(TestFlags.One, TestFlags.Two, TestFlags.Four);
        }

        private class Node
        {
            public int Value { get; set; }
            public Node? Next { get; set; }
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
    }
} 