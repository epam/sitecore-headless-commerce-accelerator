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

export enum JssDataApiType {
  Connected = 'Connected',
  Disconnected = 'Disconnected',
}

export interface OptionParameters {
  querystringParams?: {
    [key: string]: string;
  };
  [key: string]: any;
}

export interface JssDataApi {
  getRouteData: (route: string, language: string, options?: OptionParameters) => Promise<any>;
  getPlaceholderData: (
    placeholderName: string,
    route: string,
    language: string,
    options?: OptionParameters,
  ) => Promise<any>;
}
