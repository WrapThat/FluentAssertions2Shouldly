using System.Collections.Generic;
using System.Linq;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class DictionaryAssertions<TKey, TValue> : ObjectAssertions<IDictionary<TKey, TValue>>
        where TKey : notnull
    {
        public DictionaryAssertions(IDictionary<TKey, TValue> subject) : base(subject)
        {
        }

        public new DictionaryAssertions<TKey, TValue> And => this;

        public DictionaryAssertions<TKey, TValue> ContainKey(TKey expectedKey)
        {
            Subject.Keys.ShouldContain(expectedKey);
            return this;
        }

        public DictionaryAssertions<TKey, TValue> ContainValue(TValue expectedValue)
        {
            Subject.Values.ShouldContain(expectedValue);
            return this;
        }

        public DictionaryAssertions<TKey, TValue> ContainPair(TKey expectedKey, TValue expectedValue)
        {
            Subject.ShouldContainKeyAndValue(expectedKey, expectedValue);
            return this;
        }

        public DictionaryAssertions<TKey, TValue> NotContainKey(TKey key)
        {
            Subject.Keys.ShouldNotContain(key);
            return this;
        }

        public DictionaryAssertions<TKey, TValue> ContainKeys(params TKey[] keys)
        {
            foreach (var key in keys)
            {
                Subject.Keys.ShouldContain(key);
            }
            return this;
        }

        public DictionaryAssertions<TKey, TValue> ContainValues(params TValue[] values)
        {
            foreach (var value in values)
            {
                Subject.Values.ShouldContain(value);
            }
            return this;
        }

        public DictionaryAssertions<TKey, TValue> HaveValueForKey(TKey key, TValue value)
        {
            Subject[key].ShouldBe(value);
            return this;
        }

        public DictionaryAssertions<TKey, TValue> NotContainPair(TKey key, TValue value)
        {
            if (Subject.TryGetValue(key, out var actualValue) && EqualityComparer<TValue>.Default.Equals(actualValue, value))
            {
                throw new ShouldAssertException($"Dictionary should not contain key-value pair [{key}] = {value} but it does");
            }
            return this;
        }

        public DictionaryAssertions<TKey, TValue> ContainSingle()
        {
            Subject.Count.ShouldBe(1);
            return this;
        }

        public DictionaryAssertions<TKey, TValue> BeSubsetOf(IDictionary<TKey, TValue> superset)
        {
            foreach (var pair in Subject)
            {
                superset.ShouldContainKeyAndValue(pair.Key, pair.Value);
            }
            return this;
        }

        public DictionaryAssertions<TKey, TValue> NotBeSubsetOf(IDictionary<TKey, TValue> superset)
        {
            var isSubset = Subject.All(pair => superset.TryGetValue(pair.Key, out var value) && EqualityComparer<TValue>.Default.Equals(value, pair.Value));
            isSubset.ShouldBeFalse();
            return this;
        }

        public DictionaryAssertions<TKey, TValue> HaveValue(TValue value)
        {
            Subject.Values.ShouldContain(value);
            return this;
        }

        public DictionaryAssertions<TKey, TValue> HaveCount(int expected)
        {
            Subject.Count.ShouldBe(expected);
            return this;
        }

        public DictionaryAssertions<TKey, TValue> BeEmpty()
        {
            Subject.ShouldBeEmpty();
            return this;
        }

        public DictionaryAssertions<TKey, TValue> NotBeEmpty()
        {
            Subject.ShouldNotBeEmpty();
            return this;
        }
    }
} 