# API Reference Documentation

## Overview

This API reference provides detailed documentation for all assertion types available in the FluentAssertions2Shouldly library. The documentation is organized into core assertions and specialized assertions.

## Contents

### [Core Assertions](CoreAssertions.md)
- String assertions
- Numeric assertions
- Boolean assertions
- Collection assertions
- Exception assertions
- DateTime assertions
- Dictionary assertions

### [Specialized Assertions](SpecializedAssertions.md)
- File assertions
- Async/Task assertions
- Approximate equality assertions
- Enum assertions
- Property change assertions
- Custom assertions

## Quick Reference

### Most Common Assertions

```csharp
// String assertions
string text = "Hello";
text.Should().Be("Hello");                    // Exact match
text.Should().StartWith("He");                // Prefix match
text.Should().Contain("ell");                 // Contains

// Numeric assertions
int number = 42;
number.Should().Be(42);                       // Exact match
number.Should().BeGreaterThan(40);            // Comparison
number.Should().BeInRange(40, 45);            // Range check

// Collection assertions
var list = new[] { 1, 2, 3 };
list.Should().HaveCount(3);                   // Count check
list.Should().Contain(2);                     // Contains element
list.Should().BeInAscendingOrder();           // Ordering

// Exception assertions
Action action = () => throw new Exception();
action.Should().Throw<Exception>();           // Throws exception
```

### Extension Points

The library provides several extension points for custom assertions:

```csharp
// Custom string assertion
public static class CustomStringAssertions
{
    public static void BeValidEmail(
        this StringAssertions assertions)
    {
        assertions.Match(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}

// Custom object assertion
public static class CustomObjectAssertions
{
    public static void BeValid<T>(
        this ObjectAssertions assertions,
        Func<T, bool> predicate)
    {
        assertions.Subject.Should().BeOfType<T>();
        predicate((T)assertions.Subject)
            .Should().BeTrue();
    }
}
```

## Usage Guidelines

### Best Practices

1. **Use the Most Specific Assertion**
   ```csharp
   // Good
   number.Should().BeGreaterThan(0);
   
   // Less Good
   (number > 0).Should().BeTrue();
   ```

2. **Chain Assertions When Appropriate**
   ```csharp
   text.Should()
       .StartWith("Hello")
       .And.EndWith("World")
       .And.HaveLength(10);
   ```

3. **Include Meaningful Messages**
   ```csharp
   value.Should().Be(expected,
       "because {0} is required for {1}",
       expected, reason);
   ```

### Common Pitfalls

1. **Floating Point Comparisons**
   ```csharp
   // Wrong
   0.1 + 0.2.Should().Be(0.3);
   
   // Right
   0.1 + 0.2.Should().BeApproximately(0.3, 1e-10);
   ```

2. **Collection Ordering**
   ```csharp
   // Checks order
   list.Should().Equal(new[] { 1, 2, 3 });
   
   // Ignores order
   list.Should().BeEquivalentTo(new[] { 3, 1, 2 });
   ```

3. **Null Checks**
   ```csharp
   // Wrong - might throw NullReferenceException
   nullableString.Length.Should().Be(0);
   
   // Right
   nullableString.Should().BeNull();
   ```

## Performance Considerations

1. **Collection Assertions**
   - Use `HaveCount()` instead of `Count().Should().Be()`
   - Prefer `BeEquivalentTo()` over manual element comparisons

2. **String Assertions**
   - Use `Be()` with `StringComparer` for case-insensitive comparisons
   - Avoid repeated string operations in assertions

3. **Async Assertions**
   - Use appropriate timeouts for async operations
   - Consider using `CompleteWithin()` for time-sensitive tests

## Migration Notes

When migrating from FluentAssertions to Shouldly:

1. **Direct Equivalents**
   ```csharp
   // FluentAssertions
   value.Should().Be(expected);
   
   // Shouldly
   value.ShouldBe(expected);
   ```

2. **Different Patterns**
   ```csharp
   // FluentAssertions
   action.Should().Throw<Exception>();
   
   // Shouldly
   Should.Throw<Exception>(() => action());
   ```

For more details on migration, see the [Migration Guide](../Migration.md). 