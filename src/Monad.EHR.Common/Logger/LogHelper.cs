using Microsoft.Extensions.Logging;

namespace Monad.EHR.Common.Logger
{
    public static class LogHelper
    {
        public static ILogger CreateLogger<T>()
        {
            var factory = new LoggerFactory();
            var logger = factory.CreateLogger(typeof(T).FullName);
            factory.AddConsole();
            factory.AddConsole((category, logLevel) => logLevel >= LogLevel.Critical && category.Equals(typeof(T).FullName));
            return logger;
        }
    }
}
