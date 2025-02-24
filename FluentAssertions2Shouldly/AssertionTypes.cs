using System;
using System.IO;

namespace FluentAssertions2Shouldly
{
    public class FileAssertions
    {
        public FileInfo Subject { get; }

        public FileAssertions(FileInfo subject)
        {
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
        }
    }

    public class AsyncFunctionAssertions
    {
        public Func<System.Threading.Tasks.Task> Subject { get; }

        public AsyncFunctionAssertions(Func<System.Threading.Tasks.Task> subject)
        {
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
        }
    }

    public class NumericAssertions<T> where T : struct, IComparable<T>
    {
        public T Subject { get; }

        public NumericAssertions(T subject)
        {
            Subject = subject;
        }
    }

    public class EnumAssertions<T> where T : Enum
    {
        public T Subject { get; }

        public EnumAssertions(T subject)
        {
            Subject = subject;
        }
    }

    public class DelegateAssertions
    {
        public Action Subject { get; }

        public DelegateAssertions(Action subject)
        {
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
        }
    }

    public class PropertyChangeAssertions
    {
        public object Subject { get; }

        public PropertyChangeAssertions(object subject)
        {
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
        }
    }
} 