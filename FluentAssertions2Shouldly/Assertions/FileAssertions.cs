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
    }
} 