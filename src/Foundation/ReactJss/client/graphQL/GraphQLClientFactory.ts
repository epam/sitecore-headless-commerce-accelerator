//    Copyright 2019 EPAM Systems, Inc.
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

import { InMemoryCache, IntrospectionFragmentMatcher, NormalizedCacheObject } from 'apollo-cache-inmemory';
import { ApolloClient, ApolloClientOptions } from 'apollo-client';
import 'isomorphic-fetch';

/*
  INTROSPECTION DATA
  See https://www.apollographql.com/docs/react/recipes/fragment-matching.html
  This enables the Apollo cache to process fragments on interface types correctly.
  If this file does not exist, you may need to run the `jss graphql:update` script.
*/
import introspectionQueryResultData from './GraphQLFragmentTypes.json';

/*
  QUERY LINK SELECTION
  A link is transport which GraphQL queries are pushed across.
  You have many choices.
  See the apollo-link documentation for more details.

  NOTE: to use Sitecore Experience Editor it is essential that your
  link passes cookies along with requests (credentials: 'include').
*/

// choose between a basic HTTP link to run queries...
// import { createHttpLink } from 'apollo-link-http';
// const link = createHttpLink({ uri: endpoint, credentials: 'include' });

// ...or a batched link (multiple queries within 10ms all go in one HTTP request)
import { BatchHttpLink } from 'apollo-link-batch-http';

// ...and an automatic persisted query link, which reduces bandwidth by using query hashes to alias content
// the APQ link is _chained_ behind another link that performs the actual HTTP calls, so you can choose
// APQ + batched, or APQ + http links for example.
import { ApolloLink } from 'apollo-link';
import { createPersistedQueryLink } from 'apollo-link-persisted-queries';

export default function(endpoint: string = '', ssr: boolean = false, initialCacheState?: NormalizedCacheObject): ApolloClient<NormalizedCacheObject> {

  if (!endpoint) {
    endpoint = `/wooli/api/graph/?sc_apikey=${process.env.API_KEY || ''}`;
  }
  /* HTTP link selection: default to batched + APQ */
  const link: ApolloLink = createPersistedQueryLink().concat(
    new BatchHttpLink({ uri: endpoint, credentials: 'include' })
  );

  const cache = new InMemoryCache({
    fragmentMatcher: new IntrospectionFragmentMatcher({
      introspectionQueryResultData,
    }),
  });

  const options: ApolloClientOptions<NormalizedCacheObject> = {
    cache: cache.restore(initialCacheState),
    link,
    ssrForceFetchDelay: 100,
    ssrMode: ssr,
  };
  return new ApolloClient(options);
}
