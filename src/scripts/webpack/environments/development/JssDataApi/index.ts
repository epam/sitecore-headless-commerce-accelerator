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

/**
 * This JssDataApi implementation is used for development in disconnected mode by using a presaved sitecore context in json file
 * @author EPAM Systems
 */
import fetch from 'isomorphic-fetch';

import { JssDataApi, JssDataApiType } from '../../../../../Foundation/ReactJss/client/api/JssDataApi/models';

export default class DataApi implements JssDataApi {
  public getType() {
    return JssDataApiType.Disconnected;
  }

  public async getRouteData(route: string, language: string, options: any = {}) {
    // use JssDataApi as route prefix, because content is resolved relative to the development folder
    const response = await fetch('./JssDataApi/sitecore-context.json');

    if (!response.ok) {
      throw new Error(`${response.status} ${response.statusText}`);
    }

    return response.json();
  }

  public async getViewBagAsync() {
    const response = await fetch('/viewBag.json');

    if (!response.ok) {
      throw new Error(`${response.status} ${response.text}`);
    }

    return response.json();
  }

  public async getPlaceholderData(placeholderName: string, route: string, language: string, options: any = {}) {
    throw new Error('not implemented');
  }

  public async getItemData(itemPath: string, language: string, options: any = {}) {
    throw new Error('not implemented');
  }
}
