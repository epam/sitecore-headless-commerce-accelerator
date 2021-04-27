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

import React from 'react';
import { Provider } from 'react-redux';
import configureStore from 'redux-mock-store';

import { storiesOf } from '@storybook/react';

import { PopularItems, PopularItemsProps } from '../PopularItems';

const mockPopularArticles: PopularItemsProps = {
  componentHeading: 'Recent Projects',
  items: [
    {
      itemHeading: 'T-Shirt and Jeans',
      itemId: 'article1',
      itemImage: 'https://picsum.photos/120/120?random=1',
      itemLink: '/ContactUs?ci=',
      itemTag: 'Photography',
    },
    {
      itemHeading: 'T-Shirt and Jeans',
      itemId: 'article2',
      itemImage: 'https://picsum.photos/120/120?random=2',
      itemLink: '/ContactUs?ci=',
      itemTag: 'Branding',
    },
    {
      itemHeading: 'T-Shirt and Jeans',
      itemId: 'article3',
      itemImage: 'https://picsum.photos/120/120?random=3',
      itemLink: '/ContactUs?ci=',
      itemTag: 'Design',
    },
    {
      itemHeading: 'T-Shirt and Jeans',
      itemId: 'article4',
      itemImage: 'https://picsum.photos/120/120?random=4',
      itemLink: '/ContactUs?ci=',
      itemTag: 'Photography',
    },
  ],
};

const mockStore = configureStore();
const store = mockStore();

storiesOf('Popular Items', module).add('Standart popular items', () => (
  <div style={{ maxWidth: '270px' }}>
    <Provider store={store}>
      <PopularItems componentHeading={mockPopularArticles.componentHeading} items={mockPopularArticles.items} />
    </Provider>
  </div>
));
