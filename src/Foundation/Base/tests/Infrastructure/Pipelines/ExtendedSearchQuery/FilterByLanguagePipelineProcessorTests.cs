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


namespace HCA.Foundation.Base.Tests.Infrastructure.Pipelines.ExtendedSearchQuery
{
    using System;
    using System.Linq;

    using NSubstitute;

    using HCA.Foundation.Base.Infrastructure.Pipelines.ExtendedSearchQuery;

    using Xunit;
    using GraphQL.Types;
    using Sitecore.Data;
    using Sitecore.Services.GraphQL.Content.GraphTypes.ContentSearch;

    public class FilterByLanguagePipelineProcessorTests
    {
        private readonly ResolveFieldContext fieldContext;

        private IQueryable<ContentSearchResult> searchQueryable;

        private readonly Database database;

        public FilterByLanguagePipelineProcessorTests()
        {
            this.fieldContext = Substitute.For<ResolveFieldContext>();
            this.searchQueryable = Enumerable.Empty<ContentSearchResult>().AsQueryable();
            this.database = Substitute.For<Sitecore.Data.Database>();

        }

        [Fact]
        public void Process_NullArguments_ThrowNullArgumentException()
        {
            // arrange
            var processor = new FilterByLanguagePipelineProcessor();

            // act and assert
            Assert.Throws<ArgumentNullException>(() => processor.Process(null));
        }

        [Fact]
        public void Process_NotNullArguments_ResultSuccess()
        {
            // arrange
            var processor = new FilterByLanguagePipelineProcessor();
            var args = ExtendedSearchQueryHelper.GeneratePipelineArgs(this.searchQueryable, this.fieldContext, this.database);

            // act and assert
            // if an exception is not thrown = OK
            processor.Process(args);
        }
    }
}