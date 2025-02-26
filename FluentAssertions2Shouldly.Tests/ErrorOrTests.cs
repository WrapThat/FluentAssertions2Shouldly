using ErrorOr;
using Xunit;

namespace FluentAssertions2Shouldly.Tests;

public class ErrorOrTests
{
    private static readonly Error TestError = Error.Validation("Test.Error", "This is a test error");

    [Fact]
    public void Error_ShouldWork()
    {
        // Arrange
        var error = TestError;

        // Act & Assert
        error.Should().Be(TestError);
        error.Should().NotBe(Error.Unexpected("Different.Error", "A different error"));
    }

    [Fact]
    public void ErrorOr_ShouldWork()
    {
        // Arrange
        ErrorOr<int> result = TestError;

        // Act & Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(TestError);
    }
} 