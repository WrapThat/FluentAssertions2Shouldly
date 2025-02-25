using System;
using Shouldly;

namespace FluentAssertions2Shouldly.Assertions
{
    public class Int16Assertions : NumericAssertions<short>
    {
        public Int16Assertions(short value) : base(value) { }

        public Int16Assertions BeAssignableTo<T>()
        {
            Subject.ShouldBeAssignableTo<T>();
            return this;
        }

        public Int16Assertions BeAssignableTo(Type expected)
        {
            Subject.ShouldBeAssignableTo(expected);
            return this;
        }
    }

    public class Int32Assertions : NumericAssertions<int>
    {
        public Int32Assertions(int value) : base(value) { }

        public Int32Assertions BeAssignableTo<T>()
        {
            Subject.ShouldBeAssignableTo<T>();
            return this;
        }

        public Int32Assertions BeAssignableTo(Type expected)
        {
            Subject.ShouldBeAssignableTo(expected);
            return this;
        }
    }

    public class Int64Assertions : NumericAssertions<long>
    {
        public Int64Assertions(long value) : base(value) { }

        public Int64Assertions BeAssignableTo<T>()
        {
            Subject.ShouldBeAssignableTo<T>();
            return this;
        }

        public Int64Assertions BeAssignableTo(Type expected)
        {
            Subject.ShouldBeAssignableTo(expected);
            return this;
        }
    }

    public class SingleAssertions : NumericAssertions<float>
    {
        public SingleAssertions(float value) : base(value) { }

        public SingleAssertions BeAssignableTo<T>()
        {
            Subject.ShouldBeAssignableTo<T>();
            return this;
        }

        public SingleAssertions BeAssignableTo(Type expected)
        {
            Subject.ShouldBeAssignableTo(expected);
            return this;
        }
    }

    public class DoubleAssertions : NumericAssertions<double>
    {
        public DoubleAssertions(double value) : base(value) { }

        public DoubleAssertions BeAssignableTo<T>()
        {
            Subject.ShouldBeAssignableTo<T>();
            return this;
        }

        public DoubleAssertions BeAssignableTo(Type expected)
        {
            Subject.ShouldBeAssignableTo(expected);
            return this;
        }
    }

    public class DecimalAssertions : NumericAssertions<decimal>
    {
        public DecimalAssertions(decimal value) : base(value) { }

        public DecimalAssertions BeAssignableTo<T>()
        {
            Subject.ShouldBeAssignableTo<T>();
            return this;
        }

        public DecimalAssertions BeAssignableTo(Type expected)
        {
            Subject.ShouldBeAssignableTo(expected);
            return this;
        }
    }
} 