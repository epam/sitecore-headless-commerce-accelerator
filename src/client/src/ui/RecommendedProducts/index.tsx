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

import { Text } from '@sitecore-jss/sitecore-jss-react';
import { NavigationLink } from 'ui/NavigationLink';

import { Carousel } from './Carousel';
import { RecommendedProductsProps, RecommendedProductsState } from './models';

import * as JSS from 'Foundation/ReactJss';
import './styles.scss';

import GlassesImage from 'static/images/glasses-for-slider.png';
import { productsMockData } from './constant';

import { Icon } from 'components';

class RecommendedProductsComponent extends JSS.SafePureComponent<RecommendedProductsProps, RecommendedProductsState> {
  public safeRender() {
    const { fields } = this.props;

    return (
      <div className="slider sub-categories recommend-products top-picks">
        <div className="slider-header">
          <div className="slider-header_container">
            <Text field={fields.header} tag="h2" className="title" />
            <p>Lorem ipsum dolor sit amet conse ctetu.</p>
          </div>
        </div>
        <Carousel
          className="gallery-thumbs"
          buttonPreviousText={<Icon icon="icon-angle-left" />}
          buttonNextText={<Icon icon="icon-angle-right" />}
          options={{
            breakpoints: {
              480: {
                slidesPerView: 2,
                spaceBetween: 25,
              },
              1024: {
                slidesPerView: 3,
                spaceBetween: 25,
              },
              1310: {
                slidesPerView: 4,
                spaceBetween: 0,
              },
            },
            slidesPerView: 1,
            spaceBetween: 25,
          }}
        >
          {productsMockData &&
            productsMockData.map((item, index) => (
              <figure key={index} className="swiper-slide item">
                <div className="image-wrap-recommended-product">
                  <div className="product-img-badges">
                    {item.isNew && <span className="purple">New</span>}
                    {item.discount && <span className="pink">{`${item.discount * 100}%`}</span>}
                  </div>
                  <NavigationLink to={`/product/${item.productName}`}>
                    <img src={GlassesImage} alt={index.toString()} />
                  </NavigationLink>{' '}
                </div>
                <div className="item-description">
                  <div className="item-description_title">
                    <div className="item-description_title_brand">{item.productName}</div>
                    <div className="item-description_title_price">
                      <span>{`$${
                        Math.round((item.orgPrice - item.orgPrice * item.discount + Number.EPSILON) * 100) / 100
                      } - `}</span>
                      <span className="item-description_title_price_pink">{`$${item.orgPrice}`}</span>
                    </div>
                  </div>
                </div>
              </figure>
            ))}
        </Carousel>
      </div>
    );
  }
}

export const RecommendedProducts = JSS.renderingWithContextAndDatasource(RecommendedProductsComponent);
