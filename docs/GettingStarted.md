# Getting Started with FluentAssertions2Shouldly

## Installation

1. Install both required packages:
```bash
dotnet add package FluentAssertions2Shouldly
dotnet add package Shouldly
```

2. Add the necessary using statements to your test files:
```csharp
using FluentAssertions;  // Original FluentAssertions syntax
using FluentAssertions2Shouldly;  // Our conversion layer
```

## Basic Usage

### String Assertions
```csharp
// Basic equality
"test".Should().Be("test");
"Hello World".Should().StartWith("Hello");
"Hello World".Should().EndWith("World");
"Hello World".Should().Contain("lo Wo");
"Hello World".Should().HaveLength(11);
"Hello World".Should().NotBeNullOrWhiteSpace();

// Case sensitivity
"TEST".Should().Be("TEST");
"test".Should().NotBe("TEST");
```

### Numeric Assertions
```csharp
// Integer assertions
42.Should().Be(42);
42.Should().BeGreaterThan(41);
42.Should().BeLessThan(43);
42.Should().BeInRange(40, 45);

// Double/Float assertions with precision
3.14159f.Should().BeApproximately(3.14f, 0.01f);
3.14159265359.Should().BeApproximately(3.14159, 0.00001);
```

### Boolean Assertions
```csharp
// Direct boolean values
true.Should().BeTrue();
false.Should().BeFalse();

// Boolean expressions
(1 == 1).Should().BeTrue();
(1 != 1).Should().BeFalse();
```

### Collection Assertions
```csharp
var list = new List<int> { 1, 2, 3 };
list.Should().HaveCount(3);
list.Should().Contain(2);
list.Should().BeInAscendingOrder();
list.Should().NotBeEmpty();

// Empty collections
var empty = new List<int>();
empty.Should().BeEmpty();
empty.Should().HaveCount(0);
```

### Exception Assertions
```csharp
// Basic exceptions
Action action = () => throw new InvalidOperationException("test");
action.Should().Throw<InvalidOperationException>()
      .WithMessage("test");

// Derived exceptions
Action derived = () => throw new ArgumentNullException("param", "message");
derived.Should().Throw<ArgumentException>();
```

### DateTime Assertions
```csharp
var now = DateTime.Now;
var past = now.AddDays(-1);
var future = now.AddDays(1);

now.Should().BeAfter(past);
now.Should().BeBefore(future);
```

## Best Practices

1. **Consistent Style**: Stick to either FluentAssertions or Shouldly style within a single test file for readability.

2. **Meaningful Assertions**: Use the most specific assertion available:
   ```csharp
   // Good
   number.Should().BeGreaterThan(0);
   
   // Less Good
   (number > 0).Should().BeTrue();
   ```

3. **Clear Error Messages**: Both styles provide clear error messages, but you can enhance them:
   ```csharp
   // FluentAssertions style
   action.Should().Throw<InvalidOperationException>("because the input is invalid");
   ```

4. **Chaining Assertions**: Use chaining when it makes sense:
   ```csharp
   string text = "Hello World";
   text.Should()
       .StartWith("Hello")
       .And.EndWith("World")
       .And.HaveLength(11);
   ```

## Common Pitfalls

1. **Null Reference Handling**:
   ```csharp
   // This will throw NullReferenceException
   string nullString = null;
   nullString.Length.Should().Be(0);  // Wrong!

   // This is correct
   nullString.Should().BeNull();  // Right!
   ```

2. **Collection Ordering**:
   ```csharp
   var list = new[] { 1, 2, 3 };
   
   // These are different!
   list.Should().BeInAscendingOrder();  // Checks if sorted
   list.Should().Equal(new[] { 1, 2, 3 });  // Checks exact sequence
   ```

3. **Floating Point Comparisons**:
   ```csharp
   // Don't use exact equality
   0.1 + 0.2.Should().Be(0.3);  // Might fail!

   // Use approximate equality
   0.1 + 0.2.Should().BeApproximately(0.3, 1e-10);
   ``` 