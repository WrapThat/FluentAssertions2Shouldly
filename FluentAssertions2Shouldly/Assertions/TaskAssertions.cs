using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class TaskAssertions
    {
        private readonly Task _subject;

        public TaskAssertions(Task value)
        {
            _subject = value;
        }

        public AndConstraint<TaskAssertions> And => new AndConstraint<TaskAssertions>(this);

        public async Task CompleteWithinAsync(TimeSpan timeout)
        {
            using var cts = new CancellationTokenSource();
            var timeoutTask = Task.Delay(timeout, cts.Token);
            var completedTask = await Task.WhenAny(_subject, timeoutTask);
            
            if (completedTask == timeoutTask)
            {
                throw new ShouldCompleteInException(
                    $"Task should complete within {timeout.TotalSeconds} seconds",
                    new ShouldlyTimeoutException($"Task did not complete within {timeout.TotalSeconds} seconds", null));
            }
            
            cts.Cancel(); // Cancel the timeout task
            await _subject; // Propagate any exceptions from the original task
        }

        public async Task<ExceptionAssertions<T>> ThrowAsync<T>() where T : Exception
        {
            var ex = await Should.ThrowAsync<T>(() => _subject);
            return new ExceptionAssertions<T>(ex);
        }

        public async Task NotThrowAsync()
        {
            try
            {
                await _subject;
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException($"Expected no exception but found {ex.GetType().Name}", ex);
            }
        }

        public TaskAssertions BeCompleted()
        {
            _subject.IsCompleted.ShouldBeTrue();
            return this;
        }

        public TaskAssertions NotBeCompleted()
        {
            _subject.IsCompleted.ShouldBeFalse();
            return this;
        }

        public TaskAssertions BeCanceled()
        {
            _subject.IsCanceled.ShouldBeTrue();
            return this;
        }

        public TaskAssertions NotBeCanceled()
        {
            _subject.IsCanceled.ShouldBeFalse();
            return this;
        }

        public TaskAssertions BeFaulted()
        {
            _subject.IsFaulted.ShouldBeTrue();
            return this;
        }

        public TaskAssertions NotBeFaulted()
        {
            _subject.IsFaulted.ShouldBeFalse();
            return this;
        }
    }

    public class TaskAssertions<T> : TaskAssertions
    {
        private readonly Task<T> _subject;

        public TaskAssertions(Task<T> value) : base(value)
        {
            _subject = value;
        }

        public new AndConstraint<TaskAssertions<T>> And => new AndConstraint<TaskAssertions<T>>(this);

        public async Task<T> Result()
        {
            return await _subject;
        }
    }
} 