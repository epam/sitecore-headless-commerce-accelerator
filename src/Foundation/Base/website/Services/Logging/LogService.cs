//    Copyright 2020 EPAM Systems, Inc.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

namespace HCA.Foundation.Base.Services.Logging
{
    using System.Diagnostics.CodeAnalysis;

    using log4net;

    using Sitecore.Diagnostics;

    using Log = Models.Logging.Log;

    [ExcludeFromCodeCoverage]
    public abstract class LogService<TLog> : ILogService<TLog>
        where TLog : Log, new()
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

        public void Error(string message)
        {
            Assert.ArgumentNotNull(message, nameof(message));
            this.Log.Error(message);
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
    }
}