using System;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public static class Execute
    {
        public static AssertionScope Assertion => new AssertionScope();
    }

    public class AssertionScope
    {
        public AssertionScope BecauseOf(string because, params object[] becauseArgs)
        {
            return this;
        }

        public GivenSelector<T> Given<T>(Func<T> valueProvider)
        {
            return new GivenSelector<T>(valueProvider());
        }
    }

    public class GivenSelector<T>
    {
        private readonly T _value;

        public GivenSelector(T value)
        {
            _value = value;
        }

        public ForConditionSelector<T> ForCondition(Func<T, bool> predicate)
        {
            return new ForConditionSelector<T>(_value, predicate);
        }
    }

    public class ForConditionSelector<T>
    {
        private readonly T _value;
        private readonly Func<T, bool> _predicate;

        public ForConditionSelector(T value, Func<T, bool> predicate)
        {
            _value = value;
            _predicate = predicate;
        }

        public void FailWith(string failureMessage)
        {
            if (!_predicate(_value))
            {
                throw new ShouldAssertException(failureMessage);
            }
        }
    }
} 