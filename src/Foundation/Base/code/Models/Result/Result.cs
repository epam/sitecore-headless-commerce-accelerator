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

namespace HCA.Foundation.Base.Models.Result
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Result<T>
        where T : class
    {
        public Result()
        {
        }

        public Result(T result, IList<string> errorMessages = null)
        {
            this.Data = result;
            if (errorMessages != null && errorMessages.Any())
            {
                this.SetErrors(errorMessages);
            }
        }

        public bool Success { get; set; } = true;

        public T Data { get; private set; }

        public IList<string> Errors { get; set; } = new List<string>();

        public void SetError(string error)
        {
            if (string.IsNullOrEmpty(error))
            {
                return;
            }

            this.Success = false;
            this.Errors.Add(error);
        }

        public void SetErrors(string area, Exception exception)
        {
            this.SetError($"{area}: {exception.Message}");
        }

        public void SetErrors(IList<string> errors)
        {
            if (!errors.Any())
            {
                return;
            }

            foreach (var error in errors)
            {
                this.SetError(error);
            }
        }

        public void SetResult(T result)
        {
            this.Data = result;
        }
    }
}