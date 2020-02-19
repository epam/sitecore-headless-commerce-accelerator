namespace Wooli.Foundation.Base.Services.Logging
{
    using System.Diagnostics.CodeAnalysis;
    using log4net;
    using Sitecore.Diagnostics;
    using Log = Models.Logging.Log;

    [ExcludeFromCodeCoverage]
    public abstract class LogService<TLog> : ILogService<TLog> where TLog : Log, new()
    {
        protected readonly ILog Log;

        protected LogService()
        {
            var log = new TLog();
            this.Log = LogManager.GetLogger(log.Name);
        }

        public void Debug(string message)
        {
            Assert.ArgumentNotNull(message, nameof(message));
            this.Log.Debug(message);
        }

        public void Info(string message)
        {
            Assert.ArgumentNotNull(message, nameof(message));
            this.Log.Info(message);
        }

        public void Warn(string message)
        {
            Assert.ArgumentNotNull(message, nameof(message));
            this.Log.Warn(message);
        }

        public void Error(string message)
        {
            Assert.ArgumentNotNull(message, nameof(message));
            this.Log.Error(message);
        }
    }
}