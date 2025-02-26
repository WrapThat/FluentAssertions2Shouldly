using System;
using System.Collections.Generic;
using System.Text;
using NetArchTest.Rules;
using Xunit;
using FluentAssertions2Shouldly;

namespace FluentAssertions2Shouldly.Tests
{
    public interface ITestResult
    {
        bool IsSuccessful { get; }
        IEnumerable<string> FailingTypeNames { get; }
    }

    public class TestResultAdapter : ITestResult
    {
        private readonly TestResult _testResult;

        public TestResultAdapter(TestResult testResult)
        {
            _testResult = testResult ?? throw new ArgumentNullException(nameof(testResult));
        }

        public bool IsSuccessful => _testResult.IsSuccessful;
        public IEnumerable<string> FailingTypeNames => _testResult.FailingTypeNames;
    }

    public class TestResultAssertions : ReferenceTypeAssertions<ITestResult, TestResultAssertions>
    {
        public TestResultAssertions(ITestResult instance) : base(instance)
        {
        }

        protected override string Identifier => "TestResult";

        public AndConstraint<TestResultAssertions> BeSuccessful(string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject)
                .ForCondition(s => s.IsSuccessful)
                .FailWith(GetFailureMessage());

            return new AndConstraint<TestResultAssertions>(this);
        }

        private string GetFailureMessage()
        {
            if (Subject.IsSuccessful)
                return string.Empty;

            var sb = new StringBuilder();
            sb.AppendLine("The following types failed the test:");

            foreach (var name in Subject.FailingTypeNames)
                sb.AppendLine(name);

            return sb.ToString();
        }
    }

    public static class TestResultExtensions
    {
        public static TestResultAssertions Should(this ITestResult value)
        {
            return new TestResultAssertions(value);
        }
    }

    public class ArchitectureTests
    {
        [Fact]
        public void TestResult_ShouldWork()
        {
            // Arrange
            var types = Types.InCurrentDomain();
            var result = types.That()
                .ResideInNamespace("FluentAssertions2Shouldly")
                .And()
                .DoNotResideInNamespace("FluentAssertions2Shouldly.Tests")
                .And()
                .DoNotResideInNamespace("FluentAssertions2Shouldly.Examples")
                .Should()
                .BePublic()
                .GetResult();

            // Act & Assert
            new TestResultAdapter(result).Should().BeSuccessful("all types in the main library namespace should be public");
        }
    }
} 