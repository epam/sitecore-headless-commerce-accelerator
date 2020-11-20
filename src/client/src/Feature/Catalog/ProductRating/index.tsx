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

import * as React from 'react';

import * as JSS from 'Foundation/ReactJss';

import { ProductRatingProps, ProductRatingState } from './models';
import './styles.scss';

export class ProductRating extends JSS.SafePureComponent<ProductRatingProps, ProductRatingState> {
  protected safeRender() {
    const ratings = this.props.rating || 4;
    const ratedStars = new Array(5).fill(
      <span className="star">
        <i className="fa fa-star-o orange" />
      </span>
    );
    ratedStars.fill(
      (
      <span className="star">
        <i className="fa fa-star-o" />
      </span>
      ),
      ratings
    );
    return (
      <div className="product-rating">
        <div className="items-rating">
          {ratedStars && ratedStars.map((value) => {
            return (
              value
            );
          })}
        </div>
      </div>
    );
  }
}
