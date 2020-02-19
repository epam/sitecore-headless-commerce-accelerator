namespace Wooli.Foundation.Base.Services.Logging
{
    using Models.Logging;

    public interface ILogService<TLog> where TLog : Log, new()
    {
        /// <summary>
        /// Log Debug level message
        /// </summary>
        /// <param name="message">Log message.</param>
        void Debug(string message);

        /// <summary>
        /// Log Info level message
        /// </summary>
        /// <param name="message">Log message.</param>
        void Info(string message);

        /// <summary>
        /// Log Warn level message
        /// </summary>
        /// <param name="message">Log message.</param>
        void Warn(string message);

        /// <summary>
        /// Log Error level message
        /// </summary>
        /// <param name="message">Log message.</param>
        void Error(string message);
    }
}
