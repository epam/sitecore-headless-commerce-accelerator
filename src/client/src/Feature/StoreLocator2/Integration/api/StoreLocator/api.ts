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

import axios, { AxiosError } from 'axios';

import { Result } from 'Foundation/Integration';

import { GetCoordinatesResults, GetStoresResponse } from './models';

import { Store } from 'Feature/StoreLocator2/models';

const routeBase = '/apix/client/commerce';

export const findStores = async (latitude: number, longitude: number, radius: number): Promise<Result<Store[]>> =>
  axios
    .get<GetStoresResponse>(`${routeBase}/storeLocator/search`, {
      params: {
        lat: latitude,
        lng: longitude,
        radius,
      },
    })
    .then((response) => ({ data: response.data.data }))
    .catch((error: AxiosError) => ({ error }));

export const getCoordinates = async (zipCode: string, countryCode: string): Promise<Result<GetCoordinatesResults>> =>
  axios
    .get<GetCoordinatesResults>(`https://eu1.locationiq.com/v1/search.php`, {
      params: {
        countrycodes: countryCode,
        format: 'json',
        key: 'edef05bf60301c',
        postalcode: zipCode,
      },
    })
    .then((response) => ({ data: response.data }))
    .catch((error: AxiosError) => ({ error }));

export const getStores = async (): Promise<Result<Store[]>> =>
  axios
    .get<GetStoresResponse>(`${routeBase}/storeLocator/stores`)
    .then((response) => ({ data: response.data.data }))
    .catch((error: AxiosError) => ({ error }));
