using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;

namespace FluentAssertions2Shouldly
{
    public class AsyncActionAssertions
    {
        private readonly Func<Task> _action;

        public AsyncActionAssertions(Func<Task> action)
        {
            _action = action;
        }

        public AndConstraint<AsyncActionAssertions> And => new AndConstraint<AsyncActionAssertions>(this);

        public async Task<AndConstraint<AsyncActionAssertions>> NotThrowAsync()
        {
            try
            {
                await _action();
                return new AndConstraint<AsyncActionAssertions>(this);
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException($"Expected no exception but got {ex.GetType().Name}", ex);
            }
        }

        public async Task<AndConstraint<AsyncActionAssertions>> ThrowAsync<T>() where T : Exception
        {
            try
            {
                await _action();
                throw new ShouldAssertException($"Expected {typeof(T).Name} but no exception was thrown");
            }
            catch (T)
            {
                return new AndConstraint<AsyncActionAssertions>(this);
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException($"Expected {typeof(T).Name} but got {ex.GetType().Name}", ex);
            }
        }

        public async Task<AndConstraint<AsyncActionAssertions>> CompleteWithinAsync(TimeSpan timeout)
        {
            using var cts = new CancellationTokenSource();
            var timeoutTask = Task.Delay(timeout, cts.Token);
            
            try
            {
                var task = _action();
                var completedTask = await Task.WhenAny(task, timeoutTask);
                
                if (completedTask == timeoutTask)
                {
                    throw new ShouldCompleteInException(
                        $"Task should complete within {timeout.TotalSeconds} seconds",
                        new ShouldlyTimeoutException($"Task did not complete within {timeout.TotalSeconds} seconds", null));
                }
                
                cts.Cancel(); // Cancel the timeout task
                await task; // Propagate any exceptions from the original task
                return new AndConstraint<AsyncActionAssertions>(this);
            }
            catch (ShouldCompleteInException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ShouldAssertException($"Task failed with exception: {ex.Message}", ex);
            }
        }
    }
} 