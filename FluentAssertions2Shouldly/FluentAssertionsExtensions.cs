using System.ComponentModel;

namespace FluentAssertions2Shouldly;

public static class FluentAssertionsExtensions
{
    public static StringAssertions Should(this string value)
    {
        return new StringAssertions(value);
    }

    public static NumericAssertions<T> Should<T>(this T value) where T : struct
    {
        return new NumericAssertions<T>(value);
    }

    public static CollectionAssertions<T> Should<T>(this IEnumerable<T> value)
    {
        return new CollectionAssertions<T>(value);
    }

    public static ActionAssertions Should(this Action value)
    {
        return new ActionAssertions(value);
    }

    public static AsyncActionAssertions Should(this Func<Task> value)
    {
        return new AsyncActionAssertions(value);
    }

    public static FileAssertions Should(this FileInfo value)
    {
        return new FileAssertions(value);
    }

    public static PropertyChangeAssertions<T> Should<T>(this T value) where T : INotifyPropertyChanged
    {
        return new PropertyChangeAssertions<T>(value, null);
    }

    public static PropertyChangeAssertions<T> RaisePropertyChangeFor<T>(this PropertyChangeAssertions<T> assertions, string propertyName) where T : INotifyPropertyChanged
    {
        assertions.RaisePropertyChangeFor<string>(propertyName);
        return assertions;
    }

    public static AndConstraint<T> And<T>(this T assertions) where T : class
    {
        return new AndConstraint<T>(assertions);
    }
}

public class AndConstraint<T> where T : class
{
    public T And { get; }

    public AndConstraint(T value)
    {
        And = value;
    }
} 