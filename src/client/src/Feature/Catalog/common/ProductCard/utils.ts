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

import { get } from 'lodash';

import { Product } from 'Feature/Catalog/Integration/ProductsSearch';

export const getColorVariants = (product: Product, productColors: Record<string, string>) =>
  product.variants.filter((x, i) => {
    const propertyColor = get(x, ['properties', 'color']);
    const isItColor = get(productColors, [propertyColor]);

    return !!isItColor;
  });

export const getInitialVariant = (product: Product, productColors: Record<string, string>) => {
  const { variants } = product;
  const productColorVariants = getColorVariants(product, productColors);

  return productColorVariants.length !== 0 ? productColorVariants[0] : variants[0];
};
