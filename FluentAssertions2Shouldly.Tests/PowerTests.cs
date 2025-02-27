using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FluentAssertions2Shouldly.Tests
{
    public interface IValueObject { }

    public record Power : IValueObject
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

    public record HeroId
    {
        public Guid Value { get; }
        private HeroId(Guid value) => Value = value;
        public static HeroId From(Guid value) => new(value);
    }

    public class PowerLevelUpdatedEvent
    {
        public Hero Hero { get; }
        public PowerLevelUpdatedEvent(Hero hero) => Hero = hero;
    }

    public class Hero
    {
        private readonly List<object> _domainEvents = new();
        private readonly List<Power> _powers = new();
        
        public HeroId Id { get; set; }
        public string Name { get; }
        public string Alias { get; }
        public IReadOnlyList<Power> Powers => _powers.AsReadOnly();
        public int PowerLevel => _powers.Sum(p => p.Level);

        private Hero(string name, string alias)
        {
            Name = name;
            Alias = alias;
        }

        public static Hero Create(string name, string alias) => new(name, alias);

        public void UpdatePowers(IEnumerable<Power> powers)
        {
            _powers.Clear();
            _powers.AddRange(powers);
            _domainEvents.Add(new PowerLevelUpdatedEvent(this));
        }

        public IReadOnlyList<object> PopDomainEvents()
        {
            var events = _domainEvents.ToList();
            _domainEvents.Clear();
            return events;
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
            Func<Power> act = () => new Power(name, powerLevel);

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

        [Fact]
        public void Power_ReadOnlyList_ContainSingle_Should_Work()
        {
            // Arrange
            var power = new Power("Super Speed", 75);
            IReadOnlyList<Power> powers = new List<Power> { power };

            // Act & Assert
            powers.Should().ContainSingle();
            powers.Should().ContainSingle<Power>(p => p.Level == 75);
            powers.Should().NotContainSingle<Power>(p => p.Level == 100);
        }

        [Fact]
        public void UpdatePowers_ShouldRaisePowerLevelUpdatedEvent()
        {
            // Arrange
            var hero = Hero.Create("Superman", "Clark Kent");
            hero.Id = HeroId.From(Guid.NewGuid());
            
            // Act
            hero.UpdatePowers(new[] { new Power("Super-strength", 10) });

            // Assert
            var domainEvents = hero.PopDomainEvents();
            domainEvents.Should().NotBeNull();
            domainEvents.Should().HaveCount(1);
            domainEvents.First().Should().BeOfType<PowerLevelUpdatedEvent>()
                .Which.Invoking((Action<PowerLevelUpdatedEvent>)(e =>
                {
                    e.Hero.PowerLevel.Should().Be(10);
                    e.Hero.Id.Should().Be(hero.Id);
                    e.Hero.Name.Should().Be(hero.Name);
                })).Invoke();
            hero.Powers.Should().ContainSingle<Power>(p => p.Name == "Super-strength");
        }

        [Fact]
        public void RemovePower_ShouldRaisePowerLevelUpdatedEvent()
        {
            // Act
            var hero = Hero.Create("name", "alias");
            var power = new Power("Super-strength", 10);
            hero.UpdatePowers(new[] { power });

            // Assert
            var domainEvents = hero.PopDomainEvents();
            domainEvents.Should().NotBeNull();
            domainEvents.Should().HaveCount(1);
            domainEvents.Should().AllSatisfy(e => e.Should().BeOfType<PowerLevelUpdatedEvent>());
            var evt = domainEvents.Last().As<PowerLevelUpdatedEvent>();
            evt.Subject.Hero.PowerLevel.Should().Be(10);
            hero.Powers.Should().HaveCount(1);
        }
    }
} 