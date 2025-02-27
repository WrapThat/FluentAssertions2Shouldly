using System;
using System.Linq.Expressions;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public abstract class ReferenceTypeAssertions<TSubject, TAssertions>
        where TAssertions : ReferenceTypeAssertions<TSubject, TAssertions>
        where TSubject : class
    {
        protected TSubject Subject { get; }
        protected abstract string Identifier { get; }

        protected ReferenceTypeAssertions(TSubject instance)
        {
            Subject = instance;
        }

        public AndConstraint<TAssertions> Because(string because, params object[] becauseArgs)
        {
            // In Shouldly, we don't have direct support for because messages
            // but we can store it for potential use in failure messages
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        public AndConstraint<TAssertions> Be(TSubject expected)
        {
            Subject.ShouldBe(expected);
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        public AndConstraint<TAssertions> NotBe(TSubject expected)
        {
            Subject.ShouldNotBe(expected);
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        public AndConstraint<TAssertions> BeSameAs(TSubject expected)
        {
            ReferenceEquals(Subject, expected).ShouldBeTrue();
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        public AndConstraint<TAssertions> NotBeSameAs(TSubject expected)
        {
            ReferenceEquals(Subject, expected).ShouldBeFalse();
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        public AndConstraint<TAssertions> BeNull()
        {
            Subject.ShouldBeNull();
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        public AndConstraint<TAssertions> NotBeNull()
        {
            Subject.ShouldNotBeNull();
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        public AndConstraint<TAssertions> BeOfType<T>()
        {
            Subject.ShouldBeOfType<T>();
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        public AndConstraint<TAssertions> BeOfType(Type expected)
        {
            Subject.GetType().ShouldBe(expected);
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        public AndConstraint<TAssertions> NotBeOfType<T>()
        {
            Subject.ShouldNotBeOfType<T>();
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        public AndConstraint<TAssertions> NotBeOfType(Type unexpected)
        {
            Subject.GetType().ShouldNotBe(unexpected);
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        public AndConstraint<TAssertions> BeAssignableTo<T>()
        {
            Subject.ShouldBeAssignableTo<T>();
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        public AndConstraint<TAssertions> NotBeAssignableTo<T>()
        {
            Subject.ShouldNotBeAssignableTo<T>();
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        public AndConstraint<TAssertions> Match(Expression<Func<TSubject, bool>> predicate)
        {
            var func = predicate.Compile();
            func(Subject).ShouldBeTrue();
            return new AndConstraint<TAssertions>((TAssertions)this);
        }

        public AndConstraint<TAssertions> NotMatch(Expression<Func<TSubject, bool>> predicate)
        {
            var func = predicate.Compile();
            func(Subject).ShouldBeFalse();
            return new AndConstraint<TAssertions>((TAssertions)this);
        }
    }
} 