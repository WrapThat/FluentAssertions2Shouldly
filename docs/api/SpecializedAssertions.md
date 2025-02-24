# Specialized Assertions API Reference

## File Assertions

### Basic File Operations
```csharp
var file = new FileInfo("test.txt");

// Existence
file.Should().Exist();                        // File exists
file.Should().NotExist();                     // File doesn't exist

// Content
File.WriteAllText(file.FullName, "content");
File.ReadAllText(file.FullName)
    .Should().Be("content");                  // Exact content match
```

### Advanced File Operations
```csharp
// File Properties
file.Should().HaveExtension(".txt");          // File extension
file.Should().HaveLength(100);                // File size
file.Should().BeWritable();                   // File permissions

// Directory Operations
var dir = new DirectoryInfo("path");
dir.Should().Exist();                         // Directory exists
dir.Should().HaveFiles("*.txt");              // Contains files
```

## Async/Task Assertions

### Basic Task Assertions
```csharp
// Completion
Func<Task> action = async () => 
    await Task.Delay(100);
await action.Should().CompleteWithinAsync(
    TimeSpan.FromSeconds(1));                 // Completes in time

// Exceptions
Func<Task> failing = () => 
    Task.FromException(new InvalidOperationException());
await failing.Should()
    .ThrowAsync<InvalidOperationException>(); // Throws async
```

### Advanced Task Assertions
```csharp
// Task Results
Func<Task<int>> task = async () => {
    await Task.Delay(100);
    return 42;
};
var result = await task.Should()
    .CompleteWithinAsync(TimeSpan.FromSeconds(1))
    .WithResult(42);                          // Result value

// Cancellation
using var cts = new CancellationTokenSource();
Func<Task> cancelling = async () => {
    await Task.Delay(1000, cts.Token);
};
cts.Cancel();
await cancelling.Should()
    .ThrowAsync<TaskCanceledException>();     // Cancellation
```

## Approximate Equality Assertions

### Numeric Approximation
```csharp
// Float Comparisons
float f1 = 3.14159f;
f1.Should().BeApproximately(3.14f, 0.01f);   // Within tolerance

// Double Comparisons
double d1 = 3.14159265359;
d1.Should().BeApproximately(3.14159, 0.00001); // Precise tolerance

// Calculated Values
var calculated = Math.Sin(Math.PI);
calculated.Should()
    .BeApproximately(0.0, 1e-10);            // Near zero
```

### DateTime Approximation
```csharp
var time = DateTime.Now;

// Time Ranges
time.Should().BeCloseTo(DateTime.Now, 1000);  // Within milliseconds
time.Should().BeWithin(TimeSpan.FromSeconds(1))
    .Before(DateTime.Now);                    // Within timespan
```

## Enum Assertions

### Basic Enum Assertions
```csharp
[Flags]
public enum Permissions
{
    None = 0,
    Read = 1,
    Write = 2,
    Execute = 4,
    All = Read | Write | Execute
}

// Single Value
var perm = Permissions.Read;
perm.Should().Be(Permissions.Read);           // Exact match
perm.Should().NotBe(Permissions.Write);       // Different value

// Flag Checks
perm.Should().HaveFlag(Permissions.Read);     // Has flag
perm.Should().NotHaveFlag(Permissions.Write); // Doesn't have flag
```

### Combined Flag Assertions
```csharp
var combined = Permissions.Read | Permissions.Write;

// Multiple Flags
combined.Should().HaveFlag(Permissions.Read)
        .And.HaveFlag(Permissions.Write);     // Has both flags

// Complex Checks
combined.Should().NotBe(Permissions.All);     // Not all flags
(combined & Permissions.Execute).Should()
    .Be(Permissions.None);                    // No execute flag
```

## Property Change Assertions

### Basic Property Changes
```csharp
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
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(nameof(Name)));
        }
    }
}

// Change Notification
var person = new Person();
person.Should()
    .RaisePropertyChangeFor<string>("Name");  // Property changed

// Multiple Properties
person.Should()
    .RaisePropertyChangeFor<string>("Name")
    .And.RaisePropertyChangeFor<int>("Age");  // Multiple changes
```

### Advanced Property Changes
```csharp
// Change Monitoring
using var monitor = person.Monitor();
person.Name = "John";
monitor.Should().RaisePropertyChangeFor(x => x.Name)
        .WithValue("John");                   // Value check

// Timing
person.Invoking(p => p.Name = "Jane")
      .Should().RaisePropertyChange()
      .Within(TimeSpan.FromMilliseconds(100)); // Time constraint
```

## Custom Assertions

### Creating Custom Assertions
```csharp
public static class CustomAssertions
{
    public static void BeValidEmailAddress(
        this StringAssertions assertions)
    {
        assertions.Match(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            "be a valid email address");
    }

    public static void HaveValidDomain(
        this StringAssertions assertions,
        string domain)
    {
        assertions.EndWith($"@{domain}",
            $"have domain {domain}");
    }
}

// Usage
"test@example.com".Should().BeValidEmailAddress();
"user@company.com".Should().HaveValidDomain("company.com");
```

### Complex Custom Assertions
```csharp
public static class ObjectAssertions
{
    public static void BeValidEntity<T>(
        this ObjectAssertions assertions,
        Action<T> validation) where T : class
    {
        assertions.Subject.Should().NotBeNull()
            .And.BeOfType<T>();
        validation((T)assertions.Subject);
    }
}

// Usage
person.Should().BeValidEntity<Person>(p => {
    p.Name.Should().NotBeNullOrEmpty();
    p.Age.Should().BeGreaterThan(0);
}); 