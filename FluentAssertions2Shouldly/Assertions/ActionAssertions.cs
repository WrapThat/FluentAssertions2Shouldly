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

        public AndConstraint<ActionAssertions> Throw<T>() where T : Exception
        {
            try
            {
                _subject();
            }
            catch (Exception ex)
            {
                if (ex is T)
                {
                    return new AndConstraint<ActionAssertions>(this);
                }
                throw new ShouldAssertException($"Expected {typeof(T).Name} but got {ex.GetType().Name}");
            }
            throw new ShouldAssertException($"Expected {typeof(T).Name} but no exception was thrown");
        }

        public AndConstraint<ActionAssertions> NotThrow<T>() where T : Exception
        {
            try
            {
                _subject();
                return new AndConstraint<ActionAssertions>(this);
            }
            catch (Exception ex)
            {
                if (ex is T)
                {
                    throw new ShouldAssertException($"Expected no {typeof(T).Name} but got one");
                }
                throw new ShouldAssertException($"Expected no {typeof(T).Name} but got {ex.GetType().Name}");
            }
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