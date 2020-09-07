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

import { ProductInformationProps, ProductInformationState } from './models';

import './styles.scss';

class ProductInformationComponent extends JSS.SafePureComponent<ProductInformationProps, ProductInformationState> {
  public constructor(props: ProductInformationProps) {
    super(props);

    this.state = {
      selectedTab: 'description',
    };
  }

  protected safeRender() {
    const { product } = this.props.sitecoreContext;
    const { selectedTab } = this.state;
    return (
      <section className="product-description-2">
        <div className="tab">
          <button
            className={'tab-links ' + (selectedTab === 'description' ? 'active' : '')}
            onClick={() => this.setState({ selectedTab: 'description' })}
          >
            Product Description
          </button>
          <button
            className={'tab-links ' + (selectedTab === 'features' ? 'active' : '')}
            onClick={() => this.setState({ selectedTab: 'features' })}
          >
            Features
          </button>
          <button
            className={'tab-links ' + (selectedTab === 'rating' ? 'active' : '')}
            onClick={() => this.setState({ selectedTab: 'rating' })}
          >
            Rating
          </button>
        </div>
        <div className="product-content">
          {selectedTab === 'description' && (
            <div className="description-text">
              <p>{product && product.description}</p>
            </div>
          )}
          {selectedTab === 'features' && (
            <div className="description-features">
              <ul className="feature-list">
                <li>2 GB</li>
                <li>Model XE-C</li>
                <li>Charcoal</li>
                <li>Explorer Edition</li>
              </ul>
            </div>
          )}
          {selectedTab === 'rating' && (
            <div className="description-rating">
              <table className="table-striped table-sm">
                <tbody>
                  <tr>
                    <td>Feature name</td>
                    <td>Best</td>
                  </tr>
                  <tr>
                    <td>Feature name</td>
                    <td>Better</td>
                  </tr>
                  <tr>
                    <td>Feature name</td>
                    <td>Good</td>
                  </tr>
                </tbody>
              </table>
              <table className="table-striped table-lg">
                <thead>
                  <tr>
                    <td>Feature</td>
                    <td>Good</td>
                    <td>Better</td>
                    <td>Best</td>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td>Feature name</td>
                    <td />
                    <td />
                    <td>
                      <i className="fa fa-check" />
                    </td>
                  </tr>
                  <tr>
                    <td>Feature name</td>
                    <td />
                    <td>
                      <i className="fa fa-check" />
                    </td>
                    <td />
                  </tr>
                  <tr>
                    <td>Feature name</td>
                    <td>
                      <i className="fa fa-check" />
                    </td>
                    <td />
                    <td />
                  </tr>
                </tbody>
              </table>
            </div>
          )}
        </div>
      </section>
    );
  }
}

export const ProductInformation2 = JSS.renderingWithContext(ProductInformationComponent);
