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

import * as JSS from 'Foundation/ReactJss/client';

import { ProductRatingProps, ProductRatingState } from './models';

export class ProductRating extends JSS.SafePureComponent<ProductRatingProps, ProductRatingState> {
  protected safeRender() {
    return (
      <div className="product-rating">
        <ul className="star-rating">
          <li className="star filled" />
          <li className="star filled" />
          <li className="star filled" />
          <li className="star filled" />
          <li className="star" />
        </ul>
        <span className="review-read">
          <a href="" title="Read reviews" className="review-link">
            Read N reviews
          </a>
        </span>
        <span className="review-write">
          <a href="" title="Write a review" className="review-link">
            Write a review
          </a>
        </span>
      </div>
    );
  }
}
