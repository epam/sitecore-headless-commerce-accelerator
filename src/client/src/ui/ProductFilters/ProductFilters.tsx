//    Copyright 2021 EPAM Systems, Inc.
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

import React, { useCallback, FC, ChangeEvent, useMemo } from 'react';

import { useDispatch, useSelector } from 'react-redux';
import { useHistory } from 'react-router-dom';
import { get } from 'lodash';

import { tryParseUrlSearch } from 'utils';
import { LoadingStatus } from 'models';
import {
  FACET_PARAMETER_NAME,
  facetsManager,
  productSearchFacets,
  productSearchItems,
  productsSearchStatus,
  productsSearch,
  ApplyFacet as applyFacet,
  DiscardFacet as discardFacet,
} from 'services/search';

import { SidebarFilter } from 'components/SidebarFilter';

import { cnProductFilters } from './cn';
import './ProductFilters.scss';

export const ProductFilters: FC = () => {
  const dispatch = useDispatch();
  const history = useHistory();

  const search = get(history, ['location', 'search'], '');
  const items = useSelector(productSearchItems);
  const facets = useSelector(productSearchFacets);
  const status = useSelector(productsSearchStatus);
  const productSearch = useSelector(productsSearch);
  const { totalItemCount } = productSearch;
  const isLoading = status === LoadingStatus.Loading;

  const handleFacetOnChange = useCallback(
    (name: string, value: string, e: ChangeEvent<HTMLInputElement>) => {
      if (e.target.checked) {
        dispatch(applyFacet(name, value, search));
      } else {
        dispatch(discardFacet(name, value, search));
      }
    },
    [search],
  );

  const isApplied = useCallback(
    (name: string, value: string) => {
      const parsedSearch = tryParseUrlSearch(search);
      const appliedFacets = facetsManager(parsedSearch[FACET_PARAMETER_NAME]).getFacets();
      const facet = appliedFacets[name];

      if (!facet) {
        return false;
      }

      return facet.indexOf(value) !== -1;
    },
    [search],
  );

  const filteredFacets = useMemo(() => {
    if (facets[0] !== undefined) {
      facets[0].foundValues = facets[0].foundValues
        .filter((v) => v.aggregateCount !== 0)
        .sort((a, b) => {
          const nameA = a.name.toLowerCase();
          const nameB = b.name.toLowerCase();

          if (nameA < nameB) {
            return -1;
          }

          if (nameA > nameB) {
            return 1;
          }

          return 0;
        });
    }

    return facets;
  }, [facets]);

  return (
    <div className={cnProductFilters()}>
      <section className={cnProductFilters('ProductGridFilter')}>
        {isLoading && <div className={cnProductFilters('Overlay')} />}
        {items.length !== 0 &&
          totalItemCount !== 0 &&
          !isLoading &&
          filteredFacets.map((facet, index) => {
            return (
              <SidebarFilter key={index} facet={facet} isApplied={isApplied} onFacetChange={handleFacetOnChange} />
            );
          })}
      </section>
    </div>
  );
};
