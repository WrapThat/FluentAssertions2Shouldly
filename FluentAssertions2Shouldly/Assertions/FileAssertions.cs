using System;
using System.IO;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class FileAssertions
    {
        public FileInfo Subject { get; }

        public FileAssertions(FileInfo value)
        {
            Subject = value;
        }

        public AndConstraint<FileAssertions> And => new AndConstraint<FileAssertions>(this);

        public FileAssertions Exist()
        {
            Subject.Refresh();
            Subject.Exists.ShouldBeTrue($"Expected file {Subject.FullName} to exist");
            return this;
        }

        public FileAssertions NotExist()
        {
            Subject.Refresh();
            Subject.Exists.ShouldBeFalse($"Expected file {Subject.FullName} to not exist");
            return this;
        }

        public FileAssertions HaveExtension(string expected)
        {
            Subject.Extension.ShouldBe(expected, $"Expected file to have extension {expected} but found {Subject.Extension}");
            return this;
        }

        public FileAssertions HaveLength(long expected)
        {
            Subject.Refresh();
            Subject.Length.ShouldBe(expected, $"Expected file to have length {expected} bytes but found {Subject.Length} bytes");
            return this;
        }

        public FileAssertions BeWritable()
        {
            Subject.Refresh();
            Subject.IsReadOnly.ShouldBeFalse($"Expected file {Subject.FullName} to be writable");
            return this;
        }

        public FileAssertions BeReadOnly()
        {
            Subject.Refresh();
            Subject.IsReadOnly.ShouldBeTrue($"Expected file {Subject.FullName} to be read-only");
            return this;
        }

        public FileAssertions HaveAccessTimes(DateTime? creation = null, DateTime? lastWrite = null, DateTime? lastAccess = null)
        {
            Subject.Refresh();
            if (creation.HasValue)
            {
                Subject.CreationTime.ShouldBe(creation.Value);
            }
            if (lastWrite.HasValue)
            {
                Subject.LastWriteTime.ShouldBe(lastWrite.Value);
            }
            if (lastAccess.HasValue)
            {
                Subject.LastAccessTime.ShouldBe(lastAccess.Value);
            }
            return this;
        }

        public FileAssertions HaveAttributes(FileAttributes expected)
        {
            Subject.Refresh();
            Subject.Attributes.ShouldBe(expected);
            return this;
        }
    }
} 