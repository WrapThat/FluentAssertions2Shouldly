using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Shouldly;

namespace FluentAssertions2Shouldly.Tests
{
    [Flags]
    public enum TestFlags
    {
        None = 0,
        Flag1 = 1,
        Flag2 = 2,
        Both = Flag1 | Flag2
    }

    public class SpecializedAssertionTests
    {
        [Fact]
        public void FileAssertions_ShouldWork()
        {
            // Create a temporary file
            var path = Path.GetTempFileName();
            File.WriteAllText(path, "test");
            
            try
            {
                var file = new FileInfo(path);
                file.Should().Exist();
                
                // Non-existent file
                var nonExistent = new FileInfo("non-existent.txt");
                nonExistent.Invoking(f => f.Should().Exist())
                          .Should().Throw<Exception>();
            }
            finally
            {
                File.Delete(path);
            }
        }

        [Fact]
        public async Task AsyncAssertions_ShouldWork()
        {
            // Successful completion
            Func<Task> success = async () => await Task.Delay(100);
            await success.Should().CompleteWithinAsync(TimeSpan.FromSeconds(1));
            
            // Timeout
            Func<Task> timeout = async () => await Task.Delay(2000);
            await timeout.Invoking(t => t.Should().CompleteWithinAsync(TimeSpan.FromMilliseconds(100)))
                        .Should().ThrowAsync<Exception>();
            
            // Exception throwing
            Func<Task> throwing = () => Task.FromException(new InvalidOperationException("test"));
            await throwing.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public void ApproximateEqualityAssertions_ShouldWork()
        {
            // Float assertions
            float f1 = 3.14159f;
            f1.Should().BeApproximately(3.14f, 0.01f);
            
            // Double assertions
            double d1 = 3.14159265359;
            d1.Should().BeApproximately(3.14159, 0.00001);
            
            // Edge cases
            float.Epsilon.Should().BeApproximately(0f, float.Epsilon);
            double.Epsilon.Should().BeApproximately(0d, double.Epsilon);
        }

        [Fact]
        public void EnumAssertions_ShouldWork()
        {
            // Single flag
            TestFlags value = TestFlags.Flag1;
            value.Should().HaveFlag(TestFlags.Flag1);
            
            // Combined flags
            TestFlags combined = TestFlags.Both;
            combined.Should().HaveFlag(TestFlags.Flag1);
            combined.Should().HaveFlag(TestFlags.Flag2);
            
            // No flags
            TestFlags none = TestFlags.None;
            none.Invoking(n => n.Should().HaveFlag(TestFlags.Flag1))
                .Should().Throw<Exception>();
        }

        [Fact]
        public void DelegateAssertions_ShouldWork()
        {
            // Non-throwing delegate
            Action success = () => { };
            success.Should().NotThrow();
            
            // Throwing delegate
            Action throwing = () => throw new InvalidOperationException();
            throwing.Invoking(t => t.Should().NotThrow())
                   .Should().Throw<Exception>();
        }

        [Fact]
        public void PropertyChangeAssertions_ShouldWork()
        {
            var person = new NotifyingPerson { Name = "John" };
            
            // Property change
            person.Should().RaisePropertyChangeFor<string>("Name");
            
            // Non-notifying object
            var nonNotifying = new { Name = "John" };
            nonNotifying.Invoking(n => n.Should().RaisePropertyChangeFor<string>("Name"))
                       .Should().Throw<ArgumentException>();
        }

        private class NotifyingPerson : INotifyPropertyChanged
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
} 