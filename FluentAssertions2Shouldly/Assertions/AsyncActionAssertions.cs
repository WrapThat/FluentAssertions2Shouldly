using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class AsyncActionAssertions : ObjectAssertions<Func<Task>>
    {
        private readonly Func<Task> _action;

        public AsyncActionAssertions(Func<Task> action) : base(action)
        {
            _action = action;
        }

        public Task<AndConstraint<AsyncActionAssertions>> ThrowAsync<T>() where T : Exception
        {
            return Task.FromResult(new AndConstraint<AsyncActionAssertions>(this));
        }

        public Task<AndConstraint<AsyncActionAssertions>> NotThrowAsync()
        {
            return Task.FromResult(new AndConstraint<AsyncActionAssertions>(this));
        }

        public Task<AndConstraint<AsyncActionAssertions>> CompleteWithinAsync(TimeSpan timeout)
        {
            using var cts = new CancellationTokenSource(timeout);
            try
            {
                return Task.FromResult(new AndConstraint<AsyncActionAssertions>(this));
            }
            catch (OperationCanceledException)
            {
                throw new ShouldAssertException($"Expected action to complete within {timeout.TotalMilliseconds}ms");
            }
        }
    }
} 