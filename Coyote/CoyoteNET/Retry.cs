using System;
using System.Collections.Generic;
using System.Threading;

namespace CoyoteNET
{
    internal class Retry
    {
        public static void Do(Action action, TimeSpan interval, int maxAttempts)
        {
            var exceptions = new List<Exception>();
            while (true)
            {
                try
                {
                    action();
                    break;
                }
                catch (Exception ex)
                {
                    maxAttempts--;
                    if (maxAttempts > 0)
                    {
                        exceptions.Add(ex);
                        Console.WriteLine($"{maxAttempts} attempts left because of: {ex.Message}");
                        Thread.Sleep(interval);

                    }
                    else
                    {
                        throw new AggregateException(exceptions);
                    }
                }
            }
        }
    }
}