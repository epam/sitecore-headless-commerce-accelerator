﻿//    Copyright 2020 EPAM Systems, Inc.
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

namespace Wooli.Foundation.Base.Services.Configuration
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using Models.Configuration;

    using Sitecore.Configuration;

    [ExcludeFromCodeCoverage]
    public abstract class ConfigurationProvider<TConfiguration> : IConfigurationProvider<TConfiguration>
        where TConfiguration : Configuration, new()
    {
        private readonly Lazy<TConfiguration> configuration;

        protected ConfigurationProvider()
        {
            this.configuration = new Lazy<TConfiguration>(this.Read, true);
        }

        protected abstract string Path { get; set; }

        public TConfiguration Get()
        {
            return this.configuration.Value;
        }

        private TConfiguration Read()
        {
            var xmlNode = Factory.GetConfigNode(this.Path);
            return xmlNode != null ? Factory.CreateObject<TConfiguration>(xmlNode) : null;
        }
    }
}