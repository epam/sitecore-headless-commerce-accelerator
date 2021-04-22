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

import { storiesOf } from '@storybook/react';

import { Facet } from 'Foundation/Commerce';

import { SidebarFilter } from '../../components/SidebarFilter';

const mockFacet: Facet = {
  displayName: 'Brands',
  foundValues: [
    {
      aggregateCount: 100,
      displayName: 'NextCube Now',
      name: 'NextCube Now',
    },
    {
      aggregateCount: 34,
      displayName: 'Dwell Lifestyle Appliances',
      name: 'Dwell Lifestyle Appliances',
    },
    {
      aggregateCount: 31,
      displayName: 'Optix',
      name: 'Optix',
    },
    {
      aggregateCount: 22,
      displayName: 'Optix',
      name: 'Gesture Tablets',
    },
  ],
  name: 'brand',
  values: [],
};

const mockTagFacet: Facet = {
  displayName: 'Tag',
  foundValues: [
    {
      aggregateCount: 100,
      displayName: 'Clothing',
      name: 'Clothing',
    },
    {
      aggregateCount: 34,
      displayName: 'Accessories',
      name: 'Accessories',
    },
    {
      aggregateCount: 31,
      displayName: 'For Men',
      name: 'For Men',
    },
    {
      aggregateCount: 22,
      displayName: 'Women',
      name: 'Women',
    },
    {
      aggregateCount: 21,
      displayName: 'Fashion',
      name: 'Fashion',
    },
  ],
  name: 'tag',
  values: [],
};

storiesOf('Sidebar Filter', module)
  .add('Checkbox filter', () => (
    <div style={{ width: '270px' }}>
      <SidebarFilter
        facet={mockFacet}
        isApplied={(name: string, value: string) => Math.random() > 0.5}
        onFacetChange={(name: string, value: string, e: React.ChangeEvent<HTMLInputElement>) => {
          console.log('value changed');
        }}
      />
    </div>
  ))
  .add('Tag filter', () => (
    <div style={{ width: '270px' }}>
      <SidebarFilter
        facet={mockTagFacet}
        isApplied={(name: string, value: string) => Math.random() > 0.5}
        onFacetChange={(name: string, value: string, e: React.ChangeEvent<HTMLInputElement>) => {
          console.log('value changed');
        }}
        type="tag"
      />
    </div>
  ));
