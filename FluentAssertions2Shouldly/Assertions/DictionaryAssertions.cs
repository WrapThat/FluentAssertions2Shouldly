using System.Collections.Generic;
using System.Linq;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class DictionaryAssertions<TKey, TValue>
    {
        private readonly IDictionary<TKey, TValue> Subject;

        public DictionaryAssertions(IDictionary<TKey, TValue> value)
        {
            Subject = value;
        }

        public AndConstraint<DictionaryAssertions<TKey, TValue>> ContainKey(TKey key)
        {
            Subject.Keys.ShouldContain(key);
            return new AndConstraint<DictionaryAssertions<TKey, TValue>>(this);
        }

        public AndConstraint<DictionaryAssertions<TKey, TValue>> NotContainKey(TKey key)
        {
            Subject.Keys.ShouldNotContain(key);
            return new AndConstraint<DictionaryAssertions<TKey, TValue>>(this);
        }

        public AndConstraint<DictionaryAssertions<TKey, TValue>> ContainKeys(params TKey[] keys)
        {
            foreach (var key in keys)
            {
                Subject.Keys.ShouldContain(key);
            }
            return new AndConstraint<DictionaryAssertions<TKey, TValue>>(this);
        }

        public AndConstraint<DictionaryAssertions<TKey, TValue>> ContainValues(params TValue[] values)
        {
            foreach (var value in values)
            {
                Subject.Values.ShouldContain(value);
            }
            return new AndConstraint<DictionaryAssertions<TKey, TValue>>(this);
        }

        public AndConstraint<DictionaryAssertions<TKey, TValue>> HaveValueForKey(TKey key, TValue value)
        {
            Subject[key].ShouldBe(value);
            return new AndConstraint<DictionaryAssertions<TKey, TValue>>(this);
        }

        public AndConstraint<DictionaryAssertions<TKey, TValue>> HaveValue(TValue value)
        {
            Subject.Values.ShouldContain(value);
            return new AndConstraint<DictionaryAssertions<TKey, TValue>>(this);
        }

        public AndConstraint<DictionaryAssertions<TKey, TValue>> ContainValue(TValue value)
        {
            Subject.Values.ShouldContain(value);
            return new AndConstraint<DictionaryAssertions<TKey, TValue>>(this);
        }

        public AndConstraint<DictionaryAssertions<TKey, TValue>> HaveCount(int expected)
        {
            Subject.Count.ShouldBe(expected);
            return new AndConstraint<DictionaryAssertions<TKey, TValue>>(this);
        }

        public AndConstraint<DictionaryAssertions<TKey, TValue>> BeEmpty()
        {
            Subject.ShouldBeEmpty();
            return new AndConstraint<DictionaryAssertions<TKey, TValue>>(this);
        }

        public AndConstraint<DictionaryAssertions<TKey, TValue>> NotBeEmpty()
        {
            Subject.ShouldNotBeEmpty();
            return new AndConstraint<DictionaryAssertions<TKey, TValue>>(this);
        }
    }
} 