using System;
using System.Threading.Tasks;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class ActionAssertions
    {
        private readonly Action _subject;

        public ActionAssertions(Action action)
        {
            _subject = action;
        }

        public AndConstraint<ActionAssertions> And => new AndConstraint<ActionAssertions>(this);

        public ActionAssertions ThrowExactly<T>() where T : Exception
        {
            Should.Throw<T>(_subject);
            return this;
        }

        public ActionAssertions NotThrow()
        {
            Should.NotThrow(_subject);
            return this;
        }

        public ActionAssertions Throw<T>() where T : Exception
        {
            Should.Throw<T>(_subject);
            return this;
        }

        public AndConstraint<ActionAssertions> ExecuteWithin(TimeSpan timeout)
        {
            var task = Task.Run(_subject);
            if (!task.Wait(timeout))
            {
                throw new ShouldAssertException($"Expected action to complete within {timeout.TotalMilliseconds}ms");
            }
            return new AndConstraint<ActionAssertions>(this);
        }
    }
} 