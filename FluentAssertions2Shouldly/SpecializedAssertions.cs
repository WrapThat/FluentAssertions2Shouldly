using FluentAssertions;
using FluentAssertions.Specialized;
using Shouldly;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FluentAssertions2Shouldly
{
    public static class SpecializedAssertions
    {
        // File assertions
        public static FileAssertions Should(this FileInfo instance)
        {
            return new FileAssertions(instance);
        }

        public static void Exist(this FileAssertions assertions)
        {
            File.Exists(assertions.Subject.FullName).ShouldBeTrue();
        }

        // Task assertions
        public static AsyncFunctionAssertions Should(this Func<Task> instance)
        {
            return new AsyncFunctionAssertions(instance);
        }

        public static async Task CompleteWithinAsync(this AsyncFunctionAssertions assertions, TimeSpan timeSpan)
        {
            await Should.CompleteInAsync(assertions.Subject, timeSpan);
        }

        public static async Task ThrowAsync<T>(this AsyncFunctionAssertions assertions) where T : Exception
        {
            await Should.ThrowAsync<T>(assertions.Subject);
        }

        // Approximate equality assertions
        public static NumericAssertions<float> Should(this float instance)
        {
            return new NumericAssertions<float>(instance);
        }

        public static NumericAssertions<double> Should(this double instance)
        {
            return new NumericAssertions<double>(instance);
        }

        public static void BeApproximately(this NumericAssertions<float> assertions, float expectedValue, float precision)
        {
            assertions.Subject.ShouldBe(expectedValue, precision);
        }

        public static void BeApproximately(this NumericAssertions<double> assertions, double expectedValue, double precision)
        {
            assertions.Subject.ShouldBe(expectedValue, precision);
        }

        // Enum assertions
        public static EnumAssertions<T> Should<T>(this T instance) where T : Enum
        {
            return new EnumAssertions<T>(instance);
        }

        public static void HaveFlag<T>(this EnumAssertions<T> assertions, T expectedFlag) where T : Enum
        {
            assertions.Subject.HasFlag(expectedFlag).ShouldBeTrue();
        }

        // Delegate assertions
        public static DelegateAssertions Should(this Action instance)
        {
            return new DelegateAssertions(instance);
        }

        public static void NotThrow(this DelegateAssertions assertions)
        {
            Should.NotThrow(assertions.Subject);
        }

        // Property change assertions
        public static PropertyChangeAssertions Should(this object instance)
        {
            return new PropertyChangeAssertions(instance);
        }

        public static void RaisePropertyChangeFor<T>(this PropertyChangeAssertions assertions, string propertyName)
        {
            var notifier = assertions.Subject as System.ComponentModel.INotifyPropertyChanged;
            if (notifier == null)
                throw new ArgumentException("Object must implement INotifyPropertyChanged");

            bool changed = false;
            notifier.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == propertyName)
                    changed = true;
            };

            // Trigger the property change
            var property = assertions.Subject.GetType().GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException($"Property {propertyName} not found");

            var currentValue = property.GetValue(assertions.Subject);
            property.SetValue(assertions.Subject, currentValue);

            changed.ShouldBeTrue();
        }
    }
} 