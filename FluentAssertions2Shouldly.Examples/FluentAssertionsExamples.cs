using FluentAssertions;
using Xunit;

namespace FluentAssertions2Shouldly.Examples;

public class FluentAssertionsExamples
{
    [Fact]
    public void StringAssertions_FluentAssertions()
    {
        string value = "test";
        value.Should().Be("test");
        value.Should().StartWith("te");
        value.Should().EndWith("st");
        value.Should().Contain("es");
        value.Should().HaveLength(4);
    }

    [Fact]
    public void NumericAssertions_FluentAssertions()
    {
        int number = 42;
        number.Should().Be(42);
        number.Should().BeGreaterThan(40);
        number.Should().BeLessThan(50);
        number.Should().BeInRange(40, 45);
    }

    [Fact]
    public void CollectionAssertions_FluentAssertions()
    {
        var list = new[] { 1, 2, 3 };
        list.Should().HaveCount(3);
        list.Should().Contain(2);
        list.Should().BeInAscendingOrder();
        list.Should().NotBeEmpty();
    }

    [Fact]
    public void ExceptionAssertions_FluentAssertions()
    {
        var action = () => throw new InvalidOperationException("error");
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("error");
    }

    [Fact]
    public async Task AsyncAssertions_FluentAssertions()
    {
        var task = Task.Delay(100);
        await task.Should().CompleteWithinAsync(TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void FileAssertions_FluentAssertions()
    {
        var file = new FileInfo("test.txt");
        file.Should().Exist();
        file.Should().HaveExtension(".txt");
    }

    [Fact]
    public void PropertyChangeAssertions_FluentAssertions()
    {
        var person = new Person();
        using var monitor = person.Monitor();
        
        person.Name = "John";
        
        monitor.Should().RaisePropertyChangeFor(x => x.Name);
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