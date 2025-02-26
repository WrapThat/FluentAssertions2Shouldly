using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions2Shouldly;
using System.Collections.Generic;
using System.Threading;

namespace FluentAssertions2Shouldly.Tests
{
    [Flags]
    public enum TestFlags
    {
        None = 0,
        One = 1,
        Two = 2,
        Three = One | Two
    }

    public class SpecializedAssertionTests
    {
        [Fact]
        public void FileAssertions_ShouldWork()
        {
            var file = new FileInfo("test.txt");
            File.WriteAllText(file.FullName, "content");

            try
            {
                file.Should().Exist();
                file.Should().HaveExtension(".txt");
                file.Should().HaveLength(7); // "content" is 7 bytes
            }
            finally
            {
                File.Delete(file.FullName);
            }
        }

        [Fact]
        public async Task AsyncAssertions_ShouldWork()
        {
            var task = Task.Delay(100);
            await task.Should().CompleteWithinAsync(TimeSpan.FromSeconds(1));

            using var cts = new CancellationTokenSource();
            Func<Task> cancelling = async () => {
                await Task.Delay(1000, cts.Token);
            };
            cts.Cancel();
            await cancelling.Should().ThrowAsync<TaskCanceledException>();
        }

        [Fact]
        public void NumericApproximationAssertions_ShouldWork()
        {
            float f1 = 3.14159f;
            f1.Should().BeInRange(3.14f, 3.15f);

            double d1 = 3.14159265359;
            d1.Should().BeInRange(3.14159, 3.14160);

            var calculated = Math.Sin(Math.PI);
            calculated.Should().BeInRange(-0.0000001, 0.0000001);
        }

        [Fact]
        public void EnumAssertions_ShouldWork()
        {
            TestFlags value = TestFlags.One;
            value.Should().Be(TestFlags.One);

            TestFlags combined = TestFlags.One | TestFlags.Two;
            combined.Should().Be(TestFlags.Three);

            TestFlags none = TestFlags.None;
            none.Should().Be(TestFlags.None);
        }

        [Fact]
        public void DelegateAssertions_ShouldWork()
        {
            Action successAction = () => { };
            successAction.Should().NotThrow<Exception>();

            Action failAction = () => throw new InvalidOperationException("error");
            failAction.Should().Throw<InvalidOperationException>()
                .WithMessage("error");
        }

        [Fact]
        public void PropertyChangeAssertions_ShouldWork()
        {
            var person = new Person();
            person.Should().RaisePropertyChangeFor("Name");
            person.Name = "John";
        }

        [Fact]
        public void TypeAssertions_Advanced_ShouldWork()
        {
            // Interface implementation
            typeof(string).Should().Implement<IEnumerable<char>>();
            typeof(string).Should().NotImplement<IDisposable>();

            // Property checks
            typeof(string).Should().HaveProperty("Length");
            typeof(string).Should().NotHaveProperty("InvalidProperty");

            // Method checks
            typeof(string).Should().HaveMethod("Substring");
            typeof(string).Should().NotHaveMethod("InvalidMethod");

            // Constructor checks
            typeof(string).Should().HaveConstructor();
            typeof(int).Should().NotHaveConstructor(); // value types

            // Inheritance
            typeof(ArgumentException).Should().BeDerivedFrom<Exception>();
            typeof(Exception).Should().NotBeDerivedFrom<ArgumentException>();
        }

        [Fact]
        public void NumericAssertions_EdgeCases_ShouldWork()
        {
            // Integer edge cases
            int.MaxValue.Should().BePositive();
            int.MinValue.Should().BeNegative();
            0.Should().Be(0); // Zero is neither positive nor negative

            // Double edge cases
            double.PositiveInfinity.Should().BeGreaterThan(0);
            double.NegativeInfinity.Should().BeLessThan(0);
            double.NaN.Should().NotBe(0);

            // Decimal precision
            decimal d1 = 0.1M + 0.2M;
            decimal d2 = 0.3M;
            d1.Should().Be(d2); // Exact decimal comparison

            // Floating point comparison
            float f1 = 0.1f + 0.2f;
            float f2 = 0.3f;
            f1.Should().BeApproximately(f2, 0.000001f);
        }

        [Fact]
        public void PropertyChangeAssertions_Advanced_ShouldWork()
        {
            var person = new Person();
            var monitor = person.MonitorPropertyChanges();

            // Single property change
            person.Name = "John";
            monitor.RaisePropertyChangeFor("Name");

            // Multiple property changes
            person.Name = "Jane";
            monitor.RaisePropertyChangeFor("Name");

            // Property change with timeout
            var task = Task.Run(() => {
                Thread.Sleep(100);
                person.Name = "Alice";
            });
            monitor.RaisePropertyChangeFor("Name", () => task.Wait());

            // No property change
            monitor.NotRaisePropertyChangeFor("Age");
        }

        [Fact]
        public async Task TaskAssertions_Advanced_ShouldWork()
        {
            // Completion within timeout
            var task = Task.Delay(100);
            await task.Should().CompleteWithinAsync(TimeSpan.FromSeconds(1));

            // Cancellation
            using var cts = new CancellationTokenSource();
            var cancelTask = Task.Delay(1000, cts.Token);
            cts.Cancel();
            await Assert.ThrowsAsync<TaskCanceledException>(() => cancelTask);

            // Task with result
            var resultTask = Task.FromResult(42);
            await resultTask;
            resultTask.Result.Should().Be(42);

            // Task with exception
            var failingTask = Task.FromException<int>(new InvalidOperationException("error"));
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => failingTask);
            ex.Message.Should().Be("error");

            // Long running task timeout
            var longTask = Task.Delay(5000);
            await longTask.Should().CompleteWithinAsync(TimeSpan.FromMilliseconds(100));
        }
    }

    public class Person : INotifyPropertyChanged
    {
        private string _name;
        public event PropertyChangedEventHandler PropertyChanged;

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