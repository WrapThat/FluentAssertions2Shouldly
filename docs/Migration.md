# Migration Guide

This guide helps you migrate from FluentAssertions to Shouldly using FluentAssertions2Shouldly as a bridge.

## Step-by-Step Migration

### 1. Update Packages

Remove FluentAssertions and add FluentAssertions2Shouldly:

```bash
dotnet remove package FluentAssertions
dotnet add package FluentAssertions2Shouldly
```

#### Using Central Package Management

For larger solutions, it's recommended to use Central Package Management to ensure consistent package versions across all projects:

1. Create or update `Directory.Packages.props` in your solution root:
```xml
<Project>
  <PropertyGroup>
    <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
  </PropertyGroup>
  <ItemGroup>
    <PackageVersion Include="FluentAssertions2Shouldly" Version="1.0.0" />
    <PackageVersion Include="Shouldly" Version="4.3.0" />
    <PackageVersion Include="xunit" Version="2.9.3" />
  </ItemGroup>
</Project>
```

2. Update your project files to use centrally managed versions:
```xml
<ItemGroup>
  <PackageReference Include="FluentAssertions2Shouldly" />
  <PackageReference Include="Shouldly" />
  <PackageReference Include="xunit" />
</ItemGroup>
```

This approach makes it easier to:
- Maintain consistent package versions across projects
- Update packages in one place
- Avoid version conflicts
- Track dependencies more effectively

### 2. Update Using Statements

Replace:
```csharp
using FluentAssertions;
```

With:
```csharp
using FluentAssertions2Shouldly;
```

#### Using Global Using Statements

For test projects, consider using global using statements to reduce repetition and make the migration easier:

1. Create a `GlobalUsings.cs` file in your test project:
```csharp
// Core testing frameworks
global using Xunit;
global using FluentAssertions2Shouldly;

// Required for async tests and Task-based assertions
global using System.Threading.Tasks;

// Required for property change notification tests
global using System.ComponentModel;
```

2. Or add them to your test project file:
```xml
<ItemGroup>
  <!-- Core testing frameworks -->
  <Using Include="Xunit" />
  <Using Include="FluentAssertions2Shouldly" />
  
  <!-- Required for async tests -->
  <Using Include="System.Threading.Tasks" />
  
  <!-- Required for property change tests -->
  <Using Include="System.ComponentModel" />
</ItemGroup>
```

Note: You only need to include these namespaces if you're using the corresponding features:
- Include `System.Threading.Tasks` if you have async tests or use task-based assertions
- Include `System.ComponentModel` if you test property change notifications
- Additional namespaces might be needed based on your specific test scenarios

### 3. Verify Tests Still Work

Your existing tests should continue to work without modification. The library provides the same syntax as FluentAssertions while using Shouldly under the hood.

#### Running Tests

1. Run your test suite:
```bash
dotnet test
```

2. Watch for any failures that might indicate:
   - Missing assertion types
   - Incompatible assertion chains
   - Different behavior between FluentAssertions and Shouldly

#### Common Issues and Solutions

1. **Property Change Notifications**
   ```csharp
   // If this fails:
   person.Should().RaisePropertyChangeFor(x => x.Name);
   
   // Try this equivalent:
   person.MonitorPropertyChanges().RaisePropertyChangeFor("Name");
   ```

2. **Collection Ordering**
   ```csharp
   // If this fails:
   list.Should().BeInAscendingOrder();
   
   // Verify the exact behavior you need:
   list.Should().BeInAscendingOrder();        // Strict ascending
   list.Should().ContainInOrder(1, 2, 3);     // Exact sequence
   ```

3. **String Comparisons**
   ```csharp
   // If case-sensitivity is an issue:
   text.Should().Be("TEST");                  // Case-sensitive
   text.Should().BeEquivalentTo("test");      // Case-insensitive
   ```

4. **Async Assertions**
   ```csharp
   // If timing-sensitive tests fail:
   await task.Should().CompleteWithinAsync(TimeSpan.FromSeconds(1));
   
   // Consider adjusting timeouts or using more specific assertions:
   await task;  // Wait for completion
   await task.Should().NotThrowAsync();  // Verify no exceptions
   ```

#### Troubleshooting Steps

