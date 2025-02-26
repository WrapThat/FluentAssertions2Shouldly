using System.Collections.Generic;
using Xunit;

namespace FluentAssertions2Shouldly.Tests
{
    public class Mission
    {
        public string Name { get; }
        public bool IsCompleted { get; }

        public Mission(string name, bool isCompleted = false)
        {
            Name = name;
            IsCompleted = isCompleted;
        }
    }

    public class MissionTests
    {
        [Fact]
        public void ReadOnlyList_Should_Work()
        {
            // Arrange
            IReadOnlyList<Mission> missions = new List<Mission>
            {
                new Mission("First Mission"),
                new Mission("Second Mission", true)
            };

            // Act & Assert
            missions.Should().HaveCount(2);
            missions.Should().NotBeEmpty();
        }

        [Fact]
        public void ReadOnlyList_ContainSingle_Should_Work()
        {
            // Arrange
            var mission = new Mission("Test Mission", true);
            IReadOnlyList<Mission> missions = new List<Mission> { mission };

            // Act & Assert
            missions.Should().ContainSingle();
            missions.Should().ContainSingle<Mission>(m => m.IsCompleted);
            missions.Should().NotContainSingle<Mission>(m => !m.IsCompleted);
        }

        [Fact]
        public void List_Should_Work()
        {
            // Arrange
            var missions = new List<Mission>
            {
                new Mission("First Mission"),
                new Mission("Second Mission", true)
            };

            // Act & Assert
            missions.Should().HaveCount(2);
            missions.Should().NotBeEmpty();
        }
    }
} 