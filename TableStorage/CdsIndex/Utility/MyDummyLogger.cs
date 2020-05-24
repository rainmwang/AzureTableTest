using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorage.CdsIndex.Utility
{
    class MyDummyLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Console.WriteLine(state);
        }

        public void LogInformation(string message)
        {
            Console.WriteLine(message);
        }

        public void LogError(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
