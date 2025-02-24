using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions2Shouldly;

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