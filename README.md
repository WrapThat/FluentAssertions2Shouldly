# FluentAssertions2Shouldly

> This library was developed using [Cursor](https://cursor.sh/), an AI-powered code editor.

A library that provides a FluentAssertions-style syntax while using Shouldly under the hood. This is perfect for:

- Projects migrating from FluentAssertions to Shouldly
- Teams who prefer FluentAssertions' syntax but want to use Shouldly's features
- Maintaining consistent test style across mixed codebases

## Requirements

- .NET 6 or higher (required for global using statements and file-scoped namespaces)

## Installation

```bash
dotnet add package FluentAssertions2Shouldly
```

## Usage

The library provides a familiar FluentAssertions-style syntax:

```csharp
// String assertions
string text = "Hello";
text.Should().Be("Hello");
text.Should().StartWith("He");
text.Should().Contain("ell");

// Numeric assertions
int number = 42;
number.Should().Be(42);
number.Should().BeGreaterThan(40);
number.Should().BeInRange(40, 45);

// Collection assertions
var list = new[] { 1, 2, 3 };
list.Should().HaveCount(3);
list.Should().Contain(2);
list.Should().BeInAscendingOrder();

// Exception assertions
Action action = () => throw new Exception();
action.Should().Throw<Exception>();
```

While using Shouldly under the hood:

```csharp
// What you write
text.Should().Be("Hello");

// What actually runs
text.ShouldBe("Hello");
```

## Features

### Core Assertions
- String assertions
- Numeric assertions
- Boolean assertions
- Collection assertions
- Exception assertions
- DateTime assertions
- Dictionary assertions

### Specialized Assertions
- File assertions
- Async/Task assertions
- Approximate equality assertions
- Enum assertions
- Property change assertions
- Custom assertions

## Documentation

- [Getting Started](docs/GettingStarted.md)
- [Advanced Features](docs/AdvancedFeatures.md)
- [Migration Guide](docs/Migration.md)
- [API Reference](docs/api/README.md)

## Migration

The library makes it easy to migrate from FluentAssertions to Shouldly gradually:

1. Replace FluentAssertions package with FluentAssertions2Shouldly
2. Tests continue to work with the same syntax
3. Optionally migrate to native Shouldly syntax over time

Example:
```csharp
// Original FluentAssertions code
using FluentAssertions;
value.Should().Be(expected);

// Step 1: Switch to FluentAssertions2Shouldly
using FluentAssertions2Shouldly;
value.Should().Be(expected);

// Step 2 (Optional): Native Shouldly
using Shouldly;
value.ShouldBe(expected);
```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request. For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License - see the LICENSE file for details. 