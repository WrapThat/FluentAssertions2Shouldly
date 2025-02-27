using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class ObjectAssertions<T> where T : class
    {
        public T Subject { get; }

        public ObjectAssertions(T value)
        {
            Subject = value;
        }

        public AndConstraint<ObjectAssertions<T>> And => new AndConstraint<ObjectAssertions<T>>(this);

        // Collection assertions for IReadOnlyList and IEnumerable
        public ObjectAssertions<T> HaveCount(int expected)
        {
            if (Subject is IReadOnlyCollection<object> readOnlyCollection)
            {
                readOnlyCollection.Count.ShouldBe(expected);
                return this;
            }
            if (Subject is IReadOnlyList<object> readOnlyList)
            {
                readOnlyList.Count.ShouldBe(expected);
                return this;
            }
            if (Subject is IEnumerable enumerable)
            {
                var count = enumerable.Cast<object>().Count();
                count.ShouldBe(expected);
                return this;
            }

            throw new ShouldAssertException($"Expected {typeof(T).Name} to implement IEnumerable or IReadOnlyList");
        }

        public ObjectAssertions<T> NotBeEmpty()
        {
            if (Subject is IEnumerable enumerable)
            {
                enumerable.Cast<object>().ShouldNotBeEmpty();
                return this;
            }

            throw new ShouldAssertException($"Expected {typeof(T).Name} to implement IEnumerable");
        }

        public ObjectAssertions<T> BeEmpty()
        {
            if (Subject is IEnumerable enumerable)
            {
                enumerable.Cast<object>().ShouldBeEmpty();
                return this;
            }

            throw new ShouldAssertException($"Expected {typeof(T).Name} to implement IEnumerable");
        }

        public ObjectAssertions<T> ContainSingle()
        {
            if (Subject is IEnumerable<object> enumerable)
            {
                enumerable.Count().ShouldBe(1);
                return this;
            }
            if (Subject is IEnumerable nonGenericEnumerable)
            {
                nonGenericEnumerable.Cast<object>().Count().ShouldBe(1);
                return this;
            }
            throw new ShouldAssertException($"Cannot verify single item on type {typeof(T).Name} as it does not implement IEnumerable");
        }

        public ObjectAssertions<T> ContainSingle(Func<object, bool> predicate)
        {
            if (Subject is IEnumerable<object> enumerable)
            {
                enumerable.Count(predicate).ShouldBe(1);
                return this;
            }
            if (Subject is IEnumerable nonGenericEnumerable)
            {
                nonGenericEnumerable.Cast<object>().Count(predicate).ShouldBe(1);
                return this;
            }
            throw new ShouldAssertException($"Cannot verify single item with predicate on type {typeof(T).Name} as it does not implement IEnumerable");
        }

        public ObjectAssertions<T> ContainSingle<TItem>(Func<TItem, bool> predicate)
        {
            if (Subject is IEnumerable<TItem> enumerable)
            {
                enumerable.Count(predicate).ShouldBe(1);
                return this;
            }
            throw new ShouldAssertException($"Cannot verify single item with predicate on type {typeof(T).Name} as it does not implement IEnumerable<{typeof(TItem).Name}>");
        }

        public ObjectAssertions<T> NotContainSingle<TItem>(Func<TItem, bool> predicate)
        {
            if (Subject is IEnumerable<TItem> enumerable)
            {
                enumerable.Count(predicate).ShouldNotBe(1);
                return this;
            }
            throw new ShouldAssertException($"Cannot verify not single item with predicate on type {typeof(T).Name} as it does not implement IEnumerable<{typeof(TItem).Name}>");
        }

        public ObjectAssertions<T> AllSatisfy(Action<object> action)
        {
            if (Subject is IEnumerable<object> enumerable)
            {
                foreach (var item in enumerable)
                {
                    action(item);
                }
                return this;
            }
            if (Subject is System.Collections.IEnumerable nonGenericEnumerable)
            {
                foreach (var item in nonGenericEnumerable)
                {
                    action(item);
                }
                return this;
            }
            throw new ShouldAssertException($"Cannot verify all items on type {typeof(T).Name} as it does not implement IEnumerable");
        }

        // Function assertions
        public ObjectAssertions<T> ThrowExactly<TException>() where TException : Exception
        {
            if (Subject is Delegate del)
            {
                try
                {
                    del.DynamicInvoke();
                    throw new ShouldAssertException($"Expected {typeof(TException).Name} but no exception was thrown");
                }
                catch (TargetInvocationException ex) when (ex.InnerException is TException)
                {
                    return this;
                }
                catch (Exception ex)
                {
                    throw new ShouldAssertException($"Expected {typeof(TException).Name} but got {ex.GetType().Name}");
                }
            }
            throw new ShouldAssertException($"Cannot verify throw on type {typeof(T).Name} as it is not a delegate");
        }

        public ObjectAssertions<T> NotThrow()
        {
            if (Subject is Delegate del)
            {
                try
                {
                    del.DynamicInvoke();
                    return this;
                }
                catch (TargetInvocationException ex)
                {
                    throw new ShouldAssertException($"Expected no exception but got {ex.InnerException?.GetType().Name ?? ex.GetType().Name}");
                }
                catch (Exception ex)
                {
                    throw new ShouldAssertException($"Expected no exception but got {ex.GetType().Name}");
                }
            }
            throw new ShouldAssertException($"Cannot verify not throw on type {typeof(T).Name} as it is not a delegate");
        }

        // Type conversion
        public ObjectAssertions<TExpected> As<TExpected>() where TExpected : class
        {
            var converted = Subject as TExpected;
            converted.ShouldNotBeNull();
            return new ObjectAssertions<TExpected>(converted);
        }

        public ObjectAssertions<T> Which => this;

        public ObjectAssertions<T> BeNull()
        {
            Subject.ShouldBeNull();
            return this;
        }

        public ObjectAssertions<T> NotBeNull()
        {
            Subject.ShouldNotBeNull();
            return this;
        }

        public ObjectAssertions<T> Be(T expected)
        {
            Subject.ShouldBe(expected);
            return this;
        }

        public ObjectAssertions<T> BeSameAs(T expected)
        {
            ReferenceEquals(Subject, expected).ShouldBeTrue();
            return this;
        }

        public ObjectAssertions<T> NotBeSameAs(T expected)
        {
            ReferenceEquals(Subject, expected).ShouldBeFalse();
            return this;
        }

        public ObjectAssertions<T> BeOfType(Type expected)
        {
            Subject.GetType().ShouldBe(expected);
            return this;
        }

        public ObjectAssertions<T> BeOfType<TExpected>()
        {
            Subject.ShouldBeOfType<TExpected>();
            return this;
        }

        public ObjectAssertions<T> BeAssignableTo<TExpected>()
        {
            Subject.ShouldBeAssignableTo<TExpected>();
            return this;
        }

        public ObjectAssertions<T> BeAssignableTo(Type expected)
        {
            Subject.ShouldBeAssignableTo(expected);
            return this;
        }

        public ObjectAssertions<T> NotBeAssignableTo<TExpected>()
        {
            Subject.GetType().IsAssignableTo(typeof(TExpected))
                .ShouldBeFalse();
            return this;
        }

        public ObjectAssertions<T> NotBeAssignableTo(Type expected)
        {
            Subject.GetType().IsAssignableTo(expected)
                .ShouldBeFalse();
            return this;
        }

        public ObjectAssertions<T> BeEquivalentTo(T expected)
        {
            Subject.ShouldBe(expected);
            return this;
        }

        public ObjectAssertions<T> NotBeEquivalentTo(T unexpected)
        {
            Subject.ShouldNotBe(unexpected);
            return this;
        }

        public ObjectAssertions<T> Match<TValue>(Func<TValue, bool> predicate) where TValue : class
        {
            var value = Subject as TValue;
            value.ShouldNotBeNull();
            predicate(value).ShouldBeTrue();
            return this;
        }
    }
} 