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

import * as Jss from 'Foundation/ReactJss';

import { Placeholder } from '@sitecore-jss/sitecore-jss-react';

import { ProductShopOwnState, ProductShopProps } from './models';

import { cnProductShop } from './cn';

import './styles.scss';

export default class ProductShopComponent extends Jss.SafePureComponent<ProductShopProps, ProductShopOwnState> {
  public constructor(props: ProductShopProps) {
    super(props);

    this.state = {
      firstLoad: true,
    };
  }

  public componentDidUpdate() {
    if (!this.props.isLoading) {
      this.setState({ firstLoad: false });
    }
  }

  protected safeRender() {
    const { isLoading } = this.props;
    const { firstLoad } = this.state;

    return (
      <div className={cnProductShop()}>
        <div className="col-md-3">
          <Placeholder name="product-filters" rendering={this.props.rendering} />
        </div>
        <div className="col-md-9">
          <Placeholder name="product-list" rendering={this.props.rendering} />
        </div>
        <div className={isLoading && firstLoad ? 'col-md-12 Loading' : 'hidden'}>
          <div className={'Loading_Spinner Spinner Loading_Spinner_Display'}>
            <div className="Object Object-one" />
            <div className="Object Object-two" />
            <div className="Object Object-three" />
          </div>
        </div>
      </div>
    );
  }
}
