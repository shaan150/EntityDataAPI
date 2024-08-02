namespace EntityDataAPI.Utils;

public static class RetryHelper
{
    public static async Task RetryOnExceptionAsync<TException>(
        Func<Task> operation,
        int maxRetries = 3,
        TimeSpan? initialDelay = null,
        double multiplier = 2.0,
        TimeSpan? maxDelay = null) where TException : Exception
    {
        initialDelay ??= TimeSpan.FromSeconds(1);
        maxDelay ??= TimeSpan.FromSeconds(30);

        int retryCount = 0;
        TimeSpan delay = initialDelay.Value;

        while (true)
        {
            try
            {
                await operation();
                return;
            }
            catch (TException ex) when (retryCount < maxRetries)
            {
                retryCount++;
                await Task.Delay(delay);

                delay = TimeSpan.FromMilliseconds(Math.Min(delay.TotalMilliseconds * multiplier, maxDelay.Value.TotalMilliseconds));
            }
        }
    }
}
