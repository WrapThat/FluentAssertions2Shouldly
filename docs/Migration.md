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

```csharp
// FluentAssertions style
list.Should().HaveCount(3);
list.Should().Contain(2);
list.Should().BeInAscendingOrder();
list.Should().BeEquivalentTo(new[] { 3, 2, 1 });

// Shouldly equivalent
list.Count.ShouldBe(3);
list.ShouldContain(2);
list.ShouldBeInOrder();
list.ShouldBe(new[] { 3, 2, 1 });
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

## Advanced Migration Scenarios

### Property Change Notifications

```csharp
// FluentAssertions style
person.Should().RaisePropertyChangeFor(x => x.Name);

// Shouldly equivalent
var raised = false;
person.PropertyChanged += (s, e) => raised = e.PropertyName == nameof(person.Name);
person.Name = "John";
raised.ShouldBeTrue();
```

### Custom Assertions

```csharp
// FluentAssertions style
public static class CustomAssertions
{
    public static void BeValidEmail(this StringAssertions assertions)
    {
        assertions.Match(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}

// Shouldly equivalent
public static class CustomAssertions
{
    public static void ShouldBeValidEmail(this string value)
    {
        value.ShouldMatch(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
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