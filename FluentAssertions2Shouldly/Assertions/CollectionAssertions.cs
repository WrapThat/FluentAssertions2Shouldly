using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class CollectionAssertions<T> : ObjectAssertions<IEnumerable<T>>
    {
        public CollectionAssertions(IEnumerable<T> subject) : base(subject)
        {
        }

        public CollectionAssertions<T> BeEmpty()
        {
            Subject.ShouldBeEmpty();
            return this;
        }

        public CollectionAssertions<T> NotBeEmpty()
        {
            Subject.ShouldNotBeEmpty();
            return this;
        }

        public CollectionAssertions<T> HaveCount(int expected)
        {
            Subject.Count().ShouldBe(expected);
            return this;
        }

        public CollectionAssertions<T> Contain(T expected)
        {
            Subject.ShouldContain(expected);
            return this;
        }

        public CollectionAssertions<T> BeInAscendingOrder()
        {
            var list = Subject.ToList();
            var sorted = list.OrderBy(x => x).ToList();
            list.SequenceEqual(sorted).ShouldBeTrue();
            return this;
        }

        public CollectionAssertions<T> BeInDescendingOrder()
        {
            var list = Subject.ToList();
            var sorted = list.OrderByDescending(x => x).ToList();
            list.SequenceEqual(sorted).ShouldBeTrue();
            return this;
        }

        public CollectionAssertions<T> ContainInOrder(params T[] expected)
        {
            var list = Subject.ToList();
            list.Count.ShouldBeGreaterThanOrEqualTo(expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                list[i].ShouldBe(expected[i]);
            }
            return this;
        }

        public CollectionAssertions<T> OnlyContain(params T[] expected)
        {
            var list = Subject.ToList();
            list.Count.ShouldBe(expected.Length);
            foreach (var item in expected)
            {
                list.ShouldContain(item);
            }
            return this;
        }

        public CollectionAssertions<T> BeEquivalentTo(IEnumerable<T> expected)
        {
            var list = Subject.ToList();
            var expectedList = expected.ToList();
            list.Count.ShouldBe(expectedList.Count);
            foreach (var item in expectedList)
            {
                list.ShouldContain(item);
            }
            return this;
        }

        public CollectionAssertions<T> NotContain(T unexpected)
        {
            Subject.ShouldNotContain(unexpected);
            return this;
        }

        public CollectionAssertions<T> ContainSingle()
        {
            Subject.Count().ShouldBe(1);
            return this;
        }

        public CollectionAssertions<T> ContainSingle(T expected)
        {
            Subject.Single().ShouldBe(expected);
            return this;
        }

        public CollectionAssertions<T> ContainSingle(Func<T, bool> predicate)
        {
            Subject.Count(predicate).ShouldBe(1);
            return this;
        }

        public CollectionAssertions<T> OnlyContainElementsThat(Func<T, bool> predicate)
        {
            foreach (var item in Subject)
            {
                predicate(item).ShouldBeTrue();
            }
            return this;
        }

        public CollectionAssertions<T> NotContainNulls()
        {
            if (typeof(T).IsValueType)
            {
                return this;
            }

            foreach (var item in Subject)
            {
                if (item == null)
                {
                    throw new ShouldAssertException($"Collection should not contain nulls but found one at index {Subject.ToList().IndexOf(item)}");
                }
            }
            return this;
        }

        public CollectionAssertions<T> HaveElementAt(int index, T expected)
        {
            Subject.ElementAt(index).ShouldBe(expected);
            return this;
        }

        public CollectionAssertions<T> StartWith(T expected)
        {
            Subject.First().ShouldBe(expected);
            return this;
        }

        public CollectionAssertions<T> EndWith(T expected)
        {
            Subject.Last().ShouldBe(expected);
            return this;
        }

        public CollectionAssertions<T> BeSubsetOf(IEnumerable<T> superset)
        {
            var supersetList = superset.ToList();
            foreach (var item in Subject)
            {
                supersetList.ShouldContain(item);
            }
            return this;
        }

        public CollectionAssertions<T> OnlyContain(Func<T, bool> predicate)
        {
            Subject.All(predicate).ShouldBeTrue();
            return this;
        }

        public CollectionAssertions<T> BeEquivalentTo(IEnumerable<T> expected, 
            Func<T, T, bool> compareFunc = null)
        {
            var subjectList = Subject.ToList();
            var expectedList = expected.ToList();

            subjectList.Count.ShouldBe(expectedList.Count);

            if (compareFunc != null)
            {
                foreach (var expectedItem in expectedList)
                {
                    subjectList.Any(item => compareFunc(item, expectedItem))
                        .ShouldBeTrue();
                }
            }
            else
            {
                foreach (var expectedItem in expectedList)
                {
                    subjectList.ShouldContain(expectedItem);
                }
            }

            return this;
        }

        public CollectionAssertions<T> IntersectWith(IEnumerable<T> other)
        {
            var intersection = Subject.Intersect(other);
            intersection.ShouldNotBeEmpty();
            return this;
        }

        public CollectionAssertions<T> NotIntersectWith(IEnumerable<T> other)
        {
            var intersection = Subject.Intersect(other);
            intersection.ShouldBeEmpty();
            return this;
        }
    }
} 