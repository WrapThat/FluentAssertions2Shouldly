# FluentAssertions2Shouldly

A conversion library that allows you to use FluentAssertions syntax while actually using Shouldly under the hood. This is useful for projects that want to migrate from FluentAssertions to Shouldly gradually, or for developers who prefer FluentAssertions' syntax but want to use Shouldly's features.

## Quick Start

1. Install the packages:
```bash
dotnet add package FluentAssertions2Shouldly
dotnet add package Shouldly
```

2. Add using statements:
```csharp
using FluentAssertions;  // Use the familiar FluentAssertions namespace
using FluentAssertions2Shouldly;  // Add our conversion namespace
```

3. Write tests using FluentAssertions syntax:
```csharp
[Fact]
public void TestExample()
{
    // String assertions
    "Hello".Should().StartWith("H");
    
    // Numeric assertions
    42.Should().BeGreaterThan(41);
    
    // Collection assertions
    new[] { 1, 2, 3 }.Should().HaveCount(3);
    
    // Exception assertions
    Action action = () => throw new InvalidOperationException();
    action.Should().Throw<InvalidOperationException>();
}
```

## Documentation

- [Getting Started Guide](docs/GettingStarted.md) - Basic usage and examples
- [Advanced Features](docs/AdvancedFeatures.md) - Specialized assertions and complex scenarios
- [Migration Guide](docs/Migration.md) - Step-by-step guide to migrate from FluentAssertions to Shouldly

## Features

### Core Assertions
- String assertions (equality, contains, starts/ends with)
- Numeric assertions (equality, comparisons, ranges)
- Boolean assertions
- Collection assertions (count, contains, ordering)
- Object assertions (type checking, null checks)
- Exception assertions
- DateTime assertions
- Dictionary assertions

### Specialized Assertions
- File assertions
- Task/async assertions
- Approximate equality assertions
- Enum assertions
- Delegate assertions
- Property change assertions

### Advanced Features
- Nested collection assertions
- Complex object graph assertions
- Circular reference handling
- Custom assertion extensions
- Performance optimizations

## Edge Cases and Special Considerations

The library handles various edge cases:

1. Null values
```csharp
string nullString = null;
nullString.Should().BeNull();
```

2. Empty collections
```csharp
var empty = new List<int>();
empty.Should().BeEmpty();
```

3. Numeric boundaries
```csharp
int.MaxValue.Should().BeGreaterThan(0);
double.PositiveInfinity.Should().Be(double.PositiveInfinity);
```

4. Whitespace strings
```csharp
"   ".Should().NotBeEmpty();
"\t\n\r".Should().HaveLength(3);
```

5. Async operations
```csharp
await Task.Delay(100).Should().CompleteWithinAsync(TimeSpan.FromSeconds(1));
```

## Migration Guide

If you're migrating from FluentAssertions to Shouldly:

1. Install both FluentAssertions2Shouldly and Shouldly packages
2. Keep your existing FluentAssertions using statements
3. Add `using FluentAssertions2Shouldly`
4. Your tests will continue to work as before
5. Gradually migrate to native Shouldly syntax if desired

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

### Development Setup

1. Clone the repository
2. Install dependencies:
```bash
dotnet restore
```
3. Run tests:
```bash
dotnet test
```

### Guidelines

- Write tests for new features
- Update documentation when adding features
- Follow existing code style
- Add XML documentation comments
- Consider backward compatibility

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- FluentAssertions team for their excellent assertion library
- Shouldly team for their intuitive assertion syntax
- All contributors who help improve this project 