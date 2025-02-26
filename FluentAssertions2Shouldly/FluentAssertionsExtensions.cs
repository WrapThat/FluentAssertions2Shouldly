using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions2Shouldly.Assertions;
using Shouldly;

namespace FluentAssertions2Shouldly;

public static class StringExtensions
{
    public static StringAssertions Should(this string value)
    {
        return new StringAssertions(value);
    }
}

public static class NumericExtensions
{
    public static NumericAssertions<T> Should<T>(this T value) where T : struct, IComparable<T>
    {
        return new NumericAssertions<T>(value);
    }

    public static NullableNumericAssertions<T> Should<T>(this T? value) where T : struct, IComparable<T>
    {
        return new NullableNumericAssertions<T>(value);
    }
}

public static class CollectionExtensions
{
    public static CollectionAssertions<T> Should<T>(this IEnumerable<T> value)
    {
        return new CollectionAssertions<T>(value);
    }

    public static CollectionAssertions<T> Should<T>(this T[] value)
    {
        return new CollectionAssertions<T>(value);
    }

    public static CollectionAssertions<T> Should<T>(this List<T> value)
    {
        return new CollectionAssertions<T>(value);
    }

    public static ObjectAssertions<IReadOnlyList<T>> Should<T>(this IReadOnlyList<T> value)
    {
        return new ObjectAssertions<IReadOnlyList<T>>(value);
    }
}

public static class ExceptionExtensions
{
    public static ExceptionAssertions<T> Should<T>(this T value) where T : Exception
    {
        return new ExceptionAssertions<T>(value);
    }
}

public static class ActionExtensions
{
    public static ActionAssertions Should(this Action value)
    {
        return new ActionAssertions(value);
    }

    public static ActionAssertions ThrowExactly<T>(this Action action) where T : Exception
    {
        return new ActionAssertions(action).ThrowExactly<T>();
    }

    public static ActionAssertions NotThrow(this Action action)
    {
        return new ActionAssertions(action).NotThrow();
    }
}

public static class FileExtensions
{
    public static FileAssertions Should(this FileInfo value)
    {
        return new FileAssertions(value);
    }
}

public static class PropertyChangeExtensions
{
    public static PropertyChangeAssertions<T> MonitorPropertyChanges<T>(this T value) where T : class, INotifyPropertyChanged
    {
        return new PropertyChangeAssertions<T>(value, string.Empty);
    }

    public static PropertyChangeAssertions<T> RaisePropertyChangeFor<T>(this T value, string propertyName) where T : class, INotifyPropertyChanged
    {
        return new PropertyChangeAssertions<T>(value, propertyName);
    }

    public static PropertyChangeAssertions<T> RaisePropertyChangeFor<T>(this ObjectAssertions<T> assertions, string propertyName) where T : class, INotifyPropertyChanged
    {
        return new PropertyChangeAssertions<T>(assertions.Subject, propertyName);
    }
}

public static class AssertionExtensions
{
    public static T WithMessage<T>(this T value, string message)
    {
        return value;
    }
}

public static class ObjectExtensions
{
    public static ObjectAssertions<T> Should<T>(this T value) where T : class
    {
        return new ObjectAssertions<T>(value);
    }
}

public static class BooleanExtensions
{
    public static BooleanAssertions Should(this bool value)
    {
        return new BooleanAssertions(value);
    }
}

public static class DateTimeExtensions
{
    public static DateTimeAssertions Should(this DateTime value)
    {
        return new DateTimeAssertions(value);
    }
}

public static class DictionaryExtensions
{
    public static DictionaryAssertions<TKey, TValue> Should<TKey, TValue>(this IDictionary<TKey, TValue> actual) 
        where TKey : notnull
    {
        return new DictionaryAssertions<TKey, TValue>(actual);
    }

    public static DictionaryAssertions<TKey, TValue> Should<TKey, TValue>(this Dictionary<TKey, TValue> value)
        where TKey : notnull
    {
        return new DictionaryAssertions<TKey, TValue>(value);
    }
}

public static class TypeExtensions
{
    public static TypeAssertions Should(this Type value)
    {
        return new TypeAssertions(value);
    }
}

public static class TaskExtensions
{
    public static TaskAssertions Should(this Task task)
    {
        return new TaskAssertions(task);
    }

    public static Task<AsyncActionAssertions> Should(this Func<Task> taskFunc)
    {
        return Task.FromResult(new AsyncActionAssertions(taskFunc));
    }

    public static Task<AsyncActionAssertions> Invoking(this Task task, Func<Task, Task> action)
    {
        return Task.FromResult(new AsyncActionAssertions(async () => await action(task)));
    }

    public static Task<AndConstraint<AsyncActionAssertions>> ThrowAsync<T>(this Task<AsyncActionAssertions> assertions) where T : Exception
    {
        return assertions.ContinueWith(t => new AndConstraint<AsyncActionAssertions>(t.Result));
    }

    public static Task<AndConstraint<AsyncActionAssertions>> ThrowAsync<T>(this ObjectAssertions<Task<AsyncActionAssertions>> assertions) where T : Exception
    {
        if (assertions.Subject == null)
            throw new ArgumentNullException(nameof(assertions.Subject));

        return assertions.Subject.ThrowAsync<T>();
    }
}

public static class EnumExtensions
{
    public static EnumAssertions<T> Should<T>(this T value) where T : struct, Enum
    {
        return new EnumAssertions<T>(value);
    }
}

public static class RecordStructExtensions
{
    public static dynamic Should(this object? value)
    {
        if (value == null)
        {
            var objectAssertionsType = typeof(ObjectAssertions<>).MakeGenericType(typeof(object));
            return Activator.CreateInstance(objectAssertionsType, value);
        }

        var type = value.GetType();
        if (type.IsEnum)
        {
            var enumAssertionsType = typeof(EnumAssertions<>).MakeGenericType(type);
            return Activator.CreateInstance(enumAssertionsType, value);
        }

        if (!type.IsValueType)
        {
            var objectAssertionsType = typeof(ObjectAssertions<>).MakeGenericType(type);
            return Activator.CreateInstance(objectAssertionsType, value);
        }

        var recordStructAssertionsType = typeof(RecordStructAssertions<>).MakeGenericType(type);
        return Activator.CreateInstance(recordStructAssertionsType, value);
    }
}

public class AndConstraint<T> where T : class
{
    private readonly T _value;
    public T And => _value;

    public AndConstraint(T value)
    {
        _value = value;
    }
}

public static class Int16Extensions
{
    public static Int16Assertions Should(this short value)
    {
        return new Int16Assertions(value);
    }
}

public static class Int32Extensions
{
    public static Int32Assertions Should(this int value)
    {
        return new Int32Assertions(value);
    }
}

public static class Int64Extensions
{
    public static Int64Assertions Should(this long value)
    {
        return new Int64Assertions(value);
    }
}

public static class SingleExtensions
{
    public static SingleAssertions Should(this float value)
    {
        return new SingleAssertions(value);
    }
}

public static class DoubleExtensions
{
    public static DoubleAssertions Should(this double value)
    {
        return new DoubleAssertions(value);
    }
}

public static class DecimalExtensions
{
    public static DecimalAssertions Should(this decimal value)
    {
        return new DecimalAssertions(value);
    }
} 