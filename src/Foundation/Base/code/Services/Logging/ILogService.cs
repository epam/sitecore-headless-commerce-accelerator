namespace Wooli.Foundation.Base.Services.Logging
{
    using Models.Logging;

    public interface ILogService<TLog> where TLog : Log, new()
    {
        /// <summary>
        /// CommonLog Debug level message
        /// </summary>
        /// <param name="message">CommonLog message.</param>
        void Debug(string message);

        /// <summary>
        /// CommonLog Info level message
        /// </summary>
        /// <param name="message">CommonLog message.</param>
        void Info(string message);

        /// <summary>
        /// CommonLog Warn level message
        /// </summary>
        /// <param name="message">CommonLog message.</param>
        void Warn(string message);

        /// <summary>
        /// CommonLog Error level message
        /// </summary>
        /// <param name="message">CommonLog message.</param>
        void Error(string message);
    }
}
