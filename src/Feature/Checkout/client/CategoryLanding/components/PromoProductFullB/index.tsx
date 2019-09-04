//    Copyright 2019 EPAM Systems, Inc.
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

import { Text, withExperienceEditorChromes } from '@sitecore-jss/sitecore-jss-react';
import { PromoProductFullBControlProps, PromoProductFullBControlState } from './models';

import * as Jss from 'Foundation/ReactJss/client';

import './styles.scss';

class PromoProductFullBControl extends Jss.SafePureComponent<PromoProductFullBControlProps, PromoProductFullBControlState> {
  public safeRender() {
    return (
        <figure className="product-promo-full-b">
          <picture>
            <source
              srcSet="https://placeholdit.imgix.net/~text?txtsize=20&txt=placeholder&w=1080&h=608"
              media="(max-width: 767px)"
            />
            <img
              srcSet="https://placeholdit.imgix.net/~text?txtsize=20&txt=placeholder&w=1280&h=590"
              alt="Product Promo Full B default image"
            />
          </picture>
          <figcaption>
            <div className="button-container">
              <Text
                field={{ value: 'Learn More' }}
                tag="button"
                className="btn-product-promo-full-b btn-hide"
              />
              <Text
                field={{ value: 'Add to Cart +' }}
                tag="button"
                className="btn-product-promo-full-b"
              />
            </div>
          </figcaption>
        </figure>
    );
  }
}

export const PromoProductFullB = withExperienceEditorChromes(PromoProductFullBControl);
