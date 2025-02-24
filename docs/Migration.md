# Migration Guide

## Overview

This guide helps you migrate from FluentAssertions to Shouldly using our compatibility layer. The migration can be done gradually, allowing you to maintain working tests while transitioning.

## Step-by-Step Migration

### 1. Preparation

```bash
# Install required packages
dotnet add package FluentAssertions2Shouldly
dotnet add package Shouldly
```

### 2. Update Using Statements

```csharp
// Original
using FluentAssertions;

// Add our compatibility layer
using FluentAssertions2Shouldly;
```

### 3. Gradual Migration Patterns

#### Pattern 1: File-by-File Migration
```csharp
// Original test file (FluentAssertions style)
public class UserTests
{
    [Fact]
    public void User_Should_Have_Valid_Name()
    {
        var user = new User("John");
        user.Name.Should().NotBeNullOrEmpty();
    }
}

// Migrated test file (Shouldly style)
public class OrderTests
{
    [Fact]
    public void Order_Should_Have_Valid_Total()
    {
        var order = new Order(100);
        order.Total.ShouldBe(100);
    }
}
```

#### Pattern 2: Class-by-Class Migration
```csharp
// Mixed migration in same file
public class UserTests
{
    // Still using FluentAssertions syntax (works through our layer)
    [Fact]
    public void User_Should_Have_Valid_Name()
    {
        var user = new User("John");
        user.Name.Should().NotBeNullOrEmpty();
    }

    // Migrated to Shouldly syntax
    [Fact]
    public void User_Should_Have_Valid_Age()
    {
        var user = new User(25);
        user.Age.ShouldBeGreaterThan(0);
    }
}
```

### 4. Common Migration Patterns

#### String Assertions
```csharp
// FluentAssertions
string text = "Hello";
text.Should().StartWith("H");
text.Should().NotBeNullOrWhiteSpace();

// Shouldly equivalent
text.ShouldStartWith("H");
text.ShouldNotBeNullOrWhiteSpace();
```

#### Numeric Assertions
```csharp
// FluentAssertions
int number = 42;
number.Should().BeGreaterThan(0);
number.Should().BeLessThan(100);

// Shouldly equivalent
number.ShouldBeGreaterThan(0);
number.ShouldBeLessThan(100);
```

#### Collection Assertions
```csharp
// FluentAssertions
var list = new[] { 1, 2, 3 };
list.Should().HaveCount(3);
list.Should().Contain(2);

// Shouldly equivalent
list.Count.ShouldBe(3);
list.ShouldContain(2);
```

#### Exception Assertions
```csharp
// FluentAssertions
Action action = () => throw new InvalidOperationException("error");
action.Should().Throw<InvalidOperationException>()
      .WithMessage("error");

// Shouldly equivalent
Should.Throw<InvalidOperationException>(() => 
    throw new InvalidOperationException("error"))
    .Message.ShouldBe("error");
```

### 5. Advanced Migration Scenarios

#### Async Tests
```csharp
// FluentAssertions
await action.Should().CompleteWithinAsync(TimeSpan.FromSeconds(1));

// Shouldly equivalent
await Should.CompleteInAsync(action, TimeSpan.FromSeconds(1));
```

#### Property Change Notifications
```csharp
// FluentAssertions
person.Should().RaisePropertyChangeFor(x => x.Name);

// Shouldly equivalent
person.ShouldRaisePropertyChangeFor(x => x.Name);
```

#### Custom Assertions
```csharp
// FluentAssertions
public static class CustomAssertions
{
    public static AndConstraint<StringAssertions> BeValidEmail(
        this StringAssertions assertions)
    {
        assertions.Match(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        return new AndConstraint<StringAssertions>(assertions);
    }
}

// Shouldly equivalent
public static class CustomAssertions
{
    public static void ShouldBeValidEmail(this string actual)
    {
        actual.ShouldMatch(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
```

## Common Migration Challenges

### 1. Chained Assertions
```csharp
// FluentAssertions (chained)
text.Should()
    .StartWith("Hello")
    .And.EndWith("World")
    .And.HaveLength(11);

// Shouldly (separate)
text.ShouldStartWith("Hello");
text.ShouldEndWith("World");
text.Length.ShouldBe(11);
```

### 2. Complex Type Assertions
```csharp
// FluentAssertions
person.Should().BeOfType<Employee>()
      .Which.Department.Should().Be("IT");

// Shouldly
person.ShouldBeOfType<Employee>();
((Employee)person).Department.ShouldBe("IT");
```

### 3. Collection Ordering
```csharp
// FluentAssertions
list.Should().BeInAscendingOrder();

// Shouldly
list.ShouldBeInOrder();
```

## Best Practices During Migration

1. **Maintain Consistency**: Keep consistent assertion style within each test class.
2. **Test Coverage**: Ensure test coverage doesn't decrease during migration.
3. **Commit Strategy**: Commit migrations separately from functional changes.
4. **Documentation**: Update team documentation and coding guidelines.
5. **Review Process**: Have clear review guidelines for migrated tests. 