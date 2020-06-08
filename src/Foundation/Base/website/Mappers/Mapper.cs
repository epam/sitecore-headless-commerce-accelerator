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

namespace HCA.Foundation.Base.Mappers
{
    using Sitecore.Diagnostics;

    public abstract class Mapper : IMapper
    {
        protected AutoMapper.IMapper InnerMapper { get; set; }

        public TResult Map<TSource, TResult>(TSource source)
        {
            Assert.IsNotNull(this.InnerMapper, "InnerMapper must be initialized");
            Assert.ArgumentNotNull(source, nameof(source));

            return this.InnerMapper.Map<TSource, TResult>(source);
        }
    }
}