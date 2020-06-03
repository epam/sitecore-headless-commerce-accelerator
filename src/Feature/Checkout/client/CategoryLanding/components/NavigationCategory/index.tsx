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

import { Text } from '@sitecore-jss/sitecore-jss-react';
import * as React from 'react';

import * as JSS from 'Foundation/ReactJss/client';

import { NavigationCategoryProps, NavigationCategoryState } from './models';

import './styles.scss';

export class NavigationCategory extends JSS.SafePureComponent<NavigationCategoryProps, NavigationCategoryState> {
  public safeRender() {
    return (
        <nav className="nav-category">
          <ul className="nav-category-list category-list">
            <li className="category-list-item category-filter category-filter-gender">
              <ul className="filter-options">
                {
                  [
                    {
                      link: '/url',
                      title: 'Men',
                    },
                    {
                      link: '/url',
                      title: 'Women',
                    },
                  ]
                    .map(({title, link}, index) => (
                      <li key={index}>
                        <Text
                          tag="a"
                          className="filter-link"
                          href={link}
                          field={{
                            value: title,
                          }}
                        />
                      </li>
                    ))
                }
              </ul>
            </li>
            <li className="category-list-item category-filter">
                <Text field={{ value: 'Category' }} tag="h2" className="filter-title"/>
                <ul className="filter-options">
                  {
                    [
                      {title: 'Accessories', link: '/url'},
                      {title: 'Clothing', link: '/url'},
                      {title: 'Eyewear', link: '/url'},
                      {title: 'Footwear', link: '/url'},
                      {title: 'Trackers', link: '/url'},
                      {title: 'Watches', link: '/url'},
                    ].map(({title, link}, index) => (
                      <li key={index}>
                        <Text
                          tag="a"
                          className="filter-link"
                          href={link}
                          field={{
                            value: title,
                          }}
                        />
                      </li>
                    ))
                  }
                </ul>
            </li>
            <li className="category-list-item category-filter">
                <Text field={{ value: 'Featured' }} tag="h2" className="filter-title"/>
                <ul className="filter-options">
                  {
                    [
                      {title: 'New', link: '/url'},
                      {title: 'Promotions', link: '/url'},
                      {title: 'Blog', link: '/url'}
                    ].map(({title, link}, index) => (
                      <li key={index}>
                        <Text
                          tag="a"
                          className="filter-link"
                          href={link}
                          field={{
                            value: title,
                          }}
                        />
                      </li>
                    ))
                  }
                </ul>
            </li>
            <li className="category-list-item">
              <Text field={{ value: 'Clearance' }} tag="h2" className="filter-title"/>
            </li>
          </ul>
        </nav>
    );
  }
}
