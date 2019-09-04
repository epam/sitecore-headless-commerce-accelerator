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

import { FACET_NAME_VALUE_SEPARATOR, FACET_SEPARATOR, FACET_VALUE_SEPARATOR } from './constants';
import { AppliedFacets } from './models';

export class FacetsManager {
  private facets: AppliedFacets;

  public static parseFacets(facetString?: string) {
    if (facetString) {
      try {
        const decodedFacetString = decodeURIComponent(facetString);
        const facets = decodedFacetString.split(FACET_SEPARATOR);

        return facets.reduce((acc, facet) => {
          const [name, values] = facet.split(FACET_NAME_VALUE_SEPARATOR);

          acc[name] = values.split(FACET_VALUE_SEPARATOR);
          return acc;
          // tslint:disable-next-line:align
        }, {});
      } catch (e) {
        //
      }
    }

    return {};
  }

  public static facetsToString(facets: AppliedFacets) {
    return Object.keys(facets).reduce((acc, name) => {
      const facet = facets[name];
      if (!facet) {
        return acc;
      }

      const current = `${name}=${facet.join('|')}`;
      const result = acc ? `${acc}&${current}` : current;
      return encodeURIComponent(result);
      // tslint:disable-next-line:align
    }, '');
  }

  constructor(facets: AppliedFacets) {
    this.facets = facets;
  }

  public addFacet(name: string, value: string) {
    if (this.facets[name]) {
      this.facets[name].push(value);
    } else {
      this.facets[name] = [value];
    }

    return this;
  }

  public removeFacet(name: string, value: string) {
    if (this.facets[name]) {
      const index = this.facets[name].indexOf(value);
      this.facets[name].splice(index, 1);

      if (this.facets[name].length === 0) {
        delete this.facets[name];
      }
    }

    return this;
  }

  public getFacets() {
    return this.facets;
  }

  public getFacetsQueryString() {
    return FacetsManager.facetsToString(this.facets);
  }
}

export const facetsManager = (facetsString: string) => {
  const facets = FacetsManager.parseFacets(facetsString);

  return new FacetsManager(facets);
};
