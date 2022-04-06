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

/* global __SC_API_HOST__ */

import { dataApi } from '@sitecore-jss/sitecore-jss-react';

const { fetchRouteData, fetchPlaceholderData } = dataApi;

import dataFetcher from './dataFetcher';
import { JssDataApi, OptionParameters } from './models';

export default class DataApi implements JssDataApi {
  private apiKey: string;
  private host: string;

  constructor(apiKey: string, host: string = '') {
    this.apiKey = apiKey;
    this.host = host;
  }

  public async getRouteData(route: string, language: string, options: OptionParameters = {}) {
    const fetchOptions = this.getFetchOptions(language, options);
    return fetchRouteData(route, fetchOptions);
  }

  public async getPlaceholderData(
    placeholderName: string,
    route: string,
    language: string,
    options: OptionParameters = {},
  ) {
    const fetchOptions = this.getFetchOptions(language, options);
    return fetchPlaceholderData(placeholderName, route, fetchOptions);
  }

  private getFetchOptions(language: string, options: OptionParameters = {}) {
    const querystringParams = options.querystringParams || {};

    if (language) {
      querystringParams.sc_lang = language;
    }

    querystringParams.sc_apikey = this.apiKey;

    return { host: this.host, querystringParams, fetcher: dataFetcher, ...options };
  }
}
