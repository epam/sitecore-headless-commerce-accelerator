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

import { ProductInformationProps, ProductInformationState } from './models';

import './styles.scss';

class ProductInformationComponent extends JSS.SafePureComponent<ProductInformationProps, ProductInformationState> {
  protected safeRender() {
    const { product } = this.props.sitecoreContext;
    return (
        <section className="product-description">
            <div className="row">
                <div className="col-md-6">
                    <div className="description-text">
                        <h2>Product Description</h2>
                        <p>
                            {product && product.description}
                        </p>
                    </div>
                    <div className="description-features">
                        <h2 >Features</h2>
                        <ul className="feature-list">
                            <li>2 GB</li>
                            <li>Model XE-C</li>
                            <li>Charcoal</li>
                            <li>Explorer Edition</li>
                        </ul>
                    </div>
                </div>
                <div className="col-md-6">
                    <div className="description-rating">
                        <h2>Rating</h2>
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
                                <td><i className="fa fa-check" /></td>
                            </tr>
                            <tr>
                                <td>Feature name</td>
                                <td />
                                <td><i className="fa fa-check" /></td>
                                <td />
                            </tr>
                            <tr>
                                <td>Feature name</td>
                                <td><i className="fa fa-check" /></td>
                                <td />
                                <td />
                            </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </section>
    );
  }
}

export const ProductInformation = JSS.renderingWithContext(ProductInformationComponent);
