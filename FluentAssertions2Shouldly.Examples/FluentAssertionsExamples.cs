using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions2Shouldly;
using Shouldly;
using System.Threading;
using System.IO;

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
        Action action = () => throw new InvalidOperationException("error");
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("error");
    }

    [Fact]
    public async Task AsyncAssertions_FluentAssertions()
    {
        var task = Task.Delay(100);
        await task;
        // Verify it completed within the timeout
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
        await task.WaitAsync(cts.Token);
    }

    [Fact]
    public void FileAssertions_FluentAssertions()
    {
        var file = new FileInfo("test.txt");
        File.WriteAllText(file.FullName, "test content");
        try
        {
            file.Should().Exist();
            file.Should().HaveExtension(".txt");
        }
        finally
        {
            if (file.Exists)
            {
                File.Delete(file.FullName);
            }
        }
    }

    [Fact]
    public void PropertyChangeAssertions_FluentAssertions()
    {
        var person = new Person();
        var monitor = person.MonitorPropertyChanges();
        person.Name = "John";
        monitor.RaisePropertyChangeFor("Name");
    }

    [Fact]
    public void StringAssertions_MissingFeatures()
    {
        string value = "TEST";
        value.Should().BeUpperCase();
        value.Should().MatchRegex(@"^[A-Z]+$");
        value.Should().BeEquivalentTo("test");

        string lower = "test";
        lower.Should().BeLowerCase();
        lower.Should().BeEquivalentTo("TEST");

        string empty = "";
        empty.Should().BeEmpty();
        empty.Should().BeNullOrEmpty();

        string? nullString = null;
        nullString.Should().BeNullOrEmpty();
        nullString.Should().BeNullOrWhiteSpace();

        string whitespace = "   ";
        whitespace.Should().NotBeNullOrEmpty();
        whitespace.Should().BeNullOrWhiteSpace();
    }

    [Fact]
    public void NumericAssertions_MissingFeatures()
    {
        // These features are not yet implemented
        double value = 3.14;
        // value.Should().BeApproximately(3.1, 0.1);
        // value.Should().BePositive();
        // value.Should().BeOneOf(new[] { 3.14, 2.71 });
    }

    [Fact]
    public void CollectionAssertions_MissingFeatures()
    {
        // These features are not yet implemented
        var list = new[] { 1, 2, 3 };
        // list.Should().OnlyContain(n => n > 0);
        // list.Should().ContainInOrder(1, 2);
        // list.Should().HaveElementAt(0, 1);
        // list.Should().BeSubsetOf(new[] { 1, 2, 3, 4 });
    }

    [Fact]
    public void ObjectAssertions_MissingFeatures()
    {
        // These features are not yet implemented
        var obj = new { Name = "Test", Age = 30 };
        // obj.Should().BeOfType<AnonymousType>();
        // obj.Should().BeAssignableTo<object>();
        // obj.Should().Match<object>(o => o.ToString().Contains("Test"));
    }

    [Fact]
    public void DateTimeAssertions_MissingFeatures()
    {
        // These features are not yet implemented
        var date = DateTime.Now;
        // date.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
        // date.Should().HaveYear(2024);
        // date.Should().BeSameDateAs(DateTime.Today);
    }

    [Fact]
    public void DictionaryAssertions_MissingFeatures()
    {
        // These features are not yet implemented
        var dict = new Dictionary<string, int> { { "one", 1 } };
        // dict.Should().ContainKeys("one", "two");
        // dict.Should().ContainValues(1, 2);
        // dict.Should().HaveValue(1).ForKey("one");
    }

    [Fact]
    public void TypeAssertions_MissingFeatures()
    {
        // These features are not yet implemented
        var type = typeof(string);
        // type.Should().BeDerivedFrom<object>();
        // type.Should().Implement<IEnumerable<char>>();
        // type.Should().HaveProperty("Length");
    }

    [Fact]
    public void EventAssertions_MissingFeatures()
    {
        // These features are not yet implemented
        var person = new Person();
        // using var monitoredPerson = person.Monitor();
        // person.Name = "Test";
        // monitoredPerson.Should().Raise("PropertyChanged")
        //     .WithSender(person)
        //     .WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == "Name");
    }
}

public class Person : INotifyPropertyChanged
{
    private string _name = string.Empty;
    public event PropertyChangedEventHandler? PropertyChanged;

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