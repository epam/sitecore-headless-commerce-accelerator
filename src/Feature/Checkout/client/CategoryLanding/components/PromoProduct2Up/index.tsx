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

import { PromoProduct2UpProps, PromoProduct2UpState } from './models';

import './styles.scss';

export class PromoProduct2Up extends JSS.SafePureComponent<PromoProduct2UpProps, PromoProduct2UpState> {
  public safeRender() {
    return (
        <section className="promo-product-2up">
            <div className="product-2up-row">
              <div className="product-2up-col">
                <figure className="product-2up-ab">
                  <a href="#">
                    <img
                      alt="Image Alt Text"
                      src="https://placeholdit.imgix.net/~text?txtsize=20&txt=watch&w=640&h=427"
                    />
                    <figcaption>
                      <h2 className="product-2up-text">
                        <span className="text-style1" />
                        <div className="socials" />
                      </h2>
                    </figcaption>
                  </a>
                </figure>
              </div>
              <div className="product-2up-col">
                <figure className="product-2up-ab">
                  <a href="#">
                    <img
                      alt="Image Alt Text"
                      src="https://placeholdit.imgix.net/~text?txtsize=20&txt=watch&w=640&h=427"
                    />
                    <figcaption>
                      <h2 className="product-2up-text">
                        <span className="text-style1" />
                      </h2>
                      <Text
                        field={{ value: 'Select your style' }}
                        tag="button"
                        className="btn-product-2up"
                      />
                      <div className="socials" />
                    </figcaption>
                  </a>
                </figure>
              </div>
            </div>
        </section>
    );
  }
}
