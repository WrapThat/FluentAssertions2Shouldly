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

        public TypeAssertions BeDerivedFrom<T>()
        {
            Subject.ShouldBeAssignableTo<T>();
            return this;
        }

        public TypeAssertions BeDerivedFrom(Type expected)
        {
            Subject.ShouldBeAssignableTo(expected);
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

        public TypeAssertions HaveProperty(string name)
        {
            Subject.GetProperty(name).ShouldNotBeNull();
            return this;
        }

        public TypeAssertions HaveMethod(string name)
        {
            var methods = Subject.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
            var methodExists = methods.Any(m => m.Name == name);
            if (!methodExists)
            {
                throw new ShouldAssertException($"Expected type {Subject.Name} to have method {name} but it does not");
            }
            return this;
        }

        public TypeAssertions HaveConstructor()
        {
            Subject.GetConstructors().ShouldNotBeEmpty();
            return this;
        }

        public TypeAssertions NotBe(Type expected)
        {
            Subject.ShouldNotBe(expected);
            return this;
        }
    }
} 