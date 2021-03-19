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

import axios from 'axios';

import { ProductsSearchRequest, SortDirection } from 'Feature/Catalog/dataModel.Generated';
import { Facet, ProductSearchResults } from 'Foundation/Commerce/dataModel.Generated';
import { Result } from 'Foundation/Integration';
import { ProductSearchSuggestionResponse, SearchProductsParams } from './models';

const parseFacets = (facetsString: string): Facet[] => {
  const facets: Facet[] = [];

  const facetString = facetsString.split('&');
  for (const facet of facetString) {
    const pair = facet.split('=');
    facets.push({
      displayName: null,
      foundValues: [],
      name: decodeURIComponent(pair[0]),
      values: [decodeURIComponent(pair[1])],
    });
  }
  return facets;
};

const mapToProductSearchRequest = (params: SearchProductsParams): ProductsSearchRequest => {
  return {
    categoryId: params.cci,
    facets: params.f ? parseFacets(params.f) : [],
    pageNumber: params.pg,
    pageSize: +params.ps,
    searchKeyword: params.q ? params.q : '',
    sortDirection:
      !params.sd || params.sd === SortDirection.Asc.toLocaleString() ? SortDirection.Asc : SortDirection.Desc,
    sortField: params.s,
  };
};

export const searchProducts = async (params: SearchProductsParams): Promise<Result<ProductSearchResults>> => {
  try {
    params.pg = params.pg || 0;

    const request = mapToProductSearchRequest(params);
    const response = await axios.post<Result<ProductSearchResults>>('/apix/client/commerce/search/products', request);

    const { data } = response;
    if (!data.data || data.error) {
      return { error: new Error(data.error.message) };
    }
    return {
      data: {
        facets: data.data.facets,
        products: data.data.products,
        totalItemCount: data.data.totalItemCount,
        totalPageCount: data.data.totalPageCount,
      },
    };
  } catch (e) {
    return { error: e };
  }
};

export const requestSuggestions = async (search: string): Promise<Result<ProductSearchSuggestionResponse>> => {
  try {
    const response = await axios.get<Result<ProductSearchSuggestionResponse>>(
      `/apix/client/commerce/search/suggestions`,
      {
        params: {
          search,
        },
      },
    );

    const { data } = response;

    return {
      data: {
        products: data.data.products,
      },
    };
  } catch (e) {
    return { error: e };
  }
};