1. **Identify Pattern**
   - Are failures consistent across similar assertions?
   - Do they occur only with certain types (strings, collections, etc.)?
   - Are they related to async operations?

2. **Check Documentation**
   - Review the [API Reference](api/README.md) for correct usage
   - Verify you're using the most appropriate assertion
   - Look for alternative assertion patterns

3. **Examine Test Context**
   - Verify test setup is correct
   - Check for environmental dependencies
   - Consider timing and async issues

4. **Common Solutions**
   - Add more specific assertions
   - Break down complex assertion chains
   - Adjust timeouts for async operations
   - Use appropriate comparison methods

If you encounter persistent issues:
1. Check the [GitHub issues](https://github.com/yourusername/FluentAssertions2Shouldly/issues)
2. Consider reporting a new issue with:
   - Minimal reproduction code
   - Expected vs actual behavior
   - Test environment details

### 4. Optional: Gradual Migration to Native Shouldly

If you want to eventually use native Shouldly syntax, you can migrate tests gradually:

```csharp
// Original FluentAssertions syntax (still works with FluentAssertions2Shouldly)
string text = "Hello";
text.Should().Be("Hello");
text.Should().StartWith("He");

// Native Shouldly syntax
text.ShouldBe("Hello");
text.ShouldStartWith("He");
```

## Common Migration Patterns

### String Assertions

```csharp
// FluentAssertions style
text.Should().Be("expected");
text.Should().StartWith("ex");
text.Should().EndWith("ed");
text.Should().Contain("pec");
text.Should().HaveLength(8);

// Shouldly equivalent
text.ShouldBe("expected");
text.ShouldStartWith("ex");
text.ShouldEndWith("ed");
text.ShouldContain("pec");
text.Length.ShouldBe(8);
```

### Numeric Assertions

```csharp
// FluentAssertions style
number.Should().Be(42);
number.Should().BeGreaterThan(40);
number.Should().BeLessThan(50);
number.Should().BeInRange(40, 45);

// Shouldly equivalent
number.ShouldBe(42);
number.ShouldBeGreaterThan(40);
number.ShouldBeLessThan(50);
number.ShouldBeInRange(40, 45);
```

### Collection Assertions

The library provides rich support for collection assertions:

1. **Basic Collection Operations**
   ```csharp
   var list = new List<int> { 1, 2, 3, 4, 5 };
   
   // Count and emptiness
   list.Should().HaveCount(5);
   list.Should().NotBeEmpty();
   
   // Content checks
   list.Should().Contain(3);
   list.Should().NotContain(6);
   
   // Multiple elements
   list.Should().ContainInOrder(1, 2, 3);
   list.Should().BeSubsetOf(new[] { 1, 2, 3, 4, 5, 6 });
   ```

2. **Ordering and Position**
   ```csharp
   // Order checks
   list.Should().BeInAscendingOrder();
   list.Should().ContainInOrder(1, 2, 3);
   
   // Position checks
   list.Should().HaveElementAt(0, 1);
   list.Should().HaveElementAt(4, 5);
   list.Should().StartWith(1);
   list.Should().EndWith(5);
   ```

3. **Set Operations**
   ```csharp
   // Intersection
   list.Should().IntersectWith(new[] { 3, 4, 5, 6 });
   list.Should().NotIntersectWith(new[] { 6, 7, 8 });
   
   // Subset
   list.Should().BeSubsetOf(new[] { 1, 2, 3, 4, 5, 6 });
   
   // Element conditions
   list.Should().OnlyContainElementsThat(x => x > 0);
   ```

4. **Collection Types**
   ```csharp
   // Arrays
   int[] array = { 1, 2, 3 };
   array.Should().HaveCount(3);
   array.Should().Contain(2);
   
   // Lists
   var list = new List<int> { 1, 2, 3 };
   list.Should().HaveCount(3);
   list.Should().Contain(2);
   
   // IEnumerable
   IEnumerable<int> enumerable = Enumerable.Range(1, 3);
   enumerable.Should().HaveCount(3);
   enumerable.Should().Contain(2);
   ```

5. **Complex Collections**
   ```csharp
   // Nested collections
   var nested = new List<List<int>>
   {
       new List<int> { 1, 2 },
       new List<int> { 3, 4 }
   };
   
   nested.Should().HaveCount(2);
   nested[0].Should().HaveCount(2);
   nested[1].Should().Contain(4);
   
   // Dictionaries
   var dict = new Dictionary<string, List<int>>
   {
       { "odds", new List<int> { 1, 3, 5 } },
       { "evens", new List<int> { 2, 4, 6 } }
   };
   
   dict.Should().ContainKey("odds");
   dict["odds"].Should().HaveCount(3);
   dict["evens"].Should().BeInAscendingOrder();
   ```

6. **Edge Cases**
   ```csharp
   // Empty collections
   new List<int>().Should().BeEmpty();
   Array.Empty<string>().Should().BeEmpty();
   
   // Single element
   var single = new[] { 1 };
   single.Should().HaveCount(1);
   single.Should().ContainSingle();
   single.Should().ContainSingle(x => x == 1);
   
   // Null collections
   List<int> nullList = null;
   nullList.Should().BeNull();
   ```

7. **Performance Considerations**
   ```csharp
   // Avoid multiple enumerations
   var enumerable = Enumerable.Range(1, 1000000);
   
   // Good - enumerates once
   enumerable.Should().HaveCount(1000000);
   
   // Bad - enumerates twice
   enumerable.Count().Should().Be(1000000);
   
   // Good - stores result
   var list = enumerable.ToList();
   list.Should().HaveCount(1000000);
   list.Should().Contain(500000);
   ```

### Exception Assertions

```csharp
// FluentAssertions style
action.Should().Throw<InvalidOperationException>()
    .WithMessage("error");

// Shouldly equivalent
Should.Throw<InvalidOperationException>(() => action())
    .Message.ShouldBe("error");
```

### Async Assertions

```csharp
// FluentAssertions style
await task.Should().CompleteWithinAsync(TimeSpan.FromSeconds(1));
await asyncAction.Should().ThrowAsync<InvalidOperationException>();

// Shouldly equivalent
await Should.CompleteIn(task, TimeSpan.FromSeconds(1));
await Should.ThrowAsync<InvalidOperationException>(asyncAction);
```

### Numeric Type Assertions

The library provides specialized assertions for different numeric types:

1. **Integer Types**
   ```csharp
   // Int16 (short)
   short s = 42;
   s.Should().BeOfType(typeof(short));
   s.Should().BeOfType<short>();
   s.Should().BeAssignableTo<IComparable>();
   
   // Int32 (int)
   int i = 42;
   i.Should().BeOfType(typeof(int));
   i.Should().BeOfType<int>();
   i.Should().BeAssignableTo<IComparable>();
   
   // Int64 (long)
   long l = 42;
   l.Should().BeOfType(typeof(long));
   l.Should().BeOfType<long>();
   l.Should().BeAssignableTo<IComparable>();
   ```

2. **Floating Point Types**
   ```csharp
   // Single (float)
   float f = 42.0f;
   f.Should().BeOfType(typeof(float));
   f.Should().BeOfType<float>();
   f.Should().BeAssignableTo<IComparable>();
   
   // Double
   double d = 42.0;
   d.Should().BeOfType(typeof(double));
   d.Should().BeOfType<double>();
   d.Should().BeAssignableTo<IComparable>();
   
   // Decimal
   decimal m = 42.0m;
   m.Should().BeOfType(typeof(decimal));
   m.Should().BeOfType<decimal>();
   m.Should().BeAssignableTo<IComparable>();
   ```

3. **Common Operations**
   ```csharp
   // Basic comparisons
   number.Should().Be(42);
   number.Should().NotBe(43);
   
   // Range checks
   number.Should().BeGreaterThan(41);
   number.Should().BeLessThan(43);
   number.Should().BeInRange(40, 45);
   
   // Sign checks
   number.Should().BePositive();
   (-number).Should().BeNegative();
   
   // Set membership
   number.Should().BeOneOf(41, 42, 43);
   ```

4. **Floating Point Considerations**
   ```csharp
   // Approximate equality
   3.14159.Should().BeApproximately(3.14, 0.01);
   
   // Special values
   double.PositiveInfinity.Should().Be(double.PositiveInfinity);
   double.NegativeInfinity.Should().Be(double.NegativeInfinity);
   double.NaN.Should().Be(double.NaN);
   
   // Precision-sensitive comparisons
   0.1 + 0.2.Should().BeApproximately(0.3, 1e-10);
   ```

5. **Type Conversion Behavior**
   ```csharp
   // Implicit conversions are not considered
   int i = 42;
   i.Should().BeOfType<int>();        // Passes
   i.Should().BeOfType<long>();       // Fails
   i.Should().BeAssignableTo<long>(); // Passes
   
   // Numeric interface implementations
   i.Should().BeAssignableTo<IComparable>();
   i.Should().BeAssignableTo<IFormattable>();
   i.Should().BeAssignableTo<IConvertible>();
   ```

## Advanced Migration Scenarios

### Edge Cases

1. **Null Values**
   ```csharp
   // All these assertions work as expected
   string nullString = null;
   nullString.Should().BeNull();
   
   object nullObject = null;
   nullObject.Should().BeNull();
   
   List<int> nullList = null;
   nullList.Should().BeNull();
   ```

2. **Empty Collections**
   ```csharp
   // String emptiness
   string.Empty.Should().BeEmpty();
   "".Should().BeEmpty();
   
   // Collection emptiness
   new List<int>().Should().BeEmpty();
   Array.Empty<string>().Should().BeEmpty();
   new Dictionary<string, int>().Should().BeEmpty();
   ```

3. **Numeric Boundaries**
   ```csharp
   // Integer boundaries
   int.MaxValue.Should().Be(int.MaxValue);
   int.MinValue.Should().Be(int.MinValue);
   
   // Floating point boundaries
   double.MaxValue.Should().Be(double.MaxValue);
   double.MinValue.Should().Be(double.MinValue);
   double.PositiveInfinity.Should().Be(double.PositiveInfinity);
   double.NegativeInfinity.Should().Be(double.NegativeInfinity);
   ```

### Complex Scenarios

1. **Nested Collections**
   ```csharp
   var nested = new List<List<int>>
   {
       new List<int> { 1, 2 },
       new List<int> { 3, 4 }
   };
   
   nested.Should().HaveCount(2);
   nested.Should().NotBeEmpty();
   nested[0].Should().HaveCount(2);
   nested[1].Should().Contain(4);
   ```

2. **Complex Object Graphs**
   ```csharp
   var complex = new Dictionary<string, List<int>>
   {
       { "odds", new List<int> { 1, 3, 5 } },
       { "evens", new List<int> { 2, 4, 6 } }
   };
   
   complex.Should().ContainKey("odds");
   complex["odds"].Should().HaveCount(3);
   complex["odds"].Should().Contain(3);
   complex["evens"].Should().BeInAscendingOrder();
   ```

3. **Circular References**
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
   node1.Value.Should().Be(1);
   node1.Next.Should().NotBeNull();
   node1.Next.Value.Should().Be(2);
   ```

4. **Nested Property Changes**
```csharp
public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
}

