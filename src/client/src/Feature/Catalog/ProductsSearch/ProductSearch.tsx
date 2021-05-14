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

import React, { ChangeEvent, useCallback, useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useLocation } from 'react-router-dom';

import { LoadingStatus } from 'Foundation/Integration';
import { ChangeRoute as changeRoute } from 'Foundation/ReactJss/SitecoreContext';

import { SidebarSearch } from 'components';
import { KEYWORD_PARAMETER_NAME, productSearchParams, productsSearchStatus } from 'services/productsSearch';

import { buildQueryString, getKeywordFromSearch } from './utils';

export const ProductsSearch = () => {
  const dispatch = useDispatch();

  const params = useSelector(productSearchParams);
  const status = useSelector(productsSearchStatus);

  const { search, pathname } = useLocation();

  const [value, setValue] = useState('');
  const [isSearchApplied, setIsSearchApplied] = useState(false);

  const loading = status === LoadingStatus.Loading;

  useEffect(() => {
    const queryValue = getKeywordFromSearch(search);

    if (!search) {
      dispatch(changeRoute(`${pathname}?${KEYWORD_PARAMETER_NAME}=`));
    }

    if (queryValue.length > 0) {
      setValue(queryValue);
      setIsSearchApplied(true);
    }
  }, [search]);

  const onChange = useCallback((e: ChangeEvent<HTMLInputElement>) => {
    setValue(e.target.value);
    setIsSearchApplied(false);
  }, []);

  const onClear = useCallback(() => {
    setValue('');
    setIsSearchApplied(true);

    const newSearchQueryString = buildQueryString('', params);

    dispatch(changeRoute(`${pathname}${newSearchQueryString}`));
  }, [isSearchApplied, pathname, params]);

  const onSubmit = useCallback(() => {
    if (!isSearchApplied) {
      const newSearchQueryString = buildQueryString(value, params);

      setIsSearchApplied(true);
      dispatch(changeRoute(`${pathname}${newSearchQueryString}`));
    }
  }, [dispatch, params, pathname, value]);

  return <SidebarSearch disabled={loading} value={value} onChange={onChange} onClear={onClear} onSubmit={onSubmit} />;
};
