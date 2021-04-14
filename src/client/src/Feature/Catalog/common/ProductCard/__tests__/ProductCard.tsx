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

import React from 'react';
import { Provider } from 'react-redux';
import renderer from 'react-test-renderer';
import configureStore from 'redux-mock-store';

import { Product } from 'Feature/Catalog/Integration/ProductsSearch';

import { AddToCart, ColorVariants, Price, ProductCard, ProductName, ProductRating, ThumbnailSlider } from '../index';

const fallbackImageUrl = '/-/media/HCA/Project/HCA/no-image250x150.ashx';

const mockProduct: Product = {
  adjustedPrice: 240.17,
  brand: 'Spark Desktop PCs',
  currencySymbol: '$',
  customerAverageRating: null,
  description:
    'Helix’s Spark 6/1TB delivers you expanded storage, a quad-core processor, and the reliability of Helix computing at your fingertips. Finding an affordable tower with the performance you need is now here for your taking.',
  displayName: 'Spark Desktop—4GB Memory, 500GB Hard Drive',
  imageUrls: ['/-/media/Images/Habitat/6042236_01.ashx', '/-/media/Images/Habitat/6042236_02.ashx'],
  listPrice: 240.17,
  productId: '6042236',
  sitecoreId: 'bb515d29-4d7f-4699-1aa4-a4be54c69bad',
  stockStatusName: 'InStock',
  tags: ['desktop', '6gbram', '1tbhd', 'spark', 'tower'],
  variants: [
    {
      adjustedPrice: 240.17,
      brand: 'Spark Desktop PCs',
      currencySymbol: '$',
      customerAverageRating: null,
      description:
        'Helix’s Spark 6/1TB delivers you expanded storage, a quad-core processor, and the reliability of Helix computing at your fingertips. Finding an affordable tower with the performance you need is now here for your taking.',
      displayName: 'Spark Desktop—4GB Memory, 500GB Hard Drive',
      imageUrls: ['/-/media/Images/Habitat/6042236_01.ashx', '/-/media/Images/Habitat/6042236_02.ashx'],
      listPrice: 240.17,
      productId: '6042236',
      properties: { color: '', size: '' },
      stockStatusName: 'InStock',
      tags: ['desktop', '6gbram', '1tbhd', 'spark', 'tower'],
      variantId: '56042236',
    },
  ],
};

const productColors: Record<string, string> = {
  Beige: '#FFF8E7',
  Black: '#000000',
  Blue: '#3387d4',
  Camel: '#C19A6B',
  Charcoal: '#36454F',
  Clay: '#a76b29',
  Green: '#00FF00',
  Grey: '#808080',
  ['Light Blue']: '#ADD8E6',
  Mint: '#3EB489',
  Moss: '#ADDFAD',
  Orange: '#FF7F00',
  Pewter: '#8e9294',
  Pink: '#E34D67',
  Platinum: '#E5E4E2',
  Purple: '#9F00C5',
  Red: '#FF0000',
  Silver: '#C0C0C0',
  Spring: '#00FF7F',
  Stainless: '#E0DFDB',
  Teal: '#008080',
  Violet: '#7F00FF',
  White: '#F5F5F5',
  Yellow: '#FFFF00',
};

const mockStore = configureStore();
const store = mockStore({ shoppingCart: { data: {} } });

it('ProductCard renders correctly', () => {
  const tree = renderer
    .create(
      <Provider store={store}>
        <ProductCard fallbackImageUrl={fallbackImageUrl} product={mockProduct} productColors={productColors}>
          <ThumbnailSlider />
          <ColorVariants />
          <ProductName />
          <ProductRating />
          <Price />
          <AddToCart />
        </ProductCard>
      </Provider>,
    )
    .toJSON();
  expect(tree).toMatchSnapshot();
});