public class Person : INotifyPropertyChanged
{
    private Address _address;
    public event PropertyChangedEventHandler PropertyChanged;

    public Address Address
    {
        get => _address;
        set
        {
            _address = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Address)));
        }
    }
}

// Monitoring nested property changes
var person = new Person();
var monitor = person.MonitorPropertyChanges();

person.Address = new Address { Street = "123 Main St" };
monitor.RaisePropertyChangeFor("Address");
```

5. **Complex Task Chains**
```csharp
// Sequential tasks
var task1 = Task.Delay(100);
var task2 = task1.ContinueWith(_ => 42);
var task3 = task2.ContinueWith(t => t.Result * 2);

await task1.Should().CompleteWithinAsync(TimeSpan.FromSeconds(1));
await task2;
task2.Result.Should().Be(42);
await task3;
task3.Result.Should().Be(84);

// Parallel tasks
var tasks = new[]
{
    Task.Delay(100),
    Task.Delay(200),
    Task.Delay(300)
};

await Task.WhenAll(tasks).Should()
    .CompleteWithinAsync(TimeSpan.FromSeconds(1));
```

6. **Custom Comparisons**
```csharp
public class CustomObject
{
    public string Id { get; set; }
    public DateTime Created { get; set; }
}

var obj1 = new CustomObject 
{ 
    Id = "1", 
    Created = DateTime.Now 
};
var obj2 = new CustomObject 
{ 
    Id = "1", 
    Created = DateTime.Now.AddMilliseconds(100) 
};

