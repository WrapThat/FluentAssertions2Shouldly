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
```

## Type Assertions

### Basic Type Assertions
```csharp
Type type = typeof(string);

// Type equality
type.Should().Be(typeof(string));             // Exact type match
type.Should().NotBe(typeof(int));             // Not equal type

// Inheritance
type.Should().BeDerivedFrom<object>();        // Derived from type
type.Should().NotBeDerivedFrom<Exception>();  // Not derived from type

// Interface implementation
type.Should().Implement<IEnumerable<char>>(); // Implements interface
type.Should().NotImplement<IDisposable>();    // Does not implement
```

### Member Assertions
```csharp
// Properties
type.Should().HaveProperty("Length");         // Has property
type.Should().NotHaveProperty("Invalid");     // No such property

// Methods
type.Should().HaveMethod("Substring");        // Has method
type.Should().NotHaveMethod("Invalid");       // No such method

// Constructors
type.Should().HaveConstructor();              // Has constructor
type.Should().NotHaveConstructor();           // No constructor
```

## Numeric Edge Cases

### Integer Edge Cases
```csharp
// Boundaries
int.MaxValue.Should().BePositive();           // Maximum value
int.MinValue.Should().BeNegative();           // Minimum value
0.Should().Be(0);                             // Zero value

// Comparisons
int.MaxValue.Should().BeGreaterThan(0);       // Positive check
int.MinValue.Should().BeLessThan(0);          // Negative check
```

### Floating Point Edge Cases
```csharp
// Special values
double.PositiveInfinity.Should()
    .BeGreaterThan(0);                        // Positive infinity
double.NegativeInfinity.Should()
    .BeLessThan(0);                           // Negative infinity
double.NaN.Should().NotBe(0);                 // Not a number

// Precision
float f1 = 0.1f + 0.2f;                      // Floating point math
f1.Should().BeApproximately(0.3f, 0.000001f); // Within tolerance

// Decimal precision
decimal d1 = 0.1M + 0.2M;                    // Decimal math
d1.Should().Be(0.3M);                        // Exact comparison
```

## Property Change Assertions

### Basic Property Changes
```csharp
var person = new Person();
var monitor = person.MonitorPropertyChanges();

// Single change
person.Name = "John";
monitor.RaisePropertyChangeFor("Name");        // Property changed

// No change
monitor.NotRaisePropertyChangeFor("Age");      // Property unchanged
```

### Advanced Property Changes
```csharp
// Multiple changes
person.Name = "John";
monitor.RaisePropertyChangeFor("Name");
person.Name = "Jane";
monitor.RaisePropertyChangeFor("Name");

// Async changes
var task = Task.Run(() => {
    Thread.Sleep(100);
    person.Name = "Alice";
});
monitor.RaisePropertyChangeFor("Name", 
    () => task.Wait());                       // Wait for change
```

## Task Assertions

### Basic Task Assertions
```csharp
// Completion
var task = Task.Delay(100);
await task.Should()
    .CompleteWithinAsync(TimeSpan.FromSeconds(1));

// Task with result
var resultTask = Task.FromResult(42);
await resultTask;
resultTask.Result.Should().Be(42);
```

### Exception Handling
```csharp
// Cancellation
using var cts = new CancellationTokenSource();
var cancelTask = Task.Delay(1000, cts.Token);
cts.Cancel();
await Assert.ThrowsAsync<TaskCanceledException>(
    () => cancelTask);

// Task exceptions
var failingTask = Task.FromException<int>(
    new InvalidOperationException("error"));
var ex = await Assert.ThrowsAsync<InvalidOperationException>(
    () => failingTask);
ex.Message.Should().Be("error");

// Timeouts
var longTask = Task.Delay(5000);
await Assert.ThrowsAsync<TimeoutException>(
    async () => await Task.WhenAny(
        longTask, Task.Delay(1000)));
```

## Error Handling

### Custom Error Messages
```csharp
// Basic assertions with messages
value.Should().Be(expected, 
    "because {0} should equal {1}", value, expected);

// Complex assertions with formatting
collection.Should().HaveCount(3, 
    "because we expect exactly {0} items", 3);
```

### Common Error Patterns
```csharp
// Null checks
object obj = null;
obj.Should().BeNull("because no value was set");

// Empty collections
var list = new List<int>();
list.Should().BeEmpty("because no items were added");

// Type checks
var value = "test";
value.Should().BeOfType<string>(
    "because we expect a string value");