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
    using Models.Logging;

    public interface ILogService<TLog>
        where TLog : Log, new()
    {
        /// <summary>
        /// Log Debug level message
        /// </summary>
        /// <param name="message">Log message.</param>
        void Debug(string message);

        /// <summary>
        /// Log Error level message
        /// </summary>
        /// <param name="message">Log message.</param>
        void Error(string message);

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
    }
}