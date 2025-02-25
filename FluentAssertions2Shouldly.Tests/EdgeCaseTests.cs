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
            // String
            string nullString = null;
            nullString.Should().BeNull();
            
            // Object
            object nullObject = null;
            nullObject.Should().BeNull();
            
            // Collections
            List<int> nullList = null;
            nullList.Should().BeNull();
            
            // Arrays
            int[] nullArray = null;
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
            double.PositiveInfinity.Should().Be(double.PositiveInfinity);
            double.NegativeInfinity.Should().Be(double.NegativeInfinity);
            
            // Float boundaries
            float.MaxValue.Should().Be(float.MaxValue);
            float.MinValue.Should().Be(float.MinValue);
            float.Epsilon.Should().BeGreaterThan(0);
            float.PositiveInfinity.Should().Be(float.PositiveInfinity);
            float.NegativeInfinity.Should().Be(float.NegativeInfinity);
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

        private class Node
        {
            public int Value { get; set; }
            public Node Next { get; set; }
        }
    }
} 