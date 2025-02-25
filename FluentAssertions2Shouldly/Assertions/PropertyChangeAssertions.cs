using System.ComponentModel;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class PropertyChangeAssertions<T> where T : class, INotifyPropertyChanged
    {
        private readonly T Subject;
        private readonly string _propertyName;
        private string? _raisedPropertyName;

        public PropertyChangeAssertions(T subject, string propertyName)
        {
            Subject = subject;
            _propertyName = propertyName;
            Subject.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            _raisedPropertyName = e.PropertyName;
        }

        public PropertyChangeAssertions<T> RaisePropertyChangeFor(string propertyName)
        {
            if (_raisedPropertyName != propertyName)
            {
                throw new ShouldAssertException($"Expected property change for '{propertyName}' but got '{_raisedPropertyName ?? string.Empty}'");
            }
            return this;
        }
    }
} 