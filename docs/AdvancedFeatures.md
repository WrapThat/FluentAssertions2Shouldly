# Advanced Features

## Specialized Assertions

### File Assertions
```csharp
// Check if file exists
var file = new FileInfo("test.txt");
file.Should().Exist();

// Check file contents
File.WriteAllText("test.txt", "Hello World");
File.ReadAllText("test.txt").Should().Be("Hello World");
```

### Async/Task Assertions
```csharp
// Completion timing
Func<Task> action = async () => await Task.Delay(100);
await action.Should().CompleteWithinAsync(TimeSpan.FromSeconds(1));

// Async exceptions
Func<Task> failing = () => Task.FromException(new InvalidOperationException());
await failing.Should().ThrowAsync<InvalidOperationException>();

// Cancellation
using var cts = new CancellationTokenSource();
Func<Task> cancelling = async () => 
{
    await Task.Delay(1000, cts.Token);
};
cts.Cancel();
await cancelling.Should().ThrowAsync<TaskCanceledException>();
```

### Approximate Equality
```csharp
// Float comparisons
float f1 = 3.14159f;
f1.Should().BeApproximately(3.14f, 0.01f);

// Double comparisons
double d1 = 3.14159265359;
d1.Should().BeApproximately(3.14159, 0.00001);

// Comparing calculated values
var calculated = Math.Sin(Math.PI);
calculated.Should().BeApproximately(0.0, 1e-10);
```

### Enum Assertions
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

// Single flag checks
var perm = Permissions.Read;
perm.Should().HaveFlag(Permissions.Read);

// Combined flags
var combined = Permissions.Read | Permissions.Write;
combined.Should().HaveFlag(Permissions.Read);
combined.Should().HaveFlag(Permissions.Write);
```

### Property Change Notifications
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
        }
    }
}

// Test property change notifications
var person = new Person();
person.Should().RaisePropertyChangeFor<string>("Name");
```

## Advanced Scenarios

### Nested Collections
```csharp
var nested = new List<List<int>>
{
    new List<int> { 1, 2 },
    new List<int> { 3, 4 }
};

nested.Should().HaveCount(2);
nested[0].Should().HaveCount(2);
nested.Should().OnlyContain(list => list.Count == 2);
```

### Complex Object Graphs
```csharp
var complex = new Dictionary<string, List<int>>
{
    { "odds", new List<int> { 1, 3, 5 } },
    { "evens", new List<int> { 2, 4, 6 } }
};

complex.Should().ContainKey("odds")
       .And.ContainKey("evens");
complex["odds"].Should().OnlyContain(n => n % 2 == 1);
complex["evens"].Should().OnlyContain(n => n % 2 == 0);
```

### Circular References
```csharp
public class Node
{
    public int Value { get; set; }
    public Node Next { get; set; }
}

var node1 = new Node { Value = 1 };
var node2 = new Node { Value = 2 };
node1.Next = node2;
node2.Next = node1;

node1.Should().NotBeNull();
node1.Next.Should().NotBeNull();
node1.Next.Next.Should().Be(node1);
```

### Custom Assertions
```csharp
public static class CustomAssertions
{
    public static void BeValidEmailAddress(this StringAssertions assertions)
    {
        assertions.Match(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", "be a valid email address");
    }
}

// Usage
"test@example.com".Should().BeValidEmailAddress();
```

## Performance Considerations

### Async Timeouts
```csharp
// Avoid too short timeouts
await Task.Delay(100).Should().CompleteWithinAsync(TimeSpan.FromMilliseconds(50)); // Might fail

// Use reasonable timeouts
await Task.Delay(100).Should().CompleteWithinAsync(TimeSpan.FromSeconds(1)); // Better
```

### Collection Assertions
```csharp
// Avoid multiple enumerations
var enumerable = Enumerable.Range(1, 1000000);
enumerable.Should().HaveCount(1000000);  // Enumerates once
enumerable.Count().Should().Be(1000000); // Enumerates twice!
```

### String Comparisons
```csharp
// Case sensitivity matters
"test".Should().Be("TEST", StringComparer.OrdinalIgnoreCase); // Use explicit comparison
"test".Should().BeEquivalentTo("TEST"); // Alternative approach
``` 