// Compare specific properties
obj1.Id.Should().Be(obj2.Id);

// Compare with tolerance
obj1.Created.Should()
    .BeCloseTo(obj2.Created, TimeSpan.FromSeconds(1));
```

7. **Collection Ordering**
```csharp
var list = new List<CustomObject>
{
    new() { Id = "3", Created = DateTime.Now },
    new() { Id = "1", Created = DateTime.Now.AddDays(-1) },
    new() { Id = "2", Created = DateTime.Now.AddDays(1) }
};

// Order by Id
list.Select(x => x.Id).Should().BeInAscendingOrder();

// Order by Created date
list.Select(x => x.Created).Should().BeInAscendingOrder();

// Custom ordering
list.Should().BeInAscendingOrder(x => x.Id);
```

8. **Exception Details**
```csharp
try
{
    throw new InvalidOperationException("Outer", 
        new ArgumentException("Inner", "param"));
}
catch (Exception ex)
{
    ex.Should().BeOfType<InvalidOperationException>()
        .Which.Message.Should().Be("Outer");
    
    ex.Should().BeOfType<InvalidOperationException>()
        .Which.InnerException.Should().BeOfType<ArgumentException>()
        .Which.Message.Should().Be("Inner (Parameter 'param')");
}
```

9. **Type Hierarchies**
```csharp
// Interface hierarchy
typeof(IList<int>).Should()
    .Implement<ICollection<int>>()
    .And.Implement<IEnumerable<int>>();

