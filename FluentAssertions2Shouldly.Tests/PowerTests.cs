using System;
using Xunit;

namespace FluentAssertions2Shouldly.Tests
{
    public class Power
    {
        public string Name { get; }
        public int Level { get; }

        public Power(string name, int powerLevel)
        {
            if (powerLevel < 0)
                throw new ArgumentOutOfRangeException(nameof(powerLevel), "Power level cannot be negative");

            Name = name ?? throw new ArgumentNullException(nameof(name));
            Level = powerLevel;
        }
    }

    public class PowerTests
    {
        [Theory]
        [InlineData("Super Strength", 100, false)]
        [InlineData("Flight", -1, true)]
        [InlineData(null, 50, true)]
        public void Power_Constructor_Should_Validate_Input(string name, int powerLevel, bool shouldThrow)
        {
            // Arrange
            var act = () => new Power(name, powerLevel);

            // Act & Assert
            if (shouldThrow)
            {
                if (powerLevel < 0)
                {
                    act.Should().ThrowExactly<ArgumentOutOfRangeException>();
                }
                else
                {
                    act.Should().ThrowExactly<ArgumentNullException>();
                }
            }
            else
            {
                act.Should().NotThrow();
            }
        }

        [Fact]
        public void Power_Constructor_Should_Set_Properties()
        {
            // Arrange
            var name = "Super Speed";
            var level = 75;

            // Act
            var power = new Power(name, level);

            // Assert
            power.Name.Should().Be(name);
            power.Level.Should().Be(level);
        }
    }
} 