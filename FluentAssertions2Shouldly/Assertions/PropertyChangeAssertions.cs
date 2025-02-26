using System;
using System.ComponentModel;
using System.Threading;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class PropertyChangeAssertions<T> where T : class, INotifyPropertyChanged
    {
        private readonly T Subject;
        private readonly string _propertyName;
        private string? _raisedPropertyName;
        private bool _eventRaised;

        public PropertyChangeAssertions(T subject, string propertyName)
        {
            Subject = subject;
            _propertyName = propertyName;
            Subject.PropertyChanged += OnPropertyChanged;
        }

        public AndConstraint<PropertyChangeAssertions<T>> And => new AndConstraint<PropertyChangeAssertions<T>>(this);

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            _raisedPropertyName = e.PropertyName;
            _eventRaised = true;
        }

        public PropertyChangeAssertions<T> RaisePropertyChangeFor(string propertyName)
        {
            if (_raisedPropertyName != propertyName)
            {
                throw new ShouldAssertException($"Expected property change for '{propertyName}' but got '{_raisedPropertyName ?? string.Empty}'");
            }
            return this;
        }

        public PropertyChangeAssertions<T> NotRaisePropertyChangeFor(string propertyName)
        {
            if (_raisedPropertyName == propertyName)
            {
                throw new ShouldAssertException($"Expected no property change for '{propertyName}' but got one");
            }
            return this;
        }

        public PropertyChangeAssertions<T> RaisePropertyChangeFor(string propertyName, Action action)
        {
            _eventRaised = false;
            action();
            if (!_eventRaised || _raisedPropertyName != propertyName)
            {
                throw new ShouldAssertException($"Expected property change for '{propertyName}' but got '{_raisedPropertyName ?? string.Empty}'");
            }
            return this;
        }

        public PropertyChangeAssertions<T> RaisePropertyChangeFor(string propertyName, Action action, TimeSpan timeout)
        {
            _eventRaised = false;
            using var resetEvent = new ManualResetEventSlim(false);
            
            void Handler(object? s, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == propertyName)
                {
                    resetEvent.Set();
                }
            }

            Subject.PropertyChanged += Handler;
            try
            {
                action();
                if (!resetEvent.Wait(timeout))
                {
                    throw new ShouldAssertException($"Expected property change for '{propertyName}' within {timeout.TotalSeconds} seconds but got none");
                }
            }
            finally
            {
                Subject.PropertyChanged -= Handler;
            }
            return this;
        }

        public PropertyChangeAssertions<T> RaisePropertyChangeFor(string propertyName, Func<bool> predicate)
        {
            _eventRaised = false;
            while (!_eventRaised && predicate())
            {
                Thread.Sleep(10);
            }
            if (!_eventRaised || _raisedPropertyName != propertyName)
            {
                throw new ShouldAssertException($"Expected property change for '{propertyName}' but got '{_raisedPropertyName ?? string.Empty}'");
            }
            return this;
        }
    }
} 