// Class hierarchy
typeof(ArgumentNullException).Should()
    .BeDerivedFrom<ArgumentException>()
    .And.BeDerivedFrom<Exception>();

// Generic constraints
typeof(List<>).Should()
    .BeAssignableTo(typeof(IList<>));
```

10. **File System Operations**
```csharp
var file = new FileInfo("test.txt");
File.WriteAllText(file.FullName, "content");

try
{
    // Basic checks
    file.Should().Exist()
        .And.HaveExtension(".txt")
        .And.HaveLength(7);

    // File attributes
    file.Should().BeWritable();
    file.Should().NotBeReadOnly();

    // Access times
    file.Should().HaveAccessTimes(
        creation: DateTime.Today,
        lastWrite: DateTime.Today,
        lastAccess: DateTime.Today);
}
finally
{
    if (file.Exists)
    {
        File.Delete(file.FullName);
    }
}
```

11. **Async Event Handling**
```csharp
public class AsyncEventSource
{
    public event EventHandler Changed;
    
    public async Task RaiseEventAsync()
    {
        await Task.Delay(100);
        Changed?.Invoke(this, EventArgs.Empty);
    }
}

var source = new AsyncEventSource();
var eventRaised = false;
source.Changed += (s, e) => eventRaised = true;

await source.RaiseEventAsync();
eventRaised.Should().BeTrue();
```

## Best Practices During Migration

1. **Migrate One Project at a Time**
   - Start with smaller, less critical projects
   - Ensure all tests pass after each step
   - Consider using feature flags for gradual rollout

2. **Update Test Patterns**
   - Use more specific assertions when possible
   - Take advantage of Shouldly's improved error messages
   - Consider refactoring complex assertion chains

3. **Review and Clean Up**
   - Remove unnecessary assertion chains
   - Simplify complex assertions
   - Update documentation and comments

4. **Monitor Performance**
   - Watch for any performance changes
   - Use appropriate assertion methods for collections
   - Consider parallel test execution impact

## Troubleshooting

### Common Issues

1. **Ambiguous Method Calls**
   - Ensure you're not mixing FluentAssertions and FluentAssertions2Shouldly
   - Remove old FluentAssertions using statements

2. **Missing Methods**
   - Check the API documentation for equivalent methods
   - Consider using alternative assertion approaches
   - Create custom extensions if needed

3. **Different Behavior**
   - Review Shouldly's documentation for specific behavior differences
   - Update tests to account for different null handling
   - Adjust collection comparison logic if needed

### Getting Help

- Check the [GitHub issues](https://github.com/yourusername/FluentAssertions2Shouldly/issues)
- Join the discussion in the [Shouldly community](https://github.com/shouldly/shouldly/discussions)
- Submit bug reports with minimal reproduction code 