# Core Assertions API Reference

## String Assertions

### Basic String Assertions
```csharp
string value = "test";

// Equality
value.Should().Be("test");                    // Exact match
value.Should().NotBe("other");                // Not equal
value.Should().BeEquivalentTo("TEST");        // Case-insensitive match

// Containment
value.Should().Contain("es");                 // Contains substring
value.Should().StartWith("te");               // Starts with
value.Should().EndWith("st");                 // Ends with

// Length
value.Should().HaveLength(4);                 // Exact length
value.Should().NotBeEmpty();                  // Not empty
value.Should().NotBeNullOrWhiteSpace();       // Not null or whitespace
```

### Advanced String Assertions
```csharp
// Pattern Matching
value.Should().Match("t[e-z]st");            // Regex match
value.Should().MatchEquivalentOf("T[E-Z]ST"); // Case-insensitive regex

// Multiple Conditions
value.Should()
     .StartWith("t")
     .And.EndWith("st")
     .And.HaveLength(4);

// Culture-Specific
value.Should().Be("TEST", StringComparer.OrdinalIgnoreCase);
```

## Numeric Assertions

### Integer Assertions
```csharp
int number = 42;

// Basic Comparisons
number.Should().Be(42);                       // Exact equality
number.Should().NotBe(0);                     // Inequality
number.Should().BePositive();                 // Greater than zero
number.Should().BeNegative();                 // Less than zero

// Range Checks
number.Should().BeGreaterThan(41);            // >
number.Should().BeGreaterThanOrEqualTo(42);   // >=
number.Should().BeLessThan(43);               // <
number.Should().BeLessThanOrEqualTo(42);      // <=
number.Should().BeInRange(40, 45);            // Within range
```

### Floating Point Assertions
```csharp
double value = 3.14159;

// Approximate Equality
value.Should().BeApproximately(3.14, 0.01);   // Within precision
value.Should().NotBeApproximately(3.0, 0.01); // Outside precision

// Special Values
double.PositiveInfinity.Should().Be(double.PositiveInfinity);
double.NegativeInfinity.Should().Be(double.NegativeInfinity);
double.NaN.Should().Be(double.NaN);
```

## Boolean Assertions
```csharp
bool value = true;

// Basic Assertions
value.Should().BeTrue();                      // Is true
value.Should().NotBeFalse();                  // Not false
value.Should().Be(true);                      // Equality

// Conditional Assertions
(1 > 0).Should().BeTrue();                   // Expression result
bool? nullableValue = null;
nullableValue.Should().BeNull();              // Nullable boolean
```

## Collection Assertions

### Basic Collection Assertions
```csharp
var list = new[] { 1, 2, 3 };

// Count
list.Should().HaveCount(3);                   // Exact count
list.Should().NotBeEmpty();                   // Not empty
list.Should().ContainSingle();                // Single element

// Content
list.Should().Contain(2);                     // Contains element
list.Should().NotContain(4);                  // Doesn't contain
list.Should().ContainInOrder(1, 2, 3);        // Exact sequence
```

### Advanced Collection Assertions
```csharp
// Ordering
list.Should().BeInAscendingOrder();           // Sorted ascending
list.Should().BeInDescendingOrder();          // Sorted descending

// Multiple Conditions
list.Should()
    .HaveCount(3)
    .And.Contain(2)
    .And.BeInAscendingOrder();

// Predicate Matching
list.Should().OnlyContain(n => n > 0);        // All match condition
list.Should().Contain(n => n % 2 == 0);       // Any matches condition
```

## Exception Assertions
```csharp
// Basic Exception
Action action = () => throw new InvalidOperationException("error");
action.Should().Throw<InvalidOperationException>()
      .WithMessage("error");

// Derived Exceptions
action.Should().Throw<Exception>()
      .Which.Message.Should().Contain("error");

// Async Exceptions
Func<Task> asyncAction = async () => 
    throw new InvalidOperationException();
await asyncAction.Should().ThrowAsync<InvalidOperationException>();
```

## DateTime Assertions
```csharp
var date = DateTime.Now;

// Basic Comparisons
date.Should().BeAfter(DateTime.MinValue);     // Later than
date.Should().BeBefore(DateTime.MaxValue);    // Earlier than
date.Should().Be(date);                       // Exact match

// Approximate Time
date.Should().BeCloseTo(DateTime.Now, 1000);  // Within milliseconds
date.Should().BeWithin(TimeSpan.FromSeconds(1))
     .After(DateTime.Now);
```

## Dictionary Assertions
```csharp
var dict = new Dictionary<string, int> 
{ 
    { "one", 1 }, 
    { "two", 2 } 
};

// Basic Assertions
dict.Should().ContainKey("one");              // Has key
dict.Should().ContainValue(2);                // Has value
dict.Should().Contain("one", 1);              // Has key-value pair

// Multiple Assertions
dict.Should()
    .HaveCount(2)
    .And.ContainKey("one")
    .And.ContainValue(2);
``` 