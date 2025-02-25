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

        public async Task CompleteWithinAsync(TimeSpan timeout)
        {
            using var cts = new CancellationTokenSource(timeout);
            try
            {
                await _subject.WaitAsync(cts.Token);
            }
            catch (OperationCanceledException)
            {
                throw new ShouldCompleteInException(
                    $"Task should complete within {timeout.TotalSeconds} seconds",
                    new ShouldlyTimeoutException($"Task did not complete within {timeout.TotalSeconds} seconds", null));
            }
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
    }
} 