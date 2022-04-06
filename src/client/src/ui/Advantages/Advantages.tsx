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

import React, { FC } from 'react';

import Swiper from 'react-id-swiper';

import { GraphQLRenderingWithParams } from 'Foundation/ReactJss';
import { get } from 'lodash';

import { getRenderingSitecoreProps } from 'utils';
import { AdvantagesRenderingParams, AdvantagesDataSource } from 'services/pageContent/models/generated';

import { Item } from './Item/Item';
import { List } from './List/List';

import { cnAdvantages } from './cn';
import './Advantages.scss';

export type AdvantagesProps = GraphQLRenderingWithParams<AdvantagesDataSource, AdvantagesRenderingParams>;

export const Advantages: FC<AdvantagesProps> = (props) => {
  const rendering = getRenderingSitecoreProps(props);

  const advantagesList = get(rendering, ['fields', 'data', 'datasource', 'advantages', 'items'], []);

  const settings = {
    breakpoints: {
      460: {
        slidesPerView: 1.5,
      },
      650: {
        slidesPerView: 2.2,
      },
      730: {
        slidesPerView: 2.3,
      },
      950: {
        slidesPerView: 3.2,
      },
      1035: {
        slidesPerView: 3.5,
      },
      1170: {
        slidesPerView: 4,
      },
    },
    centerInsufficientSlides: true,
    setWrapperSize: true,
    slidesPerView: 1.1,
  };

  return (
    <div className={cnAdvantages()}>
      <List className={cnAdvantages('List')}>
        <Swiper {...settings}>
          {advantagesList.map((item: any) => {
            const itemLinkValues = item.link.jss.value.href;
            return (
              <span key={item.id} className={cnAdvantages('ItemContainer')}>
                <a href={itemLinkValues} className={cnAdvantages('ItemContainer')}>
                  <Item item={item} />
                </a>
              </span>
            );
          })}
        </Swiper>
      </List>
    </div>
  );
};
