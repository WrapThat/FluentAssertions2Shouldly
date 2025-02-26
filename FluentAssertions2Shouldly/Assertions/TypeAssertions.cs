using System;
using System.Reflection;
using System.Linq;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class TypeAssertions : ObjectAssertions<Type>
    {
        public TypeAssertions(Type value) : base(value)
        {
        }

        public new TypeAssertions And => this;

        public TypeAssertions BeDerivedFrom<T>()
        {
            typeof(T).IsAssignableFrom(Subject).ShouldBeTrue($"Expected type {Subject.Name} to be derived from {typeof(T).Name}");
            return this;
        }

        public TypeAssertions BeDerivedFrom(Type expected)
        {
            expected.IsAssignableFrom(Subject).ShouldBeTrue($"Expected type {Subject.Name} to be derived from {expected.Name}");
            return this;
        }

        public TypeAssertions NotBeDerivedFrom<T>()
        {
            typeof(T).IsAssignableFrom(Subject).ShouldBeFalse($"Expected type {Subject.Name} to not be derived from {typeof(T).Name}");
            return this;
        }

        public TypeAssertions NotBeDerivedFrom(Type expected)
        {
            expected.IsAssignableFrom(Subject).ShouldBeFalse($"Expected type {Subject.Name} to not be derived from {expected.Name}");
            return this;
        }

        public TypeAssertions Implement<T>()
        {
            var interfaceType = typeof(T);
            Subject.GetInterfaces().ShouldContain(interfaceType);
            return this;
        }

        public TypeAssertions Implement(Type expected)
        {
            Subject.GetInterfaces().ShouldContain(expected);
            return this;
        }

        public TypeAssertions NotImplement<T>()
        {
            var interfaceType = typeof(T);
            Subject.GetInterfaces().ShouldNotContain(interfaceType);
            return this;
        }

        public TypeAssertions NotImplement(Type expected)
        {
            Subject.GetInterfaces().ShouldNotContain(expected);
            return this;
        }

        public TypeAssertions HaveProperty(string name)
        {
            Subject.GetProperty(name).ShouldNotBeNull();
            return this;
        }

        public TypeAssertions NotHaveProperty(string name)
        {
            Subject.GetProperty(name).ShouldBeNull();
            return this;
        }

        public TypeAssertions HaveMethod(string name)
        {
            var methods = Subject.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
            var methodExists = methods.Any(m => m.Name == name);
            methodExists.ShouldBeTrue($"Expected type {Subject.Name} to have method {name} but it does not");
            return this;
        }

        public TypeAssertions NotHaveMethod(string name)
        {
            var methods = Subject.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
            var methodExists = methods.Any(m => m.Name == name);
            methodExists.ShouldBeFalse($"Expected type {Subject.Name} to not have method {name} but it does");
            return this;
        }

        public TypeAssertions HaveConstructor()
        {
            Subject.GetConstructors().ShouldNotBeEmpty();
            return this;
        }

        public TypeAssertions NotHaveConstructor()
        {
            Subject.GetConstructors().ShouldBeEmpty();
            return this;
        }

        public TypeAssertions NotBe(Type expected)
        {
            Subject.ShouldNotBe(expected);
            return this;
        }
    }
} 