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

        public new CollectionAssertions<T> And => this;

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
            for (int i = 0; i < list.Count - 1; i++)
            {
                Comparer<T>.Default.Compare(list[i], list[i + 1]).ShouldBeGreaterThanOrEqualTo(0);
            }
            return this;
        }

        public CollectionAssertions<T> ContainInOrder(params T[] expected)
        {
            var list = Subject.ToList();
            var expectedList = expected.ToList();
            
            int currentIndex = 0;
            foreach (var item in expectedList)
            {
                var index = list.IndexOf(item, currentIndex);
                index.ShouldBeGreaterThanOrEqualTo(currentIndex);
                currentIndex = index + 1;
            }
            return this;
        }

        public CollectionAssertions<T> ContainInConsecutiveOrder(params T[] expected)
        {
            var list = Subject.ToList();
            var expectedList = expected.ToList();
            
            for (int i = 0; i < expectedList.Count; i++)
            {
                list.Skip(i).Take(1).ShouldContain(expectedList[i]);
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

        public new CollectionAssertions<T> BeEquivalentTo(IEnumerable<T> expected)
        {
            var subjectList = Subject.ToList();
            var expectedList = (expected ?? Array.Empty<T>()).ToList();

            subjectList.Count.ShouldBe(expectedList.Count, "Expected collections to have the same number of elements");
            
            foreach (var item in expectedList)
            {
                subjectList.ShouldContain(item);
            }

            foreach (var item in subjectList)
            {
                expectedList.ShouldContain(item);
            }

            return this;
        }

        public new CollectionAssertions<T> NotBeEquivalentTo(IEnumerable<T> unexpected)
        {
            Subject.ShouldNotBe(unexpected ?? Array.Empty<T>());
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

        public CollectionAssertions<T> AllSatisfy(Action<T> action)
        {
            foreach (var item in Subject)
            {
                action(item);
            }
            return this;
        }

        public CollectionAssertions<T> SatisfyRespectively(params Action<T>[] actions)
        {
            var list = Subject.ToList();
            list.Count.ShouldBe(actions.Length);
            
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i](list[i]);
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
            var list = Subject.ToList();
            var supersetList = superset.ToList();
            
            foreach (var item in list)
            {
                supersetList.ShouldContain(item);